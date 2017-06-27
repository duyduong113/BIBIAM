using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_SMSMO
    {
        [AutoIncrement]
        public int Id { get; set; } //Id Deca
        //INCOM data
        [StringLengthAttribute(15)]
        public string User_ID { get; set; } //phone number 
        [StringLengthAttribute(10)]
        public string Service_ID { get; set; } //6189
        [StringLengthAttribute(10)]
        public string Command_Code { get; set; } //DAT
        [StringLengthAttribute(160)]
        public string Info { get; set; } //productId || productId-productPriceId
        public double Request_ID { get; set; } // id from INCOM
        [StringLengthAttribute(50)]
        public string OrderID { get; set; }
        //more data
        [StringLengthAttribute(50)]
        public string ProductID { get; set; }
        [StringLengthAttribute(50)]
        public string ProductPriceID { get; set; }

        public string ProductName { get; set; }
        //customer data
        public string MobilePhone { get; set; }

        [StringLengthAttribute(50)]
        public string CustomerID { get; set; }

        [StringLengthAttribute(100)]
        public string Fullname { get; set; }

        [StringLengthAttribute(50)]
        public string PhysicalID { get; set; }

        [StringLengthAttribute(50)]
        public string CompanyName { get; set; }

        [StringLengthAttribute(10)]
        public string Status { get; set; }
        [StringLengthAttribute(255)]
        public string CancelledReason { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}