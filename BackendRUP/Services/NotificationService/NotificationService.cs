using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Models;


namespace WatchTogetherAPI.Services.NotificationService
{
    public class NotificationService:INotificationService
    {
        private readonly WatchTogetherContext _context;
        public NotificationService(WatchTogetherContext context)
        {
            _context = context;
        }
        public async Task CreateNotification(User receiver, int type, string content,  User from = null, Lobby lobby = null)
        {
            var notification = new Notification()
            {
                User = receiver,
                Type = type,
                Content = content,
                From = from,
                Lobby = lobby
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
        
        public async Task<ICollection<Notification>> GetUserNotifications(User user)
        {
            return await _context.Notifications.Include(n => n.User).Include(n=>n.From).Include(l => l.Lobby).Where(n => n.User == user).ToListAsync();
        }
    }
}
