using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Model.Response;
using WebApp.Pages.Coin.category;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICoinService _coinService;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public bool ErrorOccurred { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ICoinService coinService, IBasketService basketService,
            UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _coinService = coinService;
            _basketService = basketService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public CoinMarketResponse coinList { get; set; }
        [BindProperty(SupportsGet = true)] public string Symbol { get; set; }
        public CoinSearchResponse coinSearchResponse { get; set; }
        [BindProperty] public CoinDetResponse CoinDetResponse { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
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

        public async Task<IActionResult> OnPostCart(string coinSymbol)
        {
            Console.WriteLine($"1 --->OnPostCart called with coinSymbol: {coinSymbol}");

            var coin = await _coinService.GetCoinBySymbol(coinSymbol);
            _logger.LogInformation($"2 --->coin response: {JsonConvert.SerializeObject(coin)}");

            var user = await _userManager.GetUserAsync(User);
            if (user == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/account/login");
            }

            var basketUser = await _basketService.GetBasket(user.UserName);
            _logger.LogInformation($"3 --->UserName : {basketUser.UserName}");

            basketUser.CoinCarts = new CoinCartList
            {
                CoinName = coin.Data[coinSymbol][0].Name,
                CoinId = coin.Data[coinSymbol][0].Id,
                PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,
                Amount = 0,
            };

            _logger.LogInformation($"4 --->basketUser : {JsonConvert.SerializeObject(basketUser)}");
            
            var basketUserJson = JsonConvert.SerializeObject(basketUser);
            HttpContext.Session.SetString("CartInfo", basketUserJson);


            //var basketAdd = await _basketService.PostBasket(basketUser);
            //_logger.LogInformation($"5 --->basketAdd : {JsonConvert.SerializeObject(basketAdd)}");

            return RedirectToPage("/cart/cartinfo");
        }
    }
}