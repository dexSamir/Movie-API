using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities;
public class Subtitle : BaseEntity
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int SeriesId { get; set; }
    public Serie Serie { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}

