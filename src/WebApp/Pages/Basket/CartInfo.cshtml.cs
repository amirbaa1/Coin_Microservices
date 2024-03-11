using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Services;

namespace WebApp.Pages.Basket;

public class CartInfo : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IBasketService _basketService;

    public CartInfo(UserManager<AppUser> userManager, IBasketService basketService)
    {
        _userManager = userManager;
        _basketService = basketService;
    }

    public CoinCart? CoinCartItem { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var userGet = await _userManager.GetUserAsync(User);
        CoinCartItem = await _basketService.GetBasket(userGet.UserName);

        return Page();
    }
}