using ECommerceAPI.Application.Abstractions.Token;

namespace ECommerceAPI.Application.DTOs.Token;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}