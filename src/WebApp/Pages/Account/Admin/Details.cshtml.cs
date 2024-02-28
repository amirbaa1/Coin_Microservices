using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Services.Account;

namespace WebApp.Pages.Account.Admin
{
    public class DetailsModel : PageModel
    {
        [BindProperty] public EditAdmin EditAdmins { get; set; }
        private UserManager<AppUser> _userManager;
        [BindProperty] public AppUser appUser { get; set; }
        private readonly IAuthService _authService;

        public DetailsModel(UserManager<AppUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<IActionResult> OnGet(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                EditAdmins = await _authService.GetByIdAccount(userId);
            }

            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _authService.UpdateByIdRoleAccount(EditAdmins.Id, EditAdmins.Role);
                return RedirectToPage("/account/admin/panel");
            }

            return Page();
        }

        // public IActionResult OnPost()
        // {
        //     if (ModelState.IsValid)
        //     {
        //         if (EditAdmins != null)
        //         {
        //             _authService.UpdateForAdmin(EditAdmins);
        //             TempData["success"] = "Update !";
        //             return RedirectToPage("./Details", new { id = EditAdmins.Id });
        //         }
        //         else
        //         {
        //             TempData["error"] = "Invalid data";
        //         }
        //     }
        //     else
        //     {
        //         TempData["error"] = "Model is not valid";
        //     }
        //
        //     return RedirectToPage("./Details", new { id = EditAdmins?.Id });
        // }
    }
}