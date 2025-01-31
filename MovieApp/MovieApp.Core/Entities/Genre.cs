using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.Core.Entities;
public  class Genre : BaseEntity
{
    public string Name { get; set; }
    public ICollection<MovieGenre>? Movies { get; set; } 
    public ICollection<SerieGenre>? Series { get; set; }
    public ICollection<UserPreferences> UserPreferences { get; set; }
}
