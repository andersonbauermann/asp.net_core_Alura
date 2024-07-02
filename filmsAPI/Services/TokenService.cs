using filmsAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace filmsAPI.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        Claim[] claims = new Claim[]
        {
            new("username", user.UserName),
            new("userid", user.Id),
            new(ClaimTypes.DateOfBirth, user.BirthdayDate.ToString()),
        };

        byte[] key = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }

        var symmetricKey = new SymmetricSecurityKey(key);

        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(expires: DateTime.Now.AddDays(30), claims: claims, signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}