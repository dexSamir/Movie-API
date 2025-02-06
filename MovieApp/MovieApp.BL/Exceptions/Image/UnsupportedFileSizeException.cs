using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Image;
public class UnsupportedFileSizeException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status413PayloadTooLarge;

    public string ErrorMessage { get; }

    public UnsupportedFileSizeException(int mb)
    {
        ErrorMessage = $"File size must be less than {mb}MB!";
    }
    public UnsupportedFileSizeException(string msg) : base(msg)
    {
        ErrorMessage = msg;
    }
}
public class UnsupportedFileSizeException<T> : UnsupportedFileSizeException where T : IFormFile
{
    public UnsupportedFileSizeException(int mb)
        : base($"{typeof(T).Name} file size must be less than {mb}MB!") { }
}

