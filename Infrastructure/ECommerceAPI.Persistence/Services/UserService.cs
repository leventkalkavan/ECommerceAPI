using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services;

public class UserService: IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            Email = model.Email,
            NameSurname = model.NameSurname,
        }, model.Password);
        
        CreateUserResponseDto response = new() { IsSuccess = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanici basariyla olusturuldu.";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}/n";
        return response;
    }
}