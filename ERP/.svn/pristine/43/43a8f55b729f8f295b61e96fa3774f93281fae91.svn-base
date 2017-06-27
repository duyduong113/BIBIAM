using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;


namespace ERPAPD.Controllers
{
    public class Product_HierarchyController : CustomController
    {
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Create = asset.Create;
                ViewBag.Update = asset.Update;
                ViewBag.Delete = asset.Delete;
                ViewBag.Export = asset.Export;
                ViewBag.listWebsite = CRM_Website.GetList();
                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    ViewBag.listHierarchy1 = db.Select<Hierarchy>(c => c.cap == 1).OrderBy(s => s.id);
                    ViewBag.listHierarchy2 = db.Select<Hierarchy>(c => c.cap == 2).OrderBy(s => s.id);
                    ViewBag.listHierarchy3 = db.Select<Hierarchy>(c => c.cap == 3).OrderBy(s => s.id);
                    ViewBag.listHierarchy4 = db.Select<Hierarchy>(c => c.cap == 4).OrderBy(s => s.id);
                    ViewBag.listHierarchy5 = db.Select<Hierarchy>(c => c.cap == 5).OrderBy(s => s.id);
                    ViewBag.listHierarchy6 = db.Select<Hierarchy>(c => c.cap == 6).OrderBy(s => s.id);
                    ViewBag.listHierarchy7 = db.Select<Hierarchy>(c => c.cap == 7).OrderBy(s => s.id);
                    ViewBag.listHierarchy8 = db.Select<Hierarchy>(c => c.cap == 8).OrderBy(s => s.id);
                    ViewBag.listHierarchy9 = db.Select<Hierarchy>(c => c.cap == 9).OrderBy(s => s.id);
                    ViewBag.listHierarchy10 = db.Select<Hierarchy>(c => c.cap == 10).OrderBy(s => s.id);
                    ViewBag.listStatus = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "ProductHierarchyStatus").OrderBy(s => s.ID);
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new Product_Hierarchy_DAO().ReadAll();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upsert([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Product_Hierarchy> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Update)
            {

                string st = new Product_Hierarchy_DAO().UpSert(listrow.ToList(), currentUser.UserName, "Update");
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);
                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Update");
            return Json(new List<Product_Hierarchy>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult GetHierarchyByLevel(string cap, string ma_phan_cap_cha)
        {
            if (asset.View)
            {
                using (IDbConnection db = OrmliteConnection.openConn())
                {

                    var data = db.Select<Hierarchy>("select id, ma_phan_cap, ten_phan_cap, ma_phan_cap_cha from Hierarchy where cap = '" + cap + "' and ma_phan_cap_cha = '" + ma_phan_cap_cha + "'");
                    if (data == null)
                    {
                        data.Add(new Hierarchy());
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền." });
        }
    }
}
