using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

    public UpdateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return new UpdateUserCommandResponse
            {
                IsSuccess = false,
                Message = "Kullanıcı bulunamadı."
            };
        }
        user.UserName = request.UserName;
        user.Email = request.Email;

        IdentityResult result = await _userManager.UpdateAsync(user);
        UpdateUserCommandResponse response = new() { IsSuccess = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanici basariyla guncellendi";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}/n";
        return response;
    }
}
