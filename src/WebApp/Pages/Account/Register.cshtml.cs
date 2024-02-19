using System.Security.Claims;
using WebApp.Model.AccountModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.Mail;
using WebApp.Services.Account;
using WebApp.Services.Mail;

namespace WebApp.Pages.Account;

public class Register : PageModel
{
    private readonly IAuthService _authService;
    private readonly ILogger<Register> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;

    public Register(IAuthService authService, ILogger<Register> logger, UserManager<AppUser> userManager,
        IEmailService emailService)
    {
        _authService = authService;
        _logger = logger;
        _userManager = userManager;
        _emailService = emailService;
    }

    [BindProperty] public RegisterModel RegisterModel { get; set; } = new RegisterModel();

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var claimName = new Claim("Name", RegisterModel.Name);
        var appUser = new AppUser()
        {
            Name = RegisterModel.Name,
            Email = RegisterModel.Email,
            UserName = RegisterModel.Email,
            PhoneNumber = RegisterModel.PhoneNumber,
            NormalizedEmail = RegisterModel.Email.ToUpper(),
            Role = RegisterModel.Role,
        };
        _logger.LogInformation($"User : {appUser}");
        try
        {
            var result = await _authService.RegisterAsync(appUser, RegisterModel.Password, claimName);
            if (result.Succeeded)
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                // return Redirect(Url.PageLink(pageName: "/account/RegisterConfirmation",
                //     values: new { userId = appUser.Id, token = confirmationToken }) ?? "");

                // To trigger the email confirmation flow, use the code below
                //////////////////////////////////////////////////////////////

                var confirmationLink = Url.PageLink(pageName: "/Account/RegisterConfirmation",
                    values: new { userId = appUser.Id, token = confirmationToken }) ?? "";

                var email = new Email()
                {
                    To = appUser.Email,
                    Body = $"confirmEmail : {confirmationLink}",
                    Subject = "Confirm Email CoinMarket",
                };
                await _emailService.SendEmail(email);

                return RedirectToPage("/Account/Login");
                // return RedirectToPage("/Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in code: {ex.Message}");

            return RedirectToPage("/Error");
        }
    }
}