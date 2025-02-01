using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public class Serie : BaseEntity
{ 
	public string Title { get; set; }
	public string Description { get; set; }
	public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
	public int SeasonCount { get; set; }
    public int EpisodeCount { get; set; }
    public int? WatchListId { get; set; }
    public int? HistoryId { get; set; }
    public History? History { get; set; }
    public int? DirectorId { get; set; }
    public Director? Director { get; set; }
    public ICollection<SerieSubtitle>? SerieSubtitles { get; set; }
    public ICollection<Season> Seasons { get; set; } = new HashSet<Season>();
    public ICollection<SerieActor>? Actors { get; set; } = new HashSet<SerieActor>();
    public ICollection<SerieGenre>? Genres { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<Recommendation> Recommendations { get; set; }
    public ICollection<AudioTrack> AudioTracks { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<History> Histories { get; set; }
}

