using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Common;
public class ForbiddenException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status403Forbidden;

    public string ErrorMessage { get; }

    public ForbiddenException()
    {
        ErrorMessage = "Sizin bu emeliyyata icazeniz yoxdur!"; 
    }

    public ForbiddenException(string message) : base(message)
    {
        ErrorMessage = message; 
    }
}
public class ForbiddenException<T> : ForbiddenException
{
    public ForbiddenException() : base("Unauthorized Action " + typeof(T).Name)
    { }
}

