using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class AccessDetail
    {
        [AutoIncrement]
        public Int64 id { get; set; }
        public Int64 groupId { get; set; }
        [StringLengthAttribute(255)]
        public string controllerName { get; set; }
        public Dictionary<string, bool> access { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }
    }
}