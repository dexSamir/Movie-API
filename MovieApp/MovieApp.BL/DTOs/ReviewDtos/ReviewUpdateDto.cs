using System.ComponentModel.DataAnnotations;

namespace MovieApp.BL.DTOs.ReviewDtos;
public class ReviewUpdateDto
{
    [Required]
    [MaxLength(500, ErrorMessage = "Review content cannot exceed 500 characters.")]
    public int? ParentReviewId { get; set; }
    public string NewContent { get; set; } = string.Empty;
    public int? MovieId { get; set; }
    public int? SerieId { get; set; }
    public int? EpisodeId { get; set; }
}

