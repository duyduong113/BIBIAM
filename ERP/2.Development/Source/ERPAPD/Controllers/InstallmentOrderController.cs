using ServiceStack.OrmLite;
using System;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;


namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class InstallmentOrderController : CustomController
    {
        //
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.Bank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    ViewBag.Status = dbConn.Select<Deca_Order_Status>().OrderBy(a => a.ID);
                    return View();
                }
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
                    string query = @"select 
			                            od.OrderID as OCMOrderID
			                            ,'' as ERPAPDOrderID
			                            ,cast(od.CreatedDate as date) as OrderDate
			                            ,od.DeliveriedItemDate as DeliveriedDate
			                            ,isnull(ost.Name,N'Mới') as OrderStatus
			                            ,Cast(cu.CustomerID as nvarchar(50)) as CustomerID
			                            ,cu.FullName as CustomerName
			                            ,isnull(cu.MobilePhone,isnull(cu.StandardMobilePhone,'')) as CustomerPhone
			                            ,N'VANGLAI' as Company
			                            ,'' as PhysicalID
			                            ,isnull(odd.Name,'') as ProductItem
			                            ,isnull(odd.Qty,0) as Quantity
			                            ,isnull(odd.UnitPrice,0) as UnitPrice
			                            ,isnull(odd.TotalAmt,0) as LineAmount

			                            ,case when isnull(od.PaymentGate,'') = 5 then N'Thanh toán qua VietintBank' 
				                              when isnull(od.PaymentGate,'') = 3 then N'Thanh toán qua Ngân lượng'
				                              else N'Chưa xác định' end as PaymentGateway
			                            ,case when isnull(od.PaymentStatus,'') = 0 then N'Chưa thanh toán' 
				                              when isnull(od.PaymentStatus,'') = 1 then N'Đã thanh toán'
				                              when isnull(od.PaymentStatus,'') = 2 then N'Đã hoàn tiền'
				                              else N'Chưa xác định' end as PaymentStatus
			                            ,case when isnull(od.PaymentType,'') = 9 then N'ViettinBank' else N'Chưa xác định' end as PaymentType
			                            ,'' as BankTransaction
			                            ,'1900-01-01' as TransactionTime
			                            ,'' as BankActionStatus
			                            ,'' as BankName
			                            ,'' as isUsingTokenID
			                            ,12 as Installement
			                            ,odd.TotalAmt*1.99/100 as ConvertionFee
			                            ,round(odd.TotalAmt/12,0) as PayPermonth
			                            ,'' as CancelledReason
			                            ,'' as CancalledReasonDetail
			                            ,od.DeliveryNotes
			                            ,isnull(mc.MerchantName,N'Chưa xác định') as MerchantName
			                            ,isnull(od.Carrier,N'Chưa xác định') as Carrier
			                            ,'Online' as SalesSource
			                            ,'' as Salesman
		                            from DC_OCM_Order od
		                            left join DC_OCM_Merchant mc on mc.PKMerchantID = FKMerchantID
		                            left join DC_OCM_OrderDetail odd on odd.OrderID = od.OrderID
		                            left join Deca_Order_Header odh on odh.RefID = od.OrderID
		                            left join DC_OCM_Customer cu on cu.CustomerID = od.FKCustomerID
		                            left join Deca_Order_Status ost on ost.RefID = od.OrderStatus
		                            where od.OrderID Like 'TG%' and odh.OrderID is null

		                            union  all

		                            select 
			                            isnull(od.OrderID,'') as OCMOrderID
			                            ,isnull(odh.OrderID,'') as ERPAPDOrderID
			                            ,cast(odh.CreatedAt as date) as OrderDate
			                            ,isnull(od.DeliveriedItemDate,'1900-01-01') as DeliveriedDate
			                            ,isnull(ost.Name,'') as OrderStatus
			                            ,isnull(odh.CustomerID,'') as CustomerID
			                            ,odh.CustomerName as CustomerName
			                            ,odh.MobilePhone as CustomerPhone
			                            ,odh.CompanyID as Company
			                            ,odh.PhysicalID as PhysicalID

			                            ,dod.ProductName as ProductItem
			                            ,dod.Quantity as Quantity
			                            ,dod.UnitPrice as UnitPrice
			                            ,dod.LineAmount as LineAmount

			                            ,N'Thanh toán qua VietintBank' as PaymentGetway
			                            ,case when odh.PaymentStatus = 0 then N'Chưa thanh toán' 
				                              when odh.PaymentStatus = 1 then N'Đã thanh toán'
				                              when odh.PaymentStatus = -1 then N'Thanh toán thất bại'
				                              else N'Chưa xác định' end as PaymentStatus
			                            ,N'ViettinBank' as PaymentType
			                            ,isnull(btxn.AuthTransRef, '') as BankTransaction
			                            ,isnull(btxn.AuthTime, '') as TransactionTime
			                            ,isnull(cm1.[Description], '') as BankActionStatus
			                            ,isnull(bnk.BankName,N'Chưa xác định') BankName
			                            ,case when isnull(odh.TokenID,'') = '' then '' else 'PayUseTokenID' end as isUsingTokenID
			                            ,isnull(odh.Installment,0) as Installement
			                            ,isnull(dod.LineAmount*isnull(bis.ConvertionFee,0)/100,0) as ConvertionFee
			                            ,round(dod.LineAmount/isnull(bis.Installment,1),0) as PayPerMonth
			                            ,isnull(cm.[Description],'') as CancelledReason
			                            ,isnull(odt.[Description],'') as CancalledReasonDetail
			                            ,odh.Note
			                            ,odh.ShopName as MerchantName
			                            ,isnull(odh.Carrier,N'Chưa xác định') as Carrier
			                            ,'ERPAPD' as SalesSource
			                            ,isnull(odh.SaleMan,'') as Salesman
		                            from Deca_Order_Header odh
		                            left join Deca_Order_Detail dod on dod.OrderID = odh.OrderID
		                            left join DC_OCM_Order od on odh.RefID = od.OrderID
		                            left join Deca_Order_Status ost on ost.RefID = od.OrderStatus
		                            left join Deca_Order_CancelTracking odt on odt.OrderID = odh.OrderID
		                            left join Deca_Code_Master cm on cm.CodeID = odt.ReasonID
		                            left join DC_Bank_Definition bnk on bnk.BankID = case when odh.Bank = '' then 'BA00001' else odh.Bank end
		                            left join Deca_Bank_Installment bis on bis.BankID = bnk.BankID and bis.[Default] = 1
		                            left join Deca_Bank_Transactions btxn on btxn.OrderID = odh.OrderID
		                            left join Deca_Code_Master cm1 on cm1.CodeID = btxn.BankActionStatus ";
                    data = KendoApplyFilter.KendoDataByQuery<Deca_InstallmentOrder>(request, query, "");
                }
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}