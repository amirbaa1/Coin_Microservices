using Basket.API.Model;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        Task<CoinCart> GetBasket(string userName);
        Task<CoinCart> UpdateBasket(CoinCart coin);
        Task DeleteBasket(string userName);
    }
}
