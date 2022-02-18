using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Dtos.Lobby
{
    public class UserLobbiesDto
    {
        public string Name { get; set; }
        public Participant Host { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public string Status { get; set; }
    }
}
