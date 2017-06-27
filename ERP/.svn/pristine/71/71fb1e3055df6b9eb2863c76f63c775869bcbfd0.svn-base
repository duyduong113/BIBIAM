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
    public class SMTP_Configuration
    {
        [AutoIncrement]
        public Int64 id { get; set; }
        [StringLengthAttribute(255)]
        public string email { get; set; }
        [StringLengthAttribute(255)]
        public string password { get; set; }
        [StringLengthAttribute(255)]
        public string host { get; set; }
        public int port { get; set; }
        public bool enableSsl { get; set; }
        public bool isDefault { get; set; }
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }
        [Ignore]
        public string listAgencyName { get; set; }
    }
}