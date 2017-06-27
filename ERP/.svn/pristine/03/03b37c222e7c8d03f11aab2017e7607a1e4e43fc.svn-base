using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using CRM24H.Models;
using CRM24H.Helpers;

namespace CRM24H.Controllers
{
    public class CSDailyNewViewController : CustomController
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Customer/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //this.parentAsset = Asset.GetAsset(1);
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] =asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                ViewBag.Content = dbConn.Select<CRM_CS_Daily_New>("select top(1) * from CRM_CS_Daily_New order by RowCreatedTime DESC ").ToList();
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult CSDailyNewView_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<CRM_CS_Daily_New>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where " + where + " AND Hot = 0 AND [Status] = 1 order by Id DESC");
                    }
                    else
                    {
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New where Hot = 0 AND [Status] = 1 order by Id DESC ");
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult CSDailyNewView_ReadHot([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<CRM_CS_Daily_New>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where " + where + " AND Hot = 1 AND [Status] = 1 order by Id DESC");
                    }
                    else
                    {
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New where Hot = 1 AND [Status] = 1 order by Id DESC ");
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult View(int Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                //check update rights for current controller
                if (asset.Update)
                {
                    var hOld = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where Id = " + Id).FirstOrDefault();
                    if (hOld != null)
                    {
                        return View(hOld);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        public ActionResult CSDailyNewView_ReadAll([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<CRM_CS_Daily_New>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where " + where + " AND [Status] = 1 order by Id DESC");
                    }
                    else
                    {
                        data = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New where [Status] = 1 order by Id DESC ");
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Detail(int Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                //check update rights for current controller
                if (asset.Update)
                {
                    //chet update rights for selected record
                    var hOld = dbConn.Select<CRM_CS_Daily_New>("SELECT * FROM CRM_CS_Daily_New Where Id = " + Id).FirstOrDefault();
                    if (hOld != null)
                    {
                        return Json(hOld);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
    }
}
