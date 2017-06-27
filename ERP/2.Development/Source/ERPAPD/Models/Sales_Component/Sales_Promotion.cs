using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DecaPay.Models
{
    public class Sales_Promotion_Header
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string PromotionCode { get; set; }
        [StringLengthAttribute(100)]
        public string PromotionName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string UpdatedBy { get; set; }
    }

    public class Sales_Promotion_Detail
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(Sales_Promotion_Header))]
        public int HeaderId { get; set; }
        [StringLengthAttribute(100)]
        public string MSIN { get; set; }
        [StringLengthAttribute(100)]
        public string SKU { get; set; }
        [StringLengthAttribute(255)]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string UpdatedBy { get; set; }
    }
}