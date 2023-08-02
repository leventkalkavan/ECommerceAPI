using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateAppUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
{
    readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.UserName,
            Email = request.Email,
            NameSurname = request.NameSurname,
        }, request.Password);
        
        
        
        CreateUserCommandResponse response = new() { IsSuccess = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanici basariyla olusturuldu.";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}/n";
        return response;
    }
}