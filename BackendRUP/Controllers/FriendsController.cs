using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiUser")]
    public class FriendsController : ControllerBase
    {
        private readonly WatchTogetherContext _context;



        public FriendsController(WatchTogetherContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetFriends()
        {
            var user = new User();
            var identity = User.Identity;
            user = _context.Users.Single(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFriends = await _context.Users
                .Where(u => u.is_deleted == false && u.id == user.id)
                .Select(u => new { u.friends })
                .FirstOrDefaultAsync();
            if (userFriends == null || userFriends.friends == "" || userFriends.friends == null)
                return Ok();

            var listFriendsLogin = userFriends.friends.Split(';');

            var listFriends = await _context.Users
                    .Where(u => listFriendsLogin.Contains(u.login))
                    .Select(u => new { u.name, u.surname, u.login })
                    .ToListAsync();

            return Ok(listFriends);
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddFriends(string login)
        {
            var user = new User();
            var identity = User.Identity;
            user = _context.Users.Single(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userAdding = await _context.Users
               .Where(u => u.id == user.id)
               .FirstOrDefaultAsync();

            var userAdded = await _context.Users
                .Where(u => u.login == login)
                .FirstOrDefaultAsync();

            if (userAdding == null)
                return Unauthorized();
            if (userAdded == null)
                return NotFound();
            if (userAdding.friends != null && userAdding.friends.Contains(login))
                return BadRequest();
            if (userAdded == userAdding)
                return BadRequest();

            if (userAdding.friends == null || userAdding.friends == "")
            {
                userAdding.friends = userAdded.login;
            }
            else
            {
                var userFriends = userAdding.friends.Split(";").ToList();
                userFriends.Add(userAdded.login);
                userAdding.friends = String.Join(";", userFriends);
            }

            if (userAdded.friends == null || userAdded.friends == "")
            {
                userAdded.friends = userAdding.login;
            }
            else
            {
                var userFriends = userAdded.friends.Split(";").ToList();
                userFriends.Add(userAdding.login);
                userAdded.friends = String.Join(";", userFriends);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult<User>> RemoveFriends(string login)
        {
            var user = new User();
            var identity = User.Identity;
            user = _context.Users.Single(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userRemoving = await _context.Users
               .Where(u => u.id == user.id)
               .FirstOrDefaultAsync();

            var userRemoved = await _context.Users
                .Where(u => u.login == login)
                .FirstOrDefaultAsync();

            if (userRemoving == null)
                return Unauthorized();
            if (userRemoved == null)
                return BadRequest();
            if (!userRemoving.friends.Contains(login))
                return NotFound();
            if (userRemoved == userRemoving)
                return BadRequest();
            var userFriends = userRemoving.friends.Split(";").ToList();
            userFriends.Remove(userRemoved.login);
            userRemoving.friends = String.Join(";", userFriends);

            userFriends = userRemoved.friends.Split(";").ToList();
            userFriends.Remove(userRemoving.login);
            userRemoved.friends = String.Join(";", userFriends);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}