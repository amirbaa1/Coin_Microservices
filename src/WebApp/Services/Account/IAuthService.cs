using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApp.Model.AccountModel;

namespace WebApp.Services.Account;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(AppUser appUser, string password, Claim claim);

    Task<SignInResult> LoginAsync(LoginModel loginModel);
    Task SignOut();
    Task<string> SendPasswordResetToken(string emailUser);
    Task<EditAdmin> GetByIdAccount(string id);
    Task<EditAdmin> UpdateByIdRoleAccount(string id,string roleName);
    Task<bool> AssignRole(string id, string roleName);
}