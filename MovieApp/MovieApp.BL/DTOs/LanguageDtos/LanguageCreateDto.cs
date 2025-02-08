using Microsoft.AspNetCore.Http;
namespace MovieApp.BL.DTOs.LanguageDtos;
public class LanguageCreateDto
{
    public string Code { get; set; }
    public IFormFile? Icon { get; set; }
    public string Name { get; set; }
}

