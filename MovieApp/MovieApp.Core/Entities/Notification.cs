using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Notification : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }
	public string Message { get; set; }
	public DateTime SendDate { get; set; }
	public bool IsRead { get; set; }
}

