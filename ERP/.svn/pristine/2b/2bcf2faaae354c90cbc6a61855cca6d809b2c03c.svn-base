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
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class ArticleController : CustomController
    {
        //private readonly bool overwrite;
        //
        // GET: /Article/
        public ActionResult Index()
        {
            //using (var dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(
            //        typeof(tw_Article)
            //        );
            //    dbConn.CreateTables(overwrite,
            //        typeof(tw_Article)
            //        );
            //}
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

                data = KendoApplyFilter.KendoData<Article>(request);
                //else
                //{
                //    data = KendoApplyFilter.KendoData<Article>(request,"companycode IN (SELECT showroomId FROM tw_UserInShowroom WHERE userId IN (SELECT id FROM [Users] WHERE name = '" + User.Identity.Name + "'))");
                //}                
                return Json(data);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(Article data, HttpPostedFileBase file, List<string> hashtagcodes)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                //using (var dbTrans = dbConn.OpenTransaction())
                {
                    data.hashtagcode = "";
                    if (hashtagcodes != null)
                    {
                        foreach (string item in hashtagcodes)
                            data.hashtagcode += item + ";";
                    }

                    if (!string.IsNullOrEmpty(data.hashtagcode))
                        data.hashtagcode = data.hashtagcode.Substring(0, data.hashtagcode.Length - 1);

                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {
                            if (data.isDefault)
                            {
                                var existL = dbConn.Select<Article>("id<>{0}", data.id);
                                existL.ForEach(s => { s.isDefault = false; });
                                dbConn.UpdateAll(existL);
                            }

                            var exist = dbConn.SingleOrDefault<Article>("id={0}", data.id);
                            data.allUser = isAdmin ? data.allUser : false;
                            data.imagesPublicId = exist.imagesPublicId;
                           
                            data.updatedAt = DateTime.Now;
                            data.updatedBy = currentUser.name;
                            if (data.allUser == true)
                            {
                                data.companycode = "";
                            }
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                                if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                                {
                                    //string publicId = "SaiGonFord/Article/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                                    //data.imagesPublicId = publicId;
                                    //var imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                                    //data.imagesSize = imagesSize;
                                    data.imagesPublicId = new AzureHelper().UploadImageToAzure("Article", file, currentUser.name);
                                }
                                else
                                {
                                    return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                                }
                            }

                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@articlecode", exist.articlecode));
                            param.Add(new SqlParameter("@type", data.type));
                            param.Add(new SqlParameter("@name", data.name));
                            param.Add(new SqlParameter("@description", data.description));
                            param.Add(new SqlParameter("@content", data.content));
                            param.Add(new SqlParameter("@category", string.IsNullOrEmpty(data.category) ? "" : data.category));
                            param.Add(new SqlParameter("@imagesPublicId", string.IsNullOrEmpty(data.imagesPublicId) ? "" : data.imagesPublicId));
                            //param.Add(new SqlParameter("@imagesSize", data.imagesSize == null ? "" : data.imagesSize));
                            param.Add(new SqlParameter("@active", data.active));
                            param.Add(new SqlParameter("@createdBy", exist.createdBy));
                            param.Add(new SqlParameter("@updatedBy", data.updatedBy));
                            param.Add(new SqlParameter("@allUser", data.allUser));
                            param.Add(new SqlParameter("@isDefault", data.isDefault));
                            param.Add(new SqlParameter("@companycode", data.companycode));
                            param.Add(new SqlParameter("@hashtagcode", data.hashtagcode));
                            new SqlHelper().ExecuteQuery("p_Article_SyncToMySQL", param);
                            //dbTrans.Commit();

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
                                var existL = dbConn.Select<Article>();
                                existL.ForEach(s => { s.isDefault = false; });
                                dbConn.UpdateAll(existL);
                            }
                            if (data.allUser == true)
                            {
                                data.companycode = "";
                            }

                            string articlecode = String.Empty;
                            var lastId = dbConn.FirstOrDefault<Article>("SELECT TOP 1 * FROM Article ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.articlecode.Contains(""))
                                {
                                    var nextNo = Int32.Parse(lastId.articlecode.Substring(2, 10)) + 1;
                                    articlecode = "TT" + String.Format("{0:0000000000}", nextNo);
                                }
                            }
                            else
                            {
                                articlecode = "TT0000000001";
                            }

                            data.articlecode = articlecode;
                            data.allUser = isAdmin ? data.allUser : false;
                            data.createdAt = DateTime.Now;
                            data.createdBy = currentUser.name;
                            data.updatedBy = currentUser.name;
                            data.imagesPublicId = null;
                            dbConn.Insert(data);
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@articlecode", data.articlecode));
                            param.Add(new SqlParameter("@type", data.type));
                            param.Add(new SqlParameter("@name", data.name));
                            param.Add(new SqlParameter("@description", data.description));
                            param.Add(new SqlParameter("@content", data.content));
                            param.Add(new SqlParameter("@category", string.IsNullOrEmpty(data.category) ? "" : data.category));
                            param.Add(new SqlParameter("@imagesPublicId", string.IsNullOrEmpty(data.imagesPublicId) ? "" : data.imagesPublicId));
                            //param.Add(new SqlParameter("@imagesSize", data.imagesSize));
                            param.Add(new SqlParameter("@active", data.active));
                            param.Add(new SqlParameter("@createdBy", data.createdBy));
                            param.Add(new SqlParameter("@updatedBy", data.updatedBy));
                            param.Add(new SqlParameter("@allUser", data.allUser));
                            param.Add(new SqlParameter("@isDefault", data.isDefault));
                            param.Add(new SqlParameter("@companycode", data.companycode));
                            new SqlHelper().ExecuteQuery("p_Article_SyncToMySQL", param);

                            int Id = (int)dbConn.GetLastInsertId();
                            data.id = Id;
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                                if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                                {
                                    //string publicId = "SaiGonFord/Article/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                                    //data.imagesPublicId = publicId;
                                    //var imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                                    //data.imagesSize = imagesSize;
                                    data.imagesPublicId = new AzureHelper().UploadImageToAzure("Article", file, currentUser.name);
                                    param = new List<SqlParameter>();
                                    param.Add(new SqlParameter("@articlecode", data.articlecode));
                                    param.Add(new SqlParameter("@type", data.type));
                                    param.Add(new SqlParameter("@name", data.name));
                                    param.Add(new SqlParameter("@description", data.description));
                                    param.Add(new SqlParameter("@content", data.content));
                                    param.Add(new SqlParameter("@category", string.IsNullOrEmpty(data.category) ? "" : data.category));
                                    param.Add(new SqlParameter("@imagesPublicId", string.IsNullOrEmpty(data.imagesPublicId) ? "" : data.imagesPublicId));
                                    //param.Add(new SqlParameter("@imagesSize", data.imagesSize));
                                    param.Add(new SqlParameter("@active", data.active));
                                    param.Add(new SqlParameter("@createdBy", data.createdBy));
                                    param.Add(new SqlParameter("@updatedBy", data.updatedBy));
                                    param.Add(new SqlParameter("@allUser", data.allUser));
                                    param.Add(new SqlParameter("@isDefault", data.isDefault));
                                    param.Add(new SqlParameter("@companycode", data.companycode));
                                    param.Add(new SqlParameter("@hashtagcode", data.hashtagcode));
                                    new SqlHelper().ExecuteQuery("p_Article_SyncToMySQL", param);
                                    //dbConn.Update(data);
                                }
                                else
                                {
                                    return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                                }
                            }
                            //dbTrans.Commit();
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

        public ActionResult deleteArticle(Int64 id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@id", id));
                    new SqlHelper().ExecuteQuery("p_Article_Delete_SyncToMySQL", param);
                    //dbConn.Delete<Article>("id={0}", id);
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
    }
}