using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
    private readonly UserManager<AppUser> _userManager;
    [BindProperty] public LoginModel LoginModel { get; set; }
    //[BindProperty] public IEnumerable<AuthenticationScheme> ExternalLoginProviders { get; set; }

    public Login(IAuthService authService, ILogger<Login> logger, SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
        _authService = authService;
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async void OnGetAsync()
    {
        if (TempData.TryGetValue("changePass", out var changePassMessage))
        {
            ViewData["ChangePasswordMessage"] = changePassMessage.ToString();
        }
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
            var user = await _userManager.FindByEmailAsync(LoginModel.UserName);

            if (loginResult.IsLockedOut)
            {
                ModelState.AddModelError("Login", "You are lockout");
            }

            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("Login", "Your account is not activated. Please activate your account.");
            }
            else
            {
                ModelState.AddModelError("Login", "Your email and password are incorrect!");
            }
        }

        return Page();
    }

    public async Task OnPostGoogle()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleResponse"),
        });
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
        });
        
    }
}