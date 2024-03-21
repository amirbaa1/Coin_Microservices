using Newtonsoft.Json;
using Wallet.API.Model.Response;

namespace Wallet.API.Services.Coin
{
    public class CoinService : ICoinService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CoinService> _logger;

        public CoinService(HttpClient httpClient, ILogger<CoinService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CoinSearchResponse> GetCoinBySymbol(string symbol)
        {
            var response = await _httpClient.GetAsync($"/coin/{symbol}");
            _logger.LogInformation($"Wallet get coin : {response}");
            //try
            //{
            //    //var response = await _httpClient.GetAsync($"/coin/{symbol}");
            //    //_logger.LogInformation($"Wallet get coin : {response}");
            //    var content = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<CoinSearchResponse>(content);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Something went wrong when calling api.!");
            //}
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CoinSearchResponse>(content);
        }
    }
}
