using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandRequest: IRequest<RefreshTokenLoginCommandResponse>
{
    public string RefresToken { get; set; }
}