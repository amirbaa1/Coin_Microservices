using System.Security.Claims;
using IWebApp.Model.AccountModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Services.Account;

namespace WebApp.Pages.Account;

public class Register : PageModel
{
    private readonly IAuthService _authService;
    private readonly ILogger<Register> _logger;

    public Register(IAuthService authService, ILogger<Register> logger)
    {
        _authService = authService;
        _logger = logger;
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
        AppUser appUser = new AppUser()
        {
            Name = RegisterModel.Name,
            Email = RegisterModel.Email,
            UserName = RegisterModel.Email,
            PhoneNumber = RegisterModel.PhoneNumber,
            NormalizedEmail = RegisterModel.Email.ToUpper(),
        };
        _logger.LogInformation($"User : {appUser}");
        try
        {
            var result = await _authService.RegisterAsync(appUser, RegisterModel.Password, claimName);
            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                _logger.LogError(
                    $"User registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                return RedirectToPage("/RegisterFailed");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in code: {ex.Message}");

            return RedirectToPage("/Error");
        }
    }
}