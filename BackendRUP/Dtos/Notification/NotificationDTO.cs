using System.ComponentModel.DataAnnotations;

namespace WatchTogetherAPI.Dtos.Notification
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Type { get; set; }
        public string From { get; set; }
        public string LobbyName { get; set; }
    }
}
