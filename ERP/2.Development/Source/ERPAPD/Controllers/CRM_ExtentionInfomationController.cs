using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using ERPAPD.Models;
using ServiceStack.OrmLite;
using ERPAPD.Helpers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;



namespace ERPAPD.Controllers
{
    public class CRM_ExtentionInfomationController : CustomController
    {
        // GET: CRM_ExtentionInfomation
        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listType = dbConn.Select<Parameters>("Type='ExtensionType'").OrderBy(s => s.ParamID);
                ViewBag.listStatus = dbConn.Select<Parameters>("Type='ExtensionStatus'").OrderBy(s => s.ParamID);
            }
            return View();
        }
        public ActionResult ExtsRead([DataSourceRequest] DataSourceRequest request)
        {
            string strQuery = @"SELECT * FROM [CRM_ExtsInfo]";
            return Json(KendoApplyFilter.KendoDataByQuery<CRM_ExtsInfo>(request, strQuery, ""));
        }
        public ActionResult CreateExts([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_ExtsInfo> listItem)
        {
            var rs = CRM_ExtsInfo.Save(listItem, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult DeleteItem(string data)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var detail = new CRM_ExtsInfo();
                    foreach (var item in listdata)
                    {
                        if (dbConn.Select<CRM_ExtsInfo>(s => s.RowID == int.Parse(item)).Count() > 0)
                        {
                           var ext_row = dbConn.SingleOrDefault<CRM_ExtsInfo>("RowID= {0}", item);
                            dbConn.Delete<CRM_ExtsInfo_Meta>(where: "FactorID = '" + ext_row.IDName + "'");
                            var success = dbConn.Delete<CRM_ExtsInfo>(where: "RowID = '" + item + "'") >= 1;
                            if (!success)
                            {
                                return Json(new { success = false, message = "Không thể lưu" });
                            }
                        }
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền xóa." });
            }
        }
    }
}