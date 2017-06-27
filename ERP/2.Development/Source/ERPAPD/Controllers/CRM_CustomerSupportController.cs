using ERPAPD.Models;
using ERPAPD.Helpers;
using Kendo.Mvc.UI;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;


namespace ERPAPD.Controllers
{
    public class CRM_CustomerSupportController : CustomController
    {
        // GET: CRMCustomerSupport
        public ActionResult Index()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>("Status={0}", 1);
                ViewBag.listCustomer = dbConn.Select<ERPAPD_Customer>("SELECT CustomerID, CustomerName FROM ERPAPD_Customer WHERE Status = 'HOAT_DONG'");
                ViewBag.listStatus = dbConn.Select<Parameters>("Type ={0}", "CustomerStatus").OrderByDescending(s => s.ParamID);
                ViewBag.listLabel = dbConn.Select<CRM_Label>();
                ViewBag.listWorkType = dbConn.Select<Parameters>("Type='WorkType'").OrderBy(s => s.ParamID);
                ViewBag.listAppointmentType = dbConn.Select<Parameters>("Type='AppointmentType'").OrderBy(s => s.ParamID);
                ViewBag.KPI = dbConn.Select<CRM_PlanKPI>("Kpi='KPI01'").OrderBy(s => s.Id);
                ViewBag.listKpi = dbConn.Select<Parameters>(s => s.Type == "Kpi");
                ViewBag.userInfo = currentUser;
                var exitStaff = dbConn.Select<ERPAPD_Employee>("SELECT RowID,RefEmployeeID FROM ERPAPD_Employee where UserName='" + currentUser.UserName + "'").FirstOrDefault();
                if (exitStaff != null)
                {
                    ViewBag.StaffID = exitStaff.RefEmployeeID;
                }
                else
                {
                    ViewBag.StaffID = -1;
                }
            }
            return View();
        }
        public ActionResult CustomerRead(string text = "")
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string where = @" A.Customername COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'" +
                               @" OR A.Phone2 COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'" +
                               @" OR A.CustomerID COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'" +
                               @" OR A.CodeLink COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'";
                var data = dbConn.Select<ERPAPD_Customer>(@" SELECT Top 5
                                                            A.CustomerID,
                                                            CustomerName,
                                                            CustomerCode,
                                                            Category,
                                                            CategoryParent,
                                                            Phone2 AS Phone,
                                                            Fax,
                                                            A.[Address2] AS [Address],
                                                            A.Email,
                                                            BankName,
                                                            BankCode,
                                                            BankBranch,
                                                            TaxCode,
                                                            A.Category,
                                                            CodeLink,
                                                            CustomerType,
                                                            AgencyType,                     
														    Sponsor AS Name,
														    Position AS Title
                                                            FROM ERPAPD_Customer A WHERE " + where);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ContactRead(string CustomerID)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<ERPAPD_Contacts>("SELECT  PKContactID,Name FROM ERPAPD_Contacts WHERE CustomerID = '" + CustomerID + "'");
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AnnoucementRead([DataSourceRequest] DataSourceRequest request, string Type)
        {
            string value = Type == "Annoucement" ? "Type='" + Type + "'" : "Type<>'Annoucement'";
            var data = KendoApplyFilter.KendoData<CRM_CS_ManageInfo>(request, value + " AND IsActive=1 AND ExpiredDate > GETDATE()");
            return Json(data);
        }
        public ActionResult EmulationRead([DataSourceRequest] DataSourceRequest request, string Type)
        {
            string value = Type == "Emulation" ? "Type='" + Type + "'" : "Type<>'Emulation'";
            var data = KendoApplyFilter.KendoData<CRM_CS_ManageInfo>(request, value + " AND IsActive=1 AND ExpiredDate > GETDATE()");
            return Json(data);
        }

        public ActionResult AppointmentRead([DataSourceRequest] DataSourceRequest request)
        {
            //string value = "RowCreatedUser = '" + currentUser.UserName + "' AND Status = 'YET'";

            string strQuery = @"SELECT * FROM (SELECT a.*,co.Name as Name,c.CustomerName as CustomerName  FROM CRM_Appointment a 
                LEFT JOIN ERPAPD_Customer c ON a.CustomerID = c.CustomerID  
                LEFT JOIN ERPAPD_Contacts co ON co.PKContactID = a.Person_contact
                WHERE a.RowCreatedUser = '" + currentUser.UserName + "' AND a.Status = 'YET' AND DATEDIFF(d,GETDATE(),date) >= 0) A ";

            var data = KendoApplyFilter.KendoDataByQuery<CRM_Appointment>(request, strQuery, "");
            //var data = KendoApplyFilter.KendoData<CRM_Appointment>(request, value + "AND Date > GETDATE()");
            return Json(data);
        }
        public ActionResult KPI_Read([DataSourceRequest] DataSourceRequest request)
        {
            string strQuery = @"
                               SELECT  'Month' As [Type],
		                        [Month] As [KeySearch],
		                         Kpi,
		                        SUM(k.Amount) as Value,
		                        round(SUM(k.Amount)/140000000*100,0) as per_item
                                 FROM CRM_PlanKPI k  
                                 Where [Month] = Month(getdate())
                                 group by [Month], Kpi

                                 Union All
 
                                 SELECT 'Period' As [Type],
		                        Period As [KeySearch], 
		                        Kpi,
		                        SUM(k.Amount) as Value,
		                        round(SUM(k.Amount)/140000000*100,0) as per_item
                                 FROM CRM_PlanKPI k  
                                 where Period = Case when (Month(getdate()) <= 3) Then 1 Else Case When (Month(getdate()) <= 6) Then 2 Else Case When (Month(getdate()) <= 9) Then 3 Else 4 End End End
                                 group by Period,Kpi

                                  Union All

                                SELECT 'Year' As [Type],
		                        [Year] As [KeySearch], 
		                        Kpi,
		                        SUM(k.Amount) as Value,
		                        round(SUM(k.Amount)/140000000*100,0) as per_item
                                 FROM CRM_PlanKPI k  
                                 where year = year(getdate())
                                 group by [Year],Kpi
                                ";

            var data = KendoApplyFilter.KendoDataByQuery<CRM_PlanKPI>(request, strQuery, "");
            return Json(data);
        }
        public ActionResult CountContract()
        {
            var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn();
            var StaffID = dbConn.FirstOrDefault<ERPAPD_Employee>("SELECT RowID,RefEmployeeID FROM ERPAPD_Employee where UserName='" + currentUser.UserName + "'");
            var UserID = "All";
            if (StaffID != null)
            {
                UserID = StaffID.RefEmployeeID.ToString();
            }
            var data = ERPAPD_Customer_CS.CountContract(UserID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WorksRead([DataSourceRequest] DataSourceRequest request, string status)
        {
            string strQuery = @"SELECT * FROM (SELECT w.*,p.Value as TypeName,c.CustomerName as CustomerName,e.Name as EmName,e.Email as EmEmail  
                FROM CRM_Works w 
                LEFT JOIN Parameters p ON w.Type = p.ParamID 
                LEFT JOIN ERPAPD_Customer c ON w.CustomerID = c.CustomerID  
                LEFT JOIN ERPAPD_Contacts e ON w.Person_contact = e.PKContactID   
                WHERE w.RowCreatedUser = '" + currentUser.UserName + "' AND w.Status = '" + status + "' ) A ";
            var data = KendoApplyFilter.KendoDataByQuery<CRM_Works>(request, strQuery, "");
            return Json(data);
        }
    }


}