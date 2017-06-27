using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class Deca_Company
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        [StringLengthAttribute(100)]
        public string CompanyID { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ShortName is not valid.")]
        [Required]
        [StringLengthAttribute(255)]
        [Index(Unique = true)]
        public string ShortName { get; set; }
        [StringLengthAttribute(255)]
        public string LongName { get; set; }
        [StringLengthAttribute(100)]
        public string Type { get; set; }
        public double Employee { get; set; }
        public string Status { get; set; }
        [StringLengthAttribute(100)]
        public string Owner { get; set; }
        [StringLengthAttribute(255)]
        public string Address { get; set; }
        [StringLengthAttribute(255)]
        public string Region { get; set; }
        [StringLengthAttribute(255)]
        public string City { get; set; }
        [StringLengthAttribute(255)]
        public string District { get; set; }
        [StringLengthAttribute(20)]
        public string Mobile { get; set; }
        [StringLengthAttribute(20)]
        public string Fax { get; set; }
        [StringLengthAttribute(255)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [StringLengthAttribute(255)]
        public string Website { get; set; }
        [StringLengthAttribute(255)]
        public string Contacter { get; set; }
        [StringLengthAttribute(500)]
        public string DeliveryNote { get; set; }
        [StringLengthAttribute(500)]
        public string Note { get; set; }
        public bool Active { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }

        //additional data
         [Ignore]
        public string BranchName { get; set; }
    }

    public class Deca_Company_Log
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string CompanyID { get; set; }
        public Deca_Company Log { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
    }
}