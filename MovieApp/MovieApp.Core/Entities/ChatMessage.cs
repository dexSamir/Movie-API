namespace MovieApp.Core.Entities;
public class ChatMessage
{
	public Guid WatchRoomId { get; set; }
	public WatchRoom WatchRoom { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public string Message { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

