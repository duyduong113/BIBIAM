using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Globalization;
//using ERPAPD.Infrastructure;
using System.Web;
using System.Data.OleDb;
using System.Dynamic;
using OfficeOpenXml;
using ERPAPD.Helpers;
using System.Threading.Tasks;
using DevTrends.MvcDonutCaching;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace ERPAPD.Controllers
{
    public class CustomerController : AssetsController
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Customer/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //this.parentAsset = Asset.GetAsset(1);
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                //ViewData["UserGroups"] = UGroup.GetAllUserGroups();

                ViewBag.listVip = DW_DC_Customer.GetGroupID();
                ViewBag.canEdit = currentUser.Groups.Select(g => g.Name).Contains("SuperAdmin") || currentUser.Groups.Select(g => g.Name).Contains("OPS") || currentUser.Groups.Select(g => g.Name).Contains("CustomerService");
                ViewBag.listClass = DC_ClassDefinition.GetList_ClassDefinitions();
                ViewBag.listOrg = DW_DC_Organization.GetOrganizationIDDW_DC_Organizations();
                ViewBag.listSSeniority = DC_Seniority.GetListDC_Senioritys();

                ViewBag.listType = DC_Ticket_Type.GetAllDC_Ticket_Types();
                ViewBag.listQueue = DC_Ticket_Queue.GetAllDC_Ticket_Queues();
                ViewBag.listPriority = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Ticket Priority'", "").OrderBy(s => s.CodeID);
                ViewBag.listImpact = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Ticket Impact'", "").OrderBy(s => s.CodeID);
                ViewBag.listCategory = DC_Ticket_Category.GetAllDC_Ticket_Categorys().Where(s => s.Status == "True");

                ViewBag.listDepartment = Deca_Department.GetAllDeca_Departments();
                ViewBag.listTeam = Deca_Team.GetAllDeca_Teams();
                ViewBag.listPosition = DC_Position.GetAllDC_Positions();
                ViewBag.BankName = Bank.GetAllBanks().Where(s => s.Active == true);
                ViewBag.BankBranch = Bank_Branch.GetAllBank_Branchs();
                return View();
            }
            else
            {
                ViewBag.AllowView = false;
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public class VIPGroup
        {
            public string Value { get; set; }
            public string Description { get; set; }
            public VIPGroup(string value, string description)
            {
                this.Value = value;
                this.Description = description;
            }
        }

        public ActionResult getListOrg()
        {
            try
            {
                var userId = currentUser.UserName;
                var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
                List<DW_DC_Organization> data = new List<DW_DC_Organization>();
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    data = DW_DC_Organization.GetOrganizationIDDW_DC_Organizations();
                }
                else
                {
                    data = (from c in DW_DC_Organization.GetOrganizationIDDW_DC_Organizations()
                            join lo in listOrg on c.OrganizationID equals lo.OrgID
                            where lo.UserID == userId
                            select c).ToList();
                }

                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                log.Error(e);
                return Json(new { success = false, data = e });
            }
        }

        //public ActionResult Customer_Read([DataSourceRequest]
        //                                  DataSourceRequest request,string org)
        //{
        //    var userId = currentUser.UserName;
        //    var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
        //    List<DW_DC_Customer> data = new List<DW_DC_Customer>();
        //    if (listOrg.Count() > 0)
        //    {
        //        if (listOrg.FirstOrDefault().OrgID == "All")
        //        {

        //            data = DW_DC_Customer.GetAllDW_DC_CustomersByOrg(org);

        //        }
        //        else
        //        {

        //            data = (from c in DW_DC_Customer.GetAllDW_DC_CustomersByOrg(org)
        //                        join lo in listOrg on c.OrganizationID equals lo.OrgID
        //                        where lo.UserID == userId
        //                        select c).ToList();

        //        }
        //    }
        //    return Json(data.ToDataSourceResult(request));
        //}

        public ActionResult Customer_ReadAll([DataSourceRequest]
                                          DataSourceRequest request, string OrganizationID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var userId = currentUser.UserName;
                var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
                List<DW_DC_Customer> data = new List<DW_DC_Customer>();
                if (listOrg.Count() > 0)
                {
                    if (listOrg.FirstOrDefault().OrgID == "All")
                    {
                        if (request.Filters.Any())
                        {
                            var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                            data = DW_DC_Customer.GetAllDW_DC_CustomersDynamicView(OrganizationID, where);
                        }
                        else
                        {
                            data = DW_DC_Customer.GetAllDW_DC_CustomersDynamicView(OrganizationID, "1=1");
                        }
                    }
                    else
                    {
                        if (request.Filters.Any())
                        {
                            var wheres = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.").Replace("'", "''");
                            data = (from c in DW_DC_Customer.GetAllDW_DC_CustomersDynamicView(OrganizationID, wheres)
                                    join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                                    where lo.UserID == userId
                                    select c).ToList();
                        }
                        else
                        {
                            data = (from c in DW_DC_Customer.GetAllDW_DC_CustomersDynamicView(OrganizationID, "1=1")
                                    join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                                    where lo.UserID == userId
                                    select c).ToList();
                        }
                    }
                }

                return Json(data.ToDataSourceResult(request));
            }
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult OrderListCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Order.GetOrderListOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult OrderItemCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Order_Item.GetOrderItemsOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public async Task<ActionResult> SettlementCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = await DW_DC_Order.GetSettlementOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult AirtimeCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Order.GetAirtimeOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public async Task<ActionResult> CashAdvanceCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = await DW_DC_Order.GetCashAdvanceOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public async Task<ActionResult> DepositCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = await DW_DC_Order.GetDepositOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult DiscountCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Order.GetDiscountOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult UnpayCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Order.GetUnpayOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult CustomerDetail_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Customer.GetAllDW_DC_CustomersByCus(customerID);
            return Json(data.ToDataSourceResult(request));
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult Ticket_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DC_Ticket.GetAllDC_Tickets_Customer(customerID);
            return Json(data.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customer_Update([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<DW_DC_Customer> customers)
        {
            if (asset.Update)
            {
                if (customers != null && ModelState.IsValid)
                {
                    foreach (var customer in customers)
                    {
                        var group = DC_Customer_Group.GetDC_Customer_Group(customer.CustomerID);
                        if (group != null)
                        {
                            if (!String.IsNullOrEmpty(customer.CustomerGroupID))
                            {
                                group.GroupID = customer.CustomerGroupID != null ? customer.CustomerGroupID : "";
                                group.RowLastUpdatedTime = DateTime.Now;
                                group.RowLastUpdatedUser = currentUser.UserName;
                                group.Update();


                            }
                            else
                            {

                                group.Delete();
                            }
                        }
                        else
                        {
                            DC_Customer_Group cg = new DC_Customer_Group();
                            cg.CustomerID = customer.CustomerID;
                            cg.GroupID = customer.CustomerGroupID != null ? customer.CustomerGroupID : "";
                            cg.RowCreatedTime = DateTime.Now;
                            cg.RowCreatedUser = currentUser.UserName;
                            cg.Save();


                        }

                        log.Info("Update vip customer group" + customer.CustomerID);
                        var metasn = DC_Customer_Meta.GetDC_Customer_Meta(customer.CustomerID, "SSeniority");
                        var customerMeta = DC_Customer_Meta.GetDC_Customer_Metas("[CustomerID]='" + customer.CustomerID + "'", "").ToList();
                        foreach (var c in customerMeta)
                        {
                            c.Delete();
                        }
                        DC_Customer_Meta cusMeta = new DC_Customer_Meta();
                        cusMeta.CustomerID = customer.CustomerID;
                        cusMeta.Factor = "added profile";
                        cusMeta.Value = customer.AddedProfile == true ? "true" : "false";
                        cusMeta.RowCreatedTime = DateTime.Now;
                        cusMeta.RowCreatedUser = currentUser.UserName;
                        cusMeta.Save();
                        log.Info("Update added profile:" + customer.CustomerID);



                        if (customer.CashAdvanceLimit != "" && customer.CashAdvanceLimit != null)
                        {
                            DC_Customer_Meta cusM = new DC_Customer_Meta();
                            cusM.CustomerID = customer.CustomerID;
                            cusM.Factor = "cashadvancelimit";
                            cusM.Value = customer.CashAdvanceLimit;
                            cusM.RowCreatedTime = DateTime.Now;
                            cusM.RowCreatedUser = currentUser.UserName;
                            cusM.Save();
                            log.Info("Update cash advance limit customer:" + customer.CustomerID);


                        }

                        if (customer.Note != "" && customer.Note != null)
                        {
                            DC_Customer_Meta cusM = new DC_Customer_Meta();
                            cusM.CustomerID = customer.CustomerID;
                            cusM.Factor = "note";
                            cusM.Value = customer.Note;
                            cusM.RowCreatedTime = DateTime.Now;
                            cusM.RowCreatedUser = currentUser.UserName;
                            cusM.Save();
                            log.Info("Update note:" + customer.CustomerID);

                        }
                        
                        if (customer.StartWorkingDate != null)
                        {
                            DC_Customer_Meta cusM = new DC_Customer_Meta();
                            cusM.CustomerID = customer.CustomerID;
                            cusM.Factor = "startworkingdate";
                            cusM.Value = customer.StartWorkingDate.ToString("yyyy-MM-dd");
                            cusM.RowCreatedTime = DateTime.Now;
                            cusM.RowCreatedUser = currentUser.UserName;
                            cusM.Save();
                            log.Info("Update start working date:" + customer.CustomerID);

                        }


                        DC_Customer_Meta business = new DC_Customer_Meta();
                        business.CustomerID = customer.CustomerID;
                        business.Factor = "business unit";
                        business.Value = customer.Businessunit == null ? "" : customer.Businessunit;
                        business.RowCreatedTime = DateTime.Now;
                        business.RowCreatedUser = currentUser.UserName;
                        business.Save();
                        log.Info("Update business unit:" + customer.CustomerID);



                        DC_Customer_Meta location = new DC_Customer_Meta();
                        location.CustomerID = customer.CustomerID;
                        location.Factor = "location";
                        location.Value = customer.Location == null ? "" : customer.Location;
                        location.RowCreatedTime = DateTime.Now;
                        location.RowCreatedUser = currentUser.UserName;
                        location.Save();
                        log.Info("Update location:" + customer.CustomerID);



                        DC_Customer_Meta clas = new DC_Customer_Meta();
                        clas.CustomerID = customer.CustomerID;
                        clas.Factor = "Class";
                        clas.Value = customer.ClassAuto == null ? "" : customer.ClassAuto;
                        clas.RowCreatedTime = DateTime.Now;
                        clas.RowCreatedUser = currentUser.UserName;
                        clas.Save();
                        log.Info("Update Class:" + customer.ClassName);


                        DC_Customer_Meta seni = new DC_Customer_Meta();
                        seni.CustomerID = customer.CustomerID;
                        seni.Factor = "SSeniority";
                        seni.Value = customer.SeniorityAuto == null ? "" : customer.SeniorityAuto;
                        seni.RowCreatedTime = DateTime.Now;
                        seni.RowCreatedUser = currentUser.UserName;
                        seni.Save();
                        log.Info("Update SSeniority:" + customer.SSeniority);

                        //var bank = DC_Customer_Banking.GetDC_Customer_Banking(customer.CustomerID);
                        //if (bank == null)
                        //{
                        //    DC_Customer_Banking banks = new DC_Customer_Banking();
                        //    banks.CustomerID = customer.CustomerID;
                        //    banks.BankName = !string.IsNullOrEmpty(customer.BankName) ? customer.BankName : "";
                        //    banks.BankBranch = !string.IsNullOrEmpty(customer.BankBranch) ? customer.BankBranch : "";
                        //    banks.BankAccount = !string.IsNullOrEmpty(customer.BankAccount) ? customer.BankAccount : "";
                        //    banks.RowCreatedTime = DateTime.Now;
                        //    banks.RowCreatedUser = currentUser.UserName;
                        //    banks.Save();
                        //}
                        //else
                        //{
                        //    bank.CustomerID = customer.CustomerID;
                        //    bank.BankName = !string.IsNullOrEmpty(customer.BankName) ? customer.BankName : "";
                        //    bank.BankBranch = !string.IsNullOrEmpty(customer.BankBranch) ? customer.BankBranch : "";
                        //    bank.BankAccount = !string.IsNullOrEmpty(customer.BankAccount) ? customer.BankAccount : "";
                        //    bank.RowLastUpdatedTime = DateTime.Now;
                        //    bank.RowLastUpdatedUser = currentUser.UserName;
                        //    bank.Update();
                        //}
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(customers.ToDataSourceResult(request, ModelState));
            }

            return Json(customers.ToDataSourceResult(request, ModelState));
        }

        [ExportCache]
        public FileResult Export([DataSourceRequest]
                                 DataSourceRequest request, string OrganizationID)
        {
            if (asset.Export)
            {
                var userId = currentUser.UserName;
                var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
                List<DW_DC_Customer> d = new List<DW_DC_Customer>();
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                        d = DW_DC_Customer.GetAllDW_DC_CustomersDynamicView(OrganizationID, where);
                    }
                }
                else
                {
                    if (request.Filters.Any())
                    {
                        var wheres = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.").Replace("'", "''");
                        d = (from c in DW_DC_Customer.GetAllDW_DC_CustomersDynamicView(OrganizationID, wheres)
                             join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                             where lo.UserID == userId
                             select c).ToList();
                    }
                }

                var listCustomer = d;
                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = listCustomer.ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_Customer Master.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);


                //Create new Excel sheet
                var sheet = workbook.GetSheet("Customer Master");
                var sheetSeniority = workbook.GetSheet("Seniority");
                var sheetClass = workbook.GetSheet("Class");
                var sheetBankName = workbook.GetSheet("BankName");
                var sheetBankBranch = workbook.GetSheet("BankBranch");

                int rowIndexSeniority = 1;
                var listSeniority = DC_Seniority.GetListDC_Senioritys().ToList();
                foreach (DC_Seniority item in listSeniority)
                {
                    int i = 0;
                    //Create a new row
                    var row = sheetSeniority.CreateRow(rowIndexSeniority++);
                    row.CreateCell(i++).SetCellValue(item.SeniorityID + ": " + item.Seniority);
                }


                int rowIndexClass = 1;
                var listClass = DC_ClassDefinition.GetList_ClassDefinitions().ToList();
                foreach (DC_ClassDefinition item in listClass)
                {
                    int i = 0;
                    //Create a new row
                    var row = sheetClass.CreateRow(rowIndexClass++);
                    row.CreateCell(i++).SetCellValue(item.ClassID + ": " + item.ClassName);
                }
                int rowIndexBankName = 1;
                var listBankName = Bank.GetAllBanks();
                foreach (Bank item in listBankName)
                {
                    int i = 0;
                    var row = sheetBankName.CreateRow(rowIndexBankName++);
                    row.CreateCell(i++).SetCellValue(item.BankName);
                }
                int rowIndexBankBranch = 1;
                var listBankBranch = Bank_Branch.GetAllBank_Branchs();
                foreach (Bank_Branch item in listBankBranch)
                {
                    int i = 0;
                    var row = sheetBankBranch.CreateRow(rowIndexBankBranch++);
                    row.CreateCell(i++).SetCellValue(item.BankBranchName);
                }
                int rowNumber = 1;

                var canExportAll = currentUser.Groups.Select(g => g.Name).Contains("SuperAdmin") || currentUser.Groups.Select(g => g.Name).Contains("OPS");
                //Populate the sheet with values from the grid data
                foreach (DW_DC_Customer data in datas)
                {
                    if (!canExportAll)
                    {
                        data.Email = "No Access";
                        data.Salary = 0;
                        data.SalaryReal = "No Access";
                        data.Phone = "No Access";
                        data.PhoneContact1 = "No Access";
                        data.PhoneContact2 = "No Access";
                    }
                    int i = 0;
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    //Set values for the cells
                    row.CreateCell(i++).SetCellValue(data.CustomerID);
                    row.CreateCell(i++).SetCellValue(data.Name);
                    row.CreateCell(i++).SetCellValue(data.CustomerGroupID);
                    row.CreateCell(i++).SetCellValue(data.AddedProfile);
                    //row.CreateCell(i++).SetCellValue(data.CashAdvanceLimit);
                    if (data.StartWorkingDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        row.CreateCell(i++).SetCellValue(data.StartWorkingDate);
                    }
                    else
                    {
                        row.CreateCell(i++).SetCellValue("");
                    }

                    row.CreateCell(i++).SetCellValue(data.Note);
                    row.CreateCell(i++).SetCellValue(data.SCredit);
                    row.CreateCell(i++).SetCellValue(data.SDueLimit);
                    row.CreateCell(i++).SetCellValue(data.PhysicalID);
                    row.CreateCell(i++).SetCellValue(data.Issuer);
                    row.CreateCell(i++).SetCellValue(data.XAccountID);
                    row.CreateCell(i++).SetCellValue(data.EmployeeID);
                    row.CreateCell(i++).SetCellValue(data.CreditLimit);
                    row.CreateCell(i++).SetCellValue(data.DueLimit);
                    row.CreateCell(i++).SetCellValue(data.RunningBalance);
                    row.CreateCell(i++).SetCellValue(data.SettledBalance);
                    row.CreateCell(i++).SetCellValue(data.DueBalance);
                    row.CreateCell(i++).SetCellValue(data.Status);
                    row.CreateCell(i++).SetCellValue(data.ActualStatus);
                    row.CreateCell(i++).SetCellValue(data.Gender);
                    row.CreateCell(i++).SetCellValue(data.MobilePhone);
                    row.CreateCell(i++).SetCellValue(data.Email);
                    row.CreateCell(i++).SetCellValue(data.BankName);
                    row.CreateCell(i++).SetCellValue(data.BankBranch);
                    row.CreateCell(i++).SetCellValue(data.BankAccount);
                    row.CreateCell(i++).SetCellValue(data.MaritalStatus);
                    row.CreateCell(i++).SetCellValue(data.Position);
                    row.CreateCell(i++).SetCellValue(data.Department);
                    row.CreateCell(i++).SetCellValue(data.Salary);
                    row.CreateCell(i++).SetCellValue(data.OrganizationID);
                    row.CreateCell(i++).SetCellValue(data.BD);
                    row.CreateCell(i++).SetCellValue(data.OrgContractStatus);
                    //row.CreateCell(i++).SetCellValue(data.OrgEmployee);
                    row.CreateCell(i++).SetCellValue(data.CreatedAt);
                    row.CreateCell(i++).SetCellValue(data.ModifiedAt);
                    row.CreateCell(i++).SetCellValue(data.ActivatedAt);
                    row.CreateCell(i++).SetCellValue(data.ClosedAt);
                    row.CreateCell(i++).SetCellValue(data.LastLoginDate);
                    //minhtc
                    row.CreateCell(i++).SetCellValue(data.ProfileCode);
                    row.CreateCell(i++).SetCellValue(data.Sex);
                    row.CreateCell(i++).SetCellValue(data.NaneDC);
                    row.CreateCell(i++).SetCellValue(data.DateOfBirth);
                    row.CreateCell(i++).SetCellValue(data.PlaceOfBirth);
                    row.CreateCell(i++).SetCellValue(data.Nationality);
                    row.CreateCell(i++).SetCellValue(data.Residence);
                    row.CreateCell(i++).SetCellValue(data.AddressProvided);
                    row.CreateCell(i++).SetCellValue(data.DateProvided);
                    row.CreateCell(i++).SetCellValue(data.EmailPerson);
                    row.CreateCell(i++).SetCellValue(data.EmailCompany);
                    row.CreateCell(i++).SetCellValue(data.ResidenceAddress);
                    row.CreateCell(i++).SetCellValue(data.ResidenceWard);
                    row.CreateCell(i++).SetCellValue(data.ResidenceDistrict);
                    row.CreateCell(i++).SetCellValue(data.ResidenceCity);
                    row.CreateCell(i++).SetCellValue(data.ResidenceHomePhone);
                    row.CreateCell(i++).SetCellValue(data.CurrentAddress);
                    row.CreateCell(i++).SetCellValue(data.CurrentWard);
                    row.CreateCell(i++).SetCellValue(data.CurrentDistrict);
                    row.CreateCell(i++).SetCellValue(data.CurrentCity);
                    row.CreateCell(i++).SetCellValue(data.CurrentHome);
                    row.CreateCell(i++).SetCellValue(data.ResidenceTime);
                    row.CreateCell(i++).SetCellValue(data.NumberOfDependents);
                    row.CreateCell(i++).SetCellValue(data.Education);
                    row.CreateCell(i++).SetCellValue(data.HomeOwnership);
                    row.CreateCell(i++).SetCellValue(data.PropertyOwnership);
                    row.CreateCell(i++).SetCellValue(data.PrimarySchool);
                    row.CreateCell(i++).SetCellValue(data.MotherName);
                    row.CreateCell(i++).SetCellValue(data.FrequencyOnline);
                    row.CreateCell(i++).SetCellValue(data.DevicesOnline);
                    row.CreateCell(i++).SetCellValue(data.Company);
                    row.CreateCell(i++).SetCellValue(data.StaffCode);
                    row.CreateCell(i++).SetCellValue(data.Parts);
                    row.CreateCell(i++).SetCellValue(data.TypeOfContract);
                    row.CreateCell(i++).SetCellValue(data.RemainingTime);
                    row.CreateCell(i++).SetCellValue(data.TotalIncome);
                    row.CreateCell(i++).SetCellValue(data.NameContact1);
                    row.CreateCell(i++).SetCellValue(data.RelationshipContact1);
                    row.CreateCell(i++).SetCellValue(data.PhoneContact1);
                    row.CreateCell(i++).SetCellValue(data.NameContact2);
                    row.CreateCell(i++).SetCellValue(data.RelationshipContact2);
                    row.CreateCell(i++).SetCellValue(data.PhoneContact2);
                    row.CreateCell(i++).SetCellValue(data.IntroducedTheName);
                    row.CreateCell(i++).SetCellValue(data.IntroducedThePhone);
                    row.CreateCell(i++).SetCellValue(data.IntroducedThePhysicalID);
                    row.CreateCell(i++).SetCellValue(data.UserLogin);
                    row.CreateCell(i++).SetCellValue(data.StatusLogin);
                    row.CreateCell(i++).SetCellValue(data.IsInValidYearBorn);
                    row.CreateCell(i++).SetCellValue(data.IsComplete);
                    row.CreateCell(i++).SetCellValue(data.IsNeedCheck);
                    row.CreateCell(i++).SetCellValue(data.IsRecieve);
                    row.CreateCell(i++).SetCellValue(data.UserInput);
                    row.CreateCell(i++).SetCellValue(data.SalaryReal);
                    row.CreateCell(i++).SetCellValue(data.WorkingYears);
                    row.CreateCell(i++).SetCellValue(data.Note2);
                }
                string fname = currentUser.UserName + " DC_Customer Master_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                if (ExportCache.CachResult(workbook, fname))
                {
                    return null;
                }

                //Write the workbook to a memory stream
                //MemoryStream output = new MemoryStream();
                //workbook.Write(output);

                //Return the result to the end user
                log.Info("Export Customer");
                return File(ExportCache.FilePath(fname), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer Master_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                return null;     
            }
        }


        public async Task<FileResult> ExportDetail(string CustomerID)
        {
            if (asset.Export)
            {
                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Customer_Detail Master.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                var customerinfo = DW_DC_Customer.GetDW_DC_Customer(CustomerID);

                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Settlement"];
                var listdata = await DW_DC_Order.GetSettlementOfCustomer(CustomerID);
                int rowData = 2;
                dataSheet.Cells[1, 2].Value = CustomerID;
                dataSheet.Cells[1, 4].Value = customerinfo.Name;
                foreach (DW_DC_Order data in listdata)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.PeriodID;
                    dataSheet.Cells[rowData, i++].Value = data.SettlementDate;
                    dataSheet.Cells[rowData, i++].Value = data.ActualSettlementDate;
                    dataSheet.Cells[rowData, i++].Value = data.SettlementAmount;
                    dataSheet.Cells[rowData, i++].Value = data.ConfirmAmount;
                    if (data.OrgPaymentDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.OrgPaymentDate;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    if (data.PaymentDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.PaymentDate;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.PayInPlan;
                    dataSheet.Cells[rowData, i++].Value = data.PayOutPlan;
                    dataSheet.Cells[rowData, i++].Value = data.RepayNote;
                    dataSheet.Cells[rowData, i++].Value = data.OPSNote;
                    dataSheet.Cells[rowData, i++].Value = data.ORGCollection;
                    dataSheet.Cells[rowData, i++].Value = data.SendSMS;
                    dataSheet.Cells[rowData, i++].Value = data.BankToolID;

                }


                dataSheet = excelPkg.Workbook.Worksheets["Orders"];
                var listdata2 = DW_DC_Order.GetOrderListOfCustomer(CustomerID);
                rowData = 1;

                foreach (DW_DC_Order data in listdata2)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.OrderID;
                    dataSheet.Cells[rowData, i++].Value = data.Amount;
                    dataSheet.Cells[rowData, i++].Value = data.Status;
                    if (data.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.CreatedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    if (data.ModifiedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.ModifiedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                }


                dataSheet = excelPkg.Workbook.Worksheets["OrderItems"];
                var listdata3 = DW_DC_Order_Item.GetOrderItemsOfCustomer(CustomerID);
                rowData = 1;

                foreach (DW_DC_Order_Item data in listdata3)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.OrderID;
                    dataSheet.Cells[rowData, i++].Value = data.MSIN;
                    dataSheet.Cells[rowData, i++].Value = data.Name;
                    dataSheet.Cells[rowData, i++].Value = data.Quantity;
                    dataSheet.Cells[rowData, i++].Value = data.UnitPrice;
                    dataSheet.Cells[rowData, i++].Value = data.Amount;
                    dataSheet.Cells[rowData, i++].Value = data.Status;
                    if (data.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.CreatedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    if (data.ModifiedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.ModifiedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }

                }


                dataSheet = excelPkg.Workbook.Worksheets["Airtime"];
                var listdata4 = DW_DC_Order.GetAirtimeOfCustomer(CustomerID);
                rowData = 1;

                foreach (DW_DC_Order data in listdata4)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.Type;
                    dataSheet.Cells[rowData, i++].Value = data.XTxnID;
                    dataSheet.Cells[rowData, i++].Value = data.XAccountID;
                    dataSheet.Cells[rowData, i++].Value = data.Amount;
                    dataSheet.Cells[rowData, i++].Value = data.Description;
                    dataSheet.Cells[rowData, i++].Value = data.Status;
                    if (data.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.CreatedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    if (data.ModifiedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.ModifiedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                }

                dataSheet = excelPkg.Workbook.Worksheets["Cash Advance"];
                var listdata5 = await DW_DC_Order.GetCashAdvanceOfCustomer(CustomerID);
                rowData = 1;

                foreach (DW_DC_Order data in listdata5)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.Type;
                    dataSheet.Cells[rowData, i++].Value = data.XTxnID;
                    dataSheet.Cells[rowData, i++].Value = data.XAccountID;
                    dataSheet.Cells[rowData, i++].Value = data.Amount;
                    dataSheet.Cells[rowData, i++].Value = data.Description;
                    dataSheet.Cells[rowData, i++].Value = data.Status;
                    if (data.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.CreatedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    if (data.ModifiedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.ModifiedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }

                }


                dataSheet = excelPkg.Workbook.Worksheets["Deposit"];
                var listdata6 = await DW_DC_Order.GetDepositOfCustomer(CustomerID);
                rowData = 1;

                foreach (DW_DC_Order data in listdata6)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.Type;
                    dataSheet.Cells[rowData, i++].Value = data.XTxnID;
                    dataSheet.Cells[rowData, i++].Value = data.XAccountID;
                    dataSheet.Cells[rowData, i++].Value = data.Amount;
                    dataSheet.Cells[rowData, i++].Value = data.Description;
                    dataSheet.Cells[rowData, i++].Value = data.Status;
                    if (data.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.CreatedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    if (data.ModifiedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.ModifiedAt;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }

                }

                dataSheet = excelPkg.Workbook.Worksheets["CustomerDetail"];
                var listdata7 = DW_DC_Customer.GetAllDW_DC_CustomersByCus(CustomerID);
                rowData = 1;

                foreach (DW_DC_Customer data in listdata7)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.NaneDC;
                    dataSheet.Cells[rowData, i++].Value = data.Note2;

                }


                dataSheet = excelPkg.Workbook.Worksheets["Customer"];
                var listdata8 = DW_DC_Customer.GetAllDW_DC_Customers_Export(CustomerID);
                rowData = 1;

                foreach (DW_DC_Customer data in listdata8)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.CustomerID;
                    dataSheet.Cells[rowData, i++].Value = data.FullName;
                    dataSheet.Cells[rowData, i++].Value = data.CustomerGroupID;
                    dataSheet.Cells[rowData, i++].Value = data.AddedProfile;
                    dataSheet.Cells[rowData, i++].Value = data.CashAdvanceLimit;
                    if (data.StartWorkingDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.StartWorkingDate;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.Businessunit;
                    dataSheet.Cells[rowData, i++].Value = data.Location;
                    dataSheet.Cells[rowData, i++].Value = data.Note;
                    dataSheet.Cells[rowData, i++].Value = data.PhysicalID;
                    dataSheet.Cells[rowData, i++].Value = data.Issuer;
                    dataSheet.Cells[rowData, i++].Value = data.XAccountID;
                    dataSheet.Cells[rowData, i++].Value = data.EmployeeID;
                    dataSheet.Cells[rowData, i++].Value = data.CreditLimit;
                    dataSheet.Cells[rowData, i++].Value = data.DueLimit;
                    dataSheet.Cells[rowData, i++].Value = data.RunningBalance;
                    dataSheet.Cells[rowData, i++].Value = data.SettledBalance;
                    dataSheet.Cells[rowData, i++].Value = data.DueBalance;
                    dataSheet.Cells[rowData, i++].Value = data.Status;
                    dataSheet.Cells[rowData, i++].Value = data.ActualStatus;
                    dataSheet.Cells[rowData, i++].Value = data.Gender;
                    dataSheet.Cells[rowData, i++].Value = data.MobilePhone;
                    dataSheet.Cells[rowData, i++].Value = data.Email;
                    dataSheet.Cells[rowData, i++].Value = data.MaritalStatus;
                    dataSheet.Cells[rowData, i++].Value = data.Position;
                    dataSheet.Cells[rowData, i++].Value = data.Salary;
                    dataSheet.Cells[rowData, i++].Value = data.OrganizationID;
                    dataSheet.Cells[rowData, i++].Value = data.BD;
                    dataSheet.Cells[rowData, i++].Value = data.OrgContractStatus;
                    dataSheet.Cells[rowData, i++].Value = data.OrgEmployee;
                    dataSheet.Cells[rowData, i++].Value = data.CreatedAt;
                    dataSheet.Cells[rowData, i++].Value = data.ModifiedAt;
                    dataSheet.Cells[rowData, i++].Value = data.ActivatedAt;
                    dataSheet.Cells[rowData, i++].Value = data.ClosedAt;
                    dataSheet.Cells[rowData, i++].Value = data.LastLoginDate;

                }
                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                //Return the result to the end user
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer_Detail Master_" + CustomerID + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer_Detail Master_" + CustomerID + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }



        public FileResult ExportError([DataSourceRequest]
                                 DataSourceRequest request, string listCusId)
        {
            if (asset.Export)
            {
                var userId = currentUser.UserName;
                var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
                List<DW_DC_Customer> d = new List<DW_DC_Customer>();
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    d = DW_DC_Customer.GetAllDW_DC_Customers().Where(o => listCusId.Contains(o.CustomerID)).ToList();
                }
                else
                {
                    d = (from c in DW_DC_Customer.GetAllDW_DC_Customers()
                         join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                         where lo.UserID == userId && listCusId.Contains(c.CustomerID)
                         select c).ToList();
                }

                var listCustomer = d;
                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = listCustomer.ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_Customer Master.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("Customer Master");

                int rowNumber = 1;

                //Populate the sheet with values from the grid data
                foreach (DW_DC_Customer data in datas)
                {
                    int i = 0;
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    //Set values for the cells
                    row.CreateCell(i++).SetCellValue(data.CustomerID);
                    row.CreateCell(i++).SetCellValue(data.Name);
                    row.CreateCell(i++).SetCellValue(data.CustomerGroupID);
                    row.CreateCell(i++).SetCellValue(data.AddedProfile);
                    row.CreateCell(i++).SetCellValue(data.CashAdvanceLimit);
                    if (data.StartWorkingDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        row.CreateCell(i++).SetCellValue(data.StartWorkingDate);
                    }
                    else
                    {
                        row.CreateCell(i++).SetCellValue("");
                    }
                    row.CreateCell(i++).SetCellValue(data.Note);//thieu xaccount id//
                    row.CreateCell(i++).SetCellValue(data.XAccountID);
                    row.CreateCell(i++).SetCellValue(data.EmployeeID);
                    row.CreateCell(i++).SetCellValue(data.CreditLimit);
                    row.CreateCell(i++).SetCellValue(data.RunningBalance);
                    row.CreateCell(i++).SetCellValue(data.SettledBalance);
                    row.CreateCell(i++).SetCellValue(data.DueBalance);
                    row.CreateCell(i++).SetCellValue(data.Status);
                    row.CreateCell(i++).SetCellValue(data.ActualStatus);
                    row.CreateCell(i++).SetCellValue(data.Gender);
                    row.CreateCell(i++).SetCellValue(data.MobilePhone);
                    row.CreateCell(i++).SetCellValue(data.Email);
                    row.CreateCell(i++).SetCellValue(data.MaritalStatus);
                    row.CreateCell(i++).SetCellValue(data.Position);
                    row.CreateCell(i++).SetCellValue(data.Salary);
                    row.CreateCell(i++).SetCellValue(data.OrganizationID);
                    row.CreateCell(i++).SetCellValue(data.BD);
                    row.CreateCell(i++).SetCellValue(data.OrgContractStatus);
                    row.CreateCell(i++).SetCellValue(data.OrgEmployee);
                    row.CreateCell(i++).SetCellValue(data.CreatedAt);
                    row.CreateCell(i++).SetCellValue(data.ModifiedAt);
                    row.CreateCell(i++).SetCellValue(data.ActivatedAt);
                    row.CreateCell(i++).SetCellValue(data.ClosedAt);
                    row.CreateCell(i++).SetCellValue(data.LastLoginDate);
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                workbook.Write(output);

                //Return the result to the end user
                log.Info("Export Customer");
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer Master_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer Master_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }

        [HttpPost]
        public ActionResult ImportFromExcel()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<DW_DC_Customer> listData = new List<DW_DC_Customer>();
                    int total = 0;
                    if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                    {
                        string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
                            string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);

                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);

                            Request.Files["FileUpload"].SaveAs(fileLocation);
                            string excelConnectionString = string.Empty;

                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            //if (fileExtension == ".xls")
                            //{
                            //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            //}
                            ////connection String for xlsx file format.
                            //else if (fileExtension == ".xlsx")
                            //{
                            //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //}

                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();

                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            DataSet ds = new DataSet();

                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }

                            var existcusgroup = false;
                            var existaddedProfile = false;
                            var existcashadvancelimit = false;
                            var existnote = false;
                            var existstartworkingdate = false;
                            var existbusiness = false;
                            var existlocation = false;
                            var existclass = false;
                            var existseniority = false;
                            var existbankname = false;
                            var existbankbranch = false;
                            var existbankaccount = false;



                            foreach (var item in ds.Tables[0].Columns)
                            {
                                if (item.ToString() == "Customer Group")
                                {
                                    existcusgroup = true;
                                }
                                if (item.ToString() == "Added Profile")
                                {
                                    existaddedProfile = true;
                                }
                                if (item.ToString() == "CashAdvanceLimit")
                                {
                                    existcashadvancelimit = true;
                                }
                                if (item.ToString() == "Note")
                                {
                                    existnote = true;
                                }
                                if (item.ToString() == "StartWorkingDate")
                                {
                                    existstartworkingdate = true;
                                }

                                if (item.ToString() == "Business Unit")
                                {
                                    existbusiness = true;
                                }
                                if (item.ToString() == "Location")
                                {
                                    existlocation = true;
                                }

                                if (item.ToString() == "Class")
                                {
                                    existclass = true;
                                }
                                if (item.ToString() == "Seniority")
                                {
                                    existseniority = true;
                                }
                                if (item.ToString() == "BankName")
                                {
                                    existbankname = true;
                                }
                                if (item.ToString() == "BankBranch")
                                {
                                    existbankbranch = true;
                                }
                                if (item.ToString() == "BankAccount")
                                {
                                    existbankaccount = true;
                                }
                            }
                            DateTime temp;


                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string customerId = ds.Tables[0].Rows[i]["CustomerID"].ToString();
                                try
                                {
                                    if (!String.IsNullOrEmpty(customerId))
                                    {
                                        if (existcusgroup)
                                        {
                                            var group = DC_Customer_Group.GetDC_Customer_Group(customerId);
                                            string userGroup = ds.Tables[0].Rows[i]["Customer Group"].ToString();
                                            if (group != null)
                                            {

                                                if (!String.IsNullOrEmpty(userGroup))
                                                {
                                                    group.GroupID = userGroup.ToUpper();
                                                    group.RowLastUpdatedTime = DateTime.Now;
                                                    group.RowLastUpdatedUser = currentUser.UserName;
                                                    group.Update();
                                                }
                                                else
                                                {

                                                    group.Delete();
                                                }
                                            }
                                            else
                                            {
                                                if (!String.IsNullOrEmpty(customerId) && !String.IsNullOrEmpty(userGroup) && existcusgroup)
                                                {
                                                    DC_Customer_Group g = new DC_Customer_Group();
                                                    g.CustomerID = customerId;
                                                    g.GroupID = userGroup.ToUpper();
                                                    g.RowCreatedTime = DateTime.Now;
                                                    g.RowCreatedUser = currentUser.UserName;
                                                    g.Save();

                                                }
                                            }
                                            log.Info("Update vip customer group" + customerId);
                                        }

                                        if (existaddedProfile)
                                        {
                                            var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "added profile");

                                            if (da != null)
                                            {
                                                da.Delete();
                                            }

                                            string addedProfile = ds.Tables[0].Rows[i]["Added Profile"].ToString().ToLower();
                                            DC_Customer_Meta cusMeta = new DC_Customer_Meta();
                                            cusMeta.CustomerID = customerId;
                                            cusMeta.Factor = "added profile";
                                            cusMeta.Value = addedProfile == "true" ? "true" : "false";
                                            cusMeta.RowCreatedTime = DateTime.Now;
                                            cusMeta.RowCreatedUser = currentUser.UserName;
                                            cusMeta.Save();
                                            log.Info("Update added profile:" + customerId);

                                        }

                                        if (existcashadvancelimit)
                                        {
                                            var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "cashadvancelimit");

                                            if (da != null)
                                            {
                                                da.Delete();
                                            }

                                            string cashadvancelimit = ds.Tables[0].Rows[i]["CashAdvanceLimit"].ToString();
                                            if (!String.IsNullOrEmpty(cashadvancelimit))
                                            {
                                                DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                cusM.CustomerID = customerId;
                                                cusM.Factor = "cashadvancelimit";
                                                cusM.Value = cashadvancelimit;
                                                cusM.RowCreatedTime = DateTime.Now;
                                                cusM.RowCreatedUser = currentUser.UserName;
                                                cusM.Save();
                                                log.Info("Update cash advance limit:" + customerId);

                                            }
                                        }

                                        if (existnote)
                                        {
                                            if (currentUser.Groups.Select(g => g.Name).Contains("SuperAdmin") || currentUser.Groups.Select(g => g.Name).Contains("OPS"))
                                            {
                                                var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "note");

                                                if (da != null)
                                                {
                                                    da.Delete();
                                                }

                                                string note = ds.Tables[0].Rows[i]["Note"].ToString();
                                                if (!String.IsNullOrEmpty(note))
                                                {
                                                    DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                    cusM.CustomerID = customerId;
                                                    cusM.Factor = "note";
                                                    cusM.Value = note;
                                                    cusM.RowCreatedTime = DateTime.Now;
                                                    cusM.RowCreatedUser = currentUser.UserName;
                                                    cusM.Save();
                                                    log.Info("Update note:" + customerId);
                                                }
                                            }
                                        }

                                        if (existstartworkingdate)
                                        {
                                            string startworkingdate = ds.Tables[0].Rows[i]["StartWorkingDate"].ToString() != "" ? ds.Tables[0].Rows[i]["StartWorkingDate"].ToString() : "1900-01-01";
                                            if (startworkingdate != null && DateTime.TryParse(startworkingdate, out temp))
                                            {
                                                var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "startworkingdate");

                                                if (da != null)
                                                {
                                                    da.Delete();
                                                }

                                                DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                cusM.CustomerID = customerId;
                                                cusM.Factor = "startworkingdate";
                                                cusM.Value = startworkingdate;
                                                cusM.RowCreatedTime = DateTime.Now;
                                                cusM.RowCreatedUser = currentUser.UserName;
                                                cusM.Save();
                                                log.Info("Update start working date:" + customerId);
                                            }
                                        }

                                        if (existbusiness)
                                        {
                                            string business = ds.Tables[0].Rows[i]["Business Unit"].ToString();
                                            if (business != null)
                                            {
                                                var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "business unit");

                                                if (da != null)
                                                {
                                                    da.Delete();
                                                }

                                                DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                cusM.CustomerID = customerId;
                                                cusM.Factor = "business unit";
                                                cusM.Value = business;
                                                cusM.RowCreatedTime = DateTime.Now;
                                                cusM.RowCreatedUser = currentUser.UserName;
                                                cusM.Save();
                                                log.Info("Update business unit:" + customerId);

                                            }
                                        }

                                        if (existlocation)
                                        {
                                            string location = ds.Tables[0].Rows[i]["Location"].ToString();
                                            if (location != null)
                                            {
                                                var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "location");

                                                if (da != null)
                                                {
                                                    da.Delete();
                                                }

                                                DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                cusM.CustomerID = customerId;
                                                cusM.Factor = "location";
                                                cusM.Value = location;
                                                cusM.RowCreatedTime = DateTime.Now;
                                                cusM.RowCreatedUser = currentUser.UserName;
                                                cusM.Save();
                                                log.Info("Update location:" + customerId);

                                            }
                                        }

                                        if (existclass)
                                        {
                                            string clas = ds.Tables[0].Rows[i]["Class"].ToString();
                                            if (clas != null)
                                            {
                                                var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "Class");

                                                if (da != null)
                                                {
                                                    da.Delete();
                                                }

                                                DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                cusM.CustomerID = customerId;
                                                cusM.Factor = "Class";
                                                cusM.Value = clas.Substring(0, 1);
                                                cusM.RowCreatedTime = DateTime.Now;
                                                cusM.RowCreatedUser = currentUser.UserName;
                                                cusM.Save();
                                                log.Info("Update location:" + customerId);


                                            }
                                        }


                                        if (existseniority)
                                        {
                                            string seni = ds.Tables[0].Rows[i]["Seniority"].ToString();
                                            if (seni != null)
                                            {
                                                var da = DC_Customer_Meta.GetDC_Customer_Meta(customerId, "SSeniority");

                                                if (da != null)
                                                {
                                                    da.Delete();
                                                }

                                                DC_Customer_Meta cusM = new DC_Customer_Meta();
                                                cusM.CustomerID = customerId;
                                                cusM.Factor = "SSeniority";
                                                cusM.Value = seni.Substring(0, 1);
                                                cusM.RowCreatedTime = DateTime.Now;
                                                cusM.RowCreatedUser = currentUser.UserName;
                                                cusM.Save();
                                                log.Info("Update Seniority:" + customerId);


                                            }
                                        }
                                        //var bank = DC_Customer_Banking.GetDC_Customer_Banking(customerId);
                                        //string bankaccount = ds.Tables[0].Rows[i]["BankAccount"].ToString();
                                        //string bankname = ds.Tables[0].Rows[i]["BankName"].ToString();
                                        //string bankbranch = ds.Tables[0].Rows[i]["BankBranch"].ToString();
                                        //if (bank != null)
                                        //{
                                        //    bank.CustomerID = customerId;
                                        //    bank.BankName = !string.IsNullOrEmpty(bankname) ? bankname : "";
                                        //    bank.BankBranch = !string.IsNullOrEmpty(bankbranch) ? bankbranch : "";
                                        //    bank.BankAccount = !string.IsNullOrEmpty(bankaccount) ? bankaccount : "";
                                        //    bank.RowLastUpdatedTime = DateTime.Now;
                                        //    bank.RowLastUpdatedUser = currentUser.UserName;
                                        //    bank.Update();
                                        //}
                                        //else
                                        //{
                                        //    if (!string.IsNullOrEmpty(bankname) || !string.IsNullOrEmpty(bankbranch) || !string.IsNullOrEmpty(bankaccount))
                                        //    {
                                        //        DC_Customer_Banking banks = new DC_Customer_Banking();
                                        //        banks.CustomerID = customerId;
                                        //        banks.BankName = !string.IsNullOrEmpty(bankname) ? bankname : "";
                                        //        banks.BankBranch = !string.IsNullOrEmpty(bankbranch) ? bankbranch : "";
                                        //        banks.BankAccount = !string.IsNullOrEmpty(bankaccount) ? bankaccount : "";
                                        //        banks.RowCreatedTime = DateTime.Now;
                                        //        banks.RowCreatedUser = currentUser.UserName;
                                        //        banks.Save();
                                        //    }
                                        //}
                                        
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    log.Error(e);

                                    DW_DC_Customer cus = new DW_DC_Customer();
                                    cus.CustomerID = customerId;
                                    cus.ImportNote = customerId + "-Error";
                                    listData.Add(cus);
                                    continue;
                                }

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Please select Excel File.");
                        }
                    }
                    log.Info("Import Customer");
                    return Json(new { success = true, data = listData.Select(a => a.ImportNote).ToList(), total = total });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json(new { success = false });
            }
        }

        public ActionResult CustomerImport_read([DataSourceRequest] DataSourceRequest request, string data)
        {
            dynamic newdata = Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(ExpandoObject));
            List<DW_DC_Customer> list = new List<DW_DC_Customer>();
            foreach (var i in newdata)
            {
                DW_DC_Customer cus = new DW_DC_Customer();
                cus.CustomerID = i.Split('-')[0];
                cus.ImportNote = i;
                list.Add(cus);
            }
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult getListFile([DataSourceRequest] DataSourceRequest request, string id)
        {
            if (asset.Update)
            {
                try
                {

                    string pathForSaving = Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-"));
                    var list = new List<uFile>();
                    var totalfile = Directory.GetFiles(pathForSaving);
                    foreach (var i in totalfile)
                    {
                        list.Add(new uFile(i.ToString().Split('\\').Last(), id));
                    }

                    return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult uploadfile(string id)
        {
            if (asset.Update)
            {
                // string id = "BKCO";
                HttpPostedFileBase fileU = Request.Files[0];
                try
                {
                    if (fileU != null && fileU.ContentLength > 0)
                    {
                        if (ModelState.IsValid)
                        {
                            if (fileU != null && fileU.ContentLength > 0)
                            {
                                string pathForSaving = Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-"));
                                if (this.CreateFolderIfNeeded(pathForSaving))
                                {
                                    try
                                    {
                                        if (fileU.ContentLength > 5242880)
                                        {
                                            return Json(new { success = false, error = "File size Maximum 5M" });
                                        }
                                        var totalfile = Directory.GetFiles(pathForSaving).Count();
                                        if (totalfile == 5)
                                        {
                                            return Json(new { success = false, error = "Maximum 5 files" });
                                        }

                                        var filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Locdau.ConvertFileName(fileU.FileName);
                                        fileU.SaveAs(Path.Combine(pathForSaving, filename));
                                        log.Info("Upload file " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Locdau.ConvertFileName(fileU.FileName) + "to " + pathForSaving);
                                        //var filelocation = DC_Merchant_AP_Master_Meta.GetDC_Merchant_AP_Master_Meta(id, "fileupload");
                                        //if (filelocation != null)
                                        //{
                                        //    filelocation.Value += ";" + "~/UploadFile/" + id + "/" + filename;
                                        //    filelocation.RowLastUpdatedTime = DateTime.Now;
                                        //    filelocation.RowLastUpdatedUser = currentUser.UserName;
                                        //    filelocation.Update();
                                        //}
                                        //else
                                        //{
                                        //    DC_Merchant_AP_Master_Meta mamd = new DC_Merchant_AP_Master_Meta();
                                        //    mamd.APID = id;
                                        //    mamd.Factor = "fileupload";
                                        //    mamd.Value = "~/UploadFile/" + id + "/" + filename;
                                        //    mamd.RowCreatedTime = DateTime.Now;
                                        //    mamd.RowCreatedUser = currentUser.UserName;
                                        //    mamd.Save();
                                        //    log.Info("create new file upload : " + id);
                                        //}
                                    }
                                    catch (Exception e)
                                    {
                                        log.Error(e);
                                        return Json(new { success = false, error = e });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "Please choose file" });
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        //public int CountFile(string id)
        //{
        //    string pathForSaving = Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-"));
        //    int totalfile = 0; ;
        //    if (this.CreateFolderIfNeeded(pathForSaving))
        //    {
        //        totalfile = Directory.GetFiles(pathForSaving).Count();
        //    }
        //    return totalfile;
        //}

        public ActionResult CountFile([DataSourceRequest] DataSourceRequest request, string id)
        {
            try
            {

                string pathForSaving = Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-"));
                int totalfile = 0; ;
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    totalfile = Directory.GetFiles(pathForSaving).Count();
                }

                return Json(totalfile, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e }, JsonRequestBehavior.AllowGet);
            }
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                    log.Info("Create folder " + path);
                }
                catch (Exception e)
                {
                    log.Error(e);
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        public ActionResult DeleteFile(string filename, string id)
        {
            if (asset.Update)
            {

                try
                {
                    string fileLocation = Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-") + "/" + filename);

                    if (System.IO.File.Exists(fileLocation))
                        System.IO.File.Delete(fileLocation);

                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }


        public virtual ActionResult DownloadFile(string id, string file)
        {
            if (asset.Update)
            {
                string fullPath = Path.Combine(Server.MapPath("~/UploadCustomerAttackFile/"), id.Replace(":", "-") + "/" + file);
                if (System.IO.File.Exists(fullPath))
                    return File(fullPath, "application/vnd.ms-excel", file);
                else
                    return null;
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
    }
}