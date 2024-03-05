using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages.Coin
{
    public class coinLiveModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICoinService _coinService;
        private readonly IHubContext<CoinLiveHub> _hubContext;
        public bool ErrorOccurred { get; private set; }

        public coinLiveModel(ILogger<IndexModel> logger, ICoinService coinService, IHubContext<CoinLiveHub> hubContext)
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
                //await _hubContext.Clients.All.SendAsync("ReceiveCoinStatus", coinList.CoinStatus.TimesTamp);
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
