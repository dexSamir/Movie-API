using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;
namespace MovieApp.Core.Entities;
public class SupportTicket : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime ResolvedAt { get; set; }
	public ESupportTicket Status { get; set; }
	public string? AssignedUserId { get; set; }
	public User? AssignedUser { get; set; }
}

