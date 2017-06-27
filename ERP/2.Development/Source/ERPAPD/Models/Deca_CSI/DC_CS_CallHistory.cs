using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_CS_CallHistory
    {
        [AutoIncrement]
        public int LogID { get; set; }
        public string OrganizationID { get; set; }
        public string CustomerID { get; set; }
        public string ResultID { get; set; }
        public string SubResultID { get; set; }
        public string OrderID { get; set; }
        public string CallType { get; set; }
        public string Note { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }

        [Ignore]
        public string NoteCallLog { get; set; }
        [Ignore]
        public string MobilePhone { get; set; }
        [Ignore]
        public string GroupID { get; set; }
        [Ignore]
        public string Department { get; set; }

        public static List<DC_CS_CallHistory> GetByCustomerID(string CustomerID)
        {
            string PROCEDURE = "p_DC_CS_CallHistory_ByCustomerID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_CS_CallHistory
            {
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                ResultID = row["ResultID"].ToString(),
                SubResultID = row["SubResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                CallType = row["CallType"].ToString(),
                Note = row["Note"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList();
        }

        public static List<DC_CS_CallHistory> GetAllData()
        {
            string PROCEDURE = "p_DC_CS_CallHistory_GetAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CS_CallHistory
            {
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                ResultID = row["ResultID"].ToString(),
                SubResultID = row["SubResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                CallType = row["CallType"].ToString(),
                Note = row["Note"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                GroupID = row["GroupID"].ToString(),
                Department = row["Department"].ToString(),
            }).ToList();
        }
    }
}