using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Location_City
    {
        #region Members
        private string _cityid = String.Empty;
        public string CityID { get { return _cityid; } set { this._cityid = value; } }

        private string _cityname = String.Empty;
        public string CityName { get { return _cityname; } set { this._cityname = value; } }

        private string _regionid = String.Empty;
        public string RegionID { get { return _regionid; } set { this._regionid = value; } }

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

        public DC_Location_City()
        { }

        public DC_Location_City(string CityID, string CityName, string RegionID, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._cityid = CityID;
            this._cityname = CityName;
            this._regionid = RegionID;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Location_City";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CityID";
            parameters[i].Value = this._cityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CityName";
            parameters[i].Value = this._cityname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RegionID";
            parameters[i].Value = this._regionid;
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
            string PROCEDURE = "p_UpdateDC_Location_City";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CityID";
            parameters[i].Value = this._cityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CityName";
            parameters[i].Value = this._cityname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RegionID";
            parameters[i].Value = this._regionid;
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
            string PROCEDURE = "p_DeleteDC_Location_City";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CityID";
            parameters[0].Value = CityID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Location_City GetDC_Location_City(string CityID)
        {
            string PROCEDURE = "p_SelectDC_Location_City";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CityID";
            parameters[0].Value = CityID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Location_City
            {
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Location_City> GetDC_Location_Citys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Location_CitiesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Location_City
            {
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RegionName = row["RegionName"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasName = row["AliasName"].ToString(),
            }).ToList();
        }
        [Ignore]
        public string RegionName { get; set; }
        [Ignore]
        public string CountryName { get; set; }
        [Ignore]
        public string AliasName { get; set; }
        public static List<DC_Location_City> GetAllDC_Location_Citys()
        {
            string PROCEDURE = "p_SelectDC_Location_CitiesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_City
            {
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RegionName = row["RegionName"].ToString(),
            }).ToList();
        }

        public static List<DC_Location_City> GetList_City()
        {
            string PROCEDURE = "select distinct [CityID],[CityName] from [DC_Location_City] where [Active] = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_City
            {
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),
            }).ToList();
        }
        public static List<DropListDown> GetListCity(string Region)
        {
            string PROCEDURE = "select distinct CityID,CityName from DC_Location_City  where Active = 1 and RegionID='" + Region.Replace("'", "''") + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DropListDown
            {
                Text = row["CityName"].ToString(),
                Value = row["CityID"].ToString(),
            }).ToList();
        }

    }
}