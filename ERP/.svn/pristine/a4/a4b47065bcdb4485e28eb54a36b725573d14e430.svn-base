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
    public class PositionsManageController : CustomController
    {
        // GET: PositionsManage
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;

                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    ViewBag.listStatus = db.Select<DropListDown>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " as Text FROM Code_Master WHERE Type = 'WebsitesStatus' Order By order_Num"); 
                    ViewBag.listPositionType = db.Select<DropListDown>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " as Text FROM Code_Master WHERE Type = 'PositionType' Order By order_Num");
                    ViewBag.listWebsites = db.GetDictionary<string, string>("Select ma_website as Value , ten_website as Name from cms_Websites  where trang_thai=N'Active' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                    ViewBag.listCategorys = db.Select<Code_Master_Json>("Select isnull(ma_chuyen_muc,'') as Value , isnull(ten_chuyen_muc,'') as Name from cms_Categorys where trang_thai=N'Active'");
                }
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = KendoApplyFilter.KendoData<cms_Positions>(request);
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
                string st = new cms_Positions().Delete(ids);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else if(st== "exist")
                    return Json(new { success = false, message = "Vui lòng xóa hết chi tiết trong vị trí cần xóa trước! " });
                else
                    return Json(new { success = false, message = "Xóa thất bại! " });
            }
            return Json(new { success = false, message = "Don't have permission to delete " });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_Positions> listrow)
        {
            ModelState.Clear(); // phải clear
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                string st = new cms_Positions().UpSert(listrow.ToList(), currentUser.name, "Insert");
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else if (st == "exist_ma_vi_tri")
                    ModelState.AddModelError("", "Mã vị trí đã tồn tại!");
                else
                    ModelState.AddModelError("", "Tạo vị trí thất bại");

                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Don't have permission to Create");
            return Json(new List<cms_Positions>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_Positions> listrow)
        {
            ModelState.Clear(); // phải clear
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                {
                    string st = new cms_Positions().UpSert(listrow.ToList(), currentUser.name, "Update");
                    if (st == "true")
                        return Json(new { success = true });
                    else if (st == "exist_ma_vi_tri")
                        ModelState.AddModelError("", "Mã vị trí đã tồn tại!");
                    //else if (st == "exist_website")
                    //    ModelState.AddModelError("", "Website đã được sử dụng cho vị trí khác!");
                    else
                        ModelState.AddModelError("", "Cập nhật vị trí thất bại");

                    return Json(listrow.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "Don't have permission to Create");
                    return Json(new List<cms_Positions>().ToDataSourceResult(request, ModelState));
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
                data = db.Query<DropListDown>("Select ma_chuyen_muc as Value , ten_chuyen_muc as Text from cms_Categorys where website = N'" + website + "' and trang_thai = N'Active' ").ToList();
                //var data = db.Query<string>("Select ten_chuyen_muc  from cms_Categorys where website = N'" + website + "' ");
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }


        //////////////////////////////////////////////////////////////////////

        public ActionResult SubGrid_Read([DataSourceRequest] DataSourceRequest request, string ma_vi_tri)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {

                var data = dbConn.Select<cms_WCL>(s => s.vi_tri == ma_vi_tri);
                return Json(data.ToDataSourceResult(request));

                //var data = KendoApplyFilter.KendoData<cms_WCL>(request, "vi_tri= '"+ma_vi_tri+"' ");
                //return Json(data);
            }
        }

        public ActionResult SubGrid_Delete(string data)
        {
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {

                string[] separators = { "," };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length == 0)
                {
                    return Json(new { success = false, message = "Chọn chi tiết cần xóa!" });
                }
                string st = new cms_WCL().Delete(ids);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Don't have permission to delete " });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubGrid_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_WCL> listrow, string ma_vi_tri)
        {
            ModelState.Clear(); // phải clear
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                string st = new cms_WCL().UpSert(listrow.ToList(), currentUser.name, "Insert",ma_vi_tri);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else if (st == "exist_ma_chuyen_muc")
                    ModelState.AddModelError("", "Chuyên mục của website này đã được sử dụng cho vị trí này!");
                else
                    ModelState.AddModelError("", "Thêm chi tiết thất bại");

                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Don't have permission to Create");
            return Json(new List<cms_WCL>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubGrid_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<cms_WCL> listrow,string ma_vi_tri)
        {
            ModelState.Clear(); // phải clear
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                {
                    string st = new cms_WCL().UpSert(listrow.ToList(), currentUser.name, "Update", ma_vi_tri);
                    if (st == "true")
                        return Json(new { success = true });
                    else if (st == "exist_ma_chuyen_muc")
                        ModelState.AddModelError("", "Chuyên mục của website này đã được sử dụng cho vị trí này!");
                    else
                        ModelState.AddModelError("", "Cập nhật chi tiết thất bại");

                    return Json(listrow.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "Don't have permission to Create");
                    return Json(new List<cms_WCL>().ToDataSourceResult(request, ModelState));
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