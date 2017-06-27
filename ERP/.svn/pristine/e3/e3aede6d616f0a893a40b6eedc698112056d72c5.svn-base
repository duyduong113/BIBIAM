using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CloudinaryDotNet;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using CMS.Helpers;
using CMS.Filters;
using CMS.Controllers;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class FooterManagementController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                ViewBag.isAdmin = isAdmin;
                if (isAdmin)
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    {

                        ViewBag.loai_Footer = dbConn.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'TypeHeaderFooter' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                        ViewBag.WebsiteID_list = dbConn.GetDictionary<string, string>("SELECT ma_website,ten_website FROM cms_Websites").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                    }
                }
                return View();
            }
            else return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {

                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (isAdmin)
                        return Json(dbConn.Select<cms_Footer>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdateFooter(cms_Footer Footer, HttpPostedFileBase file,string type)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                if (Footer.id == 0 && Footer.hinh_anh==true && file == null)
                {
                    return Json(new { success = false, message = "Vui lòng chọn hình ảnh" });
                }
                if (file != null)
                {
                    Footer.image_link = new AzureHelper().UploadImageToAzure("Footer", file, currentUser.name);
                }                
                Footer.ngay_tao = DateTime.Now;
                Footer.nguoi_tao = currentUser.name;
                Footer.ngay_cap_nhat = DateTime.Now;
                Footer.nguoi_cap_nhat = currentUser.name;
                Footer.slug = SqlHelper.convertToUnSign3(Footer.slug);
                string rs = new cms_Footer().CreateUpdate(Footer, currentUser.name, type);
                if (!string.IsNullOrEmpty(rs))
                {
                    string[] StringSplit = new string[] { "@@" };
                    if (rs.Split(StringSplit, StringSplitOptions.None)[0] == "false")
                        return Json(new { success = false, error = rs.Split(StringSplit, StringSplitOptions.None)[1] });
                    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var data = dbConn.FirstOrDefault<cms_Footer>("ma={0}".Params(rs.Split(StringSplit, StringSplitOptions.None)[1]));
                        return Json(new { success = true, data = data });
                    }
                }
                
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult ActiveFooter(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                try
                {
                    string[] separators = { "," };
                    string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (ids.Length == 0)
                    {
                        return Json(new { success = false, message = "Chọn các Footer cần kích hoạt!" });
                    }

                    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<cms_Footer>("id={0}".Params(id));
                            dbConn.UpdateOnly(new cms_Footer { trang_thai = exists.trang_thai == "DANG_SU_DUNG" ? "KHONG_SU_DUNG" : "DANG_SU_DUNG", nguoi_cap_nhat = currentUser.name, ngay_cap_nhat = DateTime.Now }, onlyFields: p => new { p.trang_thai, p.nguoi_cap_nhat, p.ngay_cap_nhat }, where: p => p.id == int.Parse(id));
                        }
                    }
                    return Json(new { success = true, message = "Thành công!" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền thực hiên chức năng này!" });
            }
        }
    }
}
