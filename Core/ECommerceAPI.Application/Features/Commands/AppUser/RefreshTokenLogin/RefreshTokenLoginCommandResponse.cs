using ECommerceAPI.Application.DTOs.Token;

namespace ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public Token Token { get; set; }
}