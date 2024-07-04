using filmsAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace filmsAPI.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    
    public TokenService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims = new Claim[]
        {
            new("username", user.UserName),
            new("userid", user.Id),
            new(ClaimTypes.DateOfBirth, user.BirthdayDate.ToString()),
        };

        var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);
        var symmetricKey = new SymmetricSecurityKey(key);

        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(expires: DateTime.Now.AddDays(30), claims: claims, signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}