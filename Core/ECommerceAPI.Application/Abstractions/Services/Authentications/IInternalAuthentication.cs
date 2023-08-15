using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services.Authentications;

public interface IInternalAuthentication
{
    Task<DTOs.Token.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
    Task<DTOs.Token.Token> RefreshTokenLoginAsync(string refreshToken);
}