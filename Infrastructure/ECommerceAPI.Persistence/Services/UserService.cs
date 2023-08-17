using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
    {
        var result = await _userManager.CreateAsync(new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            Email = model.Email,
            NameSurname = model.NameSurname
        }, model.Password);

        CreateUserResponseDto response = new() { IsSuccess = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanici basariyla olusturuldu.";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}/n";
        return response;
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate,
        int refreshTokenDate)
    {
        var appUser = await _userManager.FindByIdAsync(user.Id);
        if (appUser != null)
        {
            appUser.RefreshToken = refreshToken;
            appUser.RefreshTokenTime = accessTokenDate.AddSeconds(refreshTokenDate);
            await _userManager.UpdateAsync(appUser);
        }
        else
        {
            throw new Exception();
        }
    }
}