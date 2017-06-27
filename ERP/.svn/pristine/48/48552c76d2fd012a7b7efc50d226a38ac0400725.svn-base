using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.OrmLite;

namespace ERPAPD.Models
{
    public class DC_Param
    {
        #region Members
        private string _paramid = String.Empty;
        public string ParamID { get { return _paramid; } set { this._paramid = value; } }

        private string _paramname = String.Empty;
        public string ParamName { get { return _paramname; } set { this._paramname = value; } }

        private string _paramvalue = String.Empty;
        public string ParamValue { get { return _paramvalue; } set { this._paramvalue = value; } }

        private bool _active;
        [Ignore]
        public bool Active { get { return _active; } set { this._active = value; } }

        private int _rowid;
        [Ignore]
        public int RowID { get { return _rowid; } set { this._rowid = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime? _rowlastupdatedtime;
        public DateTime? RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        #endregion

        public DC_Param()
        { }

        public DC_Param(string ParamID, string ParamName, string ParamValue, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._paramid = ParamID;
            this._paramname = ParamName;
            this._paramvalue = ParamValue;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Param";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamID";
            parameters[i].Value = this._paramid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamName";
            parameters[i].Value = this._paramname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamValue";
            parameters[i].Value = this._paramvalue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Param";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamID";
            parameters[i].Value = this._paramid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamName";
            parameters[i].Value = this._paramname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamValue";
            parameters[i].Value = this._paramvalue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
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
            string PROCEDURE = "p_DeleteDC_Param";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ParamID";
            parameters[0].Value = ParamID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        //public static void UpdateTrans(IDbConnection dbConn,string ParamID, string ParamValue, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        //{
        //    DC_Param_Transaction p = new DC_Param_Transaction();
        //    p.ParamID = ParamID;
        //    p.ParamValue = ParamValue;
        //    p.RowLastUpdatedTime = RowLastUpdatedTime;
        //    p.RowLastUpdatedUser = RowLastUpdatedUser;
        //    dbConn.Update(p);
        //}
        public static DC_Param GetDC_Param(string ParamID)
        {
            string PROCEDURE = "p_SelectDC_Param";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ParamID";
            parameters[0].Value = ParamID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Param
            {
                ParamID = row["ParamID"].ToString(),
                ParamName = row["ParamName"].ToString(),
                ParamValue = row["ParamValue"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Param> GetDC_Params(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_ParamsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Param
            {
                ParamID = row["ParamID"].ToString(),
                ParamName = row["ParamName"].ToString(),
                ParamValue = row["ParamValue"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Param> GetAllDC_Params()
        {
            string PROCEDURE = "p_SelectDC_ParamsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Param
            {
                ParamID = row["ParamID"].ToString(),
                ParamName = row["ParamName"].ToString(),
                ParamValue = row["ParamValue"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        public static string GetParam(string ParamID, string Header)
        {
            for (int i = 0; i < 3; i++)
            {
                object result = GetParam_database(ParamID, Header);
                if (result != null && result.ToString() != "")
                { 
                    return result.ToString(); 
                }
            }
            return "";
        }

        private static object GetParam_database(string ParamID, string Header)
        {
            string PROCEDURE = "p_SelectGetParamInvoice";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamID";
            parameters[i].Value = ParamID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ParamHeader";
            parameters[i].Value = Header + DateTime.Now.ToString("yyMMdd");
            i++;
            return SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
    }
}