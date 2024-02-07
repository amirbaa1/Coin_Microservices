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
        public async Task<IActionResult> OnGetAsync()
        {
            coinList = await _coinService.GetCoinMarket();
            return Page();
        }
    }
}
