using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    public class Deca_AnonymousCustomer
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        public string CustomerID { get; set; }
        [Required(ErrorMessage="Họ tên khách hàng là bắt buộc.")]
        [StringLengthAttribute(100)]
        public string Fullname { get; set; }
        [StringLengthAttribute(16)]
        public string Phone { get; set; }
        [StringLengthAttribute(100)]
        public string Email { get; set; }
        [StringLengthAttribute(16)]
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }

        [StringLengthAttribute(20)]
        public string PhysicalID { get; set; }
        public int CompanyID { get; set; }
        [StringLengthAttribute(50)]
        public string CompanyName { get; set; }
        [StringLengthAttribute(100)]
        public string Department { get; set; }
        [StringLengthAttribute(100)]
        public string Position { get; set; }
        [StringLengthAttribute(50)]
        public string SourceType { get; set; }
        [StringLengthAttribute(50)]
        public string Marital { get; set; }
        [StringLengthAttribute(50)]
        public string Representative { get; set; }
        [StringLengthAttribute(100)]
        public string MobilePhone { get; set; }
        [StringLengthAttribute(100)]
        public string HomePhone { get; set; }
        [StringLengthAttribute(100)]
        public string OfficePhone { get; set; }
        [StringLengthAttribute(200)]
        public string Address { get; set; }
        [StringLengthAttribute(200)]
        public string Address1 { get; set; }
        [StringLengthAttribute(200)]
        public string Address2 { get; set; }
        [StringLengthAttribute(20)]
        public string TaxID { get; set; }
        [StringLengthAttribute(255)]
        public string CustomerRanking { get; set; }
        [StringLengthAttribute(50)]
        public string CustomerType { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}