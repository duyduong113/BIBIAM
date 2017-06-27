using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_Ticket_RoleUpdate
    {
        #region Members
        public string UserID { get; set; }

        public int RowID { get; set; }

        public DateTime RowCreatedTime { get; set; }

        public string RowCreatedUser { get; set; }

        public DateTime RowLastUpdatedTime { get; set; }

        public string RowLastUpdatedUser { get; set; }
        public string UserName { get; set; }

        #endregion

        public CRM_Ticket_RoleUpdate()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveCRM_Ticket_RoleUpdate";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this.UserID;
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
            string PROCEDURE = "p_UpdateCRM_Ticket_RoleUpdate";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this.UserName;
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
            string PROCEDURE = "p_DeleteCRM_Ticket_RoleUpdate";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserName;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static CRM_Ticket_RoleUpdate GetCRM_Ticket_RoleUpdate(string UserName)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_RoleUpdate";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_RoleUpdate
            {
                UserName = row["UserID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

     

        public static List<CRM_Ticket_RoleUpdate> GetAllCRM_Ticket_RoleUpdates()
        {
            string PROCEDURE = "p_SelectCRM_Ticket_RoleUpdatesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_RoleUpdate
            {
                UserID = row["UserID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                UserName = row["UserName"].ToString(),
            }).ToList();
        }
        public static List<CRM_Ticket_RoleUpdate> GetListUserName()
        {
            string PROCEDURE = "p_SelectCRM_Ticket_RoleUpdate_ListUser";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_RoleUpdate
            {
                UserID = row["UserName"].ToString(),
                UserName = row["UserName"].ToString(),
            }).ToList();
        }
        public static string GetListType(string UserName)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_RoleUpdate_ListType";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            string result = "";
            foreach (DataRow row in dt.Rows)
            {
                result += row[0].ToString() + ";";
            }
            return result;

        }
        public static bool Check(string UserName, string TypeID)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_RoleUpdate_Check";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = TypeID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}