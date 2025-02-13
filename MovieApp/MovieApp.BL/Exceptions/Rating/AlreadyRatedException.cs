using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Rating;
public class AlreadyRatedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status409Conflict;

    public string ErrorMessage { get; }

    public AlreadyRatedException()
    {
        ErrorMessage = "Siz bu filme artiq xal vermisiniz!"; 
    }
    public AlreadyRatedException(string msg) : base(msg)
    {
        ErrorMessage = msg; 
    }
}

