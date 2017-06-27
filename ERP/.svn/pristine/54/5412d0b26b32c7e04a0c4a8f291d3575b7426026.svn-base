using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Globalization;
using Hangfire;
using ServiceStack.OrmLite;
using CRM24H.Models;
using CRM24H.Helpers;

namespace CRM24H.Controllers
{
    public class CSDailyNewsController : CustomController
    {

        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Customer/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //this.parentAsset = Asset.GetAsset(1);
            base.Initialize(requestContext);
        }

        //
        // GET: /TelesalePluginCode/

        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.User = dbConn.Select<Users>();
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create()
        {
            if (asset.Create)
            {
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Edit(int Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                //check update rights for current controller
                if (asset.Update)
                {
                    var hOld = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where Id = " + Id).FirstOrDefault();
                    if (hOld != null)
                    {
                        return View(hOld);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(CRM_CS_Daily_New article, string task, string filepath, string txtStartDate, string txtEndDate)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        //var formats = new[] { "MM/dd/yyyy HH:mm", "MM-dd-yyyy HH:mm" };
                        if (String.IsNullOrEmpty(article.Title))
                        {
                            return Json(new { success = false, error = "Please input Title" });
                        }

                        if (String.IsNullOrEmpty(article.Description))
                        {
                            return Json(new { success = false, error = "Please input Description" });
                        }
                        if (String.IsNullOrEmpty(article.Content))
                        {
                            return Json(new { success = false, error = "Please input Content" });
                        }
                        article.Title = article.Title.Trim();
                        article.Description = article.Description.Trim();
                        article.Content = article.Content;
                        article.Hot = article.Hot;
                        article.Status = article.Status;
                        article.RowCreatedTime = DateTime.Now;
                        article.RowCreatedUser = currentUser.UserName;
                        article.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                        //article.StartDate = DateTime.Parse("1900-01-01");
                        //article.EndDate = DateTime.Parse("1900-01-01");
                        article.RowLastUpdatedUser = "";
                        article.Thumnail = !string.IsNullOrEmpty(filepath) ? filepath : "";
                        if (!string.IsNullOrEmpty(article.txtStartDate))
                        {
                            article.StartDate = DateTime.Parse(article.txtStartDate);
                        }
                        else
                        {
                            return Json(new { error = "Please check Start Date Time" });
                        }
                        if (string.IsNullOrEmpty(article.txtEndDate))
                        {
                            article.EndDate = DateTime.Parse("1900-01-01");
                        }
                        else
                        {
                            if (DateTime.Parse(article.txtStartDate) > DateTime.Parse(article.txtEndDate))
                            {
                                return Json(new { error = "Please check End Date Time > Start Date Time" });
                            }
                            else if (!string.IsNullOrEmpty(article.txtEndDate))
                            {
                                article.EndDate = DateTime.Parse(article.txtEndDate);
                            }
                            else
                            {
                                return Json(new { error = "Please check End Date Time" });
                            }
                        }
                        dbConn.Save(article);
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, error = ex.Message });
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }

            return Json(new { success = true, task = task });
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(CRM_CS_Daily_New article, string filepath, string txtStartDate, string txtEndDate)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        var CheckExit = dbConn.Select<CRM_CS_Daily_New>("SELECT Id, RowCreatedTime, RowCreatedUser FROM  CRM_CS_Daily_New WHERE Id = '" + article.Id + "'").FirstOrDefault();
                        if (String.IsNullOrEmpty(article.Title))
                        {
                            return Json(new { success = false, error = "Please input Title" });
                        }
                        if (String.IsNullOrEmpty(article.Content))
                        {
                            return Json(new { success = false, error = "Please input PostContent" });
                        }
                        if (String.IsNullOrEmpty(article.Content))
                        {
                            return Json(new { success = false, error = "Please input Content" });
                        }
                        article.Title = article.Title.Trim();
                        article.Description = article.Description.Trim();
                        article.Hot = article.Hot;
                        article.Content = article.Content;
                        article.Status = article.Status;
                        article.RowLastUpdatedTime = DateTime.Now;
                        article.RowLastUpdatedUser = currentUser.UserName;
                        article.RowCreatedTime = CheckExit.RowCreatedTime;
                        article.RowCreatedUser = CheckExit.RowCreatedUser;
                        article.Thumnail = !string.IsNullOrEmpty(article.Thumnail) ? article.Thumnail : "";                     
                        if (!string.IsNullOrEmpty(article.txtStartDate))
                        {
                            article.StartDate = DateTime.Parse(article.txtStartDate);                            
                        }
                        else
                        {
                            return Json(new { error = "Please check Start Date Time" });
                        }
                        if (string.IsNullOrEmpty(article.txtEndDate))
                        {
                            article.EndDate = DateTime.Parse("1900-01-01");
                        }
                        else
                        {
                            if (DateTime.Parse(article.txtStartDate) > DateTime.Parse(article.txtEndDate))
                            {
                                return Json(new { error = "Please check End Date Time > Start Date Time" });
                            }
                            else if (!string.IsNullOrEmpty(article.txtEndDate))
                            {
                                article.EndDate = DateTime.Parse(article.txtEndDate);
                            }
                            else
                            {
                                // do for invalid date
                                return Json(new { error = "Please check End Date Time" });
                            }
                        }
                        dbConn.Update(article);
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        return Json(new { error = ex.Message });
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
            return Json(new { success = true });
        }
        public ActionResult Delete(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var delete = new CRM_CS_Daily_New();
                        foreach (var item in listRowID)
                        {
                            delete.Id = Int32.Parse(item);
                            dbConn.Delete(delete);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }
        public ActionResult DeleteId(int data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        var delete = new CRM_CS_Daily_New();
                        delete.Id = data;
                        dbConn.Delete(delete);
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }
        public ActionResult DailyNew_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<CRM_CS_Daily_New>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where " + where + " order by Id DESC");
                    }
                    else
                    {
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New order by Id DESC ");
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult DailyNew_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_CS_Daily_New> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            var data = new CRM_CS_Daily_New();
                            foreach (var item in list)
                            {

                                if (String.IsNullOrEmpty(item.Title))
                                {
                                    ModelState.AddModelError("", "Please input Title");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.Content))
                                {
                                    ModelState.AddModelError("", "Please input Content");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                data.Title = item.Title.Trim();
                                data.Content = item.Content.Trim();
                                data.Status = item.Status;
                                data.RowCreatedTime = DateTime.Now;
                                data.RowCreatedUser = currentUser.UserName;
                                data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                data.RowLastUpdatedUser = "";
                                dbConn.Save(data);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to create record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult DailyNew_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_CS_Daily_New> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            foreach (var item in list)
                            {
                                if (String.IsNullOrEmpty(item.Title))
                                {
                                    ModelState.AddModelError("", "Please input Title");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.Content))
                                {
                                    ModelState.AddModelError("", "Please input Content");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                item.Title = item.Title.Trim();
                                item.Content = item.Content.Trim();
                                item.Status = item.Status;
                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult Inactive(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            dbConn.Update<CRM_CS_Daily_New>(set: "[Status]  = {0}".Params(false));
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("Error", e.Message);
                        return Json(new { success = false });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }
    }
}
