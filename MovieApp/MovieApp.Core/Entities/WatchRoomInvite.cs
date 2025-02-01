using MovieApp.Core.Helpers.Enums;
namespace MovieApp.Core.Entities;
public class WatchRoomInvite
{
	public string InviterId { get; set; }
	public User Inviter { get; set; }

	public string InviteeId { get; set; }
	public User Invitee { get; set; }

	public Guid WatchRoomId { get; set; }
	public WatchRoom WatchRoom { get; set; }

    public EWatchRoomInviteStatus Status { get; set; } 
    public DateTime CreatedAt { get; set; }
}

