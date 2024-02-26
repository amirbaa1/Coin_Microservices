using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Model.Mail;
using WebApp.Services.Mail;

namespace WebApp.Pages.Account;

public class ForgetPassword : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;

    public ForgetPassword(UserManager<AppUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    [BindProperty] public string Message { get; set; }
    [BindProperty] public ForgetPasswordModel forgetPasswordModels { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.FindByEmailAsync(forgetPasswordModels.Email);

        if (user != null)
        {
            var createTokenPass = await _userManager.GeneratePasswordResetTokenAsync(user);
            var changePassLink = Url.PageLink(pageName: "/Account/ChangePassword",
                values: new { userId = user.Id, token = createTokenPass }) ?? "";

            var email = new Email()
            {
                To = user.Email,
                Body = $"confirmEmail : {changePassLink}",
                Subject = "Confirm Email CoinMarket",
            };
            await _emailService.SendEmail(email);
            
            Message = "لینک تغییر پسورد برای شما ارسال شد.";
            return Page();
        }

        return Page();

    }
}