using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class Movie
    {

        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Release date"), DisplayFormat(DataFormatString = "{yyyy}",
           ApplyFormatInEditMode = true)]
        public DateTime Release_date { get; set; }
        [Required]
        public int Length { get; set; }
        [Required, MaxLength(150)]
        public string Poster_path { get; set; }
        public string Stills_path { get; set; }
        [Required]
        public float Rating { get; set; }
        public IList<Movies_genres> Movies_Genres { get; set; }
    }
}
