using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Territory
    {
        [AutoIncrement]
        public Int64 Id { get; set; }
        [StringLengthAttribute(255)]
        public string Level { get; set; }
        [StringLengthAttribute(255)]
        public string Name { get; set; }
        public Int64 ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}