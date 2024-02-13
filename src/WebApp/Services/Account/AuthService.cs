using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApp.Model.AccountModel;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebApp.Services.Account;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AuthService> _logger;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(UserManager<AppUser> userManager, ILogger<AuthService> logger,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(AppUser appUser, string password, Claim claim)
    {
        try
        {
            var result = await _userManager.CreateAsync(appUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(appUser, claim);
                await _signInManager.SignInAsync(appUser, isPersistent: false);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in code :{ex.Message}");
            throw;
        }
    }

    public async Task<SignInResult> LoginAsync(LoginModel loginModel)
    {
        try
        {
            // var result = await _signInManager.PasswordSignInAsync(loginModel.UserName,
            //     loginModel.Password,
            //     isPersistent: true,
            //     lockoutOnFailure: true);
            var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password,
                false, lockoutOnFailure: false);
            _logger.LogInformation($"---> {result}");
            if (!result.Succeeded)
            {
                // Get error details
                var errors = result.ToString();
                _logger.LogError($"Login failed with errors: {errors}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error login in code :{ex.Message}");
            throw;
        }
    }

    public async Task SignOut()
    {
        await _signInManager.SignOutAsync();
    }
}