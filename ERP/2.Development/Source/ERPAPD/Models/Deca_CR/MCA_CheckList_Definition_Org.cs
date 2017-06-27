using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_CheckList_ApplyFor
    {
        #region Members
        private string _checklistsubid = String.Empty;
        public string ChecklistSubID { get { return _checklistsubid; } set { this._checklistsubid = value; } }

        private string _checklistid = String.Empty;
        public string ChecklistID { get { return _checklistid; } set { this._checklistid = value; } }

        private string _organizationid = String.Empty;
        public string OrganizationID { get { return _organizationid; } set { this._organizationid = value; } }

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

        private string _companyid = String.Empty;
        public string CompanyID { get { return _companyid; } set { this._companyid = value; } }

        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string CompanyPhone { get; set; }
        public int NoOfEmployee { get; set; }

        #endregion

        public DC_CheckList_ApplyFor()
        { }

        public DC_CheckList_ApplyFor(string Company, string ChecklistSubID, string ChecklistID, string OrganizationID, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._checklistsubid = ChecklistSubID;
            this._checklistid = ChecklistID;
            this._organizationid = OrganizationID;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._companyid = CompanyID;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_CheckList_Definition_Org";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistSubID";
            parameters[i].Value = this._checklistsubid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistID";
            parameters[i].Value = this._checklistid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
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
            parameters[i].ParameterName = "@CompanyID";
            parameters[i].Value = this._companyid;
            i++;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_CheckList_Definition_Org";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistSubID";
            parameters[i].Value = this._checklistsubid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistID";
            parameters[i].Value = this._checklistid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
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
            parameters[i].ParameterName = "@CompanyID";
            parameters[i].Value = this._companyid;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }


        private string _checklistiddefault = String.Empty;
        public string ChecklistIDDefault { get { return _checklistiddefault; } set { this._checklistiddefault = value; } }
        public int UpdateIsDefault()
        {
            string PROCEDURE = "p_UpdateDC_CheckList_Definition_OrgDefault";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistID";
            parameters[i].Value = this._checklistid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistIDDefault";
            parameters[i].Value = this._checklistiddefault;
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
            string PROCEDURE = "p_DeleteDC_CheckList_Definition_Org";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ChecklistSubID";
            parameters[0].Value = ChecklistSubID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static List<DC_CheckList_ApplyFor> GetDC_CheckList_Definition_Org(string ChecklistID)
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_Org";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ChecklistID";
            parameters[0].Value = ChecklistID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_CheckList_ApplyFor
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                Address = row["Address"].ToString(),
                CompanyPhone = row["CompanyPhone"].ToString(),
                NoOfEmployee = Convert.ToInt32(row["NoOfEmployee"].ToString()),                
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString()                 
            }).ToList();
        }
        public string LongName { get; set; }
        public static List<DC_CheckList_ApplyFor> GetDC_CheckList_Definition_Orgs(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_OrgsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_CheckList_ApplyFor
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CompanyID = row["CompanyID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_CheckList_ApplyFor> GetAllDC_CheckList_Definition_Orgs()
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_OrgsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CheckList_ApplyFor
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        public static List<DC_CheckList_ApplyFor> GetOrgList(string ChecklistID)
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_Org_GetOrgList";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ChecklistID";
            parameters[0].Value = ChecklistID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_CheckList_ApplyFor
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static DC_CheckList_ApplyFor checkExistChecklist(string company)
        {
            string PROCEDURE = "SELECT top 1 * FROM DC_CheckList_Definition_Org WHERE CompanyID = N'" + company + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CheckList_ApplyFor
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CompanyID = row["CompanyID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),                
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).FirstOrDefault();
        }
    }
}