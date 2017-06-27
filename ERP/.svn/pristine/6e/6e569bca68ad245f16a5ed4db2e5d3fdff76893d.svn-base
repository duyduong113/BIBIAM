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
    public class SalesAppIntroController : CustomController
    {
        //
        // GET: /SalesAppIntro/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_SalesApp_Intro));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_SalesApp_Intro));
            //}
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
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_SalesApp_Intro>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_SalesApp_Intro> items)
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
                            int Id = (int)dbConn.GetLastInsertId();
                            item.Id = Id;
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_SalesApp_Intro> items)
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
                            if (item.Active)
                            {
                                var other = dbConn.Select<Deca_SalesApp_Intro>("Id<>{0}", item.Id);
                                other.ForEach(s => { s.Active = false; s.UpdatedAt = DateTime.Now; s.UpdatedBy = currentUser.UserName; });
                                dbConn.UpdateAll(other);
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
        [ValidateInput(false)]
        public ActionResult UpdateContent(int Id, string Content)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.FirstOrDefault<Deca_SalesApp_Intro>("Id={0}", Id);
                    if (data != null)
                    {
                        data.Content = Content;
                        data.UpdatedAt = DateTime.Now;
                        data.UpdatedBy = currentUser.UserName;
                        dbConn.Update(data);
                    }
                    return Json(new { success = true });
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
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