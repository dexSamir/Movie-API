

namespace MovieApp.BL.DTOs.DirectorDtos;
public class DirectorGetDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public string Surname { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Biography { get; set; }
    public int MoviesCount { get; set; }
    public int SeriesCount { get; set; }
}

