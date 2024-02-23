using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Model.Mail;
using WebApp.Services.Mail;

namespace WebApp.Pages.Account;

public class RegisterConfirmation : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;

    public RegisterConfirmation(UserManager<AppUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    [BindProperty] public string Message { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                Message = "ادرس ایمیل شما تایید شد لطفا دوباره وارد شوید.";
                return Page();
            }
        }

        Message = "Failed to validate email.";
        return Page();
    }
    public async Task<IActionResult> OnPost(string userId)
    {
        var userEmail = await _userManager.FindByIdAsync(userId);
        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userEmail);
        var confirmationLink = Url.PageLink(pageName: "/Account/RegisterConfirmation",
                   values: new { userId = userEmail.Id, token = confirmationToken }) ?? "";

        var email = new Email()
        {
            To = userEmail.Email!,
            Body = $"confirmEmail : {confirmationLink}",
            Subject = "Confirm Email CoinMarket",
        };
        await _emailService.SendEmail(email);
        return RedirectToPage("/account/login");

    }
}