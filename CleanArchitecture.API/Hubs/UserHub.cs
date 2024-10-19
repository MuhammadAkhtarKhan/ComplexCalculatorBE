using ComplexCalculator.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ComplexCalculator.API.Hubs
{
    public class UserHub : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            ConnectedUsers.TryAdd(Context.ConnectionId, Context.User.Identity.Name);
            Clients.All.SendAsync("UpdateUserCount", ConnectedUsers.Count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUsers.TryRemove(Context.ConnectionId, out _);
            Clients.All.SendAsync("UpdateUserCount", ConnectedUsers.Count);
            return base.OnDisconnectedAsync(exception);
        }

        public int GetConnectedUsersCount()
        {
            return ConnectedUsers.Count;
        }
    }
}
