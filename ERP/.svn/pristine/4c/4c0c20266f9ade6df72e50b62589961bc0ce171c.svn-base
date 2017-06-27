using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_TeleSale_History_CallResult
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(256)]
        public string CustomerID { get; set; }
        [StringLength(32)]
        public string Rule { get; set; }
        public DateTime RecallDate { get; set; }
        [StringLength(32)]
        public string ResultId { get; set; }
        [StringLength(32)]
        public string SubResultId { get; set; }
        public string Note { get; set; }
        public int isDone { get; set; }
        //People set ReAssign
        public string SetRecallBy { get; set; }
        [StringLengthAttribute(100)]
        public string ReAssign { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
        public string TxnID { get; set; }
        public string TxnType { get; set; }
        public int PreOrderId { get; set; }
        public string TicketId { get; set; }
        public bool FromBy { get; set; }

        //add column load data
        [Ignore]
        public string ResultName { get; set; }
        [Ignore]
        public string FamilyName { get; set; }
        [Ignore]
        public string GivenName { get; set; }
        [Ignore]
        public string FullName { get; set; }
        [Ignore]
        public string Phone { get; set; }
        [Ignore]
        public int CountRemider { get; set; }
        [Ignore]
        public string Team { get; set; }
        [Ignore]
        public string Type { get; set; }
        [Ignore]
        public int Amount { get; set; }
        [Ignore]
        public DateTime TranDate { get; set; }
        [Ignore]
        public DateTime CompleteDate { get; set; }
        [Ignore]
        public string Status { get; set; }
        [Ignore]
        public string Description { get; set; }
        public string CallFrom { get; set; }

        public static List<DC_TeleSale_History_CallResult> GetDC_TeleSale_History_CallResult_ToDate(string user)
        {
            string PROCEDURE = "p_SelectDC_TeleSale_History_CallResult_ToDate";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@user";
            parameters[i].Value = user;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                CustomerID = row["CustomerID"].ToString(),
                RecallDate = Convert.ToDateTime(row["RecallDate"].ToString()),
                ResultName =row["ResultName"].ToString(),
                Note = row["Note"].ToString(),
                GivenName = row["GivenName"].ToString(),
                FamilyName = row["FamilyName"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList();
        }

        public static List<DC_TeleSale_History_CallResult> GetDC_TeleSale_History_CallResult_Last7Day(string user)
        {
            string PROCEDURE = "p_SelectDC_TeleSale_History_CallResult_Last7Day";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@user";
            parameters[i].Value = user;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                CustomerID = row["CustomerID"].ToString(),
                RecallDate = Convert.ToDateTime(row["RecallDate"].ToString()),
                ResultName = row["ResultName"].ToString(),
                Note = row["Note"].ToString(),
                GivenName = row["GivenName"].ToString(),
                FamilyName = row["FamilyName"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList();
        }

        public static DC_TeleSale_History_CallResult GetDC_TeleSale_History_CallResult_Last7DayNextCustomer(string user)
        {
            string PROCEDURE = "p_SelectDC_TeleSale_History_CallResult_Last7Day_NextCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@user";
            parameters[i].Value = user;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                CustomerID = row["CustomerID"].ToString(),
                Id = int.Parse(row["Id"].ToString())
            }).ToList().FirstOrDefault();
        }
        
        public static DC_TeleSale_History_CallResult GetDC_TeleSale_History_CallResult_CountCustomer(string user)
        {
            string PROCEDURE = "p_SelectDC_TeleSale_History_CallResult_CountCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@user";
            parameters[i].Value = user;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                CountRemider = int.Parse(row["CountRemider"].ToString())
            }).ToList().FirstOrDefault();
        }


        public static List<DC_TeleSale_History_CallResult> getAllReminderOfUserToDay(string Assigned)
        {
            string procedure = "p_SelectDC_TeleSale_History_CallResultRemindCallToDay";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assigned";
            parameters[i].Value = Assigned;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Phone = row["MobilePhone"].ToString(),
                Note = row["Note"].ToString(),
                RecallDate = DateTime.Parse(row["RecallDate"].ToString()),
                Id = int.Parse(row["Id"].ToString()),
            }).ToList();
        }

        public static List<DC_TeleSale_History_CallResult> getAllReminderOfUserNext7Day(string Assigned)
        {
            string procedure = "p_SelectDC_TeleSale_History_CallResultRemindCallNext7Day";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assigned";
            parameters[i].Value = Assigned;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Phone = row["MobilePhone"].ToString(),
                Note = row["Note"].ToString(),
                RecallDate = DateTime.Parse(row["RecallDate"].ToString()),
                Id = int.Parse(row["Id"].ToString()),
            }).ToList();
        }

        public static List<DC_TeleSale_History_CallResult> getAllCallResult_Report(string Level, string Team, string Agent, string WhereCondition)
        {
            string PROCEDURE = "p_DC_TeleSale_History_CallResult_Report";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Level";
            parameters[i].Value = Level;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Team";
            parameters[i].Value = Team;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Agent";
            parameters[i].Value = Agent;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_History_CallResult
            {
                ResultId = row["ResultId"].ToString(),
                SubResultId = row["SubResultId"].ToString(),
                Team = row["Team"].ToString(),
                TxnID = row["TxnID"].ToString(),
                TxnType = row["TxnType"].ToString(),
                Type = row["Type"].ToString(),
                Amount = int.Parse(row["Amount"].ToString()),
                TranDate = DateTime.Parse(row["TranDate"].ToString()),
                CompleteDate = DateTime.Parse(row["CompleteDate"].ToString()),
                Status = row["Status"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                RowCreatedTime = DateTime.Parse(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                CustomerID = row["CustomerID"].ToString()
            }).ToList();
        }
    }
}