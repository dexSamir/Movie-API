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
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.Parse(src.ReleaseDate))); 

        CreateMap<MovieUpdateDto, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.MovieSubtitles, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.AudioTracks, opt => opt.Ignore())
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom((src, dest) =>
                string.IsNullOrWhiteSpace(src.ReleaseDate) ? dest.ReleaseDate : DateOnly.Parse(src.ReleaseDate)))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null && (srcMember is not string || !string.IsNullOrWhiteSpace(srcMember.ToString())))); 

        CreateMap<Movie, MovieGetDto>()
            .AfterMap((src, dest) => dest.DirectorName = src.Director?.Name ?? "");
    }
}

