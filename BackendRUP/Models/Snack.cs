using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class Snack
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
