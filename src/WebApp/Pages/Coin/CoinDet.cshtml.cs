using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages.Coin;

public class CoinDet : PageModel
{
    public readonly ICoinService _CoinService;
    private readonly ILogger<CoinDet> _logger;
    public CoinDet(ICoinService coinService, ILogger<CoinDet> logger)
    {
        _CoinService = coinService;
        _logger = logger;
    }

    [BindProperty]
    public CoinDetResponse coinDetResponse { get; set; }

    public async Task<IActionResult> OnGet(string coinSymbol)
    {
        coinDetResponse = await _CoinService.GetCoinBySymbolDet(coinSymbol);
        if (coinDetResponse == null)
        {
            return NotFound();
        }
        _logger.LogInformation($"===> name :{coinDetResponse.CoinSearches.First().Value.First().Name}");
        _logger.LogInformation($"===> times :{coinDetResponse.CoinStatus.TimesTamp}");

        return Page();
    }
}