using System;
using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.DTOs.DirectorDtos;
public class DirectorUpdateDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Biography { get; set; }
}

