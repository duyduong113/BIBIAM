using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class Deca_Customer_Index
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        public string CustomerID { get; set; }
        [StringLengthAttribute(100)]
        public string Fullname { get; set; }
        [StringLengthAttribute(50)]
        public string Phone { get; set; }
        [StringLengthAttribute(120)]
        public string Email { get; set; }
        [StringLengthAttribute(50)]
        public string PhysicalID { get; set; }
        [StringLengthAttribute(1024)]
        public string Meta { get; set; }
        [StringLengthAttribute(1024)]
        public string MetaOCM { get; set; }
        [StringLengthAttribute(50)]
        public string DataSource { get; set; }
        public bool isDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}