using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.DTOs.LanguageDtos;
public class LanguageUpdateDto
{
    public string? Code { get; set; }
    public IFormFile? Icon { get; set; }
    public string? Name { get; set; }
}

