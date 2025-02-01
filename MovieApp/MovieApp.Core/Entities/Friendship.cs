using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Friendship : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

    public string FriendId { get; set; } 
    public User Friend { get; set; }
}

