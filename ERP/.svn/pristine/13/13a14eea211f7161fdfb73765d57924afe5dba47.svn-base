using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_SMS_Template.
    /// </summary>
    public class DC_SMS_Template
    {
        #region Members
        private int _smstempid;
        public int SmsTempID { get { return _smstempid; } set { this._smstempid = value; } }

        private string _title = String.Empty;
        public string Title { get { return _title; } set { this._title = value; } }

        private int _maxnumber;
        public int MaxNumber { get { return _maxnumber; } set { this._maxnumber = value; } }

        private string _message = String.Empty;
        public string Message { get { return _message; } set { this._message = value; } }

        private string _roles = String.Empty;
        public string Roles { get { return _roles; } set { this._roles = value; } }

        private int _rowid;
        public int RowID { get { return _rowid; } set { this._rowid = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        #endregion

        public DC_SMS_Template()
        { }

        public DC_SMS_Template(int SmsTempID, string Title, int MaxNumber, string Message, string Roles, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._smstempid = SmsTempID;
            this._title = Title;
            this._maxnumber = MaxNumber;
            this._message = Message;
            this._roles = Roles;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_SMS_Template";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SmsTempID";
            parameters[i].Value = this._smstempid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Title";
            parameters[i].Value = this._title;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MaxNumber";
            parameters[i].Value = this._maxnumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Message";
            parameters[i].Value = this._message;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Roles";
            parameters[i].Value = this._roles;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this._rowid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_SMS_Template";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SmsTempID";
            parameters[i].Value = this._smstempid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Title";
            parameters[i].Value = this._title;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MaxNumber";
            parameters[i].Value = this._maxnumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Message";
            parameters[i].Value = this._message;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Roles";
            parameters[i].Value = this._roles;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this._rowid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_SMS_Template";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@SmsTempID";
            parameters[0].Value = SmsTempID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_SMS_Template GetDC_SMS_Template(int SmsTempID)
        {
            string PROCEDURE = "p_SelectDC_SMS_Template";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@SmsTempID";
            parameters[0].Value = SmsTempID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_SMS_Template
            {
                SmsTempID = Convert.ToInt32(row["SmsTempID"].ToString()),
                Title = row["Title"].ToString(),
                MaxNumber = Convert.ToInt32(row["MaxNumber"].ToString()),
                Message = row["Message"].ToString(),
                Roles = row["Roles"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_SMS_Template> GetDC_SMS_Templates(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_SMS_TemplatesDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_SMS_Template
            {
                SmsTempID = Convert.ToInt32(row["SmsTempID"].ToString()),
                Title = row["Title"].ToString(),
                MaxNumber = Convert.ToInt32(row["MaxNumber"].ToString()),
                Message = row["Message"].ToString(),
                Roles = row["Roles"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_SMS_Template> GetAllDC_SMS_Templates()
        {
            string PROCEDURE = "p_SelectDC_SMS_TemplatesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_SMS_Template
            {
                SmsTempID = Convert.ToInt32(row["SmsTempID"].ToString()),
                Title = row["Title"].ToString(),
                MaxNumber = Convert.ToInt32(row["MaxNumber"].ToString()),
                Message = row["Message"].ToString(),
                Roles = row["Roles"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}
