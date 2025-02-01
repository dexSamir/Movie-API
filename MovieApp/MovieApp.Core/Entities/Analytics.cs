using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;

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
    public Movie? MostPopularMovie { get; set; }
    public int? MostPopularSerieId { get; set; }
    public Serie? MostPopularSerie { get; set; }
    public int? MostRentedMovieId { get; set; }
    public Movie? MostRentedMovie { get; set; }
    public int? MostRentedSerieId { get; set; }
    public Serie? MostRentedSerie { get; set; }
    public int? MostDownloadedMovieId { get; set; }
    public Movie? MostDownloadedMovie { get; set; }
    public int? MostDownloadedSerieId { get; set; }
    public Serie? MostDownloadedSerie { get; set; }
    public int? MostReviewedMovieId { get; set; }
    public Movie? MostReviewedMovie { get; set; }
    public int? MostReviewedSerieId { get; set; }
    public Serie? MostReviewedSerie { get; set; }
    public int? MostRatedMovieId { get; set; }
    public Movie? MostRatedMovie { get; set; }
    public int? MostRatedSerieId { get; set; }
    public Serie? MostRatedSerie { get; set; }
    public DateTime AnalyticsDate { get; set; }
    public EAnalyticsType AnalyticsType { get; set; }
}