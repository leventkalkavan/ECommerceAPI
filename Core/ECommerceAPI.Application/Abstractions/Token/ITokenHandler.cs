namespace ECommerceAPI.Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token.Token CreateAccessToken(int minute);
} 