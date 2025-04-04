using ComplexCalculator.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ComplexCalculator.API.Hubs
{
    public class SyncDataHub : Hub
    {
        public async Task SyncData(List<Calculator> lstCalculations)
        {
            await Clients.All.SendAsync("ReceiveData", lstCalculations);
        }
    }

}
