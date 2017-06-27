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
using DevTrends.MvcDonutCaching;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class GroupsController : CustomController
    {
        //
        // GET: /Groups/
        public ActionResult Index()
        {
            if (asset.View)
            {
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Groups>();
                if (asset.View)
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Query<Groups>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Groups>().ToList();
                    }
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Groups> items)
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Groups> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            if (item.Name == "administrator")
                            {
                                ModelState.AddModelError("", "Can't edit group admin");
                            }
                            else
                            {
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = User.Identity.Name;
                                dbConn.Update(item);
                            }
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

        [DonutOutputCache(CacheProfile = "5Secs")]
        public ActionResult GetListAssets(int Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Assets>();
                data.ForEach(s => s.Group = Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetListAssetsSearch(string Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Assets>("SELECT * FROM Assets where Name like '%" + Id + "%'");
                //data.ForEach(s => s.Group = );
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateAssets(int AssetID, int Group, string Func, bool Checked)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var asset = dbConn.SingleOrDefault<Assets>("Id={0}", AssetID);
                    switch (Func)
                    {
                        case "View":
                            if (Checked)
                            {
                                if (asset.View != null)
                                {
                                    asset.View.Add(Group);
                                }
                                else
                                {
                                    asset.View = new List<int>() { Group };
                                }
                                asset.View = asset.View.Distinct().ToList();
                                asset.Columns = new List<string>();// { "upload/4-2015/anh_san_pham/2015-10-19/1445249945_myxaymangic.jpg", "upload/4-2015/anh_san_pham/2015-10-20/1445310199_aps.jpg", "upload/4-2015/anh_san_pham/2015-10-20/1445311297_may-xay-thit-da-nang-magic4-1200x1200copy.jpg", "upload/4-2015/anh_san_pham/2015-10-20/1445312588_cc.jpg", "upload/4-2015/anh_san_pham/2015-10-20/1445312846_600x600copy1.JPG" };
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            else
                            {
                                if (asset.View != null)
                                    asset.View.Remove(Group);
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            break;
                        case "Create":
                            if (Checked)
                            {
                                if (asset.Create != null)
                                {
                                    asset.Create.Add(Group);
                                }
                                else
                                {
                                    asset.Create = new List<int>() { Group };
                                }
                                asset.Create = asset.Create.Distinct().ToList();
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            else
                            {
                                if (asset.Create != null)
                                    asset.Create.Remove(Group);
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            break;
                        case "Update":
                            if (Checked)
                            {
                                if (asset.Update != null)
                                {
                                    asset.Update.Add(Group);
                                }
                                else
                                {
                                    asset.Update = new List<int>() { Group };
                                }
                                asset.Update = asset.Update.Distinct().ToList();
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            else
                            {
                                if (asset.Update != null)
                                    asset.Update.Remove(Group);
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            break;
                        case "Delete":
                            if (Checked)
                            {
                                if (asset.Delete != null)
                                {
                                    asset.Delete.Add(Group);
                                }
                                else
                                {
                                    asset.Delete = new List<int>() { Group };
                                }
                                asset.Delete = asset.Delete.Distinct().ToList();
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            else
                            {
                                if (asset.Delete != null)
                                    asset.Delete.Remove(Group);
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            break;
                        case "Export":
                            if (Checked)
                            {
                                if (asset.Export != null)
                                {
                                    asset.Export.Add(Group);
                                }
                                else
                                {
                                    asset.Export = new List<int>() { Group };
                                }
                                asset.Export = asset.Export.Distinct().ToList();
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            else
                            {
                                if (asset.Export != null)
                                    asset.Export.Remove(Group);
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            break;
                        case "Import":
                            if (Checked)
                            {
                                if (asset.Import != null)
                                {
                                    asset.Import.Add(Group);
                                }
                                else
                                {
                                    asset.Import = new List<int>() { Group };
                                }
                                asset.Import = asset.Import.Distinct().ToList();
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            else
                            {
                                if (asset.Import != null)
                                    asset.Import.Remove(Group);
                                asset.UpdatedAt = DateTime.Now;
                                asset.UpdatedBy = currentUser.UserName;
                                dbConn.Update(asset);
                            }
                            break;
                        default:
                            break;
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
    }
}