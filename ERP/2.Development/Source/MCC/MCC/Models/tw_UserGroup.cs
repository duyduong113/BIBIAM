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
    public class tw_UserGroup
    {
        [AutoIncrement]
        public Int64 id { get; set; }
        [StringLengthAttribute(255)]
        public string name { get; set; }
        [StringLengthAttribute(255)]
        public string description { get; set; }
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(255)]
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        [StringLengthAttribute(255)]
        public string updatedBy { get; set; }

        [Ignore]
        public List<Int64> listUser
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return dbConn.GetFirstColumn<Int64>("SELECT userId FROM tw_UserInGroup where groupId =" + id);
                }
            }
        }

        [Ignore]
        public List<Int64> users { get; set; }

        [Ignore]
        public List<AccessDetail> listAccess
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<AccessDetail>();
                    data = dbConn.Select<AccessDetail>("groupId={0}", id);
                    return data;
                }
            }
        }

        [Ignore]
        public Int64 totalUser
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return dbConn.Scalar<int>("SELECT COUNT(id) FROM tw_UserInGroup WHERE groupId =" + id);
                }
            }
        }

        //[Ignore]
        //public List<AccessRight> listAccessRight
        //{
        //    get
        //    {
        //        using (var dbConn = Helpers.OrmliteConnection.openConn())
        //        {
        //            return dbConn.Select<AccessRight>();
        //        }
        //    }
        //}
    }

    public class tw_UserGroup_Json
    {
        public Int64 id { get; set; }
        public string name { get; set; }
    }
}