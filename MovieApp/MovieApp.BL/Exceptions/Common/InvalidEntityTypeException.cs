using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Common;
public class InvalidEntityTypeException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public InvalidEntityTypeException()
    {
        ErrorMessage = "Invalid Entity type!"; 
    }

    public InvalidEntityTypeException(string message) : base(message)
    {
        ErrorMessage = message; 
    }
}

public class InvalidEntityTypeException<T> : InvalidEntityTypeException
{
    public InvalidEntityTypeException() : base("Invalid Entity type: " + typeof(T).Name)
    { }
}

