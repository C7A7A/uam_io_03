using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class UserParameters
    {
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [Required]
        [MaxLength(50)]
        public string surname { get; set; }
        [Required]
        [MaxLength(150)]
        public string email { get; set; }

        [Required]
        [MaxLength(250)]
        public string[] preferredgenres { get; set; }

        public string[] watchedmovies { get; set; }
    }
}
