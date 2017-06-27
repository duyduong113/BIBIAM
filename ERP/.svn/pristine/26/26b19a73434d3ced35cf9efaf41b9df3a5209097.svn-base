using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_RT_API_Tracking
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string TicketID { get; set; }
        public Deca_RT_TicketAPI Detail { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
    }
}