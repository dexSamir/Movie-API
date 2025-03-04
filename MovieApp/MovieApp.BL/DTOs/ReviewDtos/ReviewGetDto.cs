namespace MovieApp.BL.DTOs.ReviewDtos;
public class ReviewGetDto
{
    public int Id { get; set; }
    public string Content { get; set; } 
    public string? UserName { get; set; }

    public int? MovieId { get; set; }
    public int? SerieId { get; set; }
    public int? EpisodeId { get; set; }

    public int? ParentReviewId { get; set; }

    public DateTime ReviewDate { get; set; }
    public bool IsUpdated { get; set; }
    public DateTime? UpdatedTime { get; set; }

    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }

    public IEnumerable<ReviewGetDto>? Replies { get; set; }
}

