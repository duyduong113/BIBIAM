using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DecaPay.Models
{
    public class Sales_Order_Customer
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        [Index(Unique = true)]
        public string CustomerId { get; set; }
        [StringLengthAttribute(255)]
        public string FirstName { get; set; }
        [StringLengthAttribute(255)]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        [StringLengthAttribute(255)]
        public string PlaceOfBirth { get; set; }
        [StringLengthAttribute(20)]
        public string Gender { get; set; }
        [StringLengthAttribute(255)]
        public string Job { get; set; }
        [StringLengthAttribute(255)]
        public string MaritalStatus { get; set; }
        [StringLengthAttribute(50)]
        public string TaxNumber { get; set; }
        //contact
        [StringLengthAttribute(20)]
        public string Fax { get; set; }
        [StringLengthAttribute(20)]
        public string Phone { get; set; }
        [StringLengthAttribute(255)]
        public string Email { get; set; }
        [StringLengthAttribute(50)]
        public string Country { get; set; }
        [StringLengthAttribute(50)]
        public string Province { get; set; }
        [StringLengthAttribute(50)]
        public string District { get; set; }
        [StringLengthAttribute(50)]
        public string Ward { get; set; }
        [StringLengthAttribute(255)]
        public string Address { get; set; }
        public double IncomePoint { get; set; }
        [StringLengthAttribute(20)]
        public string Type { get; set; }
        [StringLengthAttribute(1000)]
        public string MetaSearch { get; set; }

        [StringLengthAttribute(255)]
        public string CompanyName { get; set; }
        [StringLengthAttribute(255)]
        public string CompanyAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string UpdatedBy { get; set; }

        public double UsedPoint { get; set; }
        [Ignore]
        public double ICPoint
        {
            get
            {
                return Math.Floor(IncomePoint / 800000);
            }
        }
    }

    public class Sales_Order_Header
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        public string SalesOrderId { get; set; }
        [StringLengthAttribute(50)]
        public string CustomerId { get; set; }
        [StringLengthAttribute(255)]
        public string FirstName { get; set; }
        [StringLengthAttribute(255)]
        public string LastName { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public bool PrintedInvoice { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string UpdatedBy { get; set; }

        //others meta
        [StringLengthAttribute(50)]
        public string PromotionCode { get; set; }
        public double PromotionAmount { get; set; }
    }

    public class Sales_Order_Detail
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(Sales_Order_Header))]
        public int HeaderId { get; set; }
        [StringLengthAttribute(50)]
        public string MSIN { get; set; }
        [StringLengthAttribute(50)]
        public string SKU { get; set; }
        [StringLengthAttribute(128)]
        public string Barcode { get; set; }
        [StringLengthAttribute(255)]
        public string ItemName { get; set; }
        [StringLengthAttribute(50)]
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public bool isPromotion { get; set; }
        [StringLengthAttribute(255)]
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string UpdatedBy { get; set; }

        [Ignore]
        public double Tonkho { get; set; }
    }

    public class Sales_Order_Detail_Report
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
    }

    public class Sales_Item
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        public string MSIN { get; set; }
        [StringLengthAttribute(50)]
        public string SKU { get; set; }
        [StringLengthAttribute(128)]
        public string Barcode { get; set; }
        [StringLengthAttribute(255)]
        public string ItemName { get; set; }
        [StringLengthAttribute(50)]
        public string Unit { get; set; }
        public double Tonkho { get; set; }
        public double UnitPrice { get; set; }
        public bool isPromotion { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string UpdatedBy { get; set; }
    }


}