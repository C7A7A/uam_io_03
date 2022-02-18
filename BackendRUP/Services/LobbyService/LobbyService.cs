using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Dtos.Lobby;
using WatchTogetherAPI.Models;
using WatchTogetherAPI.Services.MicrosoftAccountService;
using WatchTogetherAPI.Services.NotificationService;
using WatchTogetherAPI.Helpers;

namespace WatchTogetherAPI.Services.LobbyService
{
    public class LobbyService : ILobbyService
    {
        private readonly WatchTogetherContext _context;
        private readonly IMicrosoftAccountService _microsoftAccountService;
        private readonly INotificationService _notificationService;
        public LobbyService(WatchTogetherContext context, INotificationService notificationService, IMicrosoftAccountService microsoftAccountService)
        {
            _context = context;
            _microsoftAccountService = microsoftAccountService;
            _notificationService = notificationService;
        }
        public async Task<ICollection<User>> GetParticipantsFromFriends(User user)
        {
            Random gen = new Random();

            var friendsCollection = new Collection<User>();
            friendsCollection.Add(user);
            if (user.friends == null || user.friends == "") return friendsCollection;
            var friendsArray = user.friends.Split(";").ToList();
            var cnt = 0;
            while(cnt < 4 && friendsArray.Count > 0) { 
                var x = gen.Next(0, friendsArray.Count);
                var friendUser = await _context.Users.SingleAsync(u => u.login == friendsArray[x]);
                friendsArray.Remove(friendsArray[x]);
                friendsCollection.Add(friendUser);
                cnt++;
            }
            return friendsCollection;
        }
        //public async Task<Dictionary<Genre, double>> GetMovies(ICollection<User> participants)

        public async Task<ICollection<MovieVotes>> GetMovies(ICollection<User> participants)
        {
            Random generator = new Random();
            var movieVotes = new Collection<MovieVotes>();
            var genresChance = new Dictionary<Genre, double>();
            foreach (User participant in participants)
            {
                if(participant.preferredgenres != null && participant.preferredgenres != "") { 
                var participantGenres = participant.preferredgenres.Split(",");
                    foreach (string genre in participantGenres)
                    {
                        var genreGenre = await _context.Genres.SingleAsync(g => g.Name == genre);
                        //genreGenre.Movies_Genres = null;
                        if (genresChance.ContainsKey(genreGenre))
                        {
                            genresChance[genreGenre] = genresChance[genreGenre] + 1;
                        }
                        else
                        {
                            genresChance.Add(await _context.Genres.SingleAsync(g => g.Name == genre), 1);
                        }
                    }
                }
            }
            var watchedMovies = new HashSet<int>();
            foreach(User part in participants)
            {
                if (part.watchedmovies != null && part.watchedmovies != "")
                {
                    var ids = Array.ConvertAll(part.watchedmovies.Split(","), x => int.Parse(x));
                    var watchedMoviesPart = new HashSet<int>(ids);
                    watchedMovies.UnionWith(watchedMoviesPart);
                }
            }
            var movies = new Collection<Movie>();
            if (genresChance.Count != 0) {
                double sum = genresChance.Sum(v => v.Value);

                double chc = 0.0;
                foreach (KeyValuePair<Genre, double> genre in new Dictionary<Genre, double>(genresChance))
                {
                    genresChance[genre.Key] = chc += (genre.Value / sum);
                }

                var orderedChance = from entry in genresChance orderby entry.Value ascending select entry;
                for (int i = 0; i < 5; i++) {
                    Movie movie = null;
                    
                        var ran = generator.NextDouble();
                        
                        foreach (KeyValuePair<Genre, double> chance in orderedChance)
                        {
                            if (ran <= chance.Value)
                            {
                            var movieList = await _context.Movies_Genres
                                .Where(mg => (!movies.Contains(mg.Movie) && !watchedMovies.Contains(mg.Movie.Id) && mg.Genre == chance.Key))
                                .OrderByDescending(mg => mg.Movie.Rating)
                                .Take(30)
                                .Select(mg => mg.Movie).ToListAsync();
                            movie = movieList.OrderBy(m => Guid.NewGuid()).FirstOrDefault();

                                if(movie == null)
                                {
                                movieList = await _context.Movies.Where(m => (!movies.Contains(m) && !watchedMovies.Contains(m.Id))).OrderByDescending(m => m.Rating).Take(30).ToListAsync();
                                movie = movieList.OrderBy(m => Guid.NewGuid()).FirstOrDefault();
                                }
                            }
                        }
                    movies.Add(movie);
                }

            }
            else
            {
                for(int i=0; i<5; i++)
                {
                    var movieList = await _context.Movies.Where(m => (!movies.Contains(m) && !watchedMovies.Contains(m.Id))).OrderByDescending(m => m.Rating).Take(30).ToListAsync();
                    var movie = movieList.OrderBy(m => Guid.NewGuid()).FirstOrDefault();
                    movies.Add(movie);
                }
            }
            foreach(Movie movie in movies)
            {
                movieVotes.Add(new MovieVotes()
                {
                    Movie = movie,
                    votes = 0,
                    alreadyVoted = new Collection<User>()

                });
            }

            return movieVotes;
            //return genresChance;
        }
        public async Task<ICollection<Participant>> GetParticipants(Lobby lobby)
        {
            var participants = new Collection<Participant>();
            foreach(User user in lobby.Participants)
            {
                participants.Add(new Participant()
                {
                    login = user.login,
                    name = user.name,
                    surname = user.surname
                });
            }
            return participants;
        }
        public async Task VoteOnMovie(Lobby lobby, int movieid, User user, bool vote)
        {
            if (lobby.Status == LobbyStatus.AKF)
            {
                var proposedMovies = lobby.ProposedMovies;
                var movie = proposedMovies.First(mv => mv.Movie.Id == movieid);
                if (movie.alreadyVoted != null && !movie.alreadyVoted.Contains(user))
                {
                    if (vote == true)
                    {
                        proposedMovies.Single(mv => mv.Movie.Id == movieid).votes++;
                    }
                    movie.alreadyVoted.Add(user);
                }

                lobby.ProposedMovies = proposedMovies;
                bool ok = true;
                foreach(MovieVotes mv in lobby.ProposedMovies)
                {
                    if (!lobby.Participants.All(it => mv.alreadyVoted.Contains(it)))
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    lobby.SelectedMovie = lobby.ProposedMovies.Where(l => l.votes == lobby.ProposedMovies.Max(l => l.votes)).Select(l => l.Movie).FirstOrDefault();
                    lobby.Status = LobbyStatus.ZAK;
                    var snacks = await _context.Snacks.ToListAsync();
                    lobby.Snack = snacks.OrderBy(s => Guid.NewGuid()).FirstOrDefault();
                    foreach (User part in lobby.Participants)
                    {
                        await _notificationService.CreateNotification(part, 15, $"Film został wybrany, wydarzenie w kalendarzu zostało utworzone. Miłego seansu!", null, lobby);
                        if(part.refresh_token != null)
                        await _microsoftAccountService.CreateEvent(part, lobby.Term, lobby.Term.AddMinutes(lobby.SelectedMovie.Length), lobby.Name + ": " + UriHelper.DecodeUrlString(lobby.SelectedMovie.Title), "Obejrzyjmy To Razem");
                        if (part.watchedmovies != null && part.watchedmovies != "")
                        {
                            var ids = Array.ConvertAll(part.watchedmovies.Split(","), x => int.Parse(x));
                            var watchedMoviesPart = new List<int>(ids);
                            watchedMoviesPart.Add(lobby.SelectedMovie.Id);
                            part.watchedmovies = String.Join(",", watchedMoviesPart);
                        }
                        else
                        {
                            var watchedMoviesPart = new List<int>();
                            watchedMoviesPart.Add(lobby.SelectedMovie.Id);
                            part.watchedmovies = String.Join(",", watchedMoviesPart);

                        }
                        _context.Users.Update(part);
                    }
                    }
                _context.Lobbies.Update(lobby);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Movie>> MoviesToVote(User user, Lobby lobby)
        {
            var moviesToVote = new Collection<Movie>();
            foreach(MovieVotes mv in lobby.ProposedMovies)
            {
                if (!mv.alreadyVoted.Contains(user))
                {
                    moviesToVote.Add(mv.Movie);
                }
            }
            return moviesToVote;
        }

        public async Task<ICollection<TermProposition>> GetMeetingTime(ICollection<User> participants)
        {
            var times =  await _microsoftAccountService.GetMeetingTime(participants);
            var propositions = new List<TermProposition>();
            var order = 0;
            foreach(DateTime time in times)
            {
                propositions.Add(new TermProposition()
                {
                    Time = time,
                    Order = order++
                });
            }
            return propositions;
        }
        public async Task VoteOnTime(Lobby lobby, User user, bool vote)
        {
            if (lobby.Status == LobbyStatus.AKT)
            {
                var ind = lobby.ProposedTermIndex;
                var proposedTime = lobby.ProposedTerms.OrderBy(e => e.Order).ElementAt(ind);


                if (!proposedTime.alreadyVoted.Contains(user))
                {
                    if (vote == true)
                    {
                        proposedTime.alreadyVoted.Add(user);
                        if (lobby.Participants.All(it => proposedTime.alreadyVoted.Contains(it)))
                        {
                            lobby.Term = proposedTime.Time;
                            lobby.Status = LobbyStatus.AKF;
                            //powiadomienie o wybraniu terminu
                            foreach(User part in lobby.Participants)
                            await _notificationService.CreateNotification(part, 12, $"Zaproponowany termin spotkania został zaakceptowany przez każdego uczestnika.", null, lobby);

                        }
                    }
                    else
                    {
                        if (ind == lobby.ProposedTerms.Count() - 1)
                        {
                            lobby.Status = LobbyStatus.ZAK;
                            //powiadomienie o braku dostepnych terminow
                            foreach(User part in lobby.Participants)
                            await _notificationService.CreateNotification(part, 14, $"Nie można znaleźć żadnego pasującego terminu. Lobby zostaje zamknięte.", null, lobby);

                        }
                        else
                        {
                            lobby.ProposedTermIndex++;
                            //powiadomienie wybraniu o odrzuceniu terminu i propozycja kolejnego
                            foreach (User part in lobby.Participants)
                            await _notificationService.CreateNotification(part, 13, $"Jeden z użytkowników nie zgodził się na proponowany termin spotkania. Zaproponowany został inny termin.", null, lobby);

                        }
                    }
                }

                _context.Lobbies.Update(lobby);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ICollection<Lobby>> GetUserLobbies(User user)
        {
            var lobbies = await _context.Lobbies
                .Include(l => l.Participants)
                .Where(l => l.Participants.Contains(user)).ToListAsync();
            return lobbies;
        }   
    }
}
