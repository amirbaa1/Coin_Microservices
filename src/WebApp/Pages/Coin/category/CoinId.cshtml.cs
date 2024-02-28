using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages.Coin.category;

public class CoinId : PageModel
{
    private readonly ICoinService _coinService;
    private readonly ILogger<CoinId> _logger;

    public CoinId(ICoinService coinService, ILogger<CoinId> logger)
    {
        _coinService = coinService;
        _logger = logger;
    }

    [BindProperty] public CoinCategoryResponseListCoin cateListById { get; set; }
    [BindProperty] public CoinDetResponse coinDetResponse { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                cateListById = await _coinService.GetListCoinCateById(id);
                // _logger.LogInformation($"status:{cateListById?.Status?.TimesTamp}");
                // _logger.LogInformation($"coin : {cateListById?.coinCate?.coin?.First()?.Name}");
                if (cateListById == null)
                {
                    TempData["error"] = "Error fetching coin category data.";
                }
            }

            return Page();
        }
        catch (Exception ex)
        {
            TempData["error"] = "Something went wrong while fetching coin data.";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostSymbol(string coinSymbol)
    {
        coinDetResponse = await _coinService.GetCoinBySymbolDet(coinSymbol);
        if (coinDetResponse == null)
        {
            return NotFound();
        }

        return Page();
    }
}