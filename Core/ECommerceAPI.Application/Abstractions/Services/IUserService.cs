using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
}