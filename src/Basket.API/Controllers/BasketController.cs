using Basket.API.Model;
using Basket.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<CoinCart>> GetBasket(string userName)
        {
            var basket = await _basketService.GetBasket(userName);
            return Ok(basket ?? new CoinCart(userName));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] CoinCart coinCart)
        {
            return Ok(await _basketService.UpdateBasket(coinCart));
        }
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketService?.DeleteBasket(userName);
        return Ok();
        }
        //[HttpPost]
        //public IActionResult CheckOut(string userName)
        //{

        //}
    }
}
