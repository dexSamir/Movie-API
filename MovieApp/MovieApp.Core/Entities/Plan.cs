using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Plan : BaseEntity
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int DurationTime { get; set; } // bunu gun olarag saxliyacam

	public ICollection<Subscription> Subscriptions { get; set; }
}

