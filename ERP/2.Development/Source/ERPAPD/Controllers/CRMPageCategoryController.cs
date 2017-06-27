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
    public class CRMPageCategoryController : CustomController
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
                ViewBag.listPage = dbConn.Select<CRM_Page>("Select PageID,PageName From CRM_Page");
                ViewBag.listWebsite = dbConn.Select<CRM_Website>().OrderBy(s => s.WebsiteID);
                ViewBag.listType = dbConn.Select<Parameters>("Type = 'PAGECATEGORY_TYPE'");
            }
            return View();
        }
        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<CRM_PageCategory> list)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Update || asset.Create)
            {
                if (list != null)
                {
                    try
                    {
                        foreach (var item in list)
                        {
                            var exits = dbConn.SingleOrDefault<CRM_PageCategory>("CategoryID= {0}", item.CategoryID);
                            if (exits == null)
                            {
                                var row = new CRM_PageCategory();
                                row.CategoryName = !string.IsNullOrEmpty(item.CategoryName) ? item.CategoryName.Trim() : "";
                                row.WebsiteID = !(item.WebsiteID == 0) ? item.WebsiteID : 0;
                                row.RefID = !string.IsNullOrEmpty(item.RefID) ? item.RefID.Trim() : "";
                                row.Status =  item.Status ;
                                row.Type = item.Type;
                                row.CreatedBy = currentUser.UserName;
                                row.UpdatedBy = "";
                                row.CreatedAt = DateTime.Now;
                                row.UpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                            }
                            else {
                                exits.CategoryName = !string.IsNullOrEmpty(item.CategoryName) ? item.CategoryName.Trim() : "";
                                exits.WebsiteID = !(item.WebsiteID == 0) ? item.WebsiteID : 0;
                                exits.RefID = !string.IsNullOrEmpty(item.RefID) ? item.RefID.Trim() : "";
                                exits.Status = item.Status ;
                                exits.Type = item.Type;
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
                        var delete = new CRM_PageCategory();
                        foreach (var item in listRowID)
                        {
                            delete.CategoryID = Int32.Parse(item);
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
        public ActionResult CRM_PageCategory_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT c.*,w.WebsiteName as WebsiteName,w1.Value as TypeName
                      FROM CRM_PageCategory c
                      LEFT JOIN CRM_Website w ON w.WebsiteID = c.WebsiteID
					  LEFT JOIN Parameters w1 ON w1.ParamID = c.Type";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_PageCategory>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //Insert/Delte/Update CRM_PageCategory_Mapping ===============================================================================================
        public ActionResult Read_CRM_PageCategory_Mapping([DataSourceRequest]DataSourceRequest request, int CategoryID)
        {
            //using (IDbConnection dbConn = OrmliteConnection.openConn())
            //{
            //    if (asset.View)
            //    {
            //        ViewBag.listPage = dbConn.Select<CRM_PageCategory_Mapping>("Select PageID,PageName From CRM_Page Where WebsiteID = (Select WebsiteID From CRM_PageCategory Where CategoryID = {0})",CategoryID);
            //        var data = dbConn.Select<CRM_PageCategory_Mapping>("CategoryID = {0}", CategoryID);
            //        return Json(data);
            //    }
            //    return RedirectToAction("NoAccessRights", "Error");
            //}
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"Select * From CRM_PageCategory_Mapping Where CategoryID = " + CategoryID;
                    data = KendoApplyFilter.KendoDataByQuery<CRM_PageCategory_Mapping>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Update_CRM_PageCategory_Mapping([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<CRM_PageCategory_Mapping> list, int CategoryID)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Update || asset.Create)
            {
                if (list != null)
                {
                    try
                    {
                        
                        foreach (var item in list)
                        {
                            var isexits = dbConn.SingleOrDefault<CRM_PageCategory_Mapping>("CategoryID= {0} And PageID = {1}", CategoryID, item.PageID);
                            if (isexits == null)
                            {
                                var exits = dbConn.SingleOrDefault<CRM_PageCategory_Mapping>("RowID= {0}", item.RowID);
                                if (exits == null)
                                {
                                    var row = new CRM_PageCategory_Mapping();
                                    row.CategoryID = CategoryID;
                                    row.PageID = item.PageID;
                                    row.Type = item.Type;
                                    row.CreatedBy = currentUser.UserName;
                                    row.CreatedAt = DateTime.Now;
                                    dbConn.Insert(row);
                                }
                                else
                                {
                                    exits.CategoryID = item.CategoryID;
                                    exits.PageID = item.PageID;
                                    exits.Type = item.Type;
                                    dbConn.Update(exits);
                                }
                                ModelState.AddModelError("Thành công!", "Lưu thành công.");
                                return Json(new { sussess = true });
                            }
                            else
                            {
                                ModelState.AddModelError("error", "Đã tồn tại trang này!");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }
                            
                        }
                        
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
        public ActionResult DeleteAnswer(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        //check
                        foreach (var item in listdata)
                        {
                            dbConn.Delete<CRM_PageCategory_Mapping>(where: "RowID={0}".Params(item));
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }
        // ===============================================================================================
        public ActionResult ReadPage(string CategoryID)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { success = false, message = "Bạn chưa đăng nhập" });

            var data = CRM_Page.ReadPageOfCategory(CategoryID);
            return Json(new { success = true, data = data });
        }
        public ActionResult savePageCategory(string CategoryID, string listdata)
        {
            if (!asset.Create)
            {
                return Json(new { success = false, message = "Bạn không có quyền tạo" });
            }
            CRM_Page.SaveMapping(CategoryID, listdata);
            return Json(new { success = true});
        }
    }
}