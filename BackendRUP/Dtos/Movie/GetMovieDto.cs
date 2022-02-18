using System;
using System.Collections.Generic;
using WatchTogetherAPI.Dtos.Genre;

namespace WatchTogetherAPI.Dtos.Movie
{
    public class GetMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Release_date { get; set; }
        public int Length { get; set; }
        public string Poster_path { get; set; }
        public string Stills_path { get; set; }
        public float Rating { get; set; }
        public List<GetGenreDto> Genres { get; set; }
    }
}
