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
    public class CRM_FAQ
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(CRM_FAQ_Topic))]
        public int TopicId { get; set; }
        [StringLengthAttribute(255)]
        public string Name { get; set; }
        [StringLengthAttribute(1000)]
        public string Question { get; set; }
        [AllowHtml]
        public string Answer { get; set; }
        public bool Published { get; set; }
        public double View { get; set; }
        public double Like { get; set; }
        public double Unlike { get; set; }
        public List<int> Groups { get; set; }
        public List<string> Users { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }

        [Ignore]
        public List<CRM_FAQ_Comment> ListComment
        {
            get
            {
                var data = new List<CRM_FAQ_Comment>();
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    data = dbConn.Select<CRM_FAQ_Comment>("QuestionId={0} AND Published={1}", Id, true);
                }
                return data;
            }
        }
        [Ignore]
        public CRM_FAQ_Rating Rating
        {
            get
            {
                var data = new CRM_FAQ_Rating();
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    data = dbConn.FirstOrDefault<CRM_FAQ_Rating>("FAQId ={0} AND UserName={1}", Id, HttpContext.Current.User.Identity.Name);
                }
                return data;
            }
        }
    }

    public class Deca_FAQ_ViewModel
    {
        public string TopicName { get; set; }
        public List<CRM_FAQ> ListFAQ { get; set; }
    }
}