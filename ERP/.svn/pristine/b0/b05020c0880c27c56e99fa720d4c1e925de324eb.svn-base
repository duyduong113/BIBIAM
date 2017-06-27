using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
namespace ERPAPD.Models
{
    public class DC_TeleSale_SaveTimeActionWork
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(256)]
        public string UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
    }
}