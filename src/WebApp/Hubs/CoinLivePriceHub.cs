using Microsoft.AspNetCore.SignalR;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Hubs
{
    public class CoinLivePriceHub :Hub
    {
        private readonly ICoinService _coinService;

        public CoinLivePriceHub(ICoinService coinService)
        {
            _coinService = coinService;
        }

        public async Task UpdateCoin()
        {
            try
            {
                CoinMarketResponse coinMarket = await _coinService.GetCoinMarket();
                await Clients.All.SendAsync("ReceiveCoinStatus", coinMarket.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while sending coin status: {ex.Message}");
            }
        }
    }
}
