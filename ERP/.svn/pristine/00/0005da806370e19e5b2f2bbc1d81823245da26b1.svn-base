using MCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Kendo.Mvc.UI;
using System.Data;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using MCC.Controllers;
using MCC.Filters;
using MCC.Helpers;
using Kendo.Mvc.Extensions;
using System.Text.RegularExpressions;
using System.Text;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class MerchantManagementController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {

                ViewBag.isAdmin = isAdmin;
                return View();

            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {

            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    if (isAdmin)
                        return Json(dbConn.Select<Merchant_Info>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult AuthMerchant(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {

                string[] separators = { "," };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length == 0)
                {
                    return Json(new { success = false, message = "Chọn gian hàng cần xác thực!" });
                }
                string st = new Merchant_Info_DAO().ChangeStatusAuth(ids, AppConfigs.MCCConnectionString, currentUser.name);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công!" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Xác thực không thành công! " });
        }
        public ActionResult ApprovalMerchant(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {

                string[] separators = { "," };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length == 0)
                {
                    return Json(new { success = false, message = "Chọn gian hàng cần xét duyệt!" });
                }
                string st = new Merchant_Info_DAO().ChangeStatusApproval(ids, AppConfigs.MCCConnectionString, currentUser.name);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công!" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Xét duyệt không thành công! " });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Merchant_Info item, HttpPostedFileBase file)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                string rs = new Merchant_Info_DAO().Update(item, currentUser.name, AppConfigs.MCCConnectionString);
                if (rs == "true")
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        if(file!=null)
                        {
                            item.logo_gian_hang = new Helpers.ProcessImage().UploadImageToFolder(string.Empty, file, item.ma_gian_hang, currentUser.name);
                            dbConn.UpdateOnly(item,
                                            onlyFields: p =>
                                            new
                                            {
                                                p.logo_gian_hang,
                                            },
                                             where: p => p.id == item.id);
                        }
                       
                        
                        var data = dbConn.FirstOrDefault<Merchant_Info>("ma_gian_hang={0}".Params(item.ma_gian_hang));
                        return Json(new { success = true, data = data });
                    }
                }
                return Json(new { success = false, message = rs, JsonRequestBehavior.AllowGet });
            }
            return RedirectToAction("NoAccess", "Error");
        }
       
    }
}