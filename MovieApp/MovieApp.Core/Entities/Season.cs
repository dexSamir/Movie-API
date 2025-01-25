using System;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Season : BaseEntity
{
    public int SeasonNumber { get; set; }
    public int EpisodeCount { get; set; }
    public int? SerieId { get; set; }
    public Serie? Serie { get; set; }
    public ICollection<Episode>? Episodes { get; set; } = new HashSet<Episode>();
}
