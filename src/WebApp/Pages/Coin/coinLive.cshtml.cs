using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WebApp.Hubs;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages.Coin
{
    public class coinLiveModel : PageModel
    {   
        private readonly ILogger<IndexModel> _logger;
        private readonly ICoinService _coinService;
        private readonly IHubContext<CoinLivePriceHub> _hubContext;
        public bool ErrorOccurred { get; private set; }

        public coinLiveModel(ILogger<IndexModel> logger, ICoinService coinService, IHubContext<CoinLivePriceHub> hubContext)
        {
            _logger = logger;
            _coinService = coinService;
            _hubContext = hubContext;
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

                var updatedPrice = coinList.Data.Select(itemCoin =>
                    new { Symbol = itemCoin.Symbol, Price = itemCoin.Quote?["USD"]?.Price });

                // _logger.LogInformation($" updatedPrice :{JsonConvert.SerializeObject(updatedPrice)}");
                await _hubContext.Clients.All.SendAsync("UpdateCoinPrice", updatedPrice);

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
    }
}