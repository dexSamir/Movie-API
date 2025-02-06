using AutoMapper;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class DirectorProfile : Profile
{
	public DirectorProfile()
	{
		CreateMap<DirectorCreateDto, Director>();
		CreateMap<DirectorUpdateDto, Director>();
		CreateMap<Director, DirectorGetDto>(); 
    }
}

