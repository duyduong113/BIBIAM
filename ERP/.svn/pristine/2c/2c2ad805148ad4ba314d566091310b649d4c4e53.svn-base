using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_RecallRemind
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public DateTime RecallDate { get; set; }
        public int RowID { get; set; }

        public static List<DC_RecallRemind> getAllReminderOfUser(string Assigned)
        {
            string procedure = "p_SelectRemindCall";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assigned";
            parameters[i].Value = Assigned;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_RecallRemind
            {
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["FullName"].ToString(),
                Phone = row["MobilePhone"].ToString(),
                Note = row["Note"].ToString(),
                RecallDate = DateTime.Parse(row["RecallDate"].ToString()),
                RowID = int.Parse(row["RowID"].ToString()),
            }).ToList();
        }

        public static List<DC_RecallRemind> getAllReminderOfUserToDay(string Assigned)
        {
            string procedure = "p_SelectRemindCallToDay";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assigned";
            parameters[i].Value = Assigned;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_RecallRemind
            {
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["FullName"].ToString(),
                Phone = row["MobilePhone"].ToString(),
                Note = row["Note"].ToString(),
                RecallDate = DateTime.Parse(row["RecallDate"].ToString()),
                RowID = int.Parse(row["RowID"].ToString()),
            }).ToList();
        }


        public static List<DC_RecallRemind> getAllReminderOfUserNext7Day(string Assigned)
        {
            string procedure = "p_SelectRemindCallToDayNext7Day";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assigned";
            parameters[i].Value = Assigned;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_RecallRemind
            {
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["FullName"].ToString(),
                Phone = row["MobilePhone"].ToString(),
                Note = row["Note"].ToString(),
                RecallDate = DateTime.Parse(row["RecallDate"].ToString()),
                RowID = int.Parse(row["RowID"].ToString()),
            }).ToList();
        }
    }
}