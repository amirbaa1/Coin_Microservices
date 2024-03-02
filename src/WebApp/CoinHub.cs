using Microsoft.AspNetCore.SignalR;
using WebApp.Services;

namespace WebApp;

public sealed class CoinHub : Hub
{

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}");
    }
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}:{message}");
    }
}//{"arguments":["Test message"],"invocationId":"0","target":"SendMessage","type":1}