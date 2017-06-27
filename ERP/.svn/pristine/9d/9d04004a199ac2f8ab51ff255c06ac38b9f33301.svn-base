using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Company_Action
    {

        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(50)]
        public string CompanyID { get; set; }
        [StringLengthAttribute(1024)]
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string CreatedBy { get; set; }
    }
}