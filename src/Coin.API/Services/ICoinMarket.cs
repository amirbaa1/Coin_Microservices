using Coin.API.Model;
using Coin.API.Model.CoinModel;
using Coin.API.Model.Response;

namespace Coin.API.Services;

public interface ICoinMarket
{
    Task<CoinMarketResponse> CoinMarketGenerator();
    Task<string> GetAllCoin();
    Task<CoinSearchResponse> GetBySymbol(string symbol);
    Task<CoinSearchResponse> GetByName(string name);
    Task<CoinCategoryResponse> GetAllCategory();
    Task<CoinCategoryResponseListCoin> GetByIdCategory(string id);
}