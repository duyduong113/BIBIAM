using CMS.Helpers;
using CMS.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Hangfire;

namespace CMS.Controllers
{
    public class HangfireJobController : Controller
    {
        // GET: HangfireJob
        public ActionResult Index()
        {
            RecurringJob.AddOrUpdate("Backup_DB_Daily", () => Backup_DB_Daily(), "0 6 * * *", TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            return View();
        }
        public void Backup_DB_Daily()
        {
            using (IDbConnection dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                dbConn.ExecuteNonQuery("exec spBackUpDatabases");
              //  BackgroundJob.Enqueue(
              //                                   () => new Helpers.SendMail().Send());
            }
        }
       
    }
}