using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.DeleteUser;

public class DeleteUserCommandResponse : IRequest<DeleteUserCommandRequest>
{
    public bool IsSuccess{ get; set; }
    public string Message { get; set; }
}