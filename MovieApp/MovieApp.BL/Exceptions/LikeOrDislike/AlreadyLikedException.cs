using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.LikeOrDislike;
public class AlreadyLikedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public AlreadyLikedException()
    {
        ErrorMessage = "Buna artiq like atilib!";
    }

    public AlreadyLikedException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
public class AlreadyLikedException<T> : AlreadyLikedException
{
    public AlreadyLikedException() : base(typeof(T).Name + "already liked!")
    {

    }
}

