using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Rental : BaseEntity
{
	public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Price { get; set; }
}

