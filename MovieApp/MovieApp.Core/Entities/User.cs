using System;
using Microsoft.AspNetCore.Identity;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<CustomList>? CustomLists { get; set; } = new HashSet<CustomList>();
    public ICollection<Review>? Comment { get; set; } = new HashSet<Review>();
    public ICollection<MovieRating>? Ratings { get; set; } = new HashSet<MovieRating>();
    public ICollection<SerieRating>? RatingsSeries { get; set; } = new HashSet<SerieRating>();
    public ICollection<EpisodeRating>? EpisodeRating { get; set; } = new HashSet<EpisodeRating>();
    public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();
}

