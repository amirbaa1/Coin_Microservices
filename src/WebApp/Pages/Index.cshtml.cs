using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICoinService _coinService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;

        public IndexModel(ILogger<IndexModel> logger, ICoinService coinService, UserManager<AppUser> userManager,
            IBasketService basketService)
        {
            _logger = logger;
            _coinService = coinService;
            _userManager = userManager;
            _basketService = basketService;
        }

        public bool ErrorOccurred { get; private set; }


        public CoinMarketResponse coinList { get; set; }
        [BindProperty(SupportsGet = true)] public string Symbol { get; set; }
        public CoinSearchResponse coinSearchResponse { get; set; }
        [BindProperty] public CoinDetResponse CoinDetResponse { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData.ContainsKey("sendToIndex"))
            {
                var message = TempData["sendToIndex"];
                ViewData["Message"] = message;
            }

            if (TempData.ContainsKey("deleteCart"))
            {
                var message = TempData["deleteCart"];
                ViewData["Message"] = message;
            }

            try
            {
                coinList = await _coinService.GetCoinMarket();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorOccurred = true;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostCoinSearchAsync(string symbol)
        {
            try
            {
                if (!string.IsNullOrEmpty(symbol))
                {
                    coinSearchResponse = await _coinService.GetCoinBySymbol(symbol);
                }

                return Page();
            }

            catch (Exception ex)
            {
                ErrorOccurred = true;
                // throw new Exception("Something went wrong when calling api. " + ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostCoinName(string coinSymbol)
        {
            if (!string.IsNullOrEmpty(coinSymbol))
            {
                CoinDetResponse = await _coinService.GetCoinBySymbolDet(coinSymbol);
            }

            return Page();
        }


        public async Task<IActionResult> OnPostBasketCoin(string coinSymbol)
        {
            var coin = await _coinService.GetCoinBySymbol(coinSymbol);

            var user = await _userManager.GetUserAsync(User);
            var basketUser = await _basketService.GetBasket(user.UserName);
            if (basketUser.UserName == null)
            {
                basketUser = new CoinCart(user.UserName);
            }

            //basketUser.CoinCarts.Add(new CoinCartList
            //{
            //    CoinId = coin.Data[coinSymbol][0].Id,
            //    CoinName = coin.Data[coinSymbol][0].Name,
            //    Amount = 0,
            //    PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,

            //}) ;

            basketUser.Status = "Buy";
            basketUser.CoinCarts = new CoinCartList
            {
                CoinId = coin.Data[coinSymbol][0].Id,
                CoinName = coin.Data[coinSymbol][0].Name,
                Symbol = coin.Data[coinSymbol][0].Symbol,
                Amount = 0,
                PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,
             
            };

            var basketPost = await _basketService.PostBasket(basketUser);
            return RedirectToPage("/basket/cartinfo");
        }
    }
}