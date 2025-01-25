using System;
using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public class Episode : BaseEntity
{
	public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int SeasonId { get; set; }
    public Season Season { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<EpisodeRating>? Ratings{ get; set; }
}

