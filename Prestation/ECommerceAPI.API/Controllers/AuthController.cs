using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest req)
        {
            LoginUserCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromForm]RefreshTokenLoginCommandRequest req)
        {
            RefreshTokenLoginCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest req)
        {
            GoogleLoginCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }
        
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }
    }
}
