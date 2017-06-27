using System;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using ERPAPD.Models;
using ServiceStack.OrmLite;
using ERPAPD.Helpers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    public class CRMCustomerSupportDetailController : CustomController
    {
        // GET: CRMCustomerSupportDetail
        public ActionResult Index()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {

                var CustomerID = Request.QueryString["CustomerID"];
                var callType = Request.QueryString["callType"];
                var RowID = Request.QueryString["RowID"];
                ViewBag.CustomerID = CustomerID;
                ViewBag.detailCustomer = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID= {0}", CustomerID);
                ViewBag.listAppointmentType = dbConn.Select<Parameters>("Type='AppointmentType'").OrderBy(s => s.ParamID);
                ViewBag.listWorkType = dbConn.Select<Parameters>("Type='WorkType'").OrderBy(s => s.ParamID);
                ViewBag.listStatusType = dbConn.Select<Parameters>("Type='StatusType'").OrderBy(s => s.ParamID);
                ViewBag.listCustomer = dbConn.Select<ERPAPD_Customer>("SELECT CustomerID, CustomerName FROM ERPAPD_Customer WHERE Status = 'HOAT_DONG'");
                ViewBag.listTopic = dbConn.Select<CRM_FAQ_Topic>();
                ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "CustomerStatus").OrderByDescending(s => s.ParamID);
                ViewBag.listStatusCall = dbConn.Select<Parameters>("Type ={0}", "HistoryCallStatus").OrderBy(s => s.ParamID);
                ViewBag.listStatusTrend = dbConn.Select<Parameters>("Type ={0}", "HistoryCallTrend").OrderBy(s => s.ParamID);
                ViewBag.listGroupCall = dbConn.Select<Parameters>("Type ={0}", "GroupCallRequest").OrderBy(s => s.ParamID);
                ViewBag.listTypeCall = dbConn.Select<Parameters>("Type ={0}", "HistoryCallType").OrderBy(s => s.ParamID);
                if(callType != null)
                {
                    if(callType == "work")
                    {
                        ViewBag.currentWork = dbConn.SingleOrDefault<CRM_Works>("SELECT w.*, p.Value as TypeName FROM CRM_Works w left join Parameters p ON w.Type = p.ParamID WHERE w.RowID = " + RowID);
                    }
                    if (callType == "appointment")
                    {
                        ViewBag.currentAppointment = dbConn.SingleOrDefault<CRM_Appointment>("SELECT a.*, p.Value as TypeName FROM CRM_Appointment a left join Parameters p ON a.Type = p.ParamID WHERE a.RowID = " + RowID);
                    }


                }
                ViewBag.user = currentUser;

            }
            return View();
        }
        public ActionResult Create_Appointment(CRM_Ticket_Task_Appointment request)
        {
            var CRM_Appointment = new CRM_Appointment();
            var rs = CRM_Appointment.Save(request, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Update_Appointment(Int32 rowID)
        {
            var CRM_Appointment = new CRM_Appointment();
            var rs = CRM_Appointment.Update_Status(rowID, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult WorksRead([DataSourceRequest] DataSourceRequest request, string CustomerID)
        {
            string strQuery = @"SELECT * FROM (SELECT w.*,p.Value as TypeName,c.CustomerName as CustomerName,e.Name as EmName,e.Email as EmEmail
                FROM CRM_Works w
                LEFT JOIN Parameters p ON w.Type = p.ParamID 
                LEFT JOIN ERPAPD_Customer c ON w.CustomerID = c.CustomerID
                LEFT JOIN ERPAPD_Contacts e ON w.Person_contact = e.PKContactID    
                WHERE w.CustomerID = '"+ CustomerID + "' AND w.RowCreatedUser = '" + currentUser.UserName + "') A";
           
            var data = KendoApplyFilter.KendoDataByQuery<CRM_Works>(request, strQuery, "");

            return Json(data);
        }
        
        public ActionResult Create_Work(CRM_Ticket_Task_Appointment request)
        {
            var rs = CRM_Works.Save(request, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Update_Work(Int32 rowID)
        {
            var rs = CRM_Works.Update_Status(rowID, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult DailyNew_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = KendoApplyFilter.KendoData<CRM_Telesales_PromotionInfo>(request,"IsActive=1 AND EndDate > GETDATE()");
            return Json(data);
        }
        
        //public ActionResult DetailCustomerRead([DataSourceRequest] DataSourceRequest request, string CustomerID)
        //{
        //    var data = KendoApplyFilter.KendoData<ERPAPD_Customer>(request, "CustomerID='"+ CustomerID  + "'");
        //    return Json(data);
        //}
    }
}