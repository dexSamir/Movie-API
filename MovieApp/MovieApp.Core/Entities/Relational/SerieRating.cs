using System;
namespace MovieApp.Core.Entities.Relational;
public class SerieRating
{
	public int Id { get; set; }
    public int? SerieId { get; set; }
    public Serie? Serie { get; set; }
    public int? RatingId { get; set; }
    public Rating? Rating { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}

