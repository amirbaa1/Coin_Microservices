using AutoMapper;
using Basket.API.Model;
using Basket.API.Services;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    // [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketService basketService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketService = basketService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        [HttpPost("BasketCheckOut")]
        public async Task<ActionResult> CheckOut([FromBody] CheckOut checkOut)
        {
            var basket = await _basketService.GetBasket(checkOut.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckOutEvent>(checkOut);

            eventMessage.CoinId = basket.CoinCarts.CoinId;
            eventMessage.CoinName = basket.CoinCarts.CoinName;
            eventMessage.Amount = basket.CoinCarts.Amount;
            eventMessage.PriceCoin = basket.CoinCarts.PriceCoin;
            eventMessage.TotalPrice = basket.TotalPrice;
            eventMessage.DateTime = DateTime.Now;

            await _publishEndpoint.Publish<BasketCheckOutEvent>(eventMessage);

            await _basketService.DeleteBasket(basket.UserName);
            return Accepted();
        }
    }
}