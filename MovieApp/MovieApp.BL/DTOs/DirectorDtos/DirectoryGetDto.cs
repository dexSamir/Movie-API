using System;
using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.DTOs.DirectorDtos;
public class DirectoryGetDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsVisible { get; set; }
    public string Name { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public string Surname { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Biography { get; set; }
}

