using ECommerceAPI.Application.DTOs.Token;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandResponse
{
}

public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
{
    public Token Token { get; set; }
}

public class LoginUserCommandErrorResponse : LoginUserCommandResponse
{
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}