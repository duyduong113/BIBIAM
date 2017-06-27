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
    public class DecaCustomerController : CustomController
    {
        //
        // GET: /DecaCustomer/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Customer_Log), typeof(Deca_Customer));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Customer_Log), typeof(Deca_Customer));
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
                    ViewBag.Status = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerStatus");
                    ViewBag.listCardType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardType");
                    ViewBag.listCardRanking = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardRanking");
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
                    data = KendoApplyFilter.KendoData<Deca_Customer>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Customer> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    try
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            foreach (var item in items)
                            {
                                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                {
                                    item.IsNew = true;
                                    item.Active = true;
                                    item.CreatedAt = DateTime.Now;
                                    item.CreatedBy = User.Identity.Name;
                                    item.Status = "CUSTOMERSTATUS01";
                                    item.CompanyName = dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID) != null ? dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID).ShortName : "";
                                    dbConn.Insert(item);
                                    int Id = (int)dbConn.GetLastInsertId();
                                    item.Id = Id;
                                    string datetime = DateTime.Now.ToString("yyMMdd");

                                    item.CustomerID = "CU" + datetime + Id;
                                    var check = dbConn.FirstOrDefault<Deca_Customer>("PhysicalID={0} AND Id <> {1}", item.PhysicalID, Id);
                                    if (check != null)
                                    {
                                        ModelState.AddModelError("", "Trùng số chứng minh nhân dân!");
                                        return Json(items.ToDataSourceResult(request, ModelState));
                                    }
                                    check = dbConn.FirstOrDefault<Deca_Customer>("Phone={0} AND CompanyID={1} AND Id <> {2}", item.Phone, item.CompanyID, Id);
                                    if (check != null)
                                    {
                                        ModelState.AddModelError("", "Trùng số điện thoại!");
                                        return Json(items.ToDataSourceResult(request, ModelState));
                                    }
                                    check = dbConn.FirstOrDefault<Deca_Customer>("MobilePhone={0} AND CompanyID={1} AND Id <> {2}", item.MobilePhone, item.CompanyID, Id);
                                    if (check != null)
                                    {
                                        ModelState.AddModelError("", "Trùng số điện thoại!");
                                        return Json(items.ToDataSourceResult(request, ModelState));
                                    }
                                    check = dbConn.FirstOrDefault<Deca_Customer>("EmployeeID={0} AND CompanyID={1} AND Id <> {2}", item.EmployeeID, item.CompanyID, Id);
                                    if (check != null)
                                    {
                                        ModelState.AddModelError("", "Trùng mã nhân viên!");
                                        return Json(items.ToDataSourceResult(request, ModelState));
                                    }

                                    var existP = dbConn.FirstOrDefault<Deca_Potential_Customer>("CompanyID = {0} AND EmployeeID={1}", item.CompanyID, item.EmployeeID);
                                    if (existP != null)
                                    {
                                        existP.CustomerID = item.CustomerID;
                                        existP.HaveCard = true;
                                        existP.Status = "done";
                                        existP.UpdatedAt = DateTime.Now;
                                        existP.UpdatedBy = currentUser.UserName;
                                        dbConn.Update(existP);

                                        item.Address = string.IsNullOrEmpty(item.Address) ? existP.Address : item.Address;
                                        item.Birthday = !item.Birthday.HasValue ? existP.Birthday : item.Birthday;
                                        item.CreditLimit = item.CreditLimit > 0 ? item.CreditLimit : item.CreditLimit;
                                        item.Department = string.IsNullOrEmpty(item.Department) ? existP.Department : item.Department;
                                        item.Education = string.IsNullOrEmpty(item.Education) ? existP.Education : item.Education;
                                        item.Email = string.IsNullOrEmpty(item.Email) ? existP.Email : item.Email;
                                        item.HomeTown = string.IsNullOrEmpty(item.HomeTown) ? existP.HomeTown : item.HomeTown;
                                        item.Income = item.Income > 0 ? item.Income : item.Income;
                                        item.PayrollBank = string.IsNullOrEmpty(item.PayrollBank) ? existP.PayrollBank : item.PayrollBank;
                                        item.Position = string.IsNullOrEmpty(item.Position) ? existP.Position : item.Position;
                                        item.Sex = string.IsNullOrEmpty(item.Sex) ? existP.Sex : item.Sex;
                                        item.StartWorkingDate = !item.StartWorkingDate.HasValue ? existP.StartWorkingDate : item.StartWorkingDate;
                                    }
                                    else
                                    {
                                        existP = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID ={0}", item.PhysicalID);
                                        if (existP != null)
                                        {
                                            item.Address = string.IsNullOrEmpty(item.Address) ? existP.Address : item.Address;
                                            item.Birthday = !item.Birthday.HasValue ? existP.Birthday : item.Birthday;
                                            item.CreditLimit = item.CreditLimit > 0 ? item.CreditLimit : item.CreditLimit;
                                            item.Education = string.IsNullOrEmpty(item.Education) ? existP.Education : item.Education;
                                            item.HomeTown = string.IsNullOrEmpty(item.HomeTown) ? existP.HomeTown : item.HomeTown;
                                            item.Sex = string.IsNullOrEmpty(item.Sex) ? existP.Sex : item.Sex;
                                        }
                                    }

                                    dbConn.Update(item);

                                    // re-update order header
                                    var orderHeader = dbConn.Select<Deca_Order_Header>("PhysicalID={0} AND EmployeeID={1} AND PaymentStatus={2}", item.PhysicalID, item.EmployeeID, 0);
                                    if (orderHeader.Count() > 0)
                                    {
                                        foreach (var order in orderHeader)
                                        {
                                            order.CustomerID = item.CustomerID;
                                            order.MobilePhone = item.MobilePhone;
                                            order.UpdatedAt = DateTime.Now;
                                            order.UpdatedBy = currentUser.UserName;
                                            dbConn.Update(order);
                                        }
                                    }

                                    //insert customer Index
                                    var customerIndex = new Deca_Customer_Index();
                                    customerIndex.CustomerID = item.CustomerID;
                                    customerIndex.Fullname = item.Fullname;
                                    customerIndex.Phone = item.MobilePhone;
                                    customerIndex.Email = item.Email;
                                    customerIndex.PhysicalID = item.PhysicalID;
                                    customerIndex.Meta = Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" + item.Email + ";" + item.MobilePhone + ";" + item.Phone + ";" + item.PhysicalID;
                                    customerIndex.DataSource = "customer";
                                    customerIndex.CreatedAt = DateTime.Now;
                                    customerIndex.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(customerIndex);

                                    var log = new Deca_Customer_Log();
                                    log.CustomerID = item.CompanyID + ":" + item.EmployeeID;
                                    log.Log = item;
                                    log.CreatedAt = DateTime.Now;
                                    log.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(log);

                                    dbTrans.Commit();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }
            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Customer> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                            {
                                item.IsNew = true;
                                item.CompanyName = dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID) != null ? dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID).ShortName : "";
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = User.Identity.Name;
                                var check = dbConn.FirstOrDefault<Deca_Customer>("PhysicalID={0} AND Id<>{1}", item.PhysicalID, item.Id);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", "Trùng số chứng minh nhân dân!");
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }
                                check = dbConn.FirstOrDefault<Deca_Customer>("Phone={0} AND CompanyID={1} AND Id<>{2}", item.Phone, item.CompanyID, item.Id);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", "Trùng số điện thoại!");
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }
                                check = dbConn.FirstOrDefault<Deca_Customer>("MobilePhone={0} AND CompanyID={1} AND Id<>{2}", item.MobilePhone, item.CompanyID, item.Id);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", "Trùng số điện thoại!");
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }
                                check = dbConn.FirstOrDefault<Deca_Customer>("EmployeeID={0} AND CompanyID={1} AND Id<>{2}", item.EmployeeID, item.CompanyID, item.Id);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", "Trùng mã nhân viên!");
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }
                                dbConn.Update(item);

                                //update customer Index
                                var customerIndex = dbConn.SingleOrDefault<Deca_Customer_Index>("PhysicalID={0} AND DataSource = 'customer'", item.PhysicalID);
                                if (customerIndex != null)
                                {
                                    customerIndex.CustomerID = item.CustomerID;
                                    customerIndex.Fullname = item.Fullname;
                                    customerIndex.Phone = item.MobilePhone;
                                    customerIndex.Email = item.Email;
                                    customerIndex.PhysicalID = item.PhysicalID;
                                    customerIndex.Meta = (!String.IsNullOrEmpty(item.Fullname) ? Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.MobilePhone) ? Helpers.convertToUnSign3.Init(item.MobilePhone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(item.PhysicalID.ToLower());
                                    customerIndex.DataSource = "customer";
                                    customerIndex.UpdatedAt = DateTime.Now;
                                    customerIndex.UpdatedBy = currentUser.UserName;
                                    dbConn.Update(customerIndex);

                                }
                                else
                                {
                                    customerIndex = new Deca_Customer_Index();
                                    customerIndex.CustomerID = item.CustomerID;
                                    customerIndex.Fullname = item.Fullname;
                                    customerIndex.Phone = item.MobilePhone;
                                    customerIndex.Email = item.Email;
                                    customerIndex.PhysicalID = item.PhysicalID;
                                    customerIndex.Meta = (!String.IsNullOrEmpty(item.Fullname) ? Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.MobilePhone) ? Helpers.convertToUnSign3.Init(item.MobilePhone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(item.PhysicalID.ToLower());
                                    customerIndex.DataSource = "customer";
                                    customerIndex.CreatedAt = DateTime.Now;
                                    customerIndex.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(customerIndex);
                                }

                                var log = new Deca_Customer_Log();
                                log.CustomerID = item.CustomerID;
                                log.Log = item;
                                log.CreatedAt = DateTime.Now;
                                log.CreatedBy = currentUser.UserName;
                                dbConn.Insert(log);

                                dbTrans.Commit();
                            }
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

        private DataTable WorksheetToDataTables(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = 28;
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
        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Customer.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Customer>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Customer>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Customer>().ToList();
                    }
                    var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                    var dataStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerStatus");
                    var dataCompany = dbConn.Select<Deca_Company>();
                    var dataCardType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardType");
                    var dataCardRanking = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardRanking");
                    var dataBranch = dbConn.Select<DC_Company_Branch>();

                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["DecaCustomer"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        int i = 1;
                        rowData++;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                        expenseSheet.Cells[rowData, i++].Value = dataCompany.FirstOrDefault(a => a.Id == item.CompanyID) == null ? "" : dataCompany.FirstOrDefault(a => a.Id == item.CompanyID).ShortName;
                        expenseSheet.Cells[rowData, i++].Value = dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID) == null ? "" : dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID).BranchName;
                        expenseSheet.Cells[rowData, i++].Value = item.EmployeeID;
                        expenseSheet.Cells[rowData, i++].Value = item.Fullname;
                        expenseSheet.Cells[rowData, i++].Value = dataSex.FirstOrDefault(a => a.CodeID == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.CodeID == item.Sex).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Birthday;
                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                        expenseSheet.Cells[rowData, i++].Value = item.Phone;
                        expenseSheet.Cells[rowData, i++].Value = item.MobilePhone;
                        expenseSheet.Cells[rowData, i++].Value = item.Email;
                        expenseSheet.Cells[rowData, i++].Value = dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown).TerritoryName;
                        expenseSheet.Cells[rowData, i++].Value = item.Address;
                        expenseSheet.Cells[rowData, i++].Value = item.Department;
                        expenseSheet.Cells[rowData, i++].Value = item.Position;
                        expenseSheet.Cells[rowData, i++].Value = item.StartWorkingDate;
                        expenseSheet.Cells[rowData, i++].Value = dataEducation.FirstOrDefault(a => a.CodeID == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Education).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Income;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = item.CreditLimit;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.CreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.CreditBank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = dataCardType.FirstOrDefault(a => a.CodeID == item.CardType) == null ? "" : dataCardType.FirstOrDefault(a => a.CodeID == item.CardType).Description;
                        expenseSheet.Cells[rowData, i++].Value = dataCardRanking.FirstOrDefault(a => a.CodeID == item.CardRanking) == null ? "" : dataCardRanking.FirstOrDefault(a => a.CodeID == item.CardRanking).Description;
                        expenseSheet.Cells[rowData, i++].Value = dataStatus.FirstOrDefault(a => a.CodeID == item.Status) == null ? "" : dataStatus.FirstOrDefault(a => a.CodeID == item.Status).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Active;
                        expenseSheet.Cells[rowData, i++].Value = item.IsBlacklist;
                        expenseSheet.Cells[rowData, i++].Value = item.ExistedToken;
                        expenseSheet.Cells[rowData, i++].Value = item.OnlineAccount;
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
                    foreach (Deca_Code_Master master in dataStatus)
                    {
                        dataSheet6.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet7 = excelPkg.Workbook.Worksheets["List6"];
                    foreach (Deca_Company master in dataCompany)
                    {
                        dataSheet7.Cells[rowData++, 1].Value = master.ShortName;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet8 = excelPkg.Workbook.Worksheets["List7"];
                    foreach (Deca_Code_Master master in dataCardType)
                    {
                        dataSheet8.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet9 = excelPkg.Workbook.Worksheets["List8"];
                    foreach (Deca_Code_Master master in dataCardRanking)
                    {
                        dataSheet9.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet10 = excelPkg.Workbook.Worksheets["List9"];
                    foreach (DC_Company_Branch master in dataBranch)
                    {
                        dataSheet10.Cells[rowData++, 1].Value = master.BranchName;
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Customer.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["DecaCustomer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["DecaCustomer"];

                        var data = WorksheetToDataTables(oSheet);

                        DateTime temp;

                        var listPotential = data.AsEnumerable().Select(s => new Deca_Customer
                        {
                            CompanyName = s["Công ty"] != null ? s["Công ty"].ToString() : null,
                            BranchID = s["Chi nhánh"] != null ? s["Chi nhánh"].ToString() : "",
                            EmployeeID = s["Mã nhân viên"] != null ? s["Mã nhân viên"].ToString() : null,
                            Fullname = s["Họ và tên"] != null ? s["Họ và tên"].ToString() : null,
                            Sex = s["Giới tính"] != null ? s["Giới tính"].ToString() : null,
                            Birthday = s["Ngày sinh"] != null && DateTime.TryParse(s["Ngày sinh"].ToString(), out temp) ? DateTime.Parse(s["Ngày sinh"].ToString()) : DateTime.Parse("1900-01-01"),
                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                            MobilePhone = s["SĐT giao dịch"] != null ? s["SĐT giao dịch"].ToString() : null,
                            Phone = s["Điện thoại"] != null ? s["Điện thoại"].ToString() : null,
                            Email = s["Email"] != null ? s["Email"].ToString() : null,
                            HomeTown = s["Tỉnh"] != null ? s["Tỉnh"].ToString() : null,
                            Address = s["Địa chỉ"] != null ? s["Địa chỉ"].ToString() : null,
                            Department = s["Phòng ban"] != null ? s["Phòng ban"].ToString() : null,
                            Position = s["Chức vụ"] != null ? s["Chức vụ"].ToString() : null,
                            StartWorkingDate = s["Ngày vào làm"] != null && DateTime.TryParse(s["Ngày vào làm"].ToString(), out temp) ? DateTime.Parse(s["Ngày vào làm"].ToString()) : DateTime.Parse("1900-01-01"),
                            Education = s["Trình độ"] != null ? s["Trình độ"].ToString() : null,
                            Income = s["Thu nhập/tháng"] != null && !string.IsNullOrEmpty(s["Thu nhập/tháng"].ToString()) ? double.Parse(s["Thu nhập/tháng"].ToString()) : 0,
                            PayrollBank = s["Nhận lương qua NH"] != null ? s["Nhận lương qua NH"].ToString() : null,
                            CreditLimit = s["Tín dụng"] != null && !string.IsNullOrEmpty(s["Tín dụng"].ToString()) ? double.Parse(s["Tín dụng"].ToString()) : 0,
                            CreditBank = s["Cấp bởi"] != null ? s["Cấp bởi"].ToString() : null,
                            CardType = s["Loại thẻ"] != null ? s["Loại thẻ"].ToString() : null,
                            CardRanking = s["Nhóm thẻ"] != null ? s["Nhóm thẻ"].ToString() : null,
                            OnlineAccount = s["Tài khoản portal"] != null ? s["Tài khoản portal"].ToString() : null,
                            Status = s["Trạng thái"] != null ? s["Trạng thái"].ToString() : null,
                            IsBlacklist = s["Blacklist"] != null && !string.IsNullOrEmpty(s["Blacklist"].ToString()) ? bool.Parse(s["Blacklist"].ToString()) : false,
                            ExistedToken = s["Token"] != null && !string.IsNullOrEmpty(s["Token"].ToString()) ? bool.Parse(s["Token"].ToString()) : false,
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {

                            //check required field
                            if (n == 1)
                            {
                                var required = listPotential.Where(s =>
                                        String.IsNullOrEmpty(s.CompanyName) ||
                                        String.IsNullOrEmpty(s.EmployeeID) ||
                                        String.IsNullOrEmpty(s.Fullname) ||
                                        String.IsNullOrEmpty(s.PhysicalID) ||
                                        String.IsNullOrEmpty(s.MobilePhone)
                                        );
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
                                //check exist company
                                var company = dbConn.GetFirstColumn<string>("SELECT ShortName FROM Deca_Company");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.CompanyName));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Doanh nghiệp không tồn tại trong hệ thống");
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
                                //check exist key company,customer,status = done
                                var doneCus = dbConn.GetFirstColumn<string>("SELECT CompanyName + ':' + EmployeeID FROM Deca_Customer");

                                var exist = listPotential.Where(s => doneCus.Contains(s.CompanyName + ":" + s.EmployeeID));
                                foreach (var item in exist)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "CustomerID - Tài khoản đã tồn tại trong hệ thống");
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



                            if (n == 1)
                            {
                                //check exist sex
                                var company = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'Gender'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.Sex) && !string.IsNullOrEmpty(s.Sex));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Giới tính không tồn tại trong hệ thống");
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
                                //check exist cardtype
                                var company = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'CardType'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.CardType) && !string.IsNullOrEmpty(s.CardType));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Loại thé không tồn tại trong hệ thống");
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
                                //check exist cardtype
                                var company = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'CardRanking'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.CardRanking) && !string.IsNullOrEmpty(s.CardRanking));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Nhóm thẻ không tồn tại trong hệ thống");
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
                                //check exist status
                                var company = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'CustomerStatus'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.Status) && !string.IsNullOrEmpty(s.Status));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Trạng thái không tồn tại trong hệ thống");
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
                                //check exist BAnk
                                var company = dbConn.GetFirstColumn<string>("SELECT BankName FROM DC_Bank_Definition");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.PayrollBank) && !string.IsNullOrEmpty(s.PayrollBank));
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
                            if (n == 1)
                            {
                                //check exist Region
                                var company = dbConn.GetFirstColumn<string>("SELECT TerritoryName FROM DC_OCM_Territory WHERE [Level]='Province'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.HomeTown) && !string.IsNullOrEmpty(s.HomeTown));
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




                            if (n == 1)
                            {
                                //check exist physicalid
                                var donePhys = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Customer");

                                var existP = listPotential.Where(s => donePhys.Contains(s.PhysicalID));
                                foreach (var item in existP)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "PhysicalID - Tài khoản đã tồn tại trong hệ thống");
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
                                //check exist mobile
                                var donePhone = dbConn.GetFirstColumn<string>("SELECT Phone FROM Deca_Customer WHERE Phone IS NOT NULL");

                                var existPh = listPotential.Where(s => donePhone.Contains(s.Phone));
                                foreach (var item in existPh)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Phone - Tài khoản đã tồn tại trong hệ thống");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(existPh).ToList();
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
                                //check exist Branch
                                var branch = dbConn.GetFirstColumn<string>("SELECT BranchName FROM DC_Company_Branch");

                                var wrongBranch = listPotential.Where(s => !branch.Contains(s.BranchID) && !string.IsNullOrEmpty(s.BranchID));
                                foreach (var item in wrongBranch)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Chi nhánh không tồn tại trong hệ thống");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(wrongBranch).ToList();
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
                            var dataStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerStatus");
                            var dataCardType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardType");
                            var dataCardRanking = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardRanking");

                            //save potential
                            foreach (var item in listPotential)
                            {
                                try
                                {
                                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                    {
                                        item.IsNew = true;
                                        item.Active = true;
                                        item.CompanyID = dbConn.FirstOrDefault<Deca_Company>("ShortName = {0}", item.CompanyName) != null ? dbConn.FirstOrDefault<Deca_Company>("ShortName = {0}", item.CompanyName).Id : 0;
                                        item.BranchID = dbConn.FirstOrDefault<DC_Company_Branch>("BranchName = {0}", item.BranchID) != null ? dbConn.FirstOrDefault<DC_Company_Branch>("BranchName = {0}", item.BranchID).BranchID : "";
                                        item.HomeTown = dataCity.FirstOrDefault(a => a.TerritoryName == item.HomeTown) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryName == item.HomeTown).TerritoryID;
                                        item.Sex = dataSex.FirstOrDefault(a => a.Description == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.Description == item.Sex).CodeID;
                                        item.Education = dataEducation.FirstOrDefault(a => a.Description == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.Description == item.Education).CodeID;
                                        item.PayrollBank = dataBank.FirstOrDefault(a => a.BankName == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankName == item.PayrollBank).BankID;
                                        item.CreditBank = dataBank.FirstOrDefault(a => a.BankName == item.CreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankName == item.CreditBank).BankID;
                                        item.Status = dataStatus.FirstOrDefault(a => a.Description == item.Status) == null ? "" : dataStatus.FirstOrDefault(a => a.Description == item.Status).CodeID;
                                        item.CardType = dataCardType.FirstOrDefault(a => a.Description == item.CardType) == null ? "" : dataCardType.FirstOrDefault(a => a.Description == item.CardType).CodeID;
                                        item.CardRanking = dataCardRanking.FirstOrDefault(a => a.Description == item.CardRanking) == null ? "" : dataCardRanking.FirstOrDefault(a => a.Description == item.CardRanking).CodeID;
                                        item.CreatedAt = DateTime.Now;
                                        item.CreatedBy = currentUser.UserName;
                                        dbConn.Insert(item);

                                        int Id = (int)dbConn.GetLastInsertId();
                                        item.Id = Id;

                                        string datetime = DateTime.Now.ToString("yyMMdd");

                                        item.CustomerID = "CU" + datetime + Id.ToString();

                                        dbConn.Update(item);

                                        var existP = dbConn.FirstOrDefault<Deca_Potential_Customer>("CompanyID = {0} AND EmployeeID={1}", item.CompanyID, item.EmployeeID);
                                        if (existP != null)
                                        {
                                            existP.CustomerID = item.CustomerID;
                                            existP.HaveCard = true;
                                            existP.Status = "done";
                                            existP.UpdatedAt = DateTime.Now;
                                            existP.UpdatedBy = currentUser.UserName;
                                            dbConn.Update(existP);

                                            item.Address = string.IsNullOrEmpty(item.Address) ? existP.Address : item.Address;
                                            item.Birthday = item.Birthday == null ? existP.Birthday : item.Birthday;
                                            item.CreditLimit = item.CreditLimit > 0 ? item.CreditLimit : item.CreditLimit;
                                            item.Department = string.IsNullOrEmpty(item.Department) ? existP.Department : item.Department;
                                            item.Education = string.IsNullOrEmpty(item.Education) ? existP.Education : item.Education;
                                            item.Email = string.IsNullOrEmpty(item.Email) ? existP.Email : item.Email;
                                            item.HomeTown = string.IsNullOrEmpty(item.HomeTown) ? existP.HomeTown : item.HomeTown;
                                            item.Income = item.Income > 0 ? item.Income : item.Income;
                                            item.PayrollBank = string.IsNullOrEmpty(item.PayrollBank) ? existP.PayrollBank : item.PayrollBank;
                                            item.Position = string.IsNullOrEmpty(item.Position) ? existP.Position : item.Position;
                                            item.Sex = string.IsNullOrEmpty(item.Sex) ? existP.Sex : item.Sex;
                                            item.StartWorkingDate = item.StartWorkingDate == null ? existP.StartWorkingDate : item.StartWorkingDate;
                                        }
                                        else
                                        {
                                            existP = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID ={0}", item.PhysicalID);
                                            if (existP != null)
                                            {
                                                item.Address = string.IsNullOrEmpty(item.Address) ? existP.Address : item.Address;
                                                item.Birthday = !item.Birthday.HasValue ? existP.Birthday : item.Birthday;
                                                item.CreditLimit = item.CreditLimit > 0 ? item.CreditLimit : item.CreditLimit;
                                                item.Education = string.IsNullOrEmpty(item.Education) ? existP.Education : item.Education;
                                                item.HomeTown = string.IsNullOrEmpty(item.HomeTown) ? existP.HomeTown : item.HomeTown;
                                                item.Sex = string.IsNullOrEmpty(item.Sex) ? existP.Sex : item.Sex;
                                            }
                                        }
                                        dbConn.Update(item);

                                        var customerIndex = new Deca_Customer_Index();
                                        customerIndex.CustomerID = item.CustomerID;
                                        customerIndex.Fullname = item.Fullname;
                                        customerIndex.Phone = item.MobilePhone;
                                        customerIndex.Email = item.Email;
                                        customerIndex.PhysicalID = item.PhysicalID;
                                        customerIndex.Meta = Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" + Helpers.convertToUnSign3.Init(String.IsNullOrEmpty(item.Email) ? "" : item.Email.ToLower()) + ";" + Helpers.convertToUnSign3.Init(item.MobilePhone.ToLower()) + ";" + Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" + Helpers.convertToUnSign3.Init(item.PhysicalID.ToLower());
                                        customerIndex.DataSource = "customer";
                                        customerIndex.CreatedAt = DateTime.Now;
                                        customerIndex.CreatedBy = currentUser.UserName;
                                        dbConn.Insert(customerIndex);

                                        // re-update order header
                                        var orderHeader = dbConn.Select<Deca_Order_Header>("PhysicalID={0} AND EmployeeID={1} AND PaymentStatus={2}", item.PhysicalID, item.EmployeeID, 0);
                                        if (orderHeader.Count() > 0)
                                        {
                                            foreach (var order in orderHeader)
                                            {
                                                order.CustomerID = item.CustomerID;
                                                order.MobilePhone = item.MobilePhone;
                                                order.UpdatedAt = DateTime.Now;
                                                order.UpdatedBy = currentUser.UserName;
                                                dbConn.Update(order);
                                            }
                                        }

                                        dbTrans.Commit();
                                        total++;
                                    }

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
                return Json(new { success = false, error = e.Message });
            }

            return Json(new { success = true });
        }

        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, Deca_Customer item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                int i = 1;
                var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                var dataStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerStatus");
                var dataCompany = dbConn.Select<Deca_Company>();
                var dataBranch = dbConn.Select<DC_Company_Branch>();

                eSheet.Cells[rownumber, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                eSheet.Cells[rownumber, i++].Value = item.CompanyName;
                eSheet.Cells[rownumber, i++].Value = dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID) == null ? "" : dataBranch.FirstOrDefault(a => a.BranchID == item.BranchID).BranchName;
                eSheet.Cells[rownumber, i++].Value = item.EmployeeID;
                eSheet.Cells[rownumber, i++].Value = item.Fullname;
                eSheet.Cells[rownumber, i++].Value = dataSex.FirstOrDefault(a => a.CodeID == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.CodeID == item.Sex).Description;
                eSheet.Cells[rownumber, i++].Value = item.Birthday;
                eSheet.Cells[rownumber, i++].Value = item.PhysicalID;
                eSheet.Cells[rownumber, i++].Value = item.Phone;
                eSheet.Cells[rownumber, i++].Value = item.MobilePhone;
                eSheet.Cells[rownumber, i++].Value = item.Email;
                eSheet.Cells[rownumber, i++].Value = item.Address;
                eSheet.Cells[rownumber, i++].Value = dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryID == item.HomeTown).TerritoryName;
                eSheet.Cells[rownumber, i++].Value = item.Department;
                eSheet.Cells[rownumber, i++].Value = item.Position;
                eSheet.Cells[rownumber, i++].Value = item.StartWorkingDate;
                eSheet.Cells[rownumber, i++].Value = dataEducation.FirstOrDefault(a => a.CodeID == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Education).Description;
                eSheet.Cells[rownumber, i++].Value = item.Income;
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.PayrollBank).BankName;
                eSheet.Cells[rownumber, i++].Value = item.CreditLimit;
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.CreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.CreditBank).BankName;
                eSheet.Cells[rownumber, i++].Value = dataStatus.FirstOrDefault(a => a.CodeID == item.Status) == null ? "" : dataEducation.FirstOrDefault(a => a.CodeID == item.Status).Description;
                eSheet.Cells[rownumber, i++].Value = item.Active;
                eSheet.Cells[rownumber, i++].Value = item.IsBlacklist;
                eSheet.Cells[rownumber, i++].Value = item.ExistedToken;
                eSheet.Cells[rownumber, i++].Value = item.OnlineAccount;
                eSheet.Cells[rownumber, i++].Value = item.CreatedAt;
                eSheet.Cells[rownumber, i++].Value = item.CreatedBy;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedAt;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedBy;
                eSheet.Cells[rownumber, i++].Value = error;
            }
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Excel"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public ActionResult ExportTemplate()
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Customer.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                    var dataStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerStatus");
                    var dataCompany = dbConn.Select<Deca_Company>();
                    var dataCardType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardType");
                    var dataCardRanking = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CardRanking");
                    var dataBranch = dbConn.Select<DC_Company_Branch>();
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["DecaCustomer"];

                    int rowData = 1;

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
                    foreach (Deca_Code_Master master in dataStatus)
                    {
                        dataSheet6.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet7 = excelPkg.Workbook.Worksheets["List6"];
                    foreach (Deca_Company master in dataCompany)
                    {
                        dataSheet7.Cells[rowData++, 1].Value = master.ShortName;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet8 = excelPkg.Workbook.Worksheets["List7"];
                    foreach (Deca_Code_Master master in dataCardType)
                    {
                        dataSheet8.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet9 = excelPkg.Workbook.Worksheets["List8"];
                    foreach (Deca_Code_Master master in dataCardRanking)
                    {
                        dataSheet9.Cells[rowData++, 1].Value = master.Description;
                    }
                    rowData = 2;
                    ExcelWorksheet dataSheet10 = excelPkg.Workbook.Worksheets["List9"];
                    foreach (DC_Company_Branch master in dataBranch)
                    {
                        dataSheet10.Cells[rowData++, 1].Value = master.BranchName;
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


    }
}