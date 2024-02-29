using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICoinService _coinService;

        public IndexModel(ILogger<IndexModel> logger, ICoinService coinService)
        {
            _logger = logger;
            _coinService = coinService;
        }

        public CoinMarketResponse coinList { get; set; }
        [BindProperty(SupportsGet = true)] public string Symbol { get; set; }
        public CoinSearchResponse coinSearchResponse { get; set; }
        [BindProperty] public CoinDetResponse CoinDetResponse { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            coinList = await _coinService.GetCoinMarket();
            return Page();
        }

        public async Task<IActionResult> OnPostCoinSearchAsync(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                coinSearchResponse = await _coinService.GetCoinBySymbol(symbol);
            }

            return Page();
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