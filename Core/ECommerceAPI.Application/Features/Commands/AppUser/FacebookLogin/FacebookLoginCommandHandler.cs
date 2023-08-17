using System.Text.Json;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Application.DTOs.Token;
using Google.Apis.Http;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;

namespace ECommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;

public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest,FacebookLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public FacebookLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {
      var token =  await _authService.FacebookLoginAsync(request.AuthToken,15);
      return new()
      {
          Token = token
      };
    }
}