using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public class Movie : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public int Duration { get; set; } // bu da deqiqe olcag
    public DateTime ReleaseDate { get; set; }

    public int? DirectorId { get; set; }
    public Director? Director { get; set; }
    public int? HistoryId { get; set; }
    public History? History { get; set; }

    public int LikeCount { get; set; } = 0;
    public int DislikeCount { get; set; } = 0;

    public ICollection<MovieActor>? Actors { get; set; } = new HashSet<MovieActor>(); 
    public ICollection<MovieSubtitle>? MovieSubtitles{ get; set; }
    public ICollection<MovieGenre>? Genres { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Rental> Rentals { get; set; }
    public ICollection<AudioTrack> AudioTracks { get; set; }
    public ICollection<CustomListItem> CustomListItems { get; set; }
    public ICollection<DownloadListItem> DownloadListItems { get; set; }
    public ICollection<Recommendation> Recommendations { get; set; }
    public ICollection<History> Histories { get; set; }
}