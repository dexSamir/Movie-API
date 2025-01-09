using System;
namespace MovieApp.Core.Entities;
public class MovieRating
{
	public int Id { get; set; }
	public int MovieId { get; set; }
	public Movie Movie { get; set; }
	public int RatingId { get; set; }
	public Rating Rating { get; set; }
	public string UserId { get; set; }
	public User User { get; set; }
}

