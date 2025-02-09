namespace MovieApp.BL.DTOs.MovieDtos;
public class MovieGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public int Duration { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? DirectorName { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    //public ICollection<string> Actors { get; set; }
    //public ICollection<string> Subtitles { get; set; }
    //public ICollection<string> Genres { get; set; }
    //public ICollection<string> AudioTracks { get; set; }
}


