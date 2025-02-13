using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MovieApp.BL.Constant;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.Core.Entities;

namespace MovieApp.BL.ExternalServices.Implements;
public class CurrentUser(IHttpContextAccessor _http, IMapper _mapper) : ICurrentUser
{
    ClaimsPrincipal? User = _http.HttpContext?.User;


    public string GetEmail()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Email)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public string GetFullname()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Fullname)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public int GetId()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Id)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return Convert.ToInt32(value);
    }

    public string GetUserName()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Username)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }
}

