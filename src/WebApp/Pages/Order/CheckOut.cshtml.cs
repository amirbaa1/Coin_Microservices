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
            var UserBasket = await _basketService.GetBasket(userGet.UserName);

            check.EmailAddress = _userManager.FindByNameAsync(User.Identity.Name).Result.Email;
            check.UserName = UserBasket.UserName;

            check.FirstName = "Tester";
            check.LastName = "TesterB";


            check.CoinId = UserBasket.CoinCarts.CoinId;
            check.CoinName = UserBasket.CoinCarts.CoinName;
            check.PriceCoin = UserBasket.CoinCarts.PriceCoin;
            check.Amount = UserBasket.CoinCarts.Amount;
            check.TotalPrice = UserBasket.TotalPrice;
            check.Status = UserBasket.Status;
            check.DateTime = DateTime.Now;


            // _logger.LogInformation($"CheckOut send:{check}");

            wallet.UserName = UserBasket.UserName;
            var walletModel = new WalletModel
            {
                UserName = UserBasket.UserName,
                walletCoins = new List<WalletCoinModel>
            {
                new WalletCoinModel
                {
                    NameCoin = check.CoinName,
                    Symbol = UserBasket.CoinCarts.Symbol,
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