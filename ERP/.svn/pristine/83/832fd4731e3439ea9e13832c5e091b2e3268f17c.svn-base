using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Company_Contractor
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(50)]
        public string CompanyID { get; set; }
        [StringLengthAttribute(256)]
        public string FullName { get; set; }
        [StringLengthAttribute(64)]
        public string Title { get; set; }
        [StringLengthAttribute(64)]
        public string Position { get; set; }
        [StringLengthAttribute(64)]
        public string Phone { get; set; }
        [StringLengthAttribute(64)]
        public string Email { get; set; }
        [StringLengthAttribute(1024)]
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string UpdatedBy { get; set; }
    }
}