namespace MovieApp.BL.DTOs.LanguageDtos;
public class LanguageGetDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Icon { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}

