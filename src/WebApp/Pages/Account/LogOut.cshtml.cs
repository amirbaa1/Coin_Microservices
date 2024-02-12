using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services.Account;

namespace WebApp.Pages.Account
{
    public class LogOutModel : PageModel
    {
        private readonly IAuthService _authService;

        public LogOutModel(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            // await HttpContext.SignOutAsync("MyCooki");
            await _authService.SginOut();
            return RedirectToPage("/index");
        }
    }
}
