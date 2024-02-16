using WebApp.Model.Basket;

namespace WebApp.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string username);
        Task<BasketModel> PostBasket(BasketModel basketModel);
        Task CheckOutBasket(CheckOut checkOut);

    }
}
