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
using WatchTogetherAPI.Services.LobbyService;
using AutoMapper;
using WatchTogetherAPI.Dtos.Lobby;

namespace WatchTogetherAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WatchTogetherContext _context;
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public UserController(WatchTogetherContext context, ILobbyService lobbyService, IMapper mapper)
        {
            _context = context;
            _lobbyService = lobbyService;
            _mapper = mapper;
        }


        // GET: /user
        // GET: /user?q=<QUERY_STRING>

        //QUERY_STRING = login || name + ' ' + surname
        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery(Name = "q")] string userName)
        {
            if (userName == null)
            {
                var allUsers = await _context.Users
                    .Where(u => u.is_deleted == false)
                    .Select(u=>new { u.name,u.surname,u.email,u.login})
                    .ToListAsync();
                return Ok(allUsers);
            }          
            
            var user = await _context.Users
                    .Where(u => u.login.Contains(userName) || (u.name + " " + u.surname).Contains(userName))
                    .Select(u=>new {u.name,u.surname,u.login})
                    .ToListAsync();
            if (user.Any())
            {
                return Ok(user);
            }
            return BadRequest();

            
        }

        [Authorize(Policy="ApiUser")]
        [HttpGet("lobbies")]
        public async Task<ActionResult> GetUserLobbies()
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lobbies = await _lobbyService.GetUserLobbies(user);
            return Ok(_mapper.Map<ICollection<Lobby>, ICollection<UserLobbiesDto>>(lobbies));
        }

    }
}
