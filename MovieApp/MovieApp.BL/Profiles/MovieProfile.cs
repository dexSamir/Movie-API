using AutoMapper;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.Core.Entities;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.BL.Profiles;
public class MovieProfile : Profile
{
	public MovieProfile()
	{
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.Actors, opt => opt.Ignore()) 
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.MovieSubtitles, opt => opt.Ignore())
                .ForMember(dest => dest.AudioTracks, opt => opt.Ignore())
                .ForMember(dest => dest.AudioTracks, opt => opt.MapFrom(src => src.AudioTrackIds)); 

            CreateMap<int, MovieActor>()
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src));

            CreateMap<int, MovieGenre>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src));

            CreateMap<int, MovieSubtitle>()
                .ForMember(dest => dest.SubtitleId, opt => opt.MapFrom(src => src));

            CreateMap<int, AudioTrack>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<MovieUpdateDto, Movie>();
            CreateMap<Movie, MovieGetDto>()
                .AfterMap((src, dest) => dest.DirectorName = src.Director?.Name ?? "");
    }
}

