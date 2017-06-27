using System;
using System.Linq;
using ERPAPD.Models;
//using ERPAPD.Xlite;
//using ERPAPD.vn.mobivi.cs;
using System.Security.Cryptography;
using System.Text;
using ServiceStack.OrmLite;
using System.Configuration;

namespace ERPAPD.Helpers
{
    public static class XliteAPI
    {
        public static string Dial(string mobile, string XliteID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var username = System.Web.HttpContext.Current.User.Identity.Name;
                    var currentUser = dbConn.FirstOrDefault<Users>("UserName={0}", username);
                    if (string.IsNullOrEmpty(currentUser.info.XLiteCode))
                    {
                        return "Không thể gọi vì chưa có XliteID!";
                    }
                    RestfulClient.POST(ConfigurationManager.AppSettings["XliteAPI"].ToString().Trim() + "?extension=" + XliteID + "&code=" + currentUser.info.XLiteCode + "&prefix_number=70&number=" + mobile + "&user=deca_user&password=" + GetMd5Hash(XliteID + "1611HoaSao@@"), "");
                    return "success";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public static void Dial2(string mobile)
        {
            //    var user = System.Web.HttpContext.Current.User.Identity.Name;
            //    var userinfo = Models.User.GetAgentID(user);
            //    var agent = userinfo.RefID;
            //    var fromphone = userinfo.Phone;
            //    string hash = GetMd5Hash(agent + fromphone + mobile + "mbv@portal");
            //    ERPAPD.vn.mobivi.cs.OutboundCallWebService service = new ERPAPD.vn.mobivi.cs.OutboundCallWebService();
            //    service.dial(agent, fromphone, mobile, hash);
        }

        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}