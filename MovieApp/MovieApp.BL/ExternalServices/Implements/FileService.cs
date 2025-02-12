using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Image;
using MovieApp.BL.Extensions;
using MovieApp.BL.ExternalServices.Interfaces;

namespace MovieApp.BL.ExternalServices.Implements;
public class FileService : IFileService
{
    public async Task<string> ProcessImageAsync(IFormFile? file, string directory, string fileType, int maxSize, string? existingFilePath = null)
    {
        if (file == null)
            return existingFilePath;

        if (!file.IsValidType(fileType))
            throw new UnsupportedFileTypeException($"File must be of type {fileType}.");

        if (!file.IsValidSize(maxSize))
            throw new ValidationException($"File size must be less than {maxSize} MB.");

        if (!string.IsNullOrEmpty(existingFilePath))
            FileExtension.DeleteFile(Path.Combine("wwwroot", directory, existingFilePath));

        return await file.UploadAsync("wwwroot", directory);
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

