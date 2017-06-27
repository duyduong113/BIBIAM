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
namespace CMS.Controllers
{
    public class NewsController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                data = KendoApplyFilter.KendoData<cms_News>(request);
                return Json(data);
            }
        }
        public ActionResult updateimage(cms_News data, HttpPostedFileBase file, List<string> hashtagcodes)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                    {
                        data.hinh_dai_dien = new AzureHelper().UploadImageToAzure("News", file, currentUser.name);
                    }
                    else
                    {
                        return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                    }
                    return Json(new { success = true, data = data });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
        public ActionResult DeleteNews(int id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                    {
                        var exists = dbConn.FirstOrDefault<cms_News>("id={0}".Params(id));
                        if (exists == null)
                        {
                            return Json(new { success = false, error = "Không Xóa Được!" });
                        }

                        dbConn.Delete<cms_News>("id={0}".Params(id));
                        return Json(new { success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Không có quyền xóa" });
            }
        }
    }
}