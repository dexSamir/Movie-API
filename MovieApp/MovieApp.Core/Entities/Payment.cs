using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.Core.Entities;
public class Payment : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

	public decimal Amount { get; set; }

	public DateTime PaymentDate { get; set; }

	public EPaymentMethod PaymentMethod { get; set; }

    public EPaymentStatus Status { get; set; }

    public string? TransactionId { get; set; }
}

