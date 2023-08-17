using ECommerceAPI.Application.DTOs.Token;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandResponse : IRequest<GoogleLoginCommandRequest>
{
    public Token Token { get; set; }
}