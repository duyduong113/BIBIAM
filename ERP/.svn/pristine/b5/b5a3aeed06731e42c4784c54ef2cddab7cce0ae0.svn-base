using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Company_Branch
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(50)]
        public string CompanyID { get; set; }
        [StringLengthAttribute(50)]
        public string BranchID { get; set; }
        [StringLengthAttribute(256)]
        public string BranchName { get; set; }
        [StringLengthAttribute(1024)]
        public string Address { get; set; }
        [StringLengthAttribute(1024)]
        public string Note { get; set; }
        [StringLengthAttribute(64)]
        public string CityID { get; set; }
        [StringLengthAttribute(64)]
        public string DistrictID { get; set; }
        [StringLengthAttribute(128)]
        public string Owner { get; set; }
        [Ignore]
        public string CityName { get; set; }
        [Ignore]
        public string DistrictName { get; set; }
        public bool Telesale { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string UpdatedBy { get; set; }
    }
}