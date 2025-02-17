namespace MovieApp.BL.DTOs.RentalDtos;
public class RentalCreateDto
{
    public int MovieId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Price { get; set; }
}

