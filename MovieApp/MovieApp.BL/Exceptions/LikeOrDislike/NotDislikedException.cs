using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.LikeOrDislike;
public class NotDislikedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public NotDislikedException()
    {
        ErrorMessage = "Buna Dislike Atilmayib!";
    }

    public NotDislikedException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
public class NotDislikedException<T> : NotDislikedException
{
    public NotDislikedException() : base(typeof(T).Name + "not disliked!")
    {

    }
}

