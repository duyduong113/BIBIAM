using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AssetsController : CustomController
    {
        //
        // GET: /Assets/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewData["listGroup"] = dbConn.Select<Groups>("Active = {0}", true).Select(s => new GroupViewModel { Id = s.Id, Name = s.Name });
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Assets>();
                if (asset.View)
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Query<Assets>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Assets>().ToList();
                    }
                }

                return Json(data.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Assets> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            item.CreatedAt = DateTime.Now;
                            item.CreatedBy = User.Identity.Name;
                            dbConn.Insert(item);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }


            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Assets> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = User.Identity.Name;
                            dbConn.Update(item);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult getListGroup()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Groups>("Active={0}", true).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateViews(int AssetID, List<int> data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    asset.View = data;
                    asset.UpdatedBy = currentUser.UserName;
                    asset.UpdatedAt = DateTime.Now;
                    dbConn.Update(asset);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }

            }
        }

        public ActionResult UpdateCreates(int AssetID, List<int> data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    asset.Create = data;
                    asset.UpdatedBy = currentUser.UserName;
                    asset.UpdatedAt = DateTime.Now;
                    dbConn.Update(asset);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }

            }
        }

        public ActionResult UpdateUpdates(int AssetID, List<int> data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    asset.Update = data;
                    asset.UpdatedBy = currentUser.UserName;
                    asset.UpdatedAt = DateTime.Now;
                    dbConn.Update(asset);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }

            }
        }

        public ActionResult UpdateDeletes(int AssetID, List<int> data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    asset.Delete = data;
                    asset.UpdatedBy = currentUser.UserName;
                    asset.UpdatedAt = DateTime.Now;
                    dbConn.Update(asset);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }

            }
        }

        public ActionResult UpdateExports(int AssetID, List<int> data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    asset.Export = data;
                    asset.UpdatedBy = currentUser.UserName;
                    asset.UpdatedAt = DateTime.Now;
                    dbConn.Update(asset);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }

            }
        }

        public ActionResult UpdateImports(int AssetID, List<int> data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    asset.Import = data;
                    asset.UpdatedBy = currentUser.UserName;
                    asset.UpdatedAt = DateTime.Now;
                    dbConn.Update(asset);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }

            }
        }
    }
}