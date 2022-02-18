using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTogetherAPI.Models
{
    public enum LobbyStatus
    {
        AKT,
        AKF,
        ZAK
    }
    public class Lobby
    {
        public Lobby()
        {
            Participants = new HashSet<User>();
            ProposedTerms = new List<TermProposition>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? HostId { get; set; }
        public User Host { get; set; }


        public ICollection<User> Participants { get; set; }
        [Required]
        public LobbyStatus Status { get; set; }

        public Movie SelectedMovie { get; set; }
        public ICollection<MovieVotes> ProposedMovies { get; set; }

        public DateTime Term { get; set; }
        public ICollection<TermProposition> ProposedTerms { get; set; }
        public int ProposedTermIndex { get; set; }
        public Snack Snack { get; set; }

    }

}
