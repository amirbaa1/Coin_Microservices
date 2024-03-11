using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Model.Basket;

namespace WebApp.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BasketService> _logger;

        public BasketService(HttpClient httpClient, ILogger<BasketService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CheckOut> CheckOutBasket(CheckOut checkOut)
        {
            var response = await _httpClient.PostAsJsonAsync("/basket/BasketCheckOut", checkOut);
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }

            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CheckOut>(read);
        }

        public async Task<CoinCart> GetBasket(string username)
        {
            _logger.LogInformation($"--->Getbasketusername: {username}");

            var requestUri = new Uri(_httpClient.BaseAddress, $"/basket/{username}");
            _logger.LogInformation($"--->requestUri: {requestUri}");

            var response = await _httpClient.GetAsync(requestUri);
            _logger.LogInformation($"--->response link : {response}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get basket. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"--->readGET : {content}");

            var coinCart = System.Text.Json.JsonSerializer.Deserialize<CoinCart>(content);
            return coinCart;
        }

        public async Task<CoinCart> PostBasket([FromBody] CoinCart coinCarts)
        {
            _logger.LogInformation($"--- > basketModel in Services : {JsonConvert.SerializeObject(coinCarts)}");
            var requestUri = new Uri(_httpClient.BaseAddress, $"/basket");
            _logger.LogInformation($"--->requestUri: {requestUri}");

            // var response = await _httpClient.PostAsJsonAsync(requestUri, coinCarts);
           
            _logger.LogInformation($"Request payload: {JsonConvert.SerializeObject(coinCarts)}");
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(coinCarts), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUri, content);
            _logger.LogInformation($"--->response link : {response}");

            // var response = await _httpClient.PostAsJsonAsync("/basket", coinCarts);

            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }

            var read = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"--- > readPost : {JsonConvert.SerializeObject(read)}");
            return System.Text.Json.JsonSerializer.Deserialize<CoinCart>(read);
        }
    }
}