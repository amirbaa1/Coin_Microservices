using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Services.Mail;

namespace WebApp.Pages.Account;

public class ChangePassword : PageModel
{
    private readonly UserManager<AppUser> _userManager;

    [BindProperty] public ChangePasswordModel changePasswordModel { get; set; }
    [BindProperty] public string userId { get; set; }

    [BindProperty] public string token { get; set; }

    public ChangePassword(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty] public string Message { get; set; }

    public async Task<IActionResult> OnGetAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
        {
            Message = "Failed to validate email.";
            return Page();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string userId, string token, ChangePasswordModel changePasswordModel)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (changePasswordModel.NewPassword != changePasswordModel.ConfirmPassword)
        {
            ModelState.AddModelError("changePasswordModel.ConfirmPassword", "Password and confirmation do not match.");
            return Page();
        }

        var passwordChange = await _userManager.ResetPasswordAsync(user, token, changePasswordModel.NewPassword);
        if (passwordChange.Succeeded)
        {
            TempData["changePass"] = "پسورد تغییر کرد لطفا وارد شوید.";
            return RedirectToPage("/account/login");
        }
        else
        {
            foreach (var error in passwordChange.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}