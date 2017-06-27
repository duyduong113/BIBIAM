using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace MCC.Models
{
    public class tw_UserInGroup
    {
        [AutoIncrement]
        public Int64 id { get; set; }
        public Int64 userId { get; set; }
        public Int64 groupId { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }
    }
}