using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    public class CRM_Location_Countries
    {
        #region Members
        private string _countryid = String.Empty;
        public string CountryID { get { return _countryid; } set { this._countryid = value; } }

        private string _countryname = String.Empty;
        public string CountryName { get { return _countryname; } set { this._countryname = value; } }

        private string _aliasid = String.Empty;
        public string AliasID { get { return _aliasid; } set { this._aliasid = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

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

        public CRM_Location_Countries()
        { }

        public CRM_Location_Countries(string CountryID, string CountryName, string AliasID, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._countryid = CountryID;
            this._countryname = CountryName;
            this._aliasid = AliasID;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveCRM_Location_Country";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CountryID";
            parameters[i].Value = this._countryid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CountryName";
            parameters[i].Value = this._countryname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AliasID";
            parameters[i].Value = this._aliasid;
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
            string PROCEDURE = "p_UpdateCRM_Location_Country";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CountryID";
            parameters[i].Value = this._countryid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CountryName";
            parameters[i].Value = this._countryname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AliasID";
            parameters[i].Value = this._aliasid;
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
            string PROCEDURE = "p_DeleteCRM_Location_Country";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CountryID";
            parameters[0].Value = CountryID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static CRM_Location_Countries GetDC_Location_Countrie(string CountryID)
        {
            string PROCEDURE = "p_SelectCRM_Location_Countries";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CountryID";
            parameters[0].Value = CountryID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Location_Countries
            {
                CountryID = row["CountryID"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasID = row["AliasID"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<CRM_Location_Countries> GetCRM_Location_Countries(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectCRM_Location_CountriesDynamic";
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
            return dt.AsEnumerable().Select(row => new CRM_Location_Countries
            {
                CountryID = row["CountryID"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasID = row["AliasID"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                AliasName = row["AliasName"].ToString(),
            }).ToList();
        }
        public string AliasName { get; set; }
        public static List<CRM_Location_Countries> GetAllCRM_Location_Countries()
        {
            string PROCEDURE = "p_SelectCRM_Location_CountriesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Location_Countries
            {
                CountryID = row["CountryID"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasID = row["AliasID"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                AliasName = row["AliasName"].ToString(),
            }).ToList();
        }

        public static List<CRM_Location_Countries> GetList_Countries()
        {
            string PROCEDURE = "select distinct CountryID,CountryName from CRM_Location_Countries  where Active = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new CRM_Location_Countries
            {
                CountryID = row["CountryID"].ToString(),
                CountryName = row["CountryName"].ToString(),

            }).ToList();
        }



    }
}