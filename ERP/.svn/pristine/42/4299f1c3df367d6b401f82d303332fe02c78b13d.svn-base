using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_Customer_Resigned_Activity_CusInfo_Meta
    {
        #region Members
        private string _customerid = String.Empty;
        public string CustomerID { get { return _customerid; } set { this._customerid = value; } }

        private string _factor = String.Empty;
        public string Factor { get { return _factor; } set { this._factor = value; } }

        private string _value = String.Empty;
        public string Value { get { return _value; } set { this._value = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        #endregion

        public DC_Customer_Resigned_Activity_CusInfo_Meta()
        { }

        public DC_Customer_Resigned_Activity_CusInfo_Meta(string CustomerID, string Factor, string Value, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._customerid = CustomerID;
            this._factor = Factor;
            this._value = Value;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Customer_Resigned_Activity_CusInfo_Meta";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Factor";
            parameters[i].Value = this._factor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Value";
            parameters[i].Value = this._value;
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
            string PROCEDURE = "p_UpdateDC_Customer_Resigned_Activity_CusInfo_Meta";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Factor";
            parameters[i].Value = this._factor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Value";
            parameters[i].Value = this._value;
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
            string PROCEDURE = "p_DeleteDC_Customer_Resigned_Activity_CusInfo_Meta";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Factor";
            parameters[1].Value = Factor;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static DC_Customer_Resigned_Activity_CusInfo_Meta GetDC_Customer_Resigned_Activity_CusInfo_Meta(string CustomerID,string Factor)
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_Activity_CusInfo_Meta";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Factor";
            parameters[1].Value = Factor;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity_CusInfo_Meta
            {
                CustomerID = row["CustomerID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Customer_Resigned_Activity_CusInfo_Meta> GetDC_Customer_Resigned_Activity_CusInfo_Metas(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_Activity_CusInfo_MetasDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity_CusInfo_Meta
            {
                CustomerID = row["CustomerID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Customer_Resigned_Activity_CusInfo_Meta> GetAllDC_Customer_Resigned_Activity_CusInfo_Metas()
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_Activity_CusInfo_MetasAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity_CusInfo_Meta
            {
                CustomerID = row["CustomerID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}