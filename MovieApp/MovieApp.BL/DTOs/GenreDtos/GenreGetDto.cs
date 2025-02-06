namespace MovieApp.BL.DTOs.GenreDtos; 
public class GenreGetDto
{
    public int Id { get; set; }
    public int MovieCount { get; set; }
    public int SerieCount { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}

