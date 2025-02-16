using MovieApp.BL.DTOs.MovieDtos;

namespace MovieApp.BL.DTOs.RecommendationDtos;
public class RecommendationGetDto
{
    public int MovieId { get; set; }
    public int SerieId { get; set; }
    public string UserId { get; set; }
    public MovieGetDto Movie { get; set; }
    //public SerieGetDto Serie { get; set; }
    public DateTime RecommendedDate { get; set; }
}

