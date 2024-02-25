using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Services.Account;

namespace WebApp.Pages.Account.Admin
{
    public class DetailsModel : PageModel
    {
        //[BindProperty]
        //public EditAdmin editAdmin { get; set; }
        private UserManager<AppUser> _userManager;
        [BindProperty]
        public AppUser appUser { get; set; }
        private readonly IAuthService _authService;
        public DetailsModel(UserManager<AppUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
            {
                return NotFound();
            }

            return Page();

        }

    }

}
