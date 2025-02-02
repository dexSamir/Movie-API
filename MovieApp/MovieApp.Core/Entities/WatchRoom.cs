namespace MovieApp.Core.Entities;
public class WatchRoom
{
	public Guid Id { get; set; }

    public string RoomName { get; set; }
	public bool IsPublic { get; set; }

    public string? Password { get; set; }
    public string HostUserId { get; set; }
    public User HostUser { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    public int? MovieId { get; set; }
    public Movie? Movie { get; set; }

    public int? SerieId { get; set; }
    public Serie? Serie { get; set; }

    public string ShareCode { get; set; }

    public ICollection<WatchRoomInvite> Invites{ get; set; }
    public ICollection<WatchRoomUser> Participants { get; set; }
    public ICollection<ChatMessage> ChatMessages { get; set; }
}

