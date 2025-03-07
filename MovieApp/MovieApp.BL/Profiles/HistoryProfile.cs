using AutoMapper;
using MovieApp.BL.DTOs.HistoryDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class HistoryProfile : Profile
{
	public HistoryProfile()
	{
		CreateMap<HistoryCreateDto, History>()
            .ForMember(x => x.MovieId, x => x.MapFrom(y => y.MovieId > 0 ? y.MovieId : null))
            .ForMember(x => x.SerieId, x => x.MapFrom(y => y.SerieId > 0 ? y.SerieId : null))
            .ForMember(x => x.EpisodeId, x => x.MapFrom(y => y.EpisodeId > 0 ? y.EpisodeId : null));


        CreateMap<HistoryUpdateDto, History>();

		CreateMap<History, HistoryGetDto>()
			.ForMember(x => x.MovieTitle, x => x.MapFrom(y => y.Movie.Title ?? null))
			.ForMember(x => x.SerieTitle, x => x.MapFrom(y => y.Serie.Title ?? null))
			.ForMember(x => x.EpisodeTitle, x => x.MapFrom(y => y.Episode.Title ?? null));
			
    }
}

