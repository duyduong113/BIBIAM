using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_Bank_Action
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(50)]
        public string BankID { get; set; }
        [StringLengthAttribute(1024)]
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string CreatedBy { get; set; }
    }
}