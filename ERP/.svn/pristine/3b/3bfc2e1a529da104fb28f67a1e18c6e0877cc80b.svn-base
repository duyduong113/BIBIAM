using BIBIAM.Core.Entities;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using MCC.Filters;
using BIBIAM.Core;
using System.Collections.Generic;
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class FireBaseIncomingController : CustomController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {           
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        [HttpPost]
        public ActionResult Order(string key, string orderid)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                try
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                    {
                        var data = new List<Merchant_OrderHeader>();
                        var sp = dbConn.ExecuteSql("EXEC p_Sync_Merchant_Order_FromFE @ma_don_hang_fe={0}".Params(orderid));
                        data = dbConn.SqlList<Merchant_OrderHeader>(@"SELECT id,ma_don_hang,ma_gian_hang,hoten FROM Merchant_OrderHeader WHERE ma_don_hang_cha={0}".Params(orderid));
                        log.Info("OKI. IP:" + GetUserIPAddress()+" OrderID:"+orderid);
                        return Json(new { success = true, magh = currentUser.ma_gian_hang,  data = data });
                    }
                }
                catch (Exception e)
                {
                    log.Error("Error. IP:" + GetUserIPAddress()+ "Error:"+ e.Message);
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                log.Warn("Login-Fail. IP:"+ GetUserIPAddress());
                //Send mail
                return Json(new { success = false, error = "Không có quyền!" });
            }
            
        }
        /// <summary>
        /// Get current user ip address.
        /// </summary>
        /// <returns>The IP Address</returns>
        public static string GetUserIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            string ip = String.Empty;
            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                ip = context.Request.UserHostAddress;
            if (ip == "::1")
                ip = "127.0.0.1";
            return ip;
        }
    }
}