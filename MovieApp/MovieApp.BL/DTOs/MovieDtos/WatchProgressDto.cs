namespace MovieApp.BL.DTOs.MovieDtos;
public class WatchProgressDto
{
    public int Id { get; set; }
    public int? MovieId { get; set; }
    public int? SerieId { get; set; }
    public int? EpisodeId { get; set; }
    public string UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan CurrentTime { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool IsWatching { get; set; }
    public TimeSpan? PausedAt { get; set; }
}

