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
    private readonly ILogger<CartInfo> _logger;
    private readonly HttpClient _httpClient;

    public CartInfo(UserManager<AppUser> userManager, IBasketService basketService, ILogger<CartInfo> logger,
        HttpClient httpClient)
    {
        _userManager = userManager;
        _basketService = basketService;
        _logger = logger;
        _httpClient = httpClient;
    }

    [BindProperty] public CoinCart CoinCartItem { get; set; }

    public async Task OnGet()
    {
        var userGet = await _userManager.GetUserAsync(User);
        var getCoinUser = await _basketService.GetBasket(userGet.UserName);

        _logger.LogInformation($"get Coin for page :{getCoinUser}");

        CoinCartItem = getCoinUser;
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userManager.GetUserAsync(User);

        CoinCartItem.UserName = user.UserName;
        _logger.LogInformation($"--> CoinItem : {CoinCartItem}");

        //var user = await _userManager.GetUserAsync(User);
        _logger.LogInformation($"--> CoinItem : {CoinCartItem}");

        var basketPost = await _basketService.PostBasket(CoinCartItem);
        return RedirectToPage("/order/checkout");
    }

    public async Task<IActionResult> OnPostDeleteCartBasket()
    {
        var user = await _userManager.GetUserAsync(User);
        await _basketService.DeleteBasket(user.UserName);
        TempData["deleteCart"] = "Deleted Order Coin in basket";
        return RedirectToPage("../Index");
    }
}