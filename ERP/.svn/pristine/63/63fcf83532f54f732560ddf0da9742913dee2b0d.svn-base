using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Configure_Metric
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(225)]
        public string Name { get; set; }
        public int Customer { get; set; }
        public int Order { get; set; }
        public int TalkingTime { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [Default(typeof(string), "")]
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

    }
}