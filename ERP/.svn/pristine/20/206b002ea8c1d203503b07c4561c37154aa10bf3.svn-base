using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_Announcement
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Message { get; set; }
        [StringLengthAttribute(100)]
        public string Team { get; set; }
        //Impact: 1.Low 2.Medium 3.High
        [StringLengthAttribute(100)]
        public string Impact { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
        [Ignore]
        public string ColorBg { get; set; }
    }
}