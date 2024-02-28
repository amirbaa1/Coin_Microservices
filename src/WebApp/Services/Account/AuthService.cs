using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Model.AccountModel;
using WebApp.Model.Mail;
using WebApp.Services.Mail;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebApp.Services.Account;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AuthService> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<AppUser> userManager, ILogger<AuthService> logger,
        SignInManager<AppUser> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public async Task<IdentityResult> RegisterAsync(AppUser appUser, string password, Claim claim)
    {
        try
        {
            var result = await _userManager.CreateAsync(appUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(appUser, claim);


                // await _signInManager.SignInAsync(appUser, isPersistent: false); // kod be kod login mashavad.
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
                false, lockoutOnFailure: true);
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

    public async Task<string> SendPasswordResetToken(string emailUser)
    {
        var user = await _userManager.FindByEmailAsync(emailUser);
        if (user == null)
        {
            throw new("User not found");
        }

        var createToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        return createToken;
    }

    public async Task<EditAdmin> GetByIdAccount(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new("User not found");
        }

        var userAcc = new EditAdmin
        {
            UserName = user.UserName,
            Email = user.Email,
            EmailActive = user.EmailConfirmed,
            Id = user.Id,
            Role = user.Role
        };

        return userAcc;
    }

    public async Task<EditAdmin> UpdateByIdRoleAccount(string id, string newRole)
    {
        if (!string.IsNullOrEmpty(id))
        {
            var accountUser = await _userManager.FindByIdAsync(id);
            if (accountUser == null)
            {
                return null;
            }

            var changeRole = await AssignRole(id, newRole);
            if (changeRole != false)
            {
                var editAdmin = new EditAdmin
                {
                    Id = accountUser.Id,
                    Role = accountUser.Role,
                    Email = accountUser.Email,
                    EmailActive = accountUser.EmailConfirmed,
                    UserName = accountUser.UserName
                };

                return editAdmin;
            }
        }

        return null;
    }

    public async Task<bool> AssignRole(string id, string roleName)
    {
        // var user = await _userManager.FindByEmailAsync(email);
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(user.Role)) // if user have role
        {
            var oldClaimRole = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == "RoleAccount");
            if (oldClaimRole != null)
            {
                var Remove = await _userManager.RemoveClaimAsync(user, oldClaimRole);
                if (!Remove.Succeeded)
                {
                    return false;
                }
            }

            var newClaim = new Claim("RoleAccount", roleName);

            user.Role = roleName;
            await _userManager.UpdateAsync(user);

            var resultUpdate = await _userManager.AddClaimAsync(user, newClaim);

            if (resultUpdate.Succeeded)
            {
                //var userRole = await _roleManager
                return true;
            }

            return false;
        }
        else
        {
            var claim = new Claim("RoleAccount", roleName);
            var result = await _userManager.AddClaimAsync(user, claim);

            user.Role = roleName;
            await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}