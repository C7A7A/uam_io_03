using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class PreferredHours
    {
        public int id { get; set; }
        public int userID { get; set; }
        [Required]
        [MaxLength(3)]
        public string day { get; set; }
        [Required]
        [MaxLength(5)]
        public string StartTime { get; set; }
        [Required]
        [MaxLength(5)]
        public string EndTime { get; set; }

    }
}
