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
            var response = await _httpClient.GetAsync($"/basket/{username}");
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }
            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CoinCart>(read);
        }

        public async Task<CoinCart> PostBasket(CoinCart coinCart)
        {
            _logger.LogInformation($"basketModel : {JsonConvert.SerializeObject(coinCart)}");
            var response = await _httpClient.PostAsJsonAsync("/basket", coinCart);
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }
            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CoinCart>(read);
        }
    }
}
