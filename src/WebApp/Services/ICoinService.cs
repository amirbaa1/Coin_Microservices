using WebApp.Model.Response;

namespace WebApp.Services
{
    public interface ICoinService
    {
        Task<CoinMarketResponse> GetCoinMarket();
        Task<CoinSearchResponse> GetCoinBySymbol(string symbol);
        Task<CoinCategoryResponse> GetCoinCate();
        Task<CoinDetResponse> GetCoinBySymbolDet(string symbol);
    }
}