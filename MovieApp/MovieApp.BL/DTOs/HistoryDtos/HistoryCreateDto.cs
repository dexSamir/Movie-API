
namespace MovieApp.BL.DTOs.HistoryDtos;
public class HistoryCreateDto
{
    public int? MovieId { get; set; }
    public int? SerieId { get; set; }
    public int? EpisodeId { get; set; }
    public bool IsCompleted { get; set; }
    public int WatchedDuration { get; set; }
}

