using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MCC.Filters;
using MCC.Helpers;
using MCC.Models;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using CloudinaryDotNet;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIBIAM.Core.Data;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]    
    public class Merchant_Product_HierarchyController : CustomController
    {
        public ActionResult GetByProductID(string ma_san_pham)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    Merchant_Product_Hierarchy data;
                    if (isAdmin)
                        data = dbConn.FirstOrDefault<Merchant_Product_Hierarchy>(s => s.ma_san_pham == ma_san_pham);
                    else
                        data = dbConn.FirstOrDefault<Merchant_Product_Hierarchy>(s => s.ma_san_pham == ma_san_pham && s.ma_gian_hang == currentUser.ma_gian_hang);
                    return Json(new { success = true, data = data, JsonRequestBehavior.AllowGet });
                }
            }
            else return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult GetAll()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = dbConn.SqlList<Merchant_Product_Hierarchy>("exec _p_getMerchantProductHierarchyBy {0}".Params(currentUser.ma_gian_hang));
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else return RedirectToAction("NoAccess", "Error");
        }
    }
}