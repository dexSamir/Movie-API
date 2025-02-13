using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.AuthException;
public class AuthorisationException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status401Unauthorized;
    public string ErrorMessage { get; }

    public AuthorisationException()
    {
        ErrorMessage = "User not logged in";
    }

    public AuthorisationException(string message) : base(message)
    {
        ErrorMessage = message;
    }

}
public class AuthorisationException<T> : AuthorisationException
{
    public AuthorisationException() : base(typeof(T).Name + "not logged in")
    {

    }
}

