using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.LikeOrDislike;
public class AlreadyDislikedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public AlreadyDislikedException()
    {
        ErrorMessage = "Buna artiq dislike atilib!";
    }

    public AlreadyDislikedException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
public class AlreadyDislikedException<T> : AlreadyDislikedException
{
    public AlreadyDislikedException() : base(typeof(T).Name + "already disliked!")
    {

    }
}

