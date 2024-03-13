using Basket.API.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache _distributedCache;

        public BasketService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
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