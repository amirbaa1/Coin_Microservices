using Microsoft.AspNetCore.SignalR;
using WebApp.Model.Response;
using Newtonsoft.Json;
using WebApp.SignalR;

namespace WebApp.Services
{
    public class CoinService : ICoinService
    {
        private readonly HttpClient _httpClient;
        private readonly IHubContext<CoinHub> _hubContext;
        public CoinService(HttpClient httpClient, IHubContext<CoinHub> hubContext)
        {
            _httpClient = httpClient;
            _hubContext = hubContext;
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

        public async Task<CoinCategoryResponse> GetCateCoin()
        {
            var response = await _httpClient.GetAsync("/Coin/Categories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinCategoryResponse>(content)!;
            }
            else
            {
                throw new Exception("Something went wrong when calling api.!");
            }
        }

        public async Task<CoinCategoryResponseListCoin> GetListCoinCateById(string id)
        {
            var response = await _httpClient.GetAsync($"/Coin/Categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinCategoryResponseListCoin>(content)!;
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
                var result = JsonConvert.DeserializeObject<CoinMarketResponse>(content);
                // await _hubContext.Clients.All.SendAsync("UpdateCoin", result);
                return result;
            }
            else
            {
                throw new Exception("Something went wrong when calling api.!");
            }
        }
    }
}