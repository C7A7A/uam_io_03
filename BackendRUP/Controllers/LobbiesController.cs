using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Dtos.Lobby;
using WatchTogetherAPI.Dtos.Movie;
using WatchTogetherAPI.Models;
using WatchTogetherAPI.Services.LobbyService;
using WatchTogetherAPI.Services.NotificationService;

namespace WatchTogetherAPI.Controllers
{

    [Authorize(Policy = "ApiUser")]
    [Route("lobby")]
    [ApiController]
    public class LobbiesController : ControllerBase
    {
        private readonly WatchTogetherContext _context;
        private readonly ILobbyService _lobbyService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public LobbiesController(WatchTogetherContext context, ILobbyService lobbyService, IMapper mapper, INotificationService notificationService)
        {
            _context = context;
            _lobbyService = lobbyService;
            _mapper = mapper;
            _notificationService = notificationService;
        }


        // GET: api/Lobbies/name
        [HttpGet("{name}")]
        public async Task<ActionResult<LobbyDto>> GetLobby(string name)
        {
            var lobby = await _context.Lobbies
                .Include(l => l.Snack)
                .Include(l => l.Participants)
                .Include(l => l.ProposedTerms)
                .Include(l => l.ProposedMovies)
                .ThenInclude(mv => mv.Movie)
                .ThenInclude(mv => mv.Movies_Genres)
                .ThenInclude(g => g.Genre)
                .SingleOrDefaultAsync(l => l.Name == name);

            if (lobby == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<Lobby, LobbyDto>(lobby);
            if (mapped.Term == DateTime.MinValue) mapped.Term = null;
            return new OkObjectResult(mapped);
            //return new OkObjectResult(lobby.ProposedMovies);
        }

        [HttpGet("{name}/participant")]
        public async Task<ActionResult<ICollection<Participant>>> GetParticipants(string name)
        {
            var lobby = await _context.Lobbies
                .Include(l => l.Participants)
                .SingleOrDefaultAsync(l => l.Name == name);

            if (lobby == null)
            {
                return NotFound();
            }

            return new OkObjectResult(await _lobbyService.GetParticipants(lobby));
        }



        [HttpPost("{name}")]
        public async Task<ActionResult<LobbyDto>> PostLobby(string name)
        {

            if (await _context.Lobbies.SingleOrDefaultAsync(l => l.Name == name) != null)
            {
                return new ConflictObjectResult("Lobby with that name already exists");
            }
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var participants = await _lobbyService.GetParticipantsFromFriends(user);
            var proposedMovies = await _lobbyService.GetMovies(participants);
            var times = await _lobbyService.GetMeetingTime(participants);
            foreach(TermProposition t in times)
            {
                System.Diagnostics.Debug.WriteLine($"xv {t.Time}");
            }
            if(times.Count == 0)
            {
                return BadRequest("Couldn't find time for the meeting");
            }

            Lobby lobby = new Lobby()
            {
                Name = name,
                Host = user,
                Participants = participants,
                Status = LobbyStatus.AKT,
                ProposedMovies = proposedMovies,
                ProposedTerms = times,
                ProposedTermIndex = 0
            };
            _context.Lobbies.Add(lobby);
            await _context.SaveChangesAsync();
            for(int i =0; i<participants.Count;i++)
            {
                if(i == 0) await _notificationService.CreateNotification(participants.ElementAt(i), 10, "Pomyślnie utworzono nowe lobby.", null, lobby);
                else await _notificationService.CreateNotification(participants.ElementAt(i), 11, "Osoba z Twojej listy znajomych utworzyła nowe lobby, zostałeś dodany jako jego uczestnik.", null, lobby);
            }
            return Ok();
        }

        [HttpDelete("{name}/participant/{login}")]
        public async Task<ActionResult<Lobby>> DeleteParticipant(string name, string login)
        {
            var lobby = await _context.Lobbies
                .Include(l => l.Participants)
                .SingleOrDefaultAsync(l => l.Name == name);
            var user = await _context.Users.FirstOrDefaultAsync(l => l.login == login);

            if (lobby == null || user == null || !lobby.Participants.Contains(user))
            {
                return NotFound("lobby or user not found");
            }

            lobby.Participants.Remove(user);
            _context.Lobbies.Update(lobby);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { res = "ok" });
        }
        [HttpPost("{name}/movies")]
        public async Task<ActionResult<Lobby>> VoteOnMovie(string name, VoteOnMovieModel vt)
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lobby = await _context.Lobbies
            .Include(l => l.Participants)
            .Include(l => l.ProposedMovies)
            .ThenInclude(mv => mv.Movie)
            .ThenInclude(mv => mv.Movies_Genres)
            .ThenInclude(g => g.Genre)
            .Include(mv => mv.ProposedMovies)
            .ThenInclude(mv => mv.alreadyVoted)
            .Include(mv => mv.Snack)
            .SingleOrDefaultAsync(l => l.Name == name);
            if (lobby == null || !lobby.Participants.Contains(user))
            {
                return NotFound("that lobby doesnt exist  or user isn't participating");
            }
            if(vt.id == null || vt.vote == null)
            {
                return BadRequest();
            }
            await _lobbyService.VoteOnMovie(lobby, vt.id, user, vt.vote);
            return new OkObjectResult(new { res = "ok" });

        }
        [HttpGet("{name}/movies")]
        public async Task<ActionResult<Lobby>> MoviesToVote(string name)
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lobby = await _context.Lobbies
            .Include(l => l.Participants)
            .Include(l => l.ProposedMovies)
            .ThenInclude(mv => mv.Movie)
            .ThenInclude(mv => mv.Movies_Genres)
            .ThenInclude(g => g.Genre)
            .Include(mv => mv.ProposedMovies)
            .ThenInclude(mv => mv.alreadyVoted)
            .SingleOrDefaultAsync(l => l.Name == name);
            if (lobby == null || !lobby.Participants.Contains(user))
            {
                return NotFound("that lobby doesnt exist or user isn't participating");
            }
            return new OkObjectResult(_mapper.Map<ICollection<Movie>, ICollection<GetMovieDto>>(await _lobbyService.MoviesToVote(user, lobby)));
        }

        [HttpPost("{name}/date")]
        public async Task<ActionResult<Lobby>> VoteOnDate(string name, [FromBody]VoteOnDateModel vote)
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lobby = await _context.Lobbies
            .Include(l => l.Participants)
            .Include(l => l.ProposedTerms)
            .ThenInclude(mv => mv.alreadyVoted)
            .SingleOrDefaultAsync(l => l.Name == name);
            if (lobby == null || !lobby.Participants.Contains(user))
            {
                return NotFound("that lobby doesnt exist  or user isn't participating");
            }
            if(vote.vote == null)
            {
                return BadRequest();
            }
            await _lobbyService.VoteOnTime(lobby, user, vote.vote);
            return new OkObjectResult(new { res = "ok" });

        }

        [HttpGet("{name}/date")]
        public async Task<ActionResult> GetIfVoted(string name)
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lobby = await _context.Lobbies
           .Include(l => l.Participants)
           .Include(l => l.ProposedTerms)
           .ThenInclude(mv => mv.alreadyVoted)
           .SingleOrDefaultAsync(l => l.Name == name);
            return new OkObjectResult(new { voted = lobby.ProposedTerms.OrderBy(e => e.Order).ElementAt(lobby.ProposedTermIndex).alreadyVoted.Contains(user) });
        }

    }
}
