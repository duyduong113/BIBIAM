using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;


namespace ERPAPD.Controllers
{
    public class CRM_DNDQCController : CustomController
    {
        // GET: CRM_DNDQC
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
                        ViewBag.listStaffIndex = dbConn.Select<DropDownListData>("select RefStaffId AS Code, FullName AS Name from EmployeeInfo");
                        ViewBag.listTime = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 72");
                        ViewBag.listWebsite = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 20");
                        ViewBag.listCategory = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 22");
                        ViewBag.listLocationBanner = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List");
                        ViewBag.listLocation = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListType =23");
                        ViewBag.listNature = dbConn.Select<DropDownListData>("select PKList AS Code,Name from ERPAPD_List WHERE FKListtype = 25"); ;

                        ViewBag.listRegionIndex = dbConn.Select<DropDownListData>("SELECT [HierarchyID] AS Code,[Value] AS Name FROM [CRM_Hierarchy] WHERE HierarchyID IN (14,12,13) order by [HierarchyID] ASC");
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

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                string whereCondition = "";
                if (request.Filters.Count > 0)
                {
                    whereCondition = " AND " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                }
                data = new CRM_Adv().GetPage(request, whereCondition);
                return Json(data);
              
            }
        }
    }
}