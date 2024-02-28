using Coin.API.Model;
using Coin.API.Model.CoinModel;
using Coin.API.Model.Response;
using Coin.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coin.API.Controllers;

[Route("/[controller]")]
[ApiController]
public class CoinController : Controller
{
    private readonly ICoinMarket _coinMarket;

    public CoinController(ICoinMarket coinMarket)
    {
        _coinMarket = coinMarket;
    }

    [HttpGet]
    public async Task<IActionResult> GetCoin()
    {
        CoinMarketResponse coin = await _coinMarket.CoinMarketGenerator();
        if (coin == null)
        {
            return NotFound();
        }

        return Ok(coin);
    }

    [HttpGet("AllCoin")]
    public async Task<IActionResult> GetAll()
    {
        string coininfo = await _coinMarket.GetAllCoin();

        if (coininfo == null)
        {
            return NotFound();
        }

        return Ok(coininfo);
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetBySymbol(string symbol)
    {
        CoinSearchResponse coinSymbol = await _coinMarket.GetBySymbol(symbol);
        if (coinSymbol == null)
        {
            return NotFound();
        }

        return Ok(coinSymbol);
    }

    // [HttpGet("{name}")]
    // public async Task<IActionResult> GetByName(string name)
    // {
    //     CoinSearchResponse coinName = await _coinMarket.GetByName(name);
    //     if (coinName == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(coinName);
    // }

    [HttpGet("Categories")]
    public async Task<IActionResult> GetAllCategory()
    {
        CoinCategoryResponse cate = await _coinMarket.GetAllCategory();
        if (cate == null)
        {
            return NotFound();
        }

        return Ok(cate);
    }

    [HttpGet("Categories/{id}")]
    public async Task<IActionResult> GetCateById(string id)
    {
        CoinCategoryResponseListCoin coinList = await _coinMarket.GetByIdCategory(id);
        if (coinList == null)
        {
            return NotFound();
        }

        return Ok(coinList);
    }
}