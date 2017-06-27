using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_Xlite
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Alias("CallDate")]
        public DateTime calldate { get; set; }
        [StringLengthAttribute(20)]
        [Alias("Source")]
        public string src { get; set; }
        [StringLengthAttribute(20)]
        [Alias("Destination")]
        public string dst { get; set; }
        [Alias("Duration")]
        public double duration { get; set; }
        [Alias("BillSec")]
        public double billsec { get; set; }
        [Alias("Disposition")]
        [StringLengthAttribute(20)]
        public string disposition { get; set; }
        [Alias("RecordingFile")]
        [StringLengthAttribute(100)]
        public string recordingfile { get; set; }
        [Alias("CallRoute")]
        [StringLengthAttribute(20)]
        public string call_route { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }

        [Ignore]
        public string file
        {
            get
            {
                return calldate.AddMonths(3) > DateTime.Now ? recordingfile : "";
            }
        }
    }

    public class DC_Telesales_Xlite_API
    {
        public string num_rows { get; set; }
        public List<DC_Telesales_Xlite> data { get; set; }
    }
}