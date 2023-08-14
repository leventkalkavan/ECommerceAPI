namespace ECommerceAPI.Application.Abstractions.Services.Authentications;

public interface IExternalAuthentication
{
    Task<DTOs.Token.Token> GoogleLoginAsync(string idToken,string provider);
    Task<DTOs.Token.Token> FacebookLoginAsync(string authToken);
}