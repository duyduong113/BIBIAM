using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Globalization;
using System.Web;
using System.Data.OleDb;
using System.Dynamic;
using OfficeOpenXml;
using ERPAPD.Helpers;
using System.Threading.Tasks;
using DevTrends.MvcDonutCaching;
using System.Xml.Linq;
using Hangfire;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Configuration;

namespace ERPAPD.Controllers
{
    public class TelesalesPromotionInfoController : CustomController
    {

        //
        // GET: /Customer/

        //
        // GET: /TelesalePluginCode/

        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(DC_Telesales_PromotionInfo), typeof(DC_Telesales_PromotionInfo));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(DC_Telesales_PromotionInfo), typeof(DC_Telesales_PromotionInfo));
            //}
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
                    ViewData["listComp"] = dbConn.Select<Deca_Company>();

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
            using (var dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.Create)
                {
                    ViewData["listComp"] = dbConn.GetFirstColumn<string>("SELECT ShortName from Deca_Company");
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
                    //chet update rights for selected record
                    var hOld = dbConn.Select<DC_Telesales_PromotionInfo>("SELECT * FROM DC_Telesales_PromotionInfo Where ID = " + Id).FirstOrDefault();
                    if (hOld != null)
                    {
                        ViewData["listComp"] = dbConn.GetFirstColumn<string>("SELECT ShortName from Deca_Company");
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
        public ActionResult Create(DC_Telesales_PromotionInfo article, string filepath)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create && ModelState.IsValid)
                {
                    try
                    {
                        //var formats = new[] { "MM/dd/yyyy HH:mm", "MM-dd-yyyy HH:mm" };
                        article.CreatedAt = DateTime.Now;
                        article.CreatedBy = currentUser.UserName;
                        //article.StartDate = DateTime.Parse("1900-01-01");
                        //article.EndDate = DateTime.Parse("1900-01-01");
                        article.UpdatedAt = DateTime.Now;
                        article.UpdatedBy = currentUser.UserName;
                        article.Thumnail = !string.IsNullOrEmpty(filepath) ? filepath : "";
                        //string StartDateTime = article.StartDate.ToString(); ;
                        dbConn.Insert(article);
                        dbTrans.Commit();
                        return Json(new { error = false, message = "Thành công" });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = ex.Message });
                    }
                }
                else
                {
                    if (!asset.Create) return Json(new { error = true, message = "Không có quyền tạo mới." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(DC_Telesales_PromotionInfo article, string filepath)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update && ModelState.IsValid)
                {
                    try
                    {
                        var CheckExit = dbConn.FirstOrDefault<DC_Telesales_PromotionInfo>("ID =" + article.ID);
                        if (CheckExit == null)
                        {
                            return Json(new { error = true, message = "Không tìm thấy." });
                        }
                        article.CreatedAt = CheckExit.CreatedAt;
                        article.CreatedBy = CheckExit.CreatedBy;
                        article.UpdatedAt = DateTime.Now;
                        article.UpdatedBy = currentUser.UserName;
                        article.Thumnail = !string.IsNullOrEmpty(article.Thumnail) ? article.Thumnail : "";
                        dbConn.Update(article);
                        dbTrans.Commit();
                        return Json(new { error = false, message = "Thành công." });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = ex.Message });
                    }
                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền tạo mới." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
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
                        var delete = new DC_Telesales_PromotionInfo();
                        foreach (var item in listRowID)
                        {
                            delete.ID = Int32.Parse(item);
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
                        var delete = new DC_Telesales_PromotionInfo();
                        delete.ID = data;
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
                    return Json(KendoApplyFilter.KendoData<DC_Telesales_PromotionInfo>(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult DailyNew_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Telesales_PromotionInfo> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            var data = new DC_Telesales_PromotionInfo();
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
                                data.IsActive = item.IsActive;
                                data.IsAll = item.IsAll;
                                data.CreatedAt = DateTime.Now;
                                data.CreatedBy = currentUser.UserName;
                                dbConn.Insert(data);
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
        public ActionResult DailyNew_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Telesales_PromotionInfo> list)
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
                                item.IsActive = item.IsActive;
                                item.IsAll = item.IsAll;
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
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
                            dbConn.Update<DC_Telesales_PromotionInfo>(set: "[IsActive]  = {0}".Params(false));
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
