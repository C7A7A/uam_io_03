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
    public class SettingsController : ControllerBase
    {
        private readonly WatchTogetherContext _context;
        public SettingsController(WatchTogetherContext context)
        {
            _context = context;
        }

        // GET:/user/settings
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = new User();
            var identity = User.Identity;
            user = _context.Users.Single(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userSettings = await _context.Users
                   .Where(u => u.is_deleted == false && u.id == user.id)
                   .Select(u => new
                   {
                       u.name,
                       u.surname,
                       u.email,
                       u.login,
                       u.preferredgenres,
                       u.watchedmovies,
                       u.refresh_token
                   })
                   .FirstOrDefaultAsync();
            if (userSettings == null)
                return Unauthorized();

            List<string> preferredgenres = new List<string>();           
            
            List<Movie> watchedmovies = new List<Movie>();


            if (!string.IsNullOrEmpty(userSettings.preferredgenres))
            {
                preferredgenres = userSettings.preferredgenres.Split(",").ToList();   
            }
            if (!string.IsNullOrEmpty(userSettings.watchedmovies))
            {
                var moviesList = userSettings.watchedmovies.Split(",").ToList();

                foreach (String movie in moviesList)
                {
                    var movieToAdd = await _context.Movies
                                        .Where(m => m.Id == Int32.Parse(movie))
                                        .FirstOrDefaultAsync();
                    watchedmovies.Add(movieToAdd);
                }
            }

            var preferredhours = _context.PreferredHours
                .Where(p => p.userID == user.id)
                .Select(p => new
                {
                    p.day,
                    p.StartTime,
                    p.EndTime
                })
                .ToList();

             var returnObject = new
             {
                userSettings.name,
                userSettings.surname,
                userSettings.email,
                preferredgenres,
                watchedmovies,
                preferredhours,
                microsoft = userSettings.refresh_token != null ? true : false
             };
             return Ok(returnObject);
            
        }

        // POST:/user/settings 
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromQuery][Bind] UserParameters userParameter, [FromBody] List<PreferredHours> preferredHours)
        {
            var user = new User();
            var identity = User.Identity;
            user = _context.Users.Single(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userToUpdate = await _context.Users
                .Where(u => u.id == user.id)
                .FirstOrDefaultAsync();
            if (userToUpdate == null)
                return Unauthorized();
            if (ModelState.IsValid == false)
                return BadRequest();
            foreach (PreferredHours preferredHour in preferredHours)
            {
                var hourToUpdate = await _context.PreferredHours
                    .Where(p => p.userID == user.id && p.day == preferredHour.day)
                    .FirstOrDefaultAsync();
                if (hourToUpdate == null)
                {
                    preferredHour.userID = user.id;
                    _context.Add(preferredHour);
                }
                else
                {
                    hourToUpdate.day = preferredHour.day;
                    hourToUpdate.StartTime = preferredHour.StartTime;
                    hourToUpdate.EndTime = preferredHour.EndTime;
                }
            }
            try
            {
                userToUpdate.name = userParameter.name;
                userToUpdate.surname = userParameter.surname;
                userToUpdate.email = userParameter.email;
                userToUpdate.preferredgenres = String.Join(",", userParameter.preferredgenres);
                userToUpdate.watchedmovies = String.Join(",", userParameter.watchedmovies);
            }
            catch
            {
                return BadRequest();
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}