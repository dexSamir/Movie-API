using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.LikeOrDislike;
public class NotLikedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest; 

    public string ErrorMessage { get; }

    public NotLikedException()
    {
        ErrorMessage = "Buna Like Atilmayib!";
    }

    public NotLikedException(string message) : base(message)
    {
        ErrorMessage = message; 
    }
}
public class NotLikedException<T> : NotLikedException
{
    public NotLikedException() : base(typeof(T).Name + "not liked!")
    {

    }
}

