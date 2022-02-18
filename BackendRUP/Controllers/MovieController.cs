using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Services.MovieService;

namespace WatchTogetherAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllMovies()
        {
            (int Header, var Result) = await _movieService.GetAllMovies();
            if (Header == 400)
                return StatusCode(400);
            else
                return StatusCode(200, Result);
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            (int Header, var Result) = await _movieService.GetAllGenres();
            if (Header == 400)
                return StatusCode(400);
            else
                return StatusCode(200, Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            (int Header, var Result) = await _movieService.GetMovieById(id);
            if (Header == 400)
                return StatusCode(400);
            else if (Header == 404)
                return StatusCode(404);
            else
                return StatusCode(200, Result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieByTitle([FromQuery] string q)
        {
            (int Header, var Result) = await _movieService.GetMovieByTitle(q);
            if (Header == 400)
                return StatusCode(400);
            else if (Header == 404)
                return StatusCode(404);
            else
                return StatusCode(200, Result.Take(20)); 
        }
    }   
}
