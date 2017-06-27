using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;
namespace ERPAPD.Controllers
{
    public class TerritoryController : Controller
    {
        //
        // GET: /Territory/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRegion()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Region").OrderBy(a => a.TerritoryName), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCity()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDistrict(string CityID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<DC_OCM_Territory>("[Level] = {0} AND [ParentID] = {1}", "District", CityID).OrderBy(a => a.TerritoryName), JsonRequestBehavior.AllowGet);
            }
        }
    }
}