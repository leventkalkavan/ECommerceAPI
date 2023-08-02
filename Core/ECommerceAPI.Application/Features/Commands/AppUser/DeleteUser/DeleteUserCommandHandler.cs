using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.DeleteUser;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommandRequest,DeleteUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

    public DeleteUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return new DeleteUserCommandResponse
            {
                IsSuccess = false,
                Message = "Kullanici bulunamadi."
            };
        }
        
        IdentityResult result = await _userManager.DeleteAsync(user);
        DeleteUserCommandResponse response = new() { IsSuccess = result.Succeeded };
        if (result.Succeeded)
            response.Message = "Kullanici basariyla silinmistir.";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}/n";
        return response;
    }
}