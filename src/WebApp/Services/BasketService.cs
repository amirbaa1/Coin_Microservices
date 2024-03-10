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

        public async Task<BasketModel> CheckOutBasket(CheckOut checkOut)
        {
            var response = await _httpClient.PostAsJsonAsync("/basket/BasketCheckOut", checkOut);
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }
            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BasketModel>(read);
        }

        public async Task<BasketModel> GetBasket(string username)
        {
            var response = await _httpClient.GetAsync($"/basket/{username}");
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }
            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BasketModel>(read);
        }

        public async Task<BasketModel> PostBasket(BasketModel basketModel)
        {
            _logger.LogInformation($"basketModel : {JsonConvert.SerializeObject(basketModel)}");
            var response = await _httpClient.PostAsJsonAsync("/basket", basketModel);
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }
            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BasketModel>(read);
        }
    }
}
