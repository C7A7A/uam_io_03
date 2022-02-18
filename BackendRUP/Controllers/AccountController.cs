using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WatchTogetherAPI.Auth;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Models;
using WatchTogetherAPI.Models.JWT;
using WatchTogetherAPI.Services.MicrosoftAccountService;
using System.Web.Http.Results;
using Microsoft.Graph;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;

namespace WatchTogetherAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly WatchTogetherContext _context;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMicrosoftAccountService _microsoftAccountService;

        public AccountController(IMapper mapper, UserManager<AppUser> userManager, WatchTogetherContext context, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IMicrosoftAccountService microsoftAccountService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _microsoftAccountService = microsoftAccountService;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {

            var userIdentity = _mapper.Map<RegistrationViewModel, AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.password);

            if (!result.Succeeded)
            {
                if(result.Errors.First().Code == "DuplicateUserName")
                {
                    return Conflict(result.Errors);
                }
                return BadRequest(result.Errors);
            }

            await _context.Users.AddAsync(new Models.User
            {
                IdentityId = userIdentity.Id,
                login = userIdentity.UserName,
                name = model.name,
                surname = model.surname,
                email = model.email,

            });
            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
        {
            var identity = await GetClaimsIdentity(credentials.Login, credentials.Password);
            if (identity == null)
            {
                return Unauthorized("Incorrect username or password");
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.Login, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("microsoft")]
        [HttpOptions]
        public async Task<IActionResult> GetMicrosoftAccountUser(string code, string state)
        {
            if (code != null && state != null)
            {
                await _microsoftAccountService.GetRefreshToken(code, state);
                return new OkObjectResult("Twoje konto Microsoft zostało pomyślnie połączone. Możesz zamknąć to okno");

            }



            return BadRequest("Error");


        }


        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        /*testowe funkcje */
        [Authorize(Policy = "ApiUser")]
        [HttpGet("test/user")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var mcuser = await _microsoftAccountService.GetUser(user);
            return new OkObjectResult(mcuser);
        }
        [Authorize(Policy = "ApiUser")]
        [HttpGet("test/calendar")]
        public async Task<IActionResult> GetCalendar()
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var mcuser = await _microsoftAccountService.GetCalendar(user);
            return new OkObjectResult(mcuser);
        }
        [Authorize(Policy = "ApiUser")]
        [HttpGet("test/events")]
        public async Task<IActionResult> GetEvents()
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var mcuser = await _microsoftAccountService.GetCalendarEvents(user);
            return new OkObjectResult(mcuser);
        }
       /* [Authorize(Policy = "ApiUser")]
        [HttpPost("test/events")]
        public async Task<IActionResult> PostEvents()
        {
            var user = await _context.Users.SingleAsync(u => u.login == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(await _microsoftAccountService.CreateEvent(user));
        }*
        /*koniec testowych */
    }
}
