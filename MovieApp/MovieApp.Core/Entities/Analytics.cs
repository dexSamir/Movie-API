using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Analytics : BaseEntity
{
	public int TotalUsers { get; set; }
	public int TotalSeries { get; set; }
	public int TotalMovies { get; set; }
	public int TotalRentals { get; set; }
	public int TotalSubscriptions { get; set; }
    public DateTime AnalyticsDate { get; set; }
}

