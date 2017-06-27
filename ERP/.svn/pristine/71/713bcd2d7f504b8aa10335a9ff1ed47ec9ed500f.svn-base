using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a MCA_SMS_Template.
    /// </summary>
    public class Deca_SMS_SO
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [Required(ErrorMessage = "Nội dung tin nhắn là bắt buộc.")]
        [StringLength(1000)]
        public string Content { get; set; }
        [Required(ErrorMessage = "Mẫu tin nhắn là bắt buộc.")]
        public int TemplateID { get; set; }
        [Required]
        [StringLength(16)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string CustomerID { get; set; }
        [StringLength(50)]
        public string CustomerSource { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }

    }
}
