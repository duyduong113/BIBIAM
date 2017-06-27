using ERPAPD.Models;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ERPAPD.Helpers;
using System;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using Dapper;
using System.Data;

namespace ERPAPD.Controllers
{
    public class CRM_BookPRController : CustomController
    {
        // GET: CRM_BookPR
        public ActionResult Index()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefstaffId AS Code ,FullName AS Name from EmployeeInfo");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM','CMC')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");

                return View();
            }
        }

        public ActionResult GetListCategory(string web)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var p = new DynamicParameters();
                p.Add("@PageCode", web);
                var data = dbConn.Query<DropDownListData>("p_SelectERPAPD_Category_byPageCode", p, commandType: System.Data.CommandType.StoredProcedure);
                return Json(new { success = true, listCategory = data });
            }
        }
        public ActionResult GetListCategoryValue(string web)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var p = new DynamicParameters();
                p.Add("@PageCode", web);
                var data = dbConn.Query<DropDownListValue>("p_SelectERPAPD_Category_byPageCode2", p, commandType: System.Data.CommandType.StoredProcedure);
                return Json(new { success = true, listCategory = data });
            }
        }


        public static string strQueryPRBao()
        {
            string str1 = @"SELECT  
	                                 a.PKBookPR
	                                ,a.FKCustomer
	                                ,a.NguoiYeuCauBook
	                                ,a.Code
	                                ,a.ContractCode
	                                ,a.NgayTao
	                                ,a.NguoiTao
	                                ,a.FKContract
	                                ,ISNULL(b.PKBookPRLocation,0) AS PKBookPRLocation
	                                ,ISNULL(b.FKBookPR,0) AS FKBookPR
	                                ,ISNULL(b.Website,0) AS Website
	                                ,ISNULL(b.Category,0) AS Category
	                                ,ISNULL(b.Location,0) AS Location
	                                ,ISNULL(b.Nature,0) AS Nature
	                                ,ISNULL(b.NgayLen,'1900-01-01') AS NgayLen
	                                ,ISNULL(b.GioLen,0) AS GioLen
	                                ,ISNULL(b.GioXuong,0) AS GioXuong
	                                ,ISNULL(b.Link,'') AS Link
	                                ,ISNULL(b.VungMien,0) AS VungMien
	                                ,ISNULL(b.IdOCM,0) AS IdOCM
	                                ,ISNULL(b.[Status],0) AS [Status]
	                                ,ISNULL(b.DonGia,0) AS DonGia
	                                ,ISNULL(b.SoLuong,0) AS SoLuong
	                                ,ISNULL(b.GhiChu,'') AS GhiChu
	                                ,ISNULL(b.PublishDate,'1900-01-01') AS PublishDate
	                                ,ISNULL(b.PublishUser,'') AS PublishUser
	                                ,ISNULL(b.Km,0) AS Km
	                                ,ISNULL(b.[Type],0) AS [Type]
                                    ,ISNULL(cus.CustomerName,'') AS CustomerName
                                    , CASE 
		                                WHEN ISNULL(b.Location,'') = 764  AND [Type] = 1 THEN N'Giữa CM' 
		                                WHEN ISNULL(b.Location,'') = 765  AND [Type] = 1 THEN N'Đầu CM' 
                                        WHEN ISNULL(b.Location,'') = 771  AND [Type] = 1 THEN N'CM Con' 
		                                WHEN ISNULL(b.Location,'') = 765  AND [Type] = 2 THEN N'PR Loại 1' 
		                                WHEN ISNULL(b.Location,'') = 764  AND [Type] = 2 THEN N'PR Loại 2' 
                                        WHEN ISNULL(b.Location,'') = 771  AND [Type] = 2 THEN N'PR Mua Thêm' 
		                                ELSE  ''
	                                END AS LocationName
                                    
                                FROM CRM_Book_PR a 
                                INNER JOIN  CRM_BookPR_Location b on a.PKBookPR = b.FKBookPR
                                LEFT JOIN  ERPAPD_Customer cus on a.FKCustomer = cus.ReferID OR a.FKCustomer = cus.CustomerID 
                             ";

            return str1;
        }
        public static string strQueryPRDetail(long PKBookPR)
        {
            string str1 = @"SELECT 
		                             a.Code
		                            ,b.*
                            FROM CRM_Book_PR a 
                            INNER JOIN CRM_BookPR_Location b ON a.PKBookPR = b.FKBookPR
                            WHERE b.FKBookPR = '" + PKBookPR + "'";
            return str1;
        }

        public ActionResult BookNewPR(int id)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;

                    ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code, FullName AS Name from EmployeeInfo ");
                    ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                    ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                    ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");

                    ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                    ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");

                    ViewBag.type = id;
                    if (id == 2) // Mới
                    {
                        List<DropDownListData> dp = new List<DropDownListData>();
                        dp.Add(new DropDownListData(764, "PR Loại 1"));
                        dp.Add(new DropDownListData(765, "PR Loại 2"));
                        dp.Add(new DropDownListData(771, "PR Mua thêm"));
                        ViewBag.listLocation = dp;
                    }
                    else
                    {
                        ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM','CMC')");
                    }

                    var bookcode = dbConn.SingleOrDefault<Parameters>("ParamID = 'BookCode'");
                    if (bookcode != null)
                    {
                        string code = String.Format("{0:000000}", bookcode.Value);
                        ViewBag.BookCode = code;
                        var bk = int.Parse(code) + 1;
                        bookcode.Value = bk.ToString();
                        dbConn.Update(bookcode);
                    }

                    return View("BookNewPR");
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult EditBookPR(long id)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;

                    ViewBag.PKBookPR = id;
                    ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code, FullName AS Name from EmployeeInfo ");
                    ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                    ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                    ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                    ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM','CMC')");
                    ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                    ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");

                    string query = @"SELECT a.Code,a.FKCustomer,a.NguoiYeuCauBook,b.[Type]
                                    FROM CRM_Book_PR a 
                                    INNER JOIN  CRM_BookPR_Location b on a.PKBookPR = b.FKBookPR
                                    WHERE  a.PKBookPR = " + id;

                    var header = dbConn.SingleOrDefault<CRM_Book_PR_View>(query);
                    if (header != null)
                    {
                        ViewBag.Customer = header.FKCustomer;
                        ViewBag.bookcode = header.Code;
                        ViewBag.type = header.Type;
                        ViewBag.StaffName = dbConn.SingleOrDefault<EmployeeInfo>("RefStaffId = {0}", header.NguoiYeuCauBook) == null ? "" : dbConn.SingleOrDefault<EmployeeInfo>("RefStaffId = {0}", header.NguoiYeuCauBook).FullName;
                        if (header.FKCustomer != 0)
                        {
                            var a = dbConn.SingleOrDefault<ERPAPD_Customer>("ReferID = {0}", header.FKCustomer);
                            var b = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID = {0}", header.FKCustomer);

                            if (dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID = {0}", header.FKCustomer) != null)
                            {
                                ViewBag.customerName = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID = {0}", header.FKCustomer).CustomerName;
                            }
                            else
                            {
                                if (dbConn.SingleOrDefault<ERPAPD_Customer>("ReferID = {0}", header.FKCustomer) != null)
                                {
                                    ViewBag.customerName = a.CustomerName;
                                }
                                else
                                {
                                    ViewBag.customerName = "";
                                }

                            }
                        }
                        else
                        {
                            ViewBag.customerName = "";
                        }

                    }

                    return View();
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    string strQuery = strQueryPRBao();

                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_Book_PR_View>(request, strQuery, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult ReadDetail([DataSourceRequest] DataSourceRequest request, long pkbook = 0)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_BookPR_Location>(request, strQueryPRDetail(pkbook), ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult BookPRBao_ReadByBookCode([DataSourceRequest] DataSourceRequest request, string type, string bookcode)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_BookPR_View>(request, strQueryPRBookCode(type, bookcode), ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public static string strQueryPRBookCode(string type, string code)
        {
            string str1 = @"SELECT 
		                            a.Code as Code,
                                    a.NguoiYeuCauBook,
                                    a.NgayTao,
									D.CustomerName as CustomerName,
		                            b.*
                            FROM CRM_Book_PR a 
                            JOIN CRM_BookPR_Location b ON a.PKBookPR = b.FKBookPR
							LEFT JOIN CRM_Contract C ON c.c_code=A.ContractCode and c.c_code <> ''
							LEFT JOIN ERPAPD_Customer D ON D.CustomerID=a.FKCustomer OR a.FKCustomer = D.ReferID 
                            WHERE b.[Type] = '" + type + "' AND a.Code = " + code;
            
            return str1;
        }
        [ValidateInput(false)]
        public ActionResult SaveBook([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_BookPR_Location> list, string customerid, string staff, string bookcode, int type)
        {
            using (
                var dbConn = OrmliteConnection.openConn())
            {
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                        
                            if (string.IsNullOrEmpty(customerid) || string.IsNullOrEmpty(staff) || string.IsNullOrEmpty(bookcode))
                            {
                                ModelState.AddModelError("", "Vui lòng nhập thông tin Khách hàng");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }

                            if (string.IsNullOrEmpty(customerid) || string.IsNullOrEmpty(staff) || string.IsNullOrEmpty(bookcode))
                            {
                                ModelState.AddModelError("", "Vui lòng nhập thông tin Khách hàng");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }

                          
                            long pk = 0;
                            var header = dbConn.SingleOrDefault<CRM_Book_PR>("Code = {0}", bookcode);
                            if (header == null)
                            {
                                var data = new CRM_Book_PR();
                                data.Code = bookcode;
                                data.FKCustomer = int.Parse(customerid);
                                data.NguoiYeuCauBook = int.Parse(staff);
                                data.ContractCode = "";
                                data.NgayTao = DateTime.Now;
                                data.NguoiTao = int.Parse(staff);
                                //dbConn.Insert(data);
                                pk = CRM_Book_PR.Save(data, currentUser.UserName);
                            }
                            else
                            {
                                pk = CRM_Book_PR.Save(header, currentUser.UserName);
                            }

                            foreach (var item in list)
                            {
                                if (item.Website != 0 && item.Category !=0 && item.Location != 0 )
                                {
                                    item.FKBookPR = pk;
                                    item.Website = item.Website;
                                    item.Category = item.Category;
                                    item.NgayLen = item.NgayLen;
                                    item.Location = item.Location;
                                    item.Link = item.Link;
                                    item.GioLen = item.GioLen;
                                    item.VungMien = item.VungMien;
                                    item.Status = 0;
                                    item.Type = type;
                                    CRM_BookPR_Location.Save(item, currentUser.UserName);
                                    //dbConn.Insert(item);


                                }
                                else
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập các thông tin Website, Chuyên mục, Vị trí");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                            }

                            dbTrans.Commit();
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
                        dbTrans.Rollback();
                        ModelState.AddModelError("error", e.Message);
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }

                }

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
                        if (action == "DUYET")
                        {
                            dbConn.Execute(@"UPDATE CRM_BookPR_Location SET Status = 1 WHERE PKBookPRLocation = '" + item + "'");
                        }
                        if (action == "HUY")
                        {
                            dbConn.Execute(@"UPDATE CRM_BookPR_Location SET Status = 0 WHERE PKBookPRLocation = '" + item + "'");
                        }

                        if (action == "XOA")
                        {
                            dbConn.Delete<CRM_BookPR_Location>("PKBookPRLocation = {0}", item);
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

        public ActionResult ReportBook(string id = "1")
        {

            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select RefStaffId AS Code, FullName AS Name from EmployeeInfo ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select PKList AS Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select RegionID AS Code,RegionName AS Name from CRM_Location_Region ");
                ViewBag.type = id;
                return View("ReportPR");
            }

        }

        public ActionResult GetDataReportPR(string website, string category, string type)
        {

            using (var dbConn = OrmliteConnection.openConn())
            {
                var location = new List<DropDownListData>();
                var listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                if (type == "2") // Mới
                {
                    List<DropDownListData> dp = new List<DropDownListData>();
                    dp.Add(new DropDownListData(764, "PR Loại 1"));
                    dp.Add(new DropDownListData(765, "PR Loại 2"));
                    //dp.Add(new DropDownListData(771, "PR Mua thêm"));
                    location = dp;
                }
                else
                {
                    location = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM')");
                }

                var data = dbConn.SqlList<CRM_Book_PR_View>("EXEC getReportBookPR @web,@category,@type", new { web = website, category = category, type = type });
                return Json(new { success = true, location = location, listTime = listTime, data = data });
            }
        }
    }
}