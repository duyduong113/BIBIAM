using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_CompanyDefinition.
    /// </summary>
    public class DC_CompanyDefinition
    {
        #region Members
        private string _companyname = String.Empty;
        public string CompanyName { get { return _companyname; } set { this._companyname = value; } }

        private string _taxcode = String.Empty;
        public string TaxCode { get { return _taxcode; } set { this._taxcode = value; } }

        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _fax = String.Empty;
        public string Fax { get { return _fax; } set { this._fax = value; } }

        private string _phone = String.Empty;
        public string Phone { get { return _phone; } set { this._phone = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }

        private string _decription = String.Empty;
        public string Decription { get { return _decription; } set { this._decription = value; } }

        private int _refid;
        public int RefID { get { return _refid; } set { this._refid = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private string _website = String.Empty;
        public string Website { get { return _website; } set { this._website = value; } }

        private int _codeto;
        public int CodeTo { get { return _codeto; } set { this._codeto = value; } }

        private int _codefrom;
        public int CodeFrom { get { return _codefrom; } set { this._codefrom = value; } }

        private string _code = String.Empty;
        public string Code { get { return _code; } set { this._code = value; } }

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


        public string Country { get; set; }

        public string Currency { get; set; }

        #endregion

        public DC_CompanyDefinition()
        { }

        public DC_CompanyDefinition(string CompanyName, string TaxCode, string Email, string Fax, string Phone, string Address, string Decription, int RefID, bool Active, string Website, int CodeTo, int CodeFrom, string Code, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._companyname = CompanyName;
            this._taxcode = TaxCode;
            this._email = Email;
            this._fax = Fax;
            this._phone = Phone;
            this._address = Address;
            this._decription = Decription;
            this._refid = RefID;
            this._active = Active;
            this._website = Website;
            this._codeto = CodeTo;
            this._codefrom = CodeFrom;
            this._code = Code;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_CompanyDefinition";
            SqlParameter[] parameters = new SqlParameter[20];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyName";
            parameters[i].Value = this._companyname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TaxCode";
            parameters[i].Value = this._taxcode;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Decription";
            parameters[i].Value = this._decription;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RefID";
            parameters[i].Value = this._refid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Website";
            parameters[i].Value = this._website;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeTo";
            parameters[i].Value = this._codeto;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeFrom";
            parameters[i].Value = this._codefrom;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Code";
            parameters[i].Value = this._code;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Country";
            parameters[i].Value = this.Country;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Currency";
            parameters[i].Value = this.Currency;
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
            string PROCEDURE = "p_UpdateDC_CompanyDefinition";
            SqlParameter[] parameters = new SqlParameter[20];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyName";
            parameters[i].Value = this._companyname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TaxCode";
            parameters[i].Value = this._taxcode;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Decription";
            parameters[i].Value = this._decription;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RefID";
            parameters[i].Value = this._refid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Website";
            parameters[i].Value = this._website;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeTo";
            parameters[i].Value = this._codeto;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeFrom";
            parameters[i].Value = this._codefrom;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Code";
            parameters[i].Value = this._code;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Country";
            parameters[i].Value = this.Country;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Currency";
            parameters[i].Value = this.Currency;
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
            string PROCEDURE = "p_DeleteDC_CompanyDefinition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RowID";
            parameters[0].Value = RowID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_CompanyDefinition GetDC_CompanyDefinition(int RowID)
        {
            string PROCEDURE = "p_SelectDC_CompanyDefinition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RowID";
            parameters[0].Value = RowID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_CompanyDefinition
            {
                CompanyName = row["CompanyName"].ToString(),
                TaxCode = row["TaxCode"].ToString(),
                Email = row["Email"].ToString(),
                Fax = row["Fax"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Decription = row["Decription"].ToString(),
                RefID = Convert.ToInt32(row["RefID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                Website = row["Website"].ToString(),
                CodeTo = Convert.ToInt32(row["CodeTo"].ToString()),
                CodeFrom = Convert.ToInt32(row["CodeFrom"].ToString()),
                Code = row["Code"].ToString(),
                Country = row["Country"].ToString(),
                Currency = row["Currency"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_CompanyDefinition> GetDC_CompanyDefinitions(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_CompanyDefinitionsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_CompanyDefinition
            {
                CompanyName = row["CompanyName"].ToString(),
                TaxCode = row["TaxCode"].ToString(),
                Email = row["Email"].ToString(),
                Fax = row["Fax"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Decription = row["Decription"].ToString(),
                RefID = Convert.ToInt32(row["RefID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                Website = row["Website"].ToString(),
                CodeTo = Convert.ToInt32(row["CodeTo"].ToString()),
                CodeFrom = Convert.ToInt32(row["CodeFrom"].ToString()),
                Code = row["Code"].ToString(),
                Country = row["Country"].ToString(),
                Currency = row["Currency"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_CompanyDefinition> GetAllDC_CompanyDefinitions()
        {
            string PROCEDURE = "p_SelectDC_CompanyDefinitionsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CompanyDefinition
            {
                CompanyName = row["CompanyName"].ToString(),
                TaxCode = row["TaxCode"].ToString(),
                Email = row["Email"].ToString(),
                Fax = row["Fax"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Decription = row["Decription"].ToString(),
                RefID = Convert.ToInt32(row["RefID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                Website = row["Website"].ToString(),
                CodeTo = Convert.ToInt32(row["CodeTo"].ToString()),
                CodeFrom = Convert.ToInt32(row["CodeFrom"].ToString()),
                Code = row["Code"].ToString(),
                Country = row["Country"].ToString(),
                Currency = row["Currency"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}
