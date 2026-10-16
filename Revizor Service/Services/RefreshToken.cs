using System;
using System.Security.Cryptography;

namespace Revizor_Service.Services;

public class RefreshToken
{
    public static string GenerateRefreshToken() 
    {
        var randomNumber = new byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
