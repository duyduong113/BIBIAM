using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_Vietin_Error_Code
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        [StringLengthAttribute(50)]
        [Required]
        public string Code { get; set; }
        [Required]
        [StringLengthAttribute(255)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}