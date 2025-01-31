using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Payment : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }
	public decimal Amount { get; set; }
	public DateTime PaymentDate { get; set; }
	public string PaymentMethod { get; set; }
}

