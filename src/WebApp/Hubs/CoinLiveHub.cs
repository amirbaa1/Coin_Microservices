using Microsoft.AspNetCore.SignalR;
using WebApp.Model.CoinModel;
using WebApp.Model.Response;
using WebApp.Services;

namespace WebApp.Hubs
{
    public class CoinLiveHub : Hub
    {
        private readonly ICoinService _coinService;

        public CoinLiveHub(ICoinService coinService)
        {
            _coinService = coinService;
        }


        public async Task<DateTime> TimeAsync()
        {
            return DateTime.Now;
        }

    }
}
