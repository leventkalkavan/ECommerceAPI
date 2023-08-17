namespace ECommerceAPI.Application.Abstractions.Services.Authentications;

public interface IExternalAuthentication
{
    Task<DTOs.Token.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
    Task<DTOs.Token.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
}