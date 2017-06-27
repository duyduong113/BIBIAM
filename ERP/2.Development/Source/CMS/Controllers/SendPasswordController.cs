using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Kendo.Mvc.UI;
using System.Data;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using CMS.Controllers;
using CMS.Filters;
using CMS.Helpers;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class SendPasswordController : CustomController
    {
        //
        // GET: /AgencyManagement/
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    //dbConn.DropTables(typeof(tw_Customer));
                    //const bool overwritte = false;
                    //dbConn.CreateTables(overwritte, typeof(Customer));

                    ViewBag.isAdmin = isAdmin;
                    return View();
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }

       

      

    }
}