using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _userService.CreateAsync(new CreateUserDto
        {
            Email = request.Email,
            NameSurname = request.NameSurname,
            UserName = request.UserName,
            PasswordAgain = request.PasswordAgain,
            Password = request.Password
        });
        return new CreateUserCommandResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
}