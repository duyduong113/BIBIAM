using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_Organization_Meta
    {
        #region Members
        private string _organizationid = String.Empty;
        public string OrganizationID { get { return _organizationid; } set { this._organizationid = value; } }

        private string _factor = String.Empty;
        public string Factor { get { return _factor; } set { this._factor = value; } }

        private string _value = String.Empty;
        public string Value { get { return _value; } set { this._value = value; } }

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

        public DC_Organization_Meta()
        { }

        public DC_Organization_Meta(string OrganizationID, string Factor, string Value, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._organizationid = OrganizationID;
            this._factor = Factor;
            this._value = Value;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Organization_Meta";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
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
            try
            {
                int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
                Helpers.RemoveCache.CacheOrganization();
                return result;
            }
            catch
            {
                this.RowLastUpdatedTime = this.RowCreatedTime;
                this.RowLastUpdatedUser = this.RowCreatedUser;
                return this.Update();
            }

        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Organization_Meta";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
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
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheOrganization();
            return result;
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Organization_Meta";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Factor";
            parameters[i].Value = this._factor;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheOrganization();
            return result;
        }

        public static DC_Organization_Meta GetDC_Organization_Meta(string OrganizationID, string Factor)
        {
            string PROCEDURE = "p_SelectDC_Organization_Meta";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Factor";
            parameters[i].Value = Factor;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Organization_Meta
            {
                OrganizationID = row["OrganizationID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Organization_Meta> GetDC_Organization_Metas(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Organization_MetasDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Organization_Meta
            {
                OrganizationID = row["OrganizationID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Organization_Meta> GetAllDC_Organization_Metas()
        {
            string PROCEDURE = "p_SelectDC_Organization_MetasAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Organization_Meta
            {
                OrganizationID = row["OrganizationID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }


        public static DC_Organization_Meta CheckOrganization(string OrganizationID, string where)
        {
            string PROCEDURE = "CheckInfoOrganization";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@where";
            parameters[i].Value = where;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Organization_Meta
            {
                Value = row["Customer#"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static DC_Organization_Meta CheckOrgInCustomerCreationRequest(string OrganizationID)
        {
            string PROCEDURE = "p_GetInfoCheckInCustomerCreationRequest";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Organization_Meta
            {
                OrganizationID = row["OrganizationID"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Employee = Convert.ToInt32(row["Employee#"].ToString()),
                Register = Convert.ToInt32(row["Register#"].ToString()),
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Organization_Meta> CheckOrgInCustomerCreationRequestAll()
        {
            string PROCEDURE = "p_GetInfoCheckInCustomerCreationRequestAll";
            SqlParameter[] parameters = new SqlParameter[1];
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Organization_Meta
            {
                OrganizationID = row["OrganizationID"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Employee = Convert.ToInt32(row["Employee#"].ToString()),
                Register = Convert.ToInt32(row["Register#"].ToString()),
            }).ToList();
        }

        public int Register { get; set; }

        public int Employee { get; set; }

        public string OrgStatus { get; set; }
    }
}