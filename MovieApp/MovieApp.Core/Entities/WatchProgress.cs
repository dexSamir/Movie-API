using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class WatchProgress : BaseEntity
{
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    public int? SerieId { get; set; }
    public Serie Serie { get; set; }

    public int? EpisodeId { get; set; }
    public Episode Episode { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan CurrentTime { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool IsWatching { get; set; }
    public TimeSpan? PausedAt { get; set; }
}