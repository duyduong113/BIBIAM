using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using System.Web.Mvc;

namespace ERPAPD.Models
{
    public class Deca_Topic
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string Name { get; set; }
        [StringLengthAttribute(1000)]
        [AllowHtml]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
        [Ignore]
        public List<Deca_FAQ> ListFAQ
        {
            get;
            set;
        }

    }
}