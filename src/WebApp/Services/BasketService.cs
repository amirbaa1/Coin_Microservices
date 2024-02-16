using Newtonsoft.Json;
using WebApp.Model.Basket;

namespace WebApp.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CheckOutBasket(CheckOut checkOut)
        {
            var response = await _httpClient.PostAsJsonAsync("/basket/BasketCheckOut", checkOut);
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }
            //var read = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<BasketModel>(read);
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
