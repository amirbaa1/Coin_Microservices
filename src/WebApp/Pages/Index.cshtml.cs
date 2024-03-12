using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Model.Response;
using WebApp.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        //public async Task<IActionResult> OnPostBasketCoin(string coinSymbol)
        //{
        //    // Console.WriteLine($"1 --->OnPostCart called with coinSymbol: {coinSymbol}");

        //    var coin = await _coinService.GetCoinBySymbol(coinSymbol);
        //    _logger.LogInformation($"2 --->coin response: {JsonConvert.SerializeObject(coin)}");

        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return RedirectToPage("/account/login");
        //    }

        //    // var basketUser = await _basketService.GetBasket(user.UserName);
        //    // _logger.LogInformation($"3 --->UserName : {basketUser.UserName}");


        //    //var basketUser = new CoinCart(user.UserName);
        //    //basketUser.CoinCarts.Add(new CoinCartList
        //    //{
        //    //    CoinName = coin.Data[coinSymbol][0].Name,
        //    //    CoinId = coin.Data[coinSymbol][0].Id,
        //    //    PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,
        //    //    Amount = 0,
        //    //});

        //    var basketUser = await _basketService.GetBasket(user.UserName);
        //    _logger.LogInformation($"--->UserName : {basketUser.UserName}");
        //    //basketUser.CoinCarts.Add(new CoinCartList
        //    //{
        //    //    CoinId = coin.Data[coinSymbol][0].Id,
        //    //    CoinName = coin.Data[coinSymbol][0].Name,
        //    //    Amount = 2,
        //    //    PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,
        //    //});

        //    basketUser.CoinCarts = new CoinCartList
        //    {
        //        CoinId = coin.Data[coinSymbol][0].Id,
        //        CoinName = coin.Data[coinSymbol][0].Name,
        //        Amount = 0,
        //        PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,
        //    };

        //    _logger.LogInformation($"4 --->basketUser : {JsonConvert.SerializeObject(basketUser)}");
        //    _logger.LogInformation($"4 --->basketUser2 : {basketUser}");
        //    try
        //    {
        //        var jsonContent = System.Text.Json.JsonSerializer.Serialize(basketUser);

        //        var sendPost = await _basketService.PostBasket(basketUser);
        //        _logger.LogInformation($"5 --->sendPost in Index : {JsonConvert.SerializeObject(sendPost)}");
        //        return RedirectToPage("/Basket/CartInfo");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error posting basket: {ex.Message}");
        //        // Log or handle the exception accordingly
        //        return Page();
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnPostBasketCoin(string coinSymbol)
        {
            var coin = await _coinService.GetCoinBySymbol(coinSymbol);

            var user = await _userManager.GetUserAsync(User);
            var basketUser = await _basketService.GetBasket(user.UserName);
            if(basketUser.UserName == null)
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

            basketUser.CoinCarts = new CoinCartList
            {
                CoinId = coin.Data[coinSymbol][0].Id,
                CoinName = coin.Data[coinSymbol][0].Name,
                Amount = 0,
                PriceCoin = coin.Data[coinSymbol][0].Quote["USD"].Price,
            };

            var basketPost = await _basketService.PostBasket(basketUser);
            return RedirectToPage("/basket/cartinfo");

        }
    }
}