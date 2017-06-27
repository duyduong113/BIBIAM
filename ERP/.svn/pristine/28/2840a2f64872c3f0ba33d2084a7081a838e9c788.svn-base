using MCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Kendo.Mvc.UI;
using System.Data;
using MCC.Helpers;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using MCC.Filters;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class LanguageManagementController : CustomController
    {
        //private readonly bool overwrite;
        //
        // GET: /LanguageManagement/
        public ActionResult Index()
        {
            //using (var dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(
            //        typeof(tw_GlobalLanguage)
            //        );
            //    dbConn.CreateTables(overwrite,
            //        typeof(tw_GlobalLanguage)
            //        );
            //}
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                data = KendoApplyFilter.KendoData<tw_GlobalLanguage>(request);
                return Json(data);
            }
        }
        public ActionResult deletelanguage(Int64 id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    dbConn.Delete<tw_GlobalLanguage>("id={0}".Params(id, currentUser.ma_gian_hang));
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
        [HttpPost]
        public ActionResult CreateUpdate(tw_GlobalLanguage data, HttpPostedFileBase file)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {
                            if (data.isDefault)
                            {
                                var existL = dbConn.Select<tw_GlobalLanguage>("id<>{0}", data.id);
                                existL.ForEach(s => { s.isDefault = false; });
                                dbConn.UpdateAll(existL);
                            }

                            var exist = dbConn.SingleOrDefault<tw_GlobalLanguage>("id={0}", data.id);
                            data.imagesPublicId = exist.imagesPublicId;
                            //data.imagesSize = exist.imagesSize;
                            data.updatedAt = DateTime.Now;
                            data.updatedBy = currentUser.name;
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                                if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                                {
                                    //string publicId = "CRM/Localization/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                                    //data.imagesPublicId = publicId;
                                    //var imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                                    //data.imagesSize = imagesSize;
                                    string LocalPath = "";
                                    data.imagesPublicId = new AzureHelper().UploadImageToAzure(AllConstant.FoldderName_User, file, currentUser.name, ref LocalPath);
                                }
                                else
                                {
                                    return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                                }
                            }
                            dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.code,
                                            p.name,
                                            p.isDefault,
                                            p.active,
                                            p.imagesSize,
                                            p.imagesPublicId,
                                            p.updatedAt,
                                            p.updatedBy
                                        },
                                where: p => p.id == data.id);
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
                            if (data.isDefault)
                            {
                                var exist = dbConn.Select<tw_GlobalLanguage>();
                                exist.ForEach(s => { s.isDefault = false; });
                                dbConn.UpdateAll(exist);
                            }
                            data.createdAt = DateTime.Now;
                            data.createdBy = currentUser.name;
                            dbConn.Insert(data);
                            int Id = (int)dbConn.GetLastInsertId();
                            data.id = Id;
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                                if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                                {
                                    //string publicId = "CRM/Localization/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                                    //data.imagesPublicId = publicId;
                                    //var imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                                    //data.imagesSize = imagesSize;
                                    string LocalPath = "";
                                    data.imagesPublicId = new AzureHelper().UploadImageToAzure(AllConstant.FoldderName_User, file, currentUser.name, ref LocalPath);
                                    dbConn.Update(data);
                                }
                                else
                                {
                                    return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                                }
                            }
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to create" });
                        }
                    }
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
    }
}