using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using System.IO;
using ERPAPD.Helpers;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using System.Text;
using OfficeOpenXml;


namespace ERPAPD.Controllers
{
    public class CSInfoCustomerController : CustomController
    {
        //
        // GET: /CSInfoCustomer/
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //this.parentAsset = Asset.GetAsset(1);
            base.Initialize(requestContext);
        }
        public ActionResult Index()
        {
            if (asset.View)
            {

                ViewBag.UserName = "administartor";// currentUser.UserName;
                ViewBag.PhoneUser = currentUser.Phone;
                var GroupUser = currentUser.Groups;
                var TemplateGetUser = DC_SMS_Template.GetAllDC_SMS_Templates();
                    
                ViewBag.ListTemplate = TemplateGetUser;
                ViewBag.listCategory = DC_Ticket_Category.GetAllDC_Ticket_Categorys().Where(s => s.Status == "True");

                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listUser = dbConn.Select<Users>();
                }

                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.Registration = dbConn.Select<DC_CSRegistration>();

                    //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    //{
                    //    OrmLiteConfig.DialectProvider.UseUnicode = true;
                    //    dbConn.DropTables(typeof(DC_TeleSale_AnnouncementByCS));
                    //    const bool overwrite = false;
                    //    dbConn.CreateTables(overwrite, typeof(DC_TeleSale_AnnouncementByCS));
                    //}

                  //  ViewBag.listDepartment = Deca_Department.GetDeca_Departments_TrackingOrder();
                    ViewBag.listWH = DC_WareHouse.GetAllDC_WareHouses().Where(s => s.Status == "Active");
                    ViewBag.listOrderReason = DC_Tracking_Order_ReasonMail.GetAllDC_Tracking_Order_ReasonMails().Where(s => s.Status == "True");

                    var data = dbConn.Select<DC_AccessRightsAnnoucement>("Select * from DC_AccessRightsAnnoucement Where UserID = '" + currentUser.Id + "'");

                    //if (currentUserGroups.Where(s => s.Name == "SuperAdmin").Count() > 0 || data.Count > 0)
                    //{
                        ViewBag.AnnPermision = true;
                    //}
                    //else
                    //{
                    //    ViewBag.AnnPermision = false;
                    //}
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //Add store
        public ActionResult Search_Customer(string keyword)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DW_DC_Customer>();
                data = dbConn.SqlList<DW_DC_Customer>("exec p_SelectDC_CS_Customer_InfoGroup '" + keyword.Trim().Replace("'", "''") + "'").ToList();
                return Json(data);
            }
        }

        public ActionResult CustomerInfo(string customerID)
        {
            var data = DW_DC_Customer.GetCustomerDataByCustomerIDForCS(customerID);
            return Json(data);
        }

        public ActionResult OrganizationInfo(string OrganizationID)
        {
            var data = new DW_DC_Organization();
            data = DW_DC_Organization.GetListOrganizationForCSByOrgID(OrganizationID);
            return Json(data);
        }

        public ActionResult OrgInfo(string keyword)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DW_DC_Customer();
                data = dbConn.SqlList<DW_DC_Customer>("exec p_SelectDC_CS_OrgInfo '" + keyword + "'").FirstOrDefault();
                return Json(data);
            }
        }

        public ActionResult DW_DC_Customer_ReadSearch([DataSourceRequest] DataSourceRequest request, string key)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DW_DC_Customer>();
                data = dbConn.SqlList<DW_DC_Customer>("exec p_SelectDC_CS_Customer_InfoGroup '" + key + "'").ToList();
                return Json(data.ToDataSourceResult(request));
            }
        }

        public ActionResult TrackingRequestByCustomer([DataSourceRequest] DataSourceRequest request, string CustomerID)
        {
            var data = new List<DC_RequestCredit>();
            data = DC_RequestCredit.GetCustomerCreditRequestByCustomerID(CustomerID);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult SaleTicketRequest_Read([DataSourceRequest] DataSourceRequest request, string customerID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                //var data = new List<DC_TeleSale_WaitingList>();
                //data = dbConn.SqlList<DC_TeleSale_WaitingList>("exec p_GetListTicketRequest_SaleOrderRequestByCustomer '" + customerID + "'");
                var data = DC_TeleSale_WaitingList.GetTicketRequest(customerID);
                return Json(data.ToDataSourceResult(request));
            }
        }

        public ActionResult Customer_Creation_Request_Read([DataSourceRequest] DataSourceRequest request, string customerID)
        {
            var data = DC_Customer_Creation_Request.GetInfoCustomerForCSTool(customerID);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult Potential_Customer_Read([DataSourceRequest] DataSourceRequest request, string customerID)
        {
            var data = DC_Potential_Customer.GetDC_Potential_Customer_ForCSTool(customerID);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult TrackingOrder_Read([DataSourceRequest] DataSourceRequest request, string customerID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_Tracking_Order>();
                data = dbConn.SqlList<DC_Tracking_Order>("exec p_GetTrackingOrder_ByCustomerIDForCS '" + customerID + "'");
                return Json(data.ToDataSourceResult(request));
            }
        }

        public ActionResult CallLog_Read([DataSourceRequest] DataSourceRequest request, string CustomerID)
        {
            var data = DC_CS_CallHistory.GetByCustomerID(CustomerID);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult SaveOrganization(string listRegistration, string Note, string organization)
        {
            if (asset.View)
            {

                try
                {
                    if (!string.IsNullOrEmpty(listRegistration))
                    {
                        var ann = DC_Organization_Meta.GetDC_Organization_Meta(organization, "Registration");
                        if (ann != null)
                        {
                            ann.Delete();
                        }

                        DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                        orgMeta.OrganizationID = organization;
                        orgMeta.Factor = "Registration";
                        orgMeta.Value = listRegistration.Remove(listRegistration.TrimEnd().Length - 1);
                        orgMeta.RowCreatedTime = DateTime.Now;
                        orgMeta.RowCreatedUser = currentUser.UserName;
                        orgMeta.Save();
                        log.Info("Update Registration : " + organization);
                    }

                    if (!string.IsNullOrEmpty(Note))
                    {
                        var ann = DC_Organization_Meta.GetDC_Organization_Meta(organization, "NoteByCS");
                        if (ann != null)
                        {
                            ann.Delete();
                        }

                        DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                        orgMeta.OrganizationID = organization;
                        orgMeta.Factor = "NoteByCS";
                        orgMeta.Value = Note;
                        orgMeta.RowCreatedTime = DateTime.Now;
                        orgMeta.RowCreatedUser = currentUser.UserName;
                        orgMeta.Save();
                        log.Info("Update NoteByCS : " + organization);
                    }
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false, message = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "You don't have permission to edit record" });
            }
        }
       
        public ActionResult GetOrganizationByID(string organization)
        {
            try
            {
                var Registration = DC_Organization_Meta.GetDC_Organization_Meta(organization, "Registration");
                var Note = DC_Organization_Meta.GetDC_Organization_Meta(organization, "NoteByCS");

                return Json(new { success = true, dataRegistration = Registration.Value, dataNote = Note.Value }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                log.Error(e);
                return Json(new { success = false, message = e });
            }
        }

        public ActionResult TeleSale_Announcement_Read([DataSourceRequest] DataSourceRequest request, string UserName)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_TeleSale_AnnouncementByCS>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    data = dbConn.Select<DC_TeleSale_AnnouncementByCS>("SELECT * FROM DC_TeleSale_AnnouncementByCS");
                }
                else
                {
                    data = dbConn.Select<DC_TeleSale_AnnouncementByCS>("SELECT * FROM DC_TeleSale_AnnouncementByCS");
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        public ActionResult AnnouncementTeleSale()
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_TeleSale_AnnouncementByCS>();
                data = dbConn.SqlList<DC_TeleSale_AnnouncementByCS>("exec DC_Telesalesv2_AnnouncementByCS '" + currentUser.UserName + "'");
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult SaveAnnouncementTeleSale(string Message, string Impact)
        {
            if (asset.View)
            {
                if (string.IsNullOrEmpty(Message) || string.IsNullOrEmpty(Impact))
                {
                    return Json(new { success = false, error = "Please input data" });
                }
                else
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            var data = new DC_TeleSale_AnnouncementByCS();

                            data.Impact = Impact;
                            data.Message = Message;
                            data.RowCreatedTime = DateTime.Now;
                            data.RowCreatedUser = currentUser.UserName;
                            data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                            data.RowLastUpdatedUser = "";
                            data.Status = true;

                            dbConn.Insert(data);
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            return Json(new { success = false, error = e.Message });
                        }
                    }
                }
            }
            else
            {
                return Json(new { success = false, error = "You don't have permission to create record " });
            }
        }

        public ActionResult Remove(string data)
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            DC_TeleSale_AnnouncementByCS check = new DC_TeleSale_AnnouncementByCS();
                            check.Id = int.Parse(item);
                            dbConn.Delete(check);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        log.Error(e);
                        return Json(new { success = false, message = e });
                    }
                    return Json(new { success = true });
                }
            }
            else
            {
                return Json(new { success = false, message = "You don't have permission to delete record" });
            }
        }

        public ActionResult ExportAnnouncement([DataSourceRequest]
                                 DataSourceRequest request)
        {
            if (asset.View)
            {
                //Get the data representing the current grid state - page, sort and filter
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<DC_TeleSale_AnnouncementByCS>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<DC_TeleSale_AnnouncementByCS>("SELECT * FROM DC_TeleSale_AnnouncementByCS WHERE RowCreatedUser = '" + currentUser.UserName + "'"); ;
                    }
                    else
                    {
                        data = dbConn.Select<DC_TeleSale_AnnouncementByCS>();
                    }

                    IEnumerable datas = data.ToDataSourceResult(request).Data;

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_AnnouncementCS.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_AnnouncementCS"];
                    int rowData = 1;
                    foreach (DC_TeleSale_AnnouncementByCS item in datas)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Impact;
                        if (item.Status = true)
                        {
                            dataSheet.Cells[rowData, i++].Value = "Active";    
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "Inactive";
                        }
                        
                        dataSheet.Cells[rowData, i++].Value = item.Message;
                        dataSheet.Cells[rowData, i++].Value = item.RowCreatedUser;
                        dataSheet.Cells[rowData, i++].Value = item.RowCreatedTime;
                    }

                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "DC_AnnouncementCS_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_AnnouncementCS_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }

        [HttpPost]
        public ActionResult TeleSale_Announcement_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_TeleSale_AnnouncementByCS> listResult)
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (listResult != null && ModelState.IsValid)
                        {
                            foreach (var item in listResult)
                            {
                                if (String.IsNullOrEmpty(item.Message))
                                {
                                    ModelState.AddModelError("", "Please input field [Message]");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }
                               
                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, error = e.Message });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record ");
                return Json(listResult.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult ReadByPromotion([DataSourceRequest] DataSourceRequest request, string TypeTab, string Organization)
        {
            if (Organization == "AAA")
            {
                var data = DC_Tele_Hint.GetAllDC_Tele_HintsByPromotion(TypeTab, "AAA");
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                var data = DC_Tele_Hint.GetAllDC_Tele_HintsByPromotion(TypeTab, Organization);
                return Json(data.ToDataSourceResult(request));
            }
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
        static string convertPhone(string phone)
        {
            string result = "84" + phone.Substring(1, phone.Length - 1);
            return result;
        }

        //ham nay minhtc chiu trach nhiem toan bo su viec
        [HttpPost]
        public async Task<ActionResult> SendMessage(string phoneNumber, string message, string issend, string _cus, string _telename, string _letephone, string _other, string templateid, string cusID)
        {
            List<DW_SMSMT> listError = new List<DW_SMSMT>();
            string listResend = "";
            int success = 0;
            string currentUserName = currentUser.UserName;
            string _mess = "";
            _mess = ReturnMess(message, _cus, _telename, _letephone, _other);

            var dauso = DC_NumberNetwork.GetAllDC_NumberNetworks();

            var limit = DC_SMS_Config.GetLimit(currentUser.UserName);
            phoneNumber = phoneNumber.Replace(",","");
            DW_SMSMT mt = new DW_SMSMT(0, "", "", "8099", "Manual Message", phoneNumber.Trim(), _mess, 1, DateTime.Now, "New", 0, true, DateTime.Now, currentUserName, DateTime.Now, currentUserName);

            if (!string.IsNullOrEmpty(phoneNumber.Trim()))
            {
                if (!mt.CheckPhoneNumber(dauso))
                {
                    //Kiem tra so dien thoai.Neu sai thi thong bao loi 
                    mt.ImportNote = "Invalid Phone number";
                    listError.Add(mt);
                }
                else
                {
                //    long id = mt.Save(limit);
                //    if (id > 0 && issend == "true")
                //    {
                //            MOReceiverSoapClient mtClient = new MOReceiverSoapClient();
                //            MTservice sms = new MTservice();
                //            sms.ID = 0;
                //            sms.phoneNumber = convertPhone(phoneNumber.Trim());
                //            sms.message = mt.Message;
                //            sms.serviceID = "8099";
                //            sms.commandCode = "ECC";
                //            sms.messageType = "1";
                //            sms.requestID = id.ToString();
                //            sms.totalMessage = "1";
                //            sms.messageIndex = "1";
                //            sms.isMore = "0";
                //            sms.contentType = "0";
                //            sms.RequestDate = DateTime.Now;
                //            string result = "";
                //            try
                //            {
                //                result = mtClient.messageSend(sms);
                //            }
                //            catch (Exception ex)
                //            {
                //                DW_SMSMT updateStatus = DW_SMSMT.GetDW_SMSMT(id);
                //                updateStatus.RowLastUpdatedTime = DateTime.Now;
                //                updateStatus.RowLastUpdatedUser = currentUserName;
                //                updateStatus.Status = "Fail";
                //                updateStatus.Update();
                //                mt.ImportNote = ex.Message;
                //                listResend += mt.RowID + ",";
                //                listError.Add(mt);
                //                result = "0";
                //            }

                //            if (result == "1")
                //            {
                //                DW_SMSMT updateStatus = DW_SMSMT.GetDW_SMSMT(id);
                //                updateStatus.RowLastUpdatedTime = DateTime.Now;
                //                updateStatus.RowLastUpdatedUser = currentUserName;
                //                updateStatus.Status = "Success";
                //                updateStatus.Update(); success++;
                //            }
                //            else
                //            {
                //                DW_SMSMT updateStatus = DW_SMSMT.GetDW_SMSMT(id);
                //                updateStatus.RowLastUpdatedTime = DateTime.Now;
                //                updateStatus.RowLastUpdatedUser = currentUserName;
                //                updateStatus.Status = "Fail";
                //                updateStatus.Update();
                //                mt.ImportNote = "Has error sending message!";
                //                listResend += mt.RowID + ",";
                //                listError.Add(mt);
                //            }
                //        }
                //        else if (id < 0)
                //        {
                //            mt.ImportNote = "Message Limit " + limit.ToString() + " character";
                //            listResend += mt.UserName + ",";
                //            listError.Add(mt);
                //        }
                //        else
                //        {
                //            mt.ImportNote = "Has error saving request!";
                //            listResend += mt.UserName + ",";
                //            listError.Add(mt);
                //        }
                //    }
                }
            }
            return Json(new { data = listError, resend = listResend, success = success });
        }


        public ActionResult SaveCallLog(DC_CS_CallHistory item)
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        item.RowCreatedUser = currentUser.UserName;
                        item.RowCreatedTime = DateTime.Now;
                        item.Note = item.NoteCallLog;
                        if (!string.IsNullOrEmpty(item.OrderID))
                        {
                            var check = dbConn.Select<IXOrderFulfillmentTable>("select top 1 * from IXOrderFulfillmentTable where OrderID = {0} AND MobilePhone = {1}", item.OrderID, item.MobilePhone);
                            if (check.Count == 0 )
                            {
                                return Json(new { success = false, message = "Đơn hàng không tồn tại" });
                            }
                        }
                        else
                        {
                            item.OrderID = string.IsNullOrEmpty(item.OrderID) ? "" : item.OrderID;
                        }
                        

                        dbConn.Insert(item);
                        return Json(new { success = true });
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, message = e.Message });
                    }
                }
            }
            else
            {
                return Json(new { success = false, message = "You don't have permission to create record " });
            }
        }


        public ActionResult GetResutlList()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_CS_ResultList>();
                data = dbConn.Select<DC_CS_ResultList>("Select * From DC_CS_ResultList Where SubId = '0' AND Active = 1").Where(p => p.Active = true).OrderBy(p => p.Description).ToList();
                return Json(data);
            }
        }

        public ActionResult GetResutlListSub(string SubID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_CS_ResultList>();
                data = dbConn.Select<DC_CS_ResultList>("Select * From DC_CS_ResultList Where SubId = '" + SubID + "' AND Active = 1").Where(p => p.Active = true).OrderBy(p => p.Description).ToList();
                return Json(data);
            }
        }
    
	}
}