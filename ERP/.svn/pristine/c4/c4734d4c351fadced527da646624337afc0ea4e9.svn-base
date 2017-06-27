using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_AvoidCallingTimeFrame
    {
        [AutoIncrement]
        public int Id { get; set; }

        [StringLengthAttribute(128)]
        public string HeaderID { get; set; }

        [StringLengthAttribute(1000)]
        public string HeaderName { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
    }
}