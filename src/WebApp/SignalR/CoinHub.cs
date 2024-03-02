using Microsoft.AspNetCore.SignalR;
using WebApp.Services;

namespace WebApp.SignalR;

public class CoinHub : Hub
{
    private readonly ICoinService _coinService;

    public CoinHub(ICoinService coinService)
    {
        _coinService = coinService;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has Join");
        
    }
}