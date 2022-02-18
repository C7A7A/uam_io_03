using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Services.NotificationService
{
    public interface INotificationService
    {
        Task<ICollection<Notification>> GetUserNotifications(User user);
        Task CreateNotification(User receiver, int type, string content, User from = null, Lobby lobby = null);
    }
}
