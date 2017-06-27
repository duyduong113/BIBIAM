using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_QuestionList
    {
        [AutoIncrement]
        [Alias("ID")]
        public int ID { get; set; }
        public string RegionID { get; set; }
        public string Question{ get; set; }
        public string AnswerType { get; set; }
        [Ignore]
        public string Answer { get; set; }

        [DefaultValue("1900-01-01")]
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]

        [DefaultValue("")]
        public string RowCreatedUser { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        [DefaultValue("")]
        public string RowLastUpdatedUser { get; set; }
    }
}