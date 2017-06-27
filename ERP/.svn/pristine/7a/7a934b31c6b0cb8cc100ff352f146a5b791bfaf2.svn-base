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
    public class BankPotentialCustomerController : CustomController
    {
        //
        // GET: /BankPotentialCustomer/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Potential_Customer_Log), typeof(Deca_Potential_Customer));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Potential_Customer_Log), typeof(Deca_Potential_Customer));
            //}
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
                    ViewBag.RejectReason = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "BankDeniedReason");
                    ViewBag.CreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0} AND CodeID IN ('CCS004','CCS005','CCS006','CCS007')", "CreditCardStatus");
                    //ViewBag.CreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
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
                    return Json(KendoApplyFilter.KendoData<Deca_Potential_Customer>(request, "[CreditCardStatus] IN ('CCS004','CCS005','CCS006','CCS007')"));
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult GetListCreditStatus()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Deca_Code_Master>("[CodeType] ={0} AND CodeID IN ('CCS005','CCS006','CCS007')", "CreditCardStatus");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetListRejectReason()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "BankDeniedReason");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChangeStatus(string listData, string Status, string Reason, string CICEndDate, string CreditLimit, string PhoneTrans, string currentAddress, string Description)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(Status))
                    {
                        try
                        {
                            int s = 0, f = 0;
                            string[] separators = { "@@" };
                            var listid = listData.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                            var listPotential = dbConn.Select<Deca_Potential_Customer>("PhysicalID IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS004') AND HaveCard = 0");

                            foreach (var potential in listPotential)
                            {
                                if (Status == "CCS005") // Ngân hàng từ chối
                                {
                                    if (String.IsNullOrEmpty(Description) || String.IsNullOrEmpty(Reason))
                                    {
                                        return Json(new { error = true, message = "Vui lòng chọn & nhập lý do từ chối" });
                                    }

                                    if (Reason == "BDR004") // Nợ xấu trên CIC
                                    {
                                        if (CICEndDate == "")
                                        {
                                            return Json(new { error = true, message = "Vui lòng nhập Ngày hết nợ trên CIC" });
                                        }

                                        DateTime expectedDate;
                                        if (!DateTime.TryParse(CICEndDate, out expectedDate))
                                        {
                                            return Json(new { error = true, message = " Ngày hết nợ không hợp lệ." });
                                        }
                                        potential.CICBadDebt = DateTime.Parse(CICEndDate);

                                    }
                                    potential.RejectReason = Reason;

                                }

                                if (Status == "CCS006" || Status == "CCS007") // Phát thẻ || Hoàn tất
                                {
                                    if (String.IsNullOrEmpty(CreditLimit) || String.IsNullOrEmpty(PhoneTrans) || String.IsNullOrEmpty(currentAddress))
                                    {
                                        return Json(new { error = true, message = "Vui lòng nhập những thông tin bắt buộc" });
                                    }

                                    potential.CreditLimit = double.Parse(CreditLimit);
                                    potential.PhoneTransaction = PhoneTrans;
                                    potential.CurrentAddress = currentAddress;
                                }

                                //potential.BankNote = Description;
                                potential.CreditCardStatus = Status;
                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);
                                s++;
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
                        return Json(new { error = true, message = "Vui lòng chọn trạng thái" });
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Potential_Customer> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            item.IsNew = true;
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = User.Identity.Name;
                            var check = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID={0} AND Id<>{1}", item.PhysicalIDBank, item.Id);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng số chứng minh nhân dân!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            check = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalIDBank={0} AND Id<>{1}", item.PhysicalIDBank, item.Id);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng số chứng minh nhân dân!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            //check = dbConn.FirstOrDefault<Deca_Potential_Customer>("EmployeeID={0} AND CompanyID={1} AND Id<>{2}", item.EmployeeID, item.CompanyID, item.Id);
                            //if (check != null)
                            //{
                            //    ModelState.AddModelError("", "Trùng mã nhân viên!");
                            //    return Json(items.ToDataSourceResult(request, ModelState));
                            //}
                            dbConn.Update(item);

                            //update customer Index
                            var customerIndex = dbConn.SingleOrDefault<Deca_Customer_Index>("PhysicalID={0} AND DataSource = 'potentialCustomer'", item.PhysicalID);
                            if (customerIndex != null)
                            {
                                customerIndex.CustomerID = "";
                                customerIndex.Fullname = item.Fullname;
                                customerIndex.Phone = item.Phone;
                                customerIndex.Email = item.Email;
                                customerIndex.PhysicalID = item.PhysicalID;
                                customerIndex.Meta = (!String.IsNullOrEmpty(item.Fullname) ? Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(item.PhysicalID.ToLower());
                                customerIndex.DataSource = "potentialCustomer";
                                customerIndex.UpdatedAt = DateTime.Now;
                                customerIndex.UpdatedBy = currentUser.UserName;
                                dbConn.Update(customerIndex);
                            }
                            else
                            {
                                customerIndex = new Deca_Customer_Index();
                                customerIndex.CustomerID = "";
                                customerIndex.Fullname = item.Fullname;
                                customerIndex.Phone = item.Phone;
                                customerIndex.Email = item.Email;
                                customerIndex.PhysicalID = item.PhysicalID;
                                customerIndex.Meta = (!String.IsNullOrEmpty(item.Fullname) ? Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(item.PhysicalID.ToLower());
                                customerIndex.DataSource = "potentialCustomer";
                                customerIndex.CreatedAt = DateTime.Now;
                                customerIndex.CreatedBy = currentUser.UserName;
                                dbConn.Insert(customerIndex);
                            }


                            var log = new Deca_Potential_Customer_Log();
                            log.CustomerID = item.CustomerID;
                            log.Log = item;
                            log.CreatedAt = DateTime.Now;
                            log.CreatedBy = currentUser.UserName;
                            dbConn.Insert(log);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }
        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_BankPotential_Profile.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Bank_Potential_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    request.PageSize = 50000;
                    var data = KendoApplyFilter.KendoData<Deca_Potential_Customer>(request, "CreditCardStatus IN('CCS004','CCS005','CCS006','CCS007')").Data;
                    var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                    var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                    var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                    var dataCompany = dbConn.Select<Deca_Company>();
                    var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");
                    var dataRejectReason = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "BankDeniedReason");

                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];

                    int rowData = 1;

                    foreach (Deca_Potential_Customer item in data)
                    {
                        int i = 1;
                        rowData++;
                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalIDBank;
                        expenseSheet.Cells[rowData, i++].Value = item.CreditLimit;
                        expenseSheet.Cells[rowData, i++].Value = item.PhoneTransaction;
                        expenseSheet.Cells[rowData, i++].Value = item.CurrentAddress;
                        expenseSheet.Cells[rowData, i++].Value = dataRejectReason.FirstOrDefault(a => a.CodeID == item.RejectReason) == null ? "" : dataRejectReason.FirstOrDefault(a => a.CodeID == item.RejectReason).Description;
                        if (item.CICBadDebt == null || item.CICBadDebt == DateTime.Parse("1900-01-01 00:00:00.000"))
                        {
                            expenseSheet.Cells[rowData, i++].Value = "";
                        }
                        else
                        {
                            expenseSheet.Cells[rowData, i++].Value = item.CICBadDebt.ToString();
                        }

                        expenseSheet.Cells[rowData, i++].Value = item.BankNote;
                        expenseSheet.Cells[rowData, i++].Value = "";
                        expenseSheet.Cells[rowData, i++].Value = dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                        expenseSheet.Cells[rowData, i++].Value = item.CompanyName;
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
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerGroup;
                        expenseSheet.Cells[rowData, i++].Value = item.StartWorkingDate;
                        expenseSheet.Cells[rowData, i++].Value = dataEducation.FirstOrDefault(a => a.CodeID == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Education).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Income;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType).Description;
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
                    ExcelWorksheet dataSheet10 = excelPkg.Workbook.Worksheets["List9"];
                    foreach (Deca_Code_Master master in dataRejectReason)
                    {
                        dataSheet10.Cells[rowData++, 1].Value = master.Description;
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
        public ActionResult ExportTemplate()
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_BankPotential_Profile.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Potential_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                    var dataRejectReason = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "BankDeniedReason");

                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];

                    int rowData = 2;
                    ExcelWorksheet dataSheet10 = excelPkg.Workbook.Worksheets["List9"];
                    foreach (Deca_Code_Master master in dataRejectReason)
                    {
                        dataSheet10.Cells[rowData++, 1].Value = master.Description;
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
        private DataTable WorksheetToDataTables(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = 23;
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
                var dataRejectReason = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "BankDeniedReason");
                int i = 1;



                eSheet.Cells[rownumber, i++].Value = item.PhysicalID;
                eSheet.Cells[rownumber, i++].Value = item.PhysicalIDBank;
                eSheet.Cells[rownumber, i++].Value = item.CreditLimit;
                eSheet.Cells[rownumber, i++].Value = item.PhoneTransaction;
                eSheet.Cells[rownumber, i++].Value = item.CurrentAddress;
                eSheet.Cells[rownumber, i++].Value = dataRejectReason.FirstOrDefault(a => a.CodeID == item.RejectReason) == null ? "" : dataRejectReason.FirstOrDefault(a => a.CodeID == item.RejectReason).Description;
                eSheet.Cells[rownumber, i++].Value = item.CICBadDebt.ToString();
                eSheet.Cells[rownumber, i++].Value = item.BankNote;
                eSheet.Cells[rownumber, i++].Value = item.ResultCheck;
                eSheet.Cells[rownumber, i++].Value = dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus).Description;
                eSheet.Cells[rownumber, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                eSheet.Cells[rownumber, i++].Value = item.CompanyName;
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
                eSheet.Cells[rownumber, i++].Value = item.CustomerGroup;
                eSheet.Cells[rownumber, i++].Value = item.StartWorkingDate;
                eSheet.Cells[rownumber, i++].Value = dataEducation.FirstOrDefault(a => a.CodeID == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Education).Description;
                eSheet.Cells[rownumber, i++].Value = item.Income;
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank).BankName;
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank).BankName;
                eSheet.Cells[rownumber, i++].Value = dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType).Description;
                eSheet.Cells[rownumber, i++].Value = item.DecaRequested;
                eSheet.Cells[rownumber, i++].Value = item.SubmitedAt;
                eSheet.Cells[rownumber, i++].Value = item.WaitingProfile;
                eSheet.Cells[rownumber, i++].Value = item.BankDenied;
                eSheet.Cells[rownumber, i++].Value = item.IssuedCard;
                eSheet.Cells[rownumber, i++].Value = item.Done;
                eSheet.Cells[rownumber, i++].Value = item.Cancelled;
                eSheet.Cells[rownumber, i++].Value = item.Active;
                eSheet.Cells[rownumber, i++].Value = item.CreatedAt;
                eSheet.Cells[rownumber, i++].Value = item.CreatedBy;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedAt;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedBy;
                eSheet.Cells[rownumber, i++].Value = error;

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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_BankPotential_Profile.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["PotentialCustomer"];

                        var data = WorksheetToDataTables(oSheet);

                        DateTime temp = DateTime.Parse("1900-01-01");
                        // get list data from Excel
                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                        {
                            PhysicalID = s["CMND (Deca)"] != null ? s["CMND (Deca)"].ToString() : null,
                            PhysicalIDBank = s["CMND (NH)"] != null ? s["CMND (NH)"].ToString() : null,
                            CreditLimit = s["Tín dụng"] != null && !string.IsNullOrEmpty(s["Tín dụng"].ToString()) ? double.Parse(s["Tín dụng"].ToString()) : 0,
                            CurrentAddress = s["Địa chỉ hiện tại"] != null ? s["Địa chỉ hiện tại"].ToString() : null,
                            PhoneTransaction = s["Số điện thoại giao dịch"] != null ? s["Số điện thoại giao dịch"].ToString() : null,
                            RejectReason = s["Lý do từ chối"] != null ? s["Lý do từ chối"].ToString() : null,
                            CICBadDebt = s["Ngày hết nợ CIC"] == null ? DateTime.Parse("1900-01-01") : DateTime.TryParse(s["Ngày hết nợ CIC"].ToString(), out temp) ? DateTime.Parse(s["Ngày hết nợ CIC"].ToString()) : DateTime.Parse("1900-01-01"),
                            BankNote = s["Ngân hàng ghi chú"] != null ? s["Ngân hàng ghi chú"].ToString() : null,
                            ResultCheck = s["Kết quả kiểm định"] != null ? s["Kết quả kiểm định"].ToString() : "",
                            CreditCardStatus = s["Trạng thái mở thẻ"] != null ? s["Trạng thái mở thẻ"].ToString() : null,
                            EmployeeID = s["Mã nhân viên"] != null ? s["Mã nhân viên"].ToString() : null,
                            Fullname = s["Họ và tên"] != null ? s["Họ và tên"].ToString() : null,
                            Sex = s["Giới tính"] != null ? s["Giới tính"].ToString() : null,
                            Birthday = s["Ngày sinh"] == null ? DateTime.Parse("1900-01-01") : DateTime.TryParse(s["Ngày sinh"].ToString(), out temp) ? DateTime.Parse(s["Ngày sinh"].ToString()) : DateTime.Parse("1900-01-01"),
                            HomeTown = s["Tỉnh"] != null ? s["Tỉnh"].ToString() : null,
                            Department = s["Phòng ban"] != null ? s["Phòng ban"].ToString() : null,
                            Position = s["Chức vụ"] != null ? s["Chức vụ"].ToString() : null,
                            CustomerGroup = s["Nhóm khách hàng"] != null ? s["Nhóm khách hàng"].ToString() : null,
                            Phone = s["Điện thoại"] != null ? s["Điện thoại"].ToString() : null,
                            Email = s["Email"] != null ? s["Email"].ToString() : null,
                            //StartWorkingDate = s["Ngày vào làm"] == null ? DateTime.Parse("1900-01-01") : DateTime.TryParse(s["Ngày vào làm"].ToString(), out temp) ? DateTime.Parse(s["Ngày vào làm"].ToString()) : DateTime.Parse("1900-01-01"),
                            //Education = s["Trình độ"] != null ? s["Trình độ"].ToString() : null,
                            //Income = s["Thu nhập/tháng"] != null && !string.IsNullOrEmpty(s["Thu nhập/tháng"].ToString()) ? double.Parse(s["Thu nhập/tháng"].ToString()) : 0,
                            //PayrollBank = s["Nhận lương qua NH"] != null ? s["Nhận lương qua NH"].ToString() : null,
                            //Address = s["Địa chỉ"] != null ? s["Địa chỉ"].ToString() : null,
                            //SourceType = s["Nguồn"] != null ? s["Nguồn"].ToString() : null,
                            //RequestCreditBank = s["Yêu cầu ngân hàng cấp thẻ"] != null ? s["Yêu cầu ngân hàng cấp thẻ"].ToString() : null,
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            var dataRejectReason = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "BankDeniedReason");

                            //check required field
                            if (n == 1)
                            {
                                var required = listPotential.Where(s => String.IsNullOrEmpty(s.PhysicalID));
                                foreach (var item in required)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Vui lòng nhập CMND (portal)");
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
                                //check exist physicalID  
                                var physicalID = dbConn.GetFirstColumn<string>("SELECT [PhysicalID] FROM Deca_Potential_Customer");

                                var wrongCompany = listPotential.Where(s => !physicalID.Contains(s.PhysicalID));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "CMND không tồn tại trong hệ thống");
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

                            //if (n == 1)
                            //{
                            //    // check PhysicalIDBank in Potential : 
                            //    var physicalP = dbConn.GetFirstColumn<string>("SELECT ISNULL(PhysicalIDBank,'') FROM Deca_Potential_Customer");
                            //    var exist = listPotential.Where(s => physicalP.Contains(s.PhysicalIDBank));
                            //    foreach (var item in exist)
                            //    {
                            //        writeErrorToFile(eSheet, rownumber, item, "CMND Ngân hàng  đã tồn tại trong Potential");
                            //        rownumber++;
                            //    }
                            //    listPotential = listPotential.Except(exist).ToList();
                            //    if (listPotential.Count() > 0)
                            //    {
                            //        n = 1;
                            //    }
                            //    else
                            //    {
                            //        n = 0;
                            //    }
                            //}

                            // --  get list data have action : Issued from Excel

                            var existIssued = listPotential.Where(s => s.ResultCheck.Equals("Phát thẻ"));
                            if (existIssued.ToList().Count > 0)
                            {
                                if (n == 1)
                                {
                                    //check status : Chỉ được Phát hành thẻ khi đang ở trạng thái -> Đã gửi ngân hàng
                                    var phyID = String.Join(",", existIssued.Select(s => "'" + s.PhysicalID.Trim() + "'"));
                                    var listdata = dbConn.Select<Deca_Potential_Customer>("SELECT * FROM Deca_Potential_Customer WHERE PhysicalID IN(" + phyID + ")");
                                    var exist = listdata.Where(s => s.CreditCardStatus != "CCS004");
                                    foreach (var item in exist)
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "Trạng thái mở thẻ không hợp lệ !");
                                        rownumber++;
                                    }
                                    existIssued = existIssued.Except(exist).ToList();
                                    if (existIssued.Count() > 0)
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
                                    //check required input data : 
                                    var exist = existIssued.Where(s => s.PhysicalIDBank == "" || s.CreditLimit == 0 || s.PhoneTransaction == "" || s.CurrentAddress == "");
                                    foreach (var item in exist)
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "Vui lòng nhập đầy đủ các thông tin : CMND Ngân Hàng, Tín dụng, Sđt giao dịch,Địa chỉ hiện tại !");
                                        rownumber++;
                                    }
                                    existIssued = existIssued.Except(exist).ToList();
                                    if (existIssued.Count() > 0)
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
                                    // check PhysicalID in Customer : 
                                    var physicalCus = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Customer");
                                    var exist = existIssued.Where(s => physicalCus.Contains(s.PhysicalIDBank));
                                    foreach (var item in exist)
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "CMND Ngân Hàng  đã tồn tại trong Customer");
                                        rownumber++;
                                    }
                                    existIssued = existIssued.Except(exist).ToList();
                                    if (existIssued.Count() > 0)
                                    {
                                        n = 1;
                                    }
                                    else
                                    {
                                        n = 0;
                                    }
                                }
                                foreach (var item in existIssued)
                                {

                                    var insertCus = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID ={0}", item.PhysicalID);
                                    if (insertCus != null)
                                    {
                                        // insert into Customer
                                        Deca_Customer customer = new Deca_Customer();
                                        customer.CompanyID = insertCus.CompanyID;
                                        customer.CompanyName = dbConn.FirstOrDefault<Deca_Company>("Id = {0}", insertCus.CompanyID) != null ? dbConn.FirstOrDefault<Deca_Company>("Id = {0}", insertCus.CompanyID).ShortName : "";
                                        customer.EmployeeID = insertCus.EmployeeID;
                                        customer.Fullname = insertCus.Fullname;
                                        customer.Sex = insertCus.Sex;
                                        customer.Birthday = insertCus.Birthday;
                                        customer.MaritalStatus = false;
                                        customer.PhysicalID = item.PhysicalIDBank; // Lấy CMND do Ngân hàng confirm
                                        customer.MobilePhone = insertCus.PhoneTransaction;
                                        customer.Phone = insertCus.Phone;
                                        customer.Email = insertCus.Email;
                                        customer.HomeTown = insertCus.HomeTown;
                                        customer.Address = insertCus.CurrentAddress;
                                        customer.Department = insertCus.Department;
                                        customer.Position = insertCus.Position;
                                        customer.StartWorkingDate = insertCus.StartWorkingDate;
                                        customer.Education = insertCus.Education;
                                        customer.Income = insertCus.Income;
                                        customer.PayrollBank = insertCus.PayrollBank;
                                        customer.CreditLimit = item.CreditLimit;
                                        customer.CreditBank = insertCus.RequestCreditBank;
                                        customer.CardType = ""; // check lai
                                        customer.CreatedAt = DateTime.Now;
                                        customer.CreatedBy = currentUser.UserName;
                                        dbConn.Insert(customer);

                                        int Id = (int)dbConn.GetLastInsertId();
                                        customer.Id = Id;
                                        string datetime = DateTime.Now.ToString("yyMMdd");
                                        customer.CustomerID = "CU" + datetime + Id;
                                        dbConn.Update(customer);

                                        var rejectReason = dataRejectReason.FirstOrDefault(a => a.Description == item.RejectReason) == null ? "" : dataRejectReason.FirstOrDefault(a => a.Description == item.RejectReason).CodeID;
                                        // update in Potential
                                        total += dbConn.Update(@"Deca_Potential_Customer" // table name
                                                                                    , "IsNew =1 ,Active = 1,CustomerID = '" + customer.CustomerID + "',CreditCardStatus = 'CCS006',PhysicalIDBank = '" + item.PhysicalIDBank + "',CreditLimit = '" + item.CreditLimit + "', PhoneTransaction = '" + item.PhoneTransaction + "',CurrentAddress = '" + item.CurrentAddress + "',RejectReason = '" + rejectReason + "',CICBadDebt = '" + item.CICBadDebt + "',BankNote = '" + item.BankNote + "', IssuedCard = '" + DateTime.Now + "',UpdatedAt = '" + DateTime.Now + "' , UpdatedBy = '" + currentUser.UserName + "'" // cols update
                                                                                    , "PhysicalID='" + item.PhysicalID + "'"); // where conditions

                                        //re-update order header
                                        var orderHeader = dbConn.Select<Deca_Order_Header>("PhysicalID={0} AND EmployeeID={1} AND PaymentStatus={2}", item.PhysicalID, item.EmployeeID, 0);
                                        if (orderHeader.Count() > 0)
                                        {
                                            //update physicalId
                                            //string PhysicalID = dbConn.Scalar<string>("SELECT TOP 1 ")
                                            foreach (var order in orderHeader)
                                            {
                                                order.CustomerID = customer.CustomerID;
                                                order.MobilePhone = customer.MobilePhone;
                                                order.UpdatedAt = DateTime.Now;
                                                order.UpdatedBy = currentUser.UserName;
                                                dbConn.Update(order);
                                            }
                                        }


                                        // Update Customer Index
                                        string meta = (!String.IsNullOrEmpty(insertCus.Fullname) ? Helpers.convertToUnSign3.Init(insertCus.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(insertCus.Email) ? Helpers.convertToUnSign3.Init(insertCus.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(insertCus.Phone) ? Helpers.convertToUnSign3.Init(insertCus.Phone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(insertCus.PhysicalID.ToLower());
                                        string strCusIdex = @"UPDATE Deca_Customer_Index SET  Fullname = '" + insertCus.Fullname + "', Phone = '" + insertCus.Phone + "',Email = '" + insertCus.Email + "', Meta = '" + meta + "',DataSource = 'potentialCustomer',";
                                        strCusIdex += "UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + insertCus.PhysicalID + "')";
                                        dbConn.ExecuteSql(strCusIdex);
                                    }
                                }
                            } //  End list Issued.

                            //  get list data have action : Denied from Excel
                            var existDenied = listPotential.Where(s => s.ResultCheck.Equals("Từ chối"));
                            if (existDenied.ToList().Count > 0)
                            {
                                // Check Validate
                                if (n == 1)
                                {
                                    //check status : Chỉ được Từ chối khi trạng thái -> Đã gửi ngân hàng
                                    var phyID = String.Join(",", existDenied.Select(s => "'" + s.PhysicalID.Trim() + "'"));
                                    var listdata = dbConn.Select<Deca_Potential_Customer>("SELECT * FROM Deca_Potential_Customer WHERE PhysicalID IN(" + phyID + ")");
                                    var exist = listdata.Where(s => s.CreditCardStatus != "CCS004");

                                    foreach (var item in exist)
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "Trạng thái mở thẻ không hợp lệ !");
                                        rownumber++;
                                    }
                                    existDenied = existDenied.Except(exist).ToList();
                                    if (existDenied.Count() > 0)
                                    {
                                        n = 1;
                                    }
                                    else
                                    {
                                        n = 0;
                                    }
                                }

                                // Action 
                                foreach (var item in existDenied)
                                {
                                    if (string.IsNullOrEmpty(item.RejectReason))
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "Vui lòng chọn lý do từ chối !");
                                        rownumber++;
                                    }
                                    //else if (item.RejectReason == "BDR004")
                                    //{
                                    //    //if (item.CICBadDebt.ToString() == "" || item.CICBadDebt == DateTime.Parse("1900-01-01"))
                                    //    //{
                                    //    //    writeErrorToFile(eSheet, rownumber, item, "Vui lòng nhập ngày hết nợ trên CIC !");
                                    //    //    rownumber++;
                                    //    //}
                                    //}
                                    else
                                    {
                                        var rejectReason = dataRejectReason.FirstOrDefault(a => a.Description == item.RejectReason) == null ? "" : dataRejectReason.FirstOrDefault(a => a.Description == item.RejectReason).CodeID;
                                        total += dbConn.Update(@"Deca_Potential_Customer" // table
                                                                                 , "IsNew =1 ,Active = 1,CreditCardStatus = 'CCS005',PhysicalIDBank = '" + item.PhysicalIDBank + "',CreditLimit = '" + item.CreditLimit + "', PhoneTransaction = '" + item.PhoneTransaction + "',CurrentAddress = '" + item.CurrentAddress + "',RejectReason = '" + rejectReason + "',CICBadDebt = '" + item.CICBadDebt + "',BankNote = '" + item.BankNote + "', BankDenied = '" + DateTime.Now + "',UpdatedAt = '" + DateTime.Now + "' , UpdatedBy = '" + currentUser.UserName + "'" // cols update
                                                                                 , "PhysicalID='" + item.PhysicalID + "'"); // where conditions
                                    }
                                }
                            }

                            // get list data have action : Done from Excel
                            var existDone = listPotential.Where(s => s.ResultCheck.Equals("Hoàn tất"));
                            if (existDone.ToList().Count > 0)
                            {
                                if (n == 1)
                                {
                                    //check status : Chỉ được Hoàn tất khi trạng thái -> Phát hành thẻ
                                    var phyID = String.Join(",", existDone.Select(s => "'" + s.PhysicalID.Trim() + "'"));
                                    var listdata = dbConn.Select<Deca_Potential_Customer>("SELECT * FROM Deca_Potential_Customer WHERE PhysicalID IN(" + phyID + ")");
                                    var exist = listdata.Where(s => s.CreditCardStatus != "CCS006");

                                    foreach (var item in exist)
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "Trạng thái mở thẻ không hợp lệ !");
                                        rownumber++;
                                    }
                                    existDone = existDone.Except(exist).ToList();
                                    if (existDone.Count() > 0)
                                    {
                                        n = 1;
                                    }
                                    else
                                    {
                                        n = 0;
                                    }
                                }

                                foreach (var item in existDone)
                                {
                                    var rejectReason = dataRejectReason.FirstOrDefault(a => a.Description == item.RejectReason) == null ? "" : dataRejectReason.FirstOrDefault(a => a.Description == item.RejectReason).CodeID;
                                    total += dbConn.Update(@"Deca_Potential_Customer" // table
                                                                             , "IsNew =1 ,Active = 1,CreditCardStatus = 'CCS007',BankNote = '" + item.BankNote + "', Done = '" + DateTime.Now + "',UpdatedAt = '" + DateTime.Now + "' , UpdatedBy = '" + currentUser.UserName + "'" // cols update
                                                                             , "PhysicalID='" + item.PhysicalID + "'"); // where conditions

                                }

                            }

                            // update potentail
                            var existUpdate = listPotential.Where(s => s.ResultCheck.Equals(""));
                            if (existUpdate.ToList().Count > 0)
                            {

                                if (n == 1)
                                {
                                    // check PhysicalIDBank in Potential : 
                                    var physicalP = dbConn.GetFirstColumn<string>("SELECT ISNULL(PhysicalIDBank,'') FROM Deca_Potential_Customer");
                                    var exist = listPotential.Where(s => physicalP.Contains(s.PhysicalIDBank));
                                    foreach (var item in exist)
                                    {
                                        writeErrorToFile(eSheet, rownumber, item, "CMND Ngân hàng  đã tồn tại trong Potential");
                                        rownumber++;
                                    }
                                    listPotential = listPotential.Except(exist).ToList();
                                    if (listPotential.Count() > 0)
                                    {
                                        n = 1;
                                    }
                                    else
                                    {
                                        n = 0;
                                    }
                                }

                                //begin save data
                                foreach (var i in existUpdate)
                                {
                                    try
                                    {
                                        //if (n == 1)
                                        //{
                                        //    //check status : Chỉ được Update khi trạng thái -> Đã gửi ngân hàng
                                        //    var exist = existUpdate.Where(s => s.CreditCardStatus != "Đã gửi ngân hàng");
                                        //    foreach (var item in exist)
                                        //    {
                                        //        writeErrorToFile(eSheet, rownumber, item, "Chỉ được cập nhật dữ liệu khi Đang ở trạng thái: Đã gửi ngân hàng!");
                                        //        rownumber++;
                                        //    }
                                        //    existDone = existDone.Except(exist).ToList();
                                        //    if (existDone.Count() > 0)
                                        //    {
                                        //        n = 1;
                                        //    }
                                        //    else
                                        //    {
                                        //        n = 0;
                                        //    }
                                        //}
                                        var rejectReason = dataRejectReason.FirstOrDefault(a => a.Description == i.RejectReason) == null ? "" : dataRejectReason.FirstOrDefault(a => a.Description == i.RejectReason).CodeID;
                                        total += dbConn.Update(@"Deca_Potential_Customer"
                                                        , "IsNew =1 ,PhysicalIDBank = '" + i.PhysicalIDBank + "',CreditLimit = '" + i.CreditLimit + "', PhoneTransaction = '" + i.PhoneTransaction + "',CurrentAddress = '" + i.CurrentAddress + "',RejectReason = '" + rejectReason + "',CICBadDebt = '" + i.CICBadDebt + "',BankNote = '" + i.BankNote + "', UpdatedAt = '" + DateTime.Now + "' , UpdatedBy = '" + currentUser.UserName + "'"
                                                        , "PhysicalID='" + i.PhysicalID + "'"
                                                     );
                                        //total++;
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, i, ex.Message);
                                        rownumber++;
                                    }
                                }
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

        [HttpPost]
        public ActionResult IssuedCard(string listData, string Description)
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
                        var listid = listData.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();

                        // Chỉ được mở thẻ khi đang ở trạng thái CCS004: Đã gửi ngân hàng & Khách hàng chưa có thẻ.
                        var listPotentialError = new List<Deca_Potential_Customer>();
                        var listPotential = dbConn.Select<Deca_Potential_Customer>("PhysicalID IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS004') AND HaveCard = 0");
                        if (listPotential.Count > 0)
                        {
                            foreach (var potential in listPotential)
                            {
                                // Kiểm tra tín dụng, Sdt giao dịch, địa chỉ hiện tại ở Potential
                                if (String.IsNullOrEmpty(potential.PhysicalIDBank) || potential.CreditLimit == 0 || String.IsNullOrEmpty(potential.PhoneTransaction) || String.IsNullOrEmpty(potential.CurrentAddress))
                                {
                                    potential.Error = " - Vui lòng nhập đầy đủ các thông tin  CMND(NH), Tín dụng , Số điện thoại giao dịch , Địa chỉ hiện tại";
                                    listPotentialError.Add(potential);
                                    continue;

                                }

                                // Kiểm tra các thông tin trong bảng Deca_Customer:
                                var check = dbConn.FirstOrDefault<Deca_Customer>("PhysicalID={0}", potential.PhysicalIDBank);
                                if (check != null)
                                {
                                    potential.Error = " - Trùng số chứng minh nhân dân trong Customer";
                                    listPotentialError.Add(potential);
                                    continue;
                                }
                                check = dbConn.FirstOrDefault<Deca_Customer>("Phone={0} AND CompanyID={1}", potential.Phone, potential.CompanyID);
                                if (check != null)
                                {
                                    potential.Error = " - Trùng số điện thoại trong Customer!";
                                    listPotentialError.Add(potential);
                                    continue;
                                }
                                check = dbConn.FirstOrDefault<Deca_Customer>("MobilePhone={0} AND CompanyID={1}", potential.PhoneTransaction, potential.CompanyID);
                                if (check != null)
                                {
                                    potential.Error = " - Trùng số điện thoại giao dịch trong Customer!";
                                    listPotentialError.Add(potential);
                                    continue;
                                }
                                check = dbConn.FirstOrDefault<Deca_Customer>("EmployeeID={0} AND CompanyID={1}", potential.EmployeeID, potential.CompanyID);
                                if (check != null)
                                {
                                    potential.Error = " - Trùng mã nhân viên trong Customer!";
                                    listPotentialError.Add(potential);
                                    continue;
                                }


                                // insert vào Customer:
                                Deca_Customer customer = new Deca_Customer();
                                customer.IsNew = true;
                                customer.Active = true;
                                customer.CreatedAt = DateTime.Now;
                                customer.CreatedBy = User.Identity.Name;
                                customer.Status = "CUSTOMERSTATUS01";
                                customer.CompanyID = potential.CompanyID;
                                customer.CompanyName = dbConn.FirstOrDefault<Deca_Company>("Id = {0}", potential.CompanyID) != null ? dbConn.FirstOrDefault<Deca_Company>("Id = {0}", potential.CompanyID).ShortName : "";
                                customer.EmployeeID = potential.EmployeeID;
                                customer.Fullname = potential.Fullname;
                                customer.Sex = potential.Sex;
                                customer.Birthday = potential.Birthday;
                                customer.MaritalStatus = false; // check lai
                                customer.PhysicalID = potential.PhysicalIDBank; // Lấy CMND do Ngân hàng confirm
                                customer.MobilePhone = potential.PhoneTransaction;
                                customer.Phone = potential.Phone;
                                customer.Email = potential.Email;
                                customer.HomeTown = potential.HomeTown;
                                customer.Address = potential.CurrentAddress;
                                customer.Department = potential.Department;
                                customer.Position = potential.Position;
                                customer.StartWorkingDate = potential.StartWorkingDate;
                                customer.Education = potential.Education;
                                customer.Income = potential.Income;
                                customer.PayrollBank = potential.PayrollBank;
                                customer.CreditLimit = potential.CreditLimit;
                                customer.CreditBank = potential.RequestCreditBank;
                                customer.CardType = ""; // check lai
                                //customer.OnlineAccount = ""; 
                                dbConn.Insert(customer);

                                int Id = (int)dbConn.GetLastInsertId();
                                customer.Id = Id;
                                string datetime = DateTime.Now.ToString("yyMMdd");
                                customer.CustomerID = "CU" + datetime + Id;
                                dbConn.Update(customer);


                                // Cập nhật dữ liệu Potential
                                potential.IsNew = true;
                                potential.IssuedCard = DateTime.Now;
                                potential.CreditCardStatus = "CCS006";
                                if (Description != "")
                                {
                                    potential.BankNote = Description;
                                }
                                potential.CustomerID = customer.CustomerID;
                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);



                                // re-update order header
                                var orderHeader = dbConn.Select<Deca_Order_Header>("PhysicalID={0} AND EmployeeID={1} AND PaymentStatus={2}", customer.PhysicalID, customer.EmployeeID, 0);
                                if (orderHeader.Count() > 0)
                                {
                                    foreach (var order in orderHeader)
                                    {
                                        order.CustomerID = customer.CustomerID;
                                        order.MobilePhone = customer.MobilePhone;
                                        order.UpdatedAt = DateTime.Now;
                                        order.UpdatedBy = currentUser.UserName;
                                        dbConn.Update(order);
                                    }
                                }


                                // insert customer Index
                                var customerIndex = new Deca_Customer_Index();
                                customerIndex.CustomerID = customer.CustomerID;
                                customerIndex.Fullname = customer.Fullname;
                                customerIndex.Phone = customer.MobilePhone;
                                customerIndex.Email = customer.Email;
                                customerIndex.PhysicalID = customer.PhysicalID;
                                customerIndex.Meta = Helpers.convertToUnSign3.Init(customer.Fullname.ToLower()) + ";" + customer.Email + ";" + customer.MobilePhone + ";" + customer.Phone + ";" + customer.PhysicalID;
                                customerIndex.DataSource = "customer";
                                customerIndex.CreatedAt = DateTime.Now;
                                customerIndex.CreatedBy = currentUser.UserName;
                                dbConn.Insert(customerIndex);

                                // update log
                                var log = new Deca_Customer_Log();
                                log.CustomerID = customer.CompanyID + ":" + customer.EmployeeID;
                                log.Log = customer;
                                log.CreatedAt = DateTime.Now;
                                log.CreatedBy = currentUser.UserName;
                                dbConn.Insert(log);

                                s++;
                            }

                            f = listPotentialError.Count();
                            dbTrans.Commit();
                            return Json(new { success = s, fail = f, listError = listPotentialError.ToList() });
                        }
                        else
                        {
                            return Json(new { error = true, message = "Các khách hàng được chọn để phát hành thẻ có trạng thái mở thẻ không hợp lệ" });
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

        [HttpPost]
        public ActionResult DoneCard(string listData)
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
                        var listid = listData.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();

                        // Chỉ được Hoàn tất khi đang ở trạng thái CCS006: Phát hành thẻ & Khách hàng chưa có thẻ.
                        var listPotentialError = new List<Deca_Potential_Customer>();
                        var listPotential = dbConn.Select<Deca_Potential_Customer>("PhysicalID IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS006') AND HaveCard = 0");
                        if (listPotential.Count > 0)
                        {
                            foreach (var potential in listPotential)
                            {

                                // Cập nhật dữ liệu Potential
                                potential.IsNew = true;
                                potential.Done = DateTime.Now;
                                potential.CreditCardStatus = "CCS007";
                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);
                                s++;
                            }

                            dbTrans.Commit();
                            return Json(new { success = s, fail = f });
                        }
                        else
                        {
                            return Json(new { error = true, message = "Các khách hàng được chọn để hoàn tất có trạng thái mở thẻ không hợp lệ" });
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

        [HttpPost]
        public ActionResult DeniedCard(string listData, string Reason, string CICEndDate, string Description)
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
                        var listid = listData.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();

                        // Chỉ được từ chối mở thẻ khi đang ở trạng thái CCS004: Đã gửi ngân hàng & Khách hàng chưa có thẻ.
                        var listPotentialError = new List<Deca_Potential_Customer>();
                        var listPotential = dbConn.Select<Deca_Potential_Customer>("PhysicalID IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus IN ('CCS004') AND HaveCard = 0");
                        if (listPotential.Count > 0)
                        {
                            foreach (var potential in listPotential)
                            {

                                if (String.IsNullOrEmpty(Reason))
                                {
                                    return Json(new { error = true, message = "Vui lòng chọn lý do từ chối" });
                                }

                                //if (Reason == "BDR004") // Nợ xấu trên CIC
                                //{
                                //    if (CICEndDate == "")
                                //    {
                                //        return Json(new { error = true, message = "Vui lòng nhập Ngày hết nợ trên CIC" });
                                //    }

                                //    DateTime expectedDate;
                                //    if (!DateTime.TryParse(CICEndDate, out expectedDate))
                                //    {
                                //        return Json(new { error = true, message = " Ngày hết nợ không hợp lệ." });
                                //    }
                                //    potential.CICBadDebt = DateTime.Parse(CICEndDate);

                                //}


                                // Cập nhật dữ liệu Potential
                                potential.IsNew = true;
                                potential.Active = true;
                                potential.RejectReason = Reason;
                                potential.BankDenied = DateTime.Now;
                                potential.CreditCardStatus = "CCS005";
                                if (Description != "")
                                {
                                    potential.BankNote = Description;
                                }
                                potential.UpdatedAt = DateTime.Now;
                                potential.UpdatedBy = currentUser.UserName;
                                dbConn.Update(potential);

                                s++;
                            }
                            f = listPotentialError.Count();
                            dbTrans.Commit();
                            return Json(new { success = s, fail = f, listError = listPotentialError.ToList() });
                        }
                        else
                        {
                            return Json(new { error = true, message = "Các khách hàng được chọn để từ chối có trạng thái mở thẻ không hợp lệ" });
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
    }
}