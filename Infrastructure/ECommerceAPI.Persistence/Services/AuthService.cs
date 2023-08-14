using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Application.DTOs.Token;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using Google.Apis.Auth;

namespace ECommerceAPI.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(HttpClient httpClient, IConfiguration configuration, UserManager<AppUser> userManager,
        ITokenHandler tokenHandler, SignInManager<AppUser> signInManager)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
    }

    public async Task<Token> FacebookLoginAsync(string authToken)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id{_configuration["ExternalLoginSettings:Facebook:ClientId"]}&" +
            $"client_secret={_configuration["ExternalLoginSettings:Facebook:ClientSecret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponse facebookAccessTokenResponse =
            JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse.AccessToken}");

        FacebookUserAccessTokenValidation? validation =
            JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

        if (validation?.Data.IsValid != null)
        {
            string userInfoResponse =
                await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponse userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            Domain.Entities.Identity.AppUser user =
                await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userInfo?.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userInfo?.Email,
                        UserName = userInfo?.Email,
                        NameSurname = userInfo?.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Token token = _tokenHandler.CreateAccessToken(5);
                return token;
            }
        }

        throw new Exception("Invalid external authentication.");
    }

    public async Task<Token> GoogleLoginAsync(string idToken, string provider)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { "434898939699-afe87t9d0mn37hnlmca7vs9dobad5rf2.apps.googleusercontent.com" }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        var info = new UserLoginInfo(provider, payload.Subject, provider);
        Domain.Entities.Identity.AppUser user =
            await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        bool result = user != null;

        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.Email,
                    NameSurname = payload.Name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
            //aspNetUserLogins
            await _userManager.AddLoginAsync(user, info);
        else
            throw new Exception("Invalid external authentication.");

        Token token = _tokenHandler.CreateAccessToken(5);
        return token;
    }

    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            await _userManager.FindByEmailAsync(usernameOrEmail);
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded) //authentication basarili
        {
            Token token = _tokenHandler.CreateAccessToken(5);
            return token;
        }
        throw new Exception("The user could not be found.");
    }
}