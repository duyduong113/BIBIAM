﻿using ERPAPD.Helpers;
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
    public class CategoryController : CustomController
    {
        // GET: Category
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
                    string strQuery = @"SELECT   cat.*       
                                        FROM [CRM_Category] cat
                                         ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Category>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Category> listItem)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listItem != null)
                        {
                            foreach (var item in listItem)
                            {

                                if (String.IsNullOrEmpty(item.CategoryID))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập Mã ngành hàng");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.CategoryName))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập tên ngành hàng");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                var checkExist = dbConn.SingleOrDefault<CRM_Category>("CategoryID={0}", item.CategoryID);
                                if (checkExist != null)
                                {
                                    ModelState.AddModelError("", "Mã ngành đã tồn tại");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                item.RowCreatedTime = DateTime.Now;
                                item.RowCreatedUser = currentUser.UserName;
                                dbConn.Insert<CRM_Category>(item);
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
                        return Json(listItem.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listItem.ToDataSourceResult(request, ModelState));
            }
            return Json(listItem.ToDataSourceResult(request));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Category> listItem)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listItem != null)
                        {
                            foreach (var item in listItem)
                            {

                                if (String.IsNullOrEmpty(item.CategoryID))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập Mã ngành hàng");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.CategoryName))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập tên ngành hàng");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                var checkCode = dbConn.SingleOrDefault<CRM_Category>("CategoryID ={0} AND ID <> {1}", item.CategoryID, item.ID);
                                if (checkCode != null)
                                {
                                    ModelState.AddModelError("", "Trùng mã !");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update<CRM_Category>(item);
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
                        return Json(listItem.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listItem.ToDataSourceResult(request, ModelState));
            }
            return Json(listItem.ToDataSourceResult(request));
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
                            var check = dbConn.FirstOrDefault<CRM_Category>("ID={0}", item);
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