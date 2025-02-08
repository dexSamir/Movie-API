using Microsoft.AspNetCore.Http;

namespace MovieApp.BL.ExternalServices.Interfaces;
public interface IFileService
{
    Task<string> ProcessImageAsync(IFormFile image, string folder);
    Task DeleteImageIfNotDefault(string imageUrl, string folder);
}

