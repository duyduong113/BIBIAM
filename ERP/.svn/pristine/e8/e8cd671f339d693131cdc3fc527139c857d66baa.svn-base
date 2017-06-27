using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace ERPAPD.Models
{
    public class CRM_PagePosition
    {
        [AutoIncrement]
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public string Size { get; set; }
        public int FileSize { get; set; }
        public string ShareNumber { get; set; }
        public string RefID { get; set; }
        public string WebsiteRefID { get; set; }
        public bool Status { get; set; }
       
        public DateTime CreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [Ignore]
        public string ShareName { get; set; }
        [Ignore]
        public string SizeName { get; set; }
        [Ignore]
        public string WebsiteName { get; set; }

    }
}