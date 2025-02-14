using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Rating;
public class InvalidEpisodeAssociationException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status500InternalServerError;

    public string ErrorMessage { get; }

    public InvalidEpisodeAssociationException()
    {
        ErrorMessage = "Episode hansisa seriala ve ya Season-a aid olmalidir!"; 
    }

    public InvalidEpisodeAssociationException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}

