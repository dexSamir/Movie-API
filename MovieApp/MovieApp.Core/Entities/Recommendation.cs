using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Recommendation : BaseEntity
{
    public int MovieId { get; set; }
    public int SerieId { get; set; }
    public string UserId { get; set; }
    public Movie Movie { get; set; }
    public Serie Serie{ get; set; }
    public User User { get; set; }
    public DateTime RecommendedDate { get; set; }
}

