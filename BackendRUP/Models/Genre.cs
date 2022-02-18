using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class Genre
    {

        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public IList<Movies_genres> Movies_Genres { get; set; }
    }
}
