using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class ERPAPD_MasterData_Customer
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
     
        public bool Active { get; set; }
        public DateTime? RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }
    }
}