using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieApp.BL.Constant;
using MovieApp.BL.DTOs.Options;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.BL.ExternalServices.Implements;
public class JwtTokenHandler : IJwtTokenHandler
{
    readonly JwtOptions _opt;
    public JwtTokenHandler(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value;
    }

    public string CreateToken(User user, int hours)
    {
        List<Claim> claims = [
                    new Claim(ClaimType.Username, user.UserName),
                    new Claim(ClaimType.Email, user.Email),
                    new Claim(ClaimType.Id, user.Id), 
                    new Claim(ClaimType.Fullname, user.Name + " " + user.Surname),
                    new Claim(ClaimType.IsMale, user.IsMale.ToString())
                ];

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));

        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(hours),
                signingCredentials: cred);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(jwt);
    }
}

