using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    public class LabelController : CustomController
    {
        // GET: Label
        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            return View();
        }

        public ActionResult Label_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM CRM_Label";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Label>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Label_Save(CRM_Label label)
        {
            var CRM_Label = new CRM_Label();
            var rs = CRM_Label.Save(label, currentUser.UserName);
            return Json(rs);
            //return Json(new { success = true});
        }
        public ActionResult Label_Delete(string data)
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
                            var check = dbConn.FirstOrDefault<CRM_Label>("RowID={0}", item);
                            dbConn.Delete(check);
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

        public ActionResult Update_Color(string RowID, string Value) {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                if (RowID != null)
                {
                    try {
                        var exits = dbConn.SingleOrDefault<CRM_Label>("RowID= {0}", RowID);
                        exits.Value = !string.IsNullOrEmpty(Value) ? Value.Trim() : "";
                        exits.RowUpdatedUser = currentUser.UserName;
                        exits.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(exits);
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, message = e.Message });
                    }
                    return Json(new { success = true });

                }
                else
                {
                    return Json(new { success = false, message = "No rowID" });

                }
            }
        }
    }
}