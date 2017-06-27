using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM24H.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CRM24H.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace CRM24H.Controllers
{
    public class WebSiteController : CustomController
    {
        //
        // GET: /WebSite/
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Create = asset.Create;
                ViewBag.Update = asset.Update;
                ViewBag.Delete = asset.Delete;
                ViewBag.Export = asset.Export;
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    var data = CRM_Website.ReadAll();
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Website> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Create)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    foreach (var row in listrow)
                    {
                        if (string.IsNullOrEmpty(row.WebsiteName))
                        {
                            ModelState.Clear();
                            ModelState.AddModelError("", "Vui lòng nhập tên website");
                            continue;
                        }
                        row.CreatedAt = DateTime.Now;
                        row.CreatedBy = currentUser.UserName;
                        row.UpdatedAt = DateTime.Now;
                        row.UpdatedBy = currentUser.UserName;
                        dbConn.Save(row);
                    }
                }
                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Create");
            return Json(new List<CRM_Website>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Website> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Update)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    foreach (var row in listrow)
                    {
                        row.UpdatedAt = DateTime.Now;
                        row.UpdatedBy = currentUser.UserName;
                        dbConn.Update(row);
                    }
                }
                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Update");
            return Json(new List<CRM_Website>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeleteWebsite(string data)
        {
            if (asset.Delete)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                   // var ids = listid.Split("@@");
                    foreach (var id in ids)
                    {
                        CRM_Website.Delete(id);
                    }
                    return Json(new { success = true, message = "Thành công" });
                }
            }
            return Json(new { success = false, message = "Bạn " });
        }
    }
}