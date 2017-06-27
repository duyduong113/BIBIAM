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
    public class BrandManagementController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                ViewBag.isAdmin = isAdmin;
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
                    var data = KendoApplyFilter.KendoData<BrandManagement>(request);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdateBrand(BrandManagement brand, HttpPostedFileBase file)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                //if (brand.id == 0 && file == null)
                //{
                //    return Json(new { success = false, message = "Vui lòng chọn hình ảnh" });
                //}
                if (file != null)
                {
                    string LocalPath = "";
                    brand.logo = new AzureHelper().UploadImageToAzure(AllConstant.FoldderName_Brand, file, currentUser.name, ref LocalPath);
                }
                brand.ngay_tao = DateTime.Now;
                brand.nguoi_tao = currentUser.name;
                brand.ngay_cap_nhat = DateTime.Now;
                brand.nguoi_cap_nhat = currentUser.name;
                string rs = new BrandManagementDAO().CreateUpdate(brand, currentUser.name, AppConfigs.MCCConnectionString);
                if (!string.IsNullOrEmpty(rs))
                {
                    string[] StringSplit = new string[] { "@@" };
                    if (rs.Split(StringSplit, StringSplitOptions.None)[0] == "false")
                        return Json(new { success = false, error = rs.Split(StringSplit, StringSplitOptions.None)[1] });
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        var data = dbConn.FirstOrDefault<BrandManagement>("ma_thuong_hieu={0}".Params(rs.Split(StringSplit, StringSplitOptions.None)[1]));
                        return Json(new { success = true, data = data });
                    }
                }

            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult GetBrandInfo()
        {
            using (var dbConn = OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT ma_thuong_hieu as id, isnull(ten_thuong_hieu,'') as name FROM BrandManagement").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ActiveBrand(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                try
                {
                    string[] separators = { "," };
                    string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (ids.Length == 0)
                    {
                        return Json(new { success = false, message = "Chọn các Brand cần kích hoạt!" });
                    }

                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                    {
                        foreach (var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<BrandManagement>("id={0}".Params(id));
                            dbConn.UpdateOnly(new BrandManagement { trang_thai = exists.trang_thai == "DANG_SU_DUNG" ? "KHONG_SU_DUNG" : "DANG_SU_DUNG", nguoi_cap_nhat = currentUser.name, ngay_cap_nhat = DateTime.Now }, onlyFields: p => new { p.trang_thai, p.nguoi_cap_nhat, p.ngay_cap_nhat }, where: p => p.id == int.Parse(id));
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
        public ActionResult deleteBrand(string ma_thuong_hieu)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                string rs = new BrandManagementDAO().Delete(ma_thuong_hieu, AppConfigs.MCCConnectionString);
                if (rs != "true")
                {
                    return Json(new { success = false, message = rs });

                }
                return Json(new { success = true, message = "Xóa thành công!" });
            }
            return Json(new { success = false, message = "Không có quyền xóa!" });
        }

    }
}
