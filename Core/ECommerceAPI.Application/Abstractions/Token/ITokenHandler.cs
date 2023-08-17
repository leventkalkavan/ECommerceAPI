using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token.Token CreateAccessToken(int second, AppUser user);
    string CreateRefreshToken();
}