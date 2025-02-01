using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities;
public class WatchRoomUser : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

	public Guid WatchRoomId { get; set; }
	public WatchRoom WatchRoom { get; set; }

	public DateTime JoinedAt { get; set; }
}

