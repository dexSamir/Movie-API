using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public class Actor : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Biography { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<MovieActor>? Movies { get; set; } = new HashSet<MovieActor>();
    public ICollection<SerieActor>? Series{ get; set; } = new HashSet<SerieActor>();
}
