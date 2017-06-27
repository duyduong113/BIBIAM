using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace ERPAPD.Models
{
    public class CRM_PageProduct
    {
        [AutoIncrement]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int PositionID { get; set; }
        public int CategoryID { get; set; }
        public double Price { get; set; }
        public string PriceType { get; set; }
        public int Status { get; set; }
       
        public DateTime CreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [Ignore]
        public string ShareName { get; set; }
        [Ignore]
        public string SizeName { get; set; }

    }
}