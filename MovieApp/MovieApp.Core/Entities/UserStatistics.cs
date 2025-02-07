using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class UserStatistics : BaseEntity
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int TotalMoviesWatched { get; set; }
    public int TotalSeriesWatched { get; set; }
    public int TotalWatchTime { get; set; } //deqiqe dusunurem
    public string MostWatchedMovieGenre { get; set; }

    public Dictionary<string, int> GenreWatchTime { get; set; } 
    public Dictionary<string, int> WeeklyWatchTime { get; set; } 
    public Dictionary<string, int> MonthlyWatchTime { get; set; } 
    public List<int> TopWatchedMovies { get; set; } 
    public List<int> TopWatchedSeries { get; set; }

    public int TotalFavorites { get; set; }
    public int TotalWatchLater { get; set; }
    public int TotalComments { get; set; }
    public Dictionary<int, int> MovieRatings { get; set; } 
    public Dictionary<int, int> SerieRatings { get; set; }

    public int TotalDownloads { get; set; }
    public List<int> DownloadedMovies { get; set; } 
    public List<int> DownloadedSeries { get; set; } 
    public int TotalOfflineWatchTime { get; set; }

    public Dictionary<DateTime, int> DailyActivity { get; set; } 
    public Dictionary<string, int> MonthlyActivity { get; set; }

    public DateTime LastActivity { get; set; }

    public int TotalLikes { get; set; } 
    public int TotalDislikes { get; set; } 
    public Dictionary<int, int> MovieLikes { get; set; } 
    public Dictionary<int, int> SerieLikes { get; set; }

    public int TotalFriends { get; set; } 
    public int TotalFriendRequestsSent { get; set; } 
    public int TotalFriendRequestsReceived { get; set; }

    public int AverageWatchTimePerDay { get; set; } 
    public int AverageWatchTimePerWeek { get; set; } 
    public int LongestWatchSession { get; set; } 
    public DateOnly MostActiveDay { get; set; } 
}

