using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using ECommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceAPI.Infrastructure.Services.Token;

public class TokenHandler: ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    
    public Application.DTOs.Token.Token CreateAccessToken(int second)
    {
        Application.DTOs.Token.Token token = new();

        // security keyin simetrigini aliyoruz
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        // sifrelenmis kimligi olusturuyorz
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // olusturulacak token ayarlarini veriyoruz
        token.Expiration = DateTime.UtcNow.AddSeconds(second);
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );
        
        // token olusturucu siniftan bir ornek alalım.
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);

        token.RefreshToken = CreateRefreshToken();
        return token;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator randomNumberG = RandomNumberGenerator.Create();
        randomNumberG.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}