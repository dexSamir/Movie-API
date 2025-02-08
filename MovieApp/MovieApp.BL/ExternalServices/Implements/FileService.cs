using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Image;
using MovieApp.BL.Extensions;
using MovieApp.BL.ExternalServices.Interfaces;

namespace MovieApp.BL.ExternalServices.Implements;
public class FileService : IFileService
{
    public async Task<string> ProcessImageAsync(IFormFile image, string folder)
    {
        if (!image.IsValidType("image"))
            throw new UnsupportedFileTypeException();

        if (!image.IsValidSize(15))
            throw new UnsupportedFileSizeException(15);

        return await image.UploadAsync("wwwroot", "imgs", folder);
    }

    

    public async Task DeleteImageIfNotDefault(string imageUrl, string folder)
    {
        string defaultImage = $"/imgs/{folder}/default.jpg";

        if (!string.IsNullOrEmpty(imageUrl) && imageUrl != defaultImage)
        {
            string filePath = Path.Combine("wwwroot", "imgs", folder, imageUrl);
            FileExtension.DeleteFile(filePath);
        }
    }
}

