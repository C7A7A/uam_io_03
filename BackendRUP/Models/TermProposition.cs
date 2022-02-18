using System;
using System.Collections.Generic;

namespace WatchTogetherAPI.Models
{
    public class TermProposition
    {
        public TermProposition()
        {
            alreadyVoted = new HashSet<User>();
        }
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public ICollection<User> alreadyVoted { get; set; }
        public int Order { get; set; }
    }
}