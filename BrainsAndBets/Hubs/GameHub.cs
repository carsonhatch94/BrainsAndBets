using Microsoft.AspNetCore.SignalR;

namespace BrainsAndBets.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinGame(string user)
        {
            await Clients.All.SendAsync("JoinedGame", user);
        }
    }
}
