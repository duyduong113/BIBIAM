using ERPAPD.Helpers;
using ERPAPD.Models;
using Dapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class CRM_ProposedAdvertisementController : CustomController
    {
        // GET: CRM_ProposedAdvertisement
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
                        ViewBag.Group = currentUser.Groups;
                        ViewBag.IdLeader = -1;
                        var listgroup = currentUser.Groups;
                        int group = 0;
                        if (listgroup.Count > 0)
                        {
                            foreach (var item in listgroup)
                            {
                                if (item.Id == 57)
                                {
                                    //KT
                                    group = item.Id;
                                    break;
                                }
                                if (item.Id == 50)
                                {
                                    //KinhDoanh
                                    group = item.Id;
                                    break;
                                }
                            }
                        }

                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                        ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "TTDUYETQC").OrderBy(s => s.ID);
                        ViewBag.listStatusQC = dbConn.Select<Parameters>("Type ={0}", "TTXBQC").OrderBy(s => s.ID);
                        ViewBag.listLabel = dbConn.Select<CRM_Label>();
                        ViewBag.listTypeAdv = dbConn.Select<Parameters>("Type='LoaiDangQC'").OrderBy(s => s.ID);
                        //ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_Employee ");
                        ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                        ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                        ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                        ViewBag.listLocationBanner = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List");
                        ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListType =23");
                        ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                        ViewBag.listRegionIndex = dbConn.Select<DropDownListData>("SELECT [HierarchyID] AS Code,[Value] AS Name FROM [CRM_Hierarchy] WHERE HierarchyID IN (14,12,13) order by[HierarchyID] ASC");
                        ViewBag.listStaffIndex = dbConn.Select<DropDownListData>("select RefStaffId AS Code, FullName AS Name from EmployeeInfo");
                        if (currentUser.UserName == "administrator")
                        {
                            ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13,14) order by [HierarchyID] ASC");
                            ViewBag.listTeam = dbConn.Select<CRM_Team>(@"SELECT * FROM [CRM_Team] WHERE FKUnit IN (12,13)");
                            ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT RefStaffId,UserName,FullName from EmployeeInfo Where DepartmentID IN (28,30)");
                            ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36)");
                            ViewData["FlagViewAll"] = true;
                            ViewData["KT"] = false;
                            ViewData["TeamLeader"] = false;
                        }
                        else
                        {
                            var RegionbyUser = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,DepartmentID,Team,Region FROM EmployeeInfo WHERE UserName='" + currentUser.UserName + "'");
                            //Role KT
                            if (group == 57)
                            {
                                ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13) order by [HierarchyID] ASC");
                                ViewBag.listTeam = dbConn.Select<CRM_Team>("FKUnit={0}", RegionbyUser.Region);
                                ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId,Region from EmployeeInfo Where DepartmentID in(28,30) ");
                                ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36) and UserName={0}", RegionbyUser.UserName); //Nhóm phòng kế toán
                                ViewData["KT"] = true;
                                ViewData["FlagViewAll"] = false;
                                ViewData["TeamLeader"] = false;
                            }
                            // Nhóm KT
                            // 50 -12 : kế toán Bắc
                            // 51 -13 : Kế toán miền Nam
                            // Nhân viên
                            else
                            {
                                ViewData["KT"] = false;
                                ViewData["FlagViewAll"] = false;
                                if (!string.IsNullOrEmpty(RegionbyUser.Team))
                                {
                                    // var exit = dbConn.FirstOrDefault<CRM_Team>(@"	SELECT * FROM  CRM_Team  Where Manager='" + currentUser.UserName + "'");
                                    if (RegionbyUser.Position == "16")
                                    {
                                        // Nhóm trưởng
                                        ViewBag.Leader = RegionbyUser.UserName;
                                        ViewData["TeamLeader"] = true;
                                        ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("HierarchyID={0}", RegionbyUser.Region);
                                        ViewBag.listTeam = dbConn.Select<CRM_Team>("TeamID={0}", RegionbyUser.Team);
                                        ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId from EmployeeInfo WHERE Team={0}", RegionbyUser.Team);
                                        ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36) AND Region={0}", RegionbyUser.Region);
                                        ViewBag.IdLeader = RegionbyUser.RefStaffId;
                                    }
                                    else
                                    {
                                        ViewBag.Leader = "";
                                        ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("HierarchyID={0}", RegionbyUser.Region);
                                        ViewBag.listTeam = dbConn.Select<CRM_Team>("TeamID={0}", RegionbyUser.Team);
                                        ViewBag.listStaff = dbConn.Select<EmployeeInfo>(s => s.UserName == currentUser.UserName);
                                        ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36) AND Region={0}", RegionbyUser.Region);
                                        ViewData["TeamLeader"] = false;

                                    }
                                }
                                else
                                {
                                    ViewBag.Leader = "";
                                }
                            }
                        }
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
        public ActionResult Index2()
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
                        ViewBag.Group = currentUser.Groups;
                        ViewBag.IdLeader = -1;
                        var listgroup = currentUser.Groups;
                        int group = 0;
                        if (listgroup.Count > 0)
                        {
                            foreach (var item in listgroup)
                            {
                                if (item.Id == 57)
                                {
                                    //KT
                                    group = item.Id;
                                    break;
                                }
                                if (item.Id == 50)
                                {
                                    //KinhDoanh
                                    group = item.Id;
                                    break;
                                }
                            }
                        }

                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                        ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "TTDUYETQC").OrderBy(s => s.ID);
                        ViewBag.listStatusQC = dbConn.Select<Parameters>("Type ={0}", "TTXBQC").OrderBy(s => s.ID);
                        ViewBag.listWebsite = dbConn.Select<ERPAPD_List>(@"select  PKList,Name from ERPAPD_List where FKListtype = 20");
                        ViewBag.listCategory = dbConn.Select<ERPAPD_List>(@"select   PKList,Name from ERPAPD_List where FKListtype = 22");
                        ViewBag.listLabel = dbConn.Select<CRM_Label>();
                        ViewBag.listTypeAdv = dbConn.Select<Parameters>("Type='LoaiDangQC'").OrderBy(s => s.ID);



                        if (currentUser.UserName == "administrator")
                        {
                            ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13,14)");
                            ViewBag.listTeam = dbConn.Select<CRM_Team>(@"SELECT * FROM [CRM_Team] WHERE FKUnit IN (12,13)");
                            ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId from EmployeeInfo Where DepartmentID IN (28,30)");
                            ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36)");
                            ViewData["FlagViewAll"] = true;
                            ViewData["KT"] = false;
                            ViewData["TeamLeader"] = false;
                        }
                        else
                        {
                            var RegionbyUser = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,DepartmentID,Team,Region FROM EmployeeInfo WHERE UserName='" + currentUser.UserName + "'");
                            //Role KT
                            if (group == 57)
                            {
                                ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13)");
                                ViewBag.listTeam = dbConn.Select<CRM_Team>("FKUnit={0}", RegionbyUser.Region);
                                ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId,Region from EmployeeInfo Where DepartmentID in(28,30) ");
                                ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36) and UserName={0}", RegionbyUser.UserName); //Nhóm phòng kế toán
                                ViewData["KT"] = true;
                                ViewData["FlagViewAll"] = false;
                                ViewData["TeamLeader"] = false;
                            }
                            // Nhóm KT
                            // 50 -12 : kế toán Bắc
                            // 51 -13 : Kế toán miền Nam

                            // Nhân viên
                            else
                            {
                                ViewData["KT"] = false;
                                ViewData["FlagViewAll"] = false;
                                if (!string.IsNullOrEmpty(RegionbyUser.Team))
                                {
                                    if (RegionbyUser.Position == "16")
                                    {
                                        // Nhóm trưởng
                                        ViewBag.Leader = RegionbyUser.UserName;
                                        ViewData["TeamLeader"] = true;
                                        ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("HierarchyID={0}", RegionbyUser.Region);
                                        ViewBag.listTeam = dbConn.Select<CRM_Team>("TeamID={0}", RegionbyUser.Team);
                                        ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId from EmployeeInfo WHERE Team={0}", RegionbyUser.Team);
                                        ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36) AND Region={0}", RegionbyUser.Region);
                                        ViewBag.IdLeader = RegionbyUser.RefStaffId;
                                    }
                                    else
                                    {
                                        ViewBag.Leader = "";
                                        ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("HierarchyID={0}", RegionbyUser.Region);
                                        ViewBag.listTeam = dbConn.Select<CRM_Team>("TeamID={0}", RegionbyUser.Team);
                                        ViewBag.listStaff = dbConn.Select<EmployeeInfo>(s => s.UserName == currentUser.UserName);
                                        ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36) AND Region={0}", RegionbyUser.Region);
                                        ViewData["TeamLeader"] = false;

                                    }
                                }
                                else
                                {
                                    ViewBag.Leader = "";
                                }
                            }
                        }
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
                    string whereCondition = "";
                    if (request.Filters.Count > 0)
                    {
                        whereCondition = " AND " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    }
                    data = new CRM_Adv().GetPage(request, whereCondition);
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ReadDetail([DataSourceRequest]DataSourceRequest request, int pk_adv)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    try
                    {
                        var us = new DynamicParameters();
                        us.Add("@fk_adv", pk_adv);
                        var data = dbConn.Query<CRM_Adv_Services_View>("p_getDetail_DNQC", us, commandType: System.Data.CommandType.StoredProcedure);
                        return Json(data.ToDataSourceResult(request));

                    }
                    catch (Exception ex)
                    {

                        return Json(new { success = false, message = ex.Message });
                    }

                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        // GET data open Pop-up : DUYỆT/ TỪ CHỐI/ XÁC NHẬN LÊN TRANG
        public ActionResult ReadDetail_Orders(int pkAdv, int c_dot_order)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    try
                    {
                        var data = dbConn.Select<CRM_Adv_Orders>(@" select *,
                             ISNULL((select TOP 1 FullName from EmployeeInfo where RefStaffId=c_nguoi_xac_nhan),'') as c_ten_nguoi_xac_nhan
                             from CRM_Adv_Orders
                             where fk_adv= " + pkAdv + " and c_dot_order=" + c_dot_order);
                        if (data.Count > 0)
                        {
                            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var listOrder = dbConn.Select<CRM_Adv_Orders>(@"SELECT 1 as pk_order,pk_adv,B.c_dot_order as c_dot_order,A.c_ngay_yc_duyet AS c_ngay_yc,
	                                    c_noi_dung_gui_duyet as c_noi_dung_yc,
	                                    ISNULL(C.c_type,0) as c_trang_thai,
	                                    c_dinh_muc_bn as c_han_muc_bn,
	                                    c_dinh_muc_pr_goi AS c_han_muc_goi_pr,
	                                    c_dinh_muc_pr_thuong as c_han_muc_pr_thuong,
	                                    c_dinh_muc_cpm as c_han_muc_cpm,
	                                    c_from_date,c_to_date,
	                                    A.c_note as c_ghi_chu,
                                        fk_nguoi_duyet as c_nguoi_duyet,
	                                    (select TOP 1 FullName from EmployeeInfo where RefStaffId=fk_nguoi_duyet  and fk_nguoi_duyet <> 0 ) as c_ten_nguoi_duyet,
	                                    A.c_ngay_duyet,
	                                    c_noi_dung_duyet,
										B.fk_staff_up_ocm,
	                                    B.c_ngay_up_ocm AS c_ngay_xac_nhan,
                                        c_noi_dung_xn_dang_dv as c_noi_dung_xac_nhan,
										(CASE WHEN B.fk_staff_up_ocm=0 THEN 0 ELSE 1 END) AS c_xac_nhan_len_trang,
										ISNULL((select TOP 1 FullName from EmployeeInfo where RefStaffId=B.fk_staff_up_ocm and B.fk_staff_up_ocm <> 0),'') as c_ten_nguoi_xac_nhan
                                       FROM CRM_Adv  A
									   LEFT JOIN 
									   CRM_Adv_Services B
									   ON A.pk_adv=B.fk_adv
									   LEFT JOIN CRM_Adv_Duyet C
									   ON C.fk_adv=B.fk_adv and C.c_dot_order=B.c_dot_order
                                       where A.pk_adv={0} and B.c_dot_order={1}", pkAdv, c_dot_order);
                            if (listOrder.Count> 0)
                            {
                                return Json(new { success = true, data = listOrder }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                        return Json(new { success = false, message = ex.Message });
                    }

                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult UpdateStatus(CRM_Adv_Orders items, string keySelected)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (items.ActionSelected == "GUIDUYET")
                        {
                            var exit = dbConn.Where<CRM_Adv_Orders>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order).FirstOrDefault();
                            if (exit != null)
                            {
                                exit.c_noi_dung_yc = items.c_noi_dung_yc;
                                exit.c_ngay_yc = DateTime.Now;
                                exit.c_trang_thai = 0;
                                exit.c_dot_order = items.c_dot_order; //dbConn.SingleOrDefault<CRM_Adv_Services>("pk_services = {0}", items.fk_service) != null ? dbConn.SingleOrDefault<CRM_Adv_Services>("pk_services = {0}", items.fk_service).c_dot_order : 0;
                                exit.RowUpdatedAt = DateTime.Now;
                                exit.RowUpdatedUser = currentUser.UserName;
                                exit.c_nguoi_xac_nhan = -1;
                                CRM_Adv_Orders.Save(exit, currentUser.UserName);
                            }
                            else
                            {
                                CRM_Adv_Orders od = new CRM_Adv_Orders();
                                od.fk_adv = items.fk_adv;
                                od.c_dot_order = items.c_dot_order;
                                od.c_noi_dung_yc = items.c_noi_dung_yc;
                                od.c_ngay_yc = DateTime.Now;
                                od.c_nguoi_xac_nhan = -1;
                                od.c_trang_thai = 0;
                                CRM_Adv_Orders.Save(od, currentUser.UserName);
                            }
                            var duyet = dbConn.Where<CRM_Adv_Duyet>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order).FirstOrDefault();
                            if (duyet != null)
                            {
                                duyet.fk_adv = items.fk_adv;
                                duyet.c_type = 0;
                                duyet.c_dot_order = items.c_dot_order;
                                duyet.c_ngay_yc_duyet = DateTime.Now;
                                CRM_Adv_Duyet.Save(duyet, currentUser.UserName);
                            }
                            else
                            {
                                CRM_Adv_Duyet d = new CRM_Adv_Duyet();
                                d.fk_adv = items.fk_adv;
                                duyet.c_type = 0;
                                d.c_dot_order = items.c_dot_order;
                                d.c_ngay_yc_duyet = DateTime.Now;
                                CRM_Adv_Duyet.Save(d, currentUser.UserName);
                            }
                        }
                        else if (items.ActionSelected == "DUYET")
                        {
                            var exit = dbConn.Where<CRM_Adv_Orders>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order).FirstOrDefault();
                            if (exit != null)
                            {
                                if (items.c_trang_thai == 0)
                                {
                                    exit.c_from_date = DateTime.Parse("1900-01-01"); ;
                                    exit.c_to_date = DateTime.Parse("1900-01-01"); ;
                                    exit.c_nguoi_duyet = -1;
                                    exit.c_ngay_duyet = DateTime.Parse("1900-01-01");
                                }
                                else
                                {
                                    exit.c_from_date = !string.IsNullOrEmpty(items.c_from_dateString) ? DateTime.Parse(items.c_from_dateString) : DateTime.Parse("1900-01-01"); ;
                                    exit.c_to_date = !string.IsNullOrEmpty(items.c_to_dateString) ? DateTime.Parse(items.c_to_dateString) : DateTime.Parse("1900-01-01"); ;
                                    exit.c_nguoi_duyet = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId;
                                    exit.c_ngay_duyet = DateTime.Now;
                                }
                                exit.c_trang_thai = items.c_trang_thai;
                                exit.c_nguoi_xac_nhan = -1;
                                exit.c_noi_dung_duyet = items.c_ghi_chu;
                                exit.c_dot_order = items.c_dot_order;
                                exit.c_han_muc_bn = items.c_han_muc_bn;
                                exit.c_han_muc_goi_pr = items.c_han_muc_goi_pr;
                                exit.c_han_muc_pr_thuong = items.c_han_muc_pr_thuong;
                                exit.c_han_muc_cpm = items.c_han_muc_cpm;
                                exit.RowUpdatedAt = DateTime.Now;
                                exit.RowUpdatedUser = currentUser.UserName;
                                CRM_Adv_Orders.Save(exit, currentUser.UserName);
                            }
                            else
                            {
                                CRM_Adv_Orders od = new CRM_Adv_Orders();
                                od.fk_adv = items.fk_adv;
                                od.c_dot_order = items.c_dot_order;
                                od.c_han_muc_bn = items.c_han_muc_bn;
                                od.c_han_muc_goi_pr = items.c_han_muc_goi_pr;
                                od.c_han_muc_pr_thuong = items.c_han_muc_pr_thuong;
                                od.c_han_muc_cpm = items.c_han_muc_cpm;
                                od.c_trang_thai = items.c_trang_thai;
                                od.c_nguoi_xac_nhan = -1;
                                od.c_noi_dung_duyet = items.c_ghi_chu;
                                if (items.c_trang_thai == 0)
                                {
                                    od.c_from_date = DateTime.Parse("1900-01-01"); ;
                                    od.c_to_date = DateTime.Parse("1900-01-01"); ;
                                    od.c_nguoi_duyet = -1;
                                    od.c_ngay_duyet = DateTime.Parse("1900-01-01");
                                }
                                else
                                {
                                    od.c_ngay_duyet = DateTime.Now;
                                    od.c_from_date = !string.IsNullOrEmpty(items.c_from_dateString) ? DateTime.Parse(items.c_from_dateString) : DateTime.Parse("1900-01-01"); ;
                                    od.c_to_date = !string.IsNullOrEmpty(items.c_to_dateString) ? DateTime.Parse(items.c_to_dateString) : DateTime.Parse("1900-01-01"); ;
                                    od.c_nguoi_duyet = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId;
                                }
                                CRM_Adv_Orders.Save(od, currentUser.UserName);

                            }
                            var duyet = dbConn.Where<CRM_Adv_Duyet>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order).FirstOrDefault();
                            if (duyet != null)
                            {

                                //Truong hop khong phải la duyet
                                if (items.c_trang_thai == 0)
                                {
                                    duyet.c_ngay_duyet = DateTime.Parse("1900-01-01");
                                    duyet.fk_staff = -1;
                                    duyet.c_type = items.c_trang_thai;
                                }
                                else
                                {
                                    duyet.c_ngay_duyet = DateTime.Now;
                                    duyet.fk_staff = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId;
                                    duyet.c_type = items.c_trang_thai;
                                   
                                }
                                duyet.c_ngay_yc_duyet = exit.c_ngay_yc;
                                duyet.fk_adv = items.fk_adv;
                                duyet.c_dot_order = items.c_dot_order;
                                CRM_Adv_Duyet.Save(duyet, currentUser.UserName);
                            }
                            else
                            {
                                CRM_Adv_Duyet d = new CRM_Adv_Duyet();
                                if (items.c_trang_thai == 0)
                                {                                   
                                    d.c_ngay_duyet = DateTime.Parse("1900-01-01");
                                    d.fk_staff = -1;
                                }
                                else
                                {
                                    d.c_ngay_duyet = DateTime.Now;   
                                    d.fk_staff = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId; 
                                }
                                d.c_type = items.c_trang_thai;
                                d.c_ngay_yc_duyet = exit.c_ngay_yc;
                                d.fk_adv = items.fk_adv;
                                d.c_dot_order = items.c_dot_order;
                                CRM_Adv_Duyet.Save(d, currentUser.UserName);
                            }
                            //
                            // upate bên Booking
                            var check = dbConn.Select<CRM_Adv_Services>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order);
                            //
                            if (check != null)
                            {
                                foreach (var item in check)
                                {
                                    //Banner
                                    if (item.c_type == 1)
                                    {
                                        dbConn.Update<CRM_Book_Req>(set: "c_account_status='HAI_CHIEU'", where:"c_code='"+item.c_book_code+"'");
                                    }
                                    //GOI BR
                                    else if(item.c_type == 2)
                                    {
                                        dbConn.Update<CRM_BookPR_Location>(set:"Status=0", where: "c_code='" + item.c_book_code + "' and Type=1");
                                    }
                                    //THƯỜNG
                                    else if (item.c_type == 3)
                                    {
                                        dbConn.Update<CRM_BookPR_Location>(set: "Status=0", where: "c_code='" + item.c_book_code + "' and Type=2");
                                    }

                                }
                            }


                        }
                        else if (items.ActionSelected == "XNLENTRANG")
                        {
                            var exit = dbConn.Where<CRM_Adv_Orders>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order).FirstOrDefault();
                            if (exit != null)
                            {
                                exit.c_dot_order = items.c_dot_order;
                                exit.c_xac_nhan_len_trang = true;
                                exit.c_nguoi_xac_nhan = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId;
                                exit.c_noi_dung_xac_nhan = items.c_noi_dung_duyet;
                                exit.c_ngay_xac_nhan = DateTime.Now;
                                exit.RowUpdatedAt = DateTime.Now;
                                exit.RowUpdatedUser = currentUser.UserName;
                                CRM_Adv_Orders.Save(exit, currentUser.UserName);
                            }
                            else
                            {
                                CRM_Adv_Orders od = new CRM_Adv_Orders();
                                od.fk_adv = items.fk_adv;
                                od.c_dot_order = items.c_dot_order;
                                od.c_xac_nhan_len_trang = true;
                                od.c_nguoi_xac_nhan = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId;
                                od.c_noi_dung_xac_nhan = items.c_noi_dung_duyet;
                                od.c_ngay_xac_nhan = DateTime.Now;
                                CRM_Adv_Orders.Save(od, currentUser.UserName);
                            }
                            //// update Cot IdOCM in Service
                            var listsv = dbConn.Select<CRM_Adv_Services>("fk_adv = {0} AND c_dot_order = {1}", items.fk_adv, items.c_dot_order);
                            if (listsv != null)
                            {
                                foreach (var sv in listsv)
                                {
                                    sv.c_ngay_up_ocm = DateTime.Now;
                                    sv.fk_staff_up_ocm = dbConn.FirstOrDefault<EmployeeInfo>(s => s.UserName == currentUser.UserName).RefStaffId;
                                    sv.RowUpdatedAt = DateTime.Now;
                                    sv.RowUpdatedUser = currentUser.UserName;
                                    dbConn.Update(sv);
                                }
                            }
                            // upate bên Booking
                            var check = dbConn.Select<CRM_Adv_Services>(p => p.fk_adv == items.fk_adv && p.c_dot_order == items.c_dot_order);
                            //
                            if (check != null)
                            {
                                foreach (var item in check)
                                {
                                    //Banner
                                    if (item.c_type == 1)
                                    {
                                        dbConn.Update<CRM_Book_Req>(set: "c_account_status='LEN_TRANG'", where: "c_code='" + item.c_book_code + "'");
                                    }
                                    //GOI BR
                                    else if (item.c_type == 2)
                                    {
                                        dbConn.Update<CRM_BookPR_Location>(set: "Status=1", where: "c_code='" + item.c_book_code + "' and Type=1");
                                    }
                                    //THƯỜNG
                                    else if (item.c_type == 3)
                                    {
                                        dbConn.Update<CRM_BookPR_Location>(set: "Status=1", where: "c_code='" + item.c_book_code + "' and Type=2");
                                    }

                                }
                            }
                        }
                        return Json(new { success = true });
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, message = e.Message });
                    }
                    finally { dbConn.Close(); }
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult SaveFiles(IEnumerable<HttpPostedFileBase> filesApprove, long pk_adv_flies = 0, int c_dot_order_files = 0)
        {

            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var existAdv = dbConn.SingleOrDefault<CRM_Adv>("pk_adv ={0}", pk_adv_flies);
                    if (filesApprove != null)
                    {
                        var adv_file = new CRM_Adv_File();
                        foreach (var file in filesApprove)
                        {
                            if (file != null)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                Helpers.UploadFile.CreateFolder(Server.MapPath("~/attach-file/dndqc/duyet"));
                                var path = Path.Combine(Server.MapPath("~/attach-file/dndqc/duyet"), fileName);
                                file.SaveAs(path);
                                adv_file.fk_adv = existAdv.pk_adv;
                                adv_file.c_type = "GUI_DUYET";
                                adv_file.c_input_date = DateTime.Now;
                                adv_file.c_dot_order = c_dot_order_files;
                                adv_file.c_file_name = fileName;
                                adv_file.RowCreatedAt = DateTime.Now;
                                adv_file.RowCreatedUser = currentUser.UserName;
                                dbConn.Save(adv_file);
                            }
                        }
                    }
                    dbConn.Update(existAdv);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        public ActionResult Create()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {

                ViewBag.pkAdv = 0;
                var us = new DynamicParameters();
                us.Add("@us", currentUser.UserName);
                ViewBag.Staff = dbConn.Query<CRM_Contract_Staff>("p_getInfoStaff", us, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_Employee ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");
                ViewBag.listLocationBanner = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List");
                ViewBag.Action = "New";
                ViewBag.listWebsite2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 23");
                ViewBag.listNature2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 25");
                ViewBag.typeService = 0;
                ViewBag.c_dot_order = 0;
                return View("Detail");
            }

        }
        public ActionResult CreateByContract(string list, long pk)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewBag.pkAdv = 0;
                var us = new DynamicParameters();
                us.Add("@us", currentUser.UserName);
                ViewBag.Staff = dbConn.Query<CRM_Contract_Staff>("p_getInfoStaff", us, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_Employee ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");
                ViewBag.listLocationBanner = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List");
                ViewBag.listWebsite2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 23");
                ViewBag.listNature2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 25");
                ViewBag.typeService = 0;
                ViewBag.c_dot_order = 0;
                ViewBag.Action = "New";
                string[] separators = { "@@" };
                var listProductID = list.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var strPKLocation = "";
                foreach (var item in listProductID)
                {
                    strPKLocation += "'" + item + "',";
                }
                strPKLocation = strPKLocation.Substring(0, strPKLocation.Length - 1);
                var ContractItem = dbConn.Select<CRM_Contract>(s => s.pk_contract == pk).FirstOrDefault();
                if (ContractItem != null)
                {
                    if (ContractItem.c_contract_type.Contains("CPM"))
                    {
                        ViewBag.listServices = dbConn.Select<CRM_Contract_Product_CPM>(@"SELECT *,'' AS c_vi_tri_kenhqc_name,'' AS c_chuyen_muc_kenhqc_name , '' AS c_kenh_qc_name FROM CRM_Contract_Product_CPM where PKCpm IN (" + strPKLocation + ")");
                    }
                    else if (ContractItem.c_contract_type.Contains("GUI_GIA"))
                    {
                        ViewBag.listServices = dbConn.Select<CRM_Contract_Product>(@"
                               SELECT  B.PKContractProduct,
		                                        B.ProductType,
	                                            C.NgayLen,
		                                        C.GioLen,
		                                        C.VungMien,
		                                        B.Number,
		                                        B.PriceCharged,
	                                            Web.PKList AS WebsiteID,
		                                        Cat.PKList AS  CategoryID,
		                                        Loc.PKList AS LocationID,
		                                        Nat.PKList AS NatureID,
		                                        Region.Name AS TenVungMien,
                                                Web.Name AS WebsiteName ,
				                                Cat.Name AS CategoryName,
				                                Loc.Name AS LocationName,
				                                Nat.Name AS NatureName,
		                                        B.Website,
		                                        B.Category,
		                                        B.Location,
		                                        B.Nature,
                                                A.Code AS c_book_code
                                            FROM CRM_Contract_Product B
                                            LEFT JOIN CRM_BookPRLocation C
                                            ON B.Website = (select top 1 Code from ERPAPD_List where PKList=C.Website  ) 
                                            AND B.Category=(select top 1 Code from ERPAPD_List where PKList=C.Category ) 
                                            LEFT JOIN CRM_BookPR A
                                            ON A.PKBookPR=C.FKBookPR
                                            LEFT JOIN ERPAPD_List Web 
                                            ON Web.code = B.Website and Web.FKListtype = 20
                                            INNER JOIN ERPAPD_List T 
                                            ON T.code= B.ProductType and T.FKListtype = 19
                                            INNER JOIN ERPAPD_List Cat
                                            ON Cat.code= B.Category and Cat.FKListtype = 22
                                            INNER JOIN ERPAPD_List Loc
                                            ON Loc.code= B.Location and Loc.FKListtype = 23
                                            INNER JOIN ERPAPD_List Nat
                                            ON Nat.code= B.Nature and Nat.FKListtype = 25
                                            INNER JOIN ERPAPD_List Region
                                            ON Region.code= C.VungMien and Region.FKListtype = 52
	                        where B.FkContract=" + pk + @" AND 
	                        A.Code = (SELECT c_book_code FROM CRM_Contract WHERE pk_contract=" + pk + @")
	                        AND PKContractProduct IN (" + strPKLocation + @")    
                         ");
                    }
                    else
                    {
                        ViewBag.listServices = dbConn.Select<CRM_Contract_Product>(@"
                                SELECT 
	                                    B.PKContractProduct,
									    B.ProductType,
				                        Web.PKList AS WebsiteID,
				                        Cat.PKList AS  CategoryID,
				                        Loc.PKList AS LocationID,
				                        Nat.PKList AS NatureID,
				                        C.NgayLen,
				                        C.NgayXuong,
				                        C.Label,
		                                B.Number,
				                        B.PriceCharged,
	                                    T.Name AS ProductTypeName,
				                        Web.Name AS WebsiteName ,
				                        Cat.Name AS CategoryName,
				                        Loc.Name AS LocationName,
				                        Nat.Name AS NatureName,
                                        A.c_code AS c_book_code
	                                FROM CRM_Contract_Product B
	                                LEFT JOIN CRM_BookLocation C
                                    ON	C.Website= B.Website AND C.Category=B.Category AND C.Location=B.Location AND C.Nature =B.Nature
	                                LEFT JOIN CRM_BookReq A
	                                ON A.pk_book_req=C.FKBookReq
		                            LEFT JOIN ERPAPD_List Web 
		                            ON Web.code = B.Website and Web.FKListtype = 20
		                            INNER JOIN ERPAPD_List T 
		                            ON T.code= B.ProductType and T.FKListtype = 19
		                            INNER JOIN ERPAPD_List Cat
		                            ON Cat.code= B.Category and Cat.FKListtype = 22
		                            INNER JOIN ERPAPD_List Loc
		                            ON Loc.code= B.Location and Loc.FKListtype = 23
		                            INNER JOIN ERPAPD_List Nat
		                            ON Nat.code= B.Nature and Nat.FKListtype = 25
	                        where B.FkContract=" + pk + @" AND 
	                        A.c_code = (SELECT c_book_code FROM CRM_Contract WHERE pk_contract=" + pk + @")
	                        AND PKContractProduct IN (" + strPKLocation + @")    
                         ");
                    }

                }

                return View("Detail");
            }
        }
        public ActionResult Detail(int Id, int c_dot_order)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {

                ViewBag.listStaff = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_Employee ");
                ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocationBanner = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List");
                ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM')");
                ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;
                ViewBag.listRegion = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");
                ViewBag.listWebsite2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 20");
                ViewBag.listCategory2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 22");
                ViewBag.listLocation2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 23");
                ViewBag.listNature2 = dbConn.Select<DropDownListDataList>("select Code,Name from ERPAPD_List WHERE FKListtype = 25");
                var c = new DynamicParameters();
                c.Add("@pk_adv", Id);
                var detail = dbConn.Query<CRM_Adv>("p_getAdvDetail", c, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                ViewBag.pkAdv = detail.pk_adv;

                var us = new DynamicParameters();
                us.Add("@ref", detail.fk_staff);

                ViewBag.Staff = dbConn.Query<CRM_Contract_Staff>("p_getInfoStaffByReferId", us, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                ViewBag.listFile = dbConn.Select<CRM_Adv_File>(s => s.fk_adv == detail.pk_adv && s.c_dot_order == c_dot_order && s.c_type == "ORDER");
                ViewBag.listFileDuyet = dbConn.Select<CRM_Adv_File>(s => s.fk_adv == detail.pk_adv && s.c_dot_order == c_dot_order && s.c_type == "GUI_DUYET");
                ViewBag.itemDetail = detail;
                var list = dbConn.Select<CRM_Adv_Services>(@"SELECT A.*,
                            (select  name from ERPAPD_List where PKList = A.c_website and FKListtype = 20) AS c_website_name,
		                    (select  name from ERPAPD_List where PKList = A.c_category and FKListtype = 22) AS c_category_name,
		                    (select  name from ERPAPD_List where PKList = A.c_location and FKListtype = 23) AS c_location_name,
		                    (select  name from ERPAPD_List where PKList = A.c_nature and FKListtype = 25) AS c_nature_name,
                            (SELECT Value FROM CRM_Hierarchy  WHERE ParentID=1 AND A.c_vung_mien=HierarchyID) AS c_vung_mien_name,
                            (select  name from ERPAPD_List where Code = A.c_gio and FKListtype = 72) AS c_gio_name                           
                            FROM CRM_Adv_Services A
                            WHERE fk_adv=" + detail.pk_adv + " and c_dot_order=" + c_dot_order);
                //ViewBag.pktype = list != null ? list.FirstOrDefault().c_type : 0;
                ViewBag.listProducts = list;
                var pkAdv = new DynamicParameters();
                pkAdv.Add("@FkAdv", detail.pk_adv);
                pkAdv.Add("@c_dot_order", c_dot_order);

                // get service
                var listP = dbConn.Query<CRM_Adv_Services>("p_getListProduct_DNDQC", pkAdv, commandType: System.Data.CommandType.StoredProcedure);
                
                if (listP.Count() > 0)
                {
                    ViewBag.listBanner = listP.Where(s => s.c_type == 1).ToList();
                    ViewBag.listPackagePR = listP.Where(s => s.c_type == 2).ToList();
                    if (ViewBag.listPackagePR != null)
                    {
                        var luytien = dbConn.Select<CRM_Adv_Services>(@"select 1 as pk_services, ISNULL(max(c_luy_ke_da_dang),0) as c_luy_ke_da_dang from CRM_Adv_Services where fk_adv =" + detail.pk_adv);
                        if (luytien.Count > 0)
                        {
                            ViewBag.TienLuyTien = luytien.FirstOrDefault().c_luy_ke_da_dang;
                        }
                        else
                        {
                            ViewBag.TienLuyTien = 0;
                        }
                    }
                    else
                    {
                        ViewBag.TienLuyTien = 0;
                    }
                    ViewBag.listHDT = listP.Where(s => s.c_type == 3).ToList();
                    ViewBag.listCPM = listP.Where(s => s.c_type == 4).ToList();
                    ViewBag.typeService = listP.FirstOrDefault().c_type;
                    var pkService = listP.FirstOrDefault().pk_services;
                    ViewBag.pkService = pkService;
                    
                    //Lấy thông tin trong bảng mới
                    var listnfoOrders = dbConn.Select<CRM_Adv_Orders>(@" select *,
                             ISNULL((select TOP 1 FullName from EmployeeInfo where RefStaffId=c_nguoi_xac_nhan),'') as c_ten_nguoi_xac_nhan         
                             from CRM_Adv_Orders
                             where fk_adv= " + detail.pk_adv + " and c_dot_order=" + c_dot_order).FirstOrDefault();

                    //Lấy thông tin trên CRM cũ
                    if (listnfoOrders == null)
                    {
                        var listOrder = dbConn.FirstOrDefault<CRM_Adv_Orders>(@"SELECT 1 as pk_order,pk_adv,B.c_dot_order as c_dot_order,A.c_ngay_yc_duyet AS c_ngay_yc,
	                                    c_noi_dung_gui_duyet as c_noi_dung_yc,
	                                    ISNULL(C.c_type,0) as c_trang_thai,
	                                    c_dinh_muc_bn as c_han_muc_bn,
	                                    c_dinh_muc_pr_goi AS c_han_muc_goi_pr,
	                                    c_dinh_muc_pr_thuong as c_han_muc_pr_thuong,
	                                    c_dinh_muc_cpm as c_han_muc_cpm,
	                                    c_from_date,c_to_date,
	                                    A.c_note as c_ghi_chu,
                                        fk_nguoi_duyet as c_nguoi_duyet,
	                                    (select TOP 1 FullName from EmployeeInfo where RefStaffId=fk_nguoi_duyet  and fk_nguoi_duyet <> 0 ) as c_ten_nguoi_duyet,
	                                    A.c_ngay_duyet,
	                                    c_noi_dung_duyet,
										B.fk_staff_up_ocm,
	                                    B.c_ngay_up_ocm AS c_ngay_xac_nhan,
                                        c_noi_dung_xn_dang_dv as c_noi_dung_xac_nhan,
										(CASE WHEN B.fk_staff_up_ocm=0 THEN 0 ELSE 1 END) AS c_xac_nhan_len_trang,
										ISNULL((select TOP 1 FullName from EmployeeInfo where RefStaffId=B.fk_staff_up_ocm and B.fk_staff_up_ocm <> 0),'') as c_ten_nguoi_xac_nhan
                                       FROM CRM_Adv  A
									   LEFT JOIN 
									   CRM_Adv_Services B
									   ON A.pk_adv=B.fk_adv
									   LEFT JOIN CRM_Adv_Duyet C
									   ON C.fk_adv=B.fk_adv and C.c_dot_order=B.c_dot_order
                                       where A.pk_adv={0} and B.c_dot_order={1}", detail.pk_adv, c_dot_order);
                        ViewBag.listInfoOrders = listOrder;
                        ViewBag.Status = listOrder.c_trang_thai;
                    }
                    else
                    {
                        if (listnfoOrders == null)
                        {
                            ViewBag.Status = 0;
                        }
                        else
                        {
                            ViewBag.Status = listnfoOrders.c_trang_thai;
                        }
                        ViewBag.listInfoOrders = listnfoOrders;
                    }
                }

                ViewBag.listFile = dbConn.Query<CRM_Adv_File>("p_getListFile_DNDQC", pkAdv, commandType: System.Data.CommandType.StoredProcedure);
                ViewBag.c_dot_order = c_dot_order;
                ViewBag.Action = "Edit";
                var listgroup = currentUser.Groups;
                int group = 0;
                if (listgroup.Count > 0)
                {
                    foreach (var item in listgroup)
                    {
                        if (item.Id == 57)
                        {
                            //KT
                            group = item.Id;
                            break;
                        }
                        if (item.Id == 50)
                        {
                            //KinhDoanh
                            group = item.Id;
                            break;
                        }
                    }
                }
                if (currentUser.UserName == "administrator")
                {
                    ViewData["FlagViewAll"] = true;
                    ViewData["KT"] = false;
                    ViewData["TeamLeader"] = false;
                }
                else
                {
                    if (group == 57)
                    {
                        ViewData["KT"] = true;
                        ViewData["FlagViewAll"] = false;
                        ViewData["TeamLeader"] = false;
                    }
                    else
                    {
                        ViewData["KT"] = false;
                        ViewData["FlagViewAll"] = false;
                        ViewData["TeamLeader"] = false;
                    }
                }
                return View("Detail", detail);
            }
        }
        public ActionResult DeleteItemFile(int pk = 0)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.Delete<CRM_Adv_File>("pk_file ={0}", pk);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }

            }
        }
        public ActionResult SuggestContract(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<CRM_SuggestContract>(
                    @"  SELECT  TOP 5 
                                  con.c_code
								 ,con.c_total_money
                                 ,con.c_labels
	                            ,ISNULL(cus.CustomerName,'') AS CustomerName
	                            ,ISNULL(cusType.Value,'') AS CustomerType
                                ,ISNULL(cat.Value,'') AS CategoryName
                                ,con.c_book_code
                                ,c_contract_type
                        FROM CRM_Contract con 
                        INNER JOIN ERPAPD_Customer cus ON cus.CustomerCode = con.c_customer_code
                        LEFT JOIN  CRM_CategoryHierarchy cat ON  cus.Category = cat.HierarchyID  --con.c_category_code = cat.HierarchyID
                        LEFT JOIN  ERPAPD_MasterData_Customer cusType ON cusType.Code = cus.CustomerType
                        WHERE con.c_code LIKE N'%" + text + "%'"
                      );
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestWebsite(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestCategory(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestLocation(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>(@"select PKList AS Code,Name from ERPAPD_List WHERE Code IN('DCM','GCM','CMC')");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestRegion(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>("select Code,Name from ERPAPD_List WHERE PKList IN (724,725,727) ");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestTime(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestChannel(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 83");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SuggestLocationChannel(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DropDownListData>(@"select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 23");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReadBookBannerData_Read([DataSourceRequest] DataSourceRequest request, string contractCode, string bookcode)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var c = new DynamicParameters();
                //c.Add("@contractCode", contractCode);
                c.Add("@bookcode", bookcode);
                var detail = dbConn.Query<BookBannerViewModel>("get_List_Book_Banner_By_Contract", c, commandType: System.Data.CommandType.StoredProcedure);
                request.Filters = null;
                var data = detail.ToDataSourceResult(request);
                return Json(data);
            }

        }
        public ActionResult SaveAdv(CRM_Adv avd)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    if (avd != null)
                    {
                        var rs = CRM_Adv.Save(avd, currentUser.UserName);
                        return Json(rs);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Data is null" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            // check Adv

        }
        public ActionResult SaveAdvService(IEnumerable<CRM_Adv_Services> avd, int c_dot_order)
        {
            try
            {
                if (avd != null)
                {
                    var rs = CRM_Adv_Services.Save(avd, c_dot_order, currentUser.UserName);
                    return Json(rs);
                }
                else
                {
                    return Json(new { success = false, message = "Data is null" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
        public ActionResult SaveOther(IEnumerable<HttpPostedFileBase> files, long pk_adv = 0, string note = "", int c_dot_order = 0)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var existAdv = dbConn.SingleOrDefault<CRM_Adv>("pk_adv ={0}", pk_adv);
                    if (files != null)
                    {
                        var adv_file = new CRM_Adv_File();
                        foreach (var file in files)
                        {

                            if (file != null)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                Helpers.UploadFile.CreateFolder(Server.MapPath("~/attach-file/dndqc/order"));
                                var path = Path.Combine(Server.MapPath("~/attach-file/dndqc/order"), fileName);
                                file.SaveAs(path);
                                adv_file.fk_adv = existAdv.pk_adv;
                                adv_file.c_type = "ORDER";
                                adv_file.c_input_date = DateTime.Now;
                                adv_file.c_dot_order = c_dot_order;
                                adv_file.c_file_name = fileName;
                                adv_file.RowCreatedAt = DateTime.Now;
                                adv_file.RowCreatedUser = currentUser.UserName;
                                dbConn.Save(adv_file);
                            }
                        }
                    }
                    existAdv.c_note = note;
                    dbConn.Update(existAdv);

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }

            }

        }
        public ActionResult DeleteService(int pkservice = 0)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.Delete<CRM_Adv_Services>("pk_services ={0}", pkservice);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }

            }
        }
        public virtual ActionResult DownloadFile(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/attach-file/dndqc/order/"), fileName);
            if (System.IO.File.Exists(fullPath))
                return File(new FileStream(fullPath, FileMode.Open), "text/plain", fileName);
            else
                return null;

        }
        public virtual ActionResult DownloadFileDuyet(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/attach-file/dndqc/duyet/"), fileName);
            if (System.IO.File.Exists(fullPath))
                return File(new FileStream(fullPath, FileMode.Open), "text/plain", fileName);
            else
                return null;

        }
        public ActionResult getMoneyPublishQC(string bookcode = "")
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var money = dbConn.QueryScalar<int>(
                    @"  SELECT   ISNULL(SUM(c_don_gia_sau_ck),0) AS c_don_gia_sau_ck
                        FROM CRM_Adv_Services con 
                        WHERE fk_adv = (SELECT TOP 1 pk_adv FROM CRM_Adv WHERE c_code = '" + bookcode + "') AND fk_staff_up_ocm <> -1"
                      );
                return Json(new { success = true, money = money }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ReadAdvService([DataSourceRequest]DataSourceRequest request, int pkAdv, int type)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                string strQuery = @"SELECT a.*
                                    FROM CRM_Adv_Services a
                                    WHERE c_type = '" + type + "' AND fk_adv = " + pkAdv;
                data = KendoApplyFilter.KendoDataByQuery<CRM_Adv_Services>(request, strQuery, "");
                return Json(data);

            }
        }
        public ActionResult UpdateAdvService([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Adv_Services> list, int dot_order)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                try
                {
                    if (list != null && ModelState.IsValid)
                    {
                        CRM_Adv_Services.Save(list, dot_order, currentUser.UserName);
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
        public ActionResult DeleteAdvService(int pkadv = 0, int order = 0)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    dbConn.Delete<CRM_Adv_Services>("fk_adv = {0} AND c_dot_order = {1}", pkadv, order);
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}