using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Parameters
    {

        [AutoIncrement]
        
        public int ID { get; set; }
      
        [Required]
        public string ParamID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Descr { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool Status { get; set; }

    }
}