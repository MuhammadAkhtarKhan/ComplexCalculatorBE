using Microsoft.AspNetCore.SignalR;

namespace ComplexCalculator.API.Hubs
{
    public class DeleteLogOutUserDataHub : Hub
    {
        public async Task SendUserId(string UserId)
        {
            await Clients.All.SendAsync("ReceiveUserId", UserId);
        }
    }

}
