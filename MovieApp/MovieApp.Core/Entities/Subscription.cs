using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.Core.Entities; 
public class Subscription : BaseEntity 
{
    public DateTime EndTime { get; set; }
    public DateTime StartTime { get; set; }
    public bool IsActive => EndTime > DateTime.UtcNow; 
    public Plan? Plan { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}
