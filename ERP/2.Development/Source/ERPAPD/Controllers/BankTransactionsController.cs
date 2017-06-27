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
    public class BankTransactionsController : CustomController
    {
        //
        // GET: /BankTransactions/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Bank_Transactions));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Bank_Transactions));
            //}
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //ViewBag.City = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    ViewBag.BankActionStatus = dbConn.Select<Deca_Code_Master>("[CodeType] = {0}", "BankActionStatus").OrderBy(a => a.CodeID);
                    ViewBag.BankInstallment = dbConn.Select<Deca_Bank_Installment>();
                    ViewBag.Bank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    ViewBag.Status = dbConn.Select<Deca_Order_Status>().OrderBy(a => a.ID);
                    //ViewBag.SaleMan = dbConn.Select<Users>().OrderBy(a => a.UserName);
                    //ViewBag.Status = dbConn.GetFirstColumn<string>("SELECT DISTINCT [STATUS] FROM Deca_Order_Header");

                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = Deca_Bank_TransactionsViewModel.getRead("");
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return Json(new List<Deca_Bank_TransactionsViewModel>().ToDataSourceResult(request));
            }
        }

        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Bank_Transactions.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Bank_Transactions_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string query = "";
                    var data = Deca_Bank_TransactionsViewModel.getRead("").ToDataSourceResult(request);


                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Transactions"];
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataBankActionStatus = dbConn.Select<Deca_Code_Master>("[CodeType] = {0} AND CodeID <> 'BankActionStatus01'", "BankActionStatus").OrderBy(a => a.CodeID);
                    var dataBankInstallMent = dbConn.Select<Deca_Bank_Installment>();
                    var dataStatus = dbConn.Select<Deca_Order_Status>();
                    int rowData = 2;

                    foreach (Deca_Bank_TransactionsViewModel item in data.Data)
                    {
                        int i = 1;
                        rowData++;
                        if (item.BankActionStatus == "BankActionStatus01")
                        {
                            dbConn.Update<Deca_Bank_Transactions>("BankActionStatus = 'BankActionStatus02'", "OrderID = '" + item.OrderID + "'");
                            item.BankActionStatus = "BankActionStatus02";
                        }
                        expenseSheet.Cells[rowData, i++].Value = dataBankActionStatus.FirstOrDefault(a => a.CodeID == item.BankActionStatus) == null ? "" : dataBankActionStatus.FirstOrDefault(a => a.CodeID == item.BankActionStatus).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.OrderID;
                        expenseSheet.Cells[rowData, i++].Value = item.RefID;
                        expenseSheet.Cells[rowData, i++].Value = item.AuthTransRef;
                        expenseSheet.Cells[rowData, i++].Value = item.Amount;
                        expenseSheet.Cells[rowData, i++].Value = item.OrderDate;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                        expenseSheet.Cells[rowData, i++].Value = item.MobilePhone;
                        expenseSheet.Cells[rowData, i++].Value = item.Installment;
                        expenseSheet.Cells[rowData, i++].Value = item.BankInstallment > 0 ? dataBankInstallMent.FirstOrDefault(a => a.ID == item.BankInstallment).Installment : 0;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.Bank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.Bank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = item.TransactionInfo;
                        expenseSheet.Cells[rowData, i++].Value = dataStatus.FirstOrDefault(a => a.RefID == item.Status) == null ? "" : dataStatus.FirstOrDefault(a => a.RefID == item.Status).Name;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedBy;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet2 = excelPkg.Workbook.Worksheets["List1"];
                    foreach (Deca_Code_Master master in dataBankActionStatus)
                    {
                        dataSheet2.Cells[rowData++, 1].Value = master.Description;
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
            int totalCols = 15;
            DataTable dt = new DataTable(oSheet.Name);
            DataRow dr = null;
            for (int i = 1; i <= totalRows; i++)
            {
                if (i > 2) dr = dt.Rows.Add();
                for (int j = 1; j <= totalCols; j++)
                {
                    if (i == 1)
                        dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
                    else if (i > 2)
                        dr[j - 1] = oSheet.Cells[i, j].Value;
                }
            }
            return dt;
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Bank_Transactions.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Transactions"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Transactions"];

                        var data = WorksheetToDataTables(oSheet);
                        var listPotential = data.AsEnumerable().Select(s => new Deca_Bank_TransactionsViewModel
                        {
                            OrderID = s["Order ID"] != null ? s["Order ID"].ToString() : null,
                            BankActionStatus = s["Bank action status"] != null ? s["Bank action status"].ToString() : null,
                            BankInstallment = s["BankInstallment"] != null && !string.IsNullOrEmpty(s["BankInstallment"].ToString()) ? int.Parse(s["BankInstallment"].ToString()) : 0,
                            Bank = s["Bank Funding"] != null ? s["Bank Funding"].ToString() : null,
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {

                            //check required field
                            if (n == 1)
                            {
                                var required = listPotential.Where(s =>
                                        String.IsNullOrEmpty(s.OrderID) ||
                                        String.IsNullOrEmpty(s.BankActionStatus) ||
                                        s.BankInstallment < 0 || string.IsNullOrEmpty(s.Bank));
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
                                //check exist status
                                var company = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE [CodeType]='BankActionStatus'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.BankActionStatus) || string.IsNullOrEmpty(s.BankActionStatus));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Trạng thái không hợp lệ!");
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


                            if (n == 1)
                            {
                                //check exist bank
                                var company = dbConn.GetFirstColumn<string>("SELECT [BankName] FROM DC_Bank_Definition");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.Bank) || string.IsNullOrEmpty(s.Bank));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Tên ngân hàng không tìm thấy!");
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

                            var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                            var dataBankActionStatus = dbConn.Select<Deca_Code_Master>("[CodeType] = {0}", "BankActionStatus").OrderBy(a => a.CodeID);
                            var dataBankInstallMent = dbConn.Select<Deca_Bank_Installment>();
                            //save potential
                            foreach (var item in listPotential)
                            {
                                try
                                {

                                    Deca_Bank_Transactions updateTrans = dbConn.FirstOrDefault<Deca_Bank_Transactions>("OrderID={0}", item.OrderID);
                                    if (updateTrans == null) throw new Exception("Không tìm thấy đơn hàng trong hệ thống.");
                                    var checkBank = dataBank.FirstOrDefault(a => a.BankName == item.Bank);
                                    if (item.BankInstallment > 0)
                                    {
                                        var checkinstallment = dataBankInstallMent.FirstOrDefault(a => a.Installment == item.BankInstallment && a.BankID == checkBank.BankID);
                                        if (checkinstallment == null)
                                        {
                                            throw new Exception("Kì hạn trả góp này chưa được khai báo.");
                                        }
                                        updateTrans.BankInstallment = checkinstallment.ID;
                                    }
                                    else updateTrans.BankInstallment = 0;

                                    updateTrans.BankActionStatus = dataBankActionStatus.FirstOrDefault(a => a.Description == item.BankActionStatus) == null ? "" : dataBankActionStatus.FirstOrDefault(a => a.Description == item.BankActionStatus).CodeID;
                                    updateTrans.UpdatedAt = DateTime.Now;
                                    updateTrans.UpdatedBy = currentUser.UserName;

                                    dbConn.Update(updateTrans);
                                    total++;
                                }
                                catch (Exception ex)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, ex.Message);
                                    rownumber++;
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
        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Excel"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }

        public void writeErrorToFile(ExcelWorksheet expenseSheet, int rowData, Deca_Bank_TransactionsViewModel item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                var dataBankActionStatus = dbConn.Select<Deca_Code_Master>("[CodeType] = {0}", "BankActionStatus").OrderBy(a => a.CodeID);
                var dataBankInstallMent = dbConn.Select<Deca_Bank_Installment>();

                int i = 1;
                expenseSheet.Cells[rowData, i++].Value = dataBankActionStatus.FirstOrDefault(a => a.CodeID == item.BankActionStatus) == null ? "" : dataBankActionStatus.FirstOrDefault(a => a.CodeID == item.BankActionStatus).Description;
                expenseSheet.Cells[rowData, i++].Value = item.OrderID;
                expenseSheet.Cells[rowData, i++].Value = item.AuthTransRef;
                expenseSheet.Cells[rowData, i++].Value = item.Amount;
                expenseSheet.Cells[rowData, i++].Value = item.OrderDate;
                expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                expenseSheet.Cells[rowData, i++].Value = item.MobilePhone;
                expenseSheet.Cells[rowData, i++].Value = item.Installment;
                expenseSheet.Cells[rowData, i++].Value = item.BankInstallment > 0 ? dataBankInstallMent.FirstOrDefault(a => a.ID == item.BankInstallment).Installment : 0;
                expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.Bank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.Bank).BankName;
                expenseSheet.Cells[rowData, i++].Value = item.TransactionInfo;
                expenseSheet.Cells[rowData, i++].Value = item.CreatedAt;
                expenseSheet.Cells[rowData, i++].Value = item.CreatedBy;
                expenseSheet.Cells[rowData, i++].Value = item.UpdatedAt;
                expenseSheet.Cells[rowData, i++].Value = item.UpdatedBy;
            }
        }
    }
}