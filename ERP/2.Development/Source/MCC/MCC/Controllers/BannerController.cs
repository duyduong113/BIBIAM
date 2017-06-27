using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BIBIAM.Core.Entities;
using MCC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BIBIAM.Core.Data.DataObject;
using CloudinaryDotNet;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using MCC.Helpers;
using MCC.Filters;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class BannerController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                if (isAdmin)
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    using (var db = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                    {
                        ViewBag.ma_phan_cap_banner = db.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'HierarchyBanner' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                        ViewBag.vi_tri_Banner = db.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'BannerPosition' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                        ViewBag.loai_Banner = db.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'BannerType' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
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
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = KendoApplyFilter.KendoData<Banner>(request);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdateBanner(Banner banner, HttpPostedFileBase fileBanner)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                if (banner.id == 0 && fileBanner == null)
                {
                    return Json(new { success = false, message = "Vui lòng chọn hình ảnh" });
                }

                banner.image = "";
                if (fileBanner != null)
                {
                    string LocalPath = "";
                    banner.image_link = new AzureHelper().UploadImageToAzure(AllConstant.FoldderName_Banner, fileBanner, currentUser.name, ref LocalPath);
                    banner.image = fileBanner.FileName;
                }
                banner.ngay_tao = DateTime.Now;
                banner.nguoi_tao = currentUser.name;
                banner.ngay_cap_nhat = DateTime.Now;
                banner.nguoi_cap_nhat = currentUser.name;
                string rs = new Banner_DAO().CreateUpdate(banner, currentUser.name, AppConfigs.MCCConnectionString);
                if (!string.IsNullOrEmpty(rs))
                {
                    string[] StringSplit = new string[] { "@@" };
                    if (rs.Split(StringSplit, StringSplitOptions.None)[0] == "false")
                        return Json(new { success = false, message = rs.Split(StringSplit, StringSplitOptions.None)[1] });
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        var data = dbConn.FirstOrDefault<Banner>("ma_banner={0}".Params(rs.Split(StringSplit, StringSplitOptions.None)[1]));
                        return Json(new { success = true, data = data });
                    }
                }

            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult ActiveBanner(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                try
                {
                    string[] separators = { "," };
                    string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (ids.Length == 0)
                    {
                        return Json(new { success = false, message = "Chọn các Banner cần kích hoạt!" });
                    }

                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                    {
                        foreach (var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<Banner>("id={0}".Params(id));
                            dbConn.UpdateOnly(new Banner { trang_thai = exists.trang_thai == "DANG_SU_DUNG" ? "KHONG_SU_DUNG" : "DANG_SU_DUNG", nguoi_cap_nhat = currentUser.name, ngay_cap_nhat = DateTime.Now }, onlyFields: p => new { p.trang_thai, p.nguoi_cap_nhat, p.ngay_cap_nhat }, where: p => p.id == int.Parse(id));
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
