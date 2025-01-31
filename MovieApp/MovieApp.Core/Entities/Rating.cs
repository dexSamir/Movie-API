using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public class Rating : BaseEntity
{
    public int Score { get; set; }
    public ICollection<MovieRating> Movies { get; set; }
    public ICollection<SerieRating> Series { get; set; }
    public ICollection<EpisodeRating> Episodes { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }

}
