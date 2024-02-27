using WebApp.Model.Response;
using Newtonsoft.Json;

namespace WebApp.Services
{
    public class CoinServeice : ICoinService
    {
        private readonly HttpClient _httpClient;

        public CoinServeice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CoinSearchResponse> GetCoinBySymbol(string symbol)
        {
            var response = await _httpClient.GetAsync($"/coin/{symbol}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinSearchResponse>(content);
            }
            else
            {
                throw new Exception("Something went wrong when calling api.!");
            }
        }

        public async Task<CoinCategoryResponse> GetCoinCate()
        {
            var response = await _httpClient.GetAsync($"/coin/categories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinCategoryResponse>(content);
            }
            else
            {
                throw new Exception("Something went wrong when calling api.!");
            }
        }

        public async Task<CoinDetResponse> GetCoinBySymbolDet(string symbol)
        {
            var response = await _httpClient.GetAsync($"/coin/{symbol}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinDetResponse>(content)!;
            }
            else
            {
                throw new Exception("Something went wrong when calling api.!");
            }
        }

        public async Task<CoinMarketResponse> GetCoinMarket()
        {
            var response = await _httpClient.GetAsync("/coin/all");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinMarketResponse>(content);
            }
            else
            {
                throw new Exception("Something went wrong when calling api.!");
            }
        }
    }
}