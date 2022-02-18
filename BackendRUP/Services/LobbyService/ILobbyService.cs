using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Services.LobbyService
{
    public interface ILobbyService
    {
        Task<ICollection<User>> GetParticipantsFromFriends(User user);
        Task<ICollection<MovieVotes>> GetMovies(ICollection<User> participants);
        //Task<Dictionary<Genre, double>> GetMovies(ICollection<User> participants);
        Task<ICollection<Participant>> GetParticipants(Lobby lobby);
        Task VoteOnMovie(Lobby lobby, int movieid, User user, bool vote);
        Task<ICollection<Movie>> MoviesToVote(User user, Lobby lobby);
        Task<ICollection<TermProposition>> GetMeetingTime(ICollection<User> participants);
        Task VoteOnTime(Lobby lobby, User user, bool vote);
        Task<ICollection<Lobby>> GetUserLobbies(User user);
    }
}
