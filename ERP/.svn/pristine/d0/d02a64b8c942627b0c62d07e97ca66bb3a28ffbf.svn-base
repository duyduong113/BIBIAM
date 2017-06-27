using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Kendo.Mvc;
using System.ComponentModel;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ProfileManagementController : CustomController
    {
        //
        // GET: /ProfileManagement/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listCompany = dbConn.Select<Deca_Company>("Active={0}", true);
                    ViewBag.City = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    ViewBag.Bank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    ViewBag.Gender = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                    ViewBag.Education = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                    ViewBag.SourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                    ViewBag.CreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                    ViewBag.CustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");
                    ViewBag.Branch = dbConn.Select<DC_Company_Branch>().OrderBy(a => a.ID);
                    ViewBag.Assets = asset;
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    return Json(KendoApplyFilter.KendoData<Deca_Potential_Customer>(request, "CreditCardStatus NOT IN('CCS001')"));
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult GetListCreditStatus()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus").OrderByDescending(s => s.CodeID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListCreditStatusForChangeStatusRequest()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Deca_Code_Master>("[CodeType] ={0} AND CodeID IN ('CCS001','CCS002','CCS003','CCS008')", "CreditCardStatus").OrderBy(s => s.CodeID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RequestBank(string listOrderID, string Description, string BankID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(BankID))
                    {
                        try
                        {
                            int s = 0, f = 0;
                            string[] separators = { "@@" };
                            var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                            var listPotential = dbConn.Select<Deca_Potential_Customer>("[PhysicalID] IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS002','CCS003','CCS005')  AND IsForm = 1 AND HaveCard = 0");
                            if (listPotential.Count > 0)
                            {
                                foreach (var potential in listPotential)
                                {
                                    potential.IsNew = true;
                                    potential.Active = true;
                                    potential.IsForm = true;
                                    potential.DecaNote = Description;
                                    potential.CreditCardStatus = "CCS004";
                                    potential.SubmitedAt = DateTime.Now;
                                    potential.UpdatedAt = DateTime.Now;
                                    potential.UpdatedBy = currentUser.UserName;
                                    dbConn.Update(potential);
                                    s++;
                                }
                            }
                            else
                            {
                                return Json(new { error = true, message = "Vui lòng kiểm tra lại các thông tin Trạng thái mở thẻ hoặc Hồ sơ của khách hàng." });
                            }


                            dbTrans.Commit();
                            return Json(new { success = s, fail = f, error = false });
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            return Json(new { error = true, message = e.Message });
                        }
                    }
                    else
                    {
                        return Json(new { error = true, message = "Vui lòng chọn ngân hàng" });
                    }
                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        public ActionResult RequestStatus(string listOrderID, string Description, string StatusID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(StatusID))
                    {
                        try
                        {
                            int s = 0, f = 0;
                            string[] separators = { "@@" };
                            var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                            var listPotential = dbConn.Select<Deca_Potential_Customer>("[PhysicalID] IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus NOT IN ('CCS004','CCS006','CCS007') AND HaveCard = 0");
                            if (listPotential.Count > 0)
                            {
                                foreach (var potential in listPotential)
                                {
                                    potential.IsNew = true;
                                    potential.Active = true;
                                    if (StatusID == "CCS003") // Hẹn bổ sung hồ sơ
                                    {
                                        potential.WaitingProfile = DateTime.Now;
                                    }

                                    if (StatusID == "CCS008") // hủy bỏ
                                    {
                                        potential.Cancelled = DateTime.Now;
                                    }
                                    potential.DecaNote = Description;
                                    potential.CreditCardStatus = StatusID;
                                    potential.UpdatedAt = DateTime.Now;
                                    potential.UpdatedBy = currentUser.UserName;
                                    dbConn.Update(potential);
                                    s++;
                                }
                                dbTrans.Commit();
                                return Json(new { success = s, fail = f, error = false });
                            }
                            else
                            {
                                return Json(new { success = s, fail = f, message = "Không có Khách hàng nào có trạng thái hợp lệ" });
                            }

                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            return Json(new { error = true, message = e.Message });
                        }
                    }
                    else
                    {
                        return Json(new { error = true, message = "Vui lòng chọn ngân hàng" });
                    }
                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        public ActionResult DeniedRequest(string listOrderID, string Description)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {

                    try
                    {
                        int s = 0, f = 0;
                        string[] separators = { "@@" };
                        var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                        // Chỉ từ chối các trạng thái : Đã gửi Deca.
                        var listPotential = dbConn.Select<Deca_Potential_Customer>("[PhysicalID] IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS002') AND HaveCard = 0");
                        if (listPotential.Count > 0)
                        {
                            foreach (var potential in listPotential)
                            {
                                potential.IsNew = true;
                                potential.Active = true;
                                potential.DecaNote = Description;
                                potential.CreditCardStatus = "CCS008";
                                potential.Cancelled = DateTime.Now; // Ngày Deca từ chối
                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);
                                s++;
                            }
                            dbTrans.Commit();
                            return Json(new { success = s, fail = f, error = false });
                        }
                        else
                        {
                            return Json(new { success = s, fail = f, message = "Không có Khách hàng nào có trạng thái hợp lệ" });
                        }

                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = e.Message });
                    }

                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        public ActionResult WaitingProfile(string listOrderID, string Description)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {

                    try
                    {
                        int s = 0, f = 0;
                        string[] separators = { "@@" };
                        var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                        // Chỉ cập nhật Hẹn bổ sung hồ sơ. Khi ở trạng thái : Đã gửi Deca.
                        var listPotential = dbConn.Select<Deca_Potential_Customer>("[PhysicalID] IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS002') AND HaveCard = 0");
                        if (listPotential.Count > 0)
                        {
                            foreach (var potential in listPotential)
                            {
                                potential.IsNew = true;
                                potential.Active = true;
                                potential.DecaNote = Description;
                                potential.CreditCardStatus = "CCS003";
                                potential.WaitingProfile = DateTime.Now;
                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);
                                s++;
                            }
                            dbTrans.Commit();
                            return Json(new { success = s, fail = f, error = false });
                        }
                        else
                        {
                            return Json(new { success = s, fail = f, message = "Không có Khách hàng nào có trạng thái hợp lệ" });
                        }

                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = e.Message });
                    }

                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        public ActionResult AddProfile(string listOrderID, string Description)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {

                    try
                    {
                        int s = 0, f = 0;
                        string[] separators = { "@@" };
                        var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                        // Chỉ cập nhật IsForm = true. Khi ở trạng thái :  Hẹn bổ sung hồ sơ.
                        var listPotential = dbConn.Select<Deca_Potential_Customer>("[PhysicalID] IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS003') AND HaveCard = 0");
                        if (listPotential.Count > 0)
                        {
                            foreach (var potential in listPotential)
                            {
                                potential.IsNew = true;
                                potential.Active = true;
                                potential.IsForm = true;
                                potential.DecaNote = Description;

                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);
                                s++;
                            }
                            dbTrans.Commit();
                            return Json(new { success = s, fail = f, error = false });
                        }
                        else
                        {
                            return Json(new { success = s, fail = f, message = "Không có Khách hàng nào có trạng thái hợp lệ" });
                        }

                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = e.Message });
                    }

                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        public ActionResult AddSendBank(string listOrderID, string Description, string BankID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {

                    try
                    {
                        int s = 0, f = 0;
                        if (!String.IsNullOrEmpty(BankID))
                        {

                            string[] separators = { "@@" };
                            var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                            // Chỉ cập nhật IsForm = true. Khi ở trạng thái : Hẹn bổ sung hồ sơ.
                            var listPotential = dbConn.Select<Deca_Potential_Customer>("[PhysicalID] IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS003') AND HaveCard = 0");
                            if (listPotential.Count > 0)
                            {
                                foreach (var potential in listPotential)
                                {
                                    potential.IsNew = true;
                                    potential.Active = true;
                                    potential.DecaNote = Description;
                                    potential.IsForm = true;
                                    potential.CreditCardStatus = "CCS004";
                                    potential.WaitingProfile = DateTime.Now;
                                    potential.SubmitedAt = DateTime.Now;
                                    potential.UpdatedAt = DateTime.Now;
                                    potential.UpdatedBy = currentUser.UserName;
                                    dbConn.Update(potential);
                                    s++;
                                }
                                dbTrans.Commit();
                                return Json(new { success = s, fail = f, error = false });
                            }
                            else
                            {
                                return Json(new { success = s, fail = f, message = "Không có Khách hàng nào có trạng thái hợp lệ" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, fail = f, message = "Vui lòng chọn Ngân hàng" });
                        }



                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = e.Message });
                    }

                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Management_Profile.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Potential_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Potential_Customer>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Potential_Customer>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Potential_Customer>("SELECT TOP 50000 * FROM Deca_Potential_Customer").ToList();
                    }
                    var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                    var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                    var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                    var dataCompany = dbConn.Select<Deca_Company>();
                    var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");
                    var dataBranch = dbConn.Select<DC_Company_Branch>();
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        int i = 1;
                        rowData++;

                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = item.DecaNote;
                        expenseSheet.Cells[rowData, i++].Value = item.IsForm;
                        expenseSheet.Cells[rowData, i++].Value = dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus).Description;
                        expenseSheet.Cells[rowData, i++].Value = dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                        expenseSheet.Cells[rowData, i++].Value = item.CompanyName;
                        expenseSheet.Cells[rowData, i++].Value = dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID) == null ? "" : dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID).BranchName;
                        expenseSheet.Cells[rowData, i++].Value = item.EmployeeID;
                        expenseSheet.Cells[rowData, i++].Value = item.Fullname;
                        expenseSheet.Cells[rowData, i++].Value = dataSex.FirstOrDefault(a => a.CodeID == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.CodeID == item.Sex).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Birthday;
                        expenseSheet.Cells[rowData, i++].Value = item.Phone;
                        expenseSheet.Cells[rowData, i++].Value = item.Email;
                        expenseSheet.Cells[rowData, i++].Value = item.Address;
                        expenseSheet.Cells[rowData, i++].Value = dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown).TerritoryName;
                        expenseSheet.Cells[rowData, i++].Value = item.Department;
                        expenseSheet.Cells[rowData, i++].Value = item.Position;
                        expenseSheet.Cells[rowData, i++].Value = dataCustomerGroup.FirstOrDefault(a => a.CodeID == item.CustomerGroup) == null ? "" : dataCustomerGroup.FirstOrDefault(a => a.CodeID == item.CustomerGroup).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.StartWorkingDate;
                        expenseSheet.Cells[rowData, i++].Value = dataEducation.FirstOrDefault(a => a.CodeID == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Education).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Income;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = item.CreditLimit;
                        expenseSheet.Cells[rowData, i++].Value = item.DecaRequested;
                        expenseSheet.Cells[rowData, i++].Value = item.SubmitedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.WaitingProfile;
                        expenseSheet.Cells[rowData, i++].Value = item.BankDenied;
                        expenseSheet.Cells[rowData, i++].Value = item.IssuedCard;
                        expenseSheet.Cells[rowData, i++].Value = item.Done;
                        expenseSheet.Cells[rowData, i++].Value = item.Cancelled;
                        expenseSheet.Cells[rowData, i++].Value = item.Active;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedBy;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet2 = excelPkg.Workbook.Worksheets["List1"];
                    foreach (Deca_Code_Master master in dataSex)
                    {
                        dataSheet2.Cells[rowData++, 1].Value = master.Description;
                    }

                    rowData = 2;
                    ExcelWorksheet dataSheet3 = excelPkg.Workbook.Worksheets["List2"];
                    foreach (DC_OCM_Territory master in dataCity)
                    {
                        dataSheet3.Cells[rowData++, 1].Value = master.TerritoryName;
                    }

                    rowData = 2;
                    ExcelWorksheet dataSheet4 = excelPkg.Workbook.Worksheets["List3"];
                    foreach (DC_Bank_Definition master in dataBank)
                    {
                        dataSheet4.Cells[rowData++, 1].Value = master.BankName;
                    }

                    rowData = 2;
                    ExcelWorksheet dataSheet5 = excelPkg.Workbook.Worksheets["List4"];
                    foreach (Deca_Code_Master master in dataEducation)
                    {
                        dataSheet5.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet6 = excelPkg.Workbook.Worksheets["List5"];
                    foreach (Deca_Company master in dataCompany)
                    {
                        dataSheet6.Cells[rowData++, 1].Value = master.ShortName;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet7 = excelPkg.Workbook.Worksheets["List6"];
                    foreach (Deca_Code_Master master in dataCreditCardStatus)
                    {
                        dataSheet7.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet8 = excelPkg.Workbook.Worksheets["List7"];
                    foreach (Deca_Code_Master master in dataSourceType)
                    {
                        dataSheet8.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet9 = excelPkg.Workbook.Worksheets["List8"];
                    foreach (Deca_Code_Master master in dataCustomerGroup)
                    {
                        dataSheet9.Cells[rowData++, 1].Value = master.Description;
                    }
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        public ActionResult ImportFromExcel()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["fileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["fileUpload"].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["fileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["fileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["fileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        int n = 1;

                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Management_Profile.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["PotentialCustomer"];

                        var data = WorksheetToDataTables(oSheet);

                        DateTime temp = DateTime.Parse("1900-01-01");

                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                        {
                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                            RequestCreditBank = s["Yêu cầu ngân hàng cấp thẻ"] != null ? s["Yêu cầu ngân hàng cấp thẻ"].ToString() : null,
                            DecaNote = s["Ghi chú"] != null ? s["Ghi chú"].ToString() : null,
                            IsForm = s["Hồ sơ"] != null ? bool.Parse(s["Hồ sơ"].ToString()) : false,
                            CreditCardStatus = s["Trạng thái mở thẻ"] != null ? s["Trạng thái mở thẻ"].ToString() : null,
                            SourceType = s["Nguồn"] != null ? s["Nguồn"].ToString() : null,
                            Fullname = s["Họ và tên"] != null ? s["Họ và tên"].ToString() : null,
                            Sex = s["Giới tính"] != null ? s["Giới tính"].ToString() : null,
                            Birthday = s["Ngày sinh"] == null ? DateTime.Parse("1900-01-01") : DateTime.TryParse(s["Ngày sinh"].ToString(), out temp) ? temp : DateTime.Parse("1900-01-01"),
                            HomeTown = s["Tỉnh"] != null ? s["Tỉnh"].ToString() : null,
                            Department = s["Phòng ban"] != null ? s["Phòng ban"].ToString() : null,
                            Position = s["Chức vụ"] != null ? s["Chức vụ"].ToString() : null,
                            CustomerGroup = s["Nhóm khách hàng"] != null ? s["Nhóm khách hàng"].ToString() : null,
                            StartWorkingDate = s["Ngày vào làm"] == null ? DateTime.Parse("1900-01-01") : DateTime.TryParse(s["Ngày vào làm"].ToString(), out temp) ? temp : DateTime.Parse("1900-01-01"),
                            Education = s["Trình độ"] != null ? s["Trình độ"].ToString() : null,
                            Phone = s["Điện thoại"] != null ? s["Điện thoại"].ToString() : null,
                            Email = s["Email"] != null ? s["Email"].ToString() : null,
                            Income = s["Thu nhập/tháng"] != null && !string.IsNullOrEmpty(s["Thu nhập/tháng"].ToString()) ? double.Parse(s["Thu nhập/tháng"].ToString()) : 0,
                            PayrollBank = s["Nhận lương qua NH"] != null ? s["Nhận lương qua NH"].ToString() : null,
                            Address = s["Địa chỉ"] != null ? s["Địa chỉ"].ToString() : null,
                        }).ToList();

                        var PhysicalID = String.Join(", ", listPotential.Select(s => "'" + s.PhysicalID + "'"));

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {

                            //check required field
                            if (n == 1)
                            {
                                var required = listPotential.Where(s => String.IsNullOrEmpty(s.PhysicalID) || String.IsNullOrEmpty(s.RequestCreditBank));
                                foreach (var item in required)
                                {

                                    writeErrorToFile(eSheet, rownumber, item, "Vui lòng nhập đầy đủ thông tin bắt buộc");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(required).ToList();
                                if (listPotential.Count() > 0)
                                {
                                    n = 1;
                                }
                                else
                                {
                                    n = 0;
                                }
                            }

                            if (n == 1)
                            {
                                //check exist physicalid
                                var donePhys = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Potential_Customer");

                                var existP = listPotential.Where(s => !donePhys.Contains(s.PhysicalID));
                                foreach (var item in existP)
                                {

                                    writeErrorToFile(eSheet, rownumber, item, "PhysicalID - Không tồn tại trong hệ thống");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(existP).ToList();
                                if (listPotential.Count() > 0)
                                {
                                    n = 1;
                                }
                                else
                                {
                                    n = 0;
                                }
                            }

                            if (n == 1)
                            {
                                //check exist physicalid & have card
                                var donePhys = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Potential_Customer WHERE HaveCard = 1 ");

                                var existP = listPotential.Where(s => donePhys.Contains(s.PhysicalID));
                                foreach (var item in existP)
                                {

                                    writeErrorToFile(eSheet, rownumber, item, "PhysicalID - Đã được cấp thẻ.");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(existP).ToList();
                                if (listPotential.Count() > 0)
                                {
                                    n = 1;
                                }
                                else
                                {
                                    n = 0;
                                }
                            }

                            if (n == 1)
                            {
                                //check exist physicalid & have card
                                var donePhys = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Potential_Customer WHERE CreditCardStatus IN ('CCS002','CCS003','CCS005') AND HaveCard = 0");

                                var existP = listPotential.Where(s => !donePhys.Contains(s.PhysicalID));
                                foreach (var item in existP)
                                {

                                    writeErrorToFile(eSheet, rownumber, item, "Trạng thái mở thẻ không hợp lệ.");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(existP).ToList();
                                if (listPotential.Count() > 0)
                                {
                                    n = 1;
                                }
                                else
                                {
                                    n = 0;
                                }
                            }
                            if (n == 1)
                            {
                                //check exist BAnk
                                var company = dbConn.GetFirstColumn<string>("SELECT BankName FROM DC_Bank_Definition");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.RequestCreditBank));

                                foreach (var item in wrongCompany)
                                {

                                    writeErrorToFile(eSheet, rownumber, item, "Ngân hàng không tồn tại trong hệ thống");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(wrongCompany).ToList();
                                if (listPotential.Count() > 0)
                                {
                                    n = 1;
                                }
                                else
                                {
                                    n = 0;
                                }
                            }

                            var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                            var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                            var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                            var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                            var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                            var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                            var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");


                            // begin save data
                            try
                            {
                                if (listPotential.Count > 0)
                                {
                                    foreach (var item in listPotential)
                                    {
                                        total += dbConn.ExecuteSql(@"UPDATE Deca_Potential_Customer SET isForm = 1,Active =1,CreditCardStatus = 'CCS004',DecaNote = '" + item.DecaNote + "' ,SubmitedAt =  '" + DateTime.Now + "',WaitingProfile = '" + DateTime.Now + "',UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID = '" + item.PhysicalID + "'");
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                writeErrorToFile(eSheet, rownumber, listPotential.FirstOrDefault(), ex.Message);
                                rownumber++;

                            }

                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = "errors:" + e.Message });
            }

            return Json(new { success = true });
        }
        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, Deca_Potential_Customer item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                var dataCompany = dbConn.Select<Deca_Company>();
                var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");
                var dataBranch = dbConn.Select<DC_Company_Branch>();
                int i = 1;
                eSheet.Cells[rownumber, i++].Value = item.PhysicalID;
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank).BankName;
                eSheet.Cells[rownumber, i++].Value = item.IsForm;
                eSheet.Cells[rownumber, i++].Value = dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus).Description;
                eSheet.Cells[rownumber, i++].Value = dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType).Description;
                eSheet.Cells[rownumber, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                eSheet.Cells[rownumber, i++].Value = item.CompanyName;
                eSheet.Cells[rownumber, i++].Value = dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID) == null ? "" : dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID).BranchName;
                eSheet.Cells[rownumber, i++].Value = item.EmployeeID;
                eSheet.Cells[rownumber, i++].Value = item.Fullname;
                eSheet.Cells[rownumber, i++].Value = dataSex.FirstOrDefault(a => a.CodeID == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.CodeID == item.Sex).Description;
                eSheet.Cells[rownumber, i++].Value = item.Birthday;
                eSheet.Cells[rownumber, i++].Value = item.Phone;
                eSheet.Cells[rownumber, i++].Value = item.Email;
                eSheet.Cells[rownumber, i++].Value = item.Address;
                eSheet.Cells[rownumber, i++].Value = dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown).TerritoryName;
                eSheet.Cells[rownumber, i++].Value = item.Department;
                eSheet.Cells[rownumber, i++].Value = item.Position;
                eSheet.Cells[rownumber, i++].Value = dataCustomerGroup.FirstOrDefault(a => a.CodeID == item.CustomerGroup) == null ? "" : dataCustomerGroup.FirstOrDefault(a => a.CodeID == item.CustomerGroup).Description;
                eSheet.Cells[rownumber, i++].Value = item.StartWorkingDate;
                eSheet.Cells[rownumber, i++].Value = dataEducation.FirstOrDefault(a => a.CodeID == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Education).Description;
                eSheet.Cells[rownumber, i++].Value = item.Income;
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank).BankName;
                eSheet.Cells[rownumber, i++].Value = item.CreditLimit;
                eSheet.Cells[rownumber, i++].Value = item.Active;
                eSheet.Cells[rownumber, i++].Value = item.CreatedAt;
                eSheet.Cells[rownumber, i++].Value = item.CreatedBy;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedAt;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedBy;
                eSheet.Cells[rownumber, i++].Value = error;
            }
        }

        private DataTable WorksheetToDataTables(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = 24;
            DataTable dt = new DataTable(oSheet.Name);
            DataRow dr = null;
            for (int i = 1; i <= totalRows; i++)
            {
                if (i > 1) dr = dt.Rows.Add();
                for (int j = 1; j <= totalCols; j++)
                {
                    if (i == 1)
                        dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
                    else
                        dr[j - 1] = oSheet.Cells[i, j].Value;
                }
            }
            return dt;
        }
        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Excel"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
    }
}