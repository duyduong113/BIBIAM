using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Code_Master
    {
        [AutoIncrement]
        public Int64 Id { get; set; }
        [StringLengthAttribute(255)]
        public string Type { get; set; }
        [StringLengthAttribute(255)]
        public string Name { get; set; }
        [StringLengthAttribute(255)]
        public string Name_Vi { get; set; }
        [StringLengthAttribute(255)]
        public string Value { get; set; }
        public bool IsDefault { get; set; }
        public int order_Num { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }

    public class Code_Master_Json
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
    public class DropDownJsonString
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class DropDownHashtag
    {
        public string hashtagcode { get; set; }
        public string hashtagname { get; set; }
    }
    public class Code_Master_View
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Code_Master_Json> data { get; set; }
    }
}