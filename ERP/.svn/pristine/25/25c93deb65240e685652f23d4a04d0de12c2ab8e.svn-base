using ServiceStack.DataAnnotations;
using System;
namespace ERPAPD.Models
{
    public class Deca_Order_Header
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderID { get; set; }
        public string ShopName { get; set; }
        public string ShopID { get; set; }
        public string RefID { get; set; }
        public DateTime CompletedDate { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CompanyID { get; set; }
        public string EmployeeID { get; set; }
        public string PhysicalID { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public int ShippingCity { get; set; }
        public int ShippingDistrict { get; set; }
        public int Installment { get; set; }
        public double PayPerMonth { get; set; }
        public double BankFee { get; set; }
        public string Bank { get; set; }
        public double TotalAmount { get; set; }
        public string TokenID { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsLocked { get; set; }
        public string SaleMan { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        [Ignore]
        public string ReasonID { get; set; }
        [Ignore]
        public string CancelReason { get; set; }
        [Ignore]
        public DateTime PaymentDate { get; set; }
        [Ignore]
        public int RowCount { get; set; }
    }
    public class Deca_Order_ExcelModel
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderID { get; set; }
        public DateTime CompletedDate { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CompanyID { get; set; }
        public string EmployeeID { get; set; }
        public string PhysicalID { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public int ShippingCity { get; set; }
        public int ShippingDistrict { get; set; }
        public int Installment { get; set; }
        public double PayPerMonth { get; set; }
        public double BankFee { get; set; }
        public string Bank { get; set; }
        public double TotalAmount { get; set; }
        public string TokenID { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsLocked { get; set; }
        public string SaleMan { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string RefID { get; set; }
        public string ShopName { get; set; }
        public string CancelReason { get; set; }
        [Ignore]
        public string CancelNote { get; set; }
        //order detail here:
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double LineAmount { get; set; }

        public int PriceID { get; set; }
        public string SKUID { get; set; }
    }

    public class Deca_Order_TelesalesSuggest
    {
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
    }

    public class Deca_OCMOrder_Suggest
    {
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmt { get; set; }
    }
    public class Deca_Telesales_OrderHistory
    {
        public string OrderID { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public string TransactionInfo { get; set; }
        public int Installment { get; set; }
        public string SaleMan { get; set; }
        public string PhysicalID { get; set; }
        public string CancelReason { get; set; }
    }

    public class Deca_BuyInfo
    {
        public int productId { get; set; }
        public int colorId { get; set; }
        public string name { get; set; }
        public double sellingPrice { get; set; }
        public int quantity { get; set; }
        public int merchantID { get; set; }

    }
}