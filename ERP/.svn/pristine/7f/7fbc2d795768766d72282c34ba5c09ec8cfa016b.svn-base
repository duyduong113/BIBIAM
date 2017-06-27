using ERPAPD.Models;
using System;
using ERPAPD.Helpers;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;
using System.IO;
using OfficeOpenXml;

namespace ERPAPD.Controllers
{
    public class CRMCustomerInfoController : CustomController
    {
        // GET: MnInfoEmployer
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
                            var RegionbyUser = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,DepartmentID,Team,Region,Position FROM EmployeeInfo WHERE UserName='" + currentUser.UserName + "'");
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
                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                        ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "CustomerStatus").OrderByDescending(s => s.ParamID);
                        //ViewBag.listTeam = dbConn.Select<CRM_Team>("Active=1 AND FKUnit IS NOT NULL").OrderByDescending(s => s.TeamID);
                        ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>().OrderByDescending(s => s.PKEmployeeID);
                        ViewBag.listPresent = dbConn.Select<Parameters>("Type='CustomerPresent'").OrderByDescending(s => s.ParamID);
                        ViewBag.listPotentialLevel = dbConn.Select<Parameters>("Type='CustomerPotential'").OrderBy(s => s.ParamID);
                        ViewBag.listCustomerCare = dbConn.Select<Parameters>("Type='CustomerCare'").OrderBy(s => s.ParamID);
                        ViewBag.listCustomerAgency = dbConn.Select<Parameters>("Type='CustomerAgency'").OrderBy(s => s.ParamID);
                        ViewBag.listCustomerContract = dbConn.Select<Parameters>("Type='CustomerContract'").OrderBy(s => s.ParamID);
                        ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>("Status={0} AND ID <> 1", 1);
                        ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>("Status={0} AND Level = 2", 1);
                        ViewBag.listLabel = dbConn.Select<CRM_Label>();

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
        // GET
        public ActionResult Create()
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
                        ViewBag.listSource = dbConn.Select<Parameters>("Type = 'CustomerSource'");
                        ViewBag.listCustomerAgency = dbConn.Select<Parameters>("Type='CustomerAgency'").OrderBy(s => s.ParamID);
                        ViewBag.listTypeOfBusiness = dbConn.Select<ERPAPD_MasterData_Customer>("Type = 'TypeOfBusiness'");
                        ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>("Type = 'CustomerType'");
                        ViewBag.listCustomerPosition = dbConn.Select<CRM_Position>();
                        ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>("Status= 1 AND Level = 1");
                        ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>("Status=1 AND Level > 1");
                        ViewBag.listStaff = dbConn.Select<ERPAPD_Employee>();
                        ViewBag.listCountry = dbConn.Select<CRM_Location_Countries>("SELECT CountryID,CountryName FROM CRM_Location_Countries");
                        ViewBag.listAgencyType = dbConn.Select<ERPAPD_List>(@"SELECT PKList,Code,Name FROM ERPAPD_List
																			  Where  FKListtype=57 AND Status=1");
                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                }

                return View("_Create");
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
                    string strQuery = @"SELECT  cus.*
                                               ,ISNULL(ps1.Value,'N/A') AS StatusName
                                               ,ISNULL(source.Value,'N/A') AS SourceName
                                               ,ISNULL(ps2.Value,'N/A') AS TypeName
                                               ,ISNULL(emp.Name,'N/A') AS EmployeeName
                                               ,ISNULL(team.TeamName,'N/A') AS TeamName
                                               ,ISNULL(unit.Value,'N/A') AS RegionName
                                        FROM ERPAPD_Customer cus
                                        LEFT JOIN [Parameters] ps1 ON ps1.ParamID = cus.Status AND ps1.Type = 'CustomerStatus'
                                        LEFT JOIN [Parameters] source ON source.ParamID = cus.Source AND source.Type = 'CustomerSource'
                                        LEFT JOIN [ERPAPD_MasterData_Customer] ps2 ON ps2.Code = cus.CustomerType AND ps2.Type = 'CustomerType'
                                        LEFT JOIN [ERPAPD_Employee] emp ON cus.StaffId = emp.RefEmployeeID 
                                        LEFT JOIN [CRM_Team] team ON team.TeamID = cus.GroupId 
                                        LEFT JOIN [CRM_Hierarchy] unit ON unit.HierarchyID = cus.UnitId 
                                        ";
                    data = KendoApplyFilter.KendoDataByQuery<ERPAPD_Customer>(request, strQuery, "");

                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult CreateCustomer(ERPAPD_Customer cus, HttpPostedFileBase file)
        {
            if (asset.Create)
            {
                ERPAPD_Customer item = new ERPAPD_Customer();
                try
                {
                    using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {

                        item.CustomerID = getLastIdCustomer();
                        item.ShortName = cus.ShortName;
                        item.CustomerName = cus.CustomerName;
                        item.TaxCode = cus.TaxCode;
                        item.UnitId = cus.UnitId;
                        item.Website = cus.Website;
                        item.Source = cus.Source;
                        item.CompanyType = cus.CompanyType;
                        item.Category = cus.Category;
                        item.CustomerType = cus.CustomerType;
                        //BaoHV
                        item.AgencyType = cus.AgencyType;
                        item.AgencyRule = cus.AgencyRule;
                        //End
                        item.Area = cus.Area;
                        item.Country = cus.Country;
                        item.Province = cus.Province;
                        item.District = cus.District;
                        item.Wards = cus.Wards;
                        item.Address = cus.Address;
                        item.Phone = cus.Phone;
                        item.Address2 = cus.Address2;
                        item.Phone2 = cus.Phone2;
                        item.Fax = cus.Fax;
                        item.Account = cus.Account;
                        //item.BankCode = cus.BankCode;
                        item.BankName = cus.BankName;
                        item.BankBranch = cus.BankBranch;
                        item.Sponsor = cus.Sponsor;
                        item.Position = cus.Position;
                        item.StaffId = cus.StaffId;
                        item.Status = "MOI";
                        item.RowCreatedUser = currentUser.UserName;
                        item.RowCreatedAt = DateTime.Now;

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            Helpers.UploadFile.CreateFolder(Server.MapPath("~/Images/" + item.CustomerID + "/"));
                            var path = Path.Combine(Server.MapPath("~/Images/" + item.CustomerID + "/"), fileName);
                            file.SaveAs(path);
                            item.FileName = fileName;
                        }

                        dbConn.Insert<ERPAPD_Customer>(item);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
                //return RedirectToAction("Index", "MnDetailEmployer", new { CustomerID = item.CustomerID });
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền." });

            }

        }
        public ActionResult ChangeStatus(string data, int type, string reasons = "", string CustomerID = "")
        {
            if (asset.Delete)
            {
                try
                {
                    if (data == "")
                    {
                        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                        {
                            var listCustomer = dbConn.FirstOrDefault<ERPAPD_Customer>(p => p.CustomerID == CustomerID);
                            if (listCustomer != null)
                            {
                                if (type == 1)
                                {
                                    listCustomer.Status = "CHUA_DUYET";
                                }
                                if (type == 2)
                                {
                                    listCustomer.Status = "HOAT_DONG";
                                }
                                if (type == 3)
                                {
                                    listCustomer.Status = "DUNG_HOAT_DONG";
                                    listCustomer.DeniedContent = reasons;
                                }
                                listCustomer.RowUpdatedAt = DateTime.Now;
                                listCustomer.RowUpdatedUser = currentUser.UserName;
                                dbConn.Update(listCustomer);
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, alert = "Không có Khách hàng nào có trạng thái hợp lệ" });
                            }
                        }
                    }
                    else
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                            {
                                var listCustomer = dbConn.Select<ERPAPD_Customer>("[CustomerID] IN (" + string.Join(",", listRowID.Select(p => "'" + p + "'")) + ")");
                                if (listCustomer.Count > 0)
                                {
                                    foreach (var cus in listCustomer)
                                    {
                                        if (type == 1)
                                        {
                                            cus.Status = "CHUA_DUYET";
                                        }
                                        if (type == 2)
                                        {
                                            cus.Status = "HOAT_DONG";
                                        }
                                        if (type == 3)
                                        {
                                            cus.Status = "DUNG_HOAT_DONG";
                                            cus.DeniedContent = reasons;
                                        }

                                        cus.RowUpdatedAt = DateTime.Now;
                                        cus.RowUpdatedUser = currentUser.UserName;
                                        dbConn.Update(cus);
                                    }
                                    return Json(new { success = true });
                                }
                                else
                                {
                                    return Json(new { success = false, alert = "Không có Khách hàng nào có trạng thái hợp lệ" });
                                }
                            }

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
        private static string getLastIdCustomer()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string CustomerID = String.Empty;
                string datetimeNow = DateTime.Now.ToString("yyMMdd");

                var lastId = dbConn.FirstOrDefault<ERPAPD_Customer>("SELECT TOP 1 * FROM ERPAPD_Customer ORDER BY CustomerID DESC");
                if (lastId.CustomerID.Contains(datetimeNow))
                {
                    var nextNo = Int32.Parse(lastId.CustomerID.Substring(6, 4)) + 1;
                    CustomerID = datetimeNow + String.Format("{0:0000}", nextNo);
                }
                else
                {
                    CustomerID = datetimeNow + "0001";
                }
                return CustomerID;
            }
        }
        public ActionResult SetLabel(List<object> listID)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    foreach (var item in listID)
                    {
                        string[] temp = (string[])item;
                        var customer = dbConn.FirstOrDefault<ERPAPD_Customer>("CustomerID={0}", temp[0]);

                        customer.Label = temp[1];
                        dbConn.Update(customer);
                    }

                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
        }
        // =========================================== Export Import ===================================================================
        public FileResult ExportCustomerInfo([DataSourceRequest]DataSourceRequest request)
        {
            ExcelPackage pck = new ExcelPackage(new FileInfo(Server.MapPath("~/ExportExcelFile/24HCRM_CUSTOMER_INFO.xlsx")));
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
                    string strQuery = @"SELECT  cus.*
                                               ,ISNULL(ps1.Value,'N/A') AS StatusName
                                               ,ISNULL(source.Value,'N/A') AS SourceName
                                               ,ISNULL(ps2.Value,'N/A') AS TypeName
                                               ,ISNULL(emp.Name,'N/A') AS EmployeeName
                                               ,ISNULL(team.TeamName,'N/A') AS TeamName
                                               ,ISNULL(unit.Value,'N/A') AS RegionName
                                        FROM ERPAPD_Customer cus
                                        LEFT JOIN [Parameters] ps1 ON ps1.ParamID = cus.Status AND ps1.Type = 'CustomerStatus'
                                        LEFT JOIN [Parameters] source ON source.ParamID = cus.Source AND source.Type = 'CustomerSource'
                                        LEFT JOIN [ERPAPD_MasterData_Customer] ps2 ON ps2.Code = cus.CustomerType AND ps2.Type = 'CustomerType'
                                        LEFT JOIN [ERPAPD_Employee] emp ON cus.StaffId = emp.RefEmployeeID 
                                        LEFT JOIN [CRM_Team] team ON team.TeamID = cus.GroupId 
                                        LEFT JOIN [CRM_Hierarchy] unit ON unit.HierarchyID = cus.UnitId 
                                        WHERE 1=1
                                        ";
                    var data = dbConn.Select<ERPAPD_Customer>(strQuery + whereCondition);
                    int rowData = 2;

                    foreach (var row in data)
                    {

                        int i = 2;
                        rowData++;
                        row.Phone = row.Phone.Replace(@"[", String.Empty);
                        row.Phone = row.Phone.Replace(@"]", String.Empty);
                        row.Website = row.Website.Replace(@"[", String.Empty);
                        row.Website = row.Website.Replace(@"]", String.Empty);
                        ws.Cells[rowData, i++].Value = "- " + row.CustomerName + "\n- Mã: " + row.CustomerID + "\n- Loại: " + row.TypeName;
                        ws.Cells[rowData, i++].Value = (!String.IsNullOrEmpty(row.Address) ? "- Địa chỉ: " + row.Address : "") + (!String.IsNullOrEmpty(row.Phone) ? "\n- Phone: " + row.Phone : "") + (!String.IsNullOrEmpty(row.Website) ? "\n- Website: " + row.Website : "");
                        string trangthai = String.Empty;
                        if(row.Status == "MOI"){
                             trangthai = "Chưa gửi";
                        }
                        if(row.Status == "CHUA_DUYET"){
                             trangthai = "Chờ duyệt";
                        }
                        if(row.Status == "HOAT_DONG"){
                             trangthai = "Đã duyệt";
                        }
                        if(row.Status == "DUNG_HOAT_DONG"){
                             trangthai = "Từ chối";
                        }
                        ws.Cells[rowData, i++].Value = ("\n- Tạo lúc: " + row.RowCreatedAt) + " - " + (row.RowCreatedUser) + "\n" + "- " + trangthai + ("\n- Cập nhật lúc: " + row.RowUpdatedAt) + " - " + (row.RowUpdatedUser);
                        ws.Cells[rowData, i++].Value = "- Nhân Viên: " + row.EmployeeName + "\n- " + row.TeamName + "\n- " + row.RegionName;
                    }
                }
            }
            else
            {
                ws.Cells["A2:E2"].Merge = true;
                ws.Cells["A2"].Value = "You don't have permission to export data.";
            }
            MemoryStream output = new MemoryStream();
            pck.SaveAs(output);
            return File(output.ToArray(), //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "24HCRM_CUSTOMER_INFO_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
        public ActionResult ImportFromExcel()
        {
            try
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                    {
                        string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                        if (fileExtension == ".xlsx")
                        {
                            string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                            string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);

                            Request.Files["FileUpload"].SaveAs(fileLocation);

                            var rownumber = 2;
                            var total = 0;

                            FileInfo fileInfo = new FileInfo(fileLocation);
                            var excelPkg = new ExcelPackage(fileInfo);

                            FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\TW_Goods_Category.xlsx"));
                            template.CopyTo(errorFileLocation);
                            FileInfo _fileInfo = new FileInfo(errorFileLocation);
                            var _excelPkg = new ExcelPackage(_fileInfo);

                            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["TW_Goods_Category"];
                            ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["TW_Goods_Category"];

                            //remove row
                            int totalRows = oSheet.Dimension.End.Row;
                            for (int i = 2; i <= totalRows; i++)
                            {
                                string CatID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "0";
                                try
                                {
                                    var write = new ERPAPD_Customer();
                                    var checkCatNumber = dbConn.FirstOrDefault<ERPAPD_Customer>("CatId={0}", CatID);
                                    if (checkCatNumber != null)
                                    {
                                        //write.Note = Note;
                                        dbConn.Update(write);
                                        total++;
                                    }
                                    else
                                    {
                                        //write.Note = Note;
                                        dbConn.Insert(write);
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    //eSheet.Cells[rownumber, 2].Value = CatName;
                                    //eSheet.Cells[rownumber, 3].Value = Status;
                                    //eSheet.Cells[rownumber, 5].Value = CatNumber;
                                    eSheet.Cells[rownumber, 10].Value = e.Message;
                                    rownumber++;
                                    continue;
                                }
                            }
                            _excelPkg.Save();

                            return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
                        }
                        else
                        {
                            return Json(new { success = false, error = "File extension is not valid. *.xlsx please." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "File upload null" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}