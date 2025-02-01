using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class ActivityLog : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

	public string Action{ get; set; }
	public DateTime LogDate { get; set; }
}

