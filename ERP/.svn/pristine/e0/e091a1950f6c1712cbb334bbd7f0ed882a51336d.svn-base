using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_CallResultQuestionListForTeleSale
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        [StringLength(100)]
        public string CallResultId { get; set; }
        [StringLength(256)]
        public string CustomerID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
    }
}