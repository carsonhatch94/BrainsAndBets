using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class GameHub : Hub
{
    private static ConcurrentDictionary<string, string> _connectedUsers = new ConcurrentDictionary<string, string>();

    public async Task JoinGame(string username)
    {
        _connectedUsers[Context.ConnectionId] = username;

        // Notify all clients about the new user
        await Clients.All.SendAsync("UpdateConnectedUsers", _connectedUsers.Values);

        // Notify the new user about the current list of connected users
        await Clients.Caller.SendAsync("UpdateConnectedUsers", _connectedUsers.Values);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _connectedUsers.TryRemove(Context.ConnectionId, out _);

        // Notify all clients about the updated list of users
        await Clients.All.SendAsync("UpdateConnectedUsers", _connectedUsers.Values);

        await base.OnDisconnectedAsync(exception);
    }
}
