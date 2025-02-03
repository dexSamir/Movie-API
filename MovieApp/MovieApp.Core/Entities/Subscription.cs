using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities; 
public class Subscription : BaseEntity 
{
    public DateTime EndTime { get; set; }
    public DateTime StartTime { get; set; }
    public bool IsActive { get; set; }

    public int PlanId { get; set; }
    public Plan Plan { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}
