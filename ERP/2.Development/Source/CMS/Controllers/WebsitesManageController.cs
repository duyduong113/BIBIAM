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
using CMS.Helpers;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using CMS.Filters;
using System.Data.SqlClient;
using CMS.Controllers;
using Kendo.Mvc.Extensions;


namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class WebsitesManageController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;

                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    ViewBag.listStatus = db.Select<DropListDown>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " as Text FROM Code_Master WHERE Type = 'WebsitesStatus' Order By order_Num"); ;
                }
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = KendoApplyFilter.KendoData<cms_Websites>(request);
                return Json(data);
            }
        }


        public ActionResult Delete(string data)
        {
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {

                string[] separators = { "," };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length == 0)
                {
                    return Json(new { success = false, message = "Chọn websites cần xóa!" });
                }
                string st = new cms_Websites().Delete(ids);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = "Xóa thất bại" });
            }
            return Json(new { success = false, message = "Don't have permission to DELETE " });
            }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_Websites> listrow)
        {
            ModelState.Clear(); // phải clear
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                string st = new cms_Websites().UpSert(listrow.ToList(), currentUser.name, "Insert");
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else if (st == "Exist")
                    ModelState.AddModelError("", "Mã website đã tồn tại!");
                else
                    ModelState.AddModelError("", "Tạo website thất bại");

                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Don't have permission to Create");
            return Json(new List<cms_Websites>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_Websites> listrow)
        {
            ModelState.Clear(); // phải clear
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                {
                    
                    string st = new cms_Websites().UpSert(listrow.ToList(), currentUser.name, "Update");
                    if (st == "true")
                        return Json(new { success = true });
                    else if (st == "Exist")
                        ModelState.AddModelError("", "Mã website đã tồn tại!");
                    else
                        ModelState.AddModelError("", "Cập nhật website thất bại");

                    return Json(listrow.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "Don't have permission to Create");
                    return Json(new List<cms_Websites>().ToDataSourceResult(request, ModelState));
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    error = e.Message
                });
            }
        }

    }
}