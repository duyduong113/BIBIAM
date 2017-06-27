using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a Deca_RT_RoleUpdate.
    /// </summary>
    public class Deca_RT_RoleUpdate
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

        public Deca_RT_RoleUpdate()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDeca_RT_RoleUpdate";
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
            string PROCEDURE = "p_UpdateDeca_RT_RoleUpdate";
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
            string PROCEDURE = "p_DeleteDeca_RT_RoleUpdate";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserName;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static Deca_RT_RoleUpdate GetDeca_RT_RoleUpdate(string UserName)
        {
            string PROCEDURE = "p_SelectDeca_RT_RoleUpdate";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_RoleUpdate
            {
                UserName =row["UserID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<Deca_RT_RoleUpdate> GetDeca_RT_RoleUpdates(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDeca_RT_RoleUpdatesDynamic";
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
        //    return dt.AsEnumerable().Select(row => new Deca_RT_RoleUpdate
        //    {
        //        UserName = Convert.ToInt32(row["UserID"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<Deca_RT_RoleUpdate> GetAllDeca_RT_RoleUpdates()
        {
            string PROCEDURE = "p_SelectDeca_RT_RoleUpdatesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_RT_RoleUpdate
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
        public static List<Deca_RT_RoleUpdate> GetListUserName()
        {
            string PROCEDURE = "p_SelectDeca_RT_RoleUpdate_ListUser";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_RT_RoleUpdate
            {
                UserID = row["UserName"].ToString(),
                UserName = row["UserName"].ToString(),
            }).ToList();
        }
        public static string GetListType(string UserName)
        {
            string PROCEDURE = "p_SelectDeca_RT_RoleUpdate_ListType";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            string result ="";
            foreach (DataRow row in dt.Rows)
            {
                result += row[0].ToString()+ ";";
            }
            return result;
           
        }
        public static bool Check(string UserName, string TypeID)
        {
            string PROCEDURE = "p_SelectDeca_RT_RoleUpdate_Check";
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
