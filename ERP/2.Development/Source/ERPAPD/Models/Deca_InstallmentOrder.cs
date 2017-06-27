using System;
namespace ERPAPD.Models
{
    public class Deca_InstallmentOrder
    {
        public string OCMOrderID { get; set; }
        public string ERPAPDOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveriedDate { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Company { get; set; }
        public string PhysicalID { get; set; }
        public string ProductItem { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double LineAmount { get; set; }
        public string PaymentGateway { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public string BankTransaction { get; set; }
        public DateTime TransactionTime { get; set; }
        public string BankActionStatus { get; set; }
        public string BankName { get; set; }
        public string isUsingTokenID { get; set; }
        public int Installement { get; set; }
        public double ConvertionFee { get; set; }
        public double PayPerMonth { get; set; }
        public string CancelledReason { get; set; }
        public string CancalledReasonDetail { get; set; }
        public string DeliveryNotes { get; set; }
        public string MerchantName { get; set; }
        public string Carrier { get; set; }
        public string SalesSource { get; set; }
        public string Salesman { get; set; }
    }
  
}