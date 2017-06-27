using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_SMS_Sensitive
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(10)]
        [Required]
        public string Type { get; set; }
        [StringLength(1000)]
        [Required]
        public string Content { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }  
    }
}