using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    [Authorize]
    public class CRMProductController : CustomController
    {
        // GET: CRMProduct
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        ViewData["AllowCreate"] = asset.Create;
                        ViewData["AllowUpdate"] = asset.Update;
                        ViewData["AllowDelete"] = asset.Delete;
                        ViewData["AllowExport"] = asset.Export;

                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                }

                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT   p.*
                                        FROM [CRM_Product] p
                                         ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Product>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Product> listEx)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var pr in listEx)
                            {

                                if (string.IsNullOrEmpty(pr.ProductName))
                                {
                                    ModelState.AddModelError("", "Vui nhập tên sản phẩm");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                } 

                                pr.RowCreatedAt = DateTime.Now;
                                pr.RowCreatedUser = currentUser.UserName;
                                dbConn.Insert(pr);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Product> listEx)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var pr in listEx)
                            {
                                if (string.IsNullOrEmpty(pr.ProductName))
                                {
                                    ModelState.AddModelError("", "Vui nhập tên sản phẩm");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                pr.RowUpdatedAt = DateTime.Now;
                                pr.RowUpdatedUser = currentUser.UserName;
                                dbConn.Update(pr);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            var check = dbConn.FirstOrDefault<CRM_Product>("Id={0}", item);
                            dbConn.Delete(check);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
    }
}