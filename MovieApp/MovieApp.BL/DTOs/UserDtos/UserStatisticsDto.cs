namespace MovieApp.BL.DTOs.UserDtos;
public class UserStatisticsDto
{
    public int TotalMoviesWatched { get; set; }
    public int TotalSeriesWatched { get; set; }
    public int TotalWatchTime { get; set; }
    public string MostWatchedMovieGenre { get; set; }
    public int AverageWatchTimePerDay { get; set; }
    public int AverageWatchTimePerWeek { get; set; }
    public int LongestWatchSession { get; set; }

    public int TotalFavorites { get; set; }
    public int TotalLikes { get; set; }
    public int TotalDislikes { get; set; }
    public int TotalFriends { get; set; }

    public DateTime LastActivity { get; set; }

    public int TotalDownloads { get; set; }
    public int TotalOfflineWatchTime { get; set; }
}

