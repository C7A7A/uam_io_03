using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Extensions;
using WatchTogetherAPI.Helpers;
using WatchTogetherAPI.Models;
using DayOfWeek = System.DayOfWeek;
using User = WatchTogetherAPI.Models.User;

namespace WatchTogetherAPI.Services.MicrosoftAccountService
{
    public class MicrosoftAccountService : IMicrosoftAccountService
    {
        private IConfiguration _config;
        private readonly WatchTogetherContext _context;
        public MicrosoftAccountService(IConfiguration iConfig, WatchTogetherContext context)
        {
            _config = iConfig;
            _context = context;
        }

        public async Task<string> GetRefreshToken(string code, string state)
        {

            HttpClient Client = new HttpClient();
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(state) as JwtSecurityToken;
            var login = tokenS.Claims.First(claim => claim.Type == "sub").Value;
            var user = await _context.Users.SingleAsync(u => u.login == login);
            string tenant = _config.GetSection("MicrosoftAuthSettings:TenantId").Value;

            var formContent = new FormUrlEncodedContent(new[]
             {
            new KeyValuePair<string, string>("client_id", _config.GetSection("MicrosoftAuthSettings:ClientId").Value),
            new KeyValuePair<string, string>("scope", _config.GetSection("MicrosoftAuthSettings:Scope").Value),
            new KeyValuePair<string, string>("client_secret", _config.GetSection("MicrosoftAuthSettings:ClientSecret").Value),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri",  _config.GetSection("MicrosoftAuthSettings:RedirectUri").Value)
        });

            var response = await Client.PostAsync($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token", formContent);
            var responseString = await response.Content.ReadAsStringAsync();
            MicrosoftToken token = JsonConvert.DeserializeObject<MicrosoftToken>(responseString);

            user.refresh_token = token.refresh_token;
            await _context.SaveChangesAsync();

            return token.refresh_token;
        }

        public async Task<string> GetToken(User user)
        {
            HttpClient Client = new HttpClient();

            string tenant = _config.GetSection("MicrosoftAuthSettings:TenantId").Value;

            var formContent = new FormUrlEncodedContent(new[]
             {
            new KeyValuePair<string, string>("client_id", _config.GetSection("MicrosoftAuthSettings:ClientId").Value),
            new KeyValuePair<string, string>("scope", _config.GetSection("MicrosoftAuthSettings:Scope").Value),
            new KeyValuePair<string, string>("client_secret", _config.GetSection("MicrosoftAuthSettings:ClientSecret").Value),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", user.refresh_token)
            });
            var response = await Client.PostAsync($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token", formContent);
            var responseString = await response.Content.ReadAsStringAsync();
            MicrosoftToken token = JsonConvert.DeserializeObject<MicrosoftToken>(responseString);

            user.refresh_token = token.refresh_token;
            await _context.SaveChangesAsync();
            return token.access_token;
        }
        public async Task<string> GraphGetRequest(string uri, User user, Dictionary<string, string> AdditionalHeaders = null, Dictionary<string, string> QueryParameters = null)
        {
            HttpClient Client = new HttpClient();
            string parameters = null;
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(user));
            if (AdditionalHeaders != null)
            {
                foreach (KeyValuePair<string, string> entry in AdditionalHeaders)
                {
                    Client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }
            if (QueryParameters != null)
            {
                parameters = "?";
                var paramss = new Collection<string>();
                foreach (KeyValuePair<string, string> entry in QueryParameters)
                {
                    var param = $"{entry.Key}={entry.Value}";
                    paramss.Add(param);
                }
                parameters += String.Join('&', paramss.ToArray());
                System.Diagnostics.Debug.WriteLine(uri + parameters);
            }

            return await Client.GetStringAsync(uri + parameters);
        }
        public async Task<string> GraphPostRequest(string uri, User user, HttpContent content, Dictionary<string, string> AdditionalHeaders = null)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(user));
            if (AdditionalHeaders != null)
            {
                foreach (KeyValuePair<string, string> entry in AdditionalHeaders)
                {
                    Client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }
            var response = await Client.PostAsync(uri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        public async Task<string> GetUser(User user)
        {
            return await GraphGetRequest("https://graph.microsoft.com/v1.0/me", user);

        }
        public async Task<string> GetCalendar(User user)
        {
            return await GraphGetRequest("https://graph.microsoft.com/v1.0/me/calendar", user);
        }

        public async Task<ICollection<MicrosoftCalendarEvent>> GetCalendarEvents(User user, string startTime = null, string endTime = null)
        {
            var uri = "https://graph.microsoft.com/v1.0/me/calendar";
            var parameters = new Dictionary<string, string>();
            if (startTime != null && endTime != null)
            {

                uri += "/calendarView";
                if (startTime != null)
                    parameters.Add("startDateTime", startTime);
                if (endTime != null)
                    parameters.Add("endDateTime", endTime);


            }
            else
            {
                uri += "/events";
            }
            System.Diagnostics.Debug.WriteLine(uri);
            parameters.Add("top", "1000");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Prefer", "outlook.timezone=\"Central European Standard Time\"");
            var events = await GraphGetRequest(uri, user, headers, parameters);
            dynamic eventsJson = JsonConvert.DeserializeObject<dynamic>(events);
            ICollection<MicrosoftCalendarEvent> resp = new Collection<MicrosoftCalendarEvent>();
            var id = 0;

            foreach (dynamic entry in eventsJson["value"])
            {
                var evnt = new MicrosoftCalendarEvent()
                {
                    id = id++,
                    subject = entry["subject"],
                    startTime = entry["start"]["dateTime"],
                    endTime = entry["end"]["dateTime"]
                };
                resp.Add(evnt);
            }
            return resp;
        }
        public async Task<IEnumerable<DateTime>> GetMeetingTime(ICollection<User> participants)
        {
            IEnumerable<Interval> eventsIntervals = new Collection<Interval>();
            IEnumerable<Interval> prefferedIntervals = new Collection<Interval>();
            var defaultIntervals = new Collection<Interval>();
            var todayTz = new DateTimeOffset(DateTime.Today, TimeZoneInfo.Local.GetUtcOffset(DateTime.Today));
            var today = todayTz.DateTime;
            for (int i = 0; i <= 14; i++)
            {
                defaultIntervals.Add(new Interval()
                {
                    From = today.AddDays(i).AddHours(17),
                    To = today.AddDays(i).AddHours(24)
                });
            }


            for (int i = 0; i <= 14; i++)
            {
                IEnumerable<Interval> prefferedIntervalsDay = new Collection<Interval>();

                foreach (User participant in participants)
                {
                    var partprefhours = await _context.PreferredHours.Where(u => u.userID == participant.id).ToListAsync();
                    if (partprefhours != null)
                    {
                        var dayOfWeek = today.AddDays(i).DayOfWeek;
                        var dayOfWeekFirstThreeLetters = Enum.GetName(typeof(DayOfWeek), dayOfWeek).Substring(0, 3).ToLower();

                        //System.Diagnostics.Debug.WriteLine(dayOfWeekFirstThreeLetters);
                        /*System.Diagnostics.Debug.WriteLine(participant.prefferedhours.Count() + participant.login);
                        foreach(string k in participant.prefferedhours.Select(x => x.day))
                        System.Diagnostics.Debug.WriteLine(participant.login + k);*/
                        var pr = partprefhours.SingleOrDefault(p => p.day == dayOfWeekFirstThreeLetters);
                        //System.Diagnostics.Debug.WriteLine(pr + participant.login);
                        if (pr != null)
                        {
                            System.Diagnostics.Debug.WriteLine(pr.EndTime);
                            var from = new
                            {
                                hrs = pr.StartTime.Substring(0, 2),
                                mnt = pr.StartTime.Substring(3, 2)
                            };
                            var to = new
                            {
                                hrs = pr.EndTime.Substring(0, 2),
                                mnt = pr.EndTime.Substring(3, 2)
                            };
                            var inter = new Interval()
                            {
                                From = today.AddDays(i).AddHours(Int32.Parse(from.hrs)).AddMinutes(Int32.Parse(from.mnt)),
                                To = today.AddDays(i).AddHours(Int32.Parse(to.hrs)).AddMinutes(Int32.Parse(to.mnt)),
                            };
                            prefferedIntervalsDay = prefferedIntervalsDay.Append(inter);
                        }

                    }
                }
                foreach(Interval k in prefferedIntervalsDay)
                {
                    System.Diagnostics.Debug.WriteLine($"{k.From} {k.To}");
                }
                prefferedIntervalsDay = prefferedIntervalsDay.OrderBy(interval => interval.From).IntersectionOverlapping();
                foreach (Interval k in prefferedIntervalsDay)
                {
                    System.Diagnostics.Debug.WriteLine($"XX{k.From} {k.To}");
                }
                prefferedIntervals = prefferedIntervals.Concat(prefferedIntervalsDay);
            }

            var startTime = DateTime.Today.ToString("s", CultureInfo.InvariantCulture);
            var endTime = DateTime.Today.AddDays(14).ToString("s", CultureInfo.InvariantCulture);
            foreach (User participant in participants)
            {
                if (participant.refresh_token != null)
                {
                    var events = await GetCalendarEvents(participant, startTime, endTime);
                    var userIntervals = events.Select(e => new Interval()
                    {
                        From = e.startTime.Value,
                        To = e.endTime.Value
                    });
                    eventsIntervals = eventsIntervals.Concat(userIntervals);

                }
            }
            
            var mergedEventIntervals = eventsIntervals.OrderBy(interval => interval.From).MergeOverlapping();
            var mergedDefaultIntervals = defaultIntervals.OrderBy(interval => interval.From).MergeOverlapping();
            var movieTime = 180;
            var offset = 0;
            var selectedFromPreferred = SelectTime(prefferedIntervals, mergedEventIntervals, movieTime, offset);
            var selectedFromDefault = SelectTime(mergedDefaultIntervals, mergedEventIntervals, movieTime, offset);
            var selected = selectedFromPreferred.Concat(selectedFromDefault);
            return selected;
        }
        private ICollection<DateTime> SelectTime(IEnumerable<Interval> mergedPreferredIntervals, IEnumerable<Interval> mergedEventIntervals, int movieTime, int offset)
        {
            var propositions = new Collection<DateTime>();
            var prefHours = IntervalHelpers.doSweep(mergedPreferredIntervals.ToList(), mergedEventIntervals.ToList());
            var ifnow = 0;
            foreach (Interval i in prefHours)
            {

                if (DateTime.Compare(i.From, DateTime.Now) < 0)
                {
                    ifnow = 15 + (int)DateTime.Now.Subtract(i.From).TotalMinutes;
                }
                else
                {
                    ifnow = 0;
                }
                if (i.From.AddMinutes(offset + movieTime + ifnow) <= i.To)
                {
                    propositions.Add(i.From.AddMinutes(offset + ifnow));
                }
            }
            return propositions;
        }
        public async Task<string> CreateEvent(User user, DateTime startTime, DateTime endTime, string name, string content)
        {
            var userString = await GetUser(user);
            var userJson = JsonConvert.DeserializeObject<dynamic>(userString);

            var subject = name;
            var body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = content
            };
            var Start = new DateTimeTimeZone
            {
                DateTime = startTime.ToString("s", CultureInfo.InvariantCulture),
                TimeZone = "Central European Standard Time"
            };
            var End = new DateTimeTimeZone
            {
                DateTime = endTime.ToString("s", CultureInfo.InvariantCulture),
                TimeZone = "Central European Standard Time"
            };
            var Location = new Location
            {
                DisplayName = "Obejrzyjmy To Razem"
            };
            var Attendees = new List<Attendee>()
            {
                new Attendee
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = userJson["userPrincipalName"],
                        Name = userJson["displayName"]
                    },
                Type = AttendeeType.Required
                }
            };
            var reqObj = new
            {
                subject = subject,
                body = body,
                start = Start,
                end = End,
                location = Location,
                attendees = Attendees
            };
            var reqJson = JsonConvert.SerializeObject(reqObj);
            var reqContent = new StringContent(reqJson, Encoding.UTF8, "application/json");
            return await GraphPostRequest("https://graph.microsoft.com/v1.0/me/calendar/events", user, reqContent);
        }
    }
}
