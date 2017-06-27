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
    public class CRMPageProductController : CustomController
    {
        // GET: CRMContractExtra
        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listCategory = dbConn.Select<CRM_PageCategory>().OrderByDescending(s => s.CategoryID);
                ViewBag.listPosition = dbConn.Select<CRM_PagePosition>().OrderByDescending(s => s.PositionID);
                ViewBag.listPriceType = dbConn.Select<Parameters>("Type ={0}", "PRICETYPE").OrderByDescending(s => s.ParamID);
                ViewBag.listProductType = dbConn.Select<Parameters>("Type ={0}", "PRODUCT_TYPE").OrderByDescending(s => s.ParamID);

            }
            return View();
        }
        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<CRM_PageProduct> list)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Create || asset.Update)
            {
                if (list != null)
                {
                    try
                    {
                        foreach (var item in list)
                        {
                            var exits = dbConn.SingleOrDefault<CRM_PageProduct>("PositionID= {0}", item.PositionID);
                            if (exits == null)
                            {
                                var row = new CRM_PageProduct();
                                row.ProductName = !string.IsNullOrEmpty(item.ProductName) ? item.ProductName.Trim() : "";
                                row.ProductType = !string.IsNullOrEmpty(item.ProductType) ? item.ProductType.Trim() : "";
                                row.PositionID = !(item.PositionID == 0) ? item.PositionID : 0;
                                row.CategoryID = !(item.CategoryID == 0) ? item.CategoryID : 0;
                                row.Price = !(item.Price == 0) ? item.Price : 0;
                                row.PriceType = !string.IsNullOrEmpty(item.PriceType) ? item.PriceType.Trim() : "";
                                row.Status = !(item.Status == 0) ? item.Status : 0;

                                row.CreatedBy = currentUser.UserName;
                                row.UpdatedBy = "";
                                row.CreatedAt = DateTime.Now;
                                row.UpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                            }
                            else {
                                exits.ProductName = !string.IsNullOrEmpty(item.ProductName) ? item.ProductName.Trim() : "";
                                exits.ProductType = !string.IsNullOrEmpty(item.ProductType) ? item.ProductType.Trim() : "";
                                exits.PositionID = !(item.PositionID == 0) ? item.PositionID : 0;
                                exits.CategoryID = !(item.CategoryID == 0) ? item.CategoryID : 0;
                                exits.Price = !(item.Price == 0) ? item.Price : 0;
                                exits.PriceType = !string.IsNullOrEmpty(item.PriceType) ? item.PriceType.Trim() : "";
                                exits.Status = !(item.Status == 0) ? item.Status : 0;

                                exits.UpdatedBy = currentUser.UserName;
                                exits.UpdatedAt = DateTime.Now;
                                dbConn.Update(exits);
                            }
                        }
                        ModelState.AddModelError("Thành công!", "Lưu thành công.");
                        return Json(new { sussess = true });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("error", ex.Message);
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                }
                return Json(new { sussess = true });
            }

            else
            {
                ModelState.AddModelError("error", "Bạn không có quyền cập nhật.");
                return Json(list.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Delete(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var delete = new CRM_PageProduct();
                        foreach (var item in listRowID)
                        {
                            delete.ProductID = Int32.Parse(item);
                            dbConn.Delete(delete);
                        }
                        dbTrans.Commit();
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
        public ActionResult CRM_PageProduct_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT p.*
                    FROM CRM_PageProduct p";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_PageProduct>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
    }
}