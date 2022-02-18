using System.Collections.Generic;
using System.Threading.Tasks;
using WatchTogetherAPI.Dtos.Genre;
using WatchTogetherAPI.Dtos.Movie;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Services.MovieService
{
    public interface IMovieService
    {
        Task<(int, List<GetMovieDto>)> GetAllMovies();
        Task<(int, GetMovieDto)> GetMovieById(int id);
        Task<(int, List<GetGenreDto>)> GetAllGenres();
        Task<(int, List<GetMovieDto>)> GetMovieByTitle(string name);
    }
}
