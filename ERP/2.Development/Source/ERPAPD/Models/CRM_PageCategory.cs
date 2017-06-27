using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace ERPAPD.Models
{
    public class CRM_PageCategory
    {
        [AutoIncrement]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int WebsiteID { get; set; }
        public string RefID { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        [Ignore]
        public string WebsiteName { get; set; }
        [Ignore]
        public string TypeName { get; set; }

    }
}