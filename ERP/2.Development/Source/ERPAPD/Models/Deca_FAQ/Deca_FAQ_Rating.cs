using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class Deca_FAQ_Rating
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(Deca_FAQ))]
        public int FAQId { get; set; }
        [StringLengthAttribute(50)]
        public string UserName { get; set; }
        public bool View { get; set; }
        public bool Like { get; set; }
        public bool Unlike { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}