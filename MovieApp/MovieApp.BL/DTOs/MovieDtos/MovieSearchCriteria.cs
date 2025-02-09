namespace MovieApp.BL.DTOs.MovieDtos;
public class MovieSearchCriteria
{
    public string? Title { get; set; }
    public int? DirectorId { get; set; }
    public int? ActorId { get; set; }
    public string? Genre { get; set; }
    public double? MinRating { get; set; }
    public double? MaxRating { get; set; }
    public DateOnly? MinReleaseDate { get; set; }
    public DateOnly? MaxReleaseDate { get; set; }
}

