using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class UserInOrg
    {
        #region Members
        private string _userid = String.Empty;
        public string UserID { get { return _userid; } set { this._userid = value; } }

        private string _orgid = String.Empty;
        public string OrgID { get { return _orgid; } set { this._orgid = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get { return _createddatetime; } set { this._createddatetime = value; } }

        private string _createduser = String.Empty;
        public string CreatedUser { get { return _createduser; } set { this._createduser = value; } }

        #endregion

        public UserInOrg()
        { }

        public UserInOrg(string UserID, string OrgID, DateTime CreatedDatetime, string CreatedUser)
        {
            this._userid = UserID;
            this._orgid = OrgID;
            this._createddatetime = CreatedDatetime;
            this._createduser = CreatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveUserInOrg";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this._userid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = this._orgid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this._createddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this._createduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateUserInOrg";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this._userid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = this._orgid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this._createddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this._createduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteUserInOrg";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this._userid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = this._orgid;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static UserInOrg GetUserInOrg(string UserID, string OrgID)
        {
            string PROCEDURE = "p_SelectUserInOrg";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = OrgID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new UserInOrg
            {
                UserID = row["UserID"].ToString(),
                OrgID = row["OrgID"].ToString(),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<UserInOrg> GetUserInOrgs(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectUserInOrgsDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new UserInOrg
            {
                UserID = row["UserID"].ToString(),
                OrgID = row["OrgID"].ToString(),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString()
            }).ToList();
        }

        public static List<UserInOrg> GetAllUserInOrgs()
        {
            string PROCEDURE = "p_SelectUserInOrgsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new UserInOrg
            {
                UserID = row["UserID"].ToString(),
                OrgID = row["OrgID"].ToString(),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString()
            }).ToList();
        }
    }
}