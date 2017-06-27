using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
namespace ERPAPD.Models
{
    public class DC_Telesales_PromotionInfo
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(200)]
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLengthAttribute(4000)]
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAll { get; set; }
        public List<string> AppliedForCompanies { get; set; }
        public string Thumnail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }

}
