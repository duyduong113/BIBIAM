using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;
using System.Data;
using System.Collections.Generic;
using Dapper;
using System.IO;
using OfficeOpenXml;

namespace ERPAPD.Controllers
{
    public class CRMMnContractController : CustomController
    {
        // GET: CRMMnContract
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý hợp đồng";
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                var RegionbyUser = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,DepartmentID FROM EmployeeInfo WHERE UserName='" + currentUser.UserName + "'");
                if (RegionbyUser.DepartmentID == 0)
                {

                    ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>("select * from CRM_Hierarchy where HierarchyID IN (12,13,14) order by[HierarchyID] ASC");
                }
                else
                {
                    ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>(@"SELECT ID,[HierarchyID],Value FROM CRM_Hierarchy
                            WHERE [HierarchyID]=(select ParentID from CRM_Hierarchy where HierarchyID=" + RegionbyUser.DepartmentID + ")");

                }
                if (string.IsNullOrEmpty(RegionbyUser.Team))
                {
                    ViewBag.listTeam = dbConn.Select<CRM_Team>(@"SELECT * FROM [CRM_Team]
                                                    WHERE FKUnit IN (12,13)");
                }
                else
                {
                    ViewBag.listTeam = dbConn.Select<CRM_Team>(@"SELECT * FROM [CRM_Team]
                                                    WHERE TeamID = " + RegionbyUser.Team + "");

                }
                if (string.IsNullOrEmpty(RegionbyUser.Team) && RegionbyUser.DepartmentID == 0)
                {
                    ViewBag.listStaff = dbConn.Select<ERPAPD_Employee>(@"Select crm.RowID,crm.RefEmployeeID,crm.UserName,crm.Name
                                FROM ERPAPD_Employee crm
                                LEFT JOIN EmployeeInfo em
                                ON crm.UserName=em.UserName
                                WHERE em.DepartmentID IS NOT NULL AND em.Team IS NOT NULL");
                    ViewData["FlagViewAll"] = true;
                }
                else
                {
                    ViewData["FlagViewAll"] = false;
                    ViewBag.listStaff = dbConn.Select<ERPAPD_Employee>(s => s.UserName == currentUser.UserName);
                }
                ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "ContractStatus").OrderByDescending(s => s.ParamID);
                ViewBag.listServiceType = dbConn.Select<Parameters>("Type ={0}", "ServiceType").OrderByDescending(s => s.ParamID);
                ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>("Status={0} AND ID <> 1", 1);
                ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>("Status={0} AND Level = 2", 1);
                ViewBag.listYear = dbConn.Select<CRM_Contract>(@"SELECT DISTINCT 1 AS pk_contract, c_year FROM CRM_Contract WHERE LEN(c_year)>3 ORDER BY c_year DESC");
                ViewBag.listEmployee = dbConn.Select<EmployeeInfo>(@"SELECT A.Id,A.UserName,
                                                                    ISNULL(A.FullName,'') AS FullName ,ISNULL(A.Team,'') AS Team,
                                                                    ISNULL(B.TeamName,'') AS TeamName,ISNULL(A.RefStaffId,0) AS RefStaffId FROM EmployeeInfo A
                                                                    LEFT JOIN CRM_Team B ON
                                                                    A.Team=B.TeamID ");
                ViewBag.listBank = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 8");
                ViewBag.listPaymentForm = dbConn.Select<DropDownListData>("select Code ,Name from ERPAPD_List WHERE FKListtype = 33");

                ViewBag.listCongNo = dbConn.Select<CRM_Debit_Management>();
            }
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    //string whereCondition = "";
                    //if (request.Filters.Count > 0)
                    //{
                    //    whereCondition = " AND " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    //}
                    //var data = new CRM_Contract().GetPage(request, whereCondition);
                    //var jsonResult = Json(data, JsonRequestBehavior.AllowGet);
                    //jsonResult.MaxJsonLength = int.MaxValue;
                    //return jsonResult;
                    string where = "";
                    if (request.Filters.Count > 0 )
                    {
                        where = " and " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    }
                    //Debug.Write(DateTime.Now);
                    var data = CRM_Contract.GetPage(request.Page, request.PageSize, where);
                    //Debug.Write(DateTime.Now);
                    request.Page = 1;
                    request.Filters = null;
                    var result = data.ToDataSourceResult(request);
                    if (data.Rows.Count > 0)
                    {
                        result.Total = Convert.ToInt32(data.Rows[0]["RowCount"]);
                    }
                    var jsonResult = Json(result);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        public ActionResult Detail(Int32 Id, string typeContract = "")
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.Title = "Chi tiết hợp đồng";
                if (asset.View)
                {
                    ViewBag.listContractType = dbConn.Select<Parameters>(s => s.Type == "ContractType").OrderBy(s => s.ParamID);
                    ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>().OrderBy(s => s.HierarchyID);
                    ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>(s => s.Type == "CustomerType" && s.Active == true).OrderBy(s => s.Code);
                    var list = dbConn.Select<CRM_Contract>(@"
                         SELECT co.*,
                        (select  TOP 1 CustomerName from ERPAPD_Customer where CustomerCode = co.c_customer_code) AS c_customer_name,
                        (select  TOP 1 CustomerType from ERPAPD_Customer where CustomerCode = co.c_customer_code) AS c_customer_type,
                        (select TOP 1 ISNULL(Team,0) from EmployeeInfo where RefStaffId = co.c_staff_id) AS c_teamid,
                        (select  ISNULL(Value,'') from CRM_Hierarchy where HierarchyID = co.c_unit_id and ParentID=1) AS c_region_name,
	                    (select TOP 1  FullName from EmployeeInfo where RefStaffId = co.c_staff_id) AS c_staff_name,
	                    (select TOP 1  ISNULL(t.TeamName,'')  from EmployeeInfo e LEFT JOIN CRM_Team t ON e.Team = t.TeamID where e.RefStaffId = co.c_staff_id) AS c_teamname
                        FROM CRM_Contract co
                        where pk_contract =" + Id).FirstOrDefault();
                    ViewBag.itemContract = list;
                    ViewBag.listBank = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 8");
                    ViewBag.listPaymentForm = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 33");
                    ViewBag.PKContract = Id;
                    ViewBag.listPaymentSchedule = dbConn.Select<CRM_PaymentSchedule>(s => s.FKContract == Id);
                    ViewBag.listPaymentProgress = dbConn.Select<CRM_PaymentProgress>(s => s.FKContract == Id);
                    var listPayment = dbConn.Select<CRM_GET_MONEY_MONTH_NEXT>("SELECT * FROM CRM_GET_MONEY_MONTH_NEXT WHERE fk_contract =" + Id);
                    ViewBag.listGetMoneyMonthNext = dbConn.Select<CRM_GET_MONEY_MONTH_NEXT>("SELECT * FROM CRM_GET_MONEY_MONTH_NEXT WHERE fk_contract =" + Id);
                    ViewBag.listStatusPaypal = dbConn.Select<ERPAPD_List>(s => s.FKListtype == 29 && s.Status == 1);
                    ViewBag.itemdraff = dbConn.Select<CRM_Contract_Draff>(@"Select PKContractDraft,PhuongThucThanhToan from CRM_Contract_Draff where Code='" + list.c_code + "'").FirstOrDefault();
                    var listBill = dbConn.Select<CRM_Contract_Bill>("FKContract={0}", Id);
                    if (listBill.Count > 0)
                    {
                        ViewBag.listTypeContractBill = listBill.FirstOrDefault().Type;
                        ViewBag.listContractBill = listBill;
                    }
                    ViewBag.typeContract = typeContract;
                    ViewBag.servicetype = list.c_service_type;
                    ViewBag.ProductHDT = dbConn.Select<CRM_Contract_Product>(@"
                        SELECT p.*,(select  name from ERPAPD_List where code = p.Website and FKListtype = 20) AS WebsiteName,
                        (select  name from ERPAPD_List where code = p.ProductType and FKListtype = 19) AS ProductTypeName, 
                        (select  name from ERPAPD_List where code = p.Category and FKListtype = 22) AS CategoryName,
                        (select  name from ERPAPD_List where code = p.Location and FKListtype = 23) AS LocationName,
                        (select  name from ERPAPD_List where code = p.Nature and FKListtype = 25) AS NatureName 
                        FROM CRM_Contract_Product p
                        WHERE p.FkContract = " + Id);
                    ViewBag.ProductCPM = dbConn.Select<CRM_Contract_Product_CPM>(s => s.FkContract == Id);
                    ViewBag.bannerUpdate = dbConn.Select<ERPAPD_Banner_Update>(@"
                    SELECT bu.*, (select  name from ERPAPD_List where code = bu.c_website and FKListtype = 20) AS WebsiteName, 
                    (select  name from ERPAPD_List where code = bu.c_category and FKListtype = 22) AS CategoryName,
                    (select  name from ERPAPD_List where code = bu.c_location and FKListtype = 23) AS LocationName, 
                    (select  name from ERPAPD_List where code = bu.c_nature and FKListtype = 25) AS NatureName 
                    FROM ERPAPD_Banner_Update bu 
                    WHERE bu.fk_contract = " + Id);

                    ViewBag.realBanner = dbConn.Select<CRM_Real_Update>(@"
                    SELECT re.*, (select  name from ERPAPD_List where code = re.c_website and FKListtype = 20) AS WebsiteName, 
                    (select  name from ERPAPD_List where code = re.c_category and FKListtype = 22) AS CategoryName,
                    (select  name from ERPAPD_List where code = re.c_location and FKListtype = 23) AS LocationName, 
                    (select  name from ERPAPD_List where code = re.c_nature and FKListtype = 25) AS NatureName ,
                    (select Top 1 FullName from EmployeeInfo where RefStaffId = re.c_staff_id ) AS EmployerName
                    FROM CRM_Real_Update re 
                    WHERE re.fk_contract = " + Id);

                    ViewBag.realPR = dbConn.Select<CRM_Real_PRLocation>(@"
                    SELECT re.*, (select  name from ERPAPD_List where code = re.Website and FKListtype = 20) AS WebsiteName, 
                    (select  name from ERPAPD_List where code = re.Category and FKListtype = 22) AS CategoryName,
                    (select  name from ERPAPD_List where code = re.Location and FKListtype = 23) AS LocationName, 
                    (select  name from ERPAPD_List where code = re.VungMien and FKListtype = 52) AS TenVungMien
                    FROM CRM_Real_PRLocation re 
                    WHERE re.FKContract = " + Id);

                    ViewBag.listBaoLanh = dbConn.Select<CRM_BaoLanh>(@" SELECT a.*
                                                     ,(SELECT Top 1 FullName FROM EmployeeInfo WHERE RefStaffId = a.c_nguoi_bao_lanh) AS NguoiBaoLanhName
                                                     ,(SELECT Top 1 FullName FROM EmployeeInfo WHERE RefStaffId = a.c_nguoi_ky_bao_lanh) AS NguoiKyBaoLanhName
                                                        FROM [CRM_BaoLanh] a WHERE a.fk_contract = {0}", Id);

                    ViewBag.listTest = dbConn.Select<CRM_Contract_Test>(@" SELECT a.* FROM [CRM_Contract_Test] a WHERE a.fk_contract = {0}", Id);

                    var fk_contract = new DynamicParameters();
                    fk_contract.Add("@fk_contract", Id);
                    ViewBag.listStaff = dbConn.Query<CRM_Contract_Staff>("p_getListStaff_Contract", fk_contract, commandType: System.Data.CommandType.StoredProcedure);
                    ViewBag.listContractStatus = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 28");
                    ViewBag.listManager = dbConn.Select<EmployeeInfo>(@"select id,RefStaffId,FullName from EmployeeInfo where RefStaffId in(32,75,116)");
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }

            }
        }
        public ActionResult Create(string typeContract)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.Title = "Thêm mới hợp đồng";
                if (asset.View)
                {
                    ViewBag.listContractType = dbConn.Select<Parameters>(s => s.Type == "ContractType").OrderBy(s => s.ParamID);
                    ViewBag.listContractStatus = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 28");
                    ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>().OrderBy(s => s.HierarchyID);
                    ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>(s => s.Type == "CustomerType" && s.Active == true).OrderBy(s => s.Code);
                    ViewBag.listBank = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 8");
                    ViewBag.listPaymentForm = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 33");
                    ViewBag.listManager = dbConn.Select<EmployeeInfo>(@"select id,RefStaffId,FullName from EmployeeInfo where RefStaffId in(32,75,116)");
                    ViewBag.PKContract = 0;
                    ViewBag.typeContract = typeContract;
                }
                return View("Detail");
            }
        }
        [HttpPost]
        public ActionResult Contract_Save(CRM_Contract itemContract)
        {
            var rs = CRM_Contract.Save(itemContract, currentUser.UserName);
            return Json(rs);
        }

        // HD_CPM
        public ActionResult ContractProductCPM_Save(IEnumerable<CRM_Contract_Product_CPM> listProductCPM)
        {
            var rs = CRM_Contract_Product_CPM.Save(listProductCPM, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult ContractProductCPM_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Product_CPM>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        // HD_THUONG
        public ActionResult ContractProduct_Save(IEnumerable<CRM_Contract_Product> listProduct)
        {
            var rs = CRM_Contract_Product.Save(listProduct, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult ContractProduct_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Product>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult ContractProductTime_Save(IEnumerable<ERPAPD_Banner_Update> listTime)
        {
            var rs = ERPAPD_Banner_Update.Save(listTime, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult ContractProductTime_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<ERPAPD_Banner_Update>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult PaymentSchedule_Save(IEnumerable<CRM_GET_MONEY_MONTH_NEXT> listSchedule)
        {
            try
            {
                var rs = CRM_GET_MONEY_MONTH_NEXT.Save(listSchedule, currentUser.UserName);
                return Json(rs);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }
        public ActionResult PaymentSchedule_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_GET_MONEY_MONTH_NEXT>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult PaymentHistory_Save(IEnumerable<CRM_PaymentProgress> listPayment)
        {
            var rs = CRM_PaymentProgress.Save(listPayment, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult PaymentHistory_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_PaymentProgress>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult Bill_Save(IEnumerable<CRM_Contract_Bill> listBill)
        {
            var rs = CRM_Contract_Bill.Save(listBill, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Bill_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Bill>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult Staff_Save(IEnumerable<CRM_Contract_Staff> listStaff)
        {
            var rs = CRM_Contract_Staff.Save(listStaff, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Staff_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Staff>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult Guarantee_Save(IEnumerable<CRM_BaoLanh> listGuarantee)
        {
            var rs = CRM_BaoLanh.Save(listGuarantee, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Guarantee_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_BaoLanh>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult ContractProductRealBanner_Save(IEnumerable<CRM_Real_Update> listTime)
        {
            var rs = CRM_Real_Update.Save(listTime, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult ContractProductRealBanner_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Real_Update>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult Export([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\HopDong.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "HopDong_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    int rowData = 3;
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Report"];


                    var listEmployee = dbConn.Select<EmployeeInfo>(@"SELECT A.Id,A.UserName,
                                                                    ISNULL(A.FullName,'') AS FullName ,ISNULL(A.Team,'') AS Team,
                                                                    ISNULL(B.TeamName,'') AS TeamName,ISNULL(A.RefStaffId,0) AS RefStaffId FROM EmployeeInfo A
                                                                    LEFT JOIN CRM_Team B ON
                                                                    A.Team=B.TeamID ");

                    string whereCondition = "";
                    if (request.Filters.Count > 0)
                    {
                        whereCondition = " AND " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    }

                    request.PageSize = 100;
                    var data = new CRM_Contract().GetExport(request, whereCondition);
                    foreach (var item in data)
                    {
                        int i = 1;
                        expenseSheet.Cells[rowData, i++].Value = item.c_week;
                        expenseSheet.Cells[rowData, i++].Value = item.c_month;
                        expenseSheet.Cells[rowData, i++].Value = item.c_contract_code;
                        expenseSheet.Cells[rowData, i++].Value = item.c_customer_name;
                        expenseSheet.Cells[rowData, i++].Value = item.c_labels;
                        expenseSheet.Cells[rowData, i++].Value = item.c_staffid;
                        expenseSheet.Cells[rowData, i++].Value = item.c_staffid;
                        expenseSheet.Cells[rowData, i++].Value = item.c_dt_da_qc_den_hom_nay; // DS Đã quảng cáo
                        expenseSheet.Cells[rowData, i++].Value = item.c_dt_da_xuat_ban; // DS Đã xuất bản
                        expenseSheet.Cells[rowData, i++].Value = item.c_payment_money; // DS Đã Thu
                        expenseSheet.Cells[rowData, i++].Value = item.c_balance; // Còn nợ
                        expenseSheet.Cells[rowData, i++].Value = item.c_total_money; // DS ký
                        expenseSheet.Cells[rowData, i++].Value = item.c_name; // DS Thực hiện 
                        expenseSheet.Cells[rowData, i++].Value = item.c_total_vat; // VAT
                        expenseSheet.Cells[rowData, i++].Value = item.c_get_money_next_week; // DS Chuyển
                        expenseSheet.Cells[rowData, i++].Value = item.c_ds_huy; // DS Hủy

                        rowData++;
                    }

                    expenseSheet.Cells.AutoFitColumns();
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }

            }
            else
            {
                return Json(new { success = false, message = "Không có quyền cập nhật" });
            }
        }
        public ActionResult ContractProductRealPR_Save(IEnumerable<CRM_Real_PRLocation> listTime)
        {
            var rs = CRM_Real_PRLocation.Save(listTime, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult ContractProductRealPR_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Real_PRLocation>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        // duyet additional list
        public ActionResult Confirmation_AdditionList(Int64 PK, bool status)
        {
            var rs = CRM_Contract_Draff_AdditionalList.comfirm(PK, status);
            return Json(rs);
        }

        public ActionResult GetContractByCode(string contractCode)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var contract = dbConn.SingleOrDefault<CRM_Contract>("c_code = {0}", contractCode);
                if (contract != null)
                {
                    return Json(new { success = true, data = contract });
                }
                else
                {
                    return Json(new { success = false, message = "ok" });
                }
            }

        }
        public ActionResult ExportListStatical(string text, string strDateUp, string strDateDown)
        {
            if (asset.Export)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    var customer = dbConn.Select<ERPAPD_Customer>(@"
                        SELECT CustomerCode
                            FROM ERPAPD_Customer 
                            WHERE CustomerName COLLATE Latin1_General_CI_AI LIKE N'%" + text + "%'"
                    ).FirstOrDefault();
                    var dateUp = DateTime.Parse(strDateUp);
                    var dateDown = DateTime.Parse(strDateDown);

                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\temp_bangke.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    string fileName = "BANGKE_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    int rowData = 6;
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Report"];

                    var list = new CRM_Contract().Export_BangKe(customer.CustomerCode, dateUp, dateDown);
                    foreach (var item in list)
                    {
                        int i = 1;
                        expenseSheet.Cells[rowData, i++].Value = rowData; // STT
                        expenseSheet.Cells[rowData, i++].Value = item.c_code;
                        expenseSheet.Cells[rowData, i++].Value = item.c_website_name + "/" + item.c_category_name;
                        expenseSheet.Cells[rowData, i++].Value = item.c_customer_name;
                        expenseSheet.Cells[rowData, i++].Value = "";
                        expenseSheet.Cells[rowData, i++].Value = "";
                        expenseSheet.Cells[rowData, i++].Value = item.c_don_gia_public;
                        expenseSheet.Cells[rowData, i++].Value = item.c_don_gia_tt * item.c_number;
                        expenseSheet.Cells[rowData, i++].Value = item.c_don_gia_theo_ngay;
                        expenseSheet.Cells[rowData, i++].Value = item.c_ngay_bu;
                        rowData++;
                    }

                    expenseSheet.Cells.AutoFitColumns();
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }

            }
            else
            {
                return Json(new { success = false, message = "Không có quyền cập nhật" });
            }
        }


        public ActionResult SaveListTest(IEnumerable<CRM_Contract_Test> listTest, string contract, string note, string test, string gg, string vp)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var item = dbConn.SingleOrDefault<CRM_Contract>("pk_contract = {0}", contract);
                if (item != null)
                {
                    item.c_note_mkt = note;
                    item.c_commission = double.Parse(test);
                    item.c_chi_phi_gg = double.Parse(gg);
                    item.c_chi_phi_vp = double.Parse(vp);
                    dbConn.Update(item);
                }

                if (listTest != null && listTest.Count() > 0)
                {
                    var rs = CRM_Contract_Test.Save(listTest, currentUser.UserName);
                    return Json(rs);
                }



                return Json(new { success = true });

            }
        }

        public ActionResult Test_Delete(Int64 PK)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    dbConn.DeleteById<CRM_Contract_Test>(PK);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult DeleteItem(string data)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadUncommitted))
                    {
                        foreach (var item in listdata)
                        {
                            //Xóa các bảng liên quan
                            //Bảng nhân viên
                            dbConn.Delete<CRM_Contract_Staff>(s => s.fk_contract == int.Parse(item));
                            //Bảng hợp đồng
                            dbConn.Delete<CRM_Contract>(s => s.pk_contract == int.Parse(item));
                            //Bảng sản phẩm Thường
                            dbConn.Delete<CRM_Contract_Product>(s => s.FkContract == int.Parse(item));
                            //Bảng ngày lên xuống theo hd
                            dbConn.Delete<ERPAPD_Banner_Update>(s => s.fk_contract == long.Parse(item));
                            //Bảng sp là CPM
                            dbConn.Delete<CRM_Contract_Product_CPM>(s => s.FkContract == long.Parse(item));
                            //Bảng sp là Gói
                            dbConn.Delete<CRM_Contract_Product_Packet>(s => s.FKContract == long.Parse(item));
                            //Bảng sp là Gói
                            dbConn.Delete<CRM_GET_MONEY_MONTH_NEXT>(s => s.fk_contract == long.Parse(item));
                        }
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
                   
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền xóa." });
            }
        }
        //===============================================Import export==================================================
        public FileResult ExportContract([DataSourceRequest]DataSourceRequest request)
        {
            ExcelPackage pck = new ExcelPackage(new FileInfo(Server.MapPath("~/ExportExcelFile/24HCRM_CONTRACT.xlsx")));
            ExcelWorksheet ws = pck.Workbook.Worksheets["Data"];
            
            if (asset.Export)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    string whereCondition = "";
                    if (request.Filters.Count > 0)
                    {
                        whereCondition = " AND " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    }
                    var data = CRM_Contract.Export(request.Page, request.PageSize, whereCondition);
                    int rowData = 3;

                    foreach (DataRow row in data.Rows)
                    {

                        int i = 1;
                        rowData++;
                        
                        ws.Cells[rowData, i++].Value = row["c_week"].ToString(); // Tuần
                        ws.Cells[rowData, i++].Value = row["c_month"].ToString(); // Tháng
                        ws.Cells[rowData, i++].Value = row["c_contract_code"].ToString(); // Số HĐ
                        ws.Cells[rowData, i++].Value = row["c_customer_name"].ToString();// Tên KH
                        ws.Cells[rowData, i++].Value = row["c_service_type"].ToString(); // Loại Dịch Vụ
                        ws.Cells[rowData, i++].Value = row["c_labels"].ToString(); // Nhãn
                        ws.Cells[rowData, i++].Value = row["c_teamname"].ToString(); // Nhóm
                        ws.Cells[rowData, i++].Value = row["c_staff_name"].ToString(); // NVKD
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_dt_da_qc_den_hom_nay") ? Convert.ToDouble(row["c_dt_da_qc_den_hom_nay"]) : 0; // DS đã QC đến ngày hôm nay
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_dt_da_xuat_ban") ? Convert.ToDouble(row["c_dt_da_xuat_ban"]) : 0; // DS Đã xuất bản 
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_payment_money") ? Convert.ToDouble(row["c_payment_money"]) : 0; // DS Đã Thu
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_balance") ? Convert.ToDouble(row["c_balance"]) : 0; // Còn nợ
                        ws.Cells[rowData, i++].Value = !row.IsNull("dang_quang_cao") ? row["dang_quang_cao"].ToString() : ""; // Đăng QC
                        //ws.Cells[rowData, i++].Value = row["c_payment_date"].ToString() : 0; // Hạn TT
                        //ws.Cells[rowData, i++].Value = row["c_note"].ToString() : 0; // Ghi chú
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_total_value") ? Convert.ToDouble(row["c_total_value"].ToString()) : 0; // Phải thu
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_total_vat") ? Convert.ToDouble(row["c_total_vat"]) : 0; // VAT
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_total_money") ? Convert.ToDouble(row["c_total_money"]) : 0; // DS ký
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_total_money_in_year") ? Convert.ToDouble(row["c_total_money_in_year"]) : 0; // DS thực hiện
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_total_money_next_year") ? Convert.ToDouble(row["c_total_money_next_year"]) : 0; // DS Chuyển
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_ds_huy") ? Convert.ToDouble(row["c_ds_huy"]) : 0; // DS Chờ hủy
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_tien_khong_tinh") ? Convert.ToDouble(row["c_tien_khong_tinh"]) : 0; // DS tiền không tính
                        ws.Cells[rowData, i++].Value = !row.IsNull("tien_xuat_hoa_don") ? Convert.ToDouble(row["tien_xuat_hoa_don"]) : 0;// Xuất HĐ
                        ws.Cells[rowData, i++].Value = !row.IsNull("tien_chua_xuat_hoa_don") ? Convert.ToDouble(row["tien_chua_xuat_hoa_don"]) : 0; // Chưa Xuất HĐ
                        ws.Cells[rowData, i++].Value = !row.IsNull("c_status_name") ? row["c_status_name"].ToString() : ""; // Trạng thái
                    }
                    ws.Cells[3, 9].Formula = "=Sum(" + ws.Cells[4, 9].Address + ":" + ws.Cells[rowData, 9].Address + ")";// Sum DS ĐÃ QC ĐẾN NGÀY HÔM NAY
                    ws.Cells[3, 10].Formula = "=Sum(" + ws.Cells[4, 10].Address + ":" + ws.Cells[rowData, 10].Address + ")";
                    ws.Cells[3, 11].Formula = "=Sum(" + ws.Cells[4, 11].Address + ":" + ws.Cells[rowData, 11].Address + ")";
                    ws.Cells[3, 12].Formula = "=Sum(" + ws.Cells[4, 12].Address + ":" + ws.Cells[rowData, 12].Address + ")";
                    ws.Cells[3, 14].Formula = "=Sum(" + ws.Cells[4, 14].Address + ":" + ws.Cells[rowData, 14].Address + ")";
                    ws.Cells[3, 15].Formula = "=Sum(" + ws.Cells[4, 15].Address + ":" + ws.Cells[rowData, 15].Address + ")";
                    ws.Cells[3, 16].Formula = "=Sum(" + ws.Cells[4, 16].Address + ":" + ws.Cells[rowData, 16].Address + ")";
                    ws.Cells[3, 17].Formula = "=Sum(" + ws.Cells[4, 17].Address + ":" + ws.Cells[rowData, 17].Address + ")";
                    ws.Cells[3, 18].Formula = "=Sum(" + ws.Cells[4, 18].Address + ":" + ws.Cells[rowData, 18].Address + ")";
                    ws.Cells[3, 19].Formula = "=Sum(" + ws.Cells[4, 19].Address + ":" + ws.Cells[rowData, 19].Address + ")";
                    ws.Cells[3, 20].Formula = "=Sum(" + ws.Cells[4, 20].Address + ":" + ws.Cells[rowData, 20].Address + ")";
                    ws.Cells[3, 21].Formula = "=Sum(" + ws.Cells[4, 21].Address + ":" + ws.Cells[rowData, 21].Address + ")";
                    ws.Cells[3, 22].Formula = "=Sum(" + ws.Cells[4, 22].Address + ":" + ws.Cells[rowData, 22].Address + ")";
                }
            }
            else
            {
                ws.Cells["A2:E2"].Merge = true;
                ws.Cells["A2"].Value = "You don't have permission to export data.";
            }
            ws.Column(1).AutoFit();
            MemoryStream output = new MemoryStream();
            pck.SaveAs(output);
            return File(output.ToArray(), //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "24HCRM_CONTRACT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}