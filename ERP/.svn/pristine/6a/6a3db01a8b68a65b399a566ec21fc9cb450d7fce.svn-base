using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ERPAPD.Models
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(message);
        }
       
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name.ToUpper();

            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }
        public static void SendGroup(string group, string message, string Type)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            var obj = new {type = Type,message = message };
            context.Clients.Group(group.ToUpper(), "").broadcastMessage(obj);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            //logic code removed for brevity
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            //logic code removed for brevity
            return base.OnReconnected();
        }
        public static void SendMessageToAll(string group, string message)
        {
            // Broad cast message
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.messageReceived(group, message);
        }
    }
}