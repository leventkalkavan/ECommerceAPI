using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        if (user == null)
            await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user==null)
            return new LoginUserCommandErrorResponse()
            {
                IsSuccess = false,
                Message = "Kullanici bulunamadı."
            };

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);
        if (result.Succeeded) //authentication basarili
        {
            Token token = _tokenHandler.CreateAccessToken(5);
            return new LoginUserCommandSuccessResponse()
            {
                Token = token
            };
        }
        return new LoginUserCommandErrorResponse()
        {
            Message = "Hata olustu"
        };
    }
}