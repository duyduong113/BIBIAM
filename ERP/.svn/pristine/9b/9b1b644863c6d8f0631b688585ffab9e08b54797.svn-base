using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_CallHistory
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }
        [StringLengthAttribute(50)]
        public string Phone { get; set; }
        [StringLengthAttribute(20)]
        public string PhysicalID { get; set; }
        [StringLengthAttribute(10)]
        public string CustRule { get; set; }

        [StringLengthAttribute(50)]
        public string ResultID { get; set; }
        [StringLengthAttribute(50)]
        public string SubResultID { get; set; }
        [StringLengthAttribute(500)]
        public string Content { get; set; }
        public DateTime? RecallTime { get; set; }
        public bool isDone { get; set; }
        [StringLengthAttribute(128)]
        public string Source { get; set; }
        [StringLengthAttribute(20)]
        public string OrderID { get; set; }
        [StringLengthAttribute(128)]
        public string RefType { get; set; }
        [StringLengthAttribute(128)]
        public string RefID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
        public string CustomerName
        {
            get;
            set;
        }
    }
    public class CallHistoryViewModel
    {
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }
        [StringLengthAttribute(128)]
        public string CustomerName { get; set; }
        [Required]
        [StringLengthAttribute(50)]
        public string Phone { get; set; }
        [Required]
        [StringLengthAttribute(20)]
        public string PhysicalID { get; set; }
        [StringLengthAttribute(50)]
        public string ResultID { get; set; }
        [StringLengthAttribute(50)]
        [Required(ErrorMessage = "Kết quả cuộc gọi là bắt buộc.")]
        public string SubResultID { get; set; }
        [StringLengthAttribute(500)]
        [Required(ErrorMessage = "Nội dung cuộc gọi là bắt buộc.")]

        public string Content { get; set; }
        public string CustRule { get; set; }
        public DateTime? RecallTime { get; set; }
        [StringLengthAttribute(128)]
        public string Feeling { get; set; }

        [StringLengthAttribute(128)]
        public string Trend { get; set; }
        [StringLengthAttribute(128)]
        public string AgentSetLevel { get; set; }
        public string Source { get; set; }
        public string RefType { get; set; }
        public string RefID { get; set; }
        public string OrderID { get; set; }
        public bool IsDoneTicket { get; set; }
    }

    public class SkipCustomerModelView
    {
        [Required(ErrorMessage = "Lý do là bắt buộc")]
        public string Reason { get; set; }
        public string CustomerID { get; set; }
        public string Phone { get; set; }
        public string PhysicalID { get; set; }
        public string CustRule { get; set; }
        public string Source { get; set; }
        public string RefType { get; set; }
        public string RefID { get; set; }
        public bool IsDone { get; set; }
    }

}