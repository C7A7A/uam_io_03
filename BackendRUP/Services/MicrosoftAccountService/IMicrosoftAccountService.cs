using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Services.MicrosoftAccountService
{
    public interface IMicrosoftAccountService
    {
        Task<string> GetRefreshToken(string code, string state);
        Task<string> GetToken(User user);
        Task<string> GetUser(User user);
        Task<string> GetCalendar(User user);
        Task<ICollection<MicrosoftCalendarEvent>> GetCalendarEvents(User user, string startTime = null, string endTime = null);
        Task<string> GraphGetRequest(string uri, User user, Dictionary<string, string> AdditionalHeaders = null, Dictionary<string, string> QueryParameters = null);
        Task<string> GraphPostRequest(string uri, User user, HttpContent content, Dictionary<string, string> AdditionalHeaders);
        Task<IEnumerable<DateTime>> GetMeetingTime(ICollection<User> participants);
        Task<string> CreateEvent(User user, DateTime startTime, DateTime endTime, string name, string content);
    }
}
