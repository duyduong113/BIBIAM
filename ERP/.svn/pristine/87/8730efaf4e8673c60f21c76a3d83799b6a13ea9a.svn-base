using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
namespace ERPAPD.Models
{
    public class DC_Telesales_ManageInfo
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(200)]
        [Required]
        public string Title { get; set; }
        [StringLengthAttribute(10)]
        [Required]
        public string Priority { get; set; }
        [StringLengthAttribute(20)]
        [Required]
        public string Type { get; set; }
        [Required]
        [StringLengthAttribute(4000)]
        public string Content { get; set; }
        public DateTime ExpiredDate { get; set; }
        [Required]
        public List<string> BroadcastTo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }

}
