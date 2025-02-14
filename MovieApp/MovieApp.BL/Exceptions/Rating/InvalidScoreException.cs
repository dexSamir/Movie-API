using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Rating;
public class InvalidScoreException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public InvalidScoreException()
    {
        ErrorMessage = "Rating 1 ile 10 arasinda olmalidir!";
    }

    public InvalidScoreException(string msg) : base(msg)
    {
        ErrorMessage = msg;
    }
}

