using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_CustomerLastTouch
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(100)]
        public string PhysicalID { get; set; }
        public int TouchedTime { get; set; }
        [StringLengthAttribute(128)]
        public string LastFeeling { get; set; }
        [StringLengthAttribute(128)]
        public string LastTrend { get; set; }
        [StringLengthAttribute(128)]
        public string LastAgentSetLevel { get; set; }
        public DateTime LastTouchAt { get; set; }
        [StringLengthAttribute(100)]
        public string LastTouchBy { get; set; }
    }

}