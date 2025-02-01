using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.Core.Entities;
public class ChatMessage : BaseEntity
{
	public Guid WatchRoomId { get; set; }
	public WatchRoom WatchRoom { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public EMessageType MessageType { get; set; }

    public string? Content { get; set; }  
    public string? ImageUrl { get; set; }  
    public string? LinkUrl { get; set; }   
    public string? SystemMessage { get; set; }

    public string Message { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

