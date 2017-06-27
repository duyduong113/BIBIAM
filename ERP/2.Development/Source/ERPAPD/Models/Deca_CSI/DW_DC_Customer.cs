using Kendo.Mvc.UI;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;

namespace ERPAPD.Models
{
    public class DW_DC_Customer
    {
        #region Members
        private string _customerid = String.Empty;
        public string CustomerID { get { return _customerid; } set { this._customerid = value; } }

        private string _familyname = String.Empty;

        public string FamilyName { get { return _familyname; } set { this._familyname = value; } }

        private string _givenname = String.Empty;

        public string GivenName { get { return _givenname; } set { this._givenname = value; } }

        private string _gender = String.Empty;
        public string Gender { get { return _gender; } set { this._gender = value; } }

        private string _phone = String.Empty;

        public string Phone { get { return _phone; } set { this._phone = value; } }

        private string _mobilephone = String.Empty;
        public string MobilePhone { get { return _mobilephone; } set { this._mobilephone = value; } }

        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _maritalstatus = String.Empty;
        public string MaritalStatus { get { return _maritalstatus; } set { this._maritalstatus = value; } }

        private string _position = String.Empty;
        public string Position { get { return _position; } set { this._position = value; } }

        private bool _organizationadmin;
        public bool OrganizationAdmin { get { return _organizationadmin; } set { this._organizationadmin = value; } }

        private DateTime _orgstartdate;

        public DateTime OrgStartDate { get { return _orgstartdate; } set { this._orgstartdate = value; } }

        private DateTime _orgenddate;

        public DateTime OrgEndDate { get { return _orgenddate; } set { this._orgenddate = value; } }

        private string _xaccountid = String.Empty;
        public string XAccountID { get { return _xaccountid; } set { this._xaccountid = value; } }

        private string _groupid = String.Empty;
        public string GroupID { get { return _groupid; } set { this._groupid = value; } }

        private DateTime _lastlogin;
        public DateTime LastLogin { get { return _lastlogin; } set { this._lastlogin = value; } }

        private string _status = String.Empty;
        public string Status { get { return _status; } set { this._status = value; } }

        private string _physicalid = String.Empty;
        public string PhysicalID { get { return _physicalid; } set { this._physicalid = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }

        private string _contract = String.Empty;
        public string Contract { get { return _contract; } set { this._contract = value; } }

        private double _creditlimit;
        public double CreditLimit { get { return _creditlimit; } set { this._creditlimit = value; } }

        private long _dwid;

        public long DWID { get { return _dwid; } set { this._dwid = value; } }

        private DateTime _createdat;
        public DateTime CreatedAt { get { return _createdat; } set { this._createdat = value; } }

        private DateTime _modifiedat;
        public DateTime ModifiedAt { get { return _modifiedat; } set { this._modifiedat = value; } }

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

        //partial
        private string _customerGroupID = String.Empty;
        public string CustomerGroupID { get { return _customerGroupID; } set { this._customerGroupID = value; } }

        private string _name = String.Empty;
        public string Name { get { return _name; } set { this._name = value; } }

        private string _employeeID = String.Empty;
        public string EmployeeID { get { return _employeeID; } set { this._employeeID = value; } }

        private double _dueLimit;
        public double DueLimit { get { return _dueLimit; } set { this._dueLimit = value; } }

        private string _organizationID = String.Empty;
        public string OrganizationID { get { return _organizationID; } set { this._organizationID = value; } }

        private string _actualStatus = String.Empty;
        public string ActualStatus { get { return _actualStatus; } set { this._actualStatus = value; } }

        private string _bD = String.Empty;
        public string BD { get { return _bD; } set { this._bD = value; } }

        private string _orgContractStatus = String.Empty;
        public string OrgContractStatus { get { return _orgContractStatus; } set { this._orgContractStatus = value; } }

        private double _orgEmployee;
        public double OrgEmployee { get { return _orgEmployee; } set { this._orgEmployee = value; } }

        private double _runningBalance;
        public double RunningBalance { get { return _runningBalance; } set { this._runningBalance = value; } }

        private double _settledBalance;
        public double SettledBalance { get { return _settledBalance; } set { this._settledBalance = value; } }

        private double _dueBalance;
        public double DueBalance { get { return _dueBalance; } set { this._dueBalance = value; } }

        private double _salary;
        public double Salary { get { return _salary; } set { this._salary = value; } }

        private DateTime _activatedAt;
        public DateTime ActivatedAt { get { return _activatedAt; } set { this._activatedAt = value; } }

        private DateTime _closedAt;
        public DateTime ClosedAt { get { return _closedAt; } set { this._closedAt = value; } }

        private DateTime _lastLoginDate;
        public DateTime LastLoginDate { get { return _lastLoginDate; } set { this._lastLoginDate = value; } }

        private bool _addedProfile;
        public bool AddedProfile { get { return _addedProfile; } set { this._addedProfile = value; } }

        private string _importNote = String.Empty;

        public string ImportNote { get { return _importNote; } set { this._importNote = value; } }

        private string _cashAdvanceLimit = String.Empty;
        public string CashAdvanceLimit { get { return _cashAdvanceLimit; } set { this._cashAdvanceLimit = value; } }

        private string _note = String.Empty;
        public string Note { get { return _note; } set { this._note = value; } }

        private DateTime _startWorkingDate;
        public DateTime StartWorkingDate { get { return _startWorkingDate; } set { this._startWorkingDate = value; } }

        private string _telesalesAgent = String.Empty;
        public string TelesalesAgent { get { return _telesalesAgent; } set { this._telesalesAgent = value; } }

        //telesales Agent last update
        private DateTime _lastUpdatedAt;

        public DateTime LastUpdatedAt { get { return _lastUpdatedAt; } set { this._lastUpdatedAt = value; } }

        private string _lastUpdatedBy = String.Empty;

        public string LastUpdatedBy { get { return _lastUpdatedBy; } set { this._lastUpdatedBy = value; } }

        //-----------

        private string _DCstatus = String.Empty;

        public string DCStatus { get { return _DCstatus; } set { this._DCstatus = value; } }

        private string _fullname = String.Empty;

        public string FullName { get { return _fullname; } set { this._fullname = value; } }

        //vuongnd
        private string _fakeCustomerID = String.Empty;

        public string FakeCustomerID { get { return _fakeCustomerID; } set { this._fakeCustomerID = value; } }

        //minhtc

        private int _ProfileCode;

        public int ProfileCode { get { return _ProfileCode; } set { this._ProfileCode = value; } }

        private string _Sex = String.Empty;

        public string Sex { get { return _Sex; } set { this._Sex = value; } }

        private string _NaneDC = String.Empty;

        public string NaneDC { get { return _NaneDC; } set { this._NaneDC = value; } }

        private string _DateOfBirth = String.Empty;

        public string DateOfBirth { get { return _DateOfBirth; } set { this._DateOfBirth = value; } }

        private string _PlaceOfBirth = String.Empty;

        public string PlaceOfBirth { get { return _PlaceOfBirth; } set { this._PlaceOfBirth = value; } }

        private string _Nationality = String.Empty;

        public string Nationality { get { return _Nationality; } set { this._Nationality = value; } }

        private string _Residence = String.Empty;

        public string Residence { get { return _Residence; } set { this._Residence = value; } }


        private string _AddressProvided = String.Empty;

        public string AddressProvided { get { return _AddressProvided; } set { this._AddressProvided = value; } }

        private string _DateProvided = String.Empty;

        public string DateProvided { get { return _DateProvided; } set { this._DateProvided = value; } }

        private string _EmailPerson = String.Empty;

        public string EmailPerson { get { return _EmailPerson; } set { this._EmailPerson = value; } }

        private string _EmailCompany = String.Empty;

        public string EmailCompany { get { return _EmailCompany; } set { this._EmailCompany = value; } }

        private string _ResidenceAddress = String.Empty;

        public string ResidenceAddress { get { return _ResidenceAddress; } set { this._ResidenceAddress = value; } }

        private string _ResidenceWard = String.Empty;

        public string ResidenceWard { get { return _ResidenceWard; } set { this._ResidenceWard = value; } }

        private string _ResidenceDistrict = String.Empty;

        public string ResidenceDistrict { get { return _ResidenceDistrict; } set { this._ResidenceDistrict = value; } }

        private string _ResidenceCity = String.Empty;

        public string ResidenceCity { get { return _ResidenceCity; } set { this._ResidenceCity = value; } }

        private string _ResidenceHomePhone = String.Empty;

        public string ResidenceHomePhone { get { return _ResidenceHomePhone; } set { this._ResidenceHomePhone = value; } }

        private string _CurrentAddress = String.Empty;

        public string CurrentAddress { get { return _CurrentAddress; } set { this._CurrentAddress = value; } }

        private string _CurrentWard = String.Empty;

        public string CurrentWard { get { return _CurrentWard; } set { this._CurrentWard = value; } }

        private string _CurrentDistrict = String.Empty;

        public string CurrentDistrict { get { return _CurrentDistrict; } set { this._CurrentDistrict = value; } }
        private string _CurrentCity = String.Empty;

        public string CurrentCity { get { return _CurrentCity; } set { this._CurrentCity = value; } }
        private string _CurrentHome = String.Empty;

        public string CurrentHome { get { return _CurrentHome; } set { this._CurrentHome = value; } }

        private string _ResidenceTime = String.Empty;

        public string ResidenceTime { get { return _ResidenceTime; } set { this._ResidenceTime = value; } }
        private string _NumberOfDependents = String.Empty;

        public string NumberOfDependents { get { return _NumberOfDependents; } set { this._NumberOfDependents = value; } }

        private string _Education = String.Empty;

        public string Education { get { return _Education; } set { this._Education = value; } }

        private string _HomeOwnership = String.Empty;

        public string HomeOwnership { get { return _HomeOwnership; } set { this._HomeOwnership = value; } }

        private string _PropertyOwnership = String.Empty;

        public string PropertyOwnership { get { return _PropertyOwnership; } set { this._PropertyOwnership = value; } }

        private string _PrimarySchool = String.Empty;

        public string PrimarySchool { get { return _PrimarySchool; } set { this._PrimarySchool = value; } }

        private string _MotherName = String.Empty;

        public string MotherName { get { return _MotherName; } set { this._MotherName = value; } }

        private string _FrequencyOnline = String.Empty;

        public string FrequencyOnline { get { return _FrequencyOnline; } set { this._FrequencyOnline = value; } }

        private string _DevicesOnline = String.Empty;

        public string DevicesOnline { get { return _DevicesOnline; } set { this._DevicesOnline = value; } }

        private int _Company;

        public int Company { get { return _Company; } set { this._Company = value; } }

        private string _StaffCode = String.Empty;

        public string StaffCode { get { return _StaffCode; } set { this._StaffCode = value; } }

        private string _Parts = String.Empty;

        public string Parts { get { return _Parts; } set { this._Parts = value; } }

        private string _TypeOfContract = String.Empty;

        public string TypeOfContract { get { return _TypeOfContract; } set { this._TypeOfContract = value; } }

        private string _RemainingTime = String.Empty;

        public string RemainingTime { get { return _RemainingTime; } set { this._RemainingTime = value; } }

        private string _TotalIncome = String.Empty;

        public string TotalIncome { get { return _TotalIncome; } set { this._TotalIncome = value; } }

        private string _NameContact1 = String.Empty;

        public string NameContact1 { get { return _NameContact1; } set { this._NameContact1 = value; } }

        private string _RelationshipContact1 = String.Empty;

        public string RelationshipContact1 { get { return _RelationshipContact1; } set { this._RelationshipContact1 = value; } }

        private string _PhoneContact1 = String.Empty;

        public string PhoneContact1 { get { return _PhoneContact1; } set { this._PhoneContact1 = value; } }

        private string _NameContact2 = String.Empty;

        public string NameContact2 { get { return _NameContact2; } set { this._NameContact2 = value; } }

        private string _RelationshipContact2 = String.Empty;

        public string RelationshipContact2 { get { return _RelationshipContact2; } set { this._RelationshipContact2 = value; } }

        private string _PhoneContact2 = String.Empty;

        public string PhoneContact2 { get { return _PhoneContact2; } set { this._PhoneContact2 = value; } }

        private string _IntroducedTheName = String.Empty;

        public string IntroducedTheName { get { return _IntroducedTheName; } set { this._IntroducedTheName = value; } }

        private string _IntroducedThePhone = String.Empty;

        public string IntroducedThePhone { get { return _IntroducedThePhone; } set { this._IntroducedThePhone = value; } }

        private string _IntroducedThePhysicalID = String.Empty;

        public string IntroducedThePhysicalID { get { return _IntroducedThePhysicalID; } set { this._IntroducedThePhysicalID = value; } }

        private string _UserLogin = String.Empty;

        public string UserLogin { get { return _UserLogin; } set { this._UserLogin = value; } }

        private string _StatusLogin = String.Empty;

        public string StatusLogin { get { return _StatusLogin; } set { this._StatusLogin = value; } }

        private bool _IsInValidYearBorn;

        public bool IsInValidYearBorn { get { return _IsInValidYearBorn; } set { this._IsInValidYearBorn = value; } }

        private bool _IsComplete;

        public bool IsComplete { get { return _IsComplete; } set { this._IsComplete = value; } }

        private bool _IsNeedCheck;

        public bool IsNeedCheck { get { return _IsNeedCheck; } set { this._IsNeedCheck = value; } }

        private bool _IsRecieve;

        public bool IsRecieve { get { return _IsRecieve; } set { this._IsRecieve = value; } }

        private string _UserInput = String.Empty;

        public string UserInput { get { return _UserInput; } set { this._UserInput = value; } }

        private string _SalaryReal = String.Empty;

        public string SalaryReal { get { return _SalaryReal; } set { this._SalaryReal = value; } }

        private string _WorkingYears = String.Empty;

        public string WorkingYears { get { return _WorkingYears; } set { this._WorkingYears = value; } }

        private string _department = String.Empty;

        public string Department { get { return _department; } set { this._department = value; } }


        private string _note2 = String.Empty;

        public string Note2 { get { return _note2; } set { this._note2 = value; } }

        private bool _telesalesVerify;
        private string _verifynote = String.Empty;
        public bool TeleSalesVerify
        {
            get
            {
                return this._telesalesVerify;
            }
            set
            {
                this._telesalesVerify = value;
            }
        }
        public string VerifyNote
        {
            get
            {
                return this._verifynote;
            }
            set
            {
                this._verifynote = value;
            }
        }

        // namnh

        public string Businessunit { get; set; }

        public string Location { get; set; }

        private string _issuer = String.Empty;

        public string Issuer { get { return _issuer; } set { this._issuer = value; } }


        //MinhNQ : ngay sao ke gan nhat

        public DateTime SettlementDate { get; set; }

        //MinhNQ : filter type of transaction

        public long TotalOrder { get; set; }

        public long TotalAirtime { get; set; }

        public long TotalCashAdvance { get; set; }

        public long TotalDeposit { get; set; }

        private double _scredit;
        public double SCredit { get { return _scredit; } set { this._scredit = value; } }

        private double _sduelimit;
        public double SDueLimit { get { return _sduelimit; } set { this._sduelimit = value; } }
        // minhtc

        public string OrgRule { get; set; }

        public string MBVRule { get; set; }

        //DungNT: Add Available Credit
        public double AvailableCredit { get; set; }
        public string AreaName { get; set; }
        public string RegionName { get; set; }

        public CustomerProperty Properties { get; set; }
        public string LongName { get; set;}
        public string NoteCustomer { get; set; }
        public class CustomerProperty
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Salary { get; set; }
        }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        #endregion

        public DW_DC_Customer()
        { }

        public DW_DC_Customer(string CustomerID, string FamilyName, string GivenName, string Gender, string Phone, string MobilePhone, string Email, string MaritalStatus, string Position, bool OrganizationAdmin, DateTime OrgStartDate, DateTime OrgEndDate, string XAccountID, string GroupID, DateTime LastLogin, string Status, string PhysicalID, string Address, string Contract, double CreditLimit, long DWID, DateTime CreatedAt, DateTime ModifiedAt, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._customerid = CustomerID;
            this._familyname = FamilyName;
            this._givenname = GivenName;
            this._gender = Gender;
            this._phone = Phone;
            this._mobilephone = MobilePhone;
            this._email = Email;
            this._maritalstatus = MaritalStatus;
            this._position = Position;
            this._organizationadmin = OrganizationAdmin;
            this._orgstartdate = OrgStartDate;
            this._orgenddate = OrgEndDate;
            this._xaccountid = XAccountID;
            this._groupid = GroupID;
            this._lastlogin = LastLogin;
            this._status = Status;
            this._physicalid = PhysicalID;
            this._address = Address;
            this._contract = Contract;
            this._creditlimit = CreditLimit;
            this._dwid = DWID;
            this._createdat = CreatedAt;
            this._modifiedat = ModifiedAt;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public static void SyncCustomer()
        {
            using (var client = new RedisClient())
            {
                var before = DateTime.Now;
                //client.FlushAll();
                IRedisTypedClient<DW_DC_Customer> redis = client.As<DW_DC_Customer>();
                var listcus = redis.Lists["dw-DC-customer"].GetAll().Where(s => s.CustomerID == "SAMHO:0001");
                //var data = GetAllDW_DC_CustomersDynamic("data.OrganizationID = 'SAMHO'");
                //redis.SetSequence(0);
                //redis.RemoveAllFromList(listcus);
                //listcus.AddRange(data);

                Console.WriteLine("Took {0}ms to get the database",
                    (DateTime.Now - before).TotalMilliseconds);
            }
        }


        public int Delete()
        {
            string PROCEDURE = "p_DeleteDW_DC_Customer";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DW_DC_Customer GetDW_DC_Customer(string CustomerID)
        {
            string PROCEDURE = "select * from vw_CustomerRaw WHERE CustomerID = '" + CustomerID + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["Added Profile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Customer> GetDW_DC_Customers(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_DC_CustomersDynamic";
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
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FamilyName = row["FamilyName"].ToString(),
                GivenName = row["GivenName"].ToString(),
                Gender = row["Gender"].ToString(),
                Phone = row["Phone"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                OrganizationAdmin = Convert.ToBoolean(row["OrganizationAdmin"].ToString()),
                OrgStartDate = Convert.ToDateTime(row["OrgStartDate"].ToString()),
                OrgEndDate = Convert.ToDateTime(row["OrgEndDate"].ToString()),
                XAccountID = row["XAccountID"].ToString(),
                GroupID = row["GroupID"].ToString(),
                LastLogin = Convert.ToDateTime(row["LastLogin"].ToString()),
                Status = row["Status"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                Address = row["Address"].ToString(),
                Contract = row["Contract"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CustomerGroupID = DC_Customer_Group.GetDC_Customer_Group(row["CustomerID"].ToString()) != null ? DC_Customer_Group.GetDC_Customer_Group(row["CustomerID"].ToString()).GroupID : "",
                Name = row["FamilyName"].ToString() + " " + row["GivenName"].ToString(),
                OrganizationID = row["CustomerID"].ToString().Split(':')[0]
            }).ToList();
        }

        public static List<DW_DC_Customer> GetCustomerID_like(string orgID, string customerID)
        {
            string PROCEDURE = "SELECT a.CustomerID,a.FamilyName + ' ' + a.GivenName AS 'FullName', isnull((Select top 1 Value from DW_DC_Customer_Property where Name ='EmployeeID' and RefID=N'" + customerID + "'),'' )as 'EmployeeID' FROM DW_DC_Customer a  where a.CustomerID like '" + orgID + "%' and a.CustomerID = N'" + customerID + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
            }).ToList();
        }
        public static DW_DC_Customer GetCustomerID_ByEmployeeID(string EmployeeID, string OrganizationID)
        {
            string PROCEDURE = "select cus.CustomerID,cus.FamilyName + ' ' + cus.GivenName  AS 'FullName' from (select  * from DW_DC_Customer_Property  where Name ='EmployeeID' and Value='" + EmployeeID + "') p left join DW_DC_Customer cus on cus.CustomerID = p.RefID where p.RefID like N'" + OrganizationID + "%'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                EmployeeID = EmployeeID,
            }).ToList().FirstOrDefault();
        }

        //vuongnd-add list customerId by Organization
        public static List<DW_DC_Customer> GetCustomerIdAll(string organizationId)
        {
            string PROCEDURE = "SELECT a.CustomerID,a.FamilyName + ' ' + a.GivenName AS 'FullName'  FROM DW_DC_Customer a where LEFT(a.CustomerID,CHARINDEX(':', a.CustomerID) - 1) = '" + organizationId + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
            }).ToList();
        }
        //end

        //namnh-add list customerId by Organization
        public static List<DW_DC_Customer> GetCustomerAllForBadDebt(string cus)
        {
            cus = string.IsNullOrEmpty(cus) ? "N'%%'" : "N'%" + cus + "%'";
            string PROCEDURE = "SELECT TOP 10 a.CustomerID,a.FamilyName + ' ' + a.GivenName AS 'FullName'  FROM DW_DC_Customer a WHERE a.CustomerID LIKE " + cus + "";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
            }).ToList();
        }
        //end

        public static DW_DC_Customer GetCustomerData(string CustomerID)
        {
            string PROCEDURE = "select * from DW_DC_Customer where CustomerID='" + CustomerID.Replace("'", "''") + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FamilyName = row["FamilyName"].ToString(),
                GivenName = row["GivenName"].ToString(),
                Gender = row["Gender"].ToString(),
                Phone = row["Phone"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                OrganizationAdmin = Convert.ToBoolean(row["OrganizationAdmin"].ToString()),
                OrgStartDate = Convert.ToDateTime(row["OrgStartDate"].ToString()),
                OrgEndDate = Convert.ToDateTime(row["OrgEndDate"].ToString()),
                XAccountID = row["XAccountID"].ToString(),
                GroupID = row["GroupID"].ToString(),
                LastLogin = Convert.ToDateTime(row["LastLogin"].ToString()),
                Status = row["Status"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                Address = row["Address"].ToString(),
                Contract = row["Contract"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Customer> GetAllDW_DC_Customers()
        {
            string PROCEDURE = "select * from vw_CustomerRaw";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["Added Profile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                LastUpdatedAt = DateTime.Parse(row["LastUpdatedAt"].ToString()),
                LastUpdatedBy = row["LastUpdatedBy"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Customer> GetAllDW_DC_CustomersByOrg(string organizationID)
        {
            string PROCEDURE = "p_getCustomerRaw";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = organizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                //Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                TeleSalesVerify = Convert.ToBoolean(row["TeleSalesVerify"].ToString()),
                VerifyNote = row["VerifyNote"].ToString(),
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                Issuer = row["Issuer"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                SCredit = Double.Parse(row["SCredit"].ToString()),
                SDueLimit = Double.Parse(row["SDueLimit"].ToString()),
                ClassName = Int32.Parse(row["ClassName"].ToString()),
                SSeniority = Int32.Parse(row["SSeniority"].ToString()),
                ClassAuto = row["ClassAuto"].ToString(),
                SeniorityAuto = row["SeniorityAuto"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Customer> GetAllDW_DC_CustomersDynamic(string WhereCondition)
        {
            string PROCEDURE = "p_getCustomerRawDynamicAll";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["Name"].ToString(),
                CustomerGroupID = row["CustomerGroupID"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                Issuer = row["Issuer"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["Status"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                Position = row["Position"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                TeleSalesVerify = Convert.ToBoolean(row["TeleSalesVerify"].ToString()),
                VerifyNote = row["VerifyNote"].ToString(),
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                //tinh bang code
                SCredit = Double.Parse(row["SCredit"].ToString()),
                SDueLimit = Double.Parse(row["SDueLimit"].ToString()),

                ClassName = Int32.Parse(row["ClassName"].ToString()),
                SSeniority = Int32.Parse(row["SSeniority"].ToString()),
                ClassAuto = row["ClassAuto"].ToString(),
                SeniorityAuto = row["SeniorityAuto"].ToString(),
                Department = row["Department"].ToString(),
            }).ToList();
        }

        public static List<DW_DC_Customer> GetAllDW_DC_CustomersDynamicView(string OrganizationID, string WhereCondition)
        {
            string PROCEDURE = "p_getCustomerRawDynamicAllView";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["Name"].ToString(),
                CustomerGroupID = row["CustomerGroupID"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                Issuer = row["Issuer"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["Status"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                Position = row["Position"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                //CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                TeleSalesVerify = Convert.ToBoolean(row["TeleSalesVerify"].ToString()),
                VerifyNote = row["VerifyNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                SCredit = Double.Parse(row["SCredit"].ToString()),
                SDueLimit = Double.Parse(row["SDueLimit"].ToString()),
                Department = row["Department"].ToString(),
                BankName = row["BankName"].ToString(),
                BankBranch = row["BankBranch"].ToString(),
                BankAccount = row["BankAccount"].ToString(),
                DC_Note = row["DC_Note"].ToString(),
            }).ToList();
        }

        public static DataSourceResult GetAllDW_DC_CustomersDynamicDS(string WhereCondition, [DataSourceRequest] DataSourceRequest request)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_getCustomerRawDynamicAllDS";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;

            var data = new DataSourceResult();

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            data.Data = dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                //Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                TeleSalesVerify = Convert.ToBoolean(row["TeleSalesVerify"].ToString()),
                VerifyNote = row["VerifyNote"].ToString(),
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                Issuer = row["Issuer"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                SCredit = Double.Parse(row["SCredit"].ToString()),
                SDueLimit = Double.Parse(row["SDueLimit"].ToString()),
                ClassName = Int32.Parse(row["ClassName"].ToString()),
                SSeniority = Int32.Parse(row["SSeniority"].ToString()),
                ClassAuto = row["ClassAuto"].ToString(),
                SeniorityAuto = row["SeniorityAuto"].ToString(),
                Department = row["Department"].ToString(),
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }

        public int TotalRows { get; set; }
        public int ClassName { get; set; }
        public int SSeniority { get; set; }

        public string ClassAuto { get; set; }
        public string SeniorityAuto { get; set; }
        public int countFile { get; set; }

        public static int CountFile(string id)
        {
            try
            {
                string pathForSaving = HttpContext.Current.Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-"));
                int totalfile = 0; ;
                if (Directory.Exists(pathForSaving))
                {
                    totalfile = Directory.GetFiles(pathForSaving).Count();
                }
                return totalfile;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    result = false;
                }
            }
            return result;
        }

        //minhtc
        public static List<DW_DC_Customer> GetAllDW_DC_CustomersByOrg_Export(string organizationID)
        {
            string PROCEDURE = "p_getCustomerRaw_Export";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = organizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                ProfileCode = int.Parse(row["ProfileCode"].ToString()),
                Sex = row["Sex"].ToString(),
                NaneDC = row["NaneDC"].ToString(),
                DateOfBirth = row["DateOfBirth"].ToString(),
                PlaceOfBirth = row["PlaceOfBirth"].ToString(),
                Nationality = row["Nationality"].ToString(),
                Residence = row["Residence"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                AddressProvided = row["AddressProvided"].ToString(),
                DateProvided = row["DateProvided"].ToString(),
                EmailPerson = row["EmailPerson"].ToString(),
                EmailCompany = row["EmailCompany"].ToString(),
                ResidenceAddress = row["ResidenceAddress"].ToString(),
                ResidenceWard = row["ResidenceWard"].ToString(),
                ResidenceDistrict = row["ResidenceDistrict"].ToString(),
                ResidenceCity = row["ResidenceCity"].ToString(),
                ResidenceHomePhone = row["ResidenceHomePhone"].ToString(),
                CurrentAddress = row["CurrentAddress"].ToString(),
                CurrentWard = row["CurrentWard"].ToString(),
                CurrentDistrict = row["CurrentDistrict"].ToString(),
                CurrentCity = row["CurrentCity"].ToString(),
                CurrentHome = row["CurrentHome"].ToString(),
                ResidenceTime = row["ResidenceTime"].ToString(),
                NumberOfDependents = row["NumberOfDependents"].ToString(),
                Education = row["Education"].ToString(),
                HomeOwnership = row["HomeOwnership"].ToString(),
                PropertyOwnership = row["PropertyOwnership"].ToString(),
                PrimarySchool = row["PrimarySchool"].ToString(),
                MotherName = row["MotherName"].ToString(),
                FrequencyOnline = row["FrequencyOnline"].ToString(),
                DevicesOnline = row["DevicesOnline"].ToString(),
                Company = int.Parse(row["Company"].ToString()),
                StaffCode = row["StaffCode"].ToString(),
                Parts = row["Parts"].ToString(),
                TypeOfContract = row["TypeOfContract"].ToString(),
                RemainingTime = row["RemainingTime"].ToString(),
                TotalIncome = row["TotalIncome"].ToString(),
                NameContact1 = row["NameContact1"].ToString(),
                RelationshipContact1 = row["RelationshipContact1"].ToString(),
                PhoneContact1 = row["PhoneContact1"].ToString(),
                NameContact2 = row["NameContact2"].ToString(),
                RelationshipContact2 = row["RelationshipContact2"].ToString(),
                PhoneContact2 = row["PhoneContact2"].ToString(),
                IntroducedTheName = row["IntroducedTheName"].ToString(),
                IntroducedThePhone = row["IntroducedThePhone"].ToString(),
                IntroducedThePhysicalID = row["IntroducedThePhysicalID"].ToString(),
                UserLogin = row["UserLogin"].ToString(),
                StatusLogin = row["StatusLogin"].ToString(),
                IsInValidYearBorn = Boolean.Parse(row["IsInValidYearBorn"].ToString()),
                IsComplete = Boolean.Parse(row["IsComplete"].ToString()),
                IsNeedCheck = Boolean.Parse(row["IsNeedCheck"].ToString()),
                IsRecieve = Boolean.Parse(row["IsRecieve"].ToString()),
                UserInput = row["UserInput"].ToString(),
                SalaryReal = row["SalaryReal"].ToString(),
                WorkingYears = row["WorkingYears"].ToString(),
                Note2 = row["Note2"].ToString(),
                // namnh
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                Issuer = row["Issuer"].ToString(),
            }).ToList();
        }

        //minhtc
        public static List<DW_DC_Customer> GetAllDW_DC_Customers_Export(string CustomerID)
        {
            string PROCEDURE = "p_getCustomerRaw_Export_Report";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),

                // namnh
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                Issuer = row["Issuer"].ToString(),
            }).ToList();
        }
        public static List<DW_DC_Customer> GetAllDW_DC_CustomersByCus(string CusID)
        {
            string PROCEDURE = "p_getCustomerRaw_ByCus";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CusID";
            parameters[i].Value = CusID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            DataTable dt2 = dt.Clone();
            for (int y = 0; y < dt.Columns.Count; y++)
            {
                DataRow row = dt2.NewRow();
                row["NaneDC"] = dt.Columns[y].ColumnName;
                row["Note"] = dt.Rows[0][y].ToString();
                if (dt.Columns[y].ColumnName == "NaneDC")
                {
                    row["NaneDC"] = "NameDC";
                }
                dt2.Rows.Add(row);
            }
            return dt2.AsEnumerable().Select(row => new DW_DC_Customer
            {
                NaneDC = row["NaneDC"].ToString(),
                Note2 = row["Note"].ToString(),
            }).ToList();
        }

        public static List<DW_DC_Customer> GetGroupID()
        {
            string PROCEDURE = "select DISTINCT GroupID from DC_Customer_Group";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerGroupID = row["GroupID"].ToString()
            }).ToList();
        }


        public static List<DW_DC_Customer> GetSuggestDC_CustomersList(string OrganizationID, string CustomerID, string FullName, string Phone, string Email)
        {

            CustomerID = string.IsNullOrEmpty(CustomerID) ? "N'%%'" : "N'%" + CustomerID + "%'";
            FullName = string.IsNullOrEmpty(FullName) ? "N'%%'" : "N'%" + FullName + "%'";
            Phone = string.IsNullOrEmpty(Phone) ? "N'%%'" : "N'%" + Phone + "%'";
            Email = string.IsNullOrEmpty(Email) ? "N'%%'" : "N'%" + Email + "%'";


            string PROCEDURE = "EXEC [p_Select_DC_Customer_Suggest] @OrganizationID = '" + OrganizationID + "'," + " @CustomerID = " + CustomerID + ",@MobilePhone = " + Phone + ",@Email = " + Email + ",@FullName = " + FullName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
               // Issuer = row["Issuer"].ToString(),
            }).ToList();
        }

        // NAM (Credit Limit Request.)

        // Create new because column :   TelesalesAgent = row["TelesalesAgent"].ToString() not exist.
        public static List<DW_DC_Customer> GetAllDW_DC_CustomersCheckList()
        {
            string PROCEDURE = "select * from vw_CustomerRaw";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),

            }).ToList();
        }


        public static DW_DC_Customer GetLoadSuggestDC_Customers(string OrganizationID, string CustomerID, string Phone, string Email, string FullName)
        {

            CustomerID = string.IsNullOrEmpty(CustomerID) ? "N'%%'" : "N'" + CustomerID + "'";
            Phone = string.IsNullOrEmpty(Phone) ? "N'%%'" : "N'" + Phone + "'";
            Email = string.IsNullOrEmpty(Email) ? "N'%%'" : "N'" + Email + "'";
            FullName = string.IsNullOrEmpty(FullName) ? "N'%%'" : "N'" + FullName + "'";

            string PROCEDURE = "EXEC [p_Select_DC_Customer_Load_Suggest] @OrganizationID = '" + OrganizationID + "'," +
                " @Phone = " + Phone + ",@CustomerID= " + CustomerID + ",@FullName= " + FullName + ",@Email= " + Email;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["Added Profile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                DCStatus = row["DCStatus"].ToString(),
                Issuer = row["Issuer"].ToString(),
                OrgRule = row["OrgRule"].ToString(),
                MBVRule = row["MBVRule"].ToString()
            }).ToList().FirstOrDefault();
        }


        public static DW_DC_Customer GetLoadSuggestSetttledBalance(string CustomerID)
        {

            CustomerID = string.IsNullOrEmpty(CustomerID) ? "N'%%'" : "N'" + CustomerID + "'";

            string PROCEDURE = "EXEC [p_DC_GetSetttledBalanceCustomerResignedList] @CustomerID = " + CustomerID;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
            }).ToList().FirstOrDefault();
        }
        public static DW_DC_Customer CheckCustomerID(string CustomerID)
        {
            string PROCEDURE = "select CustomerID, FamilyName + ' ' + GivenName AS 'FullName',[Status] AS DCStatus from DW_DC_Customer where CustomerID= @CustomerID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                DCStatus = row["DCStatus"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Customer> GetAllDW_DC_CustomersPrint()
        {
            string PROCEDURE = "p_getCustomerPrint";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["UserID"].ToString()),
                CustomerID = row["UserID"].ToString(),
                FullName = row["FullName"].ToString(),
                Sex = row["Sex"].ToString(),
                DateOfBirth = row["DateOfBirth"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                Address = row["Address"].ToString(),
                CurrentAddress = row["CurrentAddress"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                EmailPerson = row["Email"].ToString(),
                NameContact1 = row["NameContact1"].ToString(),
                PhoneContact1 = row["PhoneContact1"].ToString(),
                Position = row["Position"].ToString(),
                Parts = row["Parts"].ToString(),
                TypeOfContract = row["TypeOfContract"].ToString(),
                SalaryReal = row["SalaryReal"].ToString(),
                TotalIncome = row["TotalIncome"].ToString(),
                OrganizationID = row["Organization"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                EmployeeID = row["Employee"].ToString()

            }).ToList();
        }

        //DungNT: get Customer Print Dynamic
        public static List<DW_DC_Customer> GetAllDW_DC_CustomersPrintDynamic(string WhereCondition)
        {
            string PROCEDURE = "p_getCustomerPrintDynamic";

            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["UserID"].ToString()),
                CustomerID = row["UserID"].ToString(),
                FullName = row["FullName"].ToString(),
                Sex = row["Sex"].ToString(),
                DateOfBirth = row["DateOfBirth"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                Address = row["Address"].ToString(),
                CurrentAddress = row["CurrentAddress"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                EmailPerson = row["Email"].ToString(),
                NameContact1 = row["NameContact1"].ToString(),
                PhoneContact1 = row["PhoneContact1"].ToString(),
                Position = row["Position"].ToString(),
                Parts = row["Parts"].ToString(),
                TypeOfContract = row["TypeOfContract"].ToString(),
                SalaryReal = row["SalaryReal"].ToString(),
                TotalIncome = row["TotalIncome"].ToString(),
                OrganizationID = row["Organization"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                EmployeeID = row["Employee"].ToString(),
                CustomerID1 = row["CustomerID"].ToString()
            }).ToList();
        }
        public string CustomerID1 { get; set; }


        //MinhNQ - Get All VefiryCustomer
        public static List<DW_DC_Customer> GetAllDW_DC_VerifyCustomersByOrg(string organizationID)
        {
            string PROCEDURE = "p_getVerifyCustomerRaw";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = organizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                //Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                TeleSalesVerify = Convert.ToBoolean(row["TeleSalesVerify"].ToString()),
                VerifyNote = row["VerifyNote"].ToString(),
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                Issuer = row["Issuer"].ToString(),
                SettlementDate = DateTime.Parse(row["SettlementDate"].ToString()),
                TotalOrder = long.Parse(row["TotalOrder"].ToString()),
                TotalAirtime = long.Parse(row["TotalAirtime"].ToString()),
                TotalCashAdvance = long.Parse(row["TotalCashAdvance"].ToString()),
                TotalDeposit = long.Parse(row["TotalDeposit"].ToString()),
            }).ToList();
        }
        public static List<DW_DC_Customer> GetAllDW_DC_VerifyCustomersByOrg_Export(string organizationID)
        {
            string PROCEDURE = "p_getVerifyCustomerRaw_Export";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = organizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Name = row["FullName"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Double.Parse(row["Salary"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                OrgContractStatus = row["OrgContractStatus"].ToString(),
                OrgEmployee = Double.Parse(row["OrgEmployee"].ToString()),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                ActivatedAt = DateTime.Parse(row["ActivatedAt"].ToString()),
                ClosedAt = DateTime.Parse(row["ClosedAt"].ToString()),
                LastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString()),
                AddedProfile = Boolean.Parse(row["AddedProfile"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                StartWorkingDate = DateTime.Parse(row["StartWorkingDate"].ToString()),
                TelesalesAgent = row["TelesalesAgent"].ToString(),
                ProfileCode = int.Parse(row["ProfileCode"].ToString()),
                Sex = row["Sex"].ToString(),
                NaneDC = row["NaneDC"].ToString(),
                DateOfBirth = row["DateOfBirth"].ToString(),
                PlaceOfBirth = row["PlaceOfBirth"].ToString(),
                Nationality = row["Nationality"].ToString(),
                Residence = row["Residence"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                AddressProvided = row["AddressProvided"].ToString(),
                DateProvided = row["DateProvided"].ToString(),
                EmailPerson = row["EmailPerson"].ToString(),
                EmailCompany = row["EmailCompany"].ToString(),
                ResidenceAddress = row["ResidenceAddress"].ToString(),
                ResidenceWard = row["ResidenceWard"].ToString(),
                ResidenceDistrict = row["ResidenceDistrict"].ToString(),
                ResidenceCity = row["ResidenceCity"].ToString(),
                ResidenceHomePhone = row["ResidenceHomePhone"].ToString(),
                CurrentAddress = row["CurrentAddress"].ToString(),
                CurrentWard = row["CurrentWard"].ToString(),
                CurrentDistrict = row["CurrentDistrict"].ToString(),
                CurrentCity = row["CurrentCity"].ToString(),
                CurrentHome = row["CurrentHome"].ToString(),
                ResidenceTime = row["ResidenceTime"].ToString(),
                NumberOfDependents = row["NumberOfDependents"].ToString(),
                Education = row["Education"].ToString(),
                HomeOwnership = row["HomeOwnership"].ToString(),
                PropertyOwnership = row["PropertyOwnership"].ToString(),
                PrimarySchool = row["PrimarySchool"].ToString(),
                MotherName = row["MotherName"].ToString(),
                FrequencyOnline = row["FrequencyOnline"].ToString(),
                DevicesOnline = row["DevicesOnline"].ToString(),
                Company = int.Parse(row["Company"].ToString()),
                StaffCode = row["StaffCode"].ToString(),
                Parts = row["Parts"].ToString(),
                TypeOfContract = row["TypeOfContract"].ToString(),
                RemainingTime = row["RemainingTime"].ToString(),
                TotalIncome = row["TotalIncome"].ToString(),
                NameContact1 = row["NameContact1"].ToString(),
                RelationshipContact1 = row["RelationshipContact1"].ToString(),
                PhoneContact1 = row["PhoneContact1"].ToString(),
                NameContact2 = row["NameContact2"].ToString(),
                RelationshipContact2 = row["RelationshipContact2"].ToString(),
                PhoneContact2 = row["PhoneContact2"].ToString(),
                IntroducedTheName = row["IntroducedTheName"].ToString(),
                IntroducedThePhone = row["IntroducedThePhone"].ToString(),
                IntroducedThePhysicalID = row["IntroducedThePhysicalID"].ToString(),
                UserLogin = row["UserLogin"].ToString(),
                StatusLogin = row["StatusLogin"].ToString(),
                IsInValidYearBorn = Boolean.Parse(row["IsInValidYearBorn"].ToString()),
                IsComplete = Boolean.Parse(row["IsComplete"].ToString()),
                IsNeedCheck = Boolean.Parse(row["IsNeedCheck"].ToString()),
                IsRecieve = Boolean.Parse(row["IsRecieve"].ToString()),
                UserInput = row["UserInput"].ToString(),
                SalaryReal = row["SalaryReal"].ToString(),
                WorkingYears = row["WorkingYears"].ToString(),
                Note2 = row["Note2"].ToString(),
                // namnh
                Businessunit = row["BusinessUnit"].ToString(),
                Location = row["location"].ToString(),
                Issuer = row["Issuer"].ToString(),
                SettlementDate = DateTime.Parse(row["SettlementDate"].ToString())
            }).ToList();
        }

        //MinhNQ -END Get All VefiryCustomer

        public static List<DW_DC_Customer> GetDW_DC_Customers4Salesman(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_DC_CustomersDynamic4Salesman";
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
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Gender = row["Gender"].ToString(),
                Phone = row["Phone"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                Address = row["Address"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString()
            }).ToList();
        }


        public static DataSourceResult GetDW_DC_Customers4SalesmanFilter(string whereCondition, [DataSourceRequest] DataSourceRequest request)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_SelectDW_DC_CustomersDynamic4SalesmanFilter";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            i++;
            var data = new DataSourceResult();
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            data.Data = dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Gender = row["Gender"].ToString(),
                Phone = row["Phone"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                MaritalStatus = row["MaritalStatus"].ToString(),
                Position = row["Position"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                Address = row["Address"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                TotalRequestOrder = Convert.ToDouble(row["TotalRequestOrder"].ToString()),
                TotalRequestOrderBG = row["TotalRequestOrder"].ToString() == "0" ? "" : "background-color:#FFFF99;color:#991116;"
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;
            return data;
        }

        public double TotalRequestOrder { get; set; }
        public string TotalRequestOrderBG { get; set; }


        public static List<DW_DC_Customer> GetCustomersByPhone(string phone)
        {
            string PROCEDURE = "p_getInfoCustomerSMSMO";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = phone;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),

            }).ToList();
        }

        //vuongnd
        public static DW_DC_Customer GetCustomerByPhone(string phone)
        {
            string PROCEDURE = "p_SelectDW_DC_CustomerByPhone";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = phone;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString()
            }).FirstOrDefault();
        }

        public static DW_DC_Customer getCustomerByPhoneXlite(string phone)
        {
            string PROCEDURE = "p_getInfoCustomerXlite";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = phone;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                DCStatus = row["DCStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                PhysicalID = row["PhysicalID"].ToString(),
                BD = row["BD"].ToString(),
            }).FirstOrDefault();
        }

        //DungNT: add fuction load data by customer
        public static DW_DC_Customer GetCustomerDataByCustomerID(string CustomerID)
        {
            string PROCEDURE = "p_Select_DC_Customer_GetInfomationForTeleSale";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FamilyName = row["FamilyName"].ToString(),
                GivenName = row["GivenName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Position = row["Position"].ToString(),
                GroupID = row["GroupID"].ToString(),
                Status = row["Status"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                CreditLimit = Convert.ToDouble(row["CreditLimit"].ToString()),
                DateOfBirth =row["DateOfBirth"].ToString(),
                DueLimit = Convert.ToDouble(row["DueLimit"].ToString()),
                AvailableCredit = Convert.ToDouble(row["AvailableCredit"].ToString()),
                Department = row["Department"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                TeleSalesVerify = bool.Parse(row["TeleSalesVerify"].ToString()),
                AccountStatus = row["AccountStatus"].ToString()
            }).ToList().FirstOrDefault();
        }
        public string AccountStatus { get; set; }

        public static List<DW_DC_Customer> GetCustomersTopRandom(string top)
        {
            string PROCEDURE = "select top " + top + " * from (select top 100 * from DW_DC_Customer)data order by NEWID()";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                FakeCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FamilyName"].ToString() + row["GivenName"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                DueLimit = Double.Parse(row["DueLimit"].ToString()),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                Status = row["DCStatus"].ToString(),
            }).ToList();
        }

        public string ResNote { get; set; }
        public string ResignedReason { get; set; }
        public DateTime ResignedDate { get; set; }
        public DateTime InformedDate { get; set; }
        public string LastActionStatus { get; set; }
        public string ActionCode { get; set; }
        public static DW_DC_Customer GetCustomerDataByCustomerIDForCS(string CustomerID)
        {
            string PROCEDURE = "p_SelectDC_CS_Customer_Info";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FamilyName = row["FamilyName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Position = row["Position"].ToString(),
                GroupID = row["GroupID"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                CreditLimit = Convert.ToDouble(row["CreditLimit"].ToString()),
                DateOfBirth = row["DateOfBirth"].ToString(),
                DueLimit = Convert.ToDouble(row["DueLimit"].ToString()),
                AvailableCredit = Convert.ToDouble(row["AvailableCredit"].ToString()),
                Email = row["Email"].ToString(),
                DCStatus = row["DCStatus"].ToString(),
                ActualStatus = row["ActualStatus"].ToString(),
                NoteCustomer = row["NoteCustomer"].ToString(),
                RunningBalance = Double.Parse(row["RunningBalance"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                DueBalance = Double.Parse(row["DueBalance"].ToString()),
                ResNote = row["ResNote"].ToString(),
                ResignedReason = row["ResignedReason"].ToString(),
                ResignedDate = Convert.ToDateTime(row["ResignedDate"].ToString()),
                InformedDate = Convert.ToDateTime(row["InformedDate"].ToString()),
                LastActionStatus = row["LastActionStatus"].ToString(),
                ActionCode = row["ActionCode"].ToString(),
                Address = row["Address"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                DCNote = row["DCNote"].ToString()
            }).ToList().FirstOrDefault();
        }
        public string DCNote { get; set; }
        public string DC_Note { get; set; }

        //DungNT: add function for CAAT
        public static DW_DC_Customer GetInfomationforCAAT(string OrgID, string Phone)
        {
            string PROCEDURE = "SELECT top 1 ([FamilyName] + ' ' + [GivenName]) as FullName, MobilePhone, CustomerID, CreditLimit, ISNULL(B.Value,'') Issuer";
            PROCEDURE += " from dbo.DW_DC_Customer A";
            PROCEDURE += " LEFT JOIN DW_DC_Customer_Property B ON A.CustomerID = B.RefID AND Name = 'Issuer'";
            PROCEDURE += " WHERE LEFT([CustomerID], CHARINDEX(':', [CustomerID]) - 1) = @OrgID AND( CustomerID = @Phone OR MobilePhone = @Phone) ";

            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgID";
            parameters[i].Value = OrgID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = Phone;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                FullName = row["FullName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Issuer = row["Issuer"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Customer> GetCustomerActive(string OrganizationID)
        {
            string PROCEDURE = "p_DW_DC_Customer_GetActiveUser";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                RunningBalance = double.Parse(row["RunningBalance"].ToString()),
                CreditLimit = double.Parse(row["CreditLimit"].ToString()),
                Email = row["Email"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                OrganizationID = OrganizationID
            }).ToList();
        }

        public static DW_DC_Customer CheckExitsForCashLoan(string OrganizationID, string EmployeeID)
        {
            string PROCEDURE = "select CustomerID from DW_DC_Customer where CustomerID =@RefID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RefID";
            parameters[i].Value = OrganizationID + ":" + EmployeeID;
            i++;
            ////parameters[i] = new SqlParameter();
            ////parameters[i].ParameterName = "@EmployeeID";
            ////parameters[i].Value = EmployeeID;
            ////i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Customer
            {
                CustomerID = row["CustomerID"].ToString()
            }).FirstOrDefault();
        }
    }
}