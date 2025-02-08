using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Common;
public class ExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status409Conflict;

    public string ErrorMessage { get; }

    public ExistException()
    {
        ErrorMessage = "This data already exists";
    }
    public ExistException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}

public class ExistException<T> : ExistException
{
    public ExistException() : base(typeof(T).Name + " already exists!")
    { }
}
