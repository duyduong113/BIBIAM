using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class CRM_FAQ_Rating
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(CRM_FAQ))]
        public int FAQId { get; set; }
        [StringLengthAttribute(50)]
        public string UserName { get; set; }
        public bool View { get; set; }
        public bool Like { get; set; }
        public bool Unlike { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}