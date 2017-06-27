using System;
using System.Collections.Generic;
using System.Web;
using ServiceStack.DataAnnotations;
using System.Data;
using ERPAPD.Helpers;
using System.Data.SqlClient;
namespace ERPAPD.Models
{
    public class CRM_Page
    {
        [AutoIncrement]
        [PrimaryKey]
        public int PageID { get; set; }
        public string PageName { get; set; }
        public string WebsiteID { get; set; }
        public string RefID { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public static DataTable ReadAll()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper().ExecuteString("select * from CRM_Page order by WebSiteID ASC, PageID ASC", param);
        }
        public static int Delete(string PageID)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@PageID", PageID));
            return new SqlHelper().ExecuteNoneQuery("delete CRM_Page where PageID = @PageID", param, CommandType.Text);
        }
        public static List<CRM_Page> ReadPageOfCategory(string CategoryID)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CategoryID", CategoryID));
            var dt = new SqlHelper().ExecuteString("select p.PageID, p.PageName, case when m.PageID is null then 0 else 1 end as Status from CRM_Page p left join CRM_PageCategory_Mapping m on p.PageID = m.PageID and m.CategoryID = @CategoryID", param);
            List<CRM_Page> result = new List<CRM_Page>();
            foreach (DataRow r in dt.Rows)
            {
                result.Add(new CRM_Page
                {
                    PageID = Convert.ToInt32(r["PageID"]),
                    PageName = r["PageName"].ToString(),
                    Status = Convert.ToBoolean(r["Status"]),
                });
            }
            return result;
        }
        public static int SaveMapping(string CategoryID, string listdata)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CategoryID", CategoryID));
            param.Add(new SqlParameter("@listdata", listdata));
            param.Add(new SqlParameter("@UserID", HttpContext.Current.User.Identity.Name));
            return new SqlHelper().ExecuteNoneQuery("p_CRM_Page_SaveMapping", param, CommandType.StoredProcedure);
        }
    }
}