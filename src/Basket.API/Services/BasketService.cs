﻿using Basket.API.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BasketService> _logger;
        public BasketService(IDistributedCache distributedCache, HttpClient httpClient, ILogger<BasketService> logger )
        {
            _distributedCache = distributedCache;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task DeleteBasket(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
        }

        public async Task<CoinCart> GetBasket(string userName)
        {
            var basket = await _distributedCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CoinCart>(basket);
        }

        public async Task<bool> SendBasketWallet(Object walletModel)
        {
            try
            {
                var UrlWallet = new Uri("https://localhost:7004/api/Wallet");

                _logger.LogInformation($"--- > {JsonConvert.SerializeObject(walletModel)}");
                var response = await _httpClient.PostAsJsonAsync(UrlWallet, walletModel);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<CoinCart> UpdateBasket(CoinCart coin)
        {
            await _distributedCache.SetStringAsync(
                coin.UserName,
                JsonConvert.SerializeObject(coin)
            );
            return await GetBasket(coin.UserName);
        }
    }
}