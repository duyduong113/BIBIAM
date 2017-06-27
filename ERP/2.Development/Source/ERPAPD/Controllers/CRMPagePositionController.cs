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
    public class CRMPagePositionController : CustomController
    {
        // GET: CRMContractExtra
        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listSize = dbConn.Select<Parameters>("Type ={0}", "SIZETYPE").OrderByDescending(s => s.ParamID);
                ViewBag.listShareType = dbConn.Select<Parameters>("Type ={0}", "SHARE_TYPE").OrderByDescending(s => s.ParamID);
                ViewBag.listWebsite = dbConn.Select<CRM_Website>().OrderBy(s => s.WebsiteID);
            }
            return View();
        }
        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<CRM_PagePosition> list)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Create || asset.Update)
            {
                if (list != null)
                {
                    try
                    {
                        foreach (var item in list)
                        {
                            var exits = dbConn.SingleOrDefault<CRM_PagePosition>("PositionID= {0}", item.PositionID);
                            if (exits == null)
                            {
                                var row = new CRM_PagePosition();
                                row.PositionName = !string.IsNullOrEmpty(item.PositionName) ? item.PositionName.Trim() : "";
                                row.Size = !string.IsNullOrEmpty(item.Size) ? item.Size.Trim() : "";
                                row.FileSize = !(item.FileSize == 0) ? item.FileSize : 0;
                                row.ShareNumber = !string.IsNullOrEmpty(item.ShareNumber) ? item.ShareNumber.Trim() : "";
                                row.Status = item.Status;
                                row.WebsiteRefID = item.WebsiteRefID;
                                row.CreatedBy = currentUser.UserName;
                                row.UpdatedBy = "";
                                row.CreatedAt = DateTime.Now;
                                row.UpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                            }
                            else {
                                exits.PositionName = !string.IsNullOrEmpty(item.PositionName) ? item.PositionName.Trim() : "";
                                exits.Size = !string.IsNullOrEmpty(item.Size) ? item.Size.Trim() : "";
                                exits.FileSize = !(item.FileSize == 0) ? item.FileSize : 0;
                                exits.ShareNumber = !string.IsNullOrEmpty(item.ShareNumber) ? item.ShareNumber.Trim() : "";
                                exits.Status = item.Status;
                                exits.WebsiteRefID = item.WebsiteRefID;
                                exits.UpdatedBy = currentUser.UserName;
                                exits.UpdatedAt = DateTime.Now;
                                dbConn.Update(exits);
                            }
                        }
                        ModelState.AddModelError("Thành công!", "Lưu thành công.");
                        return Json(new { sussess = true });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("error", ex.Message);
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                }
                return Json(new { sussess = true });
            }

            else
            {
                ModelState.AddModelError("error", "Bạn không có quyền cập nhật.");
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
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var delete = new CRM_PagePosition();
                        foreach (var item in listRowID)
                        {
                            delete.PositionID = Int32.Parse(item);
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
        public ActionResult CRM_PagePosition_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT p.*,
                   (SELECT value FROM Parameters WHERE ParamID = p.ShareNumber) AS ShareName,
                   (SELECT value FROM Parameters WHERE ParamID = p.Size) AS SizeName,
				   (Select WebsiteName From CRM_Website Where RefID = p.WebsiteRefID) AS WebsiteName
                   FROM CRM_PagePosition p";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_PagePosition>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
    }
}