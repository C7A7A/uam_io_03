using System.ComponentModel.DataAnnotations;

namespace WatchTogetherAPI.Models
{ 
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required, MinLength(1)]
        public string Content { get; set; }
        [Required]
        public int Type { get; set; }
        public User From { get; set; }
        public Lobby Lobby { get; set; }

    }
}
