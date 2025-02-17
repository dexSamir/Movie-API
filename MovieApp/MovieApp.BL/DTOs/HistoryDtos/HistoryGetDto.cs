namespace MovieApp.BL.DTOs.HistoryDtos;
public class HistoryGetDto
{
    public int Id { get; set; }
    public string MovieTitle { get; set; }
    public string SerieTitle { get; set; }
    public string EpisodeTitle { get; set; }
    public DateTime WatchedAt { get; set; }
    public int? StoppedAt { get; set; }
    public bool IsCompleted { get; set; }
}

