namespace MovieApp.BL.DTOs.MovieDtos;
public class MovieUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public int? Duration { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public int? DirectorId { get; set; }
    public ICollection<int>? ActorIds { get; set; }
    public ICollection<int>? SubtitleIds { get; set; }
    public ICollection<int>? GenreIds { get; set; }
    public ICollection<int>? AudioTrackIds { get; set; }
}
