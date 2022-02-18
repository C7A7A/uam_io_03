using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class RegistrationViewModel
    {
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
        [Required]
        [MaxLength(100)]
        public string password { get; set; }
    }
}
