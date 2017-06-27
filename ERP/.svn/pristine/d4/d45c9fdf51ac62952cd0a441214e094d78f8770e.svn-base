using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPAPD.Models
{
    public class Deca_FAQ_Comment
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(Deca_FAQ))]
        public int QuestionId { get; set; }
        [StringLengthAttribute(50)]
        public string UserName { get; set; }
        [StringLengthAttribute(1000)]
        [AllowHtml]
        public string Content { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}