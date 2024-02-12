using System.Security.Claims;
using IWebApp.Model.AccountModel;
using Microsoft.AspNetCore.Identity;
using WebApp.Model.AccountModel;

namespace WebApp.Services.Account;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(AppUser appUser, string password, Claim claim);
    Task<SignInResult> LoginAsync(LoginModel loginModel);
}