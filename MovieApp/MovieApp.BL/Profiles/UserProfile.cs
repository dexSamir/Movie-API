using AutoMapper;
using MovieApp.BL.DTOs.UserDtos;
using MovieApp.BL.Utilities.Helpers;
using MovieApp.Core.Entities;
namespace MovieApp.BL.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>()
            .ForMember(x => x.PasswordHash, x => x.MapFrom(y =>
              HashHelper.HashPassword(y.Password)));
        CreateMap<User, UserGetDto>();
    }
}

