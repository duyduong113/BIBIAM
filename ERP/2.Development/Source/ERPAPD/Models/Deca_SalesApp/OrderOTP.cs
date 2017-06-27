using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class OrderOTP
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
        public string OrderID { get; set; }
        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
        public DateTime CreatedTime { get; set; }
        public int SendTime { get; set; }
        public string Status { get; set; }
        public int RetryTime { get; set; }
    }
    public class OrderOTP_Log
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(100)]
        public string OrderID { get; set; }
        [StringLength(100)]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        public string ActionType { get; set; }
        [StringLength(100)]
        public string OTP { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}