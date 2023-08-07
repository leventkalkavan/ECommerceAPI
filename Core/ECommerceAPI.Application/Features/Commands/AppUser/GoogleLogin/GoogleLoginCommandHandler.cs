using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest,GoogleLoginCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }


    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> {"434898939699-afe87t9d0mn37hnlmca7vs9dobad5rf2.apps.googleusercontent.com"}
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
       var info = new UserLoginInfo(request.Provider, payload.Subject,request.Provider);
       Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
       bool result = user != null;
       if (user == null)
       {
           user = await _userManager.FindByEmailAsync(payload.Email);
           if (user == null)
           {
               user = new()
               {
                   Id = Guid.NewGuid().ToString(),
                   Email = payload.Email,
                   UserName = payload.Email,
                   NameSurname = payload.Name
               };
               var identityResult = await _userManager.CreateAsync(user);
               result = identityResult.Succeeded;
           }
       }

       if (result)
           //aspNetUserLogins
           await _userManager.AddLoginAsync(user, info);
       else
           throw new Exception("Invalid external authentication.");

       Token token = _tokenHandler.CreateAccessToken(5);

       return new()
       {
           Token = token
       };
    }
}