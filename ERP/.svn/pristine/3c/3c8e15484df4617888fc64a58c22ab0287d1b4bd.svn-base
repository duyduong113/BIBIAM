using CloudinaryDotNet;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace MCC.Models
{
    public class tw_ShowroomBranch
    {
        [AutoIncrement]
        public Int64 id { get; set; }
        [StringLengthAttribute(255)]
        public string shortlyName { get; set; }
        [StringLengthAttribute(255)]
        public string name { get; set; }
        public Int64 representative { get; set; }
        [StringLengthAttribute(255)]
        public string title { get; set; }
        [StringLengthAttribute(255)]
        public string phone { get; set; }
        [StringLengthAttribute(255)]
        public string email { get; set; }
        [StringLengthAttribute(255)]
        public string address { get; set; }
        public string parentType { get; set; }
        public Int64 showroomParent { get; set; }
        [StringLengthAttribute(255)]
        public string imagesPublicId { get; set; }
        public tw_ImagesSize imagesSize { get; set; }
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }

        [Ignore]
        public Int64 employeeNumber
        {
            get
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                {                    
                    return dbConn.Scalar<Int64>("SELECT COUNT(*) AS EmployeeNumber FROM tw_UserInShowroom WHERE showroomId = " + id);
                }
            }
        }

        [Ignore]
        public string representativeName
        {
            get
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                {
                    var cookie = HttpContext.Current != null && HttpContext.Current.Request.Cookies["_culture"] != null ? HttpContext.Current.Request.Cookies["_culture"].Value : "vi";
                    return dbConn.Scalar<string>("SELECT fullName + ISNULL((SELECT TOP 1 ' (' + " + (cookie == "vi" ? "Name_Vi" : "Name") + " + ')' FROM Code_Master WHERE [Type] = 'CRMTitle' AND Value = '" + title + "'),'') + ' - ' + phone + ' - ' + ' <a href=\"mailto:'+ email +'\">'+ email +'</a>' FROM tw_User WHERE active = 1 AND id=" + representative);
                }
            }
        }
    }

    public class tw_ShowroomBranch_Json1
    {
        public Int64 id { get; set; }
        public string name { get; set; }
    }

}