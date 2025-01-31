namespace MovieApp.Core.Entities.Relational;
public class SerieSubtitle
{
	public int Id { get; set; }
	public int SerieId { get; set; }
	public int SubtitleId { get; set; }
	public Serie Serie { get; set; }
	public Subtitle Subtitle { get; set; }
}

