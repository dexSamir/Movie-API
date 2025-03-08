using System.Text.Json.Serialization;
using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.BL.DTOs.ReviewDtos;

namespace MovieApp.BL.DTOs.MovieDtos;
public class MovieGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public double AvgRating { get; set; }
    public int Duration { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? DirectorName { get; set; }
    public decimal RentalPrice { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public int WatchCount { get; set; }

    [JsonIgnore]
    public IEnumerable<ReviewGetDto>? Reviews { get; set; }
    public IEnumerable<GenreNestedGetDto> Genres { get; set; }
    public IEnumerable<ActorNestedGetDto> Actors { get; set; }
}
