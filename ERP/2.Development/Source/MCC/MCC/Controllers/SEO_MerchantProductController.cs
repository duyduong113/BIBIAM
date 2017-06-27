using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BIBIAM.Core.Entities;
using MCC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BIBIAM.Core.Data.DataObject;
using CloudinaryDotNet;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using MCC.Helpers;
using MCC.Filters;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class SEO_MerchantProductController : CustomController
    {
        // GET: SEO_MerchantProduct
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            else return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                string sqlQuery = @"select ISNULL(b.id,0) as id, a.ma_san_pham, a.ten_san_pham, a.ma_gian_hang,ISNULL(og_title,'') as og_title, ISNULL(og_description,'') as og_description, ISNULL(og_image,'') as og_image, ISNULL(og_keyword,'') as og_keyword, ISNULL(robot,'') as robot,
                                ISNULL(b.ngay_tao,'') as ngay_tao, ISNULL(b.nguoi_tao,'') as nguoi_tao, ISNULL(b.ngay_cap_nhat,'') as ngay_cap_nhat, ISNULL(b.nguoi_cap_nhat,'') as nguoi_cap_nhat from Merchant_Product a
                                LEFT JOIN [SEO_MerchantProduct] b
                                ON a.ma_san_pham = b.ma_san_pham";
                string whereCondition = string.Empty;
                var data = new DataSourceResult();
                if (isAdmin)
                {
                    data = KendoApplyFilter.KendoDataByQuery<SEO_MerchantProduct>(request, sqlQuery, whereCondition);
                }
                else
                {
                    whereCondition = " ma_gian_hang = {0}".Params(currentUser.ma_gian_hang);
                    data = KendoApplyFilter.KendoDataByQuery<SEO_MerchantProduct>(request, sqlQuery, whereCondition);
                }
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("NoAccess", "Error");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SEO_MerchantProduct> listrow)
        {
            ModelState.Clear(); // phải clear
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                {
                    string strResult = new SEO_MerchantProduct_DAO().UpSert(listrow.ToList(), currentUser.name, AppConfigs.MCCConnectionString);
                    if (strResult == "true")
                        return Json(new { success = true });
                    else
                        return Json(new { success = false, error = strResult });
                }
                else
                {
                    return Json(new { success = false, error = "Don't have permission to update" });
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    error = e.Message
                });
            }
        }
    }
}