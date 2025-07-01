using ComplexCalculator.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ComplexCalculator.API.Hubs
{
    public class SyncDataHub : Hub
    {
        public async Task SyncData(bool val)
        {
            await Clients.All.SendAsync("ReceiveData", val);
        }
    }

}
