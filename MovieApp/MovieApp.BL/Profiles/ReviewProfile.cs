using AutoMapper;
using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.DTOs.ReviewDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class ReviewProfile : Profile
{
	public ReviewProfile()
	{
        CreateMap<Review, ReviewGetDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : "Unknown"))
            .ForMember(dest => dest.IsUpdated, opt => opt.MapFrom(src => src.UpdatedTime != null));
           
        CreateMap<ReviewCreateDto, Review>()
            .ForMember(x => x.MovieId, x => x.MapFrom(y => y.MovieId > 0 ? y.MovieId : null))
            .ForMember(x => x.SerieId, x => x.MapFrom(y => y.SerieId > 0 ? y.SerieId : null))
            .ForMember(x => x.EpisodeId, x => x.MapFrom(y => y.EpisodeId > 0 ? y.EpisodeId : null))
            .ForMember(x => x.ParentReviewId, x => x.MapFrom(y => y.ParentReviewId > 0 ? y.ParentReviewId : null))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ReviewUpdateDto, Review>()
            .ForMember(x => x.MovieId, x => x.MapFrom(y => y.MovieId > 0 ? y.MovieId : null))
            .ForMember(x => x.SerieId, x => x.MapFrom(y => y.SerieId > 0 ? y.SerieId : null))
            .ForMember(x => x.EpisodeId, x => x.MapFrom(y => y.EpisodeId > 0 ? y.EpisodeId : null))
            .ForMember(x => x.ParentReviewId, x => x.MapFrom(y => y.ParentReviewId > 0 ? y.ParentReviewId : null))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.NewContent))
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsUpdated, opt => opt.MapFrom(_ => true));


        CreateMap<Review, ReactionCountDto>();

    }
}

