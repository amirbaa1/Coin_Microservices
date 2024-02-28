using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Pages.Coin;

public class Category : PageModel
{
    private readonly ICoinService _coinService;

    public Category(ICoinService coinService)
    {
        _coinService = coinService;
    }

    [BindProperty] public CoinCategoryResponse cate { get; set; }
    [BindProperty] public CoinCategoryResponseListCoin cateListById { get; set; }

    public async Task<IActionResult> OnGet()
    {
        cate = await _coinService.GetCateCoin();
        return Page();
    }

    public async Task<IActionResult> OnPost(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            cateListById = await _coinService.GetListCoinCateById(id);
        }

        return Page();

    }
}