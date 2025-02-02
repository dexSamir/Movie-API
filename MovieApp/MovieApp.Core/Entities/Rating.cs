using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Rating : BaseEntity
{
    public int Score { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int SerieId { get; set; }
    public Serie Serie { get; set; }

    public int EpisodeId { get; set; }
    public Episode Episode { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Review> Reviews { get; set; }
}
