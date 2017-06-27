using CRM24H.Controllers;
using CRM24H.Models;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using CRM24H.Helpers;
using System;
using System.Globalization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using Dapper;
using System.Data;

namespace DecaPay.Controllers
{
    public class CRM_BookingLocationController : CustomController
    {
        // GET: CRM_BookingLocation
        public ActionResult Index()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code,FullName AS Name from EmployeeInfo ");
                ViewBag.listWebsite = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 23");
                ViewBag.listNature = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 25");
                ViewBag.listTeam = dbConn.Select<DropDownListData>("select TeamID AS Code ,TeamName AS Name from CRM_Team where FKUnit = 13");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select  Code,Name from CRM24H_List WHERE FKListtype = 52 ");
            }

            return View();
        }
        public ActionResult GetListCategory(string web)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.SqlList<DropDownListDataList>("EXEC p_SelectCRM24H_Category_byCode @PageCode", new { PageCode = web });
                return Json(new { success = true, listCategory = data });
            }
        }

        public ActionResult GetListStaff(string team)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.SqlList<DropDownListData>("EXEC p_getListStaffByTeam @team", new { team = team });
                return Json(new { success = true, listStaff = data });
            }
        }

        private static int getTeamByUser(string staff, IDbConnection dbConn)
        {
            try
            {
                return int.Parse(dbConn.SingleOrDefault<EmployeeInfo>("RefStaffId = {0}", staff).Team);
            }
            catch (Exception)
            {
                return 0;
            }
           
        }

        public ActionResult BookPRLocation_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    string sqlQuery = @"SELECT   req.*
                                                ,SUBSTRING(c_code,3, 10) AS BookCode
                                                ,ISNULL(l.ID,0) AS IDBookLocation
                                                ,ISNULL(l.PKBookLocation,0) AS PKBookLocation
                                                ,ISNULL(l.FKBookReq,'') AS FKBookReq
	                                            ,ISNULL(l.Website,'') AS Website
	                                            ,ISNULL(l.Category,'') AS Category
	                                            ,ISNULL(l.Location,'') AS Location
	                                            ,ISNULL(l.Nature,'') AS Nature
	                                            ,ISNULL(l.NgayLen,'') AS NgayLen
	                                            ,ISNULL(l.NgayXuong ,'') AS NgayXuong
                                                ,ISNULL(l.InputDate,'') AS InputDate
	                                            ,ISNULL(l.AccountStatus,'') AS AccountStatus
                                                ,ISNULL(cus.CustomerName,'') AS CustomerName
                                        FROM CRM_BookReq req
                                        INNER JOIN  CRM_BookLocation l on l.FKBookReq = req.pk_book_req
                                        LEFT JOIN  CRM24H_Customer cus on cus.CustomerCode = req.c_customer_code
                                        WHERE req.pk_book_req <> 0
                                        UNION ALL
                                        SELECT   req.*
                                                ,SUBSTRING(c_code,3, 10) AS BookCode
                                                ,ISNULL(l.ID,0) AS IDBookLocation
                                                ,ISNULL(l.PKBookLocation,0) AS PKBookLocation
                                                ,ISNULL(l.FKBookReq,'') AS FKBookReq
	                                            ,ISNULL(l.Website,'') AS Website
	                                            ,ISNULL(l.Category,'') AS Category
	                                            ,ISNULL(l.Location,'') AS Location
	                                            ,ISNULL(l.Nature,'') AS Nature
	                                            ,ISNULL(l.NgayLen,'') AS NgayLen
	                                            ,ISNULL(l.NgayXuong ,'') AS NgayXuong
                                                ,ISNULL(l.InputDate,'') AS InputDate
	                                            ,ISNULL(l.AccountStatus,'') AS AccountStatus
                                                ,ISNULL(cus.CustomerName,'') AS CustomerName
                                        FROM CRM_BookReq req
                                        INNER JOIN  CRM_BookLocation l on l.FKBookReq = req.id
                                        LEFT JOIN  CRM24H_Customer cus on cus.CustomerCode = req.c_customer_code OR cus.CustomerID = req.c_customer_code 
                                        WHERE req.pk_book_req = 0";
                  
                    return Json(KendoApplyFilter.KendoDataByQuery<BookLocationViewModel>(request, sqlQuery, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult BookPRLocation_ReadByBookCode([DataSourceRequest] DataSourceRequest request,string bookcode)
        {
            
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    string sqlQuery = @"SELECT   
                                                 req.c_code
		                                        ,req.pk_book_req
		                                        ,req.c_staff_id
		                                        ,req.c_labels
                                                ,SUBSTRING(c_code,3, 10) AS BookCode
                                                ,ISNULL(l.ID,0) AS ID
                                                ,ISNULL(l.FKBookReq,'') AS FKBookReq
	                                            ,ISNULL(l.Website,'') AS Website
	                                            ,ISNULL(l.Category,'') AS Category
	                                            ,ISNULL(l.Location,'') AS Location
	                                            ,ISNULL(l.Nature,'') AS Nature
	                                            ,ISNULL(l.NgayLen,'') AS NgayLen
	                                            ,ISNULL(l.NgayXuong ,'') AS NgayXuong
                                                ,ISNULL(l.SoNgay,0) AS SoNgay
	                                            ,ISNULL(l.SoTuan ,0) AS SoTuan
                                                ,ISNULL(l.Label ,'') AS Label
                                                ,ISNULL(l.InputDate,'') AS InputDate
	                                            ,ISNULL(l.AccountStatus,'') AS AccountStatus
                                                ,ISNULL(cus.CustomerName,'') AS CustomerName
                                        FROM CRM_BookReq req
                                        INNER JOIN  CRM_BookLocation l on l.FKBookReq = req.pk_book_req
                                        LEFT JOIN  CRM24H_Customer cus on cus.CustomerCode = req.c_customer_code
                                        WHERE req.pk_book_req <> 0 AND req.c_code = '" + bookcode + "'";
                     sqlQuery += @" UNION ALL
                                        SELECT   req.c_code
		                                        ,req.pk_book_req
		                                        ,req.c_staff_id
		                                        ,req.c_labels
                                                ,SUBSTRING(c_code,3, 10) AS BookCode
                                                ,ISNULL(l.ID,0) AS ID
                                                ,ISNULL(l.FKBookReq,'') AS FKBookReq
	                                            ,ISNULL(l.Website,'') AS Website
	                                            ,ISNULL(l.Category,'') AS Category
	                                            ,ISNULL(l.Location,'') AS Location
	                                            ,ISNULL(l.Nature,'') AS Nature
	                                            ,ISNULL(l.NgayLen,'') AS NgayLen
	                                            ,ISNULL(l.NgayXuong ,'') AS NgayXuong
                                                ,ISNULL(l.SoNgay,0) AS SoNgay
	                                            ,ISNULL(l.SoTuan ,0) AS SoTuan
                                                ,ISNULL(l.Label ,'') AS Label
                                                ,ISNULL(l.InputDate,'') AS InputDate
	                                            ,ISNULL(l.AccountStatus,'') AS AccountStatus
                                                ,ISNULL(cus.CustomerName,'') AS CustomerName

                                        FROM CRM_BookReq req
                                        INNER JOIN  CRM_BookLocation l on l.FKBookReq = req.id
                                        LEFT JOIN  CRM24H_Customer cus on cus.CustomerCode = req.c_customer_code OR cus.CustomerID = req.c_customer_code 
                                        WHERE req.pk_book_req = 0 AND req.c_code = '" + bookcode + "'";

                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_BookLocation>(request, sqlQuery, ""));
                }

                
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult BookLocationNew()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code, Name from CRM24H_Employee");
                ViewBag.listWebsite = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 23");
                ViewBag.listNature = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 25");
                ViewBag.listTeam = dbConn.Select<DropDownListData>("select TeamID AS Code ,TeamName AS Name from CRM_Team where FKUnit = 13");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select  Code,Name from CRM24H_List WHERE FKListtype = 52 ");
                ViewBag.listPosition = dbConn.Select<DropDownListData>("select PKList AS Code, Name from CRM24H_List WHERE FKListtype = 5");
                
                var bookcode = dbConn.SingleOrDefault<Parameters>("Type = 'BookLocation'");
                if (bookcode != null)
                {
                    var bk = int.Parse(bookcode.Value) + 1;
                    string b = "B." + String.Format("{0:000000}", bk);
                    bookcode.Value = bk.ToString();
                    ViewBag.BookCode = b;
                    dbConn.Update(bookcode);
                }
            }
            return View();
        }
        public ActionResult BookLocationEdit(string id)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                
                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code, Name from CRM24H_Employee");
                ViewBag.listWebsite = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 23");
                ViewBag.listNature = dbConn.Select<DropDownListDataList>("select Code,Name from CRM24H_List WHERE FKListtype = 25");
                ViewBag.listTeam = dbConn.Select<DropDownListData>("select TeamID AS Code ,TeamName AS Name from CRM_Team where FKUnit = 13");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select  Code,Name from CRM24H_List WHERE FKListtype = 52 ");
                ViewBag.listPosition = dbConn.Select<DropDownListData>("select PKList AS Code, Name from CRM24H_List WHERE FKListtype = 5");
                ViewBag.BookCode = "B." +id;

                var book = dbConn.SingleOrDefault<CRM_BookReq>("c_code = {0}", "B." + id);
                if (book != null)
                {
                    
                    ViewBag.bookHeader = book;
                    var customerIdNew = dbConn.SingleOrDefault<CRM24H_Customer>("CustomerID = {0}", book.c_customer_code);
                    if (customerIdNew != null)
                    {
                        ViewBag.CustomerName = dbConn.SingleOrDefault<CRM24H_Customer>("CustomerID = {0}", book.c_customer_code).CustomerName;
                    }
                    else
                    {
                        var customerIdOld = dbConn.SingleOrDefault<CRM24H_Customer>("CustomerCode = {0}", book.c_customer_code);
                        if (customerIdOld != null)
                        {
                            ViewBag.CustomerName = dbConn.SingleOrDefault<CRM24H_Customer>("CustomerCode = {0}", book.c_customer_code).CustomerName;
                        }
                        else
                        {
                            ViewBag.CustomerName = "";
                        }
                    }
                }

            }
            return View();
        }

        public ActionResult CRM_BookingLocation_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_BookLocation> list, 
                                                        string customerid, string staff, string bookcode, string brand,string email = "",string name ="",int positon =0, string note = "", float money = 0,string content = "",string date = "")
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                try
                {
                    if (list != null && ModelState.IsValid)
                    {
                        // check data in header booking
                        if (string.IsNullOrEmpty(customerid) || string.IsNullOrEmpty(staff) || string.IsNullOrEmpty(bookcode))
                        {
                            ModelState.AddModelError("", "Vui lòng nhập các thông tin của khách hàng");
                            return Json(list.ToDataSourceResult(request, ModelState));
                        }

                        var req = new CRM_BookReq();
                        req.pk_book_req = 0;
                        req.c_staff_id = long.Parse(staff);
                        req.c_code = bookcode;
                        req.c_customer_code = customerid;
                        req.c_labels = brand;
                        req.c_input_date = DateTime.Now;
                        req.c_check_vat = true;
                        req.c_status = "BOOK_MOI";
                        req.c_sale_status = "TPKD_CHUA_XAC_NHAN";
                        req.c_account_status = "TPKD_CHUA_XAC_NHAN";
                        req.c_monitor_status = "KS_CHUA_XAC_NHAN";
                        req.c_group_id = getTeamByUser(staff, dbConn);
                        req.c_guarantee_email = email;
                        req.c_guarantee_name = name;
                        req.c_guarantee_money = money;
                        req.c_position = positon;
                        req.c_note = note;
                        req.c_guarantee_note = content;

                        var beginDate = date.Split('-')[0];
                        var endDate = date.Split('-')[1];
                        req.c_guarantee_begin_date = DateTime.Parse(beginDate);
                        req.c_guarantee_end_date = DateTime.Parse(endDate);
                        // cac thong tin chua biet lay o dau de save
                        req.c_area_id = 0;
                        req.c_sales_leader_id = 0;
                        req.c_revenue_date = DateTime.Parse("1900-01-01");

                        req.RowCreatedAt = DateTime.Now;
                        req.RowCreatedUser = currentUser.UserName;
                        dbConn.Insert(req);
                        var fk_book = dbConn.GetLastInsertId();
                        foreach (var item in list)
                        {
                            // Các thông tin của detail:

                            item.FKBookReq = fk_book;
                            item.AccountStatus = "KT_CHUA_XAC_NHAN";
                            item.RowCreatedAt = DateTime.Now;
                            item.RowUpdatedUser = currentUser.UserName;
                            item.RowUpdatedAt = DateTime.Now;
                            item.RowUpdatedUser = currentUser.UserName;
                            dbConn.Insert(item);
                        }

                        return Json(new { sussess = true });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid data");
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
            }

        }
        public ActionResult CRM_BookingLocation_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_BookLocation> list,
                                                        string customerid, string staff, string bookcode, string brand, string email = "", string name = "", int positon = 0, string note = "", float money = 0, string content = "", string date = "")
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                try
                {
                    if (list != null && ModelState.IsValid)
                    {
                        // check data in header booking
                        if (string.IsNullOrEmpty(customerid) || string.IsNullOrEmpty(staff) || string.IsNullOrEmpty(bookcode))
                        {
                            ModelState.AddModelError("", "Vui lòng nhập Khách hàng");
                            return Json(list.ToDataSourceResult(request, ModelState));
                        }


                        dbConn.SingleOrDefault<CRM_BookReq>("FKBookReq = {0}");
                        foreach (var item in list)
                        {
                            item.RowCreatedAt = DateTime.Now;
                            item.RowUpdatedUser = currentUser.UserName;
                            item.RowUpdatedAt = DateTime.Now;
                            item.RowUpdatedUser = currentUser.UserName;
                            dbConn.Update(item);
                        }

                        return Json(new { sussess = true });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid data");
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
            }

        }
        public ActionResult CRM_BookingLocation_ChangeStatus(string listdata, string action)
        {
            try
            {
                using (IDbConnection dbConn = CRM24H.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);


                    foreach (var item in listRowID)
                    {
                        var pr_location = dbConn.SingleOrDefault<CRM_BookLocation>("ID = {0}", item);
                        if (pr_location != null)
                        {
                            //if (action == "DUYET")
                            //{
                            //    pr_location.Status = 1;
                            //    dbConn.Update<CRM_BookPRLocation>(pr_location);
                            //}
                            //if (action == "HUY")
                            //{
                            //    pr_location.Status = 0;
                            //    dbConn.Update<CRM_BookPRLocation>(pr_location);
                            //}
                            //if (action == "GOP")
                            //{
                            //    pr_location.Status = 0;
                            //    dbConn.Update<CRM_BookPRLocation>(pr_location);
                            //}
                            if (action == "XOA")
                            {
                                dbConn.Delete(pr_location);
                                //var success = dbConn.Delete<CRM_BookReq>(where: "FKBookReq = '" + pr_location2.FKBookReq + "'") >= 1;
                                //if (!success)
                                //{
                                //    return Json(new { success = false, message = "Không thể lưu" });
                                //}
                            }
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