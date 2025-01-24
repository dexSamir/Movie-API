using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Context;

public class AppDbContext : IdentityDbContext<User> 
{
	public DbSet<Actor> Actors { get; set; }
	public DbSet<Director> Directors { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<History> Histories { get; set; }
	public DbSet<Movie> Movies { get; set; }
	public DbSet<MovieActor> MovieActors { get; set; }
	public DbSet<MovieGenre> MovieGenres { get; set; }
	public DbSet<MovieRating> MovieRatings { get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<Subscription> Subscriptions { get; set; }
	public DbSet<WatchHistory> WatchHistories { get; set; }
	public DbSet<WatchList> WatchLists { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}

