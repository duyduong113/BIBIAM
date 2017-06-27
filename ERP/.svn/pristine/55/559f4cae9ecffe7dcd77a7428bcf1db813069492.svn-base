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
using CMS;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class BannerController : CustomController
    {
        // GET: Banner
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
                        ViewBag.chuyen_muc = dbConn.GetDictionary<string, string>("SELECT [ma_chuyen_muc], [ten_chuyen_muc] FROM [cms_Categorys] WHERE trang_thai = 'Active' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                        ViewBag.vi_tri_Banner = dbConn.GetDictionary<string, string>("SELECT [ma_vi_tri], [ten_vi_tri] FROM [cms_Positions] WHERE  trang_thai = 'Active'  ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                        ViewBag.loai_Banner = dbConn.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'BannerType' ").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                        ViewBag.website = dbConn.Select<Code_Master_Json>("Select isnull(ma_website,'') as Value , isnull(ten_website,'') as Name from cms_Websites where trang_thai=N'Active'");
                    }
                }
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        
        public ActionResult getPositions(string chuyen_muc)
        {
            IDbConnection db = OrmliteConnection.openConn();
            try
            {
                var data = new List<Code_Master_Json>();
                data = db.Query<Code_Master_Json>("select isnull(pos.ma_vi_tri,'') as Value, isnull(pos.ten_vi_tri,'') as Name from cms_Positions pos, cms_WCL wcl where pos.ma_vi_tri=wcl.vi_tri and wcl.chuyen_muc = N'" + chuyen_muc + "' and pos.trang_thai= N'Active'").ToList();
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
        public ActionResult getCategorys(string website)
        {
            IDbConnection db = OrmliteConnection.openConn();
            try
            {
                var data = new List<Code_Master_Json>();
                data = db.Query<Code_Master_Json>("Select isnull(ma_chuyen_muc,'') as Value , isnull(ten_chuyen_muc,'') as Name from cms_Categorys where website = N'" + website + "' and trang_thai = N'Active' ").ToList();
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
        public ActionResult Detail(string id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                if (String.IsNullOrEmpty(id))
                    return RedirectToAction("Create", "NewsManage");
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.FirstOrDefault<cms_Banner>("id = {0}", id);
                    if (data == null)
                    {
                        return RedirectToAction("Create", "NewsManage");
                    }
                    else
                    {
                        ViewBag.data = data;
                    }
                    ViewBag.chuyen_muc = dbConn.Select<Code_Master_Json>("Select isnull(ma_chuyen_muc,'') as Value , isnull(ten_chuyen_muc,'') as Name from cms_Categorys where trang_thai=N'Active'");
                    ViewBag.listWebsites = dbConn.Select<Code_Master_Json>("Select isnull(ma_website,'') as Value , isnull(ten_website,'') as Name from cms_Websites where trang_thai=N'Active'");
                    ViewBag.listPositions = dbConn.Select<Code_Master_Json>("Select isnull(ma_vi_tri,'') as Value , isnull(ten_vi_tri,'') as Name from cms_Positions where trang_thai=N'Active'");
                    return View();
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (IDbConnection dbConn = CMS.Helpers.OrmliteConnection.openConn( ))
                {
                    return Json(dbConn.Select<cms_Banner>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdateBanner(cms_Banner data, HttpPostedFileBase fileBanner)
        {
            try
            {
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    if (fileBanner != null)
                    {

                        data.image_link = new AzureHelper().UploadImageToAzure("Banner", fileBanner, currentUser.name);
                        data.image = fileBanner.FileName;
                    }
                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {
                            var checkID = dbConn.SingleOrDefault<cms_Banner>("id={0}", data.id);
                            checkID.ma_website = !string.IsNullOrEmpty(data.ma_website) ? data.ma_website : "";
                            checkID.url_link = !string.IsNullOrEmpty(data.url_link) ? data.url_link : "";
                            checkID.ten_banner = !string.IsNullOrEmpty(data.ten_banner) ? data.ten_banner : checkID.ten_banner;
                            checkID.image_link = (!string.IsNullOrEmpty(data.image_link) && checkID.image_link != data.image_link) ? data.image_link : checkID.image_link;
                            checkID.image = !string.IsNullOrEmpty(data.image) ? data.image : checkID.image;
                            checkID.ma_chuyen_muc = !string.IsNullOrEmpty(data.ma_chuyen_muc) ? data.ma_chuyen_muc : "";
                            checkID.loai = !string.IsNullOrEmpty(data.loai) ? data.loai : "";
                            checkID.vi_tri = !string.IsNullOrEmpty(data.vi_tri) ? data.vi_tri : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(data.trang_thai) ? data.trang_thai : "";
                            checkID.alt = !string.IsNullOrEmpty(data.alt) ? data.alt : "";

                            checkID.nguoi_cap_nhat = currentUser.name;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            dbConn.Update(checkID);
                            data = checkID;
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to update" });
                        }
                    }
                    else
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
                        {
                            if (fileBanner == null)
                            {
                                return Json(new { success = false, message = "Vui lòng chọn hình ảnh" });
                            }

                            var lastId = dbConn.FirstOrDefault<cms_Banner>("SELECT TOP 1 * FROM cms_Banner ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_banner.Contains("BAN"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_banner.Substring(3, 10)) + 1;
                                    data.ma_banner = "BAN" + String.Format("{0:0000000000}", nextNo);
                                }
                            }
                            else
                            {
                                data.ma_banner = "BAN" + "0000000001";
                            }
                            data.ma_website = !string.IsNullOrEmpty(data.ma_website) ? data.ma_website : "";
                            data.url_link = !string.IsNullOrEmpty(data.url_link) ? data.url_link : "";
                            data.ten_banner = !string.IsNullOrEmpty(data.ten_banner) ? data.ten_banner : "";
                            data.image = !string.IsNullOrEmpty(data.image) ? data.image : "";
                            data.ma_chuyen_muc = !string.IsNullOrEmpty(data.ma_chuyen_muc) ? data.ma_chuyen_muc : "";
                            data.vi_tri = !string.IsNullOrEmpty(data.vi_tri) ? data.vi_tri : "";
                            data.alt = !string.IsNullOrEmpty(data.alt) ? data.alt : "";

                            data.loai = !string.IsNullOrEmpty(data.loai) ? data.loai : "";
                            data.trang_thai = !string.IsNullOrEmpty(data.trang_thai) ? data.trang_thai : "DANG_SU_DUNG";
                            data.nguoi_tao = currentUser.name;
                            data.ngay_tao = DateTime.Now;
                            data.nguoi_cap_nhat = currentUser.name;
                            data.ngay_cap_nhat = DateTime.Now;
                            dbConn.Insert(data);
                            data = dbConn.SingleOrDefault<cms_Banner>("ma_banner={0}", data.ma_banner);
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to create" });
                        }
                    }
                    //Sẽ dùng sau khi có fontend
                    //  List<SqlParameter> lstParameter = new List<SqlParameter>();
                    //  lstParameter.Clear();
                    //  lstParameter.Add(new SqlParameter("@ma_banner", data.ma_banner));
                    //  new SqlHelper().ExecuteNoneQuery("p_Banner_SyncToMySQL", lstParameter);
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
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

                    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<cms_Banner>("id={0}".Params(id));
                            dbConn.UpdateOnly(new cms_Banner { trang_thai = exists.trang_thai == "DANG_SU_DUNG" ? "KHONG_SU_DUNG" : "DANG_SU_DUNG", nguoi_cap_nhat = currentUser.name, ngay_cap_nhat = DateTime.Now }, onlyFields: p => new { p.trang_thai, p.nguoi_cap_nhat, p.ngay_cap_nhat }, where: p => p.id == int.Parse(id));
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
