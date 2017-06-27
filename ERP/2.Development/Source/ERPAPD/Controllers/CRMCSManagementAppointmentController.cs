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
    public class CRMCSManagementAppointmentController : CustomController
    {
        // GET: CRMCustomerSupportDetail
        public ActionResult Index()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                // var CustomerID = Request.QueryString["CustomerID"];
                ViewBag.listCustomer = dbConn.Select<ERPAPD_Customer>("Select top 100 RowID,CustomerID,CustomerName FROM ERPAPD_Customer");
                ViewBag.listUserWork = dbConn.Select<ERPAPD_Employee>("Select RowID,PKEmployeeID,Name from ERPAPD_Employee");
                ViewBag.listAppointmentType = dbConn.Select<Parameters>("Type='AppointmentType'").OrderBy(s => s.ParamID);
                ViewBag.listWorkType = dbConn.Select<Parameters>("Type='WorkType'").OrderBy(s => s.ParamID);
                ViewBag.listStatusType = dbConn.Select<Parameters>("Type='StatusType'").OrderBy(s => s.ParamID);
                // ViewBag.listCustomer = dbConn.Select<ERPAPD_Customer>("SELECT CustomerID, CustomerName FROM ERPAPD_Customer WHERE Status = 'HOAT_DONG'");
                // ViewBag.listTopic = dbConn.Select<CRM_FAQ_Topic>();
            }
            return View();
        }
        //Used
        public ActionResult AppointmentRead([DataSourceRequest] DataSourceRequest request)
        {
            //string value = "RowCreatedUser = '" + currentUser.UserName + "' AND Status = 'YET'";

    //        string strQuery = @"SELECT * FROM (SELECT a.*,c.CustomerName as CustomerName,d.Name  FROM CRM_Appointment a 
    //            LEFT JOIN ERPAPD_Customer c ON a.CustomerID = c.CustomerID  
				//JOIN ERPAPD_Contacts d ON a.Person_contact=d.PKContactID
    //            ) A ";
            string strQuery = @"SELECT * FROM (SELECT a.*,c.CustomerName as CustomerName  FROM CRM_Appointment a 
                LEFT JOIN ERPAPD_Customer c ON a.CustomerID = c.CustomerID  
                ) A ";

            var data = KendoApplyFilter.KendoDataByQuery<CRM_Appointment>(request, strQuery, "");
            //var data = KendoApplyFilter.KendoData<CRM_Appointment>(request, value + "AND Date > GETDATE()");
            return Json(data);
        }
        public ActionResult Create_Work(CRM_Ticket_Task_Appointment request)
        {
            var CRM_Works = new CRM_Works();
            var rs = CRM_Works.Save(request, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Update_Appointment(string rowID, string typeAction)
        {
            if (asset.Delete)
            {
                try
                {
                    var dbConn = Helpers.OrmliteConnection.openConn();
                    string[] separators = { "@@" };
                    var listRowID = rowID.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        if (typeAction == "XOA")
                        {
                            dbConn.Delete<CRM_Appointment>(s => s.RowID == int.Parse(item));
                        }
                        else if (typeAction == "XN")
                        {
                            CRM_Appointment.Update_Status(int.Parse(item), currentUser.UserName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }

        }
        public ActionResult GetContactByCusID(string Id)
        {
            using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (Id != null && Id != "")
                {
                    var data = dbConn.Select<ERPAPD_Contacts>("Select PKContactID, Name FROM ERPAPD_Contacts WHERE CustomerID ={0}", Id);
                    return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new ERPAPD_Contacts();
                    return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}