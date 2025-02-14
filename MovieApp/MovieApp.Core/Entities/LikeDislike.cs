using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class LikeDislike : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

    public int? MovieId { get; set; }
	public Movie? Movie { get; set; }

	public int? EpisodeId { get; set; }
	public Episode? Episode { get; set; }

	public int? ReviewId { get; set; }
	public Review? Review { get; set; }

	public bool IsLike { get; set; }
}

