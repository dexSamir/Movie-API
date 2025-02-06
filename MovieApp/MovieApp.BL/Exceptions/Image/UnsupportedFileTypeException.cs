using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Image;
public class UnsupportedFileTypeException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status415UnsupportedMediaType; 

    public string ErrorMessage { get; }

    public UnsupportedFileTypeException()
    {
        ErrorMessage = "File type must be an image!"; 
    }
    public UnsupportedFileTypeException(string msg) : base(msg)
    {
        ErrorMessage = msg; 
    }
}
public class UnsupportedFileTypeException<T> : UnsupportedFileTypeException where T : IFormFile 
{
    public UnsupportedFileTypeException() : base($"{typeof(T).Name} must be an image!")
    { }
}

