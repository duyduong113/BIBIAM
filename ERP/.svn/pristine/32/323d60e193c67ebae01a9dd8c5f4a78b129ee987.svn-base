using ERPAPD.Models;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ERPAPD.Helpers;
using System;
using System.Globalization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.IO;

namespace ERPAPD.Controllers
{
    public class CRM_BookBannerController : CustomController
    {
        // GET: CRM_BookBanner
        public ActionResult Index()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code,FullName AS Name from EmployeeInfo ");
                ViewBag.listWebsite = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 23");
                ViewBag.listNature = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 25");
                ViewBag.listTeam = dbConn.Select<DropDownListData>("select TeamID AS Code ,TeamName AS Name from CRM_Team where FKUnit = 13");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select  Code,Name from ERPAPD_List WHERE FKListtype = 52 ");
            }

            return View();
        }
        public ActionResult GetListCategory(string web)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.SqlList<DropDownListDataList>("EXEC p_SelectERPAPD_Category_byCode @PageCode", new { PageCode = web });
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

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    string sqlQuery = @"SELECT   req.*
                                                ,SUBSTRING(c_code,3, 10) AS BookCode
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
                                        FROM CRM_Book_Req req
                                        INNER JOIN  CRM_Book_Location l on l.FKBookReq = req.pk_book_req
                                        LEFT JOIN  ERPAPD_Customer cus on cus.CustomerCode = req.c_customer_code
                                       ";

                    return Json(KendoApplyFilter.KendoDataByQuery<BookBannerViewModel>(request, sqlQuery, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ReadDetail([DataSourceRequest] DataSourceRequest request, long pkBookReq)
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
                                                ,ISNULL(l.PKBookLocation,0) AS PKBookLocation
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
                                        FROM CRM_Book_Req req
                                        LEFT JOIN  CRM_Book_Location l on l.FKBookReq = req.pk_book_req
                                        LEFT JOIN  ERPAPD_Customer cus on cus.CustomerCode = req.c_customer_code
                                        WHERE l.FKBookReq  = '" + pkBookReq + "'";
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_Book_Location>(request, sqlQuery, ""));
                }


            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult BookNew()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code,FullName AS Name from EmployeeInfo");
                ViewBag.listWebsite = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 23");
                ViewBag.listNature = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 25");
                ViewBag.listTeam = dbConn.Select<DropDownListData>("select TeamID AS Code ,TeamName AS Name from CRM_Team where FKUnit = 13");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select  Code,Name from ERPAPD_List WHERE FKListtype = 52 ");
                ViewBag.listPosition = dbConn.Select<DropDownListData>("select PKList AS Code, Name from ERPAPD_List WHERE FKListtype = 5");

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

        public ActionResult BookEdit(long id)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code,FullName AS Name from EmployeeInfo");
                ViewBag.listWebsite = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 23");
                ViewBag.listNature = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 25");
                ViewBag.listTeam = dbConn.Select<DropDownListData>("select TeamID AS Code ,TeamName AS Name from CRM_Team where FKUnit = 13");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select  Code,Name from ERPAPD_List WHERE FKListtype = 52 ");
                ViewBag.listPosition = dbConn.Select<DropDownListData>("select PKList AS Code, Name from ERPAPD_List WHERE FKListtype = 5");


                var book = dbConn.QueryById<CRM_Book_Req>(id);
                if (book != null)
                {

                    ViewBag.bookHeader = book;
                    ViewBag.BookCode = book.c_code;
                    var cus = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerCode = {0}", book.c_customer_code);
                    if (cus != null)
                    {
                        ViewBag.CustomerName = cus.CustomerName;
                    }
                    else
                    {
                        ViewBag.CustomerName = "";
                    }

                }
                else
                {
                    ViewBag.bookHeader = null;
                }

            }
            return View();
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

        public ActionResult ChangeStatus(string listdata, string action)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var pr_location = dbConn.SingleOrDefault<CRM_Book_Location>("PKBookLocation = {0}", item);
                        if (pr_location != null)
                        {
                            if (action == "XOA")
                            {
                                dbConn.Delete(pr_location);
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

        public ActionResult SaveOtherInfo(CRM_Book_Req item, HttpPostedFileBase file)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {



                var file_name = "";
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    Helpers.UploadFile.CreateFolder(Server.MapPath("~/attach-file/booking/"));
                    var path = Path.Combine(Server.MapPath("~/attach-file/booking/"), fileName);
                    file.SaveAs(path);
                    file_name = fileName;
                }

                long pkBookReq = 0;
                var req2 = dbConn.SingleOrDefault<CRM_Book_Req>("c_code = {0}", item.c_code);
                if (req2 == null)
                {
                    var req = new CRM_Book_Req();
                    req.c_staff_id = item.c_staff_id;
                    req.c_code = item.c_code;
                    req.c_customer_code = item.c_customer_code;
                    req.c_labels = item.c_labels;
                    req.c_check_vat = true;
                    req.c_status = "BOOK_MOI";
                    req.c_sale_status = "TPKD_CHUA_XAC_NHAN";
                    req.c_account_status = "TPKD_CHUA_XAC_NHAN";
                    req.c_monitor_status = "KS_CHUA_XAC_NHAN";
                    req.c_group_id = getTeamByUser(item.c_staff_id.ToString(), dbConn);

                    if (item.c_guarantee_date != null)
                    {
                        DateTime dt;
                        var beginDate = item.c_guarantee_date.Split('-')[0].Trim();
                        var endDate = item.c_guarantee_date.Split('-')[1].Trim();
                        if (DateTime.TryParseExact(beginDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            req.c_guarantee_begin_date = !string.IsNullOrEmpty(dt.ToString()) ? dt : DateTime.Parse("1900-01-01");
                        }

                        if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            req.c_guarantee_end_date = !string.IsNullOrEmpty(dt.ToString()) ? dt : DateTime.Parse("1900-01-01");

                        }
                    }
                    else
                    {
                        req.c_guarantee_begin_date = DateTime.Parse("1900-01-01");
                        req.c_guarantee_end_date = DateTime.Parse("1900-01-01");
                    }



                    req.c_guarantee_email = item.c_guarantee_email;
                    req.c_guarantee_name = item.c_guarantee_name;
                    req.c_guarantee_money = item.c_guarantee_money;
                    req.c_position = item.c_position;
                    req.c_note = item.c_note;
                    req.c_guarantee_note = item.c_guarantee_note;
                    if (file_name != "")
                    {
                        req.c_file_name = file_name;
                    }
                    req.c_revenue_date = DateTime.Parse("1900-01-01");
                    req.c_input_date = DateTime.Now;
                    req.RowCreatedAt = DateTime.Now;
                    req.RowCreatedUser = currentUser.UserName;
                    req.RowUpdatedAt = DateTime.Parse("1900-01-01");
                    req.RowUpdatedUser = "";
                    dbConn.Insert(req);
                    pkBookReq = dbConn.GetLastInsertId();
                }
                else
                {
                    req2.c_staff_id = item.c_staff_id;
                    req2.c_code = item.c_code;
                    req2.c_customer_code = item.c_customer_code;
                    req2.c_labels = item.c_labels;
                    req2.c_check_vat = true;
                    req2.c_status = "BOOK_MOI";
                    req2.c_sale_status = "TPKD_CHUA_XAC_NHAN";
                    req2.c_account_status = "TPKD_CHUA_XAC_NHAN";
                    req2.c_monitor_status = "KS_CHUA_XAC_NHAN";
                    req2.c_group_id = getTeamByUser(item.c_staff_id.ToString(), dbConn);

                    if (item.c_guarantee_date != null)
                    {
                        DateTime dt;
                        var beginDate = item.c_guarantee_date.Split('-')[0].Trim();
                        var endDate = item.c_guarantee_date.Split('-')[1].Trim();
                        if (DateTime.TryParseExact(beginDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            req2.c_guarantee_begin_date = !string.IsNullOrEmpty(dt.ToString()) ? dt : DateTime.Parse("1900-01-01");
                        }

                        if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            req2.c_guarantee_end_date = !string.IsNullOrEmpty(dt.ToString()) ? dt : DateTime.Parse("1900-01-01");

                        }
                    }
                    else
                    {
                        req2.c_guarantee_begin_date = DateTime.Parse("1900-01-01");
                        req2.c_guarantee_end_date = DateTime.Parse("1900-01-01");
                    }




                    req2.c_guarantee_email = item.c_guarantee_email;
                    req2.c_guarantee_name = item.c_guarantee_name;
                    req2.c_guarantee_money = item.c_guarantee_money;
                    req2.c_position = item.c_position;
                    req2.c_note = item.c_note;
                    req2.c_guarantee_note = item.c_guarantee_note;
                    if (file_name != "")
                    {
                        req2.c_file_name = file_name;
                    }

                    req2.c_revenue_date = DateTime.Parse("1900-01-01");
                    dbConn.Update(req2);

                    pkBookReq = req2.pk_book_req;
                }

                return Json(new { sussess = true, data = pkBookReq });



            }

        }

        public ActionResult Save([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Book_Location> list, long pkBookReq)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                try
                {

                    if (list != null && ModelState.IsValid)
                    {
                        foreach (var item in list)
                        {
                            if (String.IsNullOrEmpty(item.Website) && String.IsNullOrEmpty(item.Category) && String.IsNullOrEmpty(item.Location) && String.IsNullOrEmpty(item.Nature))
                            {
                                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin : Website, Chuyên mục, Vị trí, Tính chất");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }

                            item.FKBookReq = pkBookReq;
                            item.AccountStatus = "KT_CHUA_XAC_NHAN";
                            item.RowCreatedAt = DateTime.Now;
                            item.RowUpdatedUser = currentUser.UserName;
                            item.RowUpdatedAt = DateTime.Now;
                            item.RowUpdatedUser = currentUser.UserName;
                            if (item.PKBookLocation == 0)
                            {
                                dbConn.Insert(item);
                            }
                            else
                            {
                                dbConn.Update(item);
                            }

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

        private static bool CheckExist(IDbConnection dbConn, CRM_Book_Location item)
        {
            var checkExist = dbConn.Select<CRM_Book_Location>("Website = {0} AND Category = {1} AND Location = {2} AND Nature  = {3} AND NgayLen = {4} AND NgayXuong = {5}", item.Website, item.Category, item.Location, item.Nature, item.NgayLen, item.NgayXuong);
            if (checkExist != null && checkExist.Count > 0)
            {
                //return Json(new { success = true, data = checkExist });
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult getDuplicateData(CRM_Book_Location data)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var checkExist = dbConn.Select<CRM_Book_Location>("Website = {0} AND Category = {1} AND Location = {2} AND Nature  = {3} AND NgayLen = {4} AND NgayXuong = {5}", data.Website, data.Category, data.Location, data.Nature, data.NgayLen, data.NgayXuong);
                if (checkExist != null && checkExist.Count > 0)
                {
                    return Json(new { success = true, data = checkExist });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
        }
        public virtual ActionResult DownloadFile(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/attach-file/booking/"), fileName);
            if (System.IO.File.Exists(fullPath))
                return File(new FileStream(fullPath, FileMode.Open), "text/plain", fileName);
            else
                return null;

        }

        public ActionResult Read_Group([DataSourceRequest] DataSourceRequest request, string Id)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    if (!string.IsNullOrEmpty(Id))
                    {
                        string[] separators = { "@@" };
                        var listProductID = Id.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var pk_book_req = "";
                        foreach (var item in listProductID)
                        {
                            pk_book_req += "'" + item + "',";
                        }
                        pk_book_req = pk_book_req.Substring(0, pk_book_req.Length - 1);
                        string sqlQuery = @"SELECT   req.*
                                                ,SUBSTRING(c_code,3, 10) AS BookCode
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
                                        FROM CRM_Book_Req req
                                        INNER JOIN  CRM_Book_Location l on l.FKBookReq = req.pk_book_req
                                        LEFT JOIN  ERPAPD_Customer cus on cus.CustomerCode = req.c_customer_code
                                        WHERE l.PKBookLocation IN(" + pk_book_req + ")";

                        return Json(KendoApplyFilter.KendoDataByQuery<BookBannerViewModel>(request, sqlQuery, ""));
                    }
                    return Json(KendoApplyFilter.KendoDataByQuery<BookBannerViewModel>(request, "", ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult CheckGroup(string listdata)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    var listIdCheck = "";
                    foreach (var item in listRowID)
                    {
                        listIdCheck += "'" + item + "',";
                    }
                    listIdCheck = listIdCheck.Substring(0, listIdCheck.Length - 1);
                    string str = @"SELECT DISTINCT c_customer_code FROM CRM_Book_Req WHERE pk_book_req IN(" + listIdCheck + ")";
                    var list_customer = dbConn.Select<CRM_Book_Req>(str);
                    if (list_customer.Count > 1)
                    {
                        return Json(new { success = false });
                    }
                  
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
        }
        public ActionResult Group(string listdata, string listrowlocation, string id_keep, string bookcode)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowIDLocation = listrowlocation.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                   
                    //BaoHV
                    foreach (var item in listRowIDLocation)
                    {
                        var newItem = dbConn.FirstOrDefault<CRM_Book_Location>(s => s.PKBookLocation == long.Parse(item));
                        newItem.FKBookReq = long.Parse(id_keep);
                        dbConn.Update(newItem);
                    }
                    //NamNH
                    //foreach (var item in listRowID)
                    //{
                    //    dbConn.Execute(@"UPDATE CRM_Book_Req SET c_code = '" + bookcode + "' WHERE pk_book_req = '" + item + "'");
                    //}
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