using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Controllers
{
    public class CRMMnDebtController : CustomController
    {
        // GET: CRMMnDebt
        public ActionResult Index()
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listContractType = dbConn.Select<Parameters>(s => s.Type == "ContractType").OrderBy(s => s.ParamID);
                ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>().OrderBy(s => s.HierarchyID);
                ViewBag.listCustomerType = dbConn.Select<ERPAPD_MasterData_Customer>(s => s.Type == "CustomerType" && s.Active == true).OrderBy(s => s.Code);
            }

            ViewBag.Title = "Nhắc nợ";
            return View();
        }
        public ActionResult ReadRemindDebt([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                string where = "";
                if (request.Filters.Any())
                {
                    where = " where " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                }
                var data = CRM_Customer_Debt.ReadRemindDebt(request.Page, request.PageSize, where);
                request.Page = 1;
                request.Filters = null;
                var result = data.ToDataSourceResult(request);
                if (data.Rows.Count > 0)
                {
                    result.Total = Convert.ToInt32(data.Rows[0]["RowCount"]);
                }
                return Json(result);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ReadRemindDetail([DataSourceRequest] DataSourceRequest request, string CustomerCode)
        {
            if (asset.View)
            {
                string where = "";
                if (request.Filters.Any())
                {
                    where = " where " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                }
                var data = CRM_Contract_Debt.ReadRemindDetail(request.Page, 999999,where, CustomerCode);
                request.Page = 1;
                request.Filters = null;
                var result = data.ToDataSourceResult(request);
                if (data.Rows.Count > 0)
                {
                    result.Total = Convert.ToInt32(data.Rows[0]["RowCount"]);
                }
                return Json(result);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Detail(Int32 Id)
        {
            ViewBag.Title = "Chi tiết công nợ khách hàng";
            return View();
        }
        public ActionResult History()
        {
            ViewBag.Title = "Lịch sử thanh toán";
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listDebtType = dbConn.Select<Parameters>("select ParamID, Value from Parameters where Type = 'DebtStatus'");
            }
            return View();
        }
        public ActionResult RemindDebt(Int32 Id = 0)
        {
            ViewBag.Title = "Nhắc nợ";
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listAgency = dbConn.Select<ERPAPD_List>("select code, name from ERPAPD_List where FKListtype  = 57 ");
                ViewBag.listDebtType = dbConn.Select<Parameters>("select ParamID, Value from Parameters where Type = 'DebtStatus'");
            }
            return View();
        }
        public ActionResult RemindDetail(string CustomerCode)
        {
            ViewBag.Title = "Chi tiết nhắc nợ";

            ViewBag.CustomerCode = CustomerCode; 
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listPaymentStatus = dbConn.Select<Parameters>("Type ={0}", "PaymentStatus").OrderByDescending(s => s.ParamID);
                ViewBag.listStatusPaypal = dbConn.Select<ERPAPD_List>(s => s.FKListtype == 29 && s.Status == 1);
                ViewBag.itemDebt = dbConn.SqlList<CRM_Contract_Debt>("exec p_CRM_Contract_Debt_RemindDetail_GetHeader N'" + CustomerCode.Replace("'", "''") + "'").FirstOrDefault();
                ViewBag.listAgency = dbConn.Select<ERPAPD_List>("select code, name from ERPAPD_List where FKListtype  = 57 ");
                ViewBag.listDebtType = dbConn.Select<Parameters>("select ParamID, Value from Parameters where Type = 'DebtStatus'");
                ViewBag.listContractType = dbConn.Select<Parameters>(s => s.Type == "ContractType").OrderBy(s => s.ParamID);
            }
            return View();
        }
        public ActionResult HistoryRemindRead([DataSourceRequest] DataSourceRequest request, string CustomerCode,string Type)
        {
            string strQuery = @"SELECT d.*, (select Top 1 FullName from EmployeeInfo where RefStaffId = d.StaffID) AS StaffName 
                    FROM CRM_Debt_History d
                WHERE d.CustomerCode = '" + CustomerCode + "' AND d.Type = '"+ Type +"'";
            var data = KendoApplyFilter.KendoDataByQuery<CRM_Debt_History>(request, strQuery, "");
            return Json(data);
        }
        public ActionResult PaymentDetailRead([DataSourceRequest] DataSourceRequest request, Int32 pk_contract)
        {
            string strQuery = @"SELECT c_ngay_tt_theo_hd
              ,c_tien_tt_theo_hd
              ,c_ngay_du_kien_thu
              ,c_tien_du_kien_thu
              ,c_status
              ,DATEDIFF(day,GETDATE(),c_ngay_du_kien_thu)  AS c_so_ngay_qua_han
              ,ISNULL((SELECT sum(c_tien_thanh_toan) FROM CRM_Payment WHERE a.pk_gmoney_next = c_ma_lich_thanh_toan),0) AS c_tien_thanh_toan
              ,a.c_tien_du_kien_thu - ISNULL((SELECT sum(c_tien_thanh_toan) FROM CRM_Payment WHERE a.pk_gmoney_next = c_ma_lich_thanh_toan),0) AS c_tien_con_no
              ,a.c_trang_thai_tt
            FROM  CRM_GET_MONEY_MONTH_NEXT a
            WHERE fk_contract =" + pk_contract;
            var data = KendoApplyFilter.KendoDataByQuery<CRM_GET_MONEY_MONTH_NEXT>(request, strQuery, "");
            return Json(data);
        }
        public ActionResult PaymentProccessRead([DataSourceRequest] DataSourceRequest request)
        {
            string strQuery = @"SELECT pa.* ,
                 c.c_customer_code AS c_customer_code,
                 c.c_code AS c_contract_code,
                 cu.CustomerName as c_customer_name,
                 ca.c_ngay_du_kien_thu as c_ngay_du_kien_thu,
                 ca.c_ngay_tt_theo_hd as c_ngay_tt_theo_hd,
                 pp.Note as c_ghi_chu,
                 pp.PaymentForm as c_payment_form,
                 pp.NumberReceipt as c_number_receipt,
                 li.Name AS c_bank_name, 
 
                 DATEDIFF(day,ca.c_ngay_du_kien_thu,pa.c_ngay_thanh_toan)  AS c_so_ngay_qua_han,
                 (CASE WHEN DATEDIFF(day,GETDATE(),ca.c_ngay_du_kien_thu) > 0 THEN 2 
                 ELSE (CASE 
		                WHEN DATEDIFF(day,DateAdd(DD,-5,GETDATE()),ca.c_ngay_du_kien_thu) < 0 THEN 4 
		                WHEN DATEDIFF(day,DateAdd(DD,-5,GETDATE()),ca.c_ngay_du_kien_thu) >= 0 THEN 3 END)
		                 END)
		                 as c_trang_thai_no
                 FROM CRM_Payment pa
                 LEFT JOIN CRM_PaymentProgress pp ON pp.PKPayment = pa.c_ma_thanh_toan 
                 LEFT JOIN CRM_GET_MONEY_MONTH_NEXT ca ON ca.pk_gmoney_next = pa.c_ma_lich_thanh_toan 
                 LEFT JOIN CRM_Contract c ON c.pk_contract = pa.c_ma_hop_dong
                 LEFT JOIN ERPAPD_Customer cu ON cu.CustomerCode = c.c_customer_code
                 LEFT JOIN ERPAPD_List li ON li.Code = pp.BankCode";
            var data = KendoApplyFilter.KendoDataByQuery<CRM_Payment>(request, strQuery, "");
            return Json(data);
        }
        public ActionResult RemindDebt_Save(CRM_Debt_History item)
        {
            var rs = CRM_Debt_History.save(item, currentUser.UserName);
            return Json(rs);
        }

    }
}