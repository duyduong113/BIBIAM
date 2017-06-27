using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DecaPay.Models;
using System.Collections;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Globalization;
//using DecaPay.Infrastructure;
using System.Data.OleDb;
using System.Dynamic;
using DecaPay.Helpers;
using System.Web;
using System.Text;
//using DecaPay.SMSservice;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Threading.Tasks;
using DevTrends.MvcDonutCaching;

namespace DecaPay.Controllers
{
    [Authorize]
    public class CustomerResignedController : AssetsController
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /CustomerResigned/
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
                //ViewData["UserGroups"] = UserGroup.GetAllUserGroups();

                ViewData["listActionReason"] = ViewBag.listActionReason = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Reason'", "");
                ViewData["listActionStatus"] = ViewBag.listActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Status'", "");
                ViewData["listActionCode"] = ViewBag.listActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ActionCode'", "");
                ViewData["listContactMode"] = ViewBag.listContactMode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ContactMode'", "");
                ViewData["listPersonalContacted"] = ViewBag.listPersonalContacted = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Personal Contacted'", "");
                ViewBag.listBD = DC_GetCustomerResignedList.getListBD().Select(a => a.BD).ToList();
                ViewBag.listOrgs = DC_GetCustomerResignedList.getListOrg().Select(a => a.OrganizationID).ToList();
                //ViewBag.listCusID = DC_GetCustomerResignedList.getListCustomer().Select(s => s.CustomerID).Distinct().ToList();
                //ViewBag.listBDE = DecaPay.Models.User.GetListUserInGroup("u.UserID <> 'administrator' AND GroupID = 7");
                // Send SMS
                var GroupUser = currentUser.Groups;
                var test = DC_SMS_Template.GetAllDC_SMS_Templates();//.Where(t => t.Roles.Split(',').ToList().Contains("All") || GroupUser.Select(s => s.ID.ToString()).Intersect(t.Roles.Split(',').ToList()).Any());
                ViewBag.ListTemplate = test;
                ViewBag.UserName = currentUser.UserName;
                ViewBag.PhoneUser = currentUser.Phone;
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult ManageCreditRequest_Read([DataSourceRequest] DataSourceRequest request, string customerID)
        {
            List<DC_RequestCredit> data = new List<DC_RequestCredit>();
            data = DC_RequestCredit.GetAllDC_Customer_CreditRequests_ByCus("Manage", customerID).ToList();

            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult Remind()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            ViewData["Asset"] = asset;
            //ViewData["UserGroups"] = UserGroup.GetAllUserGroups();

            ViewData["listActionReason"] = ViewBag.listActionReason = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Reason'", "");
            ViewData["listActionStatus"] = ViewBag.listActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Status'", "");
            ViewData["listActionCode"] = ViewBag.listActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ActionCode'", "");
            ViewData["listContactMode"] = ViewBag.listContactMode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ContactMode'", "");
            ViewData["listPersonalContacted"] = ViewBag.listPersonalContacted = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Personal Contacted'", "");
            ViewBag.listBD = DC_GetCustomerResignedList.getListBD().Select(a => a.BD).ToList();
            ViewBag.listOrgs = DC_GetCustomerResignedList.getListOrg().Select(a => a.OrganizationID).ToList();
            //ViewBag.listCusID = DC_GetCustomerResignedList.getCustomerID().Select(s => s.CustomerID).Distinct().ToList();


            // Send SMS
            var GroupUser = currentUser.Groups;
            //var test = DC_SMS_Template.GetAllDC_SMS_Templates().Where(t => t.Roles.Split(',').ToList().Contains("All") || GroupUser.Select(s => s.ID.ToString()).Intersect(t.Roles.Split(',').ToList()).Any());
            //ViewBag.ListTemplate = test;
            ViewBag.UserName = currentUser.UserName;
            ViewBag.PhoneUser = currentUser.Phone;
            return View();

        }
        public ActionResult RemindMinute()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            ViewData["Asset"] = asset;
            //ViewData["UserGroups"] = UserGroup.GetAllUserGroups();

            ViewData["listActionReason"] = ViewBag.listActionReason = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Reason'", "");
            ViewData["listActionStatus"] = ViewBag.listActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Status'", "");
            ViewData["listActionCode"] = ViewBag.listActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ActionCode'", "");
            ViewData["listContactMode"] = ViewBag.listContactMode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ContactMode'", "");
            ViewData["listPersonalContacted"] = ViewBag.listPersonalContacted = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Personal Contacted'", "");

            ViewBag.listBD = DC_GetCustomerResignedList.getListBD().Select(a => a.BD).ToList();
            ViewBag.listOrgs = DC_GetCustomerResignedList.getListOrg().Select(a => a.OrganizationID).ToList();
            ViewBag.listCusID = DC_GetCustomerResignedList.getCustomerID().Distinct().ToList();

            

            // Send SMS
            var GroupUser = currentUser.Groups;
            //var test = DC_SMS_Template.GetAllDC_SMS_Templates().Where(t => t.Roles.Split(',').ToList().Contains("All") || GroupUser.Select(s => s.ID.ToString()).Intersect(t.Roles.Split(',').ToList()).Any());
            //ViewBag.ListTemplate = test;
            ViewBag.UserName = currentUser.UserName;
            ViewBag.PhoneUser = currentUser.Phone;
            return View();

        }



        public JsonResult GetCustomer([DataSourceRequest] DataSourceRequest request, string CustomerID)
        {
            var data = DW_DC_Customer.GetCustomerAllForBadDebt(CustomerID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult checkBaselineAmount(string data)
        {
            try
            {
                var rs = DC_GetCustomerResignedList.CheckBaselineSettledBalance(data.Trim().ToString()).ToList();
                if (rs.Count > 0)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ActionResult GetSettledBalance(string CustomerID)
        {
            DW_DC_Customer data = DW_DC_Customer.GetLoadSuggestSetttledBalance(CustomerID);
            if (string.IsNullOrWhiteSpace(CustomerID))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
            }
        }

        //crud DC_Customer_Resigned
        [DonutOutputCache(CacheProfile = "1Hour")]
        public async Task<ActionResult> CustomerResigned_Read([DataSourceRequest] DataSourceRequest request)
        {
            var userId = currentUser.UserName;
            var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
            List<DC_GetCustomerResignedList> data = new List<DC_GetCustomerResignedList>();
            if (listOrg.Count() > 0)
            {
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.").Replace("LastActionCode", "ActionCode");
                        data = await DC_GetCustomerResignedList.getCustomerResignedListDynamic(where);
                    }
                    else
                    {
                        data = await DC_GetCustomerResignedList.getCustomerResignedListDynamic("1=1");
                    }
                }
                else
                {
                    if (request.Filters.Any())
                    {
                        var wheres = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                        data = (from c in await DC_GetCustomerResignedList.getCustomerResignedListDynamic(wheres)
                                join lo in listOrg on c.OrganizationID equals lo.OrgID
                                where lo.UserID == userId
                                select c).ToList();
                    }
                    else
                    {
                        data = (from c in await DC_GetCustomerResignedList.getCustomerResignedListDynamic("1=1")
                                join lo in listOrg on c.OrganizationID equals lo.OrgID
                                where lo.UserID == userId
                                select c).ToList();
                    }
                }
            }

            return Json(data.ToDataSourceResult(request));
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult RemindDayResigned_Read([DataSourceRequest] DataSourceRequest request)
        {
            var userId = currentUser.UserName;
            var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
            List<DC_GetCustomerResignedList> data = new List<DC_GetCustomerResignedList>();
            if (listOrg.Count() > 0)
            {
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.").Replace("LastActionCode", "ActionCode");
                        data = DC_GetCustomerResignedList.getRemindDayCustomerResignedListDynamic(where);
                    }
                    else
                    {
                        data = DC_GetCustomerResignedList.getRemindDayCustomerResignedListDynamic("1=1");
                    }
                }
                else
                {
                    if (request.Filters.Any())
                    {
                        var wheres = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                        data = (from c in DC_GetCustomerResignedList.getRemindDayCustomerResignedListDynamic(wheres)
                                join lo in listOrg on c.OrganizationID equals lo.OrgID
                                where lo.UserID == userId
                                select c).ToList();
                    }
                    else
                    {
                        data = (from c in DC_GetCustomerResignedList.getRemindDayCustomerResignedListDynamic("1=1")
                                join lo in listOrg on c.OrganizationID equals lo.OrgID
                                where lo.UserID == userId
                                select c).ToList();
                    }
                }
            }

            return Json(data.ToDataSourceResult(request));
        }
        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult RemindMinuteResigned_Read([DataSourceRequest] DataSourceRequest request)
        {
            var userId = currentUser.UserName;
            var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
            List<DC_GetCustomerResignedList> data = new List<DC_GetCustomerResignedList>();
            if (listOrg.Count() > 0)
            {
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                        data = DC_GetCustomerResignedList.getRemindMinuteCustomerResignedListDynamic(where);
                    }
                    else
                    {
                        data = DC_GetCustomerResignedList.getRemindMinuteCustomerResignedList();
                    }
                }
                else
                {
                    if (request.Filters.Any())
                    {
                        var wheres = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                        data = (from c in DC_GetCustomerResignedList.getRemindMinuteCustomerResignedListDynamic(wheres)
                                join lo in listOrg on c.OrganizationID equals lo.OrgID
                                where lo.UserID == userId
                                select c).ToList();
                    }
                    else
                    {
                        data = (from c in DC_GetCustomerResignedList.getRemindMinuteCustomerResignedList()
                                join lo in listOrg on c.OrganizationID equals lo.OrgID
                                where lo.UserID == userId
                                select c).ToList();
                    }
                }
            }
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CustomerResigned_Create([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<DC_GetCustomerResignedList> data)
        {
            if (asset.Create)
            {
                if (data != null)
                {
                    foreach (var i in data)
                    {
                        if (!String.IsNullOrEmpty(i.CustomerID) && i.ResignedDate != null && i.InformedDate != null && !String.IsNullOrEmpty(i.ResignedReason))
                        {
                            var exist = DC_Customer_Resigned.GetDC_Customer_Resigned(i.CustomerID);
                            if (exist != null)
                            {
                                ModelState.AddModelError("Error", "CustomerID is already existed");
                                return Json(data.ToDataSourceResult(request, ModelState));
                            }

                            var exist1 = DW_DC_Customer.GetDW_DC_Customer(i.CustomerID);
                            if (exist1 == null)
                            {
                                ModelState.AddModelError("Error", "CustomerID is not existed");
                                return Json(data.ToDataSourceResult(request, ModelState));
                            }

                            DC_Customer_Resigned mcr = new DC_Customer_Resigned();
                            mcr.CustomerID = i.CustomerID.ToUpper();
                            mcr.ResignedDate = i.ResignedDate.Date;
                            mcr.InformedDate = i.InformedDate.Date;
                            mcr.ResignedReason = i.ResignedReason;
                            mcr.Note = i.Note;
                            mcr.RowCreatedTime = DateTime.Now;
                            mcr.RowCreatedUser = currentUser.UserName;
                            mcr.Save();
                            log.Info("Add resigned for Customer: " + i.CustomerID);
                        }
                    }

                }
            }
            else
            {
                ModelState.AddModelError("Error", "You don't have permisson to create record");
                return Json(data.ToDataSourceResult(request, ModelState));
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CustomerResigned_Update([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<DC_GetCustomerResignedList> data)
        {
            if (asset.Update)
            {
                if (data != null)
                {
                    foreach (var i in data)
                    {
                        if (!String.IsNullOrEmpty(i.CustomerID) && i.ResignedDate != null && i.InformedDate != null && !String.IsNullOrEmpty(i.ResignedReason))
                        {
                            var mcr = DC_Customer_Resigned.GetDC_Customer_Resigned(i.CustomerID);
                            mcr.ResignedDate = i.ResignedDate.Date;
                            mcr.InformedDate = i.InformedDate.Date;
                            mcr.ResignedReason = i.ResignedReason;
                            mcr.Note = i.Note;
                            mcr.RowLastUpdatedTime = DateTime.Now;
                            mcr.RowLastUpdatedUser = currentUser.UserName;
                            mcr.Update();
                            log.Info("Update Customer Resigned : " + i.CustomerID);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("UserID", "You don't have permisson to update record");
                return Json(data.ToDataSourceResult(request, ModelState));
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }


        //crud DC_Customer_Resigned_Activity
        public ActionResult CustomerResignedActivities_Read([DataSourceRequest] DataSourceRequest request, string cusID)
        {
            var data = DC_Customer_Resigned_Activity.GetDC_Customer_Resigned_Activitys(cusID);
            return Json(data.ToDataSourceResult(request));
        }

        public async Task<ActionResult> CountRemind([DataSourceRequest] DataSourceRequest request)
        {
            var data = await DC_Customer_Resigned_Activity.GetCountRemindMinute();
            var day = DC_Customer_Resigned_Activity.GetCountRemindDay().Count;
            return Json(new { success = true, data = data.Count, day = day });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CustomerResignedActivities_Create(string cusID, DC_Customer_Resigned_Activity data)
        {
            if (asset.Create)
            {
                if (data != null && ModelState.IsValid && data.ActionStatus != null && data.Note != null)
                {
                    //var exist = DC_Customer_Resigned_Activity.GetDC_Customer_Resigned_Activitys("[CustomerID]='" + cusID + "'", "").Select(a => a.ActionStatus).Contains(data.ActionStatus);
                    //if (exist)
                    //{
                    //    ModelState.AddModelError("Error", "Action status alrealdy exist.Please choose other");
                    //    return Json(new[] { data }.ToDataSourceResult(new DataSourceRequest(), ModelState));
                    //}

                    data.CustomerID = cusID.ToUpper();
                    data.RowCreatedTime = DateTime.Now;
                    data.RowCreatedUser = currentUser.UserName;
                    data.Save();
                    log.Info("Create Customer Resigned Activities");
                }
            }
            else
            {
                ModelState.AddModelError("UserID", "You don't have permisson to create record");
                return Json(new[] { data }.ToDataSourceResult(new DataSourceRequest(), ModelState));
            }

            return Json(new[] { data }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CustomerResignedActivities_Update(string cusID, DC_Customer_Resigned_Activity data)
        {
            if (asset.Update)
            {
                if (data != null && ModelState.IsValid)
                {
                    data.RowLastUpdatedTime = DateTime.Now;
                    data.RowLastUpdatedUser = currentUser.UserName;
                    data.Update();
                    log.Info("Update Customer Resigned Activities:" + cusID);
                }
            }
            else
            {
                ModelState.AddModelError("UserID", "You don't have permisson to update record");
                return Json(new[] { data }.ToDataSourceResult(new DataSourceRequest(), ModelState));
            }

            return Json(new[] { data }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        //public ActionResult DeleteCustomerResigned(string data)
        //{
        //    if (asset.Delete)
        //    {
        //        try
        //        {
        //            var listCustomerID = data.Split(',');
        //            for (var i = 0; i < listCustomerID.Length; i++)
        //            {
        //                var customerResigned = DC_Customer_Resigned.GetDC_Customer_Resigned(listCustomerID[i]);
        //                if (customerResigned != null)
        //                {
        //                    customerResigned.Delete();
        //                }
        //                var customerResignedActivities = DC_Customer_Resigned_Activity.GetDC_Customer_Resigned_Activitys("[CustomerID]='" + listCustomerID[i] + "'", "");
        //                if (customerResignedActivities.Count > 0)
        //                {
        //                    foreach (var c in customerResignedActivities)
        //                    {
        //                        c.Delete();
        //                    }
        //                }
        //                log.Info("Delete Customer Resigned + Activities :" + listCustomerID[i]);
        //            }
        //            return Json(new { success = true });
        //        }
        //        catch (Exception e)
        //        {
        //            log.Error(e);
        //            return Json(new { success = false, alert = e });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = false, alert = "Don't have permission to delete" });
        //    }
        //}

        public ActionResult ActiveCustomerResigned(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    var listCustomerID = data.Split(',');
                    for (var i = 0; i < listCustomerID.Length; i++)
                    {
                        var customerResigned = DC_Customer_Resigned.GetDC_Customer_Resigned(listCustomerID[i]);
                        if (customerResigned != null)
                        {
                            customerResigned.Actives();
                        }

                        log.Info("Remove Customer Resigned + Activities :" + listCustomerID[i]);
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false, alert = e });
                }
            }
            else
            {
                return Json(new { success = false, alert = "Don't have permission to delete" });
            }
        }
        public ActionResult RemoveCustomerResigned(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    var listCustomerID = data.Split(',');
                    for (var i = 0; i < listCustomerID.Length; i++)
                    {
                        var customerResigned = DC_Customer_Resigned.GetDC_Customer_Resigned(listCustomerID[i]);
                        if (customerResigned != null)
                        {
                            customerResigned.Remove();
                        }

                        log.Info("Remove Customer Resigned + Activities :" + listCustomerID[i]);
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false, alert = e });
                }
            }
            else
            {
                return Json(new { success = false, alert = "Don't have permission to delete" });
            }
        }

        [ExportCache]
        public async Task<FileResult> Export([DataSourceRequest]
                                 DataSourceRequest request, string UserID)
        {
            if (asset.Export && !string.IsNullOrEmpty(UserID) && UserID == currentUser.UserName)
            {
                var userId = currentUser.UserName;
                var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
                List<DC_GetCustomerResignedList> d = new List<DC_GetCustomerResignedList>();

                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.").Replace("LastActionCode", "ActionCode");
                    if (listOrg.FirstOrDefault().OrgID == "All")
                    {
                        d = await DC_GetCustomerResignedList.getCustomerResignedListDynamic(where);
                    }
                    else
                    {
                        d = (from c in await DC_GetCustomerResignedList.getCustomerResignedListDynamic("1=1")
                             join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                             where lo.UserID == userId
                             select c).ToList();
                    }
                }
                else
                {
                    d = (from c in await DC_GetCustomerResignedList.getCustomerResignedListDynamic("1=1")
                         join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                         where lo.UserID == userId
                         select c).ToList();
                }

                var listCustomerResignedList = d;
                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = listCustomerResignedList.ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_Customer_Resignation_List.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("Customer Resignation List");

                int rowNumber = 1;

                Deca_Code_Master_List listCode = new Deca_Code_Master_List("CodeType = 'Customer Resigned Reason'", "");
                //Populate the sheet with values from the grid data
                foreach (DC_GetCustomerResignedList data in datas)
                {
                    int i = 0;
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    //Set values for the cells
                    row.CreateCell(i).SetCellValue(data.CustomerID); i++;
                    row.CreateCell(i).SetCellValue(data.ResignedDate); i++;
                    row.CreateCell(i).SetCellValue(data.InformedDate); i++;
                    row.CreateCell(i).SetCellValue(listCode.SearchID(data.ResignedReason)); i++;
                    row.CreateCell(i).SetCellValue(data.Note); i++;
                    row.CreateCell(i).SetCellValue(data.LastActionStatus); i++;
                    row.CreateCell(i).SetCellValue(data.LastActionCode); i++;
                    row.CreateCell(i).SetCellValue(data.DayOverdue.ToShortDateString() != "1900-01-01 12:00:00 AM" ? data.DayOverdue.ToString() : ""); i++;
                    row.CreateCell(i).SetCellValue(data.BaselineSettledBalance); i++;
                    row.CreateCell(i).SetCellValue(data.DurationDebt); i++;
                    row.CreateCell(i).SetCellValue(data.LastNote); i++;

                    row.CreateCell(i).SetCellValue(data.LastUpdatedTime); i++;
                    row.CreateCell(i).SetCellValue(data.BD); i++;
                    row.CreateCell(i).SetCellValue(data.Address); i++;
                    row.CreateCell(i).SetCellValue(data.Reference); i++;
                    row.CreateCell(i).SetCellValue(data.OrganizationID); i++;
                    row.CreateCell(i).SetCellValue(data.FullName); i++;
                    row.CreateCell(i).SetCellValue(data.CreditLimit); i++;
                    row.CreateCell(i).SetCellValue(data.SettledBalance); i++;
                    row.CreateCell(i).SetCellValue(data.CollectionNotes); i++;
                    row.CreateCell(i).SetCellValue(data.CurrentStatus); i++;
                    row.CreateCell(i).SetCellValue(data.Gender); i++;
                    row.CreateCell(i).SetCellValue(data.GroupID); i++;
                    row.CreateCell(i).SetCellValue(data.MobilePhone); i++;
                    row.CreateCell(i).SetCellValue(data.Email); i++;
                    row.CreateCell(i).SetCellValue(data.DayOverdue); i++;
                    row.CreateCell(i).SetCellValue(data.AddedProfile); i++;
                }

                var sheet1 = workbook.GetSheet("List");

                int rowNumber1 = 1;
                var reason = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Reason'", "").Select(c => c.Description).Distinct().ToList();
                foreach (var list in reason)
                {
                    //Create a new row
                    var row1 = sheet1.CreateRow(rowNumber1++);
                    //Set values for the cells
                    row1.CreateCell(0).SetCellValue(list);
                }
                string fname = currentUser.UserName + " DC_Customer_Resignation_List_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                if (ExportCache.CachResult(workbook, fname))
                {
                    return null;
                }
                //Write the workbook to a memory stream
                //MemoryStream output = new MemoryStream();
                //workbook.Write(output);

                //Return the result to the end user
                log.Info("Export customer resigned list");
                return File(ExportCache.FilePath(fname), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer_Resignation_List_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                log.Info("Don't have permission to export organization");
                return null;
            }

        }

        public async Task<FileResult> ExportError([DataSourceRequest]
                                 DataSourceRequest request, string listCusId)
        {
            if (asset.Export)
            {
                var userId = currentUser.UserName;
                var listOrg = UserInOrg.GetUserInOrgs("[UserID]='" + userId + "'", "");
                List<DC_GetCustomerResignedList> d = new List<DC_GetCustomerResignedList>();
                string intos = String.Join(",", listCusId.Split(',').Select(s => "'" + s + "'").ToArray());
                if (listOrg.FirstOrDefault().OrgID == "All")
                {
                    d = await DC_GetCustomerResignedList.getCustomerResignedListDynamic("data.CustomerID IN (" + intos + ")");
                }
                else
                {
                    d = (from c in await DC_GetCustomerResignedList.getCustomerResignedListDynamic("data.CustomerID IN (" + intos + ")")
                         join lo in listOrg on c.CustomerID.Split(':')[0] equals lo.OrgID
                         where lo.UserID == userId
                         select c).ToList();
                }

                var listCustomerResignedList = d;

                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = listCustomerResignedList.Where(c => listCusId.Contains(c.CustomerID)).ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_Customer_Resignation_List.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("Customer Resignation List");

                int rowNumber = 1;

                //Populate the sheet with values from the grid data
                foreach (DC_GetCustomerResignedList data in datas)
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    //Set values for the cells
                    //row.CreateCell(0).SetCellValue(data.CustomerID);
                    //row.CreateCell(1).SetCellValue(data.ResignedDate);
                    //row.CreateCell(2).SetCellValue(data.InformedDate);
                    //row.CreateCell(3).SetCellValue(Deca_Code_Master.GetDeca_Code_Master(data.ResignedReason, "Customer Resigned Reason") != null ? Deca_Code_Master.GetDeca_Code_Master(data.ResignedReason, "Customer Resigned Reason").Description : "");
                    //row.CreateCell(4).SetCellValue(data.Note);

                    //row.CreateCell(6).SetCellValue(data.LastUpdatedTime);
                    //row.CreateCell(7).SetCellValue(data.BD);
                    //row.CreateCell(8).SetCellValue(data.OrganizationID);
                    //row.CreateCell(9).SetCellValue(data.FullName);
                    //row.CreateCell(10).SetCellValue(data.CreditLimit);
                    //row.CreateCell(11).SetCellValue(data.SettledBalance);
                    //row.CreateCell(12).SetCellValue(data.CurrentStatus);
                    //row.CreateCell(13).SetCellValue(data.Gender);
                    //row.CreateCell(14).SetCellValue(data.GroupID);
                    //row.CreateCell(15).SetCellValue(data.MobilePhone);
                    //row.CreateCell(16).SetCellValue(data.Email);


                    int i = 0;
                    row.CreateCell(i).SetCellValue(data.CustomerID); i++;
                    row.CreateCell(i).SetCellValue(data.ResignedDate); i++;
                    row.CreateCell(i).SetCellValue(data.InformedDate); i++;
                    row.CreateCell(i).SetCellValue(Deca_Code_Master.GetDeca_Code_Master(data.ResignedReason, "Customer Resigned Reason") != null ? Deca_Code_Master.GetDeca_Code_Master(data.ResignedReason, "Customer Resigned Reason").Description : ""); i++;
                    row.CreateCell(i).SetCellValue(data.Note); i++;
                    if (!String.IsNullOrEmpty(data.LastActionStatus))
                    {
                        row.CreateCell(5).SetCellValue(Deca_Code_Master.GetDeca_Code_Master(data.LastActionStatus, "Customer Resigned Status") != null ? Deca_Code_Master.GetDeca_Code_Master(data.LastActionStatus, "Customer Resigned Status").Description : ""); i++;
                    }
                    else
                    {
                        row.CreateCell(5).SetCellValue(""); i++;
                    }
                    row.CreateCell(i).SetCellValue(data.LastActionCode); i++;
                    row.CreateCell(i).SetCellValue(data.DayOverdue); i++;
                    row.CreateCell(i).SetCellValue(data.AddedProfile); i++;
                    row.CreateCell(i).SetCellValue(data.LastUpdatedTime); i++;
                    row.CreateCell(i).SetCellValue(data.BD); i++;
                    row.CreateCell(i).SetCellValue(data.Address); i++;
                    row.CreateCell(i).SetCellValue(data.Reference); i++;
                    row.CreateCell(i).SetCellValue(data.OrganizationID); i++;
                    row.CreateCell(i).SetCellValue(data.FullName); i++;
                    row.CreateCell(i).SetCellValue(data.CreditLimit); i++;
                    row.CreateCell(i).SetCellValue(data.SettledBalance); i++;
                    row.CreateCell(i).SetCellValue(data.CurrentStatus); i++;
                    row.CreateCell(i).SetCellValue(data.Gender); i++;
                    row.CreateCell(i).SetCellValue(data.GroupID); i++;
                    row.CreateCell(i).SetCellValue(data.MobilePhone); i++;
                    row.CreateCell(i).SetCellValue(data.Email); i++;
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                workbook.Write(output);

                //Return the result to the end user
                log.Info("Export customer resigned list");
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer_Resignation_List_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                log.Info("Don't have permission to export organization");
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Customer_Resignation_List.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }

        }

        [HttpPost]
        public ActionResult ImportFromExcel()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<DC_GetCustomerResignedList> listData = new List<DC_GetCustomerResignedList>();
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
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                string customerId = ds.Tables[0].Rows[i]["CustomerID"].ToString();
                                string resignedDate = ds.Tables[0].Rows[i]["ResignedDate"].ToString();
                                string informedDate = ds.Tables[0].Rows[i]["InformedDate"].ToString();
                                string resignedReason = ds.Tables[0].Rows[i]["ResignedReason"].ToString();
                                string note = ds.Tables[0].Rows[i]["Note"].ToString();
                                string reference = ds.Tables[0].Rows[i]["Reference"].ToString();
                                DateTime temp;
                                try
                                {
                                    if (!String.IsNullOrEmpty(customerId) && !String.IsNullOrEmpty(resignedDate) && !String.IsNullOrEmpty(informedDate) && !String.IsNullOrEmpty(resignedReason) && !String.IsNullOrEmpty(reference) && DateTime.TryParse(resignedDate, out temp) && DateTime.TryParse(informedDate, out temp))
                                    {
                                        var mcr = DC_Customer_Resigned.GetDC_Customer_Resigned(customerId);
                                        if (mcr != null)
                                        {
                                            mcr.ResignedDate = DateTime.Parse(resignedDate).Date;
                                            mcr.InformedDate = DateTime.Parse(informedDate).Date;
                                            mcr.ResignedReason = Deca_Code_Master.GetDeca_Code_Masters("[Description]=N'" + resignedReason + "'", "").FirstOrDefault() != null ? Deca_Code_Master.GetDeca_Code_Masters("[Description]=N'" + resignedReason + "'", "").FirstOrDefault().CodeID : "";
                                            mcr.Note = note;
                                            mcr.Reference = reference;
                                            mcr.RowLastUpdatedTime = DateTime.Now;
                                            mcr.RowLastUpdatedUser = currentUser.UserName;
                                            mcr.Update();

                                            var listCusInfo = DC_Customer_Resigned_Activity_CusInfo_Meta.GetDC_Customer_Resigned_Activity_CusInfo_Metas("[CustomerID]='" + customerId + "' AND [Factor] = 'ReferenceInfo'", "").ToList();
                                            if (listCusInfo.Count > 0)
                                            {
                                                foreach (var item in listCusInfo)
                                                {
                                                    item.Delete();
                                                }
                                            }

                                            DC_Customer_Resigned_Activity_CusInfo_Meta meta = new DC_Customer_Resigned_Activity_CusInfo_Meta();
                                            meta.CustomerID = customerId;
                                            meta.Factor = "ReferenceInfo";
                                            meta.Value = reference;
                                            meta.RowCreatedTime = DateTime.Now;
                                            meta.RowCreatedUser = currentUser.UserName;
                                            meta.RowLastUpdatedTime = DateTime.Now;
                                            meta.RowLastUpdatedUser = currentUser.UserName;
                                            meta.Save();
                                        }
                                        else
                                        {
                                            DC_Customer_Resigned newmcr = new DC_Customer_Resigned();
                                            newmcr.CustomerID = customerId;
                                            newmcr.ResignedDate = DateTime.Parse(resignedDate).Date;
                                            newmcr.InformedDate = DateTime.Parse(informedDate).Date;
                                            newmcr.ResignedReason = Deca_Code_Master.GetDeca_Code_Masters("[Description]=N'" + resignedReason + "'", "").FirstOrDefault() != null ? Deca_Code_Master.GetDeca_Code_Masters("[Description]=N'" + resignedReason + "'", "").FirstOrDefault().CodeID : "";
                                            newmcr.Note = note;
                                            newmcr.Reference = reference;
                                            newmcr.RowCreatedTime = DateTime.Now;
                                            newmcr.RowCreatedUser = currentUser.UserName;
                                            newmcr.Save();

                                            var listCusInfo = DC_Customer_Resigned_Activity_CusInfo_Meta.GetDC_Customer_Resigned_Activity_CusInfo_Metas("[CustomerID]='" + customerId + "' AND [Factor] = 'ReferenceInfo'", "").ToList();
                                            if (listCusInfo.Count > 0)
                                            {
                                                foreach (var item in listCusInfo)
                                                {
                                                    item.Delete();
                                                }
                                            }

                                            DC_Customer_Resigned_Activity_CusInfo_Meta meta = new DC_Customer_Resigned_Activity_CusInfo_Meta();
                                            meta.CustomerID = customerId;
                                            meta.Factor = "ReferenceInfo";
                                            meta.Value = reference;
                                            meta.RowCreatedTime = DateTime.Now;
                                            meta.RowCreatedUser = currentUser.UserName;
                                            meta.RowLastUpdatedTime = DateTime.Now;
                                            meta.RowLastUpdatedUser = currentUser.UserName;
                                            meta.Save();


                                        }
                                      
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    log.Error(e);

                                    DC_GetCustomerResignedList cus = new DC_GetCustomerResignedList();
                                    cus.CustomerID = customerId;
                                    cus.ImportNote = customerId + "-" + e.ToString();
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
                    log.Info("Import Customer Resigned");
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
                return RedirectToAction("Index");
            }
        }

        public ActionResult CustomerImport_read([DataSourceRequest] DataSourceRequest request, string data)
        {
            dynamic newdata = Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(ExpandoObject));
            List<DC_GetCustomerResignedList> list = new List<DC_GetCustomerResignedList>();
            foreach (var i in newdata)
            {
                DC_GetCustomerResignedList cus = new DC_GetCustomerResignedList();
                cus.CustomerID = i.Split('-')[0];
                cus.ImportNote = i;
                list.Add(cus);
            }
            return Json(list.ToDataSourceResult(request));
        }



        public ActionResult getActionStatus(string codeID)
        {
            try
            {
                var description = Deca_Code_Master.GetDeca_Code_Master(codeID, "Customer Resigned Status").Description;
                return Json(new { data = description, success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        public ActionResult getStatus()
        {
            try
            {
                var description = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Status'", "");
                return Json(new { data = description, success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }


        //DungNT: Fix them Money, Ngay thanh toan, bank
        [HttpPost]
        public ActionResult UpdateActivity(int RowId, string BaselineSettledBalance, string ActionCode, string ContactMode, string CustomerID, string Status, string Note, string ap, string Money, string DatePromise, string Bank, string PersonalContacted)
        {
            if (!string.IsNullOrEmpty(CustomerID) && !string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Note) && !string.IsNullOrEmpty(ContactMode) && !string.IsNullOrEmpty(ActionCode) && !string.IsNullOrEmpty(ap))
            {
                //Check Callback no date call ageint
                if (ap == "" || ap == null)
                {
                    ap = "1900-01-01";
                }
                DateTime doa = Convert.ToDateTime(ap);
                TimeSpan Time = doa - DateTime.Now;
                int day = Time.Days;
                if (day > 5)
                {
                    return Json("Error: Date Of Appointmen must <= 5days");
                }
                DC_Customer_Resigned_Activity activity = new DC_Customer_Resigned_Activity();

                activity.RowID = RowId;
                activity.CustomerID = CustomerID;
                activity.ActionStatus = Status;
                activity.Note = Note;
                activity.RowLastUpdatedTime = DateTime.Now;
                activity.RowLastUpdatedUser = currentUser.UserName;

                try
                {
                    activity.Update();
                    var contact = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'ContactMode'", "").ToList();
                    if (contact.Count > 0)
                    {
                        foreach (var item in contact)
                        {
                            item.Delete();
                        }
                    }
                    DC_Customer_Resigned_Activity_Meta meta = new DC_Customer_Resigned_Activity_Meta();
                    meta.RowID = RowId;
                    meta.Factor = "ContactMode";
                    meta.Value = ContactMode;
                    meta.RowCreatedTime = DateTime.Now;
                    meta.RowCreatedUser = currentUser.UserName;
                    meta.RowLastUpdatedTime = DateTime.Now;
                    meta.RowLastUpdatedUser = currentUser.UserName;
                    meta.Save();


                    var actionCode = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'ActionCode'", "").ToList();
                    if (actionCode.Count > 0)
                    {
                        foreach (var item2 in actionCode)
                        {
                            item2.Delete();
                        }
                    }
                    DC_Customer_Resigned_Activity_Meta meta2 = new DC_Customer_Resigned_Activity_Meta();
                    meta2.RowID = RowId;
                    meta2.Factor = "ActionCode";
                    meta2.Value = ActionCode;
                    meta2.RowCreatedTime = DateTime.Now;
                    meta2.RowCreatedUser = currentUser.UserName;
                    meta2.RowLastUpdatedTime = DateTime.Now;
                    meta2.RowLastUpdatedUser = currentUser.UserName;
                    meta2.Save();

                    //Neu actioncode = ACM01 - Payment to pay thi them factor Bank, Money, Date
                    if (ActionCode == "ACM01" || ActionCode.ToString() == "ACM01")
                    {
                        //Check money in number
                        double num;
                        if (double.TryParse(Money, out num))
                        {
                            // It's a number!
                            var _Money = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'Money'", "").ToList();
                            if (_Money.Count > 0)
                            {
                                foreach (var item2 in _Money)
                                {
                                    item2.Delete();
                                }
                            }
                            DC_Customer_Resigned_Activity_Meta meta5 = new DC_Customer_Resigned_Activity_Meta();
                            meta5.RowID = RowId;
                            meta5.Factor = "Money";

                            if (Money == null || Money == "")
                                meta5.Value = "";
                            else
                                meta5.Value = Money;

                            meta5.RowCreatedTime = DateTime.Now;
                            meta5.RowCreatedUser = currentUser.UserName;
                            meta5.RowLastUpdatedTime = DateTime.Now;
                            meta5.RowLastUpdatedUser = currentUser.UserName;
                            meta5.Save();
                        }
                        else
                        {
                            return Json("Error: Please check money is number ");
                        }



                        var Ngaythanhtoan = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'PaymentDate'", "").ToList();
                        if (Ngaythanhtoan.Count > 0)
                        {
                            foreach (var item2 in Ngaythanhtoan)
                            {
                                item2.Delete();
                            }
                        }
                        DC_Customer_Resigned_Activity_Meta meta6 = new DC_Customer_Resigned_Activity_Meta();
                        meta6.RowID = RowId;
                        meta6.Factor = "PaymentDate";
                        if (DatePromise == null || DatePromise == "")
                            meta6.Value = "1900-01-01";
                        else
                            meta6.Value = DateTime.Parse(DatePromise).Date.ToString();

                        meta6.RowCreatedTime = DateTime.Now;
                        meta6.RowCreatedUser = currentUser.UserName;
                        meta6.RowLastUpdatedTime = DateTime.Now;
                        meta6.RowLastUpdatedUser = currentUser.UserName;
                        meta6.Save();



                        var _bank = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'Bank'", "").ToList();
                        if (_bank.Count > 0)
                        {
                            foreach (var item2 in _bank)
                            {
                                item2.Delete();
                            }
                        }
                        DC_Customer_Resigned_Activity_Meta meta7 = new DC_Customer_Resigned_Activity_Meta();
                        meta7.RowID = RowId;
                        meta7.Factor = "Bank";
                        if (Bank == null || Bank == "")
                            meta7.Value = "";
                        else
                            meta7.Value = Bank;

                        meta7.RowCreatedTime = DateTime.Now;
                        meta7.RowCreatedUser = currentUser.UserName;
                        meta7.RowLastUpdatedTime = DateTime.Now;
                        meta7.RowLastUpdatedUser = currentUser.UserName;
                        meta7.Save();

                        var _baselineSettledAmount = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'BaselineSettledAmount'", "").ToList();
                        if (_baselineSettledAmount.Count > 0)
                        {
                            foreach (var item2 in _baselineSettledAmount)
                            {
                                item2.Delete();
                            }
                        }

                        DC_Customer_Resigned_Activity_Meta metaBSA = new DC_Customer_Resigned_Activity_Meta();
                        metaBSA.RowID = RowId;
                        metaBSA.Factor = "BaselineSettledAmount";
                        if (BaselineSettledBalance == null || BaselineSettledBalance == "")
                            metaBSA.Value = "";
                        else
                            metaBSA.Value = BaselineSettledBalance;

                        metaBSA.RowCreatedTime = DateTime.Now;
                        metaBSA.RowCreatedUser = currentUser.UserName;
                        metaBSA.RowLastUpdatedTime = DateTime.Now;
                        metaBSA.RowLastUpdatedUser = currentUser.UserName;
                        metaBSA.Save();
                    }
                    else
                    {
                        // Neu khong chon thi xoa 
                        var _bank = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'Bank'", "").ToList();
                        if (_bank.Count > 0)
                        {
                            foreach (var item2 in _bank)
                            {
                                item2.Delete();
                            }
                        }

                        var Ngaythanhtoan = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'PaymentDate'", "").ToList();
                        if (Ngaythanhtoan.Count > 0)
                        {
                            foreach (var item2 in Ngaythanhtoan)
                            {
                                item2.Delete();
                            }
                        }

                        var _Money = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'Money'", "").ToList();
                        if (_Money.Count > 0)
                        {
                            foreach (var item2 in _Money)
                            {
                                item2.Delete();
                            }
                        }

                        var _baselineSettledAmount = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'BaselineSettledAmount'", "").ToList();
                        if (_baselineSettledAmount.Count > 0)
                        {
                            foreach (var item2 in _baselineSettledAmount)
                            {
                                item2.Delete();
                            }
                        }
                    }

                    var dateOfAppointment = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'DateOfAppointment'", "").ToList();
                    if (dateOfAppointment.Count > 0)
                    {
                        foreach (var item2 in dateOfAppointment)
                        {
                            item2.Delete();
                        }
                    }

                    DateTime tmp;
                    if (DateTime.TryParse(ap, out tmp))
                    {
                        DC_Customer_Resigned_Activity_Meta meta3 = new DC_Customer_Resigned_Activity_Meta();
                        meta3.RowID = RowId;
                        meta3.Factor = "DateOfAppointment";
                        meta3.Value = DateTime.Parse(ap).ToString();
                        meta3.RowCreatedTime = DateTime.Now;
                        meta3.RowCreatedUser = currentUser.UserName;
                        meta3.RowLastUpdatedTime = DateTime.Now;
                        meta3.RowLastUpdatedUser = currentUser.UserName;
                        meta3.Save();
                    }






                    var _personalContacted = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + RowId + "' AND [Factor] = 'PersonalContacted'", "").ToList();
                    if (_personalContacted.Count > 0)
                    {
                        foreach (var item2 in _personalContacted)
                        {
                            item2.Delete();
                        }
                    }
                    DC_Customer_Resigned_Activity_Meta meta8 = new DC_Customer_Resigned_Activity_Meta();
                    meta8.RowID = RowId;
                    meta8.Factor = "PersonalContacted";
                    if (PersonalContacted == null || PersonalContacted == "")
                        meta8.Value = "";
                    else
                        meta8.Value = PersonalContacted;

                    meta8.RowCreatedTime = DateTime.Now;
                    meta8.RowCreatedUser = currentUser.UserName;
                    meta8.RowLastUpdatedTime = DateTime.Now;
                    meta8.RowLastUpdatedUser = currentUser.UserName;
                    meta8.Save();


                    var descrptionActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + ActionCode + "'", "") == null ? "" : Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + ActionCode + "'", "").FirstOrDefault().Description;
                    var descrptionActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + Status + "'", "") == null ? "" : Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + Status + "'", "").FirstOrDefault().Description;
                    DC_Customer_Resigned_Activity.UpdateLastActivityInToBadDebt(CustomerID, descrptionActionStatus, descrptionActionCode, Note, Convert.ToDateTime(ap.ToString()), DateTime.Now);

                    
                    return Json(new { success = true, customerId = Locdau.convertToUnSign3(CustomerID) });
                }
                catch (Exception e)
                {

                    return Json(new { success = false, error = e.Message });
                }


            }
            else
            {
                return Json("Error: Please insert all the field with (*) ");
            }



        }

        [HttpPost]
        public ActionResult SaveActivity(string CustomerID, string BaselineSettledBalance, string ActionCode, string ContactMode, string Status, string Note, string ap, string Money, string DatePromise, string Bank, string PersonalContacted)
        {
            if (!string.IsNullOrEmpty(CustomerID) && !string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Note) && !string.IsNullOrEmpty(ContactMode) && !string.IsNullOrEmpty(ActionCode))
            {
                if (ap == "" || ap == null)
                {
                    ap = "1900-01-01";
                }

                DateTime doa = Convert.ToDateTime(ap);
                TimeSpan Time = doa - DateTime.Now;
                int day = Time.Days;
                if (day > 5)
                {
                    return Json("Error: Date Of Appointmen must <= 5days");
                }

                DC_Customer_Resigned_Activity activity = new DC_Customer_Resigned_Activity();
                activity.CustomerID = CustomerID;
                activity.ActionStatus = Status;
                activity.Note = Note;
                activity.RowCreatedTime = DateTime.Now;
                activity.RowCreatedUser = currentUser.UserName;


                try
                {
                    int id = activity.Save();

                    var contact = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'ContactMode'", "").ToList();
                    if (contact.Count > 0)
                    {
                        foreach (var item in contact)
                        {
                            item.Delete();
                        }
                    }
                    DC_Customer_Resigned_Activity_Meta meta = new DC_Customer_Resigned_Activity_Meta();
                    meta.RowID = id;
                    meta.Factor = "ContactMode";
                    meta.Value = ContactMode;
                    meta.RowCreatedTime = DateTime.Now;
                    meta.RowCreatedUser = currentUser.UserName;
                    meta.RowLastUpdatedTime = DateTime.Now;
                    meta.RowLastUpdatedUser = currentUser.UserName;
                    meta.Save();


                    var actionCode = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'ActionCode'", "").ToList();
                    if (actionCode.Count > 0)
                    {
                        foreach (var item2 in actionCode)
                        {
                            item2.Delete();
                        }
                    }
                    DC_Customer_Resigned_Activity_Meta meta2 = new DC_Customer_Resigned_Activity_Meta();
                    meta2.RowID = id;
                    meta2.Factor = "ActionCode";
                    meta2.Value = ActionCode;
                    meta2.RowCreatedTime = DateTime.Now;
                    meta2.RowCreatedUser = currentUser.UserName;
                    meta2.RowLastUpdatedTime = DateTime.Now;
                    meta2.RowLastUpdatedUser = currentUser.UserName;
                    meta2.Save();

                    //Neu actioncode = ACM01 - Payment to pay thi them factor Bank, Money, Date
                    if (ActionCode == "ACM01" || ActionCode.ToString() == "ACM01")
                    {
                        //Check money in number
                        double num;
                        if (double.TryParse(Money, out num))
                        {
                            // It's a number!
                            var _Money = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Money'", "").ToList();
                            if (_Money.Count > 0)
                            {
                                foreach (var item2 in _Money)
                                {
                                    item2.Delete();
                                }
                            }
                            DC_Customer_Resigned_Activity_Meta meta5 = new DC_Customer_Resigned_Activity_Meta();
                            meta5.RowID = id;
                            meta5.Factor = "Money";

                            if (Money == null || Money == "")
                                meta5.Value = "";
                            else
                                meta5.Value = Money;
                            meta5.RowCreatedTime = DateTime.Now;
                            meta5.RowCreatedUser = currentUser.UserName;
                            meta5.RowLastUpdatedTime = DateTime.Now;
                            meta5.RowLastUpdatedUser = currentUser.UserName;
                            meta5.Save();
                        }
                        else
                        {
                            return Json("Error: Please check money is number ");
                        }


                        var Ngaythanhtoan = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'PaymentDate'", "").ToList();
                        if (Ngaythanhtoan.Count > 0)
                        {
                            foreach (var item2 in Ngaythanhtoan)
                            {
                                item2.Delete();
                            }
                        }
                        DC_Customer_Resigned_Activity_Meta meta6 = new DC_Customer_Resigned_Activity_Meta();
                        meta6.RowID = id;
                        meta6.Factor = "PaymentDate";
                        if (DatePromise == null || DatePromise == "")
                            meta6.Value = "1900-01-01";
                        else
                            meta6.Value = DateTime.Parse(DatePromise).Date.ToString();

                        meta6.RowCreatedTime = DateTime.Now;
                        meta6.RowCreatedUser = currentUser.UserName;
                        meta6.RowLastUpdatedTime = DateTime.Now;
                        meta6.RowLastUpdatedUser = currentUser.UserName;
                        meta6.Save();

                        var _bank = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Bank'", "").ToList();
                        if (_bank.Count > 0)
                        {
                            foreach (var item2 in _bank)
                            {
                                item2.Delete();
                            }
                        }
                        DC_Customer_Resigned_Activity_Meta meta7 = new DC_Customer_Resigned_Activity_Meta();
                        meta7.RowID = id;
                        meta7.Factor = "Bank";
                        if (Bank == null || Bank == "")
                            meta7.Value = "";
                        else
                            meta7.Value = Bank;

                        meta7.RowCreatedTime = DateTime.Now;
                        meta7.RowCreatedUser = currentUser.UserName;
                        meta7.RowLastUpdatedTime = DateTime.Now;
                        meta7.RowLastUpdatedUser = currentUser.UserName;
                        meta7.Save();

                        var _baselineSettledAmount = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'BaselineSettledAmount'", "").ToList();
                        if (_baselineSettledAmount.Count > 0)
                        {
                            foreach (var item2 in _baselineSettledAmount)
                            {
                                item2.Delete();
                            }
                        }

                        DC_Customer_Resigned_Activity_Meta metaBSA = new DC_Customer_Resigned_Activity_Meta();
                        metaBSA.RowID = id;
                        metaBSA.Factor = "BaselineSettledAmount";
                        if (BaselineSettledBalance == null || BaselineSettledBalance == "")
                            metaBSA.Value = "";
                        else
                            metaBSA.Value = BaselineSettledBalance;

                        metaBSA.RowCreatedTime = DateTime.Now;
                        metaBSA.RowCreatedUser = currentUser.UserName;
                        metaBSA.RowLastUpdatedTime = DateTime.Now;
                        metaBSA.RowLastUpdatedUser = currentUser.UserName;
                        metaBSA.Save();
                    }
                    else
                    {
                        // Neu khong chon thi xoa 
                        var _bank = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Bank'", "").ToList();
                        if (_bank.Count > 0)
                        {
                            foreach (var item2 in _bank)
                            {
                                item2.Delete();
                            }
                        }

                        var Ngaythanhtoan = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'PaymentDate'", "").ToList();
                        if (Ngaythanhtoan.Count > 0)
                        {
                            foreach (var item2 in Ngaythanhtoan)
                            {
                                item2.Delete();
                            }
                        }

                        var _Money = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Money'", "").ToList();
                        if (_Money.Count > 0)
                        {
                            foreach (var item2 in _Money)
                            {
                                item2.Delete();
                            }
                        }

                        var _baselineSettledAmount = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'BaselineSettledAmount'", "").ToList();
                        if (_baselineSettledAmount.Count > 0)
                        {
                            foreach (var item2 in _baselineSettledAmount)
                            {
                                item2.Delete();
                            }
                        }
                    }

                    var dateOfAppointment = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'DateOfAppointment'", "").ToList();
                    if (dateOfAppointment.Count > 0)
                    {
                        foreach (var item2 in dateOfAppointment)
                        {
                            item2.Delete();
                        }
                    }

                    DateTime temp;

                    if (DateTime.TryParse(ap, out temp))
                    {
                        DC_Customer_Resigned_Activity_Meta meta4 = new DC_Customer_Resigned_Activity_Meta();
                        meta4.RowID = id;
                        meta4.Factor = "DateOfAppointment";
                        meta4.Value = ap.ToString();
                        meta4.RowCreatedTime = DateTime.Now;
                        meta4.RowCreatedUser = currentUser.UserName;
                        meta4.RowLastUpdatedTime = DateTime.Now;
                        meta4.RowLastUpdatedUser = currentUser.UserName;
                        meta4.Save();
                    }

                    var _personalContacted = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'PersonalContacted'", "").ToList();
                    if (_personalContacted.Count > 0)
                    {
                        foreach (var item2 in _personalContacted)
                        {
                            item2.Delete();
                        }
                    }
                    DC_Customer_Resigned_Activity_Meta meta8 = new DC_Customer_Resigned_Activity_Meta();
                    meta8.RowID = id;
                    meta8.Factor = "PersonalContacted";
                    if (Bank == null || Bank == "")
                        meta8.Value = "";
                    else
                        meta8.Value = PersonalContacted;

                    meta8.RowCreatedTime = DateTime.Now;
                    meta8.RowCreatedUser = currentUser.UserName;
                    meta8.RowLastUpdatedTime = DateTime.Now;
                    meta8.RowLastUpdatedUser = currentUser.UserName;
                    meta8.Save();


                    // update last activity into baddebt:

                    var descrptionActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + ActionCode + "'", "") == null ? "" : Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + ActionCode + "'", "").FirstOrDefault().Description;
                    var descrptionActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + Status + "'", "") == null ? "" : Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + Status + "'", "").FirstOrDefault().Description;
                    DC_Customer_Resigned_Activity.UpdateLastActivityInToBadDebt(CustomerID, descrptionActionStatus, descrptionActionCode, Note, Convert.ToDateTime(ap.ToString()), DateTime.Now);


                    return Json(new { success = true, customerId = Locdau.convertToUnSign3(CustomerID) });

                }
                catch (Exception)
                {

                    return Json(new { success = false });
                }
            }
            else
            {
                return Json("Error: Please insert all the field with (*) ");
            }



        }

        [HttpPost]
        public ActionResult SaveCustomerResigned(string CustomerId, string ResignedDate, string InformedDate, string ResignedReason, string Note2, string Reference, bool BaselineAmount, string SetteledBalance)
        {


            if (!string.IsNullOrEmpty(CustomerId) && !string.IsNullOrEmpty(ResignedDate) && !string.IsNullOrEmpty(ResignedDate) && !string.IsNullOrEmpty(ResignedReason) && !string.IsNullOrEmpty(Note2) && !string.IsNullOrEmpty(Reference))
            {

                var exist = DC_Customer_Resigned.GetDC_Customer_Resigned(CustomerId);
                if (exist != null)
                {

                    return Json("Error : CustomerID is already existed");
                }

                var exist1 = DW_DC_Customer.GetDW_DC_Customer(CustomerId);
                if (exist1 == null)
                {

                    return Json("Error: CustomerID is not existed");
                }

                DC_Customer_Resigned mcr = new DC_Customer_Resigned();
                mcr.CustomerID = CustomerId.ToUpper();
                mcr.ResignedDate = Convert.ToDateTime(ResignedDate);
                mcr.InformedDate = Convert.ToDateTime(InformedDate);
                mcr.ResignedReason = ResignedReason;
                mcr.Note = Note2;
                mcr.RowCreatedTime = DateTime.Now;
                mcr.RowCreatedUser = currentUser.UserName;
                mcr.Save();
                log.Info("Add resigned for Customer: " + CustomerId);



                var listCusInfo = DC_Customer_Resigned_Activity_CusInfo_Meta.GetDC_Customer_Resigned_Activity_CusInfo_Metas("[CustomerID]='" + CustomerId + "' AND [Factor] = 'ReferenceInfo'", "").ToList();
                if (listCusInfo.Count > 0)
                {
                    foreach (var item in listCusInfo)
                    {
                        item.Delete();
                    }
                }


                DC_Customer_Resigned_Activity_CusInfo_Meta meta = new DC_Customer_Resigned_Activity_CusInfo_Meta();
                meta.CustomerID = CustomerId;
                meta.Factor = "ReferenceInfo";
                meta.Value = Reference;
                meta.RowCreatedTime = DateTime.Now;
                meta.RowCreatedUser = currentUser.UserName;
                meta.RowLastUpdatedTime = DateTime.Now;
                meta.RowLastUpdatedUser = currentUser.UserName;
                meta.Save();

                if (BaselineAmount == true)
                {
                    var listCusBaselineAmount = DC_Customer_Resigned_Activity_CusInfo_Meta.GetDC_Customer_Resigned_Activity_CusInfo_Metas("[CustomerID]='" + CustomerId + "' AND [Factor] = 'SettledBalance'", "").ToList();
                    if (listCusBaselineAmount.Count > 0)
                    {
                        foreach (var item in listCusBaselineAmount)
                        {
                            item.Delete();
                        }
                    }


                    DC_Customer_Resigned_Activity_CusInfo_Meta meta2 = new DC_Customer_Resigned_Activity_CusInfo_Meta();
                    meta2.CustomerID = CustomerId;
                    meta2.Factor = "SettledBalance";
                    meta2.Value = double.Parse(SetteledBalance).ToString();
                    meta2.RowCreatedTime = DateTime.Now;
                    meta2.RowCreatedUser = currentUser.UserName;
                    meta2.RowLastUpdatedTime = DateTime.Now;
                    meta2.RowLastUpdatedUser = currentUser.UserName;
                    meta2.Save();
                }



            }
            else
            {
                return Json("Error: Please insert all the field with (*) ");
            }


            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateCustomerResigned(string CustomerId, string ResignedDate, string InformedDate, string ResignedReason, string Note2, string Reference, bool BaselineAmount, string SetteledBalance)
        {


            if (!string.IsNullOrEmpty(CustomerId) && !string.IsNullOrEmpty(ResignedDate) && !string.IsNullOrEmpty(ResignedDate) && !string.IsNullOrEmpty(ResignedReason) && !string.IsNullOrEmpty(Note2) && !string.IsNullOrEmpty(Reference))
            {



                DC_Customer_Resigned mcr = new DC_Customer_Resigned();
                mcr.CustomerID = CustomerId.ToUpper();
                mcr.ResignedDate = Convert.ToDateTime(ResignedDate);
                mcr.InformedDate = Convert.ToDateTime(InformedDate);
                mcr.ResignedReason = ResignedReason;
                mcr.Note = Note2;
                mcr.RowLastUpdatedTime = DateTime.Now;
                mcr.RowLastUpdatedUser = currentUser.UserName;
                mcr.Update();
                log.Info("Add resigned for Customer: " + CustomerId);

                var listCusInfo = DC_Customer_Resigned_Activity_CusInfo_Meta.GetDC_Customer_Resigned_Activity_CusInfo_Metas("[CustomerID]='" + CustomerId + "' AND [Factor] = 'ReferenceInfo'", "").ToList();
                if (listCusInfo.Count > 0)
                {
                    foreach (var item in listCusInfo)
                    {
                        item.Delete();
                    }
                }

                DC_Customer_Resigned_Activity_CusInfo_Meta meta = new DC_Customer_Resigned_Activity_CusInfo_Meta();
                meta.CustomerID = CustomerId;
                meta.Factor = "ReferenceInfo";
                meta.Value = Reference;
                meta.RowCreatedTime = DateTime.Now;
                meta.RowCreatedUser = currentUser.UserName;
                meta.RowLastUpdatedTime = DateTime.Now;
                meta.RowLastUpdatedUser = currentUser.UserName;
                meta.Save();

                if (BaselineAmount == true)
                {
                    var listCusBaselineAmount = DC_Customer_Resigned_Activity_CusInfo_Meta.GetDC_Customer_Resigned_Activity_CusInfo_Metas("[CustomerID]='" + CustomerId + "' AND [Factor] = 'SettledBalance'", "").ToList();
                    if (listCusBaselineAmount.Count > 0)
                    {
                        foreach (var item in listCusBaselineAmount)
                        {
                            item.Delete();
                        }
                    }


                    DC_Customer_Resigned_Activity_CusInfo_Meta meta2 = new DC_Customer_Resigned_Activity_CusInfo_Meta();
                    meta2.CustomerID = CustomerId;
                    meta2.Factor = "SettledBalance";
                    meta2.Value = double.Parse(SetteledBalance).ToString();
                    meta2.RowCreatedTime = DateTime.Now;
                    meta2.RowCreatedUser = currentUser.UserName;
                    meta2.RowLastUpdatedTime = DateTime.Now;
                    meta2.RowLastUpdatedUser = currentUser.UserName;
                    meta2.Save();
                }
            }
            else
            {


                return Json("Error: Please insert all the field with (*) ");
            }


            return Json(new { success = true });
        }

        [DonutOutputCache(CacheProfile = "1Hour")]
        public ActionResult AirtimeCustomer_Read([DataSourceRequest]
                                          DataSourceRequest request, string customerID)
        {
            var data = DW_DC_Order.GetAirtimeOfCustomer(customerID);
            return Json(data.ToDataSourceResult(request));
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
        public async Task<ActionResult> CustomerRepaymentHistory_Read([DataSourceRequest] DataSourceRequest request, string cusID)
        {
            var data = await DC_Customer_Resigned.GetAllCustomerRepaymentHistory(cusID);
            return Json(data.ToDataSourceResult(request));
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


        public FileResult ExportTemplateActivity([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Activity_CustomerResigned.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //Unit Sheet Action Status:
                ExcelWorksheet actionStatusSheet = excelPkg.Workbook.Worksheets["ActionStatus"];
                var listActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Status'", "");
                int rowIndex0 = 1;
                foreach (var item in listActionStatus)
                {
                    rowIndex0++;
                    actionStatusSheet.Cells[rowIndex0, 1].Value = (String.IsNullOrEmpty(item.CodeID) ? "" : item.CodeID).Trim() + ":" + (String.IsNullOrEmpty(item.Description) ? "" : item.Description).Trim();
                }

                //Unit Sheet Action Code:
                ExcelWorksheet actionCodeSheet = excelPkg.Workbook.Worksheets["ActionCode"];
                var listActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ActionCode'", "");
                int rowIndex1 = 1;
                foreach (var item in listActionCode)
                {
                    rowIndex1++;
                    actionCodeSheet.Cells[rowIndex1, 1].Value = (String.IsNullOrEmpty(item.CodeID) ? "" : item.CodeID).Trim() + ":" + (String.IsNullOrEmpty(item.Description) ? "" : item.Description).Trim();
                }

                //Unit Sheet Contact Mode:
                ExcelWorksheet ContactModeSheet = excelPkg.Workbook.Worksheets["ContactMode"];
                var listContactMode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ContactMode'", "");
                int rowIndex2 = 1;
                foreach (var item in listContactMode)
                {
                    rowIndex2++;
                    ContactModeSheet.Cells[rowIndex2, 1].Value = (String.IsNullOrEmpty(item.CodeID) ? "" : item.CodeID).Trim() + ":" + (String.IsNullOrEmpty(item.Description) ? "" : item.Description).Trim();
                }

                //Unit Sheet PersonalContact:
                ExcelWorksheet PersonalContactSheet = excelPkg.Workbook.Worksheets["PersonalContact"];
                var listPersonalContact = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Personal Contacted'", "");
                int rowIndex3 = 1;
                foreach (var item in listPersonalContact)
                {
                    rowIndex3++;
                    PersonalContactSheet.Cells[rowIndex3, 1].Value = (String.IsNullOrEmpty(item.CodeID) ? "" : item.CodeID).Trim() + ":" + (String.IsNullOrEmpty(item.Description) ? "" : item.Description).Trim();
                }



                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Activity_CustomerResigned_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Activity_CustomerResigned_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }

        }

        [HttpPost]
        public ActionResult ImportActivityFromExcel()
        {
            try
            {
                if (Request.Files["FileUploadActivity"] != null && Request.Files["FileUploadActivity"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUploadActivity"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUploadActivity"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUploadActivity"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUploadActivity"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;

                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Activity_CustomerResigned.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Activity Customer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Activity Customer"];

                        //remove row
                        int totalRows = oSheet.Dimension.End.Row;
                        //eSheet.DeleteRow(2, totalRows, true);

                        //var realdata = WorksheetToDataTable(oSheet);
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string customerID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string actionStatus = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string actionCode = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string contactMode = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string personalContact = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string Note = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string dateOfAppointment = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            string Money = oSheet.Cells[i, 8].Value != null ? oSheet.Cells[i, 8].Value.ToString() : "";
                            string datePromise = oSheet.Cells[i, 9].Value != null ? oSheet.Cells[i, 9].Value.ToString() : "";
                            string Bank = oSheet.Cells[i, 10].Value != null ? oSheet.Cells[i, 10].Value.ToString() : "";


                            try
                            {
                                if (String.IsNullOrEmpty(customerID) || String.IsNullOrEmpty(actionStatus) || String.IsNullOrEmpty(actionCode) || String.IsNullOrEmpty(contactMode) || String.IsNullOrEmpty(personalContact))
                                {
                                    eSheet.Cells[rownumber, 1].Value = customerID;
                                    eSheet.Cells[rownumber, 2].Value = actionStatus;
                                    eSheet.Cells[rownumber, 3].Value = actionCode.ToString();
                                    eSheet.Cells[rownumber, 4].Value = contactMode.ToString();
                                    eSheet.Cells[rownumber, 5].Value = personalContact;
                                    eSheet.Cells[rownumber, 6].Value = Note;
                                    eSheet.Cells[rownumber, 7].Value = dateOfAppointment;
                                    eSheet.Cells[rownumber, 8].Value = Money;
                                    eSheet.Cells[rownumber, 9].Value = datePromise;
                                    eSheet.Cells[rownumber, 10].Value = Bank;
                                    eSheet.Cells[rownumber, 11].Value = "CustomerID, ActionStatus, ActionCode,ContactMode,PersonalContact required";
                                    rownumber++;
                                }

                                DC_Customer_Resigned cus = DC_Customer_Resigned.GetDC_Customer_Resigned(customerID);
                                if (cus == null)
                                {
                                    eSheet.Cells[rownumber, 1].Value = customerID;
                                    eSheet.Cells[rownumber, 2].Value = actionStatus;
                                    eSheet.Cells[rownumber, 3].Value = actionCode.ToString();
                                    eSheet.Cells[rownumber, 4].Value = contactMode.ToString();
                                    eSheet.Cells[rownumber, 5].Value = personalContact;
                                    eSheet.Cells[rownumber, 6].Value = Note;
                                    eSheet.Cells[rownumber, 7].Value = dateOfAppointment;
                                    eSheet.Cells[rownumber, 8].Value = Money;
                                    eSheet.Cells[rownumber, 9].Value = datePromise;
                                    eSheet.Cells[rownumber, 10].Value = Bank;
                                    eSheet.Cells[rownumber, 11].Value = "CustomerID not exist in BadDebt";
                                    rownumber++;
                                }


                                if (dateOfAppointment == "" || dateOfAppointment == null)
                                {
                                    dateOfAppointment = "1900-01-01";
                                }

                                DateTime doa = Convert.ToDateTime(dateOfAppointment);
                                TimeSpan Time = doa - DateTime.Now;
                                int day = Time.Days;
                                if (day > 5)
                                {
                                    eSheet.Cells[rownumber, 1].Value = customerID;
                                    eSheet.Cells[rownumber, 2].Value = actionStatus;
                                    eSheet.Cells[rownumber, 3].Value = actionCode.ToString();
                                    eSheet.Cells[rownumber, 4].Value = contactMode.ToString();
                                    eSheet.Cells[rownumber, 5].Value = personalContact;
                                    eSheet.Cells[rownumber, 6].Value = Note;
                                    eSheet.Cells[rownumber, 7].Value = dateOfAppointment;
                                    eSheet.Cells[rownumber, 8].Value = Money;
                                    eSheet.Cells[rownumber, 9].Value = datePromise;
                                    eSheet.Cells[rownumber, 10].Value = Bank;
                                    eSheet.Cells[rownumber, 11].Value = "Date Of Appointmen must <= 5days";
                                    rownumber++;

                                }
                                else
                                {
                                    DC_Customer_Resigned_Activity a = new DC_Customer_Resigned_Activity();
                                    a.CustomerID = customerID;
                                    a.ActionStatus = actionStatus.Substring(0, actionStatus.IndexOf(":"));
                                    a.Note = Note;
                                    a.RowCreatedTime = DateTime.Now;
                                    a.RowCreatedUser = currentUser.UserName;
                                    int id = a.Save();

                                    var contact = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'ContactMode'", "").ToList();
                                    if (contact.Count > 0)
                                    {
                                        foreach (var item in contact)
                                        {
                                            item.Delete();
                                        }
                                    }
                                    DC_Customer_Resigned_Activity_Meta meta = new DC_Customer_Resigned_Activity_Meta();
                                    meta.RowID = id;
                                    meta.Factor = "ContactMode";
                                    meta.Value = contactMode.Substring(0, contactMode.IndexOf(":"));
                                    meta.RowCreatedTime = DateTime.Now;
                                    meta.RowCreatedUser = currentUser.UserName;
                                    meta.RowLastUpdatedTime = DateTime.Now;
                                    meta.RowLastUpdatedUser = currentUser.UserName;
                                    meta.Save();

                                    var aCode = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'ActionCode'", "").ToList();
                                    if (aCode.Count > 0)
                                    {
                                        foreach (var item2 in aCode)
                                        {
                                            item2.Delete();
                                        }
                                    }

                                    DC_Customer_Resigned_Activity_Meta meta2 = new DC_Customer_Resigned_Activity_Meta();
                                    meta2.RowID = id;
                                    meta2.Factor = "ActionCode";
                                    meta2.Value = actionCode.Substring(0, actionCode.IndexOf(":"));
                                    meta2.RowCreatedTime = DateTime.Now;
                                    meta2.RowCreatedUser = currentUser.UserName;
                                    meta2.RowLastUpdatedTime = DateTime.Now;
                                    meta2.RowLastUpdatedUser = currentUser.UserName;
                                    meta2.Save();

                                    if (actionCode.Substring(0, actionCode.IndexOf(":")) == "ACM01")
                                    {
                                        //Check money in number
                                        double num;
                                        if (double.TryParse(Money, out num))
                                        {
                                            // It's a number!
                                            var _Money = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Money'", "").ToList();
                                            if (_Money.Count > 0)
                                            {
                                                foreach (var item2 in _Money)
                                                {
                                                    item2.Delete();
                                                }
                                            }
                                            DC_Customer_Resigned_Activity_Meta meta5 = new DC_Customer_Resigned_Activity_Meta();
                                            meta5.RowID = id;
                                            meta5.Factor = "Money";

                                            if (Money == null || Money == "")
                                                meta5.Value = "";
                                            else
                                                meta5.Value = Money;
                                            meta5.RowCreatedTime = DateTime.Now;
                                            meta5.RowCreatedUser = currentUser.UserName;
                                            meta5.RowLastUpdatedTime = DateTime.Now;
                                            meta5.RowLastUpdatedUser = currentUser.UserName;
                                            meta5.Save();
                                        }
                                        else
                                        {
                                            return Json("Error: Please check money is number ");
                                        }


                                        var Ngaythanhtoan = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'PaymentDate'", "").ToList();
                                        if (Ngaythanhtoan.Count > 0)
                                        {
                                            foreach (var item2 in Ngaythanhtoan)
                                            {
                                                item2.Delete();
                                            }
                                        }
                                        DC_Customer_Resigned_Activity_Meta meta6 = new DC_Customer_Resigned_Activity_Meta();
                                        meta6.RowID = id;
                                        meta6.Factor = "PaymentDate";
                                        if (datePromise == null || datePromise == "")
                                            meta6.Value = "1900-01-01";
                                        else
                                            meta6.Value = DateTime.Parse(datePromise).Date.ToString();

                                        meta6.RowCreatedTime = DateTime.Now;
                                        meta6.RowCreatedUser = currentUser.UserName;
                                        meta6.RowLastUpdatedTime = DateTime.Now;
                                        meta6.RowLastUpdatedUser = currentUser.UserName;
                                        meta6.Save();

                                        var _bank = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Bank'", "").ToList();
                                        if (_bank.Count > 0)
                                        {
                                            foreach (var item2 in _bank)
                                            {
                                                item2.Delete();
                                            }
                                        }
                                        DC_Customer_Resigned_Activity_Meta meta7 = new DC_Customer_Resigned_Activity_Meta();
                                        meta7.RowID = id;
                                        meta7.Factor = "Bank";
                                        if (Bank == null || Bank == "")
                                            meta7.Value = "";
                                        else
                                            meta7.Value = Bank;

                                        meta7.RowCreatedTime = DateTime.Now;
                                        meta7.RowCreatedUser = currentUser.UserName;
                                        meta7.RowLastUpdatedTime = DateTime.Now;
                                        meta7.RowLastUpdatedUser = currentUser.UserName;
                                        meta7.Save();
                                    }
                                    else
                                    {
                                        // Neu khong chon thi xoa 
                                        var _bank = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Bank'", "").ToList();
                                        if (_bank.Count > 0)
                                        {
                                            foreach (var item2 in _bank)
                                            {
                                                item2.Delete();
                                            }
                                        }

                                        var Ngaythanhtoan = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'PaymentDate'", "").ToList();
                                        if (Ngaythanhtoan.Count > 0)
                                        {
                                            foreach (var item2 in Ngaythanhtoan)
                                            {
                                                item2.Delete();
                                            }
                                        }

                                        var _Money = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'Money'", "").ToList();
                                        if (_Money.Count > 0)
                                        {
                                            foreach (var item2 in _Money)
                                            {
                                                item2.Delete();
                                            }
                                        }
                                    }


                                    var dap = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'DateOfAppointment'", "").ToList();
                                    if (dap.Count > 0)
                                    {
                                        foreach (var item2 in dap)
                                        {
                                            item2.Delete();
                                        }
                                    }
                                    DC_Customer_Resigned_Activity_Meta meta4 = new DC_Customer_Resigned_Activity_Meta();
                                    meta4.RowID = id;
                                    meta4.Factor = "DateOfAppointment";
                                    meta4.Value = DateTime.Parse(dateOfAppointment.ToString()).ToString();
                                    meta4.RowCreatedTime = DateTime.Now;
                                    meta4.RowCreatedUser = currentUser.UserName;
                                    meta4.RowLastUpdatedTime = DateTime.Now;
                                    meta4.RowLastUpdatedUser = currentUser.UserName;
                                    meta4.Save();

                                    var _personalContacted = DC_Customer_Resigned_Activity_Meta.GetDC_Customer_Resigned_Activity_Metas("[RowID]='" + id + "' AND [Factor] = 'PersonalContacted'", "").ToList();
                                    if (_personalContacted.Count > 0)
                                    {
                                        foreach (var item2 in _personalContacted)
                                        {
                                            item2.Delete();
                                        }
                                    }
                                    DC_Customer_Resigned_Activity_Meta meta8 = new DC_Customer_Resigned_Activity_Meta();
                                    meta8.RowID = id;
                                    meta8.Factor = "PersonalContacted";
                                    if (Bank == null || Bank == "")
                                        meta8.Value = "";
                                    else
                                        meta8.Value = personalContact.Substring(0, personalContact.IndexOf(":"));

                                    meta8.RowCreatedTime = DateTime.Now;
                                    meta8.RowCreatedUser = currentUser.UserName;
                                    meta8.RowLastUpdatedTime = DateTime.Now;
                                    meta8.RowLastUpdatedUser = currentUser.UserName;
                                    meta8.Save();

                                    
                                    var descrptionActionCode = Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + actionCode.Substring(0, actionCode.IndexOf(":")) + "'", "") == null ? "" : Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + actionCode.Substring(0, actionCode.IndexOf(":")) + "'", "").FirstOrDefault().Description;
                                    var descrptionActionStatus = Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + actionStatus.Substring(0, actionStatus.IndexOf(":")) + "'", "") == null ? "" : Deca_Code_Master.GetDeca_Code_Masters("[CodeID]='" + actionStatus.Substring(0, actionStatus.IndexOf(":")) + "'", "").FirstOrDefault().Description;
                                    DC_Customer_Resigned_Activity.UpdateLastActivityInToBadDebt(customerID, descrptionActionStatus, descrptionActionCode, Note, Convert.ToDateTime(dateOfAppointment.ToString()), DateTime.Now);


                                }

                                total++;

                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = customerID;
                                eSheet.Cells[rownumber, 2].Value = actionStatus;
                                eSheet.Cells[rownumber, 3].Value = actionCode.ToString();
                                eSheet.Cells[rownumber, 4].Value = contactMode.ToString();
                                eSheet.Cells[rownumber, 5].Value = personalContact;
                                eSheet.Cells[rownumber, 6].Value = Note;
                                eSheet.Cells[rownumber, 7].Value = dateOfAppointment;
                                eSheet.Cells[rownumber, 8].Value = Money;
                                eSheet.Cells[rownumber, 9].Value = datePromise;
                                eSheet.Cells[rownumber, 10].Value = Bank;
                                eSheet.Cells[rownumber, 11].Value = e.Message;
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
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        #region SendSms

        [HttpPost]
        public ActionResult SendMessage(string phoneNumber, string message, string issend, string _cus, string _telename, string _letephone, string _other, string templateid, string cusID)
        {
            List<DW_SMSMT> listError = new List<DW_SMSMT>();
            string listResend = "";
            int success = 0;
            string currentUserID = currentUser.UserName;
            string _mess = "";
            _mess = ReturnMess(message, _cus, _telename, _letephone, _other);

            var dauso = DC_NumberNetwork.GetAllDC_NumberNetworks();

            var limit = DC_SMS_Config.GetLimit(currentUser.UserName);
            foreach (string phone in phoneNumber.Split(','))
            {
                DW_SMSMT mt = new DW_SMSMT(0, "", "", "8099", "Manual Message", phone.Trim(), _mess, 1, DateTime.Now, "New", 0, true, DateTime.Now, currentUserID, DateTime.Now, currentUserID);

                if (!string.IsNullOrEmpty(phone.Trim()))
                {
                    if (!mt.CheckPhoneNumber(dauso))
                    {
                        //Kiem tra so dien thoai.Neu sai thi thong bao loi 
                        mt.ImportNote = "Invalid Phone number";
                        listError.Add(mt);
                    }
                    else
                    {

                        long id = mt.Save(limit);
                        if (id > 0)
                        {
                            if (issend == "false")
                            {
                                continue;
                            }
                            //MOReceiverSoapClient mtClient = new MOReceiverSoapClient();
                            //MTservice sms = new MTservice();
                            //sms.ID = 0;
                            //sms.phoneNumber = convertPhone(phone.Trim());
                            //sms.message = mt.Message;
                            //sms.serviceID = "8099";
                            //sms.commandCode = "ECC";
                            //sms.messageType = "1";
                            //sms.requestID = id.ToString();
                            //sms.totalMessage = "1";
                            //sms.messageIndex = "1";
                            //sms.isMore = "0";
                            //sms.contentType = "0";
                            //sms.RequestDate = DateTime.Now;
                            //string result = "";
                            //try
                            //{
                            //    result = mtClient.messageSend(sms);
                            //}
                            //catch (Exception ex)
                            //{
                            //    DW_SMSMT updateStatus = DW_SMSMT.GetDW_SMSMT(id);
                            //    updateStatus.RowLastUpdatedTime = DateTime.Now;
                            //    updateStatus.RowLastUpdatedUser = currentUserID;
                            //    updateStatus.Status = "Fail";
                            //    updateStatus.Update();
                            //    mt.ImportNote = ex.Message;
                            //    listResend += mt.RowID + ",";
                            //    listError.Add(mt);
                            //    continue;
                            //}

                            //if (result == "1")
                            //{
                            //    DW_SMSMT updateStatus = DW_SMSMT.GetDW_SMSMT(id);
                            //    updateStatus.RowLastUpdatedTime = DateTime.Now;
                            //    updateStatus.RowLastUpdatedUser = currentUserID;
                            //    updateStatus.Status = "Success";
                            //    updateStatus.Update(); success++;
                            //}
                            //else
                            //{
                            //    DW_SMSMT updateStatus = DW_SMSMT.GetDW_SMSMT(id);
                            //    updateStatus.RowLastUpdatedTime = DateTime.Now;
                            //    updateStatus.RowLastUpdatedUser = currentUserID;
                            //    updateStatus.Status = "Fail";
                            //    updateStatus.Update();
                            //    mt.ImportNote = "Has error sending message!";
                            //    listResend += mt.RowID + ",";
                            //    listError.Add(mt);
                            //}
                        }
                        else
                        {
                            mt.ImportNote = "Has error saving request!";
                            listResend += mt.UserID + ",";
                            listError.Add(mt);
                        }
                    }
                }
            }

            return Json(new { data = listError, resend = listResend, success = success });

        }


        public string ReturnMess(string message, string _cus, string _telename, string _letephone, string _other)
        {
            StringBuilder _mess = new StringBuilder(message);

            _mess.Replace("[CustomerName]", _cus);
            _mess.Replace("[Other]", _other);
            _mess.Replace("[TelesalesName]", _telename);
            _mess.Replace("[TelesalesPhone]", _letephone);

            return _mess.ToString();
        }

        static bool CheckPhoneNumber(string number)
        {
            long c = 0;
            if (!long.TryParse(number, out c))
                return false;
            if (number.Length < 10 || number.Length > 11)
                return false;

            var dauso = DC_NumberNetwork.GetAllDC_NumberNetworks();
            var number2 = number.Substring(0, number.Length - 7);
            foreach (var item in dauso)
            {
                if (number2 == item.Number)
                {
                    return true;
                }
            }
            return false;
        }

        static string convertPhone(string phone)
        {
            string result = "84" + phone.Substring(1, phone.Length - 1);
            return result;
        }

        public string GetTemplateID(string SmsTempID)
        {
            var data = DC_SMS_Template.GetDC_SMS_Template(int.Parse(SmsTempID.ToString()));
            string chuoi = data.Message;
            return chuoi;
        }

        public string GetTemplateMax(string SmsTempID)
        {
            var data = DC_SMS_Template.GetDC_SMS_Template(int.Parse(SmsTempID.ToString()));
            string chuoi = data.MaxNumber.ToString();
            return chuoi;
        }
        #endregion

        public void Call(string mobile)
        {
            Helpers.XliteAPI.Dial(mobile);
        }

        public FileResult ExportReport([DataSourceRequest]
                                 DataSourceRequest request, string startDate, string endDate)
        {
            if (asset.Delete)
            {
                //Get the data representing the current grid state - page, sort and filter

                IEnumerable datas;

                datas = DC_Customer_Resigned.GetReportActiveBadDebt(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
                //IEnumerable datas = DC_OrderFulfillment.GetAllDC_DBOrderFulfillment("1=1").ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_Report_Active_BadDebt.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("List");

                int rowNumber = 1;
                
                var listContactMode = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='ContactMode'", "");
                var listActionReason = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Customer Resigned Reason'", "");

                //Populate the sheet with values from the grid data
                foreach (DC_Customer_Resigned data in datas)
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    int i = 0;
                    //Set values for the cells
                    row.CreateCell(i).SetCellValue(data.OrganizationID); i++;
                    row.CreateCell(i).SetCellValue(data.BD); i++;
                    row.CreateCell(i).SetCellValue(data.BDE); i++;
                    
                    row.CreateCell(i).SetCellValue(data.CustomerID); i++;
                    row.CreateCell(i).SetCellValue(data.FullName); i++;
                    row.CreateCell(i).SetCellValue(data.MobilePhone); i++;
                    //row.CreateCell(i).SetCellValue(data.ResignedReason); i++;
                    foreach (Deca_Code_Master l1 in listActionReason)
                    {
                        if (data.ResignedReason == l1.CodeID)
                        {
                            row.CreateCell(i).SetCellValue(l1.Description);
                            break;
                        }
                        else
                        {
                            row.CreateCell(i).SetCellValue("");
                        }
                    } i++;
                    row.CreateCell(i).SetCellValue(data.SettledBalance); i++;
                    if (data.PD.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        row.CreateCell(i).SetCellValue(data.PD.ToString());
                    }
                    else
                    {
                        row.CreateCell(i).SetCellValue("");
                    } i++;
                 
                    row.CreateCell(i).SetCellValue(data.BaselineSettledBalance); i++;
                    row.CreateCell(i).SetCellValue(data.DurationDebt); i++;
                    row.CreateCell(i).SetCellValue(data.ActionStatus); i++;
                    row.CreateCell(i).SetCellValue(data.ActionCode); i++;
                    foreach (Deca_Code_Master l2 in listContactMode)
                    {
                        if (data.ContactMode == l2.CodeID)
                        {
                            row.CreateCell(i).SetCellValue(l2.Description);
                            break;
                        }
                        else
                        {
                            row.CreateCell(i).SetCellValue("");
                        }
                    } i++;
                    //row.CreateCell(i).SetCellValue(data.ContactMode); i++;
                    row.CreateCell(i).SetCellValue(data.Note); i++;
                    row.CreateCell(i).SetCellValue(data.BaselineSettledAmount); i++;
                    row.CreateCell(i).SetCellValue(data.Money); i++;
                    if (data.PaymentDate.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        row.CreateCell(i).SetCellValue(data.PaymentDate.ToString());
                    }
                    else
                    {
                        row.CreateCell(i).SetCellValue("");
                    } i++;
                    
                    row.CreateCell(i).SetCellValue(data.Bank); i++;
                    row.CreateCell(i).SetCellValue(data.RowCreatedUser); i++;
                    if (data.RowCreatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        row.CreateCell(i).SetCellValue(data.RowCreatedTime.ToShortDateString());
                    }
                    else
                    {
                        row.CreateCell(i).SetCellValue("");
                    } i++;
                    
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                workbook.Write(output);

                //Return the result to the end user
                log.Info("Export DC_Report_Active_BadDebt");
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Report_Active_BadDebt" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                log.Info("Don't have permission to export DC_Report_Active_BadDebt");
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_Report_Active_BadDebt" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }

        }

    }
}
