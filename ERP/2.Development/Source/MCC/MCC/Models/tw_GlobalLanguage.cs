using MCC.Models;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCC.Models
{
    public class tw_GlobalLanguage
    {
        [AutoIncrement]
        public Int64 id { get; set; }
        [StringLengthAttribute(50)]
        public string code { get; set; }
        public string name { get; set; }
        [StringLengthAttribute(255)]
        public string imagesPublicId { get; set; }
        public tw_ImagesSize imagesSize { get; set; }
        public bool isDefault { get; set; }
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }
    }
}