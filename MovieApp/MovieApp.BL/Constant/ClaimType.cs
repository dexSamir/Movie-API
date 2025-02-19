using System.Security.Claims;

namespace MovieApp.BL.Constant;
public class ClaimType
{
    public const string Username = "Username";
    public const string Email = "Email";
    public const string Id = ClaimTypes.NameIdentifier;
    public const string Fullname = "Fullname";
    public const string IsMale = "IsMale";
}

