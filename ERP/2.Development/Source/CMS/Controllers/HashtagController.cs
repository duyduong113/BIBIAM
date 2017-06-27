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
using Kendo.Mvc.Extensions;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class HashtagController : CustomController
    {
        // GET: Hashtag
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (IDbConnection dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    return Json(dbConn.Select<Hashtag>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }

        [HttpPost]
        public ActionResult CreateUpdate(Hashtag data)
        {
            try
            {
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {
                            var exist = dbConn.SingleOrDefault<Hashtag>("id={0}", data.id);
                            data.hashtagcode = exist.hashtagcode;
                            data.updated_by = currentUser.name;
                            data.created_at = exist.created_at;
                            data.updated_at = DateTime.Now;
                            data.updated_by = currentUser.name;

                            dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.hashtagname,
                                            p.status,
                                            p.updated_at,
                                            p.updated_by
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

                            var lastId = dbConn.FirstOrDefault<Hashtag>("SELECT TOP 1 * FROM Hashtag ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.hashtagcode.Contains("HT"))
                                {
                                    var nextNo = Int32.Parse(lastId.hashtagcode.Substring(2, 10)) + 1;
                                    data.hashtagcode = "HT" + String.Format("{0:0000000000}", nextNo);
                                }
                            }
                            else
                            {
                                data.hashtagcode = "HT" + "0000000001";
                            }

                            data.created_at = DateTime.Now;
                            data.created_by = currentUser.name;
                            data.updated_at = DateTime.Now;
                            data.updated_by = currentUser.name;
                            dbConn.Insert(data);
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to create" });
                        }
                    }
                    //List<SqlParameter> lstParameter = new List<SqlParameter>();
                    //lstParameter.Clear();
                    //lstParameter.Add(new SqlParameter("@hashtagcode", data.hashtagcode));
                    //new SqlHelper().ExecuteNoneQuery("p_Hashtag_SyncToMySQL", lstParameter);
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
        public ActionResult deleteHashtag(Int64 id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                    {
                        var exists = dbConn.FirstOrDefault<Hashtag>("id={0}".Params(id));
                        if (exists == null)
                        {
                            return Json(new { success = false, error = "Xóa thất bại!" });
                        }

                        dbConn.Delete<Hashtag>("id={0}".Params(id));
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
                return Json(new { success = false, error = "Bạn không có quyền xóa" });
            }
        }
        public ActionResult Activehashtag(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                try
                {
                    string[] separators = { "," };
                    string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (ids.Length == 0)
                    {
                        return Json(new { success = false, message = "Chọn các Hashtag cần kích hoạt!" });
                    }

                    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<Hashtag>("id={0}".Params(id));
                            dbConn.UpdateOnly(new Hashtag { status = exists.status == "DANG_SU_DUNG" ? "KHONG_SU_DUNG" : "DANG_SU_DUNG", updated_by = currentUser.name, updated_at = DateTime.Now }, onlyFields: p => new { p.status, p.updated_by, p.updated_at }, where: p => p.id == int.Parse(id));
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
