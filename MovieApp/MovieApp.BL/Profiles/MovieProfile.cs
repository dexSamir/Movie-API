using AutoMapper;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.DTOs.RecommendationDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class MovieProfile : Profile
{
	public MovieProfile()
	{
        CreateMap<Recommendation, RecommendationGetDto>();

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
            .ForMember(x=> x.Reviews, x=> x.MapFrom(y=> y.Reviews))
            .ForMember(x=> x.AvgRating, x=> x.MapFrom(y=> y.AvgRating))
            .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(x => x.LikeCount))
            .ForMember(dest => dest.DislikeCount, opt => opt.MapFrom(x => x.DislikeCount))
            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname));

        CreateMap<Movie, ReactionCountDto>(); 
    }
}

