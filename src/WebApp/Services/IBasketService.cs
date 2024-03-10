using WebApp.Model.Basket;

namespace WebApp.Services
{
    public interface IBasketService
    {
        Task<CoinCart> GetBasket(string username);
        Task<CoinCart> PostBasket(CoinCart coinCart);
        Task<CheckOut> CheckOutBasket(CheckOut checkOut);
    }
}
