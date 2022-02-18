using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WatchTogetherAPI.Models
{
    public class User
    {
        public User()
        {
            LobbiesHosted = new HashSet<Lobby>(); 
            LobbiesParticipated = new HashSet<Lobby>();
            VotedForMovie = new HashSet<MovieVotes>();
            VotedForTime = new HashSet<TermProposition>();
        }

        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string login { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [Required]
        [MaxLength(50)]
        public string surname { get; set; }
        [Required]
        [MaxLength(150)]
        public string email { get; set; }

        [MaxLength(250)]
        public string preferredgenres { get; set; }
        public string watchedmovies { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool is_deleted { get; set; }
        [MaxLength(250)]
        public string friends { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }

        public string refresh_token { get; set; }

        public virtual ICollection<Lobby> LobbiesParticipated { get; set; }
        public virtual ICollection<Lobby> LobbiesHosted { get; set; }
        public virtual ICollection<MovieVotes> VotedForMovie { get; set; }
        public virtual ICollection<TermProposition> VotedForTime { get; set; }

    }

  
}
