using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Bank_Definition.
    /// </summary>
    public class DC_Bank_Definition
    {
        #region Members
        private string _bankid = String.Empty;
        public string BankID { get { return _bankid; } set { this._bankid = value; } }

        private string _bankname = String.Empty;
        public string BankName { get { return _bankname; } set { this._bankname = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private string _xmlstring = String.Empty;
        public string XMLString { get { return _xmlstring; } set { this._xmlstring = value; } }

        private int _rowid;
        [Ignore]
        public int RowID { get { return _rowid; } set { this._rowid = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        public string FullName { get; set; }
        public string ContractID { get; set; }
        public string Assignee { get; set; }
        public string Note { get; set; }
        public string LastAction { get; set; }
        public int DefaultInstallment { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string PaymentGateway { get; set; }
        #endregion

        public DC_Bank_Definition()
        { }

        public DC_Bank_Definition(string BankID, string BankName, bool Active, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._bankid = BankID;
            this._bankname = BankName;
            this._active = Active;
            this._xmlstring = XMLString;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Bank_Definition";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = string.IsNullOrEmpty(this._bankid) ? "" : this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankName";
            parameters[i].Value = string.IsNullOrEmpty(this._bankname) ? "" : this._bankname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = string.IsNullOrEmpty(this._xmlstring) ? "" : this._xmlstring;
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
            string PROCEDURE = "p_UpdateDC_Bank_Definition";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = string.IsNullOrEmpty(this._bankid) ? "" : this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankName";
            parameters[i].Value = string.IsNullOrEmpty(this._bankname) ? "" : this._bankname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = string.IsNullOrEmpty(this._xmlstring) ? "" : this._xmlstring;
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
            string PROCEDURE = "p_DeleteDC_Bank_Definition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Bank_Definition GetDC_Bank_Definition(string BankID)
        {
            string PROCEDURE = "p_SelectDC_Bank_Definition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Bank_Definition
           {
               BankID = row["BankID"].ToString(),
               BankName = row["BankName"].ToString(),
               Active = Convert.ToBoolean(row["Active"].ToString()),
               XMLString = row["XMLString"].ToString(),
               RowID = Convert.ToInt16(row["RowID"].ToString()),
               RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
               RowCreatedUser = row["RowCreatedUser"].ToString(),
               RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
               RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
           }).ToList().FirstOrDefault();
        }

        public static List<DC_Bank_Definition> GetDC_Bank_Definitions(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Bank_DefinitionsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Bank_Definition
            {
                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Bank_Definition> GetAllDC_Bank_Definitions()
        {
            string PROCEDURE = "p_SelectDC_Bank_DefinitionsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Bank_Definition
            {
                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public int ChangeStatusActive()
        {
            string PROCEDURE = "p_ActiveDC_Bank_Definition";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Active";
            parameters[1].Value = Active;

            parameters[2] = new SqlParameter();
            parameters[2].ParameterName = "@RowLastUpdatedTime";
            parameters[2].Value = this._rowlastupdatedtime;

            parameters[3] = new SqlParameter();
            parameters[3].ParameterName = "@RowLastUpdatedUser";
            parameters[3].Value = this._rowlastupdateduser;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
    }
}
