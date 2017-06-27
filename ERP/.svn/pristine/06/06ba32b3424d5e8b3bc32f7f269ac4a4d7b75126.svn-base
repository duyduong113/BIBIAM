using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
namespace ERPAPD.Models
{
    public class DC_Customer_Creation_Request
    {
        #region Members
        private string _organizationEmployee = String.Empty;
        public string OrganizationEmployee { get { return _organizationEmployee; } set { this._organizationEmployee = value; } }

        private string _organizationid = String.Empty;
        public string OrganizationID { get { return _organizationid; } set { this._organizationid = value; } }

        private string _employeeid = String.Empty;
        public string EmployeeID { get { return _employeeid; } set { this._employeeid = value; } }

        [Required]
        private string _fullname = String.Empty;
        [Required]
        public string FullName { get { return _fullname; } set { this._fullname = value; } }

        [Required]
        private string _indentitynumber = String.Empty;
        [Required]
        public string IndentityNumber { get { return _indentitynumber; } set { this._indentitynumber = value; } }
        [Required]
        private string _phone = String.Empty;
        [Required]
        public string Phone { get { return _phone; } set { this._phone = value; } }

        private bool _opsCreated;
        public bool OPSCreated { get { return _opsCreated; } set { this._opsCreated = value; } }

        private int _status;
        public int Status { get { return _status; } set { this._status = value; } }

        private bool _exported;
        public bool Exported { get { return _exported; } set { this._exported = value; } }

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
        [Required]
        private double _salary;
        [Required]
        public double Salary { get { return _salary; } set { this._salary = value; } }

        private double _salaryperson;
        public double SalaryPerson { get { return _salaryperson; } set { this._salaryperson = value; } }

        private string _title = String.Empty;
        public string Title { get { return _title; } set { this._title = value; } }

        private string _sex = String.Empty;
        public string Sex { get { return _sex; } set { this._sex = value; } }

        private string _department = String.Empty;
        public string Department { get { return _department; } set { this._department = value; } }

        private string _contracttype = String.Empty;
        public string ContractType { get { return _contracttype; } set { this._contracttype = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }
        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _importnote = String.Empty;
        public string ImportNote { get { return _importnote; } set { this._importnote = value; } }

        private string _deniedreason = String.Empty;
        public string DeniedReason { get { return _deniedreason; } set { this._deniedreason = value; } }

        private string _DC_id = String.Empty;
        public string DC_ID { get { return _DC_id; } set { this._DC_id = value; } }

        private string _mcaemployeeid;
        public string DC_EmployeeID { get { return _mcaemployeeid; } set { this._mcaemployeeid = value; } }

        private string _orgemployeeid;
        public string OrgEmployeeID { get { return _orgemployeeid; } set { this._orgemployeeid = value; } }

        private DateTime _contractexp;
        public DateTime ContractExp { get { return _contractexp; } set { this._contractexp = value; } }


        private DateTime _birthday;
        public DateTime Birthday { get { return _birthday; } set { this._birthday = value; } }

        private DateTime _potentialBirthday;
        public DateTime PotentialBirthday { get { return _potentialBirthday; } set { this._potentialBirthday = value; } }

        private DateTime _startworkdate;
        public DateTime StartWorkDate { get { return _startworkdate; } set { this._startworkdate = value; } }

        private string _creditlitmitpolicy;
        public string CreditLimitPolicy { get { return _creditlitmitpolicy; } set { this._creditlitmitpolicy = value; } }

        private DateTime _orgstartworkdate;
        public DateTime OrgStartWorkDate { get { return _orgstartworkdate; } set { this._orgstartworkdate = value; } }

        private bool _blacklist = false;
        public bool Blacklist { get { return _blacklist; } set { this._blacklist = value; } }
        private string _note;
        public string Note { get { return _note; } set { this._note = value; } }

        private string _potentialnote;
        public string PotentialNote { get { return _potentialnote; } set { this._potentialnote = value; } }

        private string _statuscolor;
        public string StatusColor { get { return _statuscolor; } set { this._statuscolor = value; } }

        private string _blacklistcolor;
        public string BlacklistColor { get { return _blacklistcolor; } set { this._blacklistcolor = value; } }

        public string _baddebt;
        public string BadDebt { get { return _baddebt; } set { this._baddebt = value; } }

        public string _potentialPosition;
        public string PotentialPosition { get { return _potentialPosition; } set { this._potentialPosition = value; } }

        public string _potentialPhone;
        public string PotentialPhone { get { return _potentialPhone; } set { this._potentialPhone = value; } }

        public string _potentialIndentityNumber;
        public string PotentialIndentityNumber { get { return _potentialIndentityNumber; } set { this._potentialIndentityNumber = value; } }

        private bool _existPotential = false;
        public bool ExistPotential { get { return _existPotential; } set { this._existPotential = value; } }

        public string _existPotentialColor;
        public string ExistPotentialColor { get { return _existPotentialColor; } set { this._existPotentialColor = value; } }

        private bool _linkIndentityNumber = false;
        public bool LinkIndentityNumber { get { return _linkIndentityNumber; } set { this._linkIndentityNumber = value; } }

        public string _linkIndentityNumberColor;
        public string LinkIndentityNumberColor { get { return _linkIndentityNumberColor; } set { this._linkIndentityNumberColor = value; } }

        private int _totalrows;
        public int TotalRows { get { return _totalrows; } set { this._totalrows = value; } }


        public string Company { get; set; }
        #endregion

        public DC_Customer_Creation_Request()
        { }

        public DC_Customer_Creation_Request(string OrganizationID, string EmployeeID, string FullName, string IndentityNumber, string Phone, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser, bool OPSCreated, bool Exported, int Status)
        {
            this._organizationid = OrganizationID;
            this._employeeid = EmployeeID;
            this._fullname = FullName;
            this._indentitynumber = IndentityNumber;
            this._phone = Phone;
            this._active = Active;
            this._status = Status;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._exported = Exported;
            this._opsCreated = OPSCreated;
        }

        // Create
        public DC_Customer_Creation_Request(string OrganizationID, string EmployeeID, string FullName, string IndentityNumber, string Phone, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, bool OPSCreated, bool Exported, int Status)
        {
            this._organizationid = OrganizationID;
            this._employeeid = EmployeeID;
            this._fullname = FullName;
            this._indentitynumber = IndentityNumber;
            this._phone = Phone;
            this._active = Active;
            this._status = Status;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;

            this._exported = Exported;
            this._opsCreated = OPSCreated;
        }
        public int Save()
        {
            string PROCEDURE = "p_InsertUpdateDC_Customer_Creation_Request";
            SqlParameter[] parameters = new SqlParameter[14];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = this._indentitynumber != null ? this.IndentityNumber : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone != null ? this._phone : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Exported";
            parameters[i].Value = this._exported;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPSCreated";
            parameters[i].Value = this._opsCreated;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
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
            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }

        public int Add()
        {
            string PROCEDURE = "p_SaveDC_Customer_Creation_Request";
            SqlParameter[] parameters = new SqlParameter[14];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = this._indentitynumber != null ? this.IndentityNumber : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone != null ? this._phone : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Exported";
            parameters[i].Value = this._exported;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPSCreated";
            parameters[i].Value = this._opsCreated;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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
            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }

        public int AddCreate()
        {
            string PROCEDURE = "p_SaveDC_Customer_Creation_Request_Create";
            SqlParameter[] parameters = new SqlParameter[12];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = this._indentitynumber != null ? this.IndentityNumber : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone != null ? this._phone : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Exported";
            parameters[i].Value = this._exported;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPSCreated";
            parameters[i].Value = this._opsCreated;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }
        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Customer_Creation_Request";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = this._indentitynumber != null ? this.IndentityNumber.Trim() : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone != null ? this._phone : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Exported";
            parameters[i].Value = this._exported;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPSCreated";
            parameters[i].Value = this._opsCreated;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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
            Helpers.RemoveCache.CacheCCR();
            return result;
        }

        public int UpdateCreate()
        {
            string PROCEDURE = "p_UpdateDC_Customer_Creation_Request_Create";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = this._indentitynumber != null ? this.IndentityNumber : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone != null ? this._phone : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Exported";
            parameters[i].Value = this._exported;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPSCreated";
            parameters[i].Value = this._opsCreated;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }

        public int UpdateManage()
        {
            string PROCEDURE = "p_UpdateDC_Customer_Creation_Request_ForDenyApprove";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = this._indentitynumber != null ? this.IndentityNumber.Trim() : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone != null ? this._phone : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Exported";
            parameters[i].Value = this._exported;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPSCreated";
            parameters[i].Value = this._opsCreated;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Customer_Creation_Request";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EmployeeID";
            parameters[1].Value = EmployeeID;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }

        public static DC_Customer_Creation_Request GetDC_Customer_Creation_Request(string OrganizationID, string EmployeeID)
        {
            string PROCEDURE = "p_Select_DC_Customer_Creation_Request";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EmployeeID";
            parameters[1].Value = EmployeeID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Customer_Creation_Request> GetDC_Customer_Creation_Requests(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Customer_Creation_RequestsDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {

                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Status = int.Parse(row["Status"].ToString())
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                //RowID = Convert.ToInt32(row["RowID"].ToString()),
                //RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //RowCreatedUser = row["RowCreatedUser"].ToString(),
                //RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                //RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> GetAllDC_Customer_Creation_Requests()
        {
            string PROCEDURE = "p_Select_DC_Customer_Creation_RequestAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                p_OrgEmployeeID = row["Company"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Exported = Convert.ToBoolean(row["Exported"].ToString()),
                OPSCreated = Convert.ToBoolean(row["OPSCreated"].ToString()),
                DC_ID = row["DC_ID"].ToString(),
                DeniedReason = row["DeniedReason"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString(),
                Status = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? 2 : int.Parse(row["Status"].ToString()),
                Salary = Double.Parse(row["Salary"].ToString()),
                SalaryPerson = Double.Parse(row["SalaryPerson"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Note = row["Note"].ToString(),
                Blacklist = bool.Parse(row["Blacklist"].ToString()),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(Convert.ToDateTime(row["Birthday"].ToString()).ToShortDateString()),
                PotentialBirthday = Convert.ToDateTime(row["PotentialBirthday"].ToString()),
                PotentialPosition = row["PotentialPosition"].ToString(),
                PotentialPhone = row["PotentialPhone"].ToString(),
                PotentialIndentityNumber = row["PotentialIndentityNumber"].ToString(),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CreditLimitPolicy = row["CreditLimitPolicy"].ToString(),
                PotentialNote = row["PotentialNote"].ToString(),
                OrgStartWorkDate = Convert.ToDateTime(row["OrgStartWorkDate"].ToString()),
                BlacklistColor = row["BlacklistColor"].ToString(),
                StatusColor = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? "background-color:#3E9BCE;color:#FFF;" : row["StatusColor"].ToString()
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> CheckOrganizationDW_MCACustomer(string o)
        {
            string PROCEDURE = " select CustomerID from DW_DC_Customer  where CustomerID = '" + o.Trim() + "'";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["CustomerID"].ToString(),
            }).ToList();
        }

        public int ChangeStatus()
        {
            string PROCEDURE = "p_ChangeStatusDC_Customer_Creation_Request";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = this._employeeid;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCCR();
            return result;
        }
        public static List<DC_Customer_Creation_Request> CheckCustomerInDC_Potential_Customer(string org, string empid)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@orgid";
            parameters[0].Value = org;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@empid";
            parameters[1].Value = empid;

            string PROCEDURE = "p_CheckUserIn_DC_Potential_Customer";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                //OrgEmployeeID = row["OrgEmployeeID"].ToString(),
            }).ToList();
        }
        public static List<DC_Customer_Creation_Request> CheckIndentity_DC_Potential_Customer(string o, string e, string i)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@indentity";
            parameters[0].Value = i;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@orgid";
            parameters[1].Value = o;

            parameters[2] = new SqlParameter();
            parameters[2].ParameterName = "@empid";
            parameters[2].Value = e;



            string PROCEDURE = "p_CheckIndentity_DC_Potential_Customer";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                IndentityNumber = row["IndentityNumber"].ToString(),
            }).ToList();
        }
        //public static List<DC_Customer_Creation_Request> GetAllDC_Customer_Creation_RequestsForExport(string from)
        //{
        //    SqlParameter[] parameters = new SqlParameter[1];
        //    parameters[0] = new SqlParameter();
        //    parameters[0].ParameterName = "@from";
        //    parameters[0].Value = from;
        //    string PROCEDURE = "p_Select_DC_Customer_Creation_RequestOrderCase";

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
        //    {
        //        OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
        //        OrganizationID = row["OrganizationID"].ToString(),
        //        EmployeeID = row["EmployeeID"].ToString(),
        //        FullName = row["FullName"].ToString(),
        //        IndentityNumber = row["IndentityNumber"].ToString(),
        //        Phone = row["Phone"].ToString(),
        //        Active = Convert.ToBoolean(row["Active"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
        //        Exported = Convert.ToBoolean(row["Exported"].ToString()),
        //        OPSCreated = Convert.ToBoolean(row["OPSCreated"].ToString()),
        //        DC_ID = row["DC_ID"].ToString(),
        //        DeniedReason = row["DeniedReason"].ToString(),
        //        DC_EmployeeID = row["DC_EmployeeID"].ToString(),
        //        OrgEmployeeID = row["OrgEmployeeID"].ToString(),
        //        Status = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? 2 : int.Parse(row["Status"].ToString()),
        //        Salary = Double.Parse(row["Salary"].ToString()),
        //        SalaryPerson = Double.Parse(row["SalaryPerson"].ToString()),
        //        Title = row["Title"].ToString(),
        //        Sex = row["Sex"].ToString(),
        //        Note = row["Note"].ToString(),
        //        Blacklist = bool.Parse(row["Blacklist"].ToString()),
        //        Department = row["Department"].ToString(),
        //        ContractType = row["ContractType"].ToString(),
        //        Address = row["Address"].ToString(),
        //        Email = row["Email"].ToString(),
        //        ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
        //        Birthday = DateTime.Parse(Convert.ToDateTime(row["Birthday"].ToString()).ToShortDateString()),
        //        PotentialBirthday = Convert.ToDateTime(row["PotentialBirthday"].ToString()),
        //        PotentialPosition = row["PotentialPosition"].ToString(),
        //        PotentialPhone = row["PotentialPhone"].ToString(),
        //        PotentialIndentityNumber = row["PotentialIndentityNumber"].ToString(),
        //        StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
        //        CreditLimitPolicy = row["CreditLimitPolicy"].ToString(),
        //        PotentialNote = row["PotentialNote"].ToString(),
        //        OrgStartWorkDate = Convert.ToDateTime(row["OrgStartWorkDate"].ToString()),
        //        BlacklistColor = row["BlacklistColor"].ToString(),
        //        StatusColor = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? "background-color:#3E9BCE;color:#FFF;" : row["StatusColor"].ToString(),
        //        ExistPotential = Boolean.Parse(row["ExistPotential"].ToString()),
        //        ExistPotentialColor = row["ExistPotentialColor"].ToString(),
        //        LinkIndentityNumber = Boolean.Parse(row["LinkIndentityNumber"].ToString()),
        //        LinkIndentityNumberColor = row["LinkIndentityNumberColor"].ToString(),

        //        // NHNAM :
        //        p_EmployeeID = row["P-EmployeeID"].ToString(),
        //        p_OrgEmployeeID = row["P-OrgEmployeeID"].ToString(),
        //        p_FullName = row["P-FullName"].ToString(),
        //        p_IndentityNumber = row["P-Indentity#"].ToString(),
        //        p_Birthday = Convert.ToDateTime(row["P-Birthday"].ToString()),
        //        p_Phone = row["P-Phone"].ToString(),
        //        p_Email = row["P-Email"].ToString(),
        //        p_Address = row["P-Address"].ToString(),
        //        p_ContractType = row["P-ContractType"].ToString(),
        //        p_ContractExp = row["P-ContractExp"].ToString(),
        //        p_Department = row["P-Department"].ToString(),
        //        p_Position = row["P-Position"].ToString(),
        //        p_StartWorkDate = Convert.ToDateTime(row["P-OrgStartWorkDate"].ToString()),
        //        p_Title = row["P-Title"].ToString(),
        //        p_Note = row["P-Note"].ToString(),
        //        p_Salary = Convert.ToDouble(row["SalaryPerson"].ToString()),
        //        p_Sex = row["P-Sex"].ToString(),
        //        Check = row["Check"].ToString(),
        //        CheckColor = row["CheckColor"].ToString(),
        //        MBVRule = row["MBVRule"].ToString(),
        //        BD = row["BD"].ToString(),

        //        //DungNT: Add Check
        //        CheckBirthDay = row["CheckBirthDay"].ToString(),
        //        CheckSalary = row["CheckSalary"].ToString(),
        //        CheckFullName = row["CheckFullName"].ToString(),
        //        CheckSex = row["CheckSex"].ToString(),
        //        CheckTitle = row["CheckTitle"].ToString(),
        //        CheckDepartment = row["CheckDepartment"].ToString(),

        //        CheckContractExp = row["CheckContractExp"].ToString(),
        //        CheckContractType = row["CheckContractType"].ToString(),
        //        CheckEmployeeID = row["CheckEmployeeID"].ToString(),
        //        CheckOrgEmployeeID = row["CheckOrgEmployeeID"].ToString(),
        //        CheckPhone = row["CheckPhone"].ToString(),
        //        CheckEmail = row["CheckEmail"].ToString(),
        //        CheckAddress = row["CheckAddress"].ToString(),
        //        CheckPosition = row["CheckPosition"].ToString(),
        //        CheckOrgStartWorkDate = row["CheckOrgStartWorkDate"].ToString()
        //    }).ToList();
        //}

        public static async Task<List<DC_Customer_Creation_Request>> GetAllDC_Customer_Creation_RequestsForExport(string WhereCondition, string from)
        {
            string PROCEDURE = "p_GetAll_DC_Customer_Creation_RequestDynamicExport";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@from";
            parameters[i].Value = from;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Exported = Convert.ToBoolean(row["Exported"].ToString()),
                OPSCreated = Convert.ToBoolean(row["OPSCreated"].ToString()),
                DC_ID = row["DC_ID"].ToString(),
                DeniedReason = row["DeniedReason"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString(),
                Status = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? 2 : int.Parse(row["Status"].ToString()),
                Salary = Double.Parse(row["Salary"].ToString()),
                SalaryPerson = Double.Parse(row["SalaryPerson"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Note = row["Note"].ToString(),
                Blacklist = bool.Parse(row["Blacklist"].ToString()),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = DateTime.Parse(Convert.ToDateTime(row["Birthday"].ToString()).ToShortDateString()),
                PotentialBirthday = Convert.ToDateTime(row["PotentialBirthday"].ToString()),
                PotentialPosition = row["PotentialPosition"].ToString(),
                PotentialPhone = row["PotentialPhone"].ToString(),
                PotentialIndentityNumber = row["PotentialIndentityNumber"].ToString(),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CreditLimitPolicy = row["CreditLimitPolicy"].ToString(),
                PotentialNote = row["PotentialNote"].ToString(),
                OrgStartWorkDate = Convert.ToDateTime(row["OrgStartWorkDate"].ToString()),
                BlacklistColor = row["BlacklistColor"].ToString(),
                StatusColor = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? "background-color:#3E9BCE;color:#FFF;" : row["StatusColor"].ToString(),
                ExistPotential = Boolean.Parse(row["ExistPotential"].ToString()),
                ExistPotentialColor = row["ExistPotentialColor"].ToString(),
                LinkIndentityNumber = Boolean.Parse(row["LinkIndentityNumber"].ToString()),
                LinkIndentityNumberColor = row["LinkIndentityNumberColor"].ToString(),
                //vuongnd add
                SCreditLimit = Double.Parse(row["SCredit"].ToString()),
                SDueLimit = Double.Parse(row["SDueLimit"].ToString()),
                //----------
                // NHNAM :
                p_EmployeeID = row["P-EmployeeID"].ToString(),
                p_OrgEmployeeID = row["P-OrgEmployeeID"].ToString(),
                p_FullName = row["P-FullName"].ToString(),
                p_IndentityNumber = row["P-Indentity#"].ToString(),
                p_Birthday = Convert.ToDateTime(row["P-Birthday"].ToString()),
                p_Phone = row["P-Phone"].ToString(),
                p_Email = row["P-Email"].ToString(),
                p_Address = row["P-Address"].ToString(),
                p_ContractType = row["P-ContractType"].ToString(),
                p_ContractExp = row["P-ContractExp"].ToString(),
                p_Department = row["P-Department"].ToString(),
                p_Position = row["P-Position"].ToString(),
                p_StartWorkDate = Convert.ToDateTime(row["P-OrgStartWorkDate"].ToString()),
                p_Title = row["P-Title"].ToString(),
                p_Note = row["P-Note"].ToString(),
                p_Salary = Convert.ToDouble(row["SalaryPerson"].ToString()),
                p_Sex = row["P-Sex"].ToString(),
                Check = row["Check"].ToString(),
                CheckColor = row["CheckColor"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                BD = row["BD"].ToString(),
                //DungNT: Add Check
                CheckBirthDay = row["CheckBirthDay"].ToString(),
                CheckSalary = row["CheckSalary"].ToString(),
                CheckFullName = row["CheckFullName"].ToString(),
                CheckSex = row["CheckSex"].ToString(),
                CheckTitle = row["CheckTitle"].ToString(),
                CheckDepartment = row["CheckDepartment"].ToString(),
                CheckContractExp = row["CheckContractExp"].ToString(),
                CheckContractType = row["CheckContractType"].ToString(),
                CheckEmployeeID = row["CheckEmployeeID"].ToString(),
                CheckOrgEmployeeID = row["CheckOrgEmployeeID"].ToString(),
                CheckPhone = row["CheckPhone"].ToString(),
                CheckEmail = row["CheckEmail"].ToString(),
                CheckAddress = row["CheckAddress"].ToString(),
                CheckPosition = row["CheckPosition"].ToString(),
                CheckOrgStartWorkDate = row["CheckOrgStartWorkDate"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
            }).ToList();
        }

        public double SCreditLimit { get; set; }
        public double SDueLimit { get; set; }

        //DungNT: Get Check in
        public string CheckBirthDay { get; set; }
        public string CheckSalary { get; set; }
        public string CheckFullName { get; set; }
        public string CheckSex { get; set; }
        public string CheckTitle { get; set; }
        public string CheckDepartment { get; set; }
        public string CheckContractExp { get; set; }
        public string CheckContractType { get; set; }
        public string CheckEmployeeID { get; set; }
        public string CheckOrgEmployeeID { get; set; }
        public string CheckPhone { get; set; }
        public string CheckEmail { get; set; }
        public string CheckAddress { get; set; }
        public string CheckPosition { get; set; }
        public string CheckOrgStartWorkDate { get; set; }


        public static List<DC_Customer_Creation_Request> GetAllDC_Customer_Creation_Requests(string from)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@from";
            parameters[0].Value = from;
            string PROCEDURE = "p_GetAll_DC_Customer_Creation_Request";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Exported = Convert.ToBoolean(row["Exported"].ToString()),
                OPSCreated = Convert.ToBoolean(row["OPSCreated"].ToString()),
                DC_ID = row["DC_ID"].ToString(),
                DeniedReason = row["DeniedReason"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString(),
                Status = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? 2 : int.Parse(row["Status"].ToString()),
                Salary = Double.Parse(row["Salary"].ToString()),
                SalaryPerson = Double.Parse(row["SalaryPerson"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Note = row["Note"].ToString(),
                Blacklist = bool.Parse(row["Blacklist"].ToString()),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = DateTime.Parse(Convert.ToDateTime(row["Birthday"].ToString()).ToShortDateString()),
                PotentialBirthday = Convert.ToDateTime(row["PotentialBirthday"].ToString()),
                PotentialPosition = row["PotentialPosition"].ToString(),
                PotentialPhone = row["PotentialPhone"].ToString(),
                PotentialIndentityNumber = row["PotentialIndentityNumber"].ToString(),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CreditLimitPolicy = row["CreditLimitPolicy"].ToString(),
                PotentialNote = row["PotentialNote"].ToString(),
                OrgStartWorkDate = Convert.ToDateTime(row["OrgStartWorkDate"].ToString()),
                BlacklistColor = row["BlacklistColor"].ToString(),
                StatusColor = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? "background-color:#3E9BCE;color:#FFF;" : row["StatusColor"].ToString(),
                ExistPotential = Boolean.Parse(row["ExistPotential"].ToString()),
                ExistPotentialColor = row["ExistPotentialColor"].ToString(),
                LinkIndentityNumber = Boolean.Parse(row["LinkIndentityNumber"].ToString()),
                LinkIndentityNumberColor = row["LinkIndentityNumberColor"].ToString(),
                Check = row["Check"].ToString(),
                CheckColor = row["CheckColor"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                BD = row["BD"].ToString(),
                TotalCus = row["Customer#"].ToString()

            }).ToList();
        }


        public static async Task<List<DC_Customer_Creation_Request>> GetAllDC_Customer_Creation_Requests(string WhereCondition, string from)
        {
            string PROCEDURE = "p_GetAll_DC_Customer_Creation_RequestDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@from";
            parameters[i].Value = from;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Note = row["Note"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = DateTime.Parse(Convert.ToDateTime(row["Birthday"].ToString()).ToShortDateString()),
                StartWorkDate = DateTime.Parse(Convert.ToDateTime(row["StartWorkDate"].ToString()).ToShortDateString()),
                PotentialPosition = row["PotentialPosition"].ToString(),
                PotentialNote = row["PotentialNote"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                BD = row["BD"].ToString(),
                SCreditLimit = Double.Parse(row["SCredit"].ToString()),
                SDueLimit = Double.Parse(row["SDueLimit"].ToString()),
                CreditLimitPolicy = row["CreditLimitPolicy"].ToString(),
            }).ToList();
        }
        public static async Task<DataSourceResult> GetAllDC_Customer_Creation_RequestsDS(string WhereCondition, [DataSourceRequest] DataSourceRequest request, string from)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_GetAll_DC_Customer_Creation_RequestDynamicDS";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@from";
            parameters[i].Value = from;

            var data = new DataSourceResult();

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            data.Data = ds.Tables[0].AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Exported = Convert.ToBoolean(row["Exported"].ToString()),
                OPSCreated = Convert.ToBoolean(row["OPSCreated"].ToString()),
                DC_ID = row["DC_ID"].ToString(),
                DeniedReason = row["DeniedReason"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString(),
                Status = Int32.Parse(row["Status"].ToString()),
                Salary = Double.Parse(row["Salary"].ToString()),
                SalaryPerson = Double.Parse(row["SalaryPerson"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Note = row["Note"].ToString(),
                Blacklist = bool.Parse(row["Blacklist"].ToString()),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = DateTime.Parse(Convert.ToDateTime(row["Birthday"].ToString()).ToShortDateString()),
                PotentialBirthday = Convert.ToDateTime(row["PotentialBirthday"].ToString()),
                PotentialPosition = row["PotentialPosition"].ToString(),
                PotentialPhone = row["PotentialPhone"].ToString(),
                PotentialIndentityNumber = row["PotentialIndentityNumber"].ToString(),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CreditLimitPolicy = row["CreditLimitPolicy"].ToString(),
                PotentialNote = row["PotentialNote"].ToString(),
                OrgStartWorkDate = Convert.ToDateTime(row["OrgStartWorkDate"].ToString()),
                BlacklistColor = row["BlacklistColor"].ToString(),
                StatusColor = !string.IsNullOrWhiteSpace(row["DC_ID"].ToString()) ? "background-color:#3E9BCE;color:#FFF;" : row["StatusColor"].ToString(),
                ExistPotential = Boolean.Parse(row["ExistPotential"].ToString()),
                ExistPotentialColor = row["ExistPotentialColor"].ToString(),
                LinkIndentityNumber = Boolean.Parse(row["LinkIndentityNumber"].ToString()),
                LinkIndentityNumberColor = row["LinkIndentityNumberColor"].ToString(),
                Check = row["Check"].ToString(),
                CheckColor = row["CheckColor"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                BD = row["BD"].ToString(),

                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
                EmployeeTotal = row["Employee#"].ToString(),

            }).ToList();

            var total = ds.Tables[0].AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }

        public static List<DC_Customer_Creation_Request> Select_DC_Customer_Creation_Request_Check(string from)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@from";
            parameters[0].Value = from;
            string PROCEDURE = "p_Select_DC_Customer_Creation_Request_Check";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationEmployee = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                Check = row["Check"].ToString(),
                Department = row["Department"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> CheckOrgRule(string org)
        {
            string PROCEDURE = "select OrganizationID from DC_Organization_Meta where Factor ='OrgRule' AND OrganizationID = '" + org + "' AND Value  LIKE '%R003;%'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> CheckOrgRulePhysicalIDRequired(string org)
        {
            string PROCEDURE = "select OrganizationID from DC_Organization_Meta where Factor ='OrgRule' AND OrganizationID = '" + org + "' AND Value  LIKE '%R004;%'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> CheckOrgInArea(string UserID, string org)
        {
            string PROCEDURE = "SELECT TOP 1 * FROM DC_MOP_Area_HumanResource h LEFT JOIN DC_MOP_Area_Org o ON o.AreaID = h.AreaID WHERE h.UserID = '" + UserID + "' AND RoleID IN('UR008','UR027','UR026','UR007','UR013') AND o.OrganizationID = '" + org + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> CheckOrgAllInArea(string UserID, string org)
        {
            string PROCEDURE = "SELECT TOP 1 * FROM DC_MOP_Area_HumanResource h LEFT JOIN DC_MOP_Area_Org o ON o.AreaID = h.AreaID WHERE h.UserID = '" + UserID + "' AND RoleID IN('UR028','UR009') AND o.OrganizationID = '" + org + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public string p_EmployeeID { get; set; }
        public string p_OrgEmployeeID { get; set; }
        public string p_FullName { get; set; }
        public string p_IndentityNumber { get; set; }
        public DateTime p_Birthday { get; set; }
        public DateTime p_StartWorkDate { get; set; }
        public string p_Phone { get; set; }
        public string p_Email { get; set; }
        public string p_Address { get; set; }
        public string p_ContractExp { get; set; }
        public string p_ContractType { get; set; }
        public string p_Department { get; set; }
        public string p_Position { get; set; }
        public string p_Title { get; set; }
        public string p_Note { get; set; }

        public double p_Salary { get; set; }
        public string p_Sex { get; set; }

        public string Check { get; set; }

        public string CheckColor { get; set; }
        public string MBVRule { get; set; }
        public string BD { get; set; }

        public string TotalCus { get; set; }

        public string OrgStatus { get; set; }

        public string Approver { get; set; }

        public string EmployeeTotal { get; set; }

        public string Register { get; set; }

        public static List<DC_Customer_Creation_Request> CheckBlacklistInPotential_Customer(string org, string emp)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = org;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EmployeeID";
            parameters[1].Value = emp;


            string PROCEDURE = "p_CheckBlacklistInPotential_Customer";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                OrganizationID = row["OrganizationID"].ToString()
            }).ToList();
        }

        public static List<DC_Customer_Creation_Request> GetInfoCustomerForCSTool(string CustomerID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            string PROCEDURE = "p_DC_Customer_Creation_Request_SelectForCSTool";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Creation_Request
            {
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Salary = int.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Department = row["Department"].ToString(),
                StartWorkDate = DateTime.Parse(row["StartWorkDate"].ToString()),
                RowCreatedTime = DateTime.Parse(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = DateTime.Parse(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
            }).ToList();
        }
    }


}