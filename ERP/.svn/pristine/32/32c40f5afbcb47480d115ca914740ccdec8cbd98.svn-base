using CMS.Filters;
using CMS.Helpers;
using CMS.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class CategorysManageController : CustomController
    {
        // GET: CategorysManage
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;

                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    ViewBag.listWebsites = db.Select<DropListDown>("Select ma_website as Value , ten_website as Text from cms_Websites where trang_thai=N'Active' ");
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
                var data = KendoApplyFilter.KendoData<cms_Categorys>(request);
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
                    return Json(new { success = false, message = "Chọn chuyên mục cần xóa!" });
                }
                string st = new cms_Categorys().Delete(ids);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = "Xóa thất bại" });
            }
            return Json(new { success = false, message = "Don't have permission to delete " });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_Categorys> listrow)
        {
            ModelState.Clear(); // phải clear
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                string st = new cms_Categorys().UpSert(listrow.ToList(), currentUser.name, "Insert");
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", "Tạo chuyên mục thất bại");

                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Don't have permission to Create");
            return Json(new List<cms_Categorys>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_Categorys> listrow)
        {
            ModelState.Clear(); // phải clear
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                {
                    string st = new cms_Categorys().UpSert(listrow.ToList(), currentUser.name, "Update");
                    if (st == "true")
                        return Json(new { success = true });
                    else
                        ModelState.AddModelError("", "Cập nhật chuyên mục thất bại");

                    return Json(listrow.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "Don't have permission to Create");
                    return Json(new List<cms_Categorys>().ToDataSourceResult(request, ModelState));
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