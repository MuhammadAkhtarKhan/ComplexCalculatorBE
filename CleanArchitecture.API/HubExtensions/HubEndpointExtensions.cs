using ComplexCalculator.API.Hubs;

namespace ComplexCalculator.API.HubExtensions
{
    public static class HubEndpointExtensions
    {
        public static void MapHubEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<UserHub>("/userhub");
            endpoints.MapHub<ChatHub>("/chatHub");
            endpoints.MapHub<SyncDataHub>("/syncdataHub");
            endpoints.MapHub<DeleteLogOutUserDataHub>("/delete-logout-userdataHub");
            endpoints.MapHub<GroupAndOpenSelectHub>("/groupAndOpenHub");
        }
    }
}
