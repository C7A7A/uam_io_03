using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Dtos.Notification;
using WatchTogetherAPI.Models;
using WatchTogetherAPI.Services.NotificationService;
using AutoMapper;

namespace WatchTogetherAPI.Controllers
{
    [Authorize(Policy ="ApiUser")]
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly WatchTogetherContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(WatchTogetherContext context, IMapper mapper, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        // GET: Notifications
        [HttpGet]
        public async Task<ActionResult<ICollection<NotificationDTO>>> GetNotifications()
        {

            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var notifications =  await _notificationService.GetUserNotifications(user);
            return new OkObjectResult(_mapper.Map<ICollection<Notification>, ICollection<NotificationDTO>>(notifications));
        }

        // GET: Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }
            return _mapper.Map<Notification, NotificationDTO>(notification);
        }

        [HttpPost("{login}")]
        public async Task<ActionResult<NotificationDTO>> PostNotification(string login, NotificationDTO notificationDTO)
        {

            var user = await _context.Users.SingleOrDefaultAsync(u => u.login == login);
            var from = await _context.Users.SingleOrDefaultAsync(u => u.login == notificationDTO.From);
            var lobby = await _context.Lobbies.SingleOrDefaultAsync(l => l.Name == notificationDTO.LobbyName);

            if ((from == null && notificationDTO.From != null) ||
                (lobby == null && notificationDTO.LobbyName != null) || 
                notificationDTO.Type < 0
                )
            {
                return BadRequest();
            }
            await _notificationService.CreateNotification(user, notificationDTO.Type, notificationDTO.Content, from, lobby);

            return Ok();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

    }
}
