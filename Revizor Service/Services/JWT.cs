using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Revizor_Service.Database.Models;

namespace Revizor_Service.Services;

public class JWT
{
    public static string GenToken(User us, IConfiguration cfg)
    {
        var claims  = new []
        {
            new Claim(ClaimTypes.NameIdentifier, us.Id.ToString()),
            new Claim(ClaimTypes.Name, us.GetFullName()),
            new Claim(ClaimTypes.Role, us.Role!.Name)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["JWT:Key"]!));
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var tokem = new JwtSecurityToken(
            issuer: cfg["JWT:Issuer"],
            audience: cfg["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(tokem);
    }
}
