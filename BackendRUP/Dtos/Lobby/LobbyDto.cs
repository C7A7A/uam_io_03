using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Dtos.Movie;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Dtos.Lobby
{
    public class LobbyDto
    {
        public string Name { get; set; }
        public Participant Host { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public string Status { get; set; }
        public DateTime? Term { get; set; }
        public ICollection<GetMovieDto> ProposedMovies { get; set; }
        public GetMovieDto SelectedMovie { get; set; }
        public Snack Snack { get; set; }
    }
}
