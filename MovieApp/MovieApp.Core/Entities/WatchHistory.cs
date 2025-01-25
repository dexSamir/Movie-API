using System;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class WatchHistory : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }
	public int? MovieId { get; set; }
	public Movie? Movie { get; set; }
	public bool IsCompleted { get; set; } = false; 
	public DateTime WatchedTime { get; set; }
    public TimeSpan WatchedDuration { get; set; }
}

