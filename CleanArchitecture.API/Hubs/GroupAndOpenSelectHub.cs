using ComplexCalculator.Application.Models;
using ComplexCalculator.Application.Models.Admin;
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
    public class GroupNoAndUserInputHub : Hub
    {
        public async Task SendGroupNoAndUserInput(int GroupNo,int InputUserName, List<TempCalculatorResponseModel> adminCalculations)
        {
            await Clients.All.SendAsync("ReceiveGroupNoAndUserInput", GroupNo, InputUserName,adminCalculations);
        }
    }

}
