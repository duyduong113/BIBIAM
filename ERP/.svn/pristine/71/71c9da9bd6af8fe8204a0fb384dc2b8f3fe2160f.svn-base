using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_Appendix
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }

        [StringLengthAttribute(20)]
        public string PhysicalID { get; set; }
        [StringLengthAttribute(128)]
        public string Feeling { get; set; }

        [StringLengthAttribute(128)]
        public string Trend { get; set; }
        [StringLengthAttribute(128)]
        public string AgentSetLevel { get; set; }
        public int CallID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}