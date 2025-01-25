using System;
namespace MovieApp.Core.Entities.Relational;
public class EpisodeRating
{
    public int Id { get; set; }
    public int? EpisodeId { get; set; }
    public Episode? Episode { get; set; }
    public int? RatingId { get; set; }
    public Rating? Rating { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}

