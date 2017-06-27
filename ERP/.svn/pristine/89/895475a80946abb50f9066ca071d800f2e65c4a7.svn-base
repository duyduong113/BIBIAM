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
    public class SalesAppUserController : CustomController
    {
        //
        // GET: /SalesAppUser/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listUser = dbConn.Select<Users>("[IsAppUser]={0}", false);
                    return View();
                }

            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Users>();
                if (asset.View)
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Query<Users>(where + " AND IsAppUser = 1").ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Users>("IsAppUser = 1").ToList();
                    }
                }
                return Json(data.ToDataSourceResult(request));
            }
        }
        public ActionResult getListUser()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var listUser = dbConn.Select<Users>("[IsAppUser]={0}", false);
                return Json(new { success = true, data = listUser });
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Users> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            if (String.IsNullOrEmpty(item.Email))
                            {
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            else
                            {
                                ModelState.AddModelError("", "Vui lòng nhập email");
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
        public ActionResult Addnew(string id)
        {
            if (asset.Create)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string[] list = id.Split(',');
                    foreach (string username in list)
                    {
                        var user = dbConn.FirstOrDefault<Users>("UserName= {0}", username);
                        if (user != null)
                        {
                            user.IsAppUser = true;
                            user.LastUpdatedDateTime = DateTime.Now;
                            user.LastUpdatedUser = currentUser.UserName;
                            dbConn.Update(user);
                        }
                    }
                    return Json(new { success = true, message = Resources.Multi.Success });
                }
            }
            else
            {
                return Json(new { success = false, message = "503: Bạn không có quyền!" });
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