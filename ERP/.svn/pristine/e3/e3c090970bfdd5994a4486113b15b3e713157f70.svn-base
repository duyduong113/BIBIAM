using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class CustomerPositionController : CustomController
    {
        // GET: CustomerPosition
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
                    string strQuery = @"SELECT   cs.*       
                                        FROM [ERPAPD_MasterData_Customer] cs
                                        WHERE Type = 'Position'
                                         ";
                    data = KendoApplyFilter.KendoDataByQuery<ERPAPD_MasterData_Customer>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<ERPAPD_MasterData_Customer> listItem)
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

                                if (String.IsNullOrEmpty(item.Code))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập Mã");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.Value))
                                {
                                    ModelState.AddModelError("", "Vui lòng chọn Giá trị");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                var checkCode = dbConn.SingleOrDefault<ERPAPD_MasterData_Customer>("Code ={0} AND Type ={1}", item.Code, "Position");
                                if (checkCode != null)
                                {
                                    ModelState.AddModelError("", "Trùng mã !");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                item.Type = "Position";
                                item.RowCreatedTime = DateTime.Now;
                                item.RowCreatedUser = currentUser.UserName;
                                dbConn.Insert<ERPAPD_MasterData_Customer>(item);
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<ERPAPD_MasterData_Customer> listItem)
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

                                if (String.IsNullOrEmpty(item.Code))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập Mã");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.Value))
                                {
                                    ModelState.AddModelError("", "Vui lòng chọn Giá trị");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                var checkCode = dbConn.SingleOrDefault<ERPAPD_MasterData_Customer>("Code ={0} AND Type ={1} AND ID <> {2}", item.Code, "Position", item.ID);
                                if (checkCode != null)
                                {
                                    ModelState.AddModelError("", "Trùng mã !");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(item);
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
                            var check = dbConn.FirstOrDefault<ERPAPD_MasterData_Customer>("ID={0} AND Type = {1}", item, "Position");
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