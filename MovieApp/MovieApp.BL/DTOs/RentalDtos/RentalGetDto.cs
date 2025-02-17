namespace MovieApp.BL.DTOs.RentalDtos;
public class RentalGetDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Price { get; set; }
}

