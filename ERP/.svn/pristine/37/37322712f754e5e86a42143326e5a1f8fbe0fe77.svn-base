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
    public class SalesOrderItemController : CustomController
    {
        //
        // GET: /SalesOrderItem/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {

                }
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
                    data = KendoApplyFilter.KendoData<Sales_Item>(request, "1=1");
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Sales_Item> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction())
                            {
                                try
                                {
                                    item.CreatedAt = DateTime.Now;
                                    item.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(item);

                                    dbTrans.Commit();
                                }
                                catch (Exception ex)
                                {
                                    ModelState.AddModelError("", ex.Message);
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Sales_Item> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction())
                            {
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                                dbTrans.Commit();
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}