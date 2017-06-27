using ERPAPD.Models;
using System.Linq;
using System.Web.Http;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers.WebApi
{
    [RoutePrefix("api/incomingcall")]
    public class IncomingCallController : ApiController
    {
        [Route("{Extension}/{CallerID}")]
        public dynamic Get(string Extension, string CallerID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var currentEmployee = dbConn.FirstOrDefault<EmployeeInfo>("XLiteID={0}", Extension);
                if (currentEmployee == null)
                {
                    return Json(new { Message = "Extension not found", message = "No extension was found that matches the request." });
                }
                Hubs.CallHub.IncomingCall(CallerID, currentEmployee.UserName);
                return "true";
            }
        }
    }
}
