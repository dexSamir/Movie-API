using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace BlogApp.BL.Exceptions.AuthException;
public class CodeIsInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public CodeIsInvalidException()
    {
        ErrorMessage = "Gonderilen kod yanlisdir!";
    }
    public CodeIsInvalidException(string msg) : base(msg)
    {
        ErrorMessage = msg;
    }
}

