using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Dtos.Genre;
using WatchTogetherAPI.Dtos.Movie;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly WatchTogetherContext _context;
        private readonly IMapper _mapper;
        public MovieService(WatchTogetherContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(int, List<GetMovieDto>)> GetAllMovies()
        {
            List<GetMovieDto> getMovies = new List<GetMovieDto>();
            try
            {
                List<Movie> dbMovies = await _context.Movies.ToListAsync();
                getMovies = dbMovies.Select(m => _mapper.Map<GetMovieDto>(m)).ToList();
            }
            catch (Exception)
            {
                return (400, getMovies);

            }
            return (200, getMovies);
        }
        public async Task<(int , List<GetGenreDto>)> GetAllGenres()
        {
            List<GetGenreDto> getGenres = new List<GetGenreDto>();
            try
            {
                List<Genre> dbGenres = await _context.Genres.ToListAsync();
                getGenres = dbGenres.Select(g => _mapper.Map<GetGenreDto>(g)).ToList();
            }
            catch (Exception)
            {
                return (400, getGenres);

            }
            return (200, getGenres);
        }
        public async Task<(int, GetMovieDto)> GetMovieById(int id)
        {
            GetMovieDto getMovie = new GetMovieDto();
            try
            {
                Movie dbMovie = await _context.Movies.Include(m => m.Movies_Genres).ThenInclude(mg => mg.Genre)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (dbMovie != null)
                {
                    getMovie = _mapper.Map<GetMovieDto>(dbMovie);
                    return (200, getMovie);
                }
                else
                {
                    return (404, getMovie);
                }
            }
            catch (Exception)
            {
                return (200, getMovie);
            }
        }

        public async Task<(int, List<GetMovieDto>)> GetMovieByTitle(string title)
        {
            List<GetMovieDto> getMovie = new List<GetMovieDto>();
            try
            {
                title = EncodingSpecialLetters(title);
                var movies = from m in _context.Movies
                            select m;
                 movies =  _context.Movies.Include(m => m.Movies_Genres).ThenInclude(mg => mg.Genre)
                     .Where(m => m.Title.Contains(title));
                List<Movie> dbMovie = await movies.ToListAsync();
                getMovie = dbMovie.Select(m => _mapper.Map<GetMovieDto>(m)).ToList();
                if (getMovie.Any())
                {
                    return (200, getMovie);
                }
                else
                {
                    return (404, getMovie);
                }
            }
            catch (Exception)
            {
                return (200, getMovie);
            } 
        }

        public string EncodingSpecialLetters(string url)
        {
            if (url != null)
            {
                url = url.Replace(" ", "%20");
                url = url.Replace("!", "%21");
                url = url.Replace("\"", "%22");
                url = url.Replace("#", "%23");
                url = url.Replace("$", "24");
                url = url.Replace("&", "26");
                url = url.Replace("'", "%27");
                url = url.Replace("(", "%28");
                url = url.Replace(")", "%29");
                url = url.Replace("*", "%2A");
                url = url.Replace("+", "%2B");
                url = url.Replace(",", "%2C");
                url = url.Replace(".", "%2e");
                url = url.Replace("/", "%2F");
                url = url.Replace(":", "%3A");
                url = url.Replace(";", "%3B");
                url = url.Replace("<", "%3C");
                url = url.Replace("=", "%3D");
                url = url.Replace(">", "%3E");
                url = url.Replace("?", "%3F");
                url = url.Replace("@", "%40");
                url = url.Replace("[", "%5B");
                url = url.Replace("\\", "%5C");
                url = url.Replace("]", "%5D");
                url = url.Replace("^", "%5E");
                url = url.Replace("`", "%60");
                url = url.Replace("{", "%7B");
                url = url.Replace("|", "%7C");
                url = url.Replace("}", "%7D");
                url = url.Replace("~", "%7E");
            }
            return url;
        }
    }
}
