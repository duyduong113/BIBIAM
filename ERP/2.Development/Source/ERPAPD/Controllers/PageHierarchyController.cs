using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;

namespace ERPAPD.Controllers
{
    public class PageHierarchyController : CustomController
    {
        // GET: PageHierarchy
       
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Create = asset.Create;
                ViewBag.Update = asset.Update;
                ViewBag.Delete = asset.Delete;
                ViewBag.Export = asset.Export;
                ViewBag.listWebsite = CRM_Website.GetList();
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
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    var data = CRM_Page.ReadAll();
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        #endregion
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Page> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Create)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    foreach (var row in listrow)
                    {
                        if (string.IsNullOrEmpty(row.PageName))
                        {
                            ModelState.Clear();
                            ModelState.AddModelError("", "Vui lòng nhập tên trang");
                            continue;
                        }
                        if (string.IsNullOrEmpty(row.WebsiteID))
                        {
                            ModelState.Clear();
                            ModelState.AddModelError("", "Vui lòng chọn website");
                            continue;
                        }
                        row.CreatedAt = DateTime.Now;
                        row.CreatedBy = currentUser.UserName;
                        row.UpdatedAt = DateTime.Now;
                        row.UpdatedBy = currentUser.UserName;
                        dbConn.Save(row);
                    }
                }
                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Create");
            return Json(new List<CRM_Page>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Page> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Update)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    foreach (var row in listrow)
                    {
                        row.UpdatedAt = DateTime.Now;
                        row.UpdatedBy = currentUser.UserName;
                        dbConn.Update(row);
                    }
                }
                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Update");
            return Json(new List<CRM_Page>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeletePage(string data)
        {
            if (asset.Delete)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    //var ids = listid.Split(',');
                    foreach (var id in ids)
                    {
                        CRM_Page.Delete(id);
                    }
                    return Json(new { success = true, message = "Thành công" });
                }
            }
            return Json(new { success = false, message = "Bạn " });
        }
    }
}