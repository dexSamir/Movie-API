using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.DTOs.UserDtos;
public class RegisterDto
{
    public IFormFile? ProfileUrl { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? BirthDate { get; set; }
    public bool IsMale { get; set; }
}

