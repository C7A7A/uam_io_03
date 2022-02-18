using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Dtos.Genre;
using WatchTogetherAPI.Dtos.Lobby;
using WatchTogetherAPI.Dtos.Movie;
using WatchTogetherAPI.Dtos.Notification;
using WatchTogetherAPI.Models;


namespace WatchTogetherAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, GetMovieDto>();
            CreateMap<Genre, GetGenreDto>();
            CreateMap<Movie, GetMovieDto>()
                .ForMember(dto => dto.Genres, m => m.MapFrom(m => m.Movies_Genres.Select(mg => mg.Genre)));
            CreateMap<User, Participant>();
            CreateMap<Lobby, LobbyDto>()
                .ForMember(dto => dto.Host, h => h.MapFrom(h => h.Host))
                .ForMember(dto => dto.Participants, p => p.MapFrom(p => p.Participants))
                .ForMember(dto => dto.ProposedMovies, pm => pm.MapFrom(pm => pm.ProposedMovies.Select(l => l.Movie)))
                .ForMember(dto => dto.SelectedMovie, sm => sm.MapFrom(sm => sm.SelectedMovie))
                .ForMember(dto => dto.Term, sm => sm.MapFrom(sm => sm.ProposedTerms.OrderBy(e => e.Order).ElementAt(sm.ProposedTermIndex).Time))
                .ForMember(dto => dto.Snack, sm => sm.MapFrom(sm => sm.Snack));

            CreateMap<Lobby, UserLobbiesDto>()
                .ForMember(dto => dto.Host, h => h.MapFrom(h => h.Host))
                .ForMember(dto => dto.Participants, p => p.MapFrom(p => p.Participants));
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dto => dto.LobbyName, h => h.MapFrom(h => h.Lobby.Name))
                .ForMember(dto => dto.From, h => h.MapFrom(h => h.From.login));

        }
    }

}
