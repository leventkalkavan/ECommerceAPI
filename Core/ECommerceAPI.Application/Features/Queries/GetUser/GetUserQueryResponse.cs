using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.GetUser;

public class GetUserQueryResponse : IRequest<GetUserQueryRequest>
{
    public List<UserDto> Users { get; set; }
}

