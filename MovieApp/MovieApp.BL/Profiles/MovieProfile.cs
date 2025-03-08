using AutoMapper;
using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.BL.DTOs.GenreDtos;
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

        CreateMap<MovieGenre, GenreNestedGetDto>()
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Genre.Name));

        CreateMap<MovieActor, ActorNestedGetDto>()
            .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Actor.Id))
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Actor.Name + " " + src.Actor.Surname))
            .ForMember(dest => dest.ProfilePhotoUrl, opt => opt.MapFrom(src=> src.Actor.ImageUrl));

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
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(x=> x.Reviews, x=> x.MapFrom(y=> y.Reviews))
            .ForMember(x=> x.AvgRating, x=> x.MapFrom(y=> y.AvgRating))
            .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(x => x.LikeCount))
            .ForMember(dest => dest.DislikeCount, opt => opt.MapFrom(x => x.DislikeCount))
            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname));

        CreateMap<Movie, ReactionCountDto>(); 
    }
}

