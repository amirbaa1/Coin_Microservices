using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;

namespace Ordering.API.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    // [Authorize]
    [AllowAnonymous]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public OrderController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrderByUserName(string userName)
        {
            var qeury = new GetOrderListQuery(userName);
            var order = await _mediatR.Send(qeury);

            return Ok(order);
        }
        [HttpPost("OrderCheckout")]
        public async Task<ActionResult<int>> OrderCheckout([FromBody] CheckoutOrderCommand checkoutOrderCommand)
        {
            var result = await _mediatR.Send(checkoutOrderCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdadeOrderCommand updadeOrderCommand)
        {
            await _mediatR.Send(updadeOrderCommand);
            return Ok(updadeOrderCommand);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand()
            {
                Id = id
            };
            await _mediatR.Send(command);
            return Ok($"Delete ID : {command.Id}");
        }
    }
}
