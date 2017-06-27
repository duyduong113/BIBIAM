using System;
using System.Linq;
using System.Web.Mvc;
//using ERPAPD.DCCore;
using ServiceStack.OrmLite;
using ERPAPD.Models;
using System.Globalization;
using System.Threading;
using System.Collections.ObjectModel;

namespace ERPAPD.Controllers
{
    [RequireHttps]
    public class ConfigurationSystemController : CustomController
    {
        [Authorize]
        public ActionResult Index()
        {

            var dbConn = Helpers.OrmliteConnection.openConn();
            //Get all Language
            ViewBag.ListCulture = CultureInfo.GetCultures(CultureTypes.AllCultures);
            //set  defaultCulture 
            ViewBag.ListDatetime = CultureInfo.CurrentCulture.DateTimeFormat.GetAllDateTimePatterns();
            string[] SystemDateTimePatterns = new string[250];
            int i = 0;
            foreach (string name in CultureInfo.CurrentCulture.DateTimeFormat.GetAllDateTimePatterns())
            {
                SystemDateTimePatterns[i] = name;
                i++;
            }
            ViewBag.ListDatetime = SystemDateTimePatterns;
            //Get all timeZone
            ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            ViewBag.ListTimeZone = tz;

            var item = dbConn.Select<ConfigDefaultSystems>().FirstOrDefault();
            if(item!=null)
            {
                ViewBag.Item = item;
            }
            else
            {   var setitem= new ConfigDefaultSystems();
                setitem.Culture="vi";
                setitem.LanguageID="VN";
                setitem.TimeZoneID="SE Asia Standard Time";
                setitem.DateTimeFormatID = 2;
                setitem.DateTimeFormatDisplay="dd/MM/yyyy";
                ViewBag.Item = setitem;
            }
            return View();

        }
        public ActionResult GetFormatByLanguage(string culture)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                //Get all FormatDatetim by Language
                ViewBag.ListDatetime = CultureInfo.CurrentCulture.DateTimeFormat.GetAllDateTimePatterns();
                string[] SystemDateTimePatterns = new string[250];
                int i = 0;
                foreach (string name in CultureInfo.CurrentCulture.DateTimeFormat.GetAllDateTimePatterns())
                {
                    SystemDateTimePatterns[i] = name;
                    i++;
                }
                ViewBag.ListDatetime = SystemDateTimePatterns;
            }
            return View("_listDatetime");
        }

        public ActionResult SetDefault(ConfigDefaultSystems item, string returnurl)
        {
            string[] SystemDateTimePatterns = new string[250];
            SystemDateTimePatterns[0] = "dd/MM/yyyy";
            SystemDateTimePatterns[1] = "dd/MM/yyyy HH:mm";
            SystemDateTimePatterns[2] = "MM/dd/yyyy";
            SystemDateTimePatterns[3] = "MM/dd/yyyy HH:mm";
            Session["DateTimeFormat"] = SystemDateTimePatterns[item.DateTimeFormatID];
            var dbConn = Helpers.OrmliteConnection.openConn();
            var itemconfig = new ConfigDefaultSystems();
            var Exitconfig = dbConn.Select<ConfigDefaultSystems>().FirstOrDefault();
            if (Exitconfig != null)
            {
                    Exitconfig.TimeZoneID = item.TimeZoneID;
                    Exitconfig.TimeZoneDisplayName = item.TimeZoneDisplayName;
                    Exitconfig.Culture = item.Culture;
                    Exitconfig.LanguageID = item.LanguageID;
                    Exitconfig.DateTimeFormatID = item.DateTimeFormatID;
                    Exitconfig.DateTimeFormatDisplay = SystemDateTimePatterns[item.DateTimeFormatID];
                    Exitconfig.UpdatedAt = DateTime.Now;
                    Exitconfig.UpdatedBy = currentUser.UserName;
                    dbConn.Update(Exitconfig);
            }
            else
            {
                itemconfig.Culture = "vi";
                itemconfig.LanguageID = "VN";
                itemconfig.DateTimeFormatID = 0;
                itemconfig.DateTimeFormatDisplay = "dd/MM/yyyy";
                itemconfig.TimeZoneID = "SE Asia Standard Time";
                itemconfig.TimeZoneDisplayName = "(GMT+07:00) Bangkok, Hanoi, Jakarta";
                itemconfig.CreatedAt = DateTime.Now;
                itemconfig.CreatedBy = currentUser.UserName;
                itemconfig.UpdatedAt = DateTime.Parse("1900-01-01");
                itemconfig.UpdatedBy = "";
                dbConn.Insert(itemconfig);
                Session["Culture"] = new CultureInfo(itemconfig.Culture);
                Session["LanguageID"] = itemconfig.LanguageID;
                Session["DateTime"] = itemconfig.DateTimeFormatDisplay;
            }
            Session["Culture"] = new CultureInfo(item.Culture.ToString());
            Session["LanguageID"] = item.LanguageID;
            Session["DateTime"] = item.DateTimeFormatDisplay;
            return Redirect(returnurl);
        }
    }
}