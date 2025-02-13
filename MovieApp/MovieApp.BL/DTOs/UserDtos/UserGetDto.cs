namespace MovieApp.BL.DTOs.UserDtos;
public class UserGetDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool IsMale { get; set; }
    public UserStatisticsDto Statistics { get; set; }
}

