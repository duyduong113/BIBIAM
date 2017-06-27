using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    [Authorize]
    public class CRM_Debit_ManagementController : CustomController
    {
        //
        // GET: /SurveyManagement/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(CRM_Survey_Management), typeof(CRM_Survey_Management_Customer), typeof(CRM_Survey_Management_Question));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(CRM_Survey_Management_Customer), typeof(CRM_Survey_Management_Question), typeof(CRM_Survey_Management));
            //}

            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.View)
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var dbConn = Helpers.OrmliteConnection.openConn();
                string whereCondition = "";
                if (request.Filters.Count > 0)
                {
                    whereCondition = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                }
                var data = dbConn.Select<CRM_Debit_Management>(whereCondition);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Debit_Management> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null)
                        {
                            foreach (var item in list)
                            {
                                var checkColumName = dbConn.Select<CRM_Debit_Management>("[Title] = {0}", item.Title).FirstOrDefault();
                                if (checkColumName != null)
                                {
                                    ModelState.AddModelError("", "Tiêu đề đã có");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                dbConn.Insert(item);
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
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền tạo");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request));
                }
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  
            IEnumerable<CRM_Debit_Management> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null)
                        {
                            foreach (var item in list)
                            {
                                //
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
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
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
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listdata)
                        {
                            dbConn.Delete<CRM_Debit_Management>(where: "Id={0}".Params(item));
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền xóa." });
                }
        }


    }
}