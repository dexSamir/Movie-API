using MovieApp.Core.Entities;

namespace MovieApp.BL.ExternalServices.Interfaces;

public interface IJwtTokenHandler
{
    string CreateToken(User user, int hours);
}
