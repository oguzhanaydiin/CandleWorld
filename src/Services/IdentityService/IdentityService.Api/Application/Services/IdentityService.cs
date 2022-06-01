using IdentityService.Api.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Api.Application.Services;

public class IdentityService : IIdentityService
{

    public Task<LoginResponseModel> Login(LoginRequestModel request)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, request.UserName),
            new Claim(ClaimTypes.Name, "Oz Ay"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TechBuddySecretKeyShouldBeLong"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(2);

        var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: creds, notBefore: DateTime.Now);

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

        LoginResponseModel response = new()
        {
            UserToken = encodedJwt,
            UserName = request.UserName,
        };

        return Task.FromResult(response);
    }
}
