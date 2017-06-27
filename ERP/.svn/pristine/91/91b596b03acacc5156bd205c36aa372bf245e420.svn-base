using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Groups
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        [StringLengthAttribute(100)]
        public string Name { get; set; }
        [StringLengthAttribute(100)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}