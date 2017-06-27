using ERPAPD.Helpers;
using ERPAPD.Models;
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
using System.Globalization;

namespace ERPAPD.Controllers
{
    public class CRMContractListController : CustomController
    {
        // GET: CRM_ContractList
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
                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                        ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "ContractAPStatus").OrderBy(s => s.ID);
                        ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>(s => s.ParentID == "0").OrderBy(s => s.HierarchyID);
                        //ViewBag.listLabel = dbConn.Select<CRM_Label>();
                        ViewBag.listContractType = dbConn.Select<Parameters>("Type='ContractDraffType'").OrderBy(s => s.ID);
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
                        if (currentUser.UserName == "administrator")
                        {
                            ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13,14) order by[HierarchyID] ASC");
                            ViewBag.listTeam = dbConn.Select<CRM_Team>(@"SELECT * FROM [CRM_Team] WHERE FKUnit IN (12,13)");
                            ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId from EmployeeInfo Where DepartmentID IN (28,30)");
                            ViewBag.listStaffApproved = dbConn.Select<EmployeeInfo>(@"SELECT * FROM EmployeeInfo WHERE Team in (22,36)");
                            ViewData["FlagViewAll"] = true;
                            ViewData["KT"] = false;
                            ViewData["TeamLeader"] = false;
                        }
                        else
                        {
                            var RegionbyUser = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,DepartmentID,Team,Region,Position FROM EmployeeInfo WHERE UserName='" + currentUser.UserName + "'");
                            //Role KT
                            if (group == 57)
                            {
                                ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13) order by[HierarchyID] ASC");
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
                            //if ((RegionbyUser.DepartmentID == 50 && RegionbyUser.Region == "12") ||
                            //    (RegionbyUser.DepartmentID == 51 && RegionbyUser.Region == "13"))
                            //{
                            //}
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
        public ActionResult getIDContractByCode(string ContractCode)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var data = dbConn.FirstOrDefault<CRM_Contract_Draff>(@"select PKContractDraft,LoaiHopDong from CRM_Contract_Draff where Code={0}", ContractCode);

                    if (data != null)
                    {
                        return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, data = data }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }
        //GET
        public ActionResult CreateContract(string typeContract)
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
                        ViewBag.typeContract = typeContract;
                        ViewBag.PKContractDraff = 0;
                        ViewBag.Terms = dbConn.Select<CRM_Contract_Extra>("Type ={0}", typeContract).OrderBy(s => s.RowID);
                        //BaoHV ContractType
                        ViewBag.listContractType = dbConn.Select<Parameters>("Type='ContractDraffType'").OrderBy(s => s.ParamID);
                        ViewBag.listStatusContractDraff = dbConn.Select<Parameters>(s => s.Type == "ContractAPStatus").OrderBy(s => s.ParamID);
                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>(@"	  SELECT ID,Code,Value FROM ERPAPD_MasterData_Customer
																			  WHERE Type='CustomerType' AND Active =1");
                        ViewBag.listLOAID_DL = dbConn.Select<ERPAPD_List>(@"SELECT PKList,Code,Name FROM ERPAPD_List
																			  Where  FKListtype=57 AND Status=1");
                        ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "ContractAPStatus").OrderByDescending(s => s.ParamID);
                        ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>().OrderByDescending(s => s.PKEmployeeID);
                        ViewBag.listCustomerContract = dbConn.Select<Parameters>("Type='CustomerContract'").OrderBy(s => s.ParamID);
                        ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>(s => s.ParentID == "0").OrderBy(s => s.HierarchyID);
                        ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>(s => s.ParentID != "0" && s.Level == 1).OrderBy(s => s.HierarchyID);
                        ViewBag.listLabel = dbConn.Select<CRM_Label>();
                        ViewBag.listManager = dbConn.Select<CRM_Contract_ListManager>();
                        ViewBag.listPromotion = dbConn.Select<ERPAPD_List>(@"SELECT * FROM ERPAPD_List WHERE FKListtype=31");
                        ViewBag.action = "create";
                        var StaffID = dbConn.FirstOrDefault<ERPAPD_Employee>("SELECT Id,UserName,ISNULL(Region,0) AS Region FROM EmployeeInfo where UserName='" + currentUser.UserName + "'");
                        //  var UserID = "All";
                        var RegionID = 0;
                        if (StaffID != null)
                        {
                            RegionID = StaffID.Region;
                        }
                        if (RegionID != 0)
                        {
                            ViewBag.ThongTinBenB = dbConn.Select<CRM_Contract_ListManager>(p => p.RegionID == RegionID);
                        }
                        else
                        {
                            ViewBag.ThongTinBenB = dbConn.Select<CRM_Contract_ListManager>("SELECT TOP 1 * FROM CRM_Contract_ListManager");
                        }
                        var RegionbyUser = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,RefStaffId,DepartmentID,Team,Region FROM EmployeeInfo WHERE UserName='" + currentUser.UserName + "'");
                        if (RegionbyUser != null)
                        {
                            ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("HierarchyID={0}", RegionbyUser.Region);
                            ViewBag.listTeam = dbConn.Select<CRM_Team>("TeamID={0}", RegionbyUser.Team);
                            ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId from EmployeeInfo Where RefStaffId={0}", RegionbyUser.RefStaffId);
                        }

                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                }

                return View("formContract");
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult EditContract(string Id, string Type)
        {
            if (asset.Update)
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

                        ViewBag.typeContract = Type;
                        ViewBag.PKContractDraff = Id;

                        //BaoHV GetData binding to Thông tin chung
                        ViewBag.listContractType = dbConn.Select<Parameters>(s => s.Type == "ContractDraffType").OrderBy(s => s.ParamID);
                        ViewBag.listStatusContractDraff = dbConn.Select<Parameters>(s => s.Type == "ContractAPStatus").OrderBy(s => s.ParamID);
                        ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>(s => s.ParentID == "0").OrderBy(s => s.HierarchyID);
                        ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>(s => s.ParentID != "0" && s.Level == 1).OrderBy(s => s.HierarchyID);
                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>(@"	  SELECT ID,Code,Value FROM ERPAPD_MasterData_Customer
																			  WHERE Type='CustomerType' AND Active =1");
                        ViewBag.listLOAID_DL = dbConn.Select<ERPAPD_List>(@"SELECT PKList,Code,Name FROM ERPAPD_List
																			  Where  FKListtype=57 AND Status=1");
                        // Thông tin hợp đồng
                        ViewBag.staffDraff = dbConn.Select<CRM_Contract_Staff_Draff>(
                            @"  select s.*,
                                  ( SELECT TOP 1 ISNULL(t.TeamName,'') FROM EmployeeInfo A LEFT JOIN CRM_Team t on t.TeamID=A.Team where Team= s.GroupId) as TeamName,
                                  ( SELECT TOP 1 ISNULL(h.Value,'') FROM EmployeeInfo A LEFT JOIN CRM_Hierarchy h on h.HierarchyID=a.Region where Region= s.UnitID) as Province,
                                  ( SELECT TOP 1 ISNULL(FullName,'') FROM EmployeeInfo where RefStaffId = s.StaffId) as FullName  
                                  from CRM_Contract_Staff_Draff  s where s.FKContract = " + int.Parse(Id));

                        ViewBag.paymentSchedule = dbConn.Select<CRM_PaymentScheduleDraft>("select * from CRM_PaymentScheduleDraft where FKContract =" + int.Parse(Id));
                        var draffdraffansi = dbConn.Select<CRM_Contract_Draff>(@" SELECT condraff.*,cus.CustomerID,cus.CustomerName,cus.CategoryParent as CategoryID,

                                                                                    cus.Category as CategorySubID,cus.CustomerType as CustomerType, P.Value AS StatusName
                                                                                        FROM CRM_Contract_Draff condraff
                                                                                    LEFT JOIN  ERPAPD_Customer cus
                                                                                     ON cus.CodeLink =condraff.CustomerCode
                                                                                    LEFT JOIN Parameters P
                                                                                    ON P.ParamID=condraff.TrangThai AND P.Type='ContractAPStatus'
                                                                              WHERE condraff.PKContractDraft='" + Id + "'").FirstOrDefault();

                        draffdraffansi.Dieu2 = ConvertANSIToUTF8.Convert(draffdraffansi.Dieu2);

                        draffdraffansi.DieuKhoan = ConvertANSIToUTF8.Convert(draffdraffansi.DieuKhoan);
                        draffdraffansi.GhiChu = ConvertANSIToUTF8.Convert(draffdraffansi.GhiChu);
                        draffdraffansi.GhiChu = draffdraffansi.GhiChu.Replace("&lt;p&gt;", " ");
                        draffdraffansi.GhiChu = draffdraffansi.GhiChu.Replace("&lt;/p&gt;", " ");
                        ViewBag.draffdraff = draffdraffansi;
                        ViewBag.itemdraff = draffdraffansi;
                        ViewBag.listManager = dbConn.Select<CRM_Contract_ListManager>("select * From CRM_Contract_ListManager");
                        //
                        ViewBag.listProduct = "";
                        ViewBag.listPromotion = dbConn.Select<ERPAPD_List>(@"SELECT * FROM ERPAPD_List WHERE FKListtype=31");
                        ViewBag.action = "edit";
                        ViewBag.Terms = dbConn.Select<CRM_Contract_Extra>("Type ={0}", Type).OrderBy(s => s.RowID);
                        ViewBag.ThongTinBenB = dbConn.Select<CRM_Contract_ListManager>(p => p.RegionID == 0);

                        ViewBag.ThongTinBenB = dbConn.Select<CRM_Contract_ListManager>(p => p.RegionID == 0);
                        ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(s => s.FKContract == int.Parse(Id));
                        if (Type == "CPM" || Type == "PHIEUCPM")
                        {
                            ViewBag.ProductCPM = dbConn.Select<CRM_Contract_Product_CPM_Draff>(@"select * from CRM_Contract_Product_CPM_Draff where FkContract={0}", Id);
                        }
                        else if (Type == "GOI")
                        {
                            var listGoi = dbConn.Select<CRM_Contract_Product_Packet_Draff>("select * from CRM_Contract_Product_Packet_Draff where FKContract={0}", Id);
                            if (listGoi.Count == 0)
                            {
                                ViewBag.ProductHDG = dbConn.Select<CRM_Contract_Product_Packet_Draff>(@"SELECT A.PKProduct AS PKPacket,
                                                     A.FKContract, '' AS Code,'GOI' AS 'Type',A.HUONG AS Name, B.DateUp ,B.DateDown, A.Price AS UnitPrice, B.Discount1 AS Discount,B.Money AS Total
                                                     FROM CRM_Contract_Product_Draff A
                                                     LEFT JOIN CRM_Contract_Time_Draff B
                                                     ON A.FKContract=B.FKContract where A.FKContract ={0}", Id);
                            }
                            else
                            {
                                ViewBag.ProductHDG = listGoi;
                            }
                        }
                        ViewBag.productHDT = CRM_Contract_Product_Draff.getProductByPKContract(int.Parse(Id));
                        ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("HierarchyID={0}", draffdraffansi.KFUnit);
                        ViewBag.listTeam = dbConn.Select<CRM_Team>("TeamID={0}", draffdraffansi.KFGroup);
                        ViewBag.listStaff = dbConn.Select<EmployeeInfo>(@"SELECT Id, UserName, FullName, RefStaffId from EmployeeInfo Where RefStaffId={0}", draffdraffansi.FKStaff);
                    }
                    catch (Exception e) { }
                    finally { dbConn.Close(); }
                }

                return View("formContract");
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult GetListContractProduct([DataSourceRequest]DataSourceRequest request, string Id)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM ( SELECT temp.* FROM
                                        (
                                        SELECT
                                         B.[PKProduct]
                                        ,B.[FKContract]
                                        ,B.[Website]
                                        ,B.[ProductType]
                                        ,B.[Category]
                                        ,B.[Location]
                                        ,B.[Nature]
                                        ,B.[Unit]
                                        ,B.[Number]
                                        ,B.[Size]
                                        ,B.[Price]
                                        ,B.[STT]
                                        ,B.[CPM]
                                        ,B.[CPC]
                                        ,B.[HUONG]
                                        ,B.[NguonDan]
                                        ,B.[FKProduct]
                                        ,B.CKTienMat
										,ISNULL(B.TotalAmtBeforeDiscount,0) AS TotalAmtBeforeDiscount
										,ISNULL(B.TotalAmtAfterDiscount,0) AS TotalAmtAfterDiscount
										,ISNULL(B.TotalAmtSevicePlus,0) AS TotalAmtSevicePlus
										,ISNULL((B.TotalAmtAfterDiscount -B.TotalAmtSevicePlus),0) AS TotalAmt
                                        ,ISNULL(P.Name,'') AS ProductName
                                        ,
                                        ISNULL(STUFF(( SELECT N', <br> Từ: ' + FORMAT(A.DateUp,'dd/MM/yyyy') +N' đến: '+ FORMAT(A.DateDown,'dd/MM/yyyy')
                                                       + N' <br> Thời gian: ' + CAST(A.[Week] AS nvarchar(200)) +N' tuần lẻ '+ CAST(A.NumberDay AS nvarchar(200)) +' ngày'
                                        FROM CRM_Contract_ListTime_Draff A
                                        WHERE  A.FKProduct=B.PKProduct
                                        FOR XML PATH (''),TYPE).value('.', 'nvarchar(300)'),1,2,''),'') AS ListTime
                                        ,
                                        ISNULL(STUFF(( SELECT N', <br> '+ A.Promotion +': ' + CAST(A.Discount as varchar(10)) +'% '
                                        FROM CRM_Contract_ListPromotionProduct_Draff A
                                        WHERE  A.FKProduct=B.PKProduct
                                        FOR XML PATH (''),TYPE).value('.', 'nvarchar(300)'),1,2,''),'') AS ListPromotion
                                        ,
										B.Price - (B.Price*(SELECT SUM(Discount/100) FROM CRM_Contract_ListPromotionProduct_Draff E
										 WHERE E.FKContract=B.FKContract AND B.PKProduct=E.FKProduct)) AS DonGiaSauGiam
									    FROM CRM_Contract_Product_Draff B
                                        LEFT JOIN  ERPAPD_Product P
					                    ON B.FKProduct=P.PKProduct
                                        )
                                        temp WHERE temp.FKContract=" + Id + " ) A ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Contract_Product_Draff>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
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
                            //KT
                            group = item.Id;
                            break;
                        }
                    }
                }

                if (asset.View)
                {
                    string whereCondition = "";
                    if (request.Filters.Count > 0)
                    {
                        whereCondition = " AND " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    }
                    data = new CRM_Contract_Draff().GetPage(request, whereCondition, group);
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        //public ActionResult SaveTimeProductDraff(IEnumerable<CRM_Contract_ListTime_Draff> listTimeProduct,
        //    IEnumerable<CRM_Contract_ListPromotionProduct_Draff> listPromotionProduct, CRM_Contract_Product_Draff ObProduct)
        //{
        //    IDbConnection dbConn = OrmliteConnection.openConn();
        //    var IdContract = 0;
        //    Int64 IdProduct = 0;
        //    foreach (var draff in listTimeProduct)
        //    {
        //        var exitTime = dbConn.Select<CRM_Contract_ListTime_Draff>(p => p.PKTime == draff.PKTime).SingleOrDefault();
        //        if (exitTime != null)
        //        {
        //            exitTime.DateUp = draff.DateUp;
        //            exitTime.DateDown = draff.DateDown;
        //            exitTime.NumberDay = draff.NumberDay;
        //            exitTime.Week = draff.Week;
        //            exitTime.TotalAmtNoVAT = draff.TotalAmtNoVAT;
        //            exitTime.RowUpdatedAt = DateTime.Now;
        //            exitTime.RowUpdatedUser = currentUser.UserName;
        //            dbConn.Update(exitTime);
        //        }
        //        else
        //        {
        //            var time = new CRM_Contract_ListTime_Draff();
        //            time = draff;
        //            time.RowCreatedAt = DateTime.Now;
        //            time.RowCreatedUser = currentUser.UserName;
        //            time.RowUpdatedAt = DateTime.Parse("1900-01-01");
        //            time.RowUpdatedUser = "";
        //            dbConn.Insert(time);
        //        }
        //        IdContract = draff.FKContract;
        //        IdProduct = draff.FKProduct;
        //    }
        //    foreach (var draff in listPromotionProduct)
        //    {
        //        var exitpromotion = dbConn.Select<CRM_Contract_ListPromotionProduct_Draff>(p => p.PKPromotion == draff.PKPromotion).FirstOrDefault();
        //        if (exitpromotion != null)
        //        {
        //            exitpromotion.Discount = draff.Discount;
        //            exitpromotion.Promotion = draff.Promotion;
        //            exitpromotion.AmtNoVAT = draff.AmtNoVAT;
        //            exitpromotion.RowUpdatedAt = DateTime.Now;
        //            exitpromotion.RowUpdatedUser = currentUser.UserName;
        //            dbConn.Update(exitpromotion);
        //        }
        //        else
        //        {
        //            var promotion = new CRM_Contract_ListPromotionProduct_Draff();
        //            promotion = draff;
        //            promotion.RowCreatedAt = DateTime.Now;
        //            promotion.RowCreatedUser = currentUser.UserName;
        //            promotion.RowUpdatedAt = DateTime.Parse("1900-01-01");
        //            promotion.RowUpdatedUser = "";
        //            dbConn.Insert(promotion);
        //        }
        //    }
        //    var exitdraff = dbConn.Select<CRM_Contract_Product_Draff>(p => p.FKContract == IdContract && p.PKProduct == IdProduct).FirstOrDefault();
        //    if (exitdraff != null)
        //    {
        //        exitdraff.NguonDan = !string.IsNullOrEmpty(ObProduct.NguonDan) ? ObProduct.NguonDan : "";
        //        exitdraff.Price = ObProduct.Price;
        //        exitdraff.Number = ObProduct.Number;
        //        exitdraff.CKTienMat = ObProduct.CKTienMat;
        //        exitdraff.CTKMCKTien = ObProduct.CTKMCKTien;
        //        exitdraff.TotalAmtBeforeDiscount = ObProduct.TotalAmtBeforeDiscount;
        //        exitdraff.TotalAmtAfterDiscount = ObProduct.TotalAmtAfterDiscount;
        //        exitdraff.TotalAmtSevicePlus = ObProduct.TotalAmtSevicePlus;
        //        dbConn.Update(exitdraff);
        //    }
        //    return Json(new { success = true });
        //}

        //public ActionResult CreateProductDraff(IEnumerable<CRM_Contract_ListTime_Draff> listTimeProduct,
        //    IEnumerable<CRM_Contract_ListPromotionProduct_Draff> listPromotionProduct, CRM_Contract_Product_Draff ObProduct)
        //{
        //    IDbConnection dbConn = OrmliteConnection.openConn();
        //    var newdraff = ObProduct;
        //    var temp = dbConn.Select<ERPAPD_Product>(p => p.PKProduct == ObProduct.FKProduct).FirstOrDefault();
        //    if (temp != null)
        //    {
        //        newdraff.Website = temp.Website;
        //        newdraff.ProductType = temp.ProductType;
        //        newdraff.Category = temp.Category;
        //        newdraff.Location = temp.Location;
        //        newdraff.Nature = temp.Nature;
        //        newdraff.Size = temp.Size;
        //        newdraff.Unit = 0;
        //        newdraff.STT = 0;
        //        newdraff.CPC = 0;
        //        newdraff.CPM = 0;
        //        newdraff.HUONG = "";
        //        newdraff.CKTienMat = ObProduct.CKTienMat;
        //        newdraff.CTKMCKTien = ObProduct.CTKMCKTien;
        //        newdraff.NguonDan = !string.IsNullOrEmpty(ObProduct.NguonDan) ? ObProduct.NguonDan : "";
        //        newdraff.RowCreatedAt = DateTime.Now;
        //        newdraff.RowCreatedUser = currentUser.UserName;
        //        newdraff.RowUpdatedAt = DateTime.Parse("1900-01-01");
        //        newdraff.RowUpdatedUser = "";
        //        newdraff.IsNew = 1;
        //        newdraff.TotalAmtBeforeDiscount = ObProduct.TotalAmtBeforeDiscount;
        //        newdraff.TotalAmtAfterDiscount = ObProduct.TotalAmtAfterDiscount;
        //        newdraff.TotalAmtSevicePlus = ObProduct.TotalAmtSevicePlus;

        //        dbConn.Insert(newdraff);
        //        var PKProduct = (int)dbConn.GetLastInsertId();
        //        if (listTimeProduct.Count() > 0)
        //        {
        //            foreach (var draff in listTimeProduct)
        //            {
        //                if ((draff.DateUp == draff.DateDown) && (draff.NumberDay == 0 && draff.Week == null))
        //                {
        //                    break;
        //                }
        //                else
        //                {
        //                    var exitTime = dbConn.Select<CRM_Contract_ListTime_Draff>(p => p.PKTime == draff.PKTime).FirstOrDefault();
        //                    if (exitTime != null)
        //                    {
        //                        exitTime.DateUp = draff.DateUp;
        //                        exitTime.DateDown = draff.DateDown;
        //                        exitTime.NumberDay = draff.NumberDay;
        //                        exitTime.Week = draff.Week;
        //                        exitTime.TotalAmtNoVAT = draff.TotalAmtNoVAT;
        //                        exitTime.RowUpdatedAt = DateTime.Now;
        //                        exitTime.RowUpdatedUser = currentUser.UserName;
        //                        dbConn.Update(exitTime);
        //                    }
        //                    else
        //                    {
        //                        var time = new CRM_Contract_ListTime_Draff();
        //                        time = draff;
        //                        time.FKProduct = PKProduct;
        //                        time.RowCreatedAt = DateTime.Now;
        //                        time.RowCreatedUser = currentUser.UserName;
        //                        time.RowUpdatedAt = DateTime.Parse("1900-01-01");
        //                        time.RowUpdatedUser = "";
        //                        dbConn.Insert(time);
        //                    }
        //                }
        //            }
        //        }
        //        if (listPromotionProduct.Count() > 0)
        //        {
        //            foreach (var draff in listPromotionProduct)
        //            {
        //                if (draff.Discount == 0)
        //                {
        //                    break;
        //                }
        //                else
        //                {
        //                    var exitpromotion = dbConn.Select<CRM_Contract_ListPromotionProduct_Draff>(p => p.PKPromotion == draff.PKPromotion).FirstOrDefault();
        //                    if (exitpromotion != null)
        //                    {
        //                        exitpromotion.Discount = draff.Discount;
        //                        exitpromotion.Promotion = draff.Promotion;
        //                        exitpromotion.AmtNoVAT = draff.AmtNoVAT;
        //                        exitpromotion.RowUpdatedAt = DateTime.Now;
        //                        exitpromotion.RowUpdatedUser = currentUser.UserName;
        //                        dbConn.Update(exitpromotion);
        //                    }
        //                    else
        //                    {
        //                        var promotion = new CRM_Contract_ListPromotionProduct_Draff();
        //                        promotion = draff;
        //                        promotion.FKProduct = PKProduct;
        //                        promotion.RowCreatedAt = DateTime.Now;
        //                        promotion.RowCreatedUser = currentUser.UserName;
        //                        promotion.RowUpdatedAt = DateTime.Parse("1900-01-01");
        //                        promotion.RowUpdatedUser = "";
        //                        dbConn.Insert(promotion);
        //                    }
        //                }
        //            }
        //            return Json(new { success = true });
        //        }
        //        else
        //        {
        //            return Json(new { success = true });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Có lỗi" });
        //    }
        //}

        public ActionResult GetAmtByContract(string ContractID)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var data = dbConn.Select<CRM_Contract_TotalAmount_Product_Draff>(@"Select ISNULL(SUM(TotalAmtAfterDiscount-TotalAmtSevicePlus),0) AS TotalAmtNoVAT
                              FROM CRM_Contract_Product_Draff
                              Where FKContract={0}
                              GROUP BY FKContract", ContractID);
                    if (data.Count > 0)
                    {
                        return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, data = data }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ContractDraff_Save(CRM_Contract_Draff draffContract)
        {
            var rs = CRM_Contract_Draff.Save(draffContract, currentUser.UserName);
            return Json(rs);
        }

        public ActionResult PaymentScheduleDraft_Save(IEnumerable<CRM_PaymentScheduleDraft> listPayment)
        {
            var rs = CRM_PaymentScheduleDraft.Save(listPayment, currentUser.UserName);
            return Json(rs);
        }

        public ActionResult ContractStaffDraff_Save(IEnumerable<CRM_Contract_Staff_Draff> listStaff)
        {
            var rs = CRM_Contract_Staff_Draff.Save(listStaff, currentUser.UserName);
            return Json(rs);
        }

        public ActionResult ContractStaffDraff_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Staff_Draff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult SuggestEmployer(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<EmployeeInfo>(
                    @"SELECT Top 10 e.*, t.TeamName as TeamName,h.Value as RegionName
		                FROM EmployeeInfo e
		                left join CRM_Team t ON e.Team = t.TeamID
		                left join CRM_Hierarchy h ON e.Region = h.HierarchyID
                        WHERE e.FullName COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'"
                      );
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Contract_Delete_List_ById(int Id, string type)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {

                try
                {
                    if (type == "Time")
                    {
                        dbConn.DeleteById<CRM_Contract_ListTime_Draff>(Id);
                    }
                    else if (type == "Promotion")
                    {
                        dbConn.DeleteById<CRM_Contract_ListPromotionProduct_Draff>(Id);
                    }
                    else if (type == "DiscountAll")
                    {
                        dbConn.DeleteById<CRM_Contract_Draff_Promotion>(Id);
                    }
                    else if (type == "PayPal")
                    {
                        dbConn.DeleteById<CRM_PaymentScheduleDraft>(Id);
                    }
                    else if (type == "1")
                    {
                        dbConn.Delete<CRM_Contract_ListTime_Draff>(s => s.FKProduct == Id);
                        dbConn.Delete<CRM_Contract_ListPromotionProduct_Draff>(s => s.FKProduct == Id);
                        dbConn.DeleteById<CRM_Contract_Product_Draff>(Id);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult ContractProductCPM_Save(IEnumerable<CRM_Contract_Product_CPM_Draff> listProductCPM)
        {
            var rs = CRM_Contract_Product_CPM_Draff.Save(listProductCPM, currentUser.UserName);
            return Json(rs);
        }

        public ActionResult ContractProductCPM_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Product_CPM_Draff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult SuggestFromList(string text, int PKList)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<ERPAPD_List>(
                    @"SELECT Top 5 l.PKList,Code,Name
		                FROM ERPAPD_List l
                        WHERE FKListtype = " + PKList + "  AND l.Name COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'"
                      );
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ContractDraftPromotion_Save(IEnumerable<CRM_Contract_Draff_Promotion> listPromotion)
        {
            var rs = CRM_Contract_Draff_Promotion.Save(listPromotion, currentUser.UserName);
            return Json(rs);
        }

        public ActionResult ContractProtion_Delete(int PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Draff_Promotion>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
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
                        foreach (var draff in listRowID)
                        {
                            var check = dbConn.FirstOrDefault<CRM_Contract_Draff>("PKContractDraft={0}", draff);
                            check.TrangThai = 100;
                            dbConn.Update(check);
                            // dbConn.Delete(check);
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

        // HD GOI
        public ActionResult ContractProductHDG_Save(IEnumerable<CRM_Contract_Product_Packet_Draff> listProductHDG)
        {
            var rs = CRM_Contract_Product_Packet_Draff.Save(listProductHDG, currentUser.UserName);
            return Json(rs);
        }

        public ActionResult ContractProductHDG_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Product_Packet_Draff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult UpdateStatus(CRM_Contract_Draff draffcurrent, HttpPostedFileBase FileGuiDuyet, HttpPostedFileBase FileDuyet)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (draffcurrent.ActionSelected == "GUI")
                        {
                            var exit = dbConn.Select<CRM_Contract_Draff>(p => p.PKContractDraft == draffcurrent.PKContractDraft).FirstOrDefault();
                            if (exit != null)
                            {
                                if (FileGuiDuyet != null)
                                {
                                    var fileName = Path.GetFileName(FileGuiDuyet.FileName);
                                    Helpers.UploadFile.CreateFolder(Server.MapPath("~/attach-file/contract_draff/"));
                                    var path = Path.Combine(Server.MapPath("~/attach-file/contract_draff/"), fileName);
                                    FileGuiDuyet.SaveAs(path);
                                    exit.FileGuiDuyet = fileName;
                                }

                                dbConn.Update<CRM_Contract_Draff>(set: "LyDoGuiDuyet=N'" + draffcurrent.LyDoGuiDuyet + "' , ThoiGianGuiDuyet='"
                                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', TrangThai=" + 2 + ", RowUpdatedAt='" + DateTime.Now + "', RowUpdatedUser='" + currentUser.UserName
                                    + "', FileGuiDuyet='" + exit.FileGuiDuyet + "'",
                                    where: "PKContractDraft=" + exit.PKContractDraft + "");
                                //Write histoy
                                CRM_Contract_History_Action.write(draffcurrent.PKContractDraft, "Gửi duyệt",
                                    exit.FKStaff, DateTime.Now, 0, DateTime.Parse("1900-01-01"), draffcurrent.LyDoGuiDuyet,
                                    "", currentUser.UserName, exit.FileGuiDuyet);
                            }
                        }
                        else if (draffcurrent.ActionSelected == "DUYET")
                        {
                            var exit = dbConn.Select<CRM_Contract_Draff>(p => p.PKContractDraft == draffcurrent.PKContractDraft).FirstOrDefault();
                            var Action = "";
                            if (exit != null)
                            {
                                if (draffcurrent.TrangThai == 1)
                                {
                                    exit.TrangThai = 1;
                                    Action = "Duyệt";
                                }
                                else
                                {
                                    exit.TrangThai = 3;
                                    Action = "Từ chối";
                                }
                                exit.NguoiDuyet = dbConn.FirstOrDefault<EmployeeInfo>(p => p.UserName == currentUser.UserName).RefStaffId;
                                if (FileDuyet != null)
                                {
                                    var fileName = Path.GetFileName(FileDuyet.FileName);
                                    //Helpers.UploadFile.CreateFolder(Server.MapPath("~/attach-file/contract_draff/"));
                                    var path = Path.Combine(Server.MapPath("~/attach-file/contract_draff/"), fileName);
                                    FileDuyet.SaveAs(path);
                                    exit.FileDuyet = fileName;
                                }
                                dbConn.Update<CRM_Contract_Draff>(set: "LyDoDuyet=N'" + draffcurrent.LyDoDuyet + "' , ThoiGianDuyet='"
                                     + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', TrangThai=" + exit.TrangThai + ", RowUpdatedAt='" + DateTime.Now + "', RowUpdatedUser='" + currentUser.UserName
                                     + "', FileDuyet='" + exit.FileDuyet + "', NguoiDuyet=" + exit.NguoiDuyet + "",
                                     where: "PKContractDraft=" + exit.PKContractDraft + "");
                                //Write histoy
                                CRM_Contract_History_Action.write(draffcurrent.PKContractDraft, Action,
                                    exit.FKStaff, exit.ThoiGianGuiDuyet, exit.NguoiDuyet, DateTime.Now, draffcurrent.LyDoDuyet,
                                    "", currentUser.UserName, exit.FileDuyet);
                            }
                        }
                        else if (draffcurrent.ActionSelected == "DIEUCHINH" || draffcurrent.ActionSelected == "HUYDB")
                        {
                            var exit = dbConn.Select<CRM_Contract_Draff>(p => p.PKContractDraft == draffcurrent.PKContractDraft).FirstOrDefault();
                            var Action = "";
                            if (draffcurrent.ActionSelected == "DIEUCHINH")
                            {
                                exit.TrangThai = 102;
                                Action = "Điều chỉnh";
                            }
                            else
                            {
                                exit.TrangThai = 0;
                                Action = "Hủy đồng bộ";
                            }
                            if (exit != null)
                            {
                                dbConn.Update<CRM_Contract_Draff>(set: "TrangThai=" + exit.TrangThai + ", RowUpdatedAt='" + DateTime.Now + "', RowUpdatedUser='" + currentUser.UserName + "'"
                                       , where: "PKContractDraft=" + exit.PKContractDraft + "");
                                //Write history
                                CRM_Contract_History_Action.write(draffcurrent.PKContractDraft, Action,
                                    exit.FKStaff, exit.ThoiGianGuiDuyet, dbConn.FirstOrDefault<EmployeeInfo>(p => p.UserName == currentUser.UserName).RefStaffId, DateTime.Parse("1900-01-01"), "Gửi điều chỉnh",
                                    "", currentUser.UserName, "");
                                // AThao yêu cầu
                                // Xóa tất cả các bảng liên quan tới HD.
                                // Lấy thông tin hợp đồng của theo Mã HĐ
                                var itemContract = dbConn.Select<CRM_Contract>(s => s.c_code == exit.Code);
                                if (itemContract != null)
                                {
                                    //Xóa các bảng liên quan
                                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadUncommitted))
                                    {
                                        foreach (var item in itemContract)
                                        {
                                            //Bảng nhân viên
                                            dbConn.Delete<CRM_Contract_Staff>(s => s.fk_contract == item.pk_contract);
                                            //Bảng hợp đồng
                                            dbConn.Delete<CRM_Contract>(s => s.pk_contract == item.pk_contract);
                                            //Bảng sản phẩm Thường
                                            dbConn.Delete<CRM_Contract_Product>(s => s.FkContract == item.pk_contract);
                                            //Bảng ngày lên xuống theo hd
                                            dbConn.Delete<ERPAPD_Banner_Update>(s => s.fk_contract == item.pk_contract);
                                            //Bảng sp là CPM
                                            dbConn.Delete<CRM_Contract_Product_CPM>(s => s.FkContract == item.pk_contract);
                                            //Bảng sp là Gói
                                            dbConn.Delete<CRM_Contract_Product_Packet>(s => s.FKContract == item.pk_contract);
                                            //Bảng sp là Gói
                                            dbConn.Delete<CRM_GET_MONEY_MONTH_NEXT>(s => s.fk_contract == item.pk_contract);
                                        }
                                        dbTrans.Commit();
                                    }

                                }
                            }
                        }
                        else if (draffcurrent.ActionSelected == "DONGBO")
                        {
                            try
                            {
                                var draff = dbConn.Select<CRM_Contract_Draff>(p => p.PKContractDraft == draffcurrent.PKContractDraft).FirstOrDefault();
                                var customer = dbConn.FirstOrDefault<ERPAPD_Customer>("CodeLink ={0}", draff.CustomerCode); // check lại
                                if (customer == null)
                                {
                                    return Json(new { success = false, message = "Không lấy được thông tin khách hàng" });
                                }
                                else
                                {
                                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadUncommitted))
                                    {
                                        // sync general :
                                        var row = new CRM_Contract();
                                        row.c_code = draff.Code.Trim();
                                        row.c_internal_code = "";
                                        row.c_name = "";
                                        row.c_agency_type = draff.AgencyType;
                                        if (draff.LoaiHopDong == "CPM" || draff.LoaiHopDong == "PHIEUCPM")
                                        {
                                            row.c_contract_type = draff.LoaiHopDong;
                                            var itime = dbConn.Select<CRM_Contract_Product_CPM_Draff>(@"select 1 AS PKCpm ,ISNULL(Min(DateUp),'1900-01-01') AS DateUp ,ISNULL(MAX(DateDown),'1900-01-01') AS DateDown  from 
                                                                                        CRM_Contract_Product_CPM_Draff
                                                                                           where FkContract={0}", draffcurrent.PKContractDraft);
                                            if (itime != null)
                                            {
                                                row.c_begin_date = itime.FirstOrDefault().DateUp;
                                                row.c_end_date = itime.FirstOrDefault().DateDown;
                                            }
                                            else
                                            {
                                                row.c_begin_date = DateTime.Parse("1900-01-01");
                                                row.c_end_date = DateTime.Parse("1900-01-01");
                                            }
                                        }
                                        else if (draff.LoaiHopDong == "THUONG" || draff.LoaiHopDong == "PHIEU" || draff.LoaiHopDong == "PHIEUPR")
                                        {
                                            if (draff.LoaiHopDong == "THUONG")
                                            {
                                                row.c_contract_type = "HD_THUONG";
                                            }
                                            else
                                            {
                                                row.c_contract_type = draff.LoaiHopDong;
                                            }
                                            var itime = dbConn.Select<CRM_Contract_ListTime_Draff>(@"select 1 AS PKTime ,ISNULL(Min(DateUp),'1900-01-01') AS DateUp ,ISNULL(MAX(DateDown),'1900-01-01') AS DateDown  from 
                                                                                        CRM_Contract_ListTime_Draff
                                                                                           where FKContract={0}", draffcurrent.PKContractDraft);

                                            if (itime != null)
                                            {
                                                row.c_begin_date = itime.FirstOrDefault().DateUp;
                                                row.c_end_date = itime.FirstOrDefault().DateDown;
                                            }
                                            else
                                            {
                                                row.c_begin_date = DateTime.Parse("1900-01-01");
                                                row.c_end_date = DateTime.Parse("1900-01-01");
                                            }
                                        }
                                        else if (draff.LoaiHopDong == "GOI")
                                        {
                                            row.c_contract_type = "GOI";
                                            var itime = dbConn.Select<CRM_Contract_Product_Packet_Draff>(@"select 1 AS PKPacket, ISNULL(Min(DateUp), '1900-01-01') AS DateUp, ISNULL(MAX(DateDown), '1900-01-01') AS DateDown  from
                                                                                          CRM_Contract_Product_Packet_Draff
                                                                                             where FKContract ={0}", draffcurrent.PKContractDraft);

                                            if (itime != null)
                                            {
                                                row.c_begin_date = itime.FirstOrDefault().DateUp;
                                                row.c_end_date = itime.FirstOrDefault().DateDown;
                                            }
                                            else
                                            {
                                                row.c_begin_date = DateTime.Parse("1900-01-01");
                                                row.c_end_date = DateTime.Parse("1900-01-01");
                                            }

                                        }
                                        row.c_revenue_date = draff.NgayVeHaiChieu;
                                        row.c_book_code = draff.BookCode;
                                        row.c_status = "HAI_CHIEU";

                                        // End
                                        row.c_banner_up = 0;
                                        row.c_customer_code = customer.CustomerCode;
                                        row.c_check_vat = draff.CheckVat;
                                        row.c_category_code = draff.CategoryCode.Trim();
                                        row.c_labels = draff.Labels.Trim();
                                        row.c_staff_id = draff.FKStaff;
                                        row.c_unit_id = draff.KFUnit;
                                        row.c_regionid = draff.RegionID;
                                        row.c_customer_acc = draff.CustomerCode;
                                        row.c_input_date = DateTime.Now;
                                        if (draff.CheckVat == 1)
                                        {
                                            row.c_total_money = Math.Round((draff.TongTienHD / (1.1)), 0); // DS ký = tổng tiền hợp đồng 
                                        }
                                        else
                                        {
                                            row.c_total_money = draff.TongTienHD; // DS Ky
                                        }
                                        row.c_total_value = draff.TongTienHD;
                                        row.c_total_vat = 0;
                                        CultureInfo cul = CultureInfo.CurrentCulture;
                                        row.c_week = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                                        row.c_year = cul.Calendar.GetYear(DateTime.Now);
                                        row.c_month = cul.Calendar.GetMonth(DateTime.Now);

                                        //
                                        var lastPKContract = CRM_Contract.Sync(row, currentUser.UserName);
                                        // sync product:
                                        if (draff.LoaiHopDong == "THUONG" || draff.LoaiHopDong == "PHIEU" || draff.LoaiHopDong == "PHIEUPR")
                                        {
                                            var product_draft = CRM_Contract_Product_Draff.getProductByPKContract(draff.PKContractDraft);
                                            var product_promotion = dbConn.Select<CRM_Contract_Draff_Promotion>(s => s.FKContract == draff.PKContractDraft);
                                            if (product_draft.Count > 0)
                                            {
                                                // update c_service_type
                                                var serviceType = "";
                                                product_draft.ForEach(s =>
                                                {
                                                    var item = new CRM_Contract_Product();
                                                    item.FkContract = (int)lastPKContract;
                                                    item.FkProduct = s.FKProduct;
                                                    item.Code = "";
                                                    item.Website = s.Website;
                                                    item.ProductType = s.ProductType;
                                                    item.Category = s.Category;
                                                    item.Location = s.Location;
                                                    item.Nature = s.Nature;
                                                    double unitPrice = s.Price - s.CKTienMat;
                                                    foreach (var dc in s.ListPromotion)
                                                    {
                                                        unitPrice = unitPrice - (dc.Discount * unitPrice / 100);
                                                    }
                                                    if (product_promotion != null)
                                                    {
                                                        // Tính lại khi có chiết khấu chung
                                                        foreach (var promo in product_promotion)
                                                        {
                                                            unitPrice = unitPrice - (unitPrice * promo.ChietKhauChung / 100);
                                                        }
                                                    }
                                                    double lastMoneyOfItem = 0;
                                                    if (item.ProductType == "BANNER")
                                                    {
                                                        var sl = Math.Round((double)(s.NumberDay / 7.0), 2);
                                                        lastMoneyOfItem = sl * unitPrice;
                                                        item.Number = sl;
                                                        serviceType = item.ProductType;
                                                    }
                                                    else
                                                    {
                                                        item.ProductType = "DB";
                                                        lastMoneyOfItem = unitPrice * s.Number;
                                                        item.Number = s.Number;
                                                        serviceType = "PR";

                                                    }
                                                    item.PriceCharged = unitPrice; // Giá TT
                                                    item.PricePublic = s.Price; // Giá public
                                                    item.Price = lastMoneyOfItem; // Tổng tiền
                                                    item.RowCreatedAt = DateTime.Now;
                                                    item.RowCreatedUser = currentUser.UserName;
                                                    item.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                                    item.RowUpdatedUser = "";
                                                    dbConn.Insert(item);
                                                });
                                                dbConn.Update<CRM_Contract>(set: "c_service_type={0}".Params(serviceType), where: "pk_contract={0}".Params((int)lastPKContract));
                                            }
                                            // Ngay Len/Xuong theo hợp đồng
                                            var listtime = dbConn.Select<CRM_Contract_ListTime_Draff>(s => s.FKContract == draff.PKContractDraft);
                                            if (listtime.Count > 0)
                                            {
                                                listtime.ForEach(s =>
                                                {

                                                    var item = new ERPAPD_Banner_Update();
                                                    item.fk_contract = (int)lastPKContract;

                                                    var product = dbConn.SingleOrDefault<CRM_Contract_Product_Draff>("PKProduct = {0}", s.FKProduct);
                                                    if (product != null)
                                                    {
                                                        item.c_website = product.Website;
                                                        item.c_category = product.Category;
                                                        item.c_location = product.Location;
                                                        item.c_nature = product.Nature;
                                                    }

                                                    item.StrUpdate = s.DateUp.ToString();
                                                    item.StrDowndate = s.DateDown.ToString();
                                                    item.c_note = "";
                                                    //BaoHV add
                                                    TimeSpan Time = s.DateDown - s.DateUp;
                                                    int TongSoNgay = Time.Days + 1;
                                                    int SoTuan = TongSoNgay / 7;
                                                    int soNgayLe = TongSoNgay % 7;
                                                    if (soNgayLe > 0)
                                                    {
                                                        item.c_time = SoTuan.ToString() + " tuần " + soNgayLe.ToString() + " ngày";
                                                    }
                                                    else
                                                    {
                                                        item.c_time = SoTuan.ToString() + " tuần ";
                                                    }
                                                    ERPAPD_Banner_Update.SaveSingleItem(item, currentUser.UserName);

                                                });
                                            }

                                        }
                                        if (draff.LoaiHopDong == "GOI")
                                        {
                                            var packet_draff = dbConn.Select<CRM_Contract_Product_Packet_Draff>("FKContract = {0}", draff.PKContractDraft);
                                            if (packet_draff.Count > 0)
                                            {
                                                // update c_service_type
                                                var serviceType = "PR";
                                                packet_draff.ForEach(s =>
                                                {
                                                    var item = new CRM_Contract_Product();
                                                    item.FkContract = (int)lastPKContract;
                                                    item.FkProduct = 0;
                                                    item.Code = "";
                                                    item.Website = "24";
                                                    item.ProductType = "DB";
                                                    item.Category = "KHAC";
                                                    item.Location = "";
                                                    item.Nature = "";
                                                    item.PriceCharged = s.Total; // Giá TT
                                                    item.PricePublic = s.UnitPrice; // Giá public
                                                    item.Price = s.Total; // Tổng tiền
                                                    item.Number = 1;
                                                    item.RowCreatedAt = DateTime.Now;
                                                    item.RowCreatedUser = currentUser.UserName;
                                                    item.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                                    item.RowUpdatedUser = "";
                                                    dbConn.Insert(item);
                                                    //Sync time
                                                    var itemtime = new ERPAPD_Banner_Update();
                                                    itemtime.fk_contract = (int)lastPKContract;
                                                    itemtime.c_website = item.Website;
                                                    itemtime.c_category = item.Category;
                                                    itemtime.c_location = item.Location;
                                                    itemtime.c_nature = item.Nature;
                                                    itemtime.StrUpdate = s.DateUp.ToString();
                                                    itemtime.StrDowndate = s.DateDown.ToString();
                                                    itemtime.c_note = "";
                                                    itemtime.c_time = "";
                                                    ERPAPD_Banner_Update.SaveSingleItem(itemtime, currentUser.UserName);
                                                });
                                                dbConn.Update<CRM_Contract>(set: "c_service_type={0}".Params(serviceType), where: "pk_contract={0}".Params((int)lastPKContract));

                                            }
                                        }
                                        if (draff.LoaiHopDong == "CPM" || draff.LoaiHopDong == "PHIEUCPM")
                                        {

                                            var cpm_draft = dbConn.Select<CRM_Contract_Product_CPM_Draft>("SELECT * FROM CRM_Contract_Product_CPM_Draff WHERE FkContract= {0}", draff.PKContractDraft);
                                            if (cpm_draft.Count > 0)
                                            {
                                                // update c_service_type
                                                var serviceType = "CPM";
                                                cpm_draft.ForEach(s =>
                                                {
                                                    var item = new CRM_Contract_Product_CPM();
                                                    item.FkContract = (int)lastPKContract;
                                                    item.Code = s.Code;
                                                    item.AdvChannel = s.AdvChannel;
                                                    item.Location = s.Location;
                                                    item.Category = s.Category;
                                                    item.DateUp = s.DateUp;
                                                    item.DateDown = s.DateDown;
                                                    item.UnitPrice = s.UnitPrice;
                                                    item.Quantity = s.Quantity;
                                                    item.Discount = s.Discount;
                                                    item.TotalAmt = s.TotalAmt;
                                                    item.InputDate = s.InputDate;
                                                    item.RowCreatedAt = DateTime.Now;
                                                    item.RowCreatedUser = currentUser.UserName;
                                                    item.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                                    item.RowUpdatedUser = "";
                                                    dbConn.Insert(item);
                                                });
                                                dbConn.Update<CRM_Contract>(set: "c_service_type={0}".Params(serviceType), where: "pk_contract={0}".Params((int)lastPKContract));
                                            }
                                        }

                                        // sync Lịch thanh toán
                                        var schedule_draft = dbConn.Select<CRM_PaymentScheduleDraft>("FKContract = {0}", draff.PKContractDraft);
                                        if (schedule_draft.Count > 0)
                                        {
                                            schedule_draft.ForEach(s =>
                                            {
                                                var item = new CRM_GET_MONEY_MONTH_NEXT();
                                                item.fk_contract = (int)lastPKContract;
                                                item.c_ngay_du_kien_thu = s.Date;
                                                item.c_ngay_tt_theo_hd = s.Date;
                                                item.c_contract_date = s.Date;
                                                item.c_payment_date = s.Date;
                                                item.c_note = s.Note;
                                                item.c_tien_du_kien_thu = (double)s.Money;
                                                item.c_tien_tt_theo_hd = (double)s.Money;
                                                //1. Quá hạn:
                                                //2. Đến hạn:Tính từ ngày dự kiến thanh toán <= 5 ngày so ngày hiện tại.
                                                //3. Chưa dên hạn: Lớn hơn 5 ngày 
                                                TimeSpan Time = DateTime.Now - item.c_ngay_du_kien_thu;
                                                int TongSoNgay = Time.Days;
                                                if (TongSoNgay > -4 && TongSoNgay <= 0)
                                                {
                                                    item.c_status = "2";
                                                }
                                                else if (TongSoNgay >= 1)
                                                {
                                                    item.c_status = "1";
                                                }
                                                else
                                                {
                                                    item.c_status = "3";
                                                }
                                                item.RowCreatedAt = DateTime.Now;
                                                item.RowCreatedUser = currentUser.UserName;
                                                item.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                                item.RowUpdatedUser = "";
                                                dbConn.Insert(item);
                                            });
                                        }
                                        // sync Staff
                                        var staffAdditional = dbConn.Select<CRM_Contract_Draff_Additional_Staff>("FKContract = {0}", draff.PKContractDraft);
                                        if (staffAdditional != null && staffAdditional.Count > 0)
                                        {
                                            staffAdditional.ForEach(s =>
                                            {
                                                var staff = new CRM_Contract_Staff();
                                                staff.fk_contract = (int)lastPKContract;
                                                staff.c_unit_id = s.UnitId;
                                                staff.c_staff_id = s.StaffId;
                                                staff.c_departments_id = 0;
                                                staff.c_group_id = s.GroupId;
                                                staff.c_percent = s.Percent;
                                                staff.c_money = s.Money;
                                                staff.c_money_in_year = s.MoneyInYear;
                                                staff.c_money_web_other = s.MoneyWebOther;
                                                staff.c_charge = s.Charge;
                                                staff.c_money_next_year = s.MoneyNextYear;
                                                staff.c_money_next_year2 = 0;
                                                dbConn.Insert(staff);
                                            });
                                        }
                                        else
                                        {
                                            var staff_draff = dbConn.Select<CRM_Contract_Staff_Draff>("FKContract = {0}", draff.PKContractDraft);
                                            if (staff_draff != null && staff_draff.Count > 0)
                                            {
                                                staff_draff.ForEach(s =>
                                                {
                                                    var staff = new CRM_Contract_Staff();
                                                    staff.fk_contract = (int)lastPKContract;
                                                    staff.c_unit_id = s.UnitId;
                                                    staff.c_staff_id = s.StaffId;
                                                    staff.c_departments_id = s.DepartmentId;
                                                    staff.c_group_id = s.GroupId;
                                                    staff.c_percent = s.Percent;
                                                    staff.c_money = s.Money;
                                                    staff.c_money_in_year = s.MoneyInYear;
                                                    staff.c_money_web_other = s.MoneyWebOther;
                                                    staff.c_charge = s.Charge;
                                                    staff.c_money_next_year = s.MoneyNextYear;
                                                    staff.c_money_next_year2 = s.MoneyNextYear2;
                                                    dbConn.Insert(staff);
                                                });
                                            }
                                        }
                                        

                                        draff.TrangThai = 10;// Đã đồng bộ.
                                        dbConn.Update(draff);
                                        //Write history
                                        CRM_Contract_History_Action.write(draffcurrent.PKContractDraft, "Đã đồng bộ",
                                            draff.FKStaff, draff.ThoiGianGuiDuyet, dbConn.FirstOrDefault<EmployeeInfo>(p => p.UserName == currentUser.UserName).RefStaffId, DateTime.Parse("1900-01-01"), "Đã đồng bộ",
                                            "", currentUser.UserName, "");

                                        dbTrans.Commit();
                                        return Json(new { success = true, lastPKContract = lastPKContract, contractType = row.c_contract_type });
                                    }

                                }

                            }
                            catch (Exception e)
                            {

                                return Json(new { success = false, message = e.Message });
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, message = e.Message });
                    }
                    finally { dbConn.Close(); }
                }
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult ReadHistory([DataSourceRequest]DataSourceRequest request, string Id)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();

                if (asset.View)
                {
                    string strQuery = @"SELECT
                                          A.*,
	                                      (SELECT TOP 1 Email FROM EmployeeInfo E WHERE E.RefStaffId=A.UserRequest) AS EmailRequester,
	                                      (SELECT TOP 1 FullName FROM EmployeeInfo E WHERE E.RefStaffId=A.UserRequest) AS UserNameRequester,
	                                      ISNULL((SELECT TOP 1 FullName FROM EmployeeInfo E WHERE E.RefStaffId=A.UserApproved),'') AS UserNameApprover
	                                      FROM CRM_Contract_History_Action A
	                                      LEFT JOIN CRM_Contract_Draff  B
	                                      ON A.ContractID=B.PKContractDraft
	                                      WHERE A.ContractID=" + Id + "";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Contract_History_Action>(request, strQuery, "");
                    if (data.Total == 0)
                    {
                        strQuery = @"SELECT DIStinct PKContractDraft AS ID,
                                   PKContractDraft AS ContractID,
	                               '' AS [Action],
	                               ThoiGianGuiDuyet AS RequestAt,
	                               ThoiGianDuyet AS ApprovedAt,
	                               LyDoDuyet AS Reason,
	                               LyDoGuiDuyet AS Note,
	                               NgayTao AS CreatedAt,
	                               RowCreatedUser AS CreatedUser,
	                               FileGuiDuyet AS FileGuiDuyet,
                                   (SELECT TOP 1 Email FROM EmployeeInfo E WHERE E.RefStaffId=A.FKStaff) AS EmailRequester,
                                   (SELECT TOP 1 FullName FROM EmployeeInfo E WHERE E.RefStaffId=A.FKStaff) AS UserNameRequester,
                                   ISNULL((SELECT TOP 1 FullName FROM EmployeeInfo E WHERE E.RefStaffId=A.NguoiDuyet),'') AS UserNameApprover
	                               FROM  CRM_Contract_Draff A
	                               JOIN EmployeeInfo E
	                               ON A.FKStaff=E.RefStaffId
                             WHERE  A.PKContractDraft=" + Id + "";
                        data = KendoApplyFilter.KendoDataByQuery<CRM_Contract_History_Action>(request, strQuery, "");
                    }
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ContractProductDraff_Save(IEnumerable<CRM_Contract_Product_Draff> listProduct)
        {
            var rs = CRM_Contract_Product_Draff.Save(listProduct, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult ContractProductDraff_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Product_Draff>(PK);
                    dbConn.Delete<CRM_Contract_ListTime_Draff>("FKProduct ={0}", PK);
                    dbConn.Delete<CRM_Contract_ListPromotionProduct_Draff>("FKProduct ={0}", PK);

                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult ContractTimeProductDraff_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_ListTime_Draff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult ContractPromotionProductDraff_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_ListPromotionProduct_Draff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        // Bang ke bo sung
        public ActionResult AdditionalList_Save(CRM_Contract_Draff_AdditionalList draffContract)
        {
            var rs = CRM_Contract_Draff_AdditionalList.Save(draffContract, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult AdditionalStaff_Save(IEnumerable<CRM_Contract_Draff_Additional_Staff> listItem)
        {
            var rs = CRM_Contract_Draff_Additional_Staff.Save(listItem, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult AdditionalStaff_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Draff_Additional_Staff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult PaymentSchedule_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_PaymentScheduleDraft>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public JsonResult CheckAdv(string c_code)
        {
            int c = 0;
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var query = dbConn.Select<CRM_Adv>(@"
                        SELECT X.c_code, SUM(DaDQC) c_dot_order
                         FROM
                         (
	                        Select a.c_code, ISNULL(Count(pk_contract),0) AS DaDQC
	                        From CRM_Contract a
	                        INNER JOIN  CRM_Real_PRLocation b ON a.pk_contract = b.FKContract
	                        GROUP BY a.c_code
	                        UNION ALL
	                        Select a.c_code, ISNULL(Count(pk_contract),0) AS DaDQC
	                        From CRM_Contract a
	                        INNER JOIN  CRM_Real_Update b ON a.pk_contract = b.fk_contract
	                        GROUP BY a.c_code
	                        UNION ALL
	                        Select a.c_code, ISNULL(Count(pk_contract),0) AS DaDQC
	                        From CRM_Contract a
	                        INNER JOIN  CRM_Adv b ON a.c_code = b.c_code
	                        GROUP BY a.c_code
                         ) AS X
                         WHERE c_code = '" + c_code + "' GROUP BY c_code"
                    ).FirstOrDefault();
                    c = query.c_dot_order;

                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e });
                }
            }

            return Json(new { success = true, count = c });
        }
    }
}