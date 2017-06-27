using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Potential_Customer.
    /// </summary>
    public class DC_Potential_Customer
    {
        #region Members
        private string _organizationid = String.Empty;
        public string OrganizationID { get { return _organizationid; } set { this._organizationid = value; } }

        private string _employeeid = String.Empty;
        public string EmployeeID { get { return _employeeid; } set { this._employeeid = value; } }

        private string _fullname = String.Empty;
        public string FullName { get { return _fullname; } set { this._fullname = value; } }

        private string _indentitynumber = String.Empty;
        public string IndentityNumber { get { return _indentitynumber; } set { this._indentitynumber = value; } }

        private string _phone = String.Empty;
        public string Phone { get { return _phone; } set { this._phone = value; } }

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

        private double _salary;
        public double Salary { get { return _salary; } set { this._salary = value; } }


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

        private string _cifbank = String.Empty;
        public string CIFbank { get { return _cifbank; } set { this._cifbank = value; } }

        private bool _blacklist = false;
        public bool Blacklist { get { return _blacklist; } set { this._blacklist = value; } }

        private DateTime _contractexp;
        public DateTime ContractExp { get { return _contractexp; } set { this._contractexp = value; } }

        private string _note;
        public string Note { get { return _note; } set { this._note = value; } }

        private string _mcaid;
        public string DC_ID { get { return _mcaid; } set { this._mcaid = value; } }

        private string _mcaemployeeid;
        public string DC_EmployeeID { get { return _mcaemployeeid; } set { this._mcaemployeeid = value; } }

        private string _orgemployeeid;
        public string OrgEmployeeID { get { return _orgemployeeid; } set { this._orgemployeeid = value; } }

        private string _creditlimitpolicy;
        public string CreditLimitPolicy { get { return _creditlimitpolicy; } set { this._creditlimitpolicy = value; } }

        private DateTime _birthday;
        public DateTime Birthday { get { return _birthday; } set { this._birthday = value; } }

        private DateTime _startworkdate;
        public DateTime StartWorkDate { get { return _startworkdate; } set { this._startworkdate = value; } }

        private string _position;
        public string Position { get { return _position; } set { this._position = value; } }

        //vuongnd - add key
        private string _id;
        public string ID { get { return _id; } set { this._id = value; } }


        // nhnam - Add 2 column: DateOfIssuance & PlaceOfIssuance
        private DateTime _dateOfIsuance;
        public DateTime DateOfIssuance { get { return _dateOfIsuance; } set { this._dateOfIsuance = value; } }

        private string _placeOfIssuance;
        public string PlaceOfIssuance { get { return _placeOfIssuance; } set { this._placeOfIssuance = value; } }

        public string SalaryRange { get; set; }
        public string Seniority { get; set; }

        public double SCredit { get; set; }
        public double SDueLimit { get; set; }

        //vuongnd : add xmlstring
        private string _xmlstring = String.Empty;
        public string XMLString { get { return _xmlstring; } set { this._xmlstring = value; } }

        #endregion

        public DC_Potential_Customer()
        { }

        public DC_Potential_Customer(string OrganizationID, string EmployeeID, string FullName, string IndentityNumber, string Phone, string XMLString, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._organizationid = OrganizationID;
            this._employeeid = EmployeeID;
            this._fullname = FullName;
            this._indentitynumber = IndentityNumber;
            this._phone = Phone;
            this._xmlstring = XMLString;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Potential_Customer";
            SqlParameter[] parameters = new SqlParameter[9];
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
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = this._xmlstring != null ? this._xmlstring : "";
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
            string PROCEDURE = "p_UpdateDC_Potential_Customer";
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
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = this._xmlstring != null ? this._xmlstring : "";
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Potential_Customer";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EmployeeID";
            parameters[1].Value = EmployeeID;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        public int Delete2()
        {
            string PROCEDURE = "delete DC_Potential_Customer where OrganizationID + '-' + EmployeeID = @OrganizationID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);
        }
        public static DC_Potential_Customer GetSingleDC_Potential_Customer(string OrganizationID, string EmployeeID)
        {
            string text = @"SELECT OrganizationID,EmployeeID,RowCreatedTime,RowCreatedUser,IndentityNumber,FullName,
                            ISNULL(XMLString.value('(/Root/StartWorkDate/text())[1]', 'nvarchar(max)'),'1900-01-01') AS 'StartWorkDate'
			                FROM DC_Potential_Customer Where OrganizationID = @OrganizationID AND EmployeeID = @EmployeeID ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EmployeeID";
            parameters[1].Value = EmployeeID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                RowCreatedTime = DateTime.Parse(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
            }).ToList().FirstOrDefault();
        }

        public static DC_Potential_Customer GetDC_Potential_Customer(string OrganizationID, string EmployeeID)
        {
            string PROCEDURE = "p_Select_DC_Potential_Customer";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EmployeeID";
            parameters[1].Value = EmployeeID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
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
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                Blacklist = bool.Parse(row["Blacklist"].ToString()),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CIFbank = row["CIFbank"].ToString(),
                Note = row["Note"].ToString(),
                DC_ID = row["DC_ID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString() != "" ? row["OrgEmployeeID"].ToString() : row["EmployeeID"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Potential_Customer> GetDC_Potential_Customers(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Potential_CustomersDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
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
            }).ToList();
        }

        public static List<DC_Potential_Customer> GetAllDC_Potential_Customers()
        {
            string PROCEDURE = "p_Select_DC_Potential_CustomerAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                ID = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
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
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CIFbank = row["CIFbank"].ToString(),
                Note = row["Note"].ToString(),
                DC_ID = row["DC_ID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString() != "" ? row["OrgEmployeeID"].ToString() : row["EmployeeID"].ToString(),
                // nhnam - Add 2 column: DateOfIssuance & PlaceOfIssuance
                DateOfIssuance = Convert.ToDateTime(row["DateOfIssuance"].ToString()),
                PlaceOfIssuance = row["PlaceOfIssuance"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                Position = row["Position"].ToString(),
                SalaryRange = "Min : " + row["ClassMin"].ToString() + " - " + "Max: " + row["ClassMax"].ToString(),
                Seniority = row["Seniority"].ToString(),
                SCredit = double.Parse(row["SCredit"].ToString()),
                SDueLimit = double.Parse(row["SDueLimit"].ToString()),
                ClassAuto = row["ClassAuto"].ToString(),
                SeniorityAuto = row["SeniorityAuto"].ToString()
            }).ToList();
        }
        public static List<DC_Potential_Customer> GetAllDC_Potential_CustomersDynamic(string whereCondition)
        {
            string PROCEDURE = "p_Select_DC_Potential_CustomerDynamicAll";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                ID = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
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
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CIFbank = row["CIFbank"].ToString(),
                Note = row["Note"].ToString(),
                DC_ID = row["DC_ID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString() != "" ? row["OrgEmployeeID"].ToString() : row["EmployeeID"].ToString(),
                // nhnam - Add 2 column: DateOfIssuance & PlaceOfIssuance
                DateOfIssuance = Convert.ToDateTime(row["DateOfIssuance"].ToString()),
                PlaceOfIssuance = row["PlaceOfIssuance"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                Position = row["Position"].ToString(),
                SalaryRange = "Min : " + row["ClassMin"].ToString() + " - " + "Max: " + row["ClassMax"].ToString(),
                Seniority = row["Seniority"].ToString(),
                SCredit = double.Parse(row["SCredit"].ToString()),
                SDueLimit = double.Parse(row["SDueLimit"].ToString()),
                ClassName = row["ClassName"].ToString(),
                SSeniority = row["SSeniority"].ToString(),
                ClassAuto = row["ClassAuto"].ToString(),
                SeniorityAuto = row["SeniorityAuto"].ToString()
            }).ToList();
        }
        public string ClassName { get; set; }
        public string SSeniority { get; set; }

        public string ClassAuto { get; set; }
        public string SeniorityAuto { get; set; }
        public static List<DC_Potential_Customer> GetAllDC_Potential_CustomersByOrganizationID(string organizationID)
        {
            string PROCEDURE = "p_getPotential_CustomerAll";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = organizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                ID = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
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
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CIFbank = row["CIFbank"].ToString(),
                Note = row["Note"].ToString(),
                DC_ID = row["DC_ID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString() != "" ? row["OrgEmployeeID"].ToString() : row["EmployeeID"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                DateOfIssuance = Convert.ToDateTime(row["DateOfIssuance"].ToString()),
                PlaceOfIssuance = row["PlaceOfIssuance"].ToString(),
            }).ToList();
        }

        public static List<DC_Potential_Customer> GetSuggestDC_Potential_Customers(string OrganizationID, string EmployeeID, string Phone, string IndentityNumber, string FullName)
        {

            EmployeeID = string.IsNullOrEmpty(EmployeeID) ? "N'%%'" : "N'%" + EmployeeID + "%'";
            Phone = string.IsNullOrEmpty(Phone) ? "N'%%'" : "N'%" + Phone + "%'";
            IndentityNumber = string.IsNullOrEmpty(IndentityNumber) ? "N'%'" : "N'" + IndentityNumber + "%'";
            FullName = string.IsNullOrEmpty(FullName) ? "N'%%'" : "N'%" + FullName + "%'";

            string PROCEDURE = "EXEC [p_Select_DC_Potential_Customer_Suggest] @OrganizationID = '" + OrganizationID + "'," + " @Phone = " + Phone + ",@EmployeeID= " + EmployeeID + ",@FullName= " + FullName + ",@IndentityNumber= " + IndentityNumber;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString()
            }).ToList();
        }

        public static DC_Potential_Customer GetLoadSuggestDC_Potential_Customers(string OrganizationID, string EmployeeID, string Phone, string IndentityNumber, string FullName)
        {

            EmployeeID = string.IsNullOrEmpty(EmployeeID) ? "N'%%'" : "N'" + EmployeeID + "'";
            Phone = string.IsNullOrEmpty(Phone) ? "N'%%'" : "N'" + Phone + "'";
            IndentityNumber = string.IsNullOrEmpty(IndentityNumber) ? "N'%%'" : "N'" + IndentityNumber + "'";
            FullName = string.IsNullOrEmpty(FullName) ? "N'%%'" : "N'" + FullName + "'";

            string PROCEDURE = "EXEC [p_Select_DC_Potential_Customer_Load_Suggest] @OrganizationID = '" + OrganizationID + "'," +
                " @Phone = " + Phone + ",@EmployeeID= " + EmployeeID + ",@FullName= " + FullName + ",@IndentityNumber= " + IndentityNumber;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = string.IsNullOrEmpty(row["OrgEmployeeID"].ToString()) ? row["EmployeeID"].ToString() : row["OrgEmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Salary = double.Parse(row["Salary"].ToString()),
                Position = row["Position"].ToString(),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                Blacklist = bool.Parse(row["Blacklist"].ToString()),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CIFbank = row["CIFbank"].ToString(),
                Note = row["Note"].ToString(),
                DC_ID = row["DC_ID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString() != "" ? row["OrgEmployeeID"].ToString() : row["EmployeeID"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString()
            }).ToList().FirstOrDefault();
        }



        //vuongnd add xml
        public static async Task<List<DC_Potential_Customer>> GetDC_Potential_CustomersXML(string whereCondition, string OrganizationID)
        {
            string PROCEDURE = "p_Select_DC_Potential_CustomerXML";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = OrganizationID;
            i++;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Potential_Customer
            {
                ID = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
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
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                CIFbank = row["CIFbank"].ToString(),
                Note = row["Note"].ToString(),
                DC_ID = row["DC_ID"].ToString(),
                OrgEmployeeID = row["OrgEmployeeID"].ToString() != "" ? row["OrgEmployeeID"].ToString() : "",
                // nhnam - Add 2 column: DateOfIssuance & PlaceOfIssuance
                DateOfIssuance = Convert.ToDateTime(row["DateOfIssuance"].ToString()),
                PlaceOfIssuance = row["PlaceOfIssuance"].ToString(),
                DC_EmployeeID = row["DC_EmployeeID"].ToString(),
                Position = row["Position"].ToString(),
                SalaryRange = "Min : " + row["ClassMin"].ToString() + " - " + "Max: " + row["ClassMax"].ToString(),
                Seniority = row["Seniority"].ToString(),
                SCredit = double.Parse(row["SCredit"].ToString()),
                SDueLimit = double.Parse(row["SDueLimit"].ToString()),
                ClassName = row["ClassName"].ToString(),
                SSeniority = row["SSeniority"].ToString(),
                ClassAuto = row["ClassAuto"].ToString(),
                SeniorityAuto = row["SeniorityAuto"].ToString(),
                Blacklist = Boolean.Parse(row["Blacklist"].ToString())
            }).ToList();
        }


        //vuongnd add xml
        public static async Task<List<DC_Potential_Customer>> GetDC_Potential_Customer_Suggest(string whereCondition, string OrganizationID)
        {
            string PROCEDURE = "p_Select_DC_Potential_SuggestCreationRequest";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = OrganizationID;
            i++;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Potential_Customer
            {
                ID = row["OrganizationID"].ToString() + "-" + row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
               
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Sex = row["Sex"].ToString(),
                Department = row["Department"].ToString(),
                ContractType = row["ContractType"].ToString(),
                Address = row["Address"].ToString(),
                Email = row["Email"].ToString(),
                ContractExp = Convert.ToDateTime(row["ContractExp"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),

            }).ToList();
        }

        public static List<DC_Potential_Customer> GetDC_Potential_Customer_ForCSTool(string CustomerID)
        {
            string PROCEDURE = "p_DC_Potential_Customer_SelectForCSTool";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                IndentityNumber = row["IndentityNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Salary = double.Parse(row["Salary"].ToString()),
                Title = row["Title"].ToString(),
                Department = row["Department"].ToString(),
                Address = row["Address"].ToString(),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                StartWorkDate = Convert.ToDateTime(row["StartWorkDate"].ToString()),
                PlaceOfIssuance = row["PlaceOfIssuance"].ToString(),
                DateOfIssuance = Convert.ToDateTime(row["DateOfIssuance"].ToString()),
            }).ToList();
        }

        public static DC_Potential_Customer checkIndentity(string IndentityNumber, string OrganizationID, string EmployeeID)
        {
            string PROCEDURE = "select top 1 EmployeeID,OrganizationID from DC_Potential_Customer where IndentityNumber = @IndentityNumber and (organizationID <> @OrganizationID or EmployeeID <> @EmployeeID)";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IndentityNumber";
            parameters[i].Value = IndentityNumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = EmployeeID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                EmployeeID = row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static DC_Potential_Customer checkPhone(string Phone, string OrganizationID, string EmployeeID)
        {
            string PROCEDURE = "select top 1 EmployeeID,OrganizationID from DC_Potential_Customer where Phone = @Phone and organizationID = @OrganizationID and EmployeeID <> @EmployeeID";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = Phone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = EmployeeID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Potential_Customer
            {
                EmployeeID = row["EmployeeID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList().FirstOrDefault();
        }
    }
}
