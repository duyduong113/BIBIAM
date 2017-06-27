using System;
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
    [CustomActionFilter(permission = "all,view,update,delete,export")]
    public class HierarchyController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    //ViewBag.listAll = db.Select<Hierarchy>();
                    //ViewBag.listAll = dbConn.GetDictionary<string, string>("Select ma_phan_cap as Value , ten_phan_cap as Name from Hierarchy").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });

                    ViewBag.listAll = dbConn.Select<Code_Master_Json>("Select isnull(ma_phan_cap,'') as Value , isnull(ten_phan_cap,'') as Name from Hierarchy");


                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    ViewBag.listStatus = dbConn.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'HierarchyStatus' Order By order_Num").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                    ViewBag.listHierarchyType = dbConn.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'HierarchyType' Order By order_Num").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });

                }

                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        #region Read_Action

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {

                var data = new Hierarchy_DAO().ReadAll(AppConfigs.MCCConnectionString);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        #endregion


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Hierarchy> listrow)
        {
            ModelState.Clear(); // phải clear
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {

                string st = new Hierarchy_DAO().UpSert(listrow.ToList(), currentUser.name, "Insert", AppConfigs.MCCConnectionString);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);

                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Create");
            return Json(new List<Hierarchy>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Hierarchy> listrow)
        {
            ModelState.Clear(); // phải clear
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {

                string st = new Hierarchy_DAO().UpSert(listrow.ToList(), currentUser.name, "Update", AppConfigs.MCCConnectionString);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);


                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Update");
            return Json(new List<Hierarchy>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult Delete(string data)
        {
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {

                string[] separators = { "," };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if(ids.Length==0)
                {
                    return Json(new { success = false, message = "Chọn mã phân cấp cần xóa!" });
                }
                string st = new Hierarchy_DAO().Delete(ids, AppConfigs.MCCConnectionString);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Bạn " });
        }
        public ActionResult GetHierarchyInfo()
        {
            using (var dbConn = OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT ma_phan_cap as id, isnull(ten_phan_cap,'') as name FROM Hierarchy").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getHirerachyParent(int cap, string loai_phan_cap, string ma_phan_cap)
        {
           IDbConnection db = OrmliteConnection.openConn();
            try
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = new List<Code_Master_Json>();
                      data = dbConn.Select<Code_Master_Json>(@"Select isnull(ma_phan_cap,'') as Value , isnull(ten_phan_cap,'') as Name from Hierarchy where cap={0} and loai_phan_cap={1} and ma_phan_cap != {2}".Params((cap - 1),loai_phan_cap, ma_phan_cap));
                    return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

    }
}