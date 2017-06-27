using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;


namespace ERPAPD.Models
{
    public class DC_SMS_Sensitive_User
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(DC_SMS_Sensitive))]
        public int SMSId { get; set; }
        [StringLength(128)]
        public string UserID { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }  
        [Ignore]
        public string Content { get; set; }
    }
}