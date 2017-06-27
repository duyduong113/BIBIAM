using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_Email_SeenUids
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(255)]
        public string Uid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}