using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Wallet.API.Model;
using Wallet.API.Services;

namespace Wallet.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet([FromBody] WalletModel walletModels)
        {
            var user = await _walletService.GetUserNameWallet(walletModels.UserName);
            if (user.Count() == 0)
            {
                await _walletService.AddWallet(walletModels);
                return Ok();
            }
            else
            {
                await _walletService.UpdateWallet(walletModels);
                return Ok();
            }
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetByUserNameWallet(string userName)
        {
            var userw = await _walletService.GetUserNameWallet(userName);
            if (userw == null)
            {
                return NotFound();
            }

            return Ok(userw);
        }
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateWalletCoin(string username)
        {
            var userw = await _walletService.GetUserNameWallet(username);
            if (userw == null)
            {
                return BadRequest("You have not wallet!");
            }
            var updateCoin = await _walletService.UpdateCoinWallet(username);
            if (updateCoin == null)
            {
                return BadRequest(updateCoin.ToString());
            }
            return Ok(updateCoin);
        }
    }
}