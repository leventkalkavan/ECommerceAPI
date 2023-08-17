using ECommerceAPI.Application.Features.Commands.AppUser.DeleteUser;
using ECommerceAPI.Application.Features.Commands.AppUser.UpdateUser;
using ECommerceAPI.Application.Features.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    [HttpGet]
    public async Task<ActionResult<GetUserQueryResponse>> GetAllUsers()
    {
        var query = new GetUserQueryRequest();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserCommandRequest req)
    {
        var result = await _mediator.Send(req);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest req)
    {
        var res = await _mediator.Send(req);
        return Ok(res);
    }
}