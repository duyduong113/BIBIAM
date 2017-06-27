using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_Telesales_Agent_KPI
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        public string Agent { get; set; }
        [StringLengthAttribute(20)]
        public string Month { get; set; }
        [StringLengthAttribute(100)]
        [Index(Unique = true)]
        public string AgentMonth { get { return Agent + Month; } }
        public double Revenue { get; set; }
        public double Order { get; set; }
        public double Call { get; set; }
        public double Customer { get; set; }
        public double TalkTime { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}