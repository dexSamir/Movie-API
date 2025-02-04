using System;
using Microsoft.AspNetCore.Http;
using MovieApp.BL.Exceptions.Base;

namespace MovieApp.BL.Exceptions.Common;
public class NotFoundException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound; 

    public string ErrorMessage { get; }

    public NotFoundException()
    {
        ErrorMessage = "Not found!";
    }

    public NotFoundException(string msg) : base (msg)
    {
        ErrorMessage = msg; 
    }
}

public class NotFoundException<T> : NotFoundException
{
    public NotFoundException() : base(typeof(T).Name + " is not found!")
    { }
} 