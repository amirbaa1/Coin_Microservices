using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Services.Account;

namespace WebApp.Pages.Account;

public class Login : PageModel
{
    private readonly IAuthService _authService;
    private readonly ILogger<Login> _logger;
    private readonly SignInManager<AppUser> _signInManager;

    [BindProperty] public LoginModel LoginModel { get; set; }
    //[BindProperty] public IEnumerable<AuthenticationScheme> ExternalLoginProviders { get; set; }

    public Login(IAuthService authService, ILogger<Login> logger, SignInManager<AppUser> signInManager)
    {
        _authService = authService;
        _logger = logger;
        _signInManager = signInManager;
    }

    public async void OnGetAsync()
    {
        //ExternalLoginProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
    }

    public async Task<IActionResult> OnPost()
    {
        var loginResult = await _authService.LoginAsync(LoginModel);
        if (loginResult.Succeeded)
        {
            return RedirectToPage("/Index");
        }
        else
        {
            if (loginResult.IsLockedOut)
            {
                ModelState.AddModelError("Login", "You are lockout");
            }
            else
            {

                ModelState.AddModelError("Login", "Failed Login");
            }
        }
        return Page();
    }
}