using AutoMapper;
using MovieApp.BL.DTOs.HistoryDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class HistoryProfile : Profile
{
	public HistoryProfile()
	{
		CreateMap<HistoryCreateDto, History>();
        CreateMap<HistoryUpdateDto, History>();
		CreateMap<History, HistoryGetDto>();
    }
}

