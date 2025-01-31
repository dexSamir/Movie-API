using System;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class History : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

	public int? MovieId { get; set; }
	public Movie Movie { get; set; }

    public int? SerieId { get; set; }
    public Movie Serie { get; set; }

	public int? EpisodeId { get; set; }
	public Episode Episode { get; set; }

	public DateTime WatchedAt { get; set; }
	public int? StoppedAt { get; set; }
	public bool IsCompleted { get; set; }
	public int WhatchedDuration { get; set; }

}
