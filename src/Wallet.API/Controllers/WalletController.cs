using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Wallet.API.Handlers.CheckWallet;
using Wallet.API.Services;

namespace Wallet.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IMediator _mediatR;

        public WalletController(IWalletService walletService, IMediator mediatR)
        {
            _walletService = walletService;
            _mediatR = mediatR;
        }

        //[HttpPost("{userName}")]
        //public async Task<IActionResult> GetWalletByUserName(string userName)
        //{
        //    //    var result = await _walletService.GetUserNameWallet(userName);
        //    //    if (result == null)
        //    //    {
        //    //        return BadRequest(result);
        //    //    }
        //    //    return Ok(result);
        //    //}
        //    var qeury = new (userName);
        //    var order = await _mediatR.Send(qeury);

        //    return Ok(order);
        //}
        [HttpPost]
        public async Task<ActionResult<ObjectId>> WalletPost([FromBody] CheckWalletCommand command)
        {
             await _mediatR.Send(command);
             return Ok();
        }
    }
}
