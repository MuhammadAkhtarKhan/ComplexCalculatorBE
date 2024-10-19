using Microsoft.AspNetCore.SignalR;

namespace ComplexCalculator.API.Hubs
{
    public class UserCountHub : Hub
    {
        private static int _userCount = 0;

        public override Task OnConnectedAsync()
        {
            _userCount++;
            Clients.All.SendAsync("UpdateUserCount", _userCount);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _userCount--;
            Clients.All.SendAsync("UpdateUserCount", _userCount);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
