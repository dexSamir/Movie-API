using AutoMapper;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class DirectorProfile : Profile
{
	public DirectorProfile()
	{
		CreateMap<DirectorCreateDto, Director>();
		CreateMap<DirectorUpdateDto, Director>()
			.ForAllMembers(x=> x.Condition((src, dest, srcMember) => srcMember != null));
		CreateMap<Director, DirectorGetDto>(); 
    }
}

