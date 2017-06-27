using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_Ticket_Queue
    {
        #region Members

        public string QueueID { get; set; }


        public string QueueName { get; set; }


        public string Description { get; set; }


        public string Department { get; set; }


        public string Team { get; set; }


        public string User { get; set; }


        public string Status { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }
        public string UserName { get; set; }

        #endregion

        public CRM_Ticket_Queue(){ }


        public int Save()
        {
            string PROCEDURE = "p_SaveCRM_Ticket_Queue";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QueueName";
            parameters[i].Value = this.QueueName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateCRM_Ticket_Queue";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QueueID";
            parameters[i].Value = this.QueueID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QueueName";
            parameters[i].Value = this.QueueName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this.Department;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Team";
            parameters[i].Value = this.Team;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@User";
            parameters[i].Value = this.User;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteCRM_Ticket_Queue";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@QueueID";
            parameters[0].Value = QueueID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static CRM_Ticket_Queue GetCRM_Ticket_Queue(string QueueID)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_Queue";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@QueueID";
            parameters[0].Value = QueueID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Queue
            {
                QueueID = row["QueueID"].ToString(),
                QueueName = row["QueueName"].ToString(),
                Description = row["Description"].ToString(),
                Department = row["Department"].ToString(),
                Team = row["Team"].ToString(),
                User = row["User"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<CRM_Ticket_Queue> GetCRM_Ticket_Queues(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectCRM_Ticket_QueuesDynamic";
        //    SqlParameter[] parameters = new SqlParameter[2];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@WhereCondition";
        //    parameters[i].Value = whereCondition;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@OrderByExpression";
        //    parameters[i].Value = orderByExpression;

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new CRM_Ticket_Queue
        //    {
        //        QueueID = row["QueueID"].ToString(),
        //        QueueName = row["QueueName"].ToString(),
        //        Description = row["Description"].ToString(),
        //        Department = row["Department"].ToString(),
        //        Team = row["Team"].ToString(),
        //        User = row["User"].ToString(),
        //        Status = Convert.ToBoolean(row["Status"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<CRM_Ticket_Queue> GetAllCRM_Ticket_Queues()
        {
            string PROCEDURE = "p_SelectCRM_Ticket_QueuesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Queue
            {
                QueueID = row["QueueID"].ToString(),
                QueueName = row["QueueName"].ToString(),
                Description = row["Description"].ToString(),
                Department = row["Department"].ToString(),
                Team = row["Team"].ToString(),
                User = row["User"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }


        public static List<CRM_Ticket_Queue> GetListUserInQueue(string QueueID)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_Queue_User";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@QueueID";
            parameters[0].Value = QueueID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Queue
            {
                UserName = row["UserName"].ToString(),

            }).ToList();
        }
        public static bool CheckUserInQueue(string UserName, string QueueID)
        {
            if (UserName != "" && QueueID != "")
            {
                string PROCEDURE = "p_SelectCRM_Ticket_Queue_CheckUserInQueue";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter();
                parameters[0].ParameterName = "@UserID";
                parameters[0].Value = UserName;

                parameters[1] = new SqlParameter();
                parameters[1].ParameterName = "@QueueID";
                parameters[1].Value = QueueID;

                DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}