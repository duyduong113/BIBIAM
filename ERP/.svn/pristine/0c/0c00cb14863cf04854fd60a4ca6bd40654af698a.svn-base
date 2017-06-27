using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DecaPay.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DecaPay.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using Dapper;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DecaPay.Controllers.Sales_Component
{
    [Authorize]
    [RequireHttps]
    public class SalesPromotionController : CustomController
    {
        //
        // GET: /SalesPromotion/
        public ActionResult Index()
        {
            if (asset.View)
            {
                //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                //{
                //    dbConn.DropTables(typeof(Sales_Promotion_Detail), typeof(Sales_Promotion_Header));
                //    const bool overwrite = true;
                //    dbConn.CreateTables(overwrite, typeof(Sales_Promotion_Header), typeof(Sales_Promotion_Detail));
                //}
                //var test = DateTime.UtcNow;
                //var test1 = DateTime.Now.ToUniversalTime();
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        //list don hang
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Sales_Promotion_Header>(request, "1=1");
                }
                return Json(data);
            }
        }

        //list detail cua don hang
        public ActionResult Detail_Read([DataSourceRequest]DataSourceRequest request, string Id)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Sales_Promotion_Detail>();
                if (asset.View)
                {
                    data = dbConn.Select<Sales_Promotion_Detail>("HeaderId={0}", Id);
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}