using System.ComponentModel.DataAnnotations;

namespace MovieApp.BL.DTOs.ReviewDtos;
public class ReviewCreateDto
{
    public int? MovieId { get; set; }
    public int? SerieId { get; set; }
    public int? EpisodeId { get; set; }
    public int? ParentReviewId { get; set; }
    [Required]
    [MaxLength(1000, ErrorMessage = "Review content cannot exceed 1000 characters.")]
    public string Content { get; set; }
}

