using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;
namespace MovieApp.Core.Entities;
public class Subtitle : BaseEntity
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public ICollection<SerieSubtitle>? SerieSubtitles { get; set; }
    public ICollection<MovieSubtitle>? MovieSubtitles { get; set; } 
}

