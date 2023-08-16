using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Token;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler: IRequestHandler<RefreshTokenLoginCommandRequest,RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        Token token= await _authService.RefreshTokenLoginAsync(request.RefresToken);
        return new()
        {
            Token = token
        };
    }
}