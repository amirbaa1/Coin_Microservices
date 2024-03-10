using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Services;

namespace WebApp.Pages.Cart;

public class cartinfo : PageModel
{
    private readonly IBasketService _basketService;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<cartinfo> _logger;

    public cartinfo(IBasketService basketService, UserManager<AppUser> userManager, ILogger<cartinfo> logger)
    {
        _basketService = basketService;
        _userManager = userManager;
        _logger = logger;
    }

    public BasketModel cart { get; set; } = new BasketModel();

    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/account/login");
        }

        cart = await _basketService.GetBasket(user.UserName);
        _logger.LogInformation($"cart : {JsonConvert.SerializeObject(cart)}");
        return Page();
    }
}