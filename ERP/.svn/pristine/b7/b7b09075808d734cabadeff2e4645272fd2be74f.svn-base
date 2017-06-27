using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    public class CRMContractExtraController : CustomController
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
                ViewBag.listContractTerms = dbConn.Select<Parameters>("Type ={0}", "ContractDraffType").OrderBy(s => s.ParamID);
            }
            return View();
        }
        public ActionResult Create(int Id = 0)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listContractTerms = dbConn.Select<Parameters>("Type ={0}", "ContractDraffType").OrderBy(s => s.ParamID);
                if (Id != 0)
                {
                    ViewBag.rowItem = dbConn.SingleOrDefault<CRM_Contract_Extra>("RowID= {0}", Id);
                    if (asset.Update)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NoAccessRights", "Error");
                    }
                }
                else
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
            }
           
        }
        [ValidateInput(false)]
        public ActionResult Save_item(CRM_Contract_Extra req)
        {
            var rs = CRM_Contract_Extra.Save(req, currentUser.UserName);
            return Json(rs);
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
                        var delete = new CRM_Contract_Extra();
                        foreach (var item in listRowID)
                        {
                            delete.RowID = Int32.Parse(item);
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
        public ActionResult Contract_Extra_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM CRM_Contract_Extra";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Contract_Extra>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
    }
}