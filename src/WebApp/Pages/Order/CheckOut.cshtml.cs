using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.ContentModel;
using WebApp.Model;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Services;

namespace WebApp.Pages.Order
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<CheckOutModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWalletService _walletservice;
        public CheckOutModel(IBasketService basketService, ILogger<CheckOutModel> logger,
            UserManager<AppUser> userManager, IWalletService walletservice)
        {
            _basketService = basketService;
            _logger = logger;
            _userManager = userManager;
            _walletservice = walletservice;
        }

        [BindProperty] public CheckOut check { get; set; }
        [BindProperty] public CoinCart CoinCartItem { get; set; }
        [BindProperty] public WalletModel wallet { get; set; }  

        public async Task OnGet()
        {
            var userGet = await _userManager.GetUserAsync(User);
            var getCoinUser = await _basketService.GetBasket(userGet.UserName);

            //_logger.LogInformation($"get Coin for page :{getCoinUser}");

            CoinCartItem = getCoinUser;
        }

        public async Task<IActionResult> OnPost()
        {
            var userGet = await _userManager.GetUserAsync(User);
            var coinUser = await _basketService.GetBasket(userGet.UserName);

            check.EmailAddress = _userManager.FindByNameAsync(User.Identity.Name).Result.Email;
            check.UserName = coinUser.UserName;

            check.FirstName = "Tester";
            check.LastName = "TesterB";


            check.CoinId = coinUser.CoinCarts.CoinId;
            check.CoinName = coinUser.CoinCarts.CoinName;
            check.PriceCoin = coinUser.CoinCarts.PriceCoin;
            check.Amount = coinUser.CoinCarts.Amount;
            check.TotalPrice = coinUser.TotalPrice;
            check.DateTime = DateTime.Now;


            // _logger.LogInformation($"CheckOut send:{check}");

            wallet.UserName = coinUser.UserName;
            var walletModel = new WalletModel
            {
                UserName = coinUser.UserName,
                walletCoins = new List<WalletCoinModel>
            {
                new WalletCoinModel
                {
                    NameCoin = check.CoinName,
                    Symbol = coinUser.CoinCarts.Symbol,
                    Amount = check.Amount,
                    PriceUSD = check.TotalPrice,
                    coinPrice = check.PriceCoin
                }
            }
            };

             _logger.LogInformation($"CheckOut send:{walletModel}");

            await _basketService.SendWallet(wallet.UserName);
            await _basketService.CheckOutBasket(check);

            TempData["sendToIndex"] = $"buy coin ";

            return RedirectToPage("../Index");
        }

        public async Task<IActionResult> OnPostDeleteCart()
        {
            var user = await _userManager.GetUserAsync(User);
            await _basketService.DeleteBasket(user.UserName);
            return RedirectToPage("../Index");
        }
    }
}