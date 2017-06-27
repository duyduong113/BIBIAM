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
    public class CRM_BookingPRController : CustomController
    {
        // GET: CRM_BookingPRBao
        public ActionResult Index(int id)
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            if (id == 1)
                return RedirectToAction("ListBookPRBao", id);
            else
                return RedirectToAction("ListBookPRMoi", id);
        }

        public ActionResult GetListCategory(string web)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var p = new DynamicParameters();
                p.Add("@PageCode", web);
                var data = dbConn.Query<DropDownListData>("p_SelectCRM24H_Category_byPageCode", p, commandType: System.Data.CommandType.StoredProcedure);
                return Json(new { success = true, listCategory = data });
            }
        }
        public ActionResult GetListCategoryValue(string web)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var p = new DynamicParameters();
                p.Add("@PageCode", web);
                var data = dbConn.Query<DropDownListValue>("p_SelectCRM24H_Category_byPageCode2", p, commandType: System.Data.CommandType.StoredProcedure);
                return Json(new { success = true, listCategory = data });
            }
        }

        public static bool CheckCategoryInWebsite(string web, string category)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                string sql = @" SELECT   *   
			                    FROM CRM24H_List 
			                    WHERE FKListtype = 22
				                      AND Status = 1
				                      AND XmlData.value('(/root/data_list/thuoc_website)[1]', 'varchar(50)')  = '" + web + "' AND Code = '" + category + "'";
                var data = dbConn.SqlList<CRM_BookPR_View>(sql);
                if (data.Count == 0)
                {
                    return false;
                }
                return true;
            }
        }
        public static bool CheckScheduleBooking(string action, long id, string type, string website, string category, string location, DateTime dateup, int hour)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                if (action == "update")
                {
                    var bookBao = dbConn.SingleOrDefault<CRM_BookPRLocation>("Website = {0} AND Category = {1} AND Location = {2} AND NgayLen = {3} AND GioLen = {4} AND Type = {5} AND ID <> {6}", website, category, location, dateup, hour, type, id);
                    if (bookBao != null)
                    {
                        return false;
                    }

                }
                else
                {
                    var bookBao = dbConn.SingleOrDefault<CRM_BookPRLocation>("Website = {0} AND Category = {1} AND Location = {2} AND NgayLen = {3} AND GioLen = {4} AND Type = {5} ", website, category, location, dateup, hour, type);
                    if (bookBao != null)
                    {
                        return false;
                    }
                }


                return true;
            }
        }

        public static string strQueryPRBao(string type)
        {
            string str1 = @"SELECT  
                                     a.ID
	                                ,a.PKBookPR
	                                ,a.FKCustomer
	                                ,a.NguoiYeuCauBook
	                                ,a.Code
	                                ,a.ContractCode
	                                ,a.NgayTao
	                                ,a.NguoiTao
	                                ,a.FKContract
	                                ,ISNULL(b.ID,0) AS IDBookPRLocation
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
		                                ELSE  ''
	                                END AS LocationName
                                FROM CRM_BookPR a 
                                LEFT JOIN  CRM_BookPRLocation b on a.PKBookPR = b.FKBookPR
                                LEFT JOIN  CRM24H_Customer cus on a.FKCustomer = cus.ReferID OR a.FKCustomer = cus.CustomerID 
                                WHERE a.PKBookPR <> 0 ";
            string str2 = @"    UNION ALL
                                SELECT 
                                     a.ID
	                                ,a.PKBookPR
	                                ,a.FKCustomer
	                                ,a.NguoiYeuCauBook
	                                ,a.Code
	                                ,a.ContractCode
	                                ,a.NgayTao
	                                ,a.NguoiTao
	                                ,a.FKContract
                                    ,ISNULL(b.ID,0) AS IDBookPRLocation
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
		                                ELSE  ''
	                                END AS LocationName
                                FROM CRM_BookPR a 
                                LEFT JOIN  CRM_BookPRLocation b on a.ID = b.FKBookPR
                                LEFT JOIN  CRM24H_Customer cus on a.FKCustomer = cus.CustomerID OR a.FKCustomer = cus.ReferID 
                                WHERE a.PKBookPR = 0 ";

            return str1 + str2;
        }

        public static string strQueryPRBookCode(string type, string code)
        {
            string str1 = @"SELECT 
		                            a.Code
		                            ,b.*
                            FROM CRM_BookPR a 
                            LEFT JOIN CRM_BookPRLocation b ON a.PKBookPR = b.FKBookPR
                            WHERE b.[Type] = '" + type + "' AND a.PKBookPR <> 0 AND a.Code = " + code;
            string str2 = @"  UNION ALL
                            SELECT 
		                            a.Code
		                            ,b.*
                            FROM CRM_BookPR a 
                            LEFT JOIN CRM_BookPRLocation b ON a.ID = b.FKBookPR
                            WHERE b.[Type] = '" + type + "' AND a.PKBookPR = 0 AND a.Code = " + code;

            return str1 + str2;
        }

        public static string strQueryPRBookCode(string code)
        {
            string s1 = @"  SELECT 
                                 a.ID
	                            ,a.PKBookPR
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
                            FROM CRM24H_BookPR a 
                            LEFT JOIN  CRM24H_BookPRLocation b on a.PKBookPR = b.FKBookPR
                            WHERE b.[Type] = 1 AND b.FKBookPR = " + code;
            string s2 = @"  UNION ALL
                            SELECT a.ID
	                              ,a.PKBookPR
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
                            FROM CRM_BookPR a 
                            LEFT JOIN  CRM_BookPRLocation b on a.PKBookPR = b.FKBookPR
                            WHERE b.[Type] = 1 AND b.FKBookPR = " + code;
            return s1 + s2;
        }

        public static string strQueryPRBaoReport(string web, string cate, string type)
        {

            string str1 = @" SELECT  a.PKBookPR
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
                                FROM CRM_BookPR a 
                                LEFT JOIN  CRM_BookPRLocation b on a.PKBookPR = b.FKBookPR
                                LEFT JOIN  CRM24H_Customer cus on a.FKCustomer = cus.ReferID OR a.FKCustomer = cus.CustomerID 
                                WHERE b.[Type] =  '" + type + "' AND  b.Website = '" + web + "' AND  b.Category ='" + cate + "' AND  b.Status = 1";

            string str2 = @" UNION ALL SELECT  a.PKBookPR
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
                            FROM CRM_BookPR a 
                            LEFT JOIN  CRM_BookPRLocation b on a.ID = b.FKBookPR 
                            LEFT JOIN  CRM24H_Customer cus on a.FKCustomer = cus.ReferID  OR a.FKCustomer = cus.CustomerID 
                            WHERE b.[Type] =  '" + type + "' AND  b.Website = '" + web + "' AND  b.Category ='" + cate + "' AND  b.Status = 1";


            return str1;
        }


        public ActionResult ListBookPRBao(string id = "1")
        {

            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_Employee ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from CRM24H_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE Code IN('DCM','GCM','CMC')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 25"); ;
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_List WHERE PKList IN (724,725,727) ");
                ViewBag.type = id;


                return View("ListBookPRBao");
            }

        }
        public ActionResult ListBookPRMoi(string id = "2")
        {

            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_Employee ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from CRM24H_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE Code IN('DCM','GCM')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 25"); ;
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_List WHERE PKList IN (724,725,727) ");
                ViewBag.type = id;


                return View("ListBookPRMoi");
            }

        }

        public ActionResult ReportBookPRBao(string id = "1")
        {

            using (var dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_Employee ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select PKList AS Code ,Name from CRM24H_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE Code IN('DCM','GCM')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 25");
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select RegionID AS Code,RegionName AS Name from CRM_Location_Region ");
                ViewBag.type = id;
                return View("ReportPRBao");
            }

        }

        public ActionResult BookNewPR(string id)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;

                    ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_Employee ");
                    ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from CRM24H_List WHERE FKListtype = 72");
                    ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 20");
                    ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 22");
                    ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE Code IN('DCM','GCM','CMC')");
                    ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 25"); ;
                    ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_List WHERE PKList IN (724,725,727) ");

                    ViewBag.type = id;
                    var bookcode = dbConn.SingleOrDefault<Parameters>("ParamID = 'BookCode'");
                    if (bookcode != null)
                    {
                        string code = String.Format("{0:000000}", bookcode.Value);
                        ViewBag.BookCode = code;
                        var bk = int.Parse(code) + 1;
                        bookcode.Value = bk.ToString();
                        dbConn.Update(bookcode);
                    }

                    return View("BookNewPRBao");
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult EditBookPRBao(string id)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;

                    ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_Employee ");
                    ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from CRM24H_List WHERE FKListtype = 72");
                    ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 20");
                    ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 22");
                    ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE Code IN('DCM','GCM','CMC')");
                    ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE FKListtype = 25"); ;
                    ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from CRM24H_List WHERE PKList IN (724,725,727) ");

                    var p = new DynamicParameters();
                    p.Add("@code", id);

                    var customer = dbConn.Query<CRM_BookPR_View>("p_GetCustomer_BookPRByBookCode", p, commandType: System.Data.CommandType.StoredProcedure);
                    foreach (var item in customer)
                    {
                        ViewBag.Customer = item.FKCustomer;

                        if (item.FKCustomer != 0)
                        {
                            var a = dbConn.SingleOrDefault<CRM24H_Customer>("ReferID = {0}", item.FKCustomer);
                            var b = dbConn.SingleOrDefault<CRM24H_Customer>("CustomerID = {0}", item.FKCustomer);

                            if (dbConn.SingleOrDefault<CRM24H_Customer>("CustomerID = {0}", item.FKCustomer) != null)
                            {
                                ViewBag.customerName = dbConn.SingleOrDefault<CRM24H_Customer>("CustomerID = {0}", item.FKCustomer).CustomerName;
                            }
                            else
                            {
                                if (dbConn.SingleOrDefault<CRM24H_Customer>("ReferID = {0}", item.FKCustomer) != null)
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

                        ViewBag.bookcode = item.Code; ;
                        //ViewBag.fk_book = id;
                        ViewBag.type = item.Type;
                        break;
                    }



                    return View("EditBookingPRBao");
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult BookPRBao_Read([DataSourceRequest] DataSourceRequest request, string type)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    string strQuery = strQueryPRBao(type);

                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_BookPR_View>(request, strQuery, ""));
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
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_BookPRLocation>(request, strQueryPRBookCode(type, bookcode), ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [ValidateInput(false)]
        public ActionResult BookPRBao_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_BookPRLocation> list, string customerid, string staff, string bookcode, string type)
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
                            // check data in header booking
                            if (string.IsNullOrEmpty(customerid) || string.IsNullOrEmpty(staff) || string.IsNullOrEmpty(bookcode))
                            {
                                ModelState.AddModelError("", "Vui lòng nhập Khách hàng");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }

                            var staffId = dbConn.SingleOrDefault<EmployeeInfo>("UserName ={0}", staff);

                            var data = new CRM_BookPR();
                            data.Code = bookcode;
                            data.FKCustomer = int.Parse(customerid);
                            data.NguoiYeuCauBook = staffId == null ? 0 : staffId.RefStaffId;
                            data.ContractCode = "";
                            data.NgayTao = DateTime.Now;
                            data.NguoiTao = staffId == null ? 0 : staffId.RefStaffId;
                            data.RowCreatedUser = currentUser.UserName;
                            data.RowCreatedAt = DateTime.Now;
                            dbConn.Insert(data);

                            var PKBookPR = dbConn.GetLastInsertId();

                            foreach (var item in list)
                            {
                                //var prLocation = new CRM_BookPRLocation();
                                if (!string.IsNullOrEmpty(item.Website.ToString()) && !string.IsNullOrEmpty(item.Category.ToString()) && !string.IsNullOrEmpty(item.Location.ToString()) && !string.IsNullOrEmpty(item.NgayLen.ToString()) && !string.IsNullOrEmpty(item.GioLen.ToString()))
                                {
                                    //check DateUp
                                    if (item.NgayLen.Date < DateTime.Now.Date)
                                    {
                                        ModelState.AddModelError("", "Ngày book >= Hiện tại");
                                        return Json(list.ToDataSourceResult(request, ModelState));
                                    }

                                    //// check Category
                                    //if (!CheckCategoryInWebsite(prLocation.Website.ToString(), prLocation.Category.ToString()))
                                    //{
                                    //    ModelState.AddModelError("", "Chuyên mục " + item.Category + " không thuộc website " + item.Website);
                                    //    return Json(list.ToDataSourceResult(request, ModelState));
                                    //}

                                    // check lịch booking
                                    if (!CheckScheduleBooking("create", 0, type, item.Website.ToString(), item.Category.ToString(), item.Location.ToString(), item.NgayLen, item.GioLen))
                                    {
                                        ModelState.AddModelError("", "Lịch book bị trùng");
                                        dbTrans.Rollback();
                                        return Json(list.ToDataSourceResult(request, ModelState));
                                    }

                                    item.FKBookPR = PKBookPR;
                                    item.Website = item.Website;
                                    item.Category = item.Category;
                                    item.NgayLen = item.NgayLen;
                                    item.Location = item.Location;
                                    item.Link = item.Link;
                                    item.GioLen = item.GioLen;
                                    item.VungMien = item.VungMien;
                                    item.Status = 0;
                                    item.Type = int.Parse(type);
                                    dbConn.Insert(item);


                                }
                                else
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập các thông tin bắt buộc");
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

        [ValidateInput(false)]
        public ActionResult BookPRBao_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_BookPRLocation> list, string customerid, string staff, string bookcode)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                try
                {
                    if (list != null && ModelState.IsValid)
                    {
                        foreach (var item in list)
                        {
                            if (!string.IsNullOrEmpty(item.Website.ToString()) && !string.IsNullOrEmpty(item.Category.ToString()) && !string.IsNullOrEmpty(item.Location.ToString()) && !string.IsNullOrEmpty(item.NgayLen.ToString()) && !string.IsNullOrEmpty(item.GioLen.ToString()))
                            {
                                //check DateUp
                                if (item.NgayLen.Date < DateTime.Now.Date)
                                {
                                    ModelState.AddModelError("", "Ngày book >= Hiện tại");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }

                                //// check Category
                                //if (!CheckCategoryInWebsite(prLocation.Website.ToString(), prLocation.Category.ToString()))
                                //{
                                //    ModelState.AddModelError("", "Chuyên mục " + item.Category + " không thuộc website " + item.Website);
                                //    return Json(list.ToDataSourceResult(request, ModelState));
                                //}

                                // check lịch booking
                                if (!CheckScheduleBooking("update", item.ID, item.Type.ToString(), item.Website.ToString(), item.Category.ToString(), item.Location.ToString(), item.NgayLen, item.GioLen))
                                {
                                    ModelState.AddModelError("", "ID:  " + item.ID + ". Lịch book bị trùng");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }

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

        public ActionResult BookPRBao_ChangeStatus(string listdata, string action, string from)
        {
            try
            {
                using (IDbConnection dbConn = CRM24H.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);


                    foreach (var item in listRowID)
                    {

                        var pr_location = dbConn.SingleOrDefault<CRM_BookPRLocation>("ID = {0}", item);
                        if (pr_location != null)
                        {
                            if (action == "DUYET")
                            {
                                pr_location.Status = 1;
                                dbConn.Update<CRM_BookPRLocation>(pr_location);
                            }
                            if (action == "HUY")
                            {
                                pr_location.Status = 0;
                                dbConn.Update<CRM_BookPRLocation>(pr_location);
                            }
                            if (action == "GOP")
                            {
                                pr_location.Status = 0;
                                dbConn.Update<CRM_BookPRLocation>(pr_location);
                            }


                            if (action == "XOA")
                            {
                                dbConn.Delete<CRM_BookPRLocation>("ID = {0}", item);
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

        public ActionResult GetDataReportPRBao(string website, string category, string type)
        {

            using (var dbConn = OrmliteConnection.openConn())
            {
                var listTime = dbConn.Select<DropDownListData>("select Code ,Name from CRM24H_List WHERE FKListtype = 72");
                var location = dbConn.Select<DropDownListData>("select PKList AS Code,Name from CRM24H_List WHERE Code IN('DCM','GCM')");

                var data = dbConn.SqlList<CRM_BookPR_View>("EXEC getReportBookPR @web,@category,@type", new { web = website, category = category, type = type });
                return Json(new { success = true, location = location, listTime = listTime, data = data });
            }
        }

    }
}