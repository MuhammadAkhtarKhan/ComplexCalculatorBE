using ComplexCalculator.Application.Models;
using ComplexCalculator.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ComplexCalculator.API.Hubs
{
    // ChatHub.cs
    public class ChatHub : Hub
    {
        public async Task SendMessage(List<Calculator> lstCalculations)
        {
            await Clients.All.SendAsync("ReceiveMessage", lstCalculations);
        }
    }


}
