using System;
namespace MovieApp.Core.Entities.Relational;
public class MovieSubtitle
{
	public int Id { get; set; }
    public int MovieId { get; set; }
	public int SubtitleId { get; set; }
	public Movie Movie { get; set; }
    public Subtitle Subtitle { get; set; }
}

