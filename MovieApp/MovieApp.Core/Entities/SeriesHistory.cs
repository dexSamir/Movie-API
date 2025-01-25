using System;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities.Relational;
public class SeriesHistory : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

	public int SeriesId { get; set; }
    public Serie Serie { get; set; }

    public ICollection<EpisodeWatchHistory> EpisodeWatchHistories { get; set; } = new HashSet<EpisodeWatchHistory>();
	public bool IsCompleted => EpisodeWatchHistories.All(x => x.IsCompleted);
	public TimeSpan TotalWatchedDuration => EpisodeWatchHistories.Aggregate(TimeSpan.Zero, (sum, e) => sum + e.WatchedDuration); 
}

