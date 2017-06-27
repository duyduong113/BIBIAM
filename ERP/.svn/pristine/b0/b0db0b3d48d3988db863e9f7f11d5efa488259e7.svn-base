using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using System.Data;
using ERPAPD.Helpers;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class CRM_Website
    {
        [AutoIncrement]
        [PrimaryKey]
        public int WebsiteID { get; set; }
        public string WebsiteName { get; set; }
        public string RefID { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public static DataTable ReadAll()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper().ExecuteString("select * from CRM_Website order by WebsiteID ASC", param);
        }
        public static List<CRM_Website> GetList()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            var dt= new SqlHelper().ExecuteString("select * from CRM_Website order by WebsiteID desc", param);
            List<CRM_Website> result = new List<CRM_Website>();
            foreach (DataRow r in dt.Rows)
            {
                result.Add(new CRM_Website
                {
                    WebsiteID =Convert.ToInt32(r["WebsiteID"]),
                    WebsiteName = r["WebsiteName"].ToString(),
                });
            }
            return result;
        }
        public static int Delete(string WebsiteID)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@WebsiteID", WebsiteID));
            return new SqlHelper().ExecuteNoneQuery("delete CRM_Website where WebsiteID = @WebsiteID", param, CommandType.Text);
        }
    }
}