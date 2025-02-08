using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.DTOs.ActorDtos;
public class ActorUpdateDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public string? BirthDate { get; set; }
    public string? Biography { get; set; }
}

