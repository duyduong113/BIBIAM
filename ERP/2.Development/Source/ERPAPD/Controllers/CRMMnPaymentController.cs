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
    public class CRMMnPaymentController : CustomController
    {
        //
        // GET: /CRMMnPayment/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listAgency = dbConn.Select<ERPAPD_List>("select code, name from ERPAPD_List where FKListtype  = 57 ");
                ViewBag.listDebtType = dbConn.Select<Parameters>("select ParamID, Value from Parameters where Type = 'DebtStatus'");
                ViewBag.listBank = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 8");

            }
            ViewBag.Title = "Quản lý công nợ và thanh toán";
            return View();
        }
        public ActionResult ReadData([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                string where = "";
                if (request.Filters.Any())
                {
                    where = " where " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                }
                var data = CRM_Customer_Debt.ReadData(request.Page, request.PageSize, where);
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
        public ActionResult ReadAdjust([DataSourceRequest] DataSourceRequest request, string CustomerCode)
        {
            if (asset.View)
            {
                var data = CRM_Customer_Debt_Adjust.ReadData(CustomerCode);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ReadPaymentProgress([DataSourceRequest] DataSourceRequest request, string CustomerCode)
        {
            if (asset.View)
            {
                var data = CRM_PaymentProgress.ReadDataByCustomerCode(CustomerCode);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //[OutputCache(Duration = 1000, VaryByParam = "none")]
        public ActionResult Detail(string CustomerCode)
        {
            ViewBag.Title = "Chi tiết công nợ khách hàng";
            ViewBag.CustomerCode = CustomerCode; 
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewBag.listPaymentStatus = dbConn.Select<Parameters>("Type ={0}", "PaymentStatus").OrderByDescending(s => s.ParamID);
                ViewBag.listStatusPaypal = dbConn.Select<ERPAPD_List>(s => s.FKListtype == 29 && s.Status == 1);
                ViewBag.itemDebt = dbConn.SqlList<CRM_Contract_Debt>("exec p_CRM_Contract_Debt_RemindDetail_GetHeader N'" + CustomerCode.Replace("'", "''") + "'").FirstOrDefault();
                ViewBag.listAgency = dbConn.Select<ERPAPD_List>("select code, name from ERPAPD_List where FKListtype  = 57 ");
                ViewBag.listDebtType = dbConn.Select<Parameters>("select ParamID, Value from Parameters where Type = 'DebtStatus'");
                ViewBag.listContractType = dbConn.Select<Parameters>(s => s.Type == "ContractType").OrderBy(s => s.ParamID);
                ViewBag.listBank = dbConn.Select<DropDownListDataList>("select Code ,Name from ERPAPD_List WHERE FKListtype = 8");

            }
            return View();
        }
        public ActionResult SaveAdjust(CRM_Customer_Debt_Adjust row)
        {
            if (asset.Create)
            {
                if (row.Amount <= 0)
                {
                    return Json(new { success = false, message = "Số tiền không hợp lệ" });
                }
                if (row.AdjustDate < DateTime.Parse("2000-01-01"))
                {
                    return Json(new { success = false, message = "Ngày ghi nhận không hợp lệ" });
                }
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    row.CreatedBy = currentUser.UserName;
                    row.CreatedAt = DateTime.Now;
                    dbConn.Insert(row);
                }
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult SavePayment(CRM_PaymentProgress row)
        {
            if (asset.Create)
            {
                if (string.IsNullOrEmpty(row.NumberReceipt))
                {
                    return Json(new { success = false, message = "Số phiếu thu không hợp lệ" });
                }
                if (row.Money <= 0)
                {
                    return Json(new { success = false, message = "Số tiền không hợp lệ" });
                }
                if (row.PaymentDate < DateTime.Parse("2000-01-01"))
                {
                    return Json(new { success = false, message = "Ngày thanh toán không hợp lệ" });
                }
                if (row.PaymentForm != "TIEN_MAT" && row.PaymentForm != "CHUYEN_KHOAN")
                {
                    return Json(new { success = false, message = "Hình thức thanh toán không hợp lệ" });
                }
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    row.Number = "1";
                    row.RowCreatedUser = currentUser.UserName;
                    row.RowUpdatedUser = currentUser.UserName;
                    row.RowCreatedAt = DateTime.Now;
                    row.RowUpdatedAt = DateTime.Now;
                    dbConn.Insert(row);
                }
                return Json(new { success = true});
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ReadContractDebt([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                string where = "";
                if (request.Filters.Any())
                {
                    where = " where " + KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                }
                var data = CRM_Contract_Debt.ReadContractDebt(request.Page, request.PageSize, where);
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

	}
}