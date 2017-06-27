using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    public class DC_Location_MappingLocation
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
        public string CountryName { get; set; }
        public string AliasName { get; set; }
        #region Members
        private string _cityid = String.Empty;
        public string CityID { get { return _cityid; } set { this._cityid = value; } }

        private string _cityname = String.Empty;
        public string CityName { get { return _cityname; } set { this._cityname = value; } }

        #endregion
        #endregion
        public static List<DC_Location_MappingLocation> GetAllDC_Location_Regions()
        {
            string PROCEDURE = "p_SelectDC_Location_MappingLocationAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasName = row["AliasName"].ToString(),
            }).ToList();
        }
        public string ShippingCity { get; set; }
        public static List<DC_Location_MappingLocation> GetAllDC_Location_Mapping_City(string RegionID)
        {
            string PROCEDURE = "p_SelectDC_Location_Mapping_City";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RegionID";
            parameters[0].Value = RegionID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                ShippingCity = row["ShippingCity"].ToString(),

            }).ToList();

        }

        public static List<DC_Location_MappingLocation>checkRegionANDCityName(string RegionID,string CityName)
        {
            string PROCEDURE = "select [RegionID] from DC_Location_Mapping_City where [RegionID]+[CityName] = '" + RegionID + "'+ N'" + CityName + "'";
       
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                RegionID = row["RegionID"].ToString(),
            }).ToList();

        }
        public static List<DC_Location_MappingLocation> GetAllDC_Location_Portal_City(string RegionID)
        {
            string PROCEDURE = "p_SelectDC_Location_Portal_City";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RegionID";
            parameters[0].Value = RegionID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),

            }).ToList();

        }
        public static List<DC_Location_MappingLocation> GetAllDC_Location_Mappinged_City()
        {
            string PROCEDURE = "p_SelectDC_Location_Mappinged_City";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
            }).ToList();
        }
        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Location_Mapping_City";
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
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Location_Mapping_City";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CityID";
            parameters[0].Value = CityID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        public static List<DC_Location_MappingLocation> GetAllDC_Location_Mapping_Citys()
        {
            string PROCEDURE = "p_SelectDC_Location_Mapping_CitiesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
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
            }).ToList();
        }
        public static List<DC_Location_MappingLocation> GetAllListCityMappingFrom_ShippingCity()
        {
            string PROCEDURE = "p_SelectDC_Location_Mappinged_Region_ShippingCity";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                ShippingCity = row["ShippingCity"].ToString(),
                RegionName = row["RegionName"].ToString(),
            }).ToList();
        }
        public static List<DC_Location_MappingLocation> GetAllListCityMappingFrom_PortalCity()
        {
            string PROCEDURE = "p_SelectDC_Location_Mappinged_Region_PortalCity";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                CityName = row["CityName"].ToString(),
                RegionName = row["RegionName"].ToString(),
                RegionID = row["RegionID"].ToString(),
            }).ToList();
        }
        public static List<DC_Location_MappingLocation> GetAllListWaitingMapping_ShippingCity()
        {
            string PROCEDURE = "p_SelectDC_Location_WaitingMapping_Region_ShippingCity";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                ShippingCity = row["ShippingCity"].ToString(),
                //RegionName = row["RegionName"].ToString(),
            }).ToList();
        }
        public static List<DC_Location_MappingLocation> GetAllListWaitingMapping_ShippingPortal()
        {
            string PROCEDURE = "p_SelectDC_Location_WaitingMapping_Region_PortalCity";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Location_MappingLocation
            {
                CityName = row["CityName"].ToString(),
            }).ToList();
        }
    }
}