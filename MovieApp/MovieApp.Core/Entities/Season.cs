using System;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Season : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int SeasonNumber { get; set; }
    public int? SerieId { get; set; }
    public Serie? Serie { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<Episode>? Episodes { get; set; } = new HashSet<Episode>();
}
