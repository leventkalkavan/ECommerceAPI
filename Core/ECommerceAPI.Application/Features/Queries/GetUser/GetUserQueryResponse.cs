using ECommerceAPI.Application.DTOs.User;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.GetUser;

public class GetUserQueryResponse : IRequest<GetUserQueryRequest>
{
    public List<UserDto> Users { get; set; }
}