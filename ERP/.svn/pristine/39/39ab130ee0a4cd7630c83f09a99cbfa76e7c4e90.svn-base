using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class CRM_CS_Appendix
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(200)]
        [Required]
        public string TicketID { get; set; }
        [StringLengthAttribute(128)]
        public string Feeling { get; set; }
        [StringLengthAttribute(128)]
        public string Trend { get; set; }      
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}