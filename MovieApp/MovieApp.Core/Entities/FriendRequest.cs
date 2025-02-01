using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.Core.Entities;
public class FriendRequest : BaseEntity
{
	public string SenderId { get; set; }
	public User Sender { get; set; }

    public string ReceiverId { get; set; } 
    public User Receiver { get; set; }

    public EFriendRequestStatus Status { get; set; }
    public DateTime SentAt { get; set; }
}

