using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class FailedVietinTransactionController : CustomController
    {
        //
        // GET: /FailedVietinTransaction/
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Assets = asset;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listError = dbConn.Select<Deca_Vietin_Error_Code>();
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_SignedVietinReponse_Log>();
                if (asset.View)
                {
                    data = dbConn.Select<DC_SignedVietinReponse_Log>().Where(s => s.log.decision == "DECLINE" || s.log.decision == "ERROR").ToList();
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}