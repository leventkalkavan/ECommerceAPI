using ECommerceAPI.Application.DTOs.Token;

namespace ECommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;

public class FacebookLoginCommandResponse
{
    public Token Token { get; set; }
}