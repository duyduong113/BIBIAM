using MCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data;
using MCC.Helpers;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using Dapper;
using MCC.Filters;


namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class CodeMasterManagementController : CustomController
    {
        //
        // GET: /CodeMasterManagement/
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                data = KendoApplyFilter.KendoData<Code_Master>(request);
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Code_Master> datas)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                if (datas != null && ModelState.IsValid)
                {
                    foreach (var item in datas)
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            item.CreatedAt = DateTime.Now;
                            item.CreatedBy = currentUser.name;
                            dbConn.Insert(item);
                            var Id = (Int64)dbConn.GetLastInsertId();
                            item.Id = Id;

                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission to create");
            }

            return Json(datas.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models", Exclude = "UpdatedAt")]IEnumerable<Code_Master> datas)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                if (datas != null && ModelState.IsValid)
                {
                    foreach (var item in datas)
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = currentUser.name;

                            dbConn.UpdateOnly(item,
                                        onlyFields: p =>
                                            new
                                            {
                                                p.Type,
                                                p.Name,
                                                p.Name_Vi,
                                                p.Value,
                                                p.IsDefault,
                                                p.order_Num,
                                                p.UpdatedAt,
                                                p.UpdatedBy
                                            },
                                    where: p => p.Id == item.Id);

                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission to update");
            }

            return Json(datas.ToDataSourceResult(request, ModelState));
        }

    }
}