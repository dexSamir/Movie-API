using System;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Core.Entities;
public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
    public ICollection<MovieRating>? Ratings { get; set; } = new HashSet<MovieRating>();
    public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();
    public ICollection<WatchList>? WatchLists { get; set; } = new HashSet<WatchList>(); 
}

