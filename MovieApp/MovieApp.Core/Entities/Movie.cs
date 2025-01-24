using MovieApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Entities;
public class Movie : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int? DirectorId { get; set; }
    public Director? Director { get; set; }
    public int? WatchListId { get; set; }
    public WatchList? WatchList { get; set; }
    public int? HistoryId { get; set; }
    public History? History { get; set; }
    public ICollection<MovieActor>? Actors { get; set; } = new HashSet<MovieActor>(); 
    public ICollection<MovieGenre>? Genres { get; set; }
    public ICollection<MovieRating>? Ratings { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}