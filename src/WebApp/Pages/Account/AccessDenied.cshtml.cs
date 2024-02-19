using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Account;

[Authorize]
public class AccessDenied : PageModel
{
    public void OnGet()
    {
    }
}