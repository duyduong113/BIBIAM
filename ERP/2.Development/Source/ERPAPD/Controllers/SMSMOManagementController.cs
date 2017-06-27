using ServiceStack.OrmLite;
using System;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;


namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SMSMOManagementController : CustomController
    {
        //
        // GET: /SMSMOManagement/
        public ActionResult Index()
        {
            if (asset.View)
            {
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_SMSMO>(request);
                }
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult ChangeStatusDone(int Id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<Deca_SMSMO>("Id={0}", Id);
                    if (data != null)
                    {
                        if (data.Status == "done" || data.Status == "cancelled")
                        {
                            return Json(new { success = "false", error = "Đổi trạng thái thất bại." });
                        }

                        data.Status = "done";
                        data.UpdatedAt = DateTime.Now;
                        data.UpdatedBy = currentUser.UserName;
                        dbConn.Update(data);
                    }
                    return Json(new {success = "true" });
                }
            }
            catch (Exception e)
            {
                return Json(new {success = "false",error = e.Message });
            }
        }

        public ActionResult ChangeStatusProcessing(int Id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<Deca_SMSMO>("Id={0}", Id);
                    if (data != null)
                    {
                        if (data.Status == "done")
                        {
                            return Json(new { success = "false", error = "Đổi trạng thái thất bại." });
                        }
                        data.Status = "processing";
                        data.UpdatedAt = DateTime.Now;
                        data.UpdatedBy = currentUser.UserName;
                        dbConn.Update(data);
                    }
                    return Json(new { success = "true" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = "false", error = e.Message });
            }

        }

        public ActionResult ChangeStatusCancelled(int Id, string Description)
        {
            try
            {
                if (String.IsNullOrEmpty(Description))
                {
                    return Json(new { success = "false", error = "Vui lòng nhập lý do" });
                }
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<Deca_SMSMO>("Id={0}", Id);
                    if (data.Status == "done" || data.Status == "cancelled")
                    {
                        return Json(new { success = "false", error = "Đổi trạng thái thất bại." });
                    }
                    if (data != null)
                    {
                        data.CancelledReason = Description;
                        data.Status = "cancelled";
                        data.UpdatedAt = DateTime.Now;
                        data.UpdatedBy = currentUser.UserName;
                        dbConn.Update(data);
                    }
                    return Json(new { success = "true" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = "false", error = e.Message });
            }

        }
    }
}