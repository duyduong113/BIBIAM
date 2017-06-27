using System.Web.Mvc;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;


namespace ERPAPD.Controllers
{
    public class TelesaleController : CustomController
    {
        // GET: Telesale
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveCallLog()
        {

            return View();
        }
        public ActionResult CallHistoryRead([DataSourceRequest] DataSourceRequest request, string CustomerID)
        {
            string queryStr = "CustomerID = '" + CustomerID + "'";
           return Json(KendoApplyFilter.KendoData<CRM_Telesale_History_Call>(request, queryStr ));
            //return Json(KendoApplyFilter.KendoData<CRM_Telesale_History_Call>(request, ""));

        }
        //public ActionResult CreateHistoryCall(CRM_Ticket_Task_Appointment request)
        //{
        //    var CRMTicketController = new CRMTicketController();
        //    var idTicket = CRMTicketController.Create_Ticket_From_CustomerSupport(request);
        //    if (idTicket != null)
        //    {
        //        var rs = CRM_Telesale_History_Call.Save(request, currentUser.UserName, idTicket);
        //        rs = CRM_Works.Save(request, currentUser.UserName, idTicket);
        //        if (!string.IsNullOrEmpty(request.IDTask.ToString()))
        //        {
        //            CRM_Works.Update_Status(request.IDTask, currentUser.UserName);
        //        }
        //        if (!string.IsNullOrEmpty(request.IDAppoint.ToString()))
        //        {
        //            CRM_Appointment.Update_Status(request.IDAppoint, currentUser.UserName);
        //        }
        //        if (!string.IsNullOrEmpty(request.RecallTime.ToString()))
        //        {
        //            rs = CRM_Appointment.Save(request, currentUser.UserName, idTicket);
        //        }
        //        return Json(rs);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Ticket is null !"});
        //    }
            
        //}
    }
}