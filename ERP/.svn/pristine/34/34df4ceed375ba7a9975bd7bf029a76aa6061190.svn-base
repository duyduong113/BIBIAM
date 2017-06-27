using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    public class CRMPlanKpiController : CustomController
    {
        // GET: CRMPlanKpi
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        ViewData["AllowCreate"] = asset.Create;
                        ViewData["AllowUpdate"] = asset.Update;
                        ViewData["AllowDelete"] = asset.Delete;
                        ViewData["AllowExport"] = asset.Export;
                        ViewBag.listKpi = dbConn.Select<Parameters>(s => s.Type == "Kpi");
                        ViewBag.listYear = dbConn.Select<CRM_Calendar>("SELECT  DISTINCT TOP 10 Year FROM [ERPAPDDev].[Auxiliary].[Calendar] ORDER BY Year DESC");
                        ViewBag.listMonth = dbConn.Select<CRM_Calendar>("SELECT DISTINCT Month FROM [ERPAPDDev].[Auxiliary].[Calendar]").OrderBy(s => s.Month);
                        ViewBag.listWeek = dbConn.Select<CRM_Calendar>("SELECT DISTINCT Week FROM [ERPAPDDev].[Auxiliary].[Calendar]").OrderBy(s => s.Week);
                        ViewBag.listQuarter = dbConn.Select<CRM_Calendar>("SELECT DISTINCT Quarter FROM [ERPAPDDev].[Auxiliary].[Calendar]").OrderBy(s => s.Quarter);
                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                }

                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT kpi.* FROM [CRM_PlanKPI] kpi ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_PlanKPI>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_PlanKPI> listEx)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var pr in listEx)
                            {

                                if (string.IsNullOrEmpty(pr.Kpi))
                                {
                                    ModelState.AddModelError("", "Vui nhập Kpi");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                pr.CreatedAt = DateTime.Now;
                                pr.CreatedBy = currentUser.UserName;
                                dbConn.Insert(pr);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_PlanKPI> listEx)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var pr in listEx)
                            {
                                if (string.IsNullOrEmpty(pr.Kpi))
                                {
                                    ModelState.AddModelError("", "Vui nhập Kpi");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                pr.UpdatedAt = DateTime.Now;
                                pr.UpdatedBy = currentUser.UserName;
                                dbConn.Update(pr);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            //var check = dbConn.FirstOrDefault<CRM_PlanKPI>("Id={0}", item);
                            dbConn.Delete<CRM_PlanKPI>("Id={0}", item);
                        }
                    }
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
    }
}