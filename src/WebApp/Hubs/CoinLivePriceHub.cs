using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Hubs
{
    public class CoinLivePriceHub : Hub
    {
        private readonly ILogger<CoinLivePriceHub> _logger;
        private readonly ICoinService _coinService;

        public CoinLivePriceHub(ILogger<CoinLivePriceHub> logger, ICoinService coinService)
        {
            _logger = logger;
            _coinService = coinService;
        }

        public CoinMarketResponse coinList { get; set; }
        [BindProperty(SupportsGet = true)] public string Symbol { get; set; }

        public async Task UpdateCoinPrice(Object updatedPrices)
        {
            // try
            // {
            //     CoinMarketResponse coinMarket = await _coinService.GetCoinMarket();
            //     await Clients.All.SendAsync("ReceiveCoinStatus", coinMarket.Data);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Error occurred while sending coin status: {ex.Message}");
            // }
            coinList = await _coinService.GetCoinMarket();

            var updatedPrice = coinList.Data.Select(itemCoin =>
                new { Symbol = itemCoin.Symbol, Price = itemCoin.Quote?["USD"]?.Price });

            _logger.LogInformation($" updatedPrice :{JsonConvert.SerializeObject(updatedPrice)}");
            await Clients.All.SendAsync("UpdateCoinPrice", updatedPrice);

            // await Clients.All.SendAsync("ReceiveCoinStatus", updatedPrices);
            // _logger.LogInformation($"UP --- > {JsonConvert.SerializeObject(updatedPrices)}");
        }
    }
}