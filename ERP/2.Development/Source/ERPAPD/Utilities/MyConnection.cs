using Microsoft.AspNet.SignalR;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace ERPAPD.Utilities
{
    public class MyConnection : PersistentConnection
    {
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            //string ip = HttpContext.Current.Request.UserHostAddress;
            //int save = User.SaveUserBroadcast(System.Web.HttpContext.Current.User.Identity.Name, data.ToString(), GetVisitorIpAddress(), GetLanIPAddress());
            // Broadcast data to all clients
            return Connection.Broadcast(data);
        }

        public string GetVisitorIpAddress()
        {
            string stringIpAddress;
            stringIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (stringIpAddress == null) //may be the HTTP_X_FORWARDED_FOR is null
                stringIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; //we can use REMOTE_ADDR
            else if (stringIpAddress == null)
                stringIpAddress = GetLanIPAddress();

            return stringIpAddress;
        }

        //Get Lan Connected IP address method
        public string GetLanIPAddress()
        {
            //Get the Host Name
            string stringHostName = Dns.GetHostName();
            //Get The Ip Host Entry
            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
            //Get The Ip Address From The Ip Host Entry Address List
            System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;
            return arrIpAddress[arrIpAddress.Length - 1].ToString();
        }
    }
}