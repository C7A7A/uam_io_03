using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class MovieVotes
    {
        public MovieVotes(){
            alreadyVoted = new HashSet<User>();
        }
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int votes { get; set; }
        public ICollection<User> alreadyVoted { get; set; }
    }
}
