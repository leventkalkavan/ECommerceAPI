using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateAppUser;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.DeleteUser;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Features.Commands.AppUser.UpdateUser;
using ECommerceAPI.Application.Features.Queries.GetUser;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse res = await _mediator.Send(createUserCommandRequest);
            return Ok(res);
        }
        
        [HttpGet]
        public async Task<ActionResult<GetUserQueryResponse>> GetAllUsers()
        {
            var query = new GetUserQueryRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
//test
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]DeleteUserCommandRequest req)
        {
            DeleteUserCommandResponse result = await _mediator.Send(req);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserCommandRequest req)
        {
            UpdateUserCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest req)
        {
            LoginUserCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}
