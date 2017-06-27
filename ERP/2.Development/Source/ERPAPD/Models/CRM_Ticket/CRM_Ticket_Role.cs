using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    public class CRM_Ticket_Role
    {
        #region Members
        public int ID { get; set; }
        public int RowID { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        #endregion

        public CRM_Ticket_Role()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveCRM_Ticket_Role";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
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
            string PROCEDURE = "p_UpdateCRM_Ticket_Role";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
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
            string PROCEDURE = "p_DeleteCRM_Ticket_Role";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static CRM_Ticket_Role GetCRM_Ticket_Role(int ID)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_Role";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Role
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<CRM_Ticket_Role> GetCRM_Ticket_Roles(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectCRM_Ticket_RolesDynamic";
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
        //    return dt.AsEnumerable().Select(row => new CRM_Ticket_Role
        //    {
        //        ID = Convert.ToInt32(row["ID"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<CRM_Ticket_Role> GetAllCRM_Ticket_Roles()
        {
            string PROCEDURE = "p_SelectCRM_Ticket_RolesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Role
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Description = row["Description"].ToString(),
                Name = row["Name"].ToString(),
            }).ToList();
        }

        public static List<CRM_Ticket_Role> GetListGroupID()
        {
            string PROCEDURE = "p_SelectCRM_Ticket_Role_ListGroup";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Role
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                Name = row["Name"].ToString(),
            }).ToList();
        }
    }
}
