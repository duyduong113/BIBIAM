using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Location_Region
    {
        #region Members
        private string _regionid = String.Empty;
        public string RegionID { get { return _regionid; } set { this._regionid = value; } }

        private string _regionname = String.Empty;
        public string RegionName { get { return _regionname; } set { this._regionname = value; } }

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

        private string _countryid = String.Empty;
        public string CountryID { get { return _countryid; } set { this._countryid = value; } }
        [Ignore]
        public string listUser { get; set; }
        #endregion

        public DC_Location_Region()
        { }

        public DC_Location_Region(string RegionID, string RegionName, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser, string CountryID)
        {
            this._regionid = RegionID;
            this._regionname = RegionName;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._countryid = CountryID;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Location_Region";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RegionID";
            parameters[i].Value = this._regionid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RegionName";
            parameters[i].Value = this._regionname;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CountryID";
            parameters[i].Value = this._countryid;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Location_Region";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RegionID";
            parameters[i].Value = this._regionid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RegionName";
            parameters[i].Value = this._regionname;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CountryID";
            parameters[i].Value = this._countryid;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Location_Region";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RegionID";
            parameters[0].Value = RegionID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Location_Region GetDC_Location_Region(string RegionID)
        {
            string PROCEDURE = "p_SelectDC_Location_Region";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RegionID";
            parameters[0].Value = RegionID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Location_Region
            {
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CountryID = row["CountryID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Location_Region> GetDC_Location_Regions(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Location_RegionsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Location_Region
            {
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CountryID = row["CountryID"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasName = row["AliasName"].ToString(),
            }).ToList();
        }
        [Ignore]
        public string AliasName { get; set; }
        [Ignore]

        public string CountryName { get; set; }
        public static List<DC_Location_Region> GetAllDC_Location_Regions()
        {
            string PROCEDURE = "p_SelectDC_Location_RegionsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_Region
            {
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CountryID = row["CountryID"].ToString(),
                CountryName = row["CountryName"].ToString(),
                listUser = row["UserInRegion"].ToString()
            }).ToList();
        }

        public static List<DC_Location_Region> GetList_Regions()
        {
            string PROCEDURE = "select distinct [RegionID],[RegionName] from [DC_Location_Region] where [Active] = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_Region
            {
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
            }).ToList();
        }

        public static List<DropListDown> GetListRegion(string Country)
        {
            string PROCEDURE = "select distinct RegionID,RegionName from DC_Location_Region  where Active = 1 and CountryID='" + Country.Replace("'", "''") + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DropListDown
            {
                Text = row["RegionName"].ToString(),
                Value = row["RegionID"].ToString(),
            }).ToList();
        }


    }
}