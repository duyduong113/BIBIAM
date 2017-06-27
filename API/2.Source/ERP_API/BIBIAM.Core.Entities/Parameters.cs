using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Parameters
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        public string ParamID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Descr { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
