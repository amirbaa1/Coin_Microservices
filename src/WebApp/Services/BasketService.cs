using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Newtonsoft.Json;
using WebApp.Model;
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
            _logger.LogInformation($"--- > checkOut in Services : {JsonConvert.SerializeObject(checkOut)}");
            var response = await _httpClient.PostAsJsonAsync("/basket/BasketCheckOut", checkOut);
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
            }

            var read = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CheckOut>(read);
        }

        public async Task<bool> DeleteBasket(string username)
        {
            var response = await _httpClient.DeleteAsync($"/basket/{username}");
            if (response == null)
            {
                throw new Exception("Something went wrong when calling api.");
                return false;
            }

            var read = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject(read);
            return true;
        }

        public async Task<CoinCart> GetBasket(string username)
        {
            // _logger.LogInformation($"--->Getbasketusername: {username}");

            var requestUri = new Uri(_httpClient.BaseAddress, $"/basket/{username}");
            // _logger.LogInformation($"--->requestUri: {requestUri}");

            var response = await _httpClient.GetAsync(requestUri);
            // _logger.LogInformation($"--->response link : {response}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get basket. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            // _logger.LogInformation($"--->readGET : {content}");

            var coinCart = JsonConvert.DeserializeObject<CoinCart>(content);
            // _logger.LogInformation($"---> Send Json Cart : {coinCart}");
            return coinCart;
        }

        public async Task<CoinCart> PostBasket(CoinCart coinCarts)
        {
            {
                _logger.LogInformation($"basketModel : {JsonConvert.SerializeObject(coinCarts)}");
                var requestUri = new Uri(_httpClient.BaseAddress, $"/basket");
                // _logger.LogInformation($"requsetUri : {requestUri}");

                var response = await _httpClient.PostAsJsonAsync(requestUri, coinCarts);
                // _logger.LogInformation($"----> response : {response}");
                if (response == null)
                {
                    throw new Exception("Something went wrong when calling api.");
                }

                var read = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CoinCart>(read);
            }
        }

        public async Task<bool> SendWallet(string username)
        {
            try
            {
                //_logger.LogInformation($"--- > SendWallet in Services : {JsonConvert.SerializeObject(walletModel)}");
                var content = new StringContent(string.Empty);
                var response = await _httpClient.PostAsync($"/basket/wallet/{username}",content);

                _logger.LogInformation($"---> basket send wallet response:{response}");

                if (response.IsSuccessStatusCode)
                {
                    var read = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"---> wallet :{read}");

                    //return JsonConvert.DeserializeObject<WalletModel>(read);
                    return response.IsSuccessStatusCode;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when calling api.");
            }

        }
    }
}