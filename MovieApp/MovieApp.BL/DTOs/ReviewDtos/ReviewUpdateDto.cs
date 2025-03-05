using System.ComponentModel.DataAnnotations;

namespace MovieApp.BL.DTOs.ReviewDtos;
public class ReviewUpdateDto
{
    [Required]
    [MaxLength(500, ErrorMessage = "Review content cannot exceed 500 characters.")]
    public string NewContent { get; set; } = string.Empty;
}

