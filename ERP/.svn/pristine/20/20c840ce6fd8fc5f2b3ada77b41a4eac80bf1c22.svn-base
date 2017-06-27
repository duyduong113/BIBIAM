using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Article
    {
        public Article()
        {
            active = true;
        }

        [AutoIncrement]
        public Int64 id { get; set; }
        public string articlecode { get; set; }
        [StringLengthAttribute(100)]
        public string type { get; set; }
        [StringLengthAttribute(255)]
        public string name { get; set; }
        [StringLengthAttribute(1000)]
        public string description { get; set; }
        public string content { get; set; }
        public string category { get; set; }
        [StringLengthAttribute(255)]
        public string imagesPublicId { get; set; }
      //  public tw_ImagesSize imagesSize { get; set; }
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }
        public bool allUser { get; set; }
        public bool isDefault { get; set; }
        public string companycode { get; set; }
        [Ignore]
        public string hashtagcode { get; set; }
    }
}