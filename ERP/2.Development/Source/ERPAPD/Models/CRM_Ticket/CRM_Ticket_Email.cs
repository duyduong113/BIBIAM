using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class CRM_Ticket_Email
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string TicketID { get; set; }
        [StringLength(255)]
        [Required]
        public string EmailTo { get; set; }
        [StringLength(255)]
        public string CCTo { get; set; }
        [StringLength(500)]
        [Required]
        public string Subject { get; set; }
        [StringLength(4000)]
        [Required]
        public string EmailContent { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}