using Microsoft.AspNetCore.SignalR;

namespace ComplexCalculator.API.Hubs
{
    public class GroupAndOpenSelectHub : Hub
    {
        public async Task SendGroupNoAndOpen(int GroupNo,int OpenVal)
        {
            await Clients.All.SendAsync("ReceiveGroupNoAndOpen", GroupNo,OpenVal);
        }
    }

}
