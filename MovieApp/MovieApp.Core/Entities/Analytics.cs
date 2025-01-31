using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Analytics : BaseEntity
{
    public int TotalUsers { get; set; }
    public int TotalSeries { get; set; }
    public int TotalMovies { get; set; }
    public int TotalRentals { get; set; }
    public int TotalSubscriptions { get; set; }
    public int TotalViews { get; set; }
    public int TotalFavorites { get; set; }
    public int TotalReviews { get; set; }
    public int TotalRatings { get; set; }
    public int TotalDownloads { get; set; }
    public int ActiveUsers { get; set; }
    public int NewUsers { get; set; }
    public decimal TotalRevenue { get; set; }
    public double AverageRating { get; set; }
    public int? MostPopularMovieId { get; set; }
    public int? MostPopularSerieId { get; set; }
    public int? MostRentedMovieId { get; set; }
    public int? MostRentedSerieId { get; set; }
    public int? MostDownloadedMovieId { get; set; }
    public int? MostDownloadedSerieId { get; set; }
    public int? MostReviewedMovieId { get; set; }
    public int? MostReviewedSerieId { get; set; }
    public int? MostRatedMovieId { get; set; }
    public int? MostRatedSerieId { get; set; }
    public DateTime AnalyticsDate { get; set; }
    
}

