using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.DTOs.DirectorDtos;
public class DirectorCreateDto
{
	public string Name { get; set; }
	public string Surname { get; set; }
    public IFormFile? ImageUrl { get; set; }
	public string? BirthDate { get; set; }
    public string? Biography { get; set; }
}

