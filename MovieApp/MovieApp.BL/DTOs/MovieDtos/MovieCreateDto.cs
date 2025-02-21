namespace MovieApp.BL.DTOs.MovieDtos;
public class MovieCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }  
    public string ReleaseDate { get; set; }
    public int? DirectorId { get; set; }
    public decimal RentalPrice { get; set; }

    public ICollection<int>? ActorIds { get; set; }
    public ICollection<int>? GenreIds { get; set; }
}

