using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Model.AccountModel;
using WebApp.Services.Account;

namespace WebApp.Pages.Account.Admin;

[Authorize(Policy = "Admin")]
public class Panel : PageModel
{
    private readonly DatadbContext _context;
    public List<AppUser> AppUsers { get; set; } = new List<AppUser>();

    public Panel(DatadbContext context)
    {
        _context = context;
    }


    public async Task OnGet()
    {
        AppUsers = await _context.Users.ToListAsync();
    }
}