using ServiceStack.DataAnnotations;
using System;

namespace CMS.Models
{
    public class Hierarchy
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string hierarchycode { get; set; }
        public string hierarchyname { get; set; }
        public int level { get; set; }
        public string parentcode { get; set; }
        public string hierarchytype { get; set; }
        public string status { get; set; }
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string updatedBy { get; set; }
    }
}