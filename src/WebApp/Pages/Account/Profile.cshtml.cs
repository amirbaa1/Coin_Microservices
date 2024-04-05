using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Model.OrderModel;
using WebApp.Services;


namespace WebApp.Pages.Account;

public class Profile : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderService _orderService;
    private readonly ILogger<Profile> _logger;
    private readonly IWalletService _walletService;
    private readonly IBasketService _basketService;
    [BindProperty] public UserDto UserDto { get; set; }
    [BindProperty] public AppUser appuser { get; set; }
    [BindProperty] public List<OrderModel> orderModel { get; set; }
    [BindProperty] public List<WalletModel> wallet { get; set; }
    public bool ErrorFlags { get; set; } = false;

    public Profile(UserManager<AppUser> userManager, IOrderService orderService, ILogger<Profile> logger, IWalletService walletService, IBasketService basketService)
    {
        _userManager = userManager;
        _orderService = orderService;
        _logger = logger;
        _walletService = walletService;
        _basketService = basketService;
    }

    public async Task<IActionResult> OnGet(string id)
    {
        try
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
                Name = userP.Name,
                Role = userP.Role,
            };

            await _walletService.UpdateCoinWallet(userP.UserName);

            orderModel = (await _orderService.GetOrder(userP.UserName)).OrderByDescending(order => order.DateTime).ToList();

            wallet = await _walletService.OnGetWalletByUserName(userP.UserName);

            return Page();
        }
        catch (Exception ex)
        {
            ErrorFlags = true;
            return Page();
        }
    }

    public async Task<IActionResult> OnPostSellCoin(WalletCoinModel walletModels)
    {
        var user = await _userManager.GetUserAsync(User);
        var basketUser = await _basketService.GetBasket(user.UserName);

        if (basketUser == null)
        {
            basketUser = new CoinCart(user.UserName);
        }

        basketUser.Status = "Sell";
        basketUser.CoinCarts = new CoinCartList
        {
            CoinName = walletModels.NameCoin,
            Amount = walletModels.Amount,
            PriceCoin = walletModels.PriceUSD,
            Symbol = walletModels.Symbol
        };

        var basketPost = await _basketService.PostBasket(basketUser);
        return RedirectToPage("/basket/cartinfo");
    }
}