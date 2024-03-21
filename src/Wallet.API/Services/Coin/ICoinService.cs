using Wallet.API.Model.Response;

namespace Wallet.API.Services.Coin
{
    public interface ICoinService
    {
        Task<CoinSearchResponse> GetCoinBySymbol(string symbol);
    }
}
