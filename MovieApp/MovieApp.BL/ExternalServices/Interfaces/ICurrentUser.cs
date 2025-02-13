using MovieApp.BL.DTOs.UserDtos;

namespace MovieApp.BL.ExternalServices.Interfaces;
public interface ICurrentUser
{
    int GetId();
    string GetUserName();
    string GetEmail();
    string GetFullname();
    //Task<UserGetDto> GetUserAsync();
}

