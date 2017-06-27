using Microsoft.AspNet.SignalR;

namespace ERPAPD.Hubs
{
    public partial class CallHub : Hub
    {
        public static void IncomingCall(string Phone, string UserName)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CallHub>();
            hubContext.Clients.User(UserName).IncomingCallFromCustomer(Phone);
        }
    }
}