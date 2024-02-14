using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Model.OrderModel;
using WebApp.Services;


namespace WebApp.Pages.Account;

public class Profile : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderService _orderService;
    private readonly ILogger<Profile> _logger;
    [BindProperty] public UserDto UserDto { get; set; }
    [BindProperty] public AppUser appuser { get; set; }
    [BindProperty] public List<OrderModel> orderModel { get; set; }

    public Profile(UserManager<AppUser> userManager, IOrderService orderService, ILogger<Profile> logger)
    {
        _userManager = userManager;
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet(string id)
    {
        var userP = await _userManager.GetUserAsync(User);
        if (userP == null)
        {
            return RedirectToPage("/account/login");
        }

        UserDto = new UserDto // see in profile
        {
            ID = userP.Id.ToString(),
            Email = userP.Email,
            PhoneNumber = userP.PhoneNumber,
            Name = userP.Name,
        };
        appuser = new AppUser
        {
            EmailConfirmed = userP.EmailConfirmed,
        };

        orderModel = await _orderService.GetOrder(userP.UserName);
        // _logger.LogInformation($"order : --->{orderModel}");
        return Page();
    }

    // public Task<IActionResult> OnPost()
    // {
    // }
}