using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using System.Data;
using ERPAPD.Helpers;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class CRM_PageType
    {
        [AutoIncrement]
        [PrimaryKey]
        public int PageTypeID { get; set; }
        public string PageTypeName { get; set; }
        public string RefID { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public static DataTable ReadAll()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper().ExecuteString("select * from CRM_PageType order by PageTypeID desc", param);
        }
        public static int Delete(string PageTypeID)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@PageTypeID", PageTypeID));
            return new SqlHelper().ExecuteNoneQuery("delete CRM_PageType where PageTypeID = @PageTypeID", param, CommandType.Text);
        }
    }
}