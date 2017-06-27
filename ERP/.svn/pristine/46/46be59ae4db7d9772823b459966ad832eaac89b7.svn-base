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
    public class PotentialCustomerController : CustomController
    {
        //
        // GET: /PotentialCustomer/
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
                    return Json(KendoApplyFilter.KendoData<Deca_Potential_Customer>(request));
                }
                return RedirectToAction("NoAccessRights", "Error");

            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Potential_Customer> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            item.IsNew = true;
                            item.Active = true;
                            item.CreatedAt = DateTime.Now;
                            item.CreatedBy = User.Identity.Name;
                            item.CompanyName = dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID) != null ? dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID).ShortName : "";
                            var check = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID={0}", item.PhysicalID);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng số chứng minh nhân dân!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            check = dbConn.FirstOrDefault<Deca_Potential_Customer>("Phone={0} AND CompanyID={1}", item.Phone, item.CompanyID);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng số điện thoại!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            check = dbConn.FirstOrDefault<Deca_Potential_Customer>("EmployeeID={0} AND CompanyID={1}", item.EmployeeID, item.CompanyID);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng mã nhân viên!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            dbConn.Insert(item);
                            //int Id = (int)dbConn.GetLastInsertId();
                            //item.Id = Id;
                            //item.CustomerID = "CUS" + DateTime.Now.ToString("yyMMdd") + string.Format("{0:00000}", Id);
                            //dbConn.Update(item);

                            var customerIndex = new Deca_Customer_Index();
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

                            var log = new Deca_Potential_Customer_Log();
                            log.CustomerID = item.CompanyID + ":" + item.EmployeeID;
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
                            item.CompanyName = dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID) != null ? dbConn.FirstOrDefault<Deca_Company>("Id = {0}", item.CompanyID).ShortName : "";
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = User.Identity.Name;
                            var check = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID={0} AND Id<>{1}", item.PhysicalID, item.Id);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng số chứng minh nhân dân!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            check = dbConn.FirstOrDefault<Deca_Potential_Customer>("Phone={0} AND CompanyID={1} AND Id<>{2}", item.Phone, item.CompanyID, item.Id);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng số điện thoại!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
                            check = dbConn.FirstOrDefault<Deca_Potential_Customer>("EmployeeID={0} AND CompanyID={1} AND Id<>{2}", item.EmployeeID, item.CompanyID, item.Id);
                            if (check != null)
                            {
                                ModelState.AddModelError("", "Trùng mã nhân viên!");
                                return Json(items.ToDataSourceResult(request, ModelState));
                            }
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
        public ActionResult Delete(string data)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    int i = 0;
                    string[] separators = { "@@" };
                    var listid = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var id in listid)
                    {
                        var item = dbConn.FirstOrDefault<Deca_Potential_Customer>("Id={0}", id);
                        if (item.CreatedBy == currentUser.UserName || currentUser.Groups.Count(a => a.Id.Equals(1)) > 0)
                        {
                            if (string.IsNullOrEmpty(item.CustomerID) && item.CreditCardStatus == "CCS001")
                            {
                                dbConn.Delete(item);
                                i++;
                                //delete index
                                dbConn.Delete<Deca_Customer_Index>(where: "PhysicalID='" + item.PhysicalID + "' AND DataSource='potentialCustomer'");
                            }
                        }
                    }
                    dbTrans.Commit();
                    return Json(new { success = true, message = i });
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    return Json(new { success = false, error = e.Message });
                }
            }
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
            int totalCols = oSheet.Dimension.End.Column;

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

        [HttpPost]
        public ActionResult ImportFromExcelNew()
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Potential_Customer.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["PotentialCustomer"];

                        if (oSheet.Dimension.End.Row > 10000)
                        {
                            return Json(new { success = false, error = "Chỉ được import tối đa 10,000 dòng" });
                        }

                        var data = WorksheetToDataTables(oSheet);

                        DateTime temp = DateTime.Parse("1900-01-01");

                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                        {
                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                            CompanyName = s["Công ty"] != null ? s["Công ty"].ToString() : null,
                            BranchID = s["Chi nhánh"] != null ? s["Chi nhánh"].ToString() : "",
                            EmployeeID = s["Mã nhân viên"] != null ? s["Mã nhân viên"].ToString() : null,
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
                            CreditLimit = s["Tín dụng"] != null && !string.IsNullOrEmpty(s["Tín dụng"].ToString()) ? double.Parse(s["Tín dụng"].ToString()) : 0,
                            Address = s["Địa chỉ"] != null ? s["Địa chỉ"].ToString() : null,
                            CreditCardStatus = s["Trạng thái mở thẻ"] != null ? s["Trạng thái mở thẻ"].ToString() : null,
                            SourceType = s["Nguồn"] != null ? s["Nguồn"].ToString() : null,
                            RequestCreditBank = s["Yêu cầu ngân hàng cấp thẻ"] != null ? s["Yêu cầu ngân hàng cấp thẻ"].ToString() : null,
                            IsForm = (s["Hồ sơ"] != null || s["Hồ sơ"].ToString() != "") ? Boolean.Parse(s["Hồ sơ"].ToString()) : false
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
                                        String.IsNullOrEmpty(s.PhysicalID)
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
                                var physicalID = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Potential_Customer");
                                var wrongPhysicalID = listPotential.Where(s => physicalID.Contains(s.PhysicalID));
                                foreach (var item in wrongPhysicalID)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "CMND tồn tại trong hệ thống");
                                    rownumber++;
                                }
                                listPotential = listPotential.Except(wrongPhysicalID).ToList();
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
                                //check exist sex
                                var company = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'CustomerGroup'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.CustomerGroup) && !string.IsNullOrEmpty(s.CustomerGroup));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Nhóm khách hàng không tồn tại trong hệ thống");
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
                                if (wrongCompany.Count() < 1)
                                    wrongCompany = listPotential.Where(s => !company.Contains(s.RequestCreditBank) && !string.IsNullOrEmpty(s.RequestCreditBank));

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
                                    writeErrorToFile(eSheet, rownumber, item, "Tỉnh không tồn tại trong hệ thống");
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
                                //check exist SourceType
                                var company = dbConn.GetFirstColumn<string>("SELECT Description FROM Deca_Code_Master WHERE [CodeType]='PotentialSourceType'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.SourceType) && !string.IsNullOrEmpty(s.SourceType));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Nguồn không tồn tại trong hệ thống");
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
                                //check exist CreditcardStatus
                                var company = dbConn.GetFirstColumn<string>("SELECT Description FROM Deca_Code_Master WHERE [CodeType]='CreditCardStatus'");

                                var wrongCompany = listPotential.Where(s => !company.Contains(s.CreditCardStatus) && !string.IsNullOrEmpty(s.CreditCardStatus));
                                foreach (var item in wrongCompany)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Trạng thái mở thẻ không tồn tại trong hệ thống");
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
                            var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                            var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                            var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");

                            foreach (var item in listPotential)
                            {
                                try
                                {
                                    item.IsNew = true;
                                    item.Status = "new";
                                    item.Active = true;
                                    item.PhysicalIDBank = "";
                                    item.CompanyID = dbConn.FirstOrDefault<Deca_Company>("ShortName = {0}", item.CompanyName) != null ? dbConn.FirstOrDefault<Deca_Company>("ShortName = {0}", item.CompanyName).Id : 0;
                                    item.BranchID = dbConn.FirstOrDefault<DC_Company_Branch>("BranchName = {0}", item.BranchID) != null ? dbConn.FirstOrDefault<DC_Company_Branch>("BranchName = {0}", item.BranchID).BranchID : "";
                                    item.HomeTown = dataCity.FirstOrDefault(a => a.TerritoryName == item.HomeTown) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryName == item.HomeTown).TerritoryID;
                                    item.Sex = dataSex.FirstOrDefault(a => a.Description == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.Description == item.Sex).CodeID;
                                    item.Education = dataEducation.FirstOrDefault(a => a.Description == item.Education) == null ? "" : dataEducation.FirstOrDefault(a => a.Description == item.Education).CodeID;
                                    item.PayrollBank = dataBank.FirstOrDefault(a => a.BankName == item.PayrollBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankName == item.PayrollBank).BankID;
                                    item.SourceType = dataSourceType.FirstOrDefault(a => a.Description == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.Description == item.SourceType).CodeID;
                                    item.CreditCardStatus = dataCreditCardStatus.FirstOrDefault(a => a.Description == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.Description == item.CreditCardStatus).CodeID;
                                    item.CustomerGroup = dataCustomerGroup.FirstOrDefault(a => a.Description == item.CustomerGroup) == null ? "" : dataCustomerGroup.FirstOrDefault(a => a.Description == item.CustomerGroup).CodeID;
                                    item.CreatedAt = DateTime.Now;
                                    item.RequestCreditBank = dataBank.FirstOrDefault(a => a.BankName == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankName == item.RequestCreditBank).BankID;
                                    item.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(item);

                                    var customerIndex = new Deca_Customer_Index();
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

                                    total++;
                                }
                                catch (Exception ex)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, ex.Message);
                                    rownumber++;
                                }
                            }

                            //save potential

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
        public ActionResult ImportFromExcelUpdate()
        {
            try
            {
                if (Request.Files["FileUploadUpdate"] != null && Request.Files["FileUploadUpdate"].ContentLength > 0)
                {
                    string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["FileUploadUpdate"].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUploadUpdate"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUploadUpdate"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUploadUpdate"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        int n = 1;

                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Potential_Customer.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["PotentialCustomer"];

                        if (oSheet.Dimension.End.Row > 10000)
                        {
                            return Json(new { success = false, error = "Chỉ được import tối đa 10,000 dòng" });
                        }

                        var data = WorksheetToDataTables(oSheet);

                        var colPhysical = false;
                        var colSource = false;
                        var colIsForm = false;
                        var colCompany = false;
                        var colBranch = false;
                        var colEmployee = false;
                        var colFullname = false;
                        var colSex = false;
                        var colPhone = false;
                        var colEmail = false;
                        var colAddress = false;
                        var colCity = false;
                        var colDepartment = false;
                        var colPosition = false;
                        var colCusGroup = false;
                        var colStartWDate = false;
                        var colEducation = false;
                        var colIncome = false;
                        var colCreditLimit = false;
                        var colRequestCreditBank = false;
                        var colPayrollBank = false;
                        var colCreditStatus = false;
                        DateTime temp = DateTime.Parse("1900-01-01");

                        foreach (var item in data.Columns)
                        {
                            if (item.ToString() == "CMND")
                            {
                                colPhysical = true;
                            }
                            if (item.ToString() == "Nguồn")
                            {
                                colSource = true;
                            }
                            if (item.ToString() == "Hồ sơ")
                            {
                                colIsForm = true;
                            }
                            if (item.ToString() == "Công ty")
                            {
                                colCompany = true;
                            }
                            if (item.ToString() == "Chi nhánh")
                            {
                                colBranch = true;
                            }
                            if (item.ToString() == "Mã nhân viên")
                            {
                                colEmployee = true;
                            }
                            if (item.ToString() == "Họ và tên")
                            {
                                colFullname = true;
                            }
                            if (item.ToString() == "Giới tính")
                            {
                                colSex = true;
                            }
                            if (item.ToString() == "Điện thoại")
                            {
                                colPhone = true;
                            }
                            if (item.ToString() == "Email")
                            {
                                colEmail = true;
                            }
                            if (item.ToString() == "Địa chỉ")
                            {
                                colAddress = true;
                            }
                            if (item.ToString() == "Tỉnh")
                            {
                                colCity = true;
                            }
                            if (item.ToString() == "Phòng ban")
                            {
                                colDepartment = true;
                            }
                            if (item.ToString() == "Chức vụ")
                            {
                                colPosition = true;
                            }
                            if (item.ToString() == "Nhóm khách hàng")
                            {
                                colCusGroup = true;
                            }
                            if (item.ToString() == "Ngày vào làm")
                            {
                                colStartWDate = true;
                            }
                            if (item.ToString() == "Trình độ")
                            {
                                colEducation = true;
                            }
                            if (item.ToString() == "Thu nhập")
                            {
                                colIncome = true;
                            }
                            if (item.ToString() == "Tín dụng")
                            {
                                colCreditLimit = true;
                            }
                            if (item.ToString() == "Yêu cầu ngân hàng cấp thẻ")
                            {
                                colRequestCreditBank = true;
                            }
                            if (item.ToString() == "Nhận lương qua NH")
                            {
                                colPayrollBank = true;
                            }
                            if (item.ToString() == "Trạng thái mở thẻ")
                            {
                                colCreditStatus = true;
                            }
                        }


                        if (colPhysical == false) // Bắt buộc phải có cột CMND
                        {
                            writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), "Không tìm thấy cột CMND");
                            rownumber++;
                        }
                        else
                        {

                            using (var dbConn = Helpers.OrmliteConnection.openConn())
                            {
                                var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                                var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                                var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                                var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                                var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                                var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                                var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");

                                var existPhys = dbConn.GetFirstColumn<string>("SELECT PhysicalID FROM Deca_Potential_Customer");
                                if (colSource == true)
                                {
                                    var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                    {
                                        PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                        SourceType = s["Nguồn"] != null ? s["Nguồn"].ToString() : null,
                                    }).ToList();

                                    var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                    if (n == 1)
                                    {
                                        //check exist SourceType
                                        var source = dbConn.GetFirstColumn<string>("SELECT Description FROM Deca_Code_Master WHERE [CodeType]='PotentialSourceType'");
                                        var wrongSource = listPotential.Where(s => !source.Contains(s.SourceType) && !string.IsNullOrEmpty(s.SourceType));
                                        foreach (var item in wrongSource)
                                        {
                                            writeErrorToFile(eSheet, rownumber, item, "Nguồn không tồn tại trong hệ thống");
                                            rownumber++;
                                        }
                                        listUpdate = listUpdate.Except(wrongSource).ToList();
                                        if (listUpdate.Count() > 0)
                                        {
                                            n = 1;
                                        }
                                        else
                                        {
                                            n = 0;
                                        }
                                    }

                                    // update potential:
                                    foreach (var item in listUpdate)
                                    {
                                        var SourceType = dataSourceType.FirstOrDefault(a => a.Description == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.Description == item.SourceType).CodeID;
                                        // Update potential
                                        if (!String.IsNullOrEmpty(SourceType))
                                        {
                                            string strPotential = @"UPDATE Deca_Potential_Customer  SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     SourceType ='" + SourceType + "'";
                                            strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                            total += dbConn.ExecuteSql(strPotential);
                                        }
                                    }
                                }

                                if (colIsForm == true)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            IsForm = s["Hồ sơ"] != null ? Boolean.Parse(s["Hồ sơ"].ToString()) : false,
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        // update isForm:
                                        foreach (var item in listUpdate)
                                        {
                                            // Update potential
                                            string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     IsForm ='" + item.IsForm + "'";
                                            strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                            total += dbConn.ExecuteSql(strPotential);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colCompany)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            CompanyName = s["Công ty"] != null ? s["Công ty"].ToString() : null,
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist company
                                            var company = dbConn.GetFirstColumn<string>("SELECT ShortName FROM Deca_Company");

                                            var wrongCompany = listPotential.Where(s => !company.Contains(s.CompanyName) && !string.IsNullOrEmpty(s.CompanyID.ToString()));
                                            foreach (var item in wrongCompany)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Doanh nghiệp không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listPotential.Except(wrongCompany).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }

                                        foreach (var item in listUpdate)
                                        {
                                            var CompanyID = dbConn.FirstOrDefault<Deca_Company>("ShortName = {0}", item.CompanyName);
                                            if (CompanyID != null)
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     CompanyID ='" + CompanyID.Id + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colBranch)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            BranchID = s["Chi nhánh"] != null ? s["Chi nhánh"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist company
                                            var branch = dbConn.GetFirstColumn<string>("SELECT BranchName FROM DC_Company_Branch");
                                            var wrongBranch = listPotential.Where(s => !branch.Contains(s.BranchID.Trim()) && !string.IsNullOrEmpty(s.BranchID));
                                            foreach (var item in wrongBranch)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Chi nhánh không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listPotential.Except(wrongBranch).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }

                                        foreach (var item in listUpdate)
                                        {
                                            var Branch = dbConn.FirstOrDefault<DC_Company_Branch>("BranchName = {0}", item.BranchID);
                                            // Update potential
                                            if (Branch != null)
                                            {
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     BranchID ='" + Branch.BranchID + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }
                                if (colEmployee)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            EmployeeID = s["Mã nhân viên"] != null ? s["Mã nhân viên"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist company
                                            var emp = dbConn.GetFirstColumn<string>("SELECT EmployeeID FROM Deca_Potential_Customer");
                                            var wrongEmployee = listPotential.Where(s => emp.Contains(s.EmployeeID.Trim()));
                                            foreach (var item in wrongEmployee)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Mã nhân viên tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listPotential.Except(wrongEmployee).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }

                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.EmployeeID))
                                            {
                                                if (item.EmployeeID == "N/A")
                                                {
                                                    string strPotential = @"UPDATE Deca_Potential_Customer SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     EmployeeID =''";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }
                                                else
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     EmployeeID ='" + item.EmployeeID + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }

                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colSex)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Sex = s["Giới tính"] != null ? s["Giới tính"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist sex
                                            var sex = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'Gender'");

                                            var wrongSex = listPotential.Where(s => !sex.Contains(s.Sex) && !string.IsNullOrEmpty(s.Sex));
                                            foreach (var item in wrongSex)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Giới tính không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listPotential.Except(wrongSex).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }
                                        foreach (var item in listUpdate)
                                        {
                                            var Sex = dataSex.FirstOrDefault(a => a.Description == item.Sex) == null ? "" : dataSex.FirstOrDefault(a => a.Description == item.Sex).CodeID;
                                            // Update potential
                                            string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     Sex ='" + Sex + "'";
                                            strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                            total = dbConn.ExecuteSql(strPotential);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }
                                if (colAddress)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Address = s["Địa chỉ"] != null ? s["Địa chỉ"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.Address))
                                            {
                                                if (item.Address == "N/A")
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                             Active = 1,
                                                                                                             [Address] =''";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }
                                                else
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                                  Active = 1,
                                                                                                                  [Address] =N'" + item.Address + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colCity)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            HomeTown = s["Tỉnh"] != null ? s["Tỉnh"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist Region
                                            var homeTown = dbConn.GetFirstColumn<string>("SELECT TerritoryName FROM DC_OCM_Territory WHERE [Level]='Province'");

                                            var wrongHomeTown = listPotential.Where(s => !homeTown.Contains(s.HomeTown) && !string.IsNullOrEmpty(s.HomeTown));
                                            foreach (var item in wrongHomeTown)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Tỉnh không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listPotential.Except(wrongHomeTown).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }

                                        foreach (var item in listUpdate)
                                        {
                                            var HomeTown = dataCity.FirstOrDefault(a => a.TerritoryName == item.HomeTown);
                                            if (HomeTown != null)
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [HomeTown] =N'" + HomeTown.TerritoryID + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colDepartment)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Department = s["Phòng ban"] != null ? s["Phòng ban"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.Department))
                                            {
                                                if (item.Department == "N/A")
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET    IsNew = 1,
                                                                                                                    Active = 1,
                                                                                                                    [Department] = ''";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }
                                                else
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET     IsNew = 1,
                                                                                                                     Active = 1,
                                                                                                                     [Department] =N'" + item.Department + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colPosition)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Position = s["Chức vụ"] != null ? s["Chức vụ"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.Position))
                                            {
                                                if (item.Position == "N/A")
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET    IsNew = 1,
                                                                                                                    Active = 1,
                                                                                                                    [Position] =N''";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }
                                                else
                                                {
                                                    // Update potential
                                                    string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [Position] =N'" + item.Position + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                    total = dbConn.ExecuteSql(strPotential);
                                                }
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colCusGroup)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            CustomerGroup = s["Nhóm khách hàng"] != null ? s["Nhóm khách hàng"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist Customer Group.
                                            var cusGroup = dbConn.GetFirstColumn<string>("SELECT [Description] FROM Deca_Code_Master WHERE CodeType = 'CustomerGroup'");

                                            var wrongCusGroup = listPotential.Where(s => !cusGroup.Contains(s.CustomerGroup) && !string.IsNullOrEmpty(s.CustomerGroup));
                                            foreach (var item in wrongCusGroup)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Nhóm khách hàng không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listUpdate.Except(wrongCusGroup).ToList();
                                            if (listPotential.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }
                                        foreach (var item in listUpdate)
                                        {
                                            var CustomerGroup = dataCustomerGroup.FirstOrDefault(a => a.Description == item.CustomerGroup);
                                            if (CustomerGroup != null)
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [CustomerGroup] =N'" + CustomerGroup.CodeID + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colStartWDate)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            StartWorkingDate = s["Ngày vào làm"] != null ? DateTime.Parse(s["Ngày vào làm"].ToString()) : DateTime.Parse("1900-01-01")
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                        foreach (var item in listUpdate)
                                        {

                                            // Update potential
                                            string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [StartWorkingDate] ='" + item.StartWorkingDate + "'";
                                            strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                            total = dbConn.ExecuteSql(strPotential);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }
                                if (colEducation)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Education = s["Trình độ"] != null ? s["Trình độ"].ToString() : ""
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                        foreach (var item in listUpdate)
                                        {
                                            var Education = dataEducation.FirstOrDefault(a => a.Description == item.Education);
                                            if (Education != null)
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [Education] ='" + Education.CodeID + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colIncome) // toi day
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Income = s["Thu nhập"] != null ? Double.Parse(s["Thu nhập"].ToString()) : 0
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                        foreach (var item in listUpdate)
                                        {
                                            // Update potential
                                            string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [Income] ='" + item.Income + "'";
                                            strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                            total = dbConn.ExecuteSql(strPotential);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colCreditLimit)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            CreditLimit = s["Tín dụng"] != null ? Double.Parse(s["Tín dụng"].ToString()) : 0
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));

                                        foreach (var item in listUpdate)
                                        {
                                            // Update potential
                                            string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [CreditLimit] ='" + item.CreditLimit + "'";
                                            strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                            total = dbConn.ExecuteSql(strPotential);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colRequestCreditBank)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            RequestCreditBank = s["Yêu cầu ngân hàng cấp thẻ"] != null ? s["Yêu cầu ngân hàng cấp thẻ"].ToString() : ""
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist BAnk
                                            var bank = dbConn.GetFirstColumn<string>("SELECT BankName FROM DC_Bank_Definition");

                                            var wrongBank = listUpdate.Where(s => !bank.Contains(s.RequestCreditBank) && !string.IsNullOrEmpty(s.RequestCreditBank));

                                            foreach (var item in wrongBank)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Ngân hàng không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listUpdate.Except(wrongBank).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }
                                        foreach (var item in listUpdate)
                                        {
                                            var RequestCreditBank = dataBank.FirstOrDefault(a => a.BankName == item.RequestCreditBank);
                                            // Update potential
                                            if (RequestCreditBank != null)
                                            {
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [RequestCreditBank] ='" + RequestCreditBank.BankID + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colPayrollBank)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            RequestCreditBank = s["Nhận lương qua NH"] != null ? s["Nhận lương qua NH"].ToString() : ""
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist BAnk
                                            var bank = dbConn.GetFirstColumn<string>("SELECT BankName FROM DC_Bank_Definition");

                                            var wrongBank = listUpdate.Where(s => !bank.Contains(s.PayrollBank) && !string.IsNullOrEmpty(s.PayrollBank));

                                            foreach (var item in wrongBank)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Ngân hàng không tồn tại trong hệ thống");
                                                rownumber++;
                                            }
                                            listUpdate = listUpdate.Except(wrongBank).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }
                                        foreach (var item in listUpdate)
                                        {
                                            var PayrollBank = dataBank.FirstOrDefault(a => a.BankName == item.PayrollBank);
                                            // Update potential
                                            if (PayrollBank != null)
                                            {
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET  IsNew = 1,
                                                                                                     Active = 1,
                                                                                                     [PayrollBank] ='" + PayrollBank.BankID + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        writeErrorToFile(eSheet, rownumber, new Deca_Potential_Customer(), ex.Message);
                                        rownumber++;
                                    }
                                }

                                if (colFullname)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Fullname = s["Họ và tên"] != null ? s["Họ và tên"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.Fullname))
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET IsNew = 1,
                                                                                                                 Active = 1,
                                                                                                                 FullName =N'" + item.Fullname + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);

                                                // update CustomerIndex
                                                var index = dbConn.Select<Deca_Customer_Index>("PhysicalID = {0} AND DataSource = 'potentialCustomer'", item.PhysicalID).FirstOrDefault();
                                                if (index != null)
                                                {
                                                    string meta = (!String.IsNullOrEmpty(item.Fullname) ? Helpers.convertToUnSign3.Init(item.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(index.Email) ? Helpers.convertToUnSign3.Init(index.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(index.Phone) ? Helpers.convertToUnSign3.Init(index.Phone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(index.PhysicalID.ToLower());
                                                    string strCustomerIndex = @"UPDATE Deca_Customer_Index   SET FullName =N'" + item.Fullname + "' ,Meta = '" + meta + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "') AND DataSource = 'potentialCustomer'";
                                                    dbConn.ExecuteSql(strCustomerIndex);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }

                                if (colPhone)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Phone = s["Điện thoại"] != null ? s["Điện thoại"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.Phone))
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET IsNew = 1,
                                                                                                                 Active = 1,
                                                                                                                 FullName =N'" + item.Phone + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);

                                                // update CustomerIndex
                                                var index = dbConn.Select<Deca_Customer_Index>("PhysicalID = {0} AND DataSource = 'potentialCustomer'", item.PhysicalID).FirstOrDefault();
                                                if (index != null)
                                                {
                                                    string meta = (!String.IsNullOrEmpty(index.Fullname) ? Helpers.convertToUnSign3.Init(index.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(index.Email) ? Helpers.convertToUnSign3.Init(index.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(index.PhysicalID.ToLower());
                                                    string strCustomerIndex = @"UPDATE Deca_Customer_Index   SET Phone =N'" + item.Phone + "' ,Meta = '" + meta + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "') AND DataSource = 'potentialCustomer'";
                                                    dbConn.ExecuteSql(strCustomerIndex);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }

                                if (colEmail)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            Email = s["Email"] != null ? s["Email"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        foreach (var item in listUpdate)
                                        {
                                            if (!String.IsNullOrEmpty(item.Email))
                                            {
                                                // Update potential
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET IsNew = 1,
                                                                                                                 Active = 1,
                                                                                                                 Email =N'" + item.Email + "'";
                                                strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);

                                                // update CustomerIndex
                                                var index = dbConn.Select<Deca_Customer_Index>("PhysicalID = {0} AND DataSource = 'potentialCustomer'", item.PhysicalID).FirstOrDefault();
                                                if (index != null)
                                                {
                                                    string meta = (!String.IsNullOrEmpty(index.Fullname) ? Helpers.convertToUnSign3.Init(index.Fullname.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "") + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) + ";" : "") + Helpers.convertToUnSign3.Init(index.PhysicalID.ToLower());
                                                    string strCustomerIndex = @"UPDATE Deca_Customer_Index   SET Phone =N'" + item.Phone + "' ,Meta = '" + meta + "'";
                                                    strPotential += ",UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "') AND DataSource = 'potentialCustomer'";
                                                    dbConn.ExecuteSql(strCustomerIndex);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }

                                if (colCreditStatus)
                                {
                                    try
                                    {
                                        var listPotential = data.AsEnumerable().Select(s => new Deca_Potential_Customer
                                        {
                                            PhysicalID = s["CMND"] != null ? s["CMND"].ToString() : null,
                                            CreditCardStatus = s["Trạng thái mở thẻ"] != null ? s["Trạng thái mở thẻ"].ToString() : "",
                                        }).ToList();

                                        var listUpdate = listPotential.Where(s => existPhys.Contains(s.PhysicalID));
                                        if (n == 1)
                                        {
                                            //check exist credit status:
                                            var status = dbConn.GetFirstColumn<string>("select Description from Deca_Code_Master where CodeID = 'CCS002'");
                                            var wrongStatus = listUpdate.Where(s => !status.Contains(s.CreditCardStatus) && !string.IsNullOrEmpty(s.CreditCardStatus));

                                            foreach (var item in wrongStatus)
                                            {
                                                writeErrorToFile(eSheet, rownumber, item, "Trạng thái mở thẻ không hợp lệ");
                                                rownumber++;
                                            }
                                            listUpdate = listUpdate.Except(wrongStatus).ToList();
                                            if (listUpdate.Count() > 0)
                                            {
                                                n = 1;
                                            }
                                            else
                                            {
                                                n = 0;
                                            }
                                        }
                                        foreach (var item in listUpdate)
                                        {
                                            var CreditStatus = dataCreditCardStatus.FirstOrDefault(a => a.Description == item.CreditCardStatus);
                                            // Update potential
                                            if (CreditStatus != null)
                                            {
                                                string strPotential = @"UPDATE Deca_Potential_Customer   SET    IsNew = 1,
                                                                                                                Active = 1,
                                                                                                                [CreditCardStatus] ='" + CreditStatus.CodeID + "'";
                                                strPotential += " ,DecaRequested = '" + DateTime.Now + "',UpdatedAt = '" + DateTime.Now + "',UpdatedBy = '" + currentUser.UserName + "' WHERE PhysicalID IN('" + item.PhysicalID + "')";
                                                total = dbConn.ExecuteSql(strPotential);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
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
        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Potential_Customer.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Potential_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Potential_Customer>();
                    var data2 = new DataSourceResult();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Potential_Customer>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Potential_Customer>("SELECT TOP 50000 * FROM Deca_Potential_Customer").ToList();
                        //data2 = KendoApplyFilter.KendoData<Deca_Potential_Customer>(request);
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
                        expenseSheet.Cells[rowData, i++].Value = dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType).Description;
                        expenseSheet.Cells[rowData, i++].Value = dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.IsForm;
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
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank).BankName;
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
                eSheet.Cells[rownumber, i++].Value = dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType) == null ? "" : dataSourceType.FirstOrDefault(a => a.CodeID == item.SourceType).Description;
                eSheet.Cells[rownumber, i++].Value = dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus) == null ? "" : dataCreditCardStatus.FirstOrDefault(a => a.CodeID == item.CreditCardStatus).Description;
                eSheet.Cells[rownumber, i++].Value = item.IsForm;
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
                eSheet.Cells[rownumber, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.RequestCreditBank).BankName;
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Potential_Customer.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Potential_Customer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var dataSex = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Gender");
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataEducation = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "Education");
                    var dataCompany = dbConn.Select<Deca_Company>();
                    var dataCreditCardStatus = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CreditCardStatus");
                    var dataSourceType = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "PotentialSourceType");
                    var dataCustomerGroup = dbConn.Select<Deca_Code_Master>("[CodeType] ={0}", "CustomerGroup");
                    var dataBranch = dbConn.Select<DC_Company_Branch>();
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["PotentialCustomer"];

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