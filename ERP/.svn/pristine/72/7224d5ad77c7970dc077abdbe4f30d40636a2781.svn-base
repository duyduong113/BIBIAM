using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class ManageSyncLogController : CustomController
    {
        // GET: CustomerPresenter
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
                        ViewBag.listSyncLog = dbConn.Select<CRM_Manage_Sync_Log>();
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
        public ActionResult SyncData( string listsp_sync1, string listsp_sync2, string syncType)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRow1 = listsp_sync1.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var listRow2 = listsp_sync2.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for(var i=0; i < listRow1.Length; i++)
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@SyncType", syncType));
                        param.Add(new SqlParameter("@sp_sync", listRow2[i]));
                        int k = new SqlHelper().ExecuteNoneQuery(listRow1[i], param);
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"Select distinct * from CRM_Sync_Log";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Sync_Log>(request, strQuery, "");
                    request.Filters = null;
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //public ActionResult CustomerPresenter_Create(CRM_Customer_Presenter item)
        //{
        //    if (asset.View)
        //    {
        //        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
        //        {
        //            if (item != null)
        //            {
        //                try
        //                {
        //                    if (item.Id == 0)
        //                    {
        //                        CRM_Customer_Presenter insert = new CRM_Customer_Presenter();
        //                        insert.FirstName = !String.IsNullOrEmpty(item.FirstName) ? item.FirstName : "";
        //                        insert.MiddleName = !String.IsNullOrEmpty(item.MiddleName) ? item.MiddleName : "";
        //                        insert.LastName = !String.IsNullOrEmpty(item.LastName) ? item.LastName : "";
        //                        insert.Personal = !String.IsNullOrEmpty(item.Personal) ? item.Personal : "";
        //                        insert.DayOfBirth = !String.IsNullOrEmpty(item.DayOfBirth) ? item.DayOfBirth : "";
        //                        insert.MonthOfBirth = !String.IsNullOrEmpty(item.MonthOfBirth) ? item.MonthOfBirth : "";
        //                        insert.YearOfBirth = !String.IsNullOrEmpty(item.YearOfBirth) ? item.YearOfBirth : "";
        //                        insert.Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : "";
        //                        insert.Email = !String.IsNullOrEmpty(item.Email) ? item.Email : "";
        //                        insert.Position = !String.IsNullOrEmpty(item.Position) ? item.Position : "";
        //                        insert.Company = !String.IsNullOrEmpty(item.Company) ? item.Company : "";
        //                        //insert.CustomerID = !String.IsNullOrEmpty(item.CustomerID) ? item.CustomerID : "";
        //                        insert.Note = !String.IsNullOrEmpty(item.Note) ? item.Note : "";
        //                        dbConn.Insert<CRM_Customer_Presenter>(insert);
        //                        long lastIdInsert = dbConn.GetLastInsertId();
        //                        if (item.customerArray != null && item.customerArray.Length > 0)
        //                        {
        //                            foreach (string str in item.customerArray)
        //                            {
        //                                CRM_CustomerLead_Generation g = new CRM_CustomerLead_Generation();
        //                                g.CustomerID = str;
        //                                g.PresenterID = lastIdInsert;
        //                                g.RowCreatedTime = DateTime.Now;
        //                                g.RowCreatedUser = currentUser.UserName;
        //                                //if (g != null)
        //                                //{
        //                                //    dbConn.Delete<CRM_CustomerLead_Generation>(g);
        //                                //}
        //                                dbConn.Insert<CRM_CustomerLead_Generation>(g);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        CRM_Customer_Presenter insert = new CRM_Customer_Presenter();
        //                        insert.Id = item.Id;
        //                        insert.FirstName = !String.IsNullOrEmpty(item.FirstName) ? item.FirstName : "";
        //                        insert.MiddleName = !String.IsNullOrEmpty(item.MiddleName) ? item.MiddleName : "";
        //                        insert.LastName = !String.IsNullOrEmpty(item.LastName) ? item.LastName : "";
        //                        insert.Personal = !String.IsNullOrEmpty(item.Personal) ? item.Personal : "";
        //                        insert.DayOfBirth = !String.IsNullOrEmpty(item.DayOfBirth) ? item.DayOfBirth : "";
        //                        insert.MonthOfBirth = !String.IsNullOrEmpty(item.MonthOfBirth) ? item.MonthOfBirth : "";
        //                        insert.YearOfBirth = !String.IsNullOrEmpty(item.YearOfBirth) ? item.YearOfBirth : "";
        //                        insert.Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : "";
        //                        insert.Email = !String.IsNullOrEmpty(item.Email) ? item.Email : "";
        //                        insert.Position = !String.IsNullOrEmpty(item.Position) ? item.Position : "";
        //                        insert.Company = !String.IsNullOrEmpty(item.Company) ? item.Company : "";
        //                        //insert.CustomerID = !String.IsNullOrEmpty(item.CustomerID) ? item.CustomerID : "";
        //                        insert.Note = !String.IsNullOrEmpty(item.Note) ? item.Note : "";
        //                        dbConn.Update<CRM_Customer_Presenter>(insert);
        //                        if (item.customerArray != null && item.customerArray.Length > 0)
        //                        {
        //                            dbConn.Delete<CRM_CustomerLead_Generation>(where: "PresenterID='" + insert.Id + "'");
        //                            foreach (string s in item.customerArray)
        //                            {
        //                                CRM_CustomerLead_Generation g = new CRM_CustomerLead_Generation();
        //                                g.CustomerID = s;
        //                                g.PresenterID = insert.Id;
        //                                g.RowCreatedTime = DateTime.Now;
        //                                g.RowCreatedUser = currentUser.UserName;
        //                                dbConn.Insert<CRM_CustomerLead_Generation>(g);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            dbConn.Delete<CRM_CustomerLead_Generation>(where: "PresenterID='" + insert.Id + "'");
        //                        }
        //                    }
        //                    return Json(new { success = true });
        //                }
        //                catch (Exception ex)
        //                {
        //                    return Json(new { success = false, error = ex.Message });
        //                }
        //                finally { dbConn.Close(); }

        //            }
        //            else
        //            {
        //                return Json(new { success = false, error = "Bạn không có quyền" });
        //            }

        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = false, error = "Bạn không có quyền" });
        //    }

        //}
        //public ActionResult CustomerPresenter_Update(CRM_Customer_Presenter item)
        //{
        //    if (asset.View)
        //    {
        //        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
        //        {
        //            if (item != null)
        //            {
        //                try
        //                {
        //                    CRM_Customer_Presenter insert = new CRM_Customer_Presenter();
        //                    insert.Id = item.Id;
        //                    insert.FirstName = !String.IsNullOrEmpty(item.FirstName) ? item.FirstName : "";
        //                    insert.MiddleName = !String.IsNullOrEmpty(item.MiddleName) ? item.MiddleName : "";
        //                    insert.LastName = !String.IsNullOrEmpty(item.LastName) ? item.LastName : "";
        //                    insert.Personal = !String.IsNullOrEmpty(item.Personal) ? item.Personal : "";
        //                    insert.DayOfBirth = !String.IsNullOrEmpty(item.DayOfBirth) ? item.DayOfBirth : "";
        //                    insert.MonthOfBirth = !String.IsNullOrEmpty(item.MonthOfBirth) ? item.MonthOfBirth : "";
        //                    insert.YearOfBirth = !String.IsNullOrEmpty(item.YearOfBirth) ? item.YearOfBirth : "";
        //                    insert.Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : "";
        //                    insert.Email = !String.IsNullOrEmpty(item.Email) ? item.Email : "";
        //                    insert.Position = !String.IsNullOrEmpty(item.Position) ? item.Position : "";
        //                    insert.Company = !String.IsNullOrEmpty(item.Company) ? item.Company : "";
        //                    //insert.CustomerID = !String.IsNullOrEmpty(item.CustomerID) ? item.CustomerID : "";
        //                    insert.Note = !String.IsNullOrEmpty(item.Note) ? item.Note : "";
        //                    dbConn.Update<CRM_Customer_Presenter>(insert);
        //                    if (item.customerArray != null && item.customerArray.Length > 0)
        //                    {
        //                        foreach (string s in item.customerArray)
        //                        {
        //                            CRM_CustomerLead_Generation g = new CRM_CustomerLead_Generation();
        //                            g.CustomerID = s;
        //                            g.PresenterID = insert.Id;
        //                            g.RowCreatedTime = DateTime.Now;
        //                            g.RowCreatedUser = currentUser.UserName;
        //                            if (g != null)
        //                            {
        //                                dbConn.Delete<CRM_CustomerLead_Generation>(g);
        //                            }
        //                            dbConn.Insert<CRM_CustomerLead_Generation>(g);
        //                        }
        //                    }

        //                    return Json(new { success = true });
        //                }
        //                catch (Exception ex)
        //                {
        //                    return Json(new { success = false, error = ex.Message });
        //                }
        //                finally { dbConn.Close(); }

        //            }
        //            else
        //            {
        //                return Json(new { success = false, error = "Bạn không có quyền" });
        //            }

        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = false, error = "Bạn không có quyền" });
        //    }
        //}
        public ActionResult Delete(int id)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    var check = dbConn.FirstOrDefault<CRM_Manage_Sync_Log>("ID={0}", id);
                    if (check != null)
                    {
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
        public ActionResult DeleteAll(string listdata)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var success = dbConn.Delete<CRM_Manage_Sync_Log>(where: "ID = '" + item + "'") >= 1;
                        if (!success)
                        {
                            return Json(new { success = false, message = "Không thể lưu" });
                        }
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
        }
       
    }
}