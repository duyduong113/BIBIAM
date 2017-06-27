using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_TeleSale_SkipCustomer
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(256)]
        public string CustomerID { get; set; }
        public string SkipNote { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
        [Ignore]
        public string MobilePhone { get; set; }
        [Ignore]
        public string Team { get; set; }
    }
}