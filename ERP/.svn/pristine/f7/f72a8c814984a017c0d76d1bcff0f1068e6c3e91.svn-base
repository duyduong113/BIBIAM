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


namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SalesAppBannerController : CustomController
    {
        //
        // GET: /SalesAppBanner/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_SalesApp_Banner));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_SalesApp_Banner));
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
                    data = KendoApplyFilter.KendoData<Deca_SalesApp_Banner>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_SalesApp_Banner> items)
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_SalesApp_Banner> items)
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

        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                int error = 0;
                int success = 0;
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {

                            //// check [Bank] in Company:
                            //var cBank = DC_Company.GetDC_Companys("[CompanyBankID] = '" + item + "'", "").FirstOrDefault();
                            //if (cBank == null)
                            //{
                            var check = dbConn.FirstOrDefault<Deca_SalesApp_Banner>("Id=" + item);
                            dbConn.Delete(check);
                            success++;
                            //}
                            //else
                            //{
                            //    error++;
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }



        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.FirstOrDefault<Deca_SalesApp_Banner>("Id={0}", Id);
                if (file != null)
                {
                    if (file.ContentType.Contains("image"))
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var date = DateTime.Now.ToString("ddMMyyyyHHmmss");
                        var physicalPath = Path.Combine(Server.MapPath("~/Content/SalesApp/Banner/"), date + fileName);
                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);
                        string path = "Content/SalesApp/Banner/" + date + fileName;

                        data.ImageUrl = path;
                        data.UpdatedAt = DateTime.Now;
                        data.UpdatedBy = currentUser.UserName;
                        dbConn.Update(data);
                    }
                    else
                    {
                        return Content("fail");
                    }

                }
            }
            return Content("");
        }

        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Content/SalesApp/Banner/"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}