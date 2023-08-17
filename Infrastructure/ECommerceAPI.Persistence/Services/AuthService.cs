using System.Text.Json;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Application.DTOs.Token;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserService _userService;

    public AuthService(IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        UserManager<AppUser> userManager,
        ITokenHandler tokenHandler,
        SignInManager<AppUser> signInManager,
        IUserService userService)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
    }

    public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        var accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        var facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        var userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

        if (validation?.Data.IsValid != null)
        {
            var userInfoResponse =
                await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
        }

        throw new Exception("Invalid external authentication.");
    }

    public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
    }

    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user == null)
            throw new Exception();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded) //Authentication başarılı!
        {
            var token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }

        throw new Exception();
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user != null && user?.RefreshTokenTime > DateTime.UtcNow)
        {
            var token = _tokenHandler.CreateAccessToken(15, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }

        throw new Exception();
    }

    private async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info,
        int accessTokenLifeTime)
    {
        var result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    NameSurname = name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
        {
            await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

            var token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }

        throw new Exception("Invalid external authentication.");
    }
}