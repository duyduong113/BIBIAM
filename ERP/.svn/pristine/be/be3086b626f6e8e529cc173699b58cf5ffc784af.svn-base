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
    public class BoxtinManageController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;

                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    ViewBag.listStatus = db.Select<DropListDown>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " as Text FROM Code_Master WHERE Type = 'BoxtinStatus' Order By order_Num");
                    ViewBag.listPosition = db.Select<DropListDown>("SELECT ma_vi_tri as Value,ten_vi_tri as Text FROM cms_Positions");
                    ViewBag.listWebsites = db.GetDictionary<string, string>("Select ma_website as Value , ten_website as Name from cms_Websites").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                    ViewBag.listCategorys = db.Select<Code_Master_Json>("Select isnull(ma_chuyen_muc,'') as Value , isnull(ten_chuyen_muc,'') as Name from cms_Categorys");
                }
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = KendoApplyFilter.KendoData<cms_BoxTin>(request);
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
                    return Json(new { success = false, message = "Chọn vị trí cần xóa!" });
                }
                string st = new cms_BoxTin().Delete(ids);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Don't have permission to delete " });
        }
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_BoxTin> listrow)
        {
            ModelState.Clear(); 
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                string st = new cms_BoxTin().UpSert(listrow.ToList(), currentUser.name, "Insert");
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else if (st == "Exist")
                    ModelState.AddModelError("", "Đã tồn tại!");
                else
                    ModelState.AddModelError("", "Thất bại");

                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Don't have permission to Create");
            return Json(new List<cms_BoxTin>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_BoxTin> listrow)
        {
            ModelState.Clear();
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                {
                    string st = new cms_BoxTin().UpSert(listrow.ToList(), currentUser.name, "Update");
                    if (st == "true")
                        return Json(new { success = true });
                    else if (st == "Exist")
                        ModelState.AddModelError("", "Đã tồn tại!");
                    else
                        ModelState.AddModelError("", "Thất bại");

                    return Json(listrow.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "Don't have permission to Create");
                    return Json(new List<cms_BoxTin>().ToDataSourceResult(request, ModelState));
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
        public ActionResult getCategorys(string website)
        {
            IDbConnection db = OrmliteConnection.openConn();
            try
            {
                var data = new List<DropListDown>();
                data = db.Query<DropListDown>("Select ma_chuyen_muc as Value , ten_chuyen_muc as Text from cms_Categorys where website = N'" + website + "' ").ToList();
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}