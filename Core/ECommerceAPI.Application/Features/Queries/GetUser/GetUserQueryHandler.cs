using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQueryRequest, GetUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public GetUserQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetUserQueryResponse> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync();
        var response = new GetUserQueryResponse
        {
            Users = users.Select(user => new UserDto
            {
                Id = user.Id,
                NameSurname = user.NameSurname,
                UserName = user.UserName,
                Email = user.Email
            }).ToList()
        };

        return response;
    }
}