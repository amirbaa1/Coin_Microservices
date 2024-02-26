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

        public async Task OnGet(string userId)
        {
            //     if (userId == null)
            //     {
            //         return NotFound();
            //     }
            //     appUser = await _userManager.FindByIdAsync(userId);
            //
            //     if (appUser == null)
            //     {
            //         return NotFound();
            //     }
            //
            //     return Page();
            // var result = await _authService.GetIdUserEditAdmin(userId);
            // if (result != null && result.Any())
            // {
            //     EditAdmins = result.First();
            // }
            // else
            // {
            //     EditAdmins = null;
            // }
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