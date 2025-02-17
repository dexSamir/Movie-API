namespace MovieApp.BL.DTOs.HistoryDtos;
public class HistoryUpdateDto
{
    public int Id { get; set; }
    public int? StoppedAt { get; set; }
    public bool IsCompleted { get; set; }
}

