using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;

namespace ERPAPD.Models
{
    public class DW_DC_Organization
    {
        #region Members
        private string _organizationid = String.Empty;
        public string OrganizationID
        {
            get
            {
                return _organizationid;
            }
            set
            {
                this._organizationid = value;
            }
        }

        private string _shortname = String.Empty;
        public string ShortName { get { return _shortname; } set { this._shortname = value; } }

        private string _longname = String.Empty;
        public string LongName { get { return _longname; } set { this._longname = value; } }

        private string _type = String.Empty;
        public string Type { get { return _type; } set { this._type = value; } }

        private string _group = String.Empty;
        public string Group { get { return _group; } set { this._group = value; } }

        private string _status = String.Empty;
        public string Status { get { return _status; } set { this._status = value; } }

        private string _xaccountid = String.Empty;
        public string XAccountID { get { return _xaccountid; } set { this._xaccountid = value; } }

        private DateTime _usageperiodstart;
        public DateTime UsagePeriodStart { get { return _usageperiodstart; } set { this._usageperiodstart = value; } }

        private DateTime _usageperiodend;
        public DateTime UsagePeriodEnd { get { return _usageperiodend; } set { this._usageperiodend = value; } }

        private DateTime _createdat;
        public DateTime CreatedAt { get { return _createdat; } set { this._createdat = value; } }

        private DateTime _modifiedat;
        public DateTime ModifiedAt { get { return _modifiedat; } set { this._modifiedat = value; } }

        private long _dwid;
        public long DWID { get { return _dwid; } set { this._dwid = value; } }

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
        private string _businessDevelopment = String.Empty;
        public string BusinessDevelopment { get { return _businessDevelopment; } set { this._businessDevelopment = value; } }


        private string _partnerDevelopment = String.Empty;
        public string PartnerDevelopment { get { return _partnerDevelopment; } set { this._partnerDevelopment = value; } }

        private string _bde = String.Empty;
        public string BDE { get { return _bde; } set { this._bde = value; } }

        private string _partner = String.Empty;
        public string Partner { get { return _partner; } set { this._partner = value; } }

        private string _contractstatus = String.Empty;
        public string ContractStatus { get { return _contractstatus; } set { this._contractstatus = value; } }


        private string _employee = String.Empty;
        public string Employee { get { return _employee; } set { this._employee = value; } }

        private DateTime _TentativeDeadline;
        public DateTime TentativeDeadline { get { return _TentativeDeadline; } set { this._TentativeDeadline = value; } }

        private DateTime _officalContractDate;
        public DateTime OfficalContractDate { get { return _officalContractDate; } set { this._officalContractDate = value; } }

        private DateTime _contractDate;
        public DateTime ContractDate { get { return _contractDate; } set { this._contractDate = value; } }

        private string _importNote = String.Empty;
        public string ImportNote { get { return _importNote; } set { this._importNote = value; } }

        private double _minCredit;
        public double MinCredit { get { return _minCredit; } set { this._minCredit = value; } }

        private double _maxCredit;
        public double MaxCredit { get { return _maxCredit; } set { this._maxCredit = value; } }

        private string _cashAdvanceLimit = String.Empty;
        public string CashAdvanceLimit { get { return _cashAdvanceLimit; } set { this._cashAdvanceLimit = value; } }

        private string _note = String.Empty;
        public string Note { get { return _note; } set { this._note = value; } }

        private string _creditLimitRules = String.Empty;
        public string CreditLimitRules { get { return _creditLimitRules; } set { this._creditLimitRules = value; } }

        private string _city = String.Empty;
        public string City { get { return _city; } set { this._city = value; } }

        private string _province = String.Empty;
        public string Province { get { return _province; } set { this._province = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }

        private string _deliverynote = String.Empty;
        public string DeliveryNote { get { return _deliverynote; } set { this._deliverynote = value; } }

        //new meta for org
        private string _OrgType = String.Empty;
        public string OrgType { get { return _OrgType; } set { this._OrgType = value; } }

        private string _OrgStatus = String.Empty;
        public string OrgStatus { get { return _OrgStatus; } set { this._OrgStatus = value; } }

        private string _OrgReason = String.Empty;
        public string OrgReason { get { return _OrgReason; } set { this._OrgReason = value; } }

        //new update for ops

        private string _SettlementDate = String.Empty;
        public string SettlementDate { get { return _SettlementDate; } set { this._SettlementDate = value; } }

        private string _ORGSettlementContact = String.Empty;
        public string ORGSettlementContact { get { return _ORGSettlementContact; } set { this._ORGSettlementContact = value; } }

        private string _SettlementRequire = String.Empty;
        public string SettlementRequire { get { return _SettlementRequire; } set { this._SettlementRequire = value; } }

        private string _SalaryDay = String.Empty;
        public string SalaryDay { get { return _SalaryDay; } set { this._SalaryDay = value; } }

        private string _PayDay = String.Empty;
        public string PayDay { get { return _PayDay; } set { this._PayDay = value; } }

        private string _ICareList = String.Empty;
        public string ICareList { get { return _ICareList; } set { this._ICareList = value; } }

        private string _PaymentTerm;
        public string PaymentTerm { get { return _PaymentTerm; } set { this._PaymentTerm = value; } }

        public string CityName { get; set; }
        public string ProvinceName { get; set; }

        private string _cashadvance = String.Empty;
        public string Cashadvance { get { return _cashadvance; } set { this._cashadvance = value; } }

        private string _bdLeader = String.Empty;
        public string BDLeader { get { return _bdLeader; } set { this._bdLeader = value; } }


        public string ContractNo { get; set; }
        public string RenewContract { get; set; }
        public int ContractTerm { get; set; }
        public string LatePaymentFee { get; set; }
        public string ContractNote { get; set; }

        public int WeekendSD { get; set; }
        public string SettlementHardCoppy { get; set; }
        public string PaymentBank { get; set; }
        public string SettlementSplit { get; set; }

        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public string EmailContactPerson { get; set; }
        public string OrgRule { get; set; }

        public string MBVRule { get; set; }
        public string Ranking { get; set; }
        public string OrgSector { get; set; }
        public int ActualEmp { get; set; }
        public string AreaName { get; set; }

        public int Register { get; set; }
        public double Coverage { get; set; }
        public string TeleSaleNote { get; set; }
        [Ignore]
        public string NoteByCS { get; set; }
        public string ActualPaymentDate { get; set; }

        //Diem add
        private string _cashloan = String.Empty;
        public string CashLoan { get { return _cashloan; } set { this._cashloan = value; } }

        private string _cf = String.Empty;
        public string CF { get { return _cf; } set { this._cf = value; } }

        private string _BankCollect = String.Empty;
        public string BankCollect { get { return _BankCollect; } set { this._BankCollect = value; } }

        private string _OrgNote = String.Empty;
        public string OrgNote { get { return _OrgNote; } set { this._OrgNote = value; } }
        //End Diem add
        #endregion

        public DW_DC_Organization()
        { }

        public DW_DC_Organization(string OrganizationID, string ShortName, string LongName, string Type, string Group, string Status, string XAccountID, DateTime UsagePeriodStart, DateTime UsagePeriodEnd, DateTime CreatedAt, DateTime ModifiedAt, long DWID, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._organizationid = OrganizationID;
            this._shortname = ShortName;
            this._longname = LongName;
            this._type = Type;
            this._group = Group;
            this._status = Status;
            this._xaccountid = XAccountID;
            this._usageperiodstart = UsagePeriodStart;
            this._usageperiodend = UsagePeriodEnd;
            this._createdat = CreatedAt;
            this._modifiedat = ModifiedAt;
            this._dwid = DWID;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }
        public string ContactGroup { get; set; }
        public static DW_DC_Organization GetDW_DC_Organization(string OrganizationID)
        {
            string PROCEDURE = "select * from vw_OrganizationRaw where OrganizationID='" + OrganizationID + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                Type = row["Type"].ToString(),
                Group = row["Group"].ToString(),
                Status = row["Status"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                //UsagePeriodStart = Convert.ToDateTime(row["UsagePeriodStart"].ToString()),
                //UsagePeriodEnd = Convert.ToDateTime(row["UsagePeriodEnd"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                //RowID = Convert.ToInt32(row["RowID"].ToString()),
                //RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //RowCreatedUser = row["RowCreatedUser"].ToString(),
                //RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                //RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                BusinessDevelopment = row["BD"].ToString(),
                PartnerDevelopment = row["PD"].ToString(),
                BDE = row["BDE"].ToString(),
                ContractStatus = row["ContractStatus"].ToString(),
                Employee = row["Employee"].ToString(),
                TentativeDeadline = DateTime.Parse(row["TentativeDeadline"].ToString()),
                OfficalContractDate = DateTime.Parse(row["OfficialContractDate"].ToString()),
                //MinCredit = Double.Parse(row["MinCredit"].ToString()),
                //MaxCredit = Double.Parse(row["MaxCredit"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                Partner = row["Partner"].ToString(),
                OrgType = row["OrgType"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                OrgReason = row["OrgReason"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                City = row["City"].ToString(),
                Province = row["Province"].ToString(),
                Address = row["OrgAddress"].ToString(),
                //Ranking = row["Name"].ToString(),
                //Ranking = row["Name"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Organization> GetDW_DC_Organizations(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_DC_OrganizationsDynamic";
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
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                Type = row["Type"].ToString(),
                Group = row["Group"].ToString(),
                Status = row["Status"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                UsagePeriodStart = Convert.ToDateTime(row["UsagePeriodStart"].ToString()),
                UsagePeriodEnd = Convert.ToDateTime(row["UsagePeriodEnd"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetAllDW_DC_Organizations()
        {
            string PROCEDURE = "select * from vw_OrganizationRaw";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                Type = row["Type"].ToString(),
                Group = row["Group"].ToString(),
                Status = row["Status"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                //UsagePeriodStart = Convert.ToDateTime(row["UsagePeriodStart"].ToString()),
                //UsagePeriodEnd = Convert.ToDateTime(row["UsagePeriodEnd"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                //RowID = Convert.ToInt32(row["RowID"].ToString()),
                //RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //RowCreatedUser = row["RowCreatedUser"].ToString(),
                //RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                //RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                BusinessDevelopment = row["BD"].ToString(),
                PartnerDevelopment = row["PD"].ToString(),
                BDE = row["BDE"].ToString(),
                ContractStatus = row["ContractStatus"].ToString(),
                Employee = row["Employee"].ToString(),
                TentativeDeadline = DateTime.Parse(row["TentativeDeadline"].ToString()),
                OfficalContractDate = DateTime.Parse(row["OfficialContractDate"].ToString()),
                //MinCredit = Double.Parse(row["MinCredit"].ToString()),
                //MaxCredit = Double.Parse(row["MaxCredit"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                Partner = row["Partner"].ToString(),

                OrgType = row["OrgType"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                OrgReason = row["OrgReason"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                City = row["City"].ToString(),
                Province = row["Province"].ToString(),
                Address = row["OrgAddress"].ToString(),
                Cashadvance = row["Cashadvance"].ToString(),

            }).ToList();
        }
        public static DW_DC_Organization CheckExistOrg(string OrganizationID)
        {
            string PROCEDURE = "select OrganizationID from DW_DC_Organization where OrganizationID = @OrganizationID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList().FirstOrDefault();
        }
        public static List<DW_DC_Organization> GetOrganizationIDDW_DC_Organizations()
        {
            string PROCEDURE = "select OrganizationID,ShortName from DW_DC_Organization";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetListOrgByArea(string userId)
        {
            string PROCEDURE = "SELECT o.OrganizationID FROM DC_MOP_Area_HumanResource h LEFT JOIN DC_MOP_Area_Org o ON o.AreaID =h.AreaID WHERE h.UserID = '" + userId + "' AND RoleID IN ('UR008','UR027','UR026','UR007','UR013')";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetListOrgAllByArea(string userId)
        {
            string PROCEDURE = "SELECT o.OrganizationID FROM DC_MOP_Area_HumanResource h LEFT JOIN DC_MOP_Area_Org o ON o.AreaID =h.AreaID WHERE h.UserID = '" + userId + "' AND RoleID IN ('UR028','UR009')";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString()
            }).ToList();
        }

        public static string GetBD(string OrganizationID)
        {
            string PROCEDURE = "select Value  from DC_Organization_Meta where Factor = 'BD' and OrganizationID = N'" + OrganizationID.Replace("'", "''") + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }
        public static List<DW_DC_Organization> GetAllDW_DC_Organizations_OPS()
        {
            string PROCEDURE = "select * from vw_OrganizationRawOPS";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                FakeOrganizationID = Helpers.Locdau.convertToUnSign3(row["OrganizationID"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                Type = row["Type"].ToString(),
                Group = row["Group"].ToString(),
                Status = row["Status"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                //UsagePeriodStart = Convert.ToDateTime(row["UsagePeriodStart"].ToString()),
                //UsagePeriodEnd = Convert.ToDateTime(row["UsagePeriodEnd"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                //RowID = Convert.ToInt32(row["RowID"].ToString()),
                //RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //RowCreatedUser = row["RowCreatedUser"].ToString(),
                //RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                //RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                BusinessDevelopment = row["BD"].ToString(),
                PartnerDevelopment = row["PD"].ToString(),
                BDE = row["BDE"].ToString(),
                ContractStatus = row["ContractStatus"].ToString(),
                Employee = row["Employee"].ToString(),
                TentativeDeadline = DateTime.Parse(row["TentativeDeadline"].ToString()),
                OfficalContractDate = DateTime.Parse(row["OfficialContractDate"].ToString()),
                ContractDate = DateTime.Parse(row["ContractDate"].ToString()),
                //MinCredit = Double.Parse(row["MinCredit"].ToString()),
                //MaxCredit = Double.Parse(row["MaxCredit"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                Partner = row["Partner"].ToString(),
                OrgType = row["OrgType"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                OrgReason = row["OrgReason"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                City = row["City"].ToString(),
                Province = row["Province"].ToString(),
                Address = row["OrgAddress"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                PaymentTerm = row["PaymentTerm"].ToString(),
                ORGSettlementContact = row["ORGSettlementContact"].ToString(),
                SettlementRequire = row["SettlementRequire"].ToString(),
                PayDay = row["PayDay"].ToString(),
                SalaryDay = row["SalaryDay"].ToString(),
                ICareList = row["ICareList"].ToString(),
                CityName = row["CityName"].ToString(),
                ProvinceName = row["ProvinceName"].ToString(),
                Cashadvance = row["Cashadvance"].ToString(),
                BDLeader = row["BDLeader"].ToString(),
                countFile = CountFile(row["OrganizationID"].ToString()),
                ContractNo = row["ContractNo"].ToString(),
                RenewContract = row["RenewContract"].ToString(),
                ContractTerm = Convert.ToInt32(row["ContractTerm"].ToString()),
                LatePaymentFee = row["LatePaymentFee"].ToString(),
                ContractNote = row["ContractNote"].ToString(),
                SettlementHardCoppy = row["SettlementHardCoppy"].ToString(),
                PaymentBank = row["PaymentBank"].ToString(),
                SettlementSplit = row["SettlementSplit"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                EmailTo = row["EmailTo"].ToString(),
                EmailCc = row["EmailCc"].ToString(),
                EmailContactPerson = row["EmailContactPerson"].ToString(),
                OrgRule = row["OrgRule"].ToString(),
                Ranking = row["Name"].ToString(),
                FixedSDate = row["FixedSDate"].ToString(),
                Region = row["Region"].ToString(),
                Country = row["Country"].ToString(),
                Alias = row["Alias"].ToString(),
                OrgSector = row["OrgSector"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString()),
                AreaName = row["AreaName"].ToString(),
                CollectionNotes = row["CollectionNotes"].ToString(),
                SBank = row["SBank"].ToString(),
                CustomerNum = int.Parse(row["Customer#"].ToString()),
                MOPAreaID = row["MOPAreaID"].ToString(),
                MOPBranchName = row["MOPBranchName"].ToString(),
                MOPRegionName = row["MOPRegionName"].ToString(),
                PaymentType = row["PaymentType"].ToString(),
                Register = int.Parse(row["Register"].ToString()),
                Coverage = double.Parse(row["Coverage"].ToString()),
                ContactGroup = row["ContactGroup"].ToString(),
                AnnouncementDays = row["AnnouncementDays"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                TemplateName = row["TemplateName"].ToString(),
                EmployeeType = row["EmployeeType"].ToString(),
                SupportType = row["SupportType"].ToString(),
                CollectionType = row["CollectionType"].ToString(),
                OnsiteRate = row["OnsiteRate"].ToString(),
                OrgCustomerType = row["OrgCustomerType"].ToString(),
                HadEmployeeList = Convert.ToBoolean(row["HadEmployeeList"].ToString()),
                RBDPhone = row["RBDPhone"].ToString(),
                RBDEmail = row["RBDEmail"].ToString(),
                ActualPaymentDate = row["ActualPaymentDate"].ToString(),
                CashLoan = row["CashLoan"].ToString(),
                CF = row["CF"].ToString(),
                BankCollect = row["BankCollect"].ToString(),
                OrgNote = row["OrgNote"].ToString(),
            }).ToList();
        }

        public static async Task<DataSourceResult> GetAllDW_DC_Organizations_OPS([DataSourceRequest] DataSourceRequest request, string All, string where)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_DW_DC_Organization_GetAllRaw";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = where;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            if (All == "All")
            {
                parameters[i].Value = "1=1";
            }
            else
            {
                parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            }


            var data = new DataSourceResult();

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            data.Data = ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Organization
            {
                FakeOrganizationID = Helpers.Locdau.convertToUnSign3(row["OrganizationID"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                Type = row["Type"].ToString(),
                Group = row["Group"].ToString(),
                Status = row["Status"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                BusinessDevelopment = row["BD"].ToString(),
                PartnerDevelopment = row["PD"].ToString(),
                BDE = row["BDE"].ToString(),
                ContractStatus = row["ContractStatus"].ToString(),
                Employee = row["Employee"].ToString(),
                TentativeDeadline = DateTime.Parse(row["TentativeDeadline"].ToString()),
                OfficalContractDate = DateTime.Parse(row["OfficialContractDate"].ToString()),
                ContractDate = DateTime.Parse(row["ContractDate"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                Partner = row["Partner"].ToString(),
                OrgType = row["OrgType"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                OrgReason = row["OrgReason"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                City = row["City"].ToString(),
                Province = row["Province"].ToString(),
                Address = row["OrgAddress"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                PaymentTerm = row["PaymentTerm"].ToString(),
                ORGSettlementContact = row["ORGSettlementContact"].ToString(),
                SettlementRequire = row["SettlementRequire"].ToString(),
                PayDay = row["PayDay"].ToString(),
                SalaryDay = row["SalaryDay"].ToString(),
                ICareList = row["ICareList"].ToString(),
                CityName = row["CityName"].ToString(),
                ProvinceName = row["ProvinceName"].ToString(),
                Cashadvance = row["Cashadvance"].ToString(),
                BDLeader = row["BDLeader"].ToString(),
                countFile = CountFile(row["OrganizationID"].ToString()),
                ContractNo = row["ContractNo"].ToString(),
                RenewContract = row["RenewContract"].ToString(),
                ContractTerm = Convert.ToInt32(row["ContractTerm"].ToString()),
                LatePaymentFee = row["LatePaymentFee"].ToString(),
                ContractNote = row["ContractNote"].ToString(),
                SettlementHardCoppy = row["SettlementHardCoppy"].ToString(),
                PaymentBank = row["PaymentBank"].ToString(),
                SettlementSplit = row["SettlementSplit"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                EmailTo = row["EmailTo"].ToString(),
                EmailCc = row["EmailCc"].ToString(),
                EmailContactPerson = row["EmailContactPerson"].ToString(),
                OrgRule = row["OrgRule"].ToString(),
                Ranking = row["Name"].ToString(),
                FixedSDate = row["FixedSDate"].ToString(),
                Region = row["Region"].ToString(),
                Country = row["Country"].ToString(),
                Alias = row["Alias"].ToString(),
                OrgSector = row["OrgSector"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString()),
                AreaName = row["AreaName"].ToString(),
                CollectionNotes = row["CollectionNotes"].ToString(),
                SBank = row["SBank"].ToString(),
                //CustomerNum = int.Parse(row["Customer#"].ToString()),
                MOPAreaID = row["MOPAreaID"].ToString(),
                MOPBranchName = row["MOPBranchName"].ToString(),
                MOPRegionName = row["MOPRegionName"].ToString(),
                PaymentType = row["PaymentType"].ToString(),
                Register = int.Parse(row["Register"].ToString()),
                Coverage = double.Parse(row["Coverage"].ToString()),
                ContactGroup = row["ContactGroup"].ToString(),
                TemplateName = row["TemplateName"].ToString(),
                AnnouncementDays = row["AnnouncementDays"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                EmployeeType = row["EmployeeType"].ToString(),
                SupportType = row["SupportType"].ToString(),
                CollectionType = row["CollectionType"].ToString(),
                OnsiteRate = row["OnsiteRate"].ToString(),
                OrgCustomerType = row["OrgCustomerType"].ToString(),
                HadEmployeeList = Convert.ToBoolean(row["HadEmployeeList"].ToString()),
                PDSupport = row["PDSupport"].ToString(),
                RBDPhone = row["RBDPhone"].ToString(),
                RBDEmail = row["RBDEmail"].ToString(),
                ActualPaymentDate = row["ActualPaymentDate"].ToString(),
                CashLoan = row["CashLoan"].ToString(),
                CF = row["CF"].ToString(),
                BankCollect = row["BankCollect"].ToString(),
                OrgNote = row["OrgNote"].ToString(),
            }).ToList();

            var total = ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Organization
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }
        public string PDSupport { get; set; }
        public static async Task<DataSourceResult> GetAllDW_DC_Organizations_OPSbyUser([DataSourceRequest] DataSourceRequest request, string where)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_DW_DC_Organization_GetAllRawByUser";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = where;

            var data = new DataSourceResult();
            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            data.Data = ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Organization
            {
                FakeOrganizationID = Helpers.Locdau.convertToUnSign3(row["OrganizationID"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                Type = row["Type"].ToString(),
                Group = row["Group"].ToString(),
                Status = row["Status"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                BusinessDevelopment = row["BD"].ToString(),
                PartnerDevelopment = row["PD"].ToString(),
                BDE = row["BDE"].ToString(),
                ContractStatus = row["ContractStatus"].ToString(),
                Employee = row["Employee"].ToString(),
                TentativeDeadline = DateTime.Parse(row["TentativeDeadline"].ToString()),
                OfficalContractDate = DateTime.Parse(row["OfficialContractDate"].ToString()),
                ContractDate = DateTime.Parse(row["ContractDate"].ToString()),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                Partner = row["Partner"].ToString(),
                OrgType = row["OrgType"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                OrgReason = row["OrgReason"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                City = row["City"].ToString(),
                Province = row["Province"].ToString(),
                Address = row["OrgAddress"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                PaymentTerm = row["PaymentTerm"].ToString(),
                ORGSettlementContact = row["ORGSettlementContact"].ToString(),
                SettlementRequire = row["SettlementRequire"].ToString(),
                PayDay = row["PayDay"].ToString(),
                SalaryDay = row["SalaryDay"].ToString(),
                ICareList = row["ICareList"].ToString(),
                CityName = row["CityName"].ToString(),
                ProvinceName = row["ProvinceName"].ToString(),
                Cashadvance = row["Cashadvance"].ToString(),
                BDLeader = row["BDLeader"].ToString(),
                countFile = CountFile(row["OrganizationID"].ToString()),
                ContractNo = row["ContractNo"].ToString(),
                RenewContract = row["RenewContract"].ToString(),
                ContractTerm = Convert.ToInt32(row["ContractTerm"].ToString()),
                LatePaymentFee = row["LatePaymentFee"].ToString(),
                ContractNote = row["ContractNote"].ToString(),
                SettlementHardCoppy = row["SettlementHardCoppy"].ToString(),
                PaymentBank = row["PaymentBank"].ToString(),
                SettlementSplit = row["SettlementSplit"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                EmailTo = row["EmailTo"].ToString(),
                EmailCc = row["EmailCc"].ToString(),
                EmailContactPerson = row["EmailContactPerson"].ToString(),
                OrgRule = row["OrgRule"].ToString(),
                Ranking = row["Name"].ToString(),
                FixedSDate = row["FixedSDate"].ToString(),
                Region = row["Region"].ToString(),
                Country = row["Country"].ToString(),
                Alias = row["Alias"].ToString(),
                OrgSector = row["OrgSector"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString()),
                AreaName = row["AreaName"].ToString(),
                CollectionNotes = row["CollectionNotes"].ToString(),
                SBank = row["SBank"].ToString(),
                CustomerNum = int.Parse(row["Customer#"].ToString()),
                MOPAreaID = row["MOPAreaID"].ToString(),
                MOPBranchName = row["MOPBranchName"].ToString(),
                MOPRegionName = row["MOPRegionName"].ToString(),
                PaymentType = row["PaymentType"].ToString(),
                Register = int.Parse(row["Register"].ToString()),
                Coverage = double.Parse(row["Coverage"].ToString()),
                ContactGroup = row["ContactGroup"].ToString(),
                TemplateName = row["TemplateName"].ToString(),
                AnnouncementDays = row["AnnouncementDays"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                EmployeeType = row["EmployeeType"].ToString(),
                SupportType = row["SupportType"].ToString(),
                CollectionType = row["CollectionType"].ToString(),
                OnsiteRate = row["OnsiteRate"].ToString(),
                OrgCustomerType = row["OrgCustomerType"].ToString(),
                HadEmployeeList = Convert.ToBoolean(row["HadEmployeeList"].ToString()),
                PDSupport = row["PDSupport"].ToString(),
                RBDPhone = row["RBDPhone"].ToString(),
                RBDEmail = row["RBDEmail"].ToString(),
                ActualPaymentDate = row["ActualPaymentDate"].ToString(),
                CashLoan = row["CashLoan"].ToString(),
                CF = row["CF"].ToString(),
                BankCollect = row["BankCollect"].ToString(),
                OrgNote = row["OrgNote"].ToString(),
            }).ToList();

            var total = ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Organization
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }


        public int TotalRows { get; set; }
        public string TemplateName { get; set; }
        public int CustomerNum { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Alias { get; set; }

        public string FixedSDate { get; set; }
        public int countFile { get; set; }
        public static int CountFile(string id)
        {
            try
            {
                string pathForSaving = HttpContext.Current.Server.MapPath("~/OrgUploadFile/" + id.Replace(":", "-"));
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

        public static List<DW_DC_Organization> GetListBD()
        {
            //string PROCEDURE = "SELECT DISTINCT BD FROM vw_OrganizationRaw WHERE BD <> ''";
            // Modify CanhLV Query 31/07/2014
            string PROCEDURE = "SELECT Description FROM DC_Partner_PD_Definition WHERE Type ='BD' and Active = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                BusinessDevelopment = row["Description"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetListPD()
        {
            //string PROCEDURE = "SELECT DISTINCT PD FROM vw_OrganizationRaw WHERE PD <> ''";
            // Modify CanhLV Query 31/07/2014
            string PROCEDURE = "SELECT PartnerID,Description FROM DC_Partner_PD_Definition WHERE Type ='PD' and Active = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                PartnerID = row["PartnerID"].ToString(),
                PartnerDevelopment = row["Description"].ToString()
            }).ToList();
        }

        public static string getPartnerById(string Id)
        {
            string text = "SELECT Description FROM DC_Partner_PD_Definition WHERE PartnerID ='" + Id + "'";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, text, null);
            var data = dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                PartnerDevelopment = row["Description"].ToString()
            }).FirstOrDefault();
            return data != null ? data.PartnerDevelopment : "";
        }



        public static List<DW_DC_Organization> GetListPartner()
        {
            //string PROCEDURE = "SELECT DISTINCT Partner FROM vw_OrganizationRaw WHERE Partner <> ''";
            // Modify CanhLV Query 31/07/2014
            string PROCEDURE = "SELECT Description FROM DC_Partner_PD_Definition WHERE Type ='Partner' and Active = 1 ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                Partner = row["Description"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetListBDE()
        {
            string PROCEDURE = "SELECT DISTINCT BDE FROM vw_OrganizationRaw WHERE BDE <> ''";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                BDE = row["BDE"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetListOrganizationID()
        {
            string PROCEDURE = "SELECT  OrganizationID, OrgStatus FROM vw_OrganizationRaw";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
            }).ToList();
        }

        public static List<DW_DC_Organization> GetOnlyListOrganizationID()
        {
            string PROCEDURE = "SELECT DISTINCT  OrganizationID FROM DW_DC_Organization";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }
        public static List<DW_DC_Organization> GetListCity()
        {
            string PROCEDURE = "SELECT DISTINCT City FROM vw_OrganizationRaw WHERE City <> ''";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                City = row["City"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetListProvince()
        {
            string PROCEDURE = "SELECT DISTINCT Province FROM vw_OrganizationRaw WHERE Province <> ''";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                Province = row["Province"].ToString()
            }).ToList();
        }
        private string _totalCustomer;
        public string TotalCustomer
        {
            get
            {
                return this._totalCustomer;
            }
            set
            {
                this._totalCustomer = value;
            }
        }
        private int _todayUnassigned;
        public int TodayUnassigned
        {
            get
            {
                return this._todayUnassigned;
            }
            set
            {
                this._todayUnassigned = value;
            }
        }
        private string _keyOrganizationID;
        public string KeyOrganizationID
        {
            get
            {
                var data = Locdau.convertToUnSign3(OrganizationID);
                return data;
            }
            set
            {
                this._keyOrganizationID = value;
            }
        }

        private int _maxAssigned;
        public int MaxAssigned
        {
            get
            {
                return this._maxAssigned;
            }
            set
            {
                this._maxAssigned = value;
            }
        }

        //list status
        private int _aStatus;
        public int AStatus
        {
            get
            {
                return this._aStatus;
            }
            set
            {
                this._aStatus = value;
            }
        }
        private int _nAStatus;
        public int NAStatus
        {
            get
            {
                return this._nAStatus;
            }
            set
            {
                this._nAStatus = value;
            }
        }
        private int _bStatus;
        public int BStatus
        {
            get
            {
                return this._bStatus;
            }
            set
            {
                this._bStatus = value;
            }
        }
        private int _tStatus;
        public int TStatus
        {
            get
            {
                return this._tStatus;
            }
            set
            {
                this._tStatus = value;
            }
        }


        public static List<DW_DC_Organization> getAllList()
        {
            string PROCEDURE = "p_DC_getOrgListForAssignment";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                KeyOrganizationID = Locdau.convertToUnSign3(row["OrganizationID"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                TotalCustomer = row["TotalCustomer"].ToString(),
                TodayUnassigned = Int32.Parse(row["TodayUnAssigned"].ToString()),
                MaxAssigned = Int32.Parse(row["TotalCustomer"].ToString().Split('(')[0]),
                AStatus = Int32.Parse(row["AStatus"].ToString()),
                NAStatus = Int32.Parse(row["NAStatus"].ToString()),
                BStatus = Int32.Parse(row["BStatus"].ToString()),
                TStatus = Int32.Parse(row["TStatus"].ToString())
            }).ToList();
        }

        public static List<DW_DC_Organization> getAllOrgListDynamic(string whereCondition)
        {
            string PROCEDURE = "p_DC_getOrgListForAssignmentDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@whereCondition";
            parameters[i].Value = whereCondition;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                KeyOrganizationID = Locdau.convertToUnSign3(row["OrganizationID"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                ShortName = row["ShortName"].ToString(),
                LongName = row["LongName"].ToString(),
                TotalCustomer = row["TotalCustomer"].ToString(),
                TodayUnassigned = Int32.Parse(row["TodayUnAssigned"].ToString()),
                MaxAssigned = Int32.Parse(row["TotalCustomer"].ToString().Split('(')[0]),
                AStatus = Int32.Parse(row["AStatus"].ToString()),
                NAStatus = Int32.Parse(row["NAStatus"].ToString()),
                BStatus = Int32.Parse(row["BStatus"].ToString()),
                TStatus = Int32.Parse(row["TStatus"].ToString())
            }).ToList();
        }

        public static List<DW_DC_Organization> getLisOrgApplyForAgent(string agentID)
        {
            string PROCEDURE = "p_getLisOrgApplyForAgent";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = agentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                BD = row["BD"].ToString(),
                BDE = row["BDE"].ToString(),
            }).ToList();
        }

        public string BD { get; set; }
        public static List<DW_DC_Organization> GetListOrgForProjectManage(string ProjectID)
        {
            string PROCEDURE = "p_SelectDC_Project_ListOrg";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ProjectID";
            parameters[i].Value = ProjectID;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                BusinessDevelopment = row["BDTeam"].ToString(),
                BDE = row["BDE"].ToString(),
            }).ToList();
        }

        //DungNT: create function get Infomation for teleSale
        public string Branch { get; set; }
        public string DueDate { get; set; }
        public string AvoidCallTime { get; set; }
        public string AllowService { get; set; }
        public bool AllowCash2Home { get; set; }
        public bool AllowServices { get; set; }
        public bool AllowAirTime { get; set; }
        public bool AllowPhysicalGoods { get; set; }
        public string CollectionType { get; set; }
        public string Place { get; set; }
        public string RegionalBD { get; set; }
        public string OnsiteInfo { get; set; }
        public string KeyPerson { get; set; }
        public int SalesPriority { get; set; }
        public string CheckAllowServices { get; set; }
        public static List<DW_DC_Organization> GetListOrganizationForTeleSale()
        {
            string PROCEDURE = "p_Select_DC_Organization_GetInfomationForTeleSale";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                Address = row["Address"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                DueDate = row["DueDate"].ToString(),
                AvoidCallTime = row["AvoidCallTime"].ToString(),
                AllowService = row["AllowService"].ToString(),
                CollectionType = row["CollectionType"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                OnsiteInfo = row["OnsiteInfo"].ToString(),
                Note = row["OtherNote"].ToString(),
                KeyPerson = row["KeyPerson"].ToString(),
                Branch = row["Branch"].ToString(),
                AllowAirTime = bool.Parse(row["AllowAirTime"].ToString()),
                AllowCash2Home = bool.Parse(row["AllowCash2Home"].ToString()),
                AllowServices = bool.Parse(row["AllowServices"].ToString()),
                AllowPhysicalGoods = bool.Parse(row["AllowPhysicalGoods"].ToString()),
                TeleSaleNote = row["TeleSaleNote"].ToString(),
                SalesPriority = int.Parse(row["SalesPriority"].ToString()),
                CheckAllowServices = row["CheckAllowServices"].ToString()
            }).ToList();
        }

        public static DW_DC_Organization GetListOrganizationForTeleSaleByOrgID(string OrganizationID)
        {
            string PROCEDURE = "p_Select_DC_Organization_GetInfomationForTeleSaleByOrgID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                Address = row["Address"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                DueDate = row["DueDate"].ToString(),
                AvoidCallTime = row["AvoidCallTime"].ToString(),
                AllowService = row["AllowService"].ToString(),
                CollectionType = row["CollectionType"].ToString(),
                DeliveryNote = row["DeliveryNote"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                OnsiteInfo = row["OnsiteInfo"].ToString(),
                Note = row["OtherNote"].ToString(),
                KeyPerson = row["KeyPerson"].ToString(),
                Branch = row["Branch"].ToString(),
                AllowAirTime = bool.Parse(row["AllowAirTime"].ToString()),
                AllowCash2Home = bool.Parse(row["AllowCash2Home"].ToString()),
                AllowServices = bool.Parse(row["AllowServices"].ToString()),
                AllowPhysicalGoods = bool.Parse(row["AllowPhysicalGoods"].ToString()),
                TeleSaleNote = row["TeleSaleNote"].ToString(),
                OrgRule = row["OrgRule"].ToString(),
                Cashadvance = row["Cashadvance"].ToString()
            }).FirstOrDefault();
        }

        public static DW_DC_Organization GetListOrganizationForCSByOrgID(string OrganizationID)
        {
            string PROCEDURE = "p_SelectDC_CS_Organization_Info";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                Address = row["Address"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                AllowAirTime = bool.Parse(row["AllowAirTime"].ToString()),
                AllowCash2Home = bool.Parse(row["AllowCash2Home"].ToString()),
                AllowServices = bool.Parse(row["AllowServices"].ToString()),
                AllowPhysicalGoods = bool.Parse(row["AllowPhysicalGoods"].ToString()),
                AreaName = row["AreaName"].ToString(),
                RegionName = row["RegionName"].ToString(),
                BranchName = row["BranchName"].ToString(),
                Registration = row["Registration"].ToString(),
                PaymentType = row["PaymentType"].ToString(),
                OnsiteSchedule = row["OnsiteSchedule"].ToString(),
                RegionalBDEmail = row["RegionalBDEmail"].ToString(),
                RegionalBDPhone = row["RegionalBDPhone"].ToString(),
                PD = row["PD"].ToString(),
                PDEmail = row["PDEmail"].ToString(),
                PDPhone = row["PDPhone"].ToString(),
                IcareCenter = row["IcareCenter"].ToString(),
                IcareAddress = row["IcareAddress"].ToString(),
                IcareMainContact = row["IcareMainContact"].ToString(),
                IcarePhone = row["IcarePhone"].ToString(),
                ContractStauts = row["ContractStauts"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                NoteByCS = row["NoteByCS"].ToString(),
                OrgRule = row["OrgRule"].ToString(),
                Cashadvance = row["Cashadvance"].ToString()
            }).FirstOrDefault();
        }
        public string ContractStauts { get; set; }
        public string RegionName { get; set; }
        public string BranchName { get; set; }
        public string Registration { get; set; }
        public string DebtCollection { get; set; }
        public string OnsiteSchedule { get; set; }
        public string RegionalBDEmail { get; set; }
        public string RegionalBDPhone { get; set; }
        public string PD { get; set; }
        public string PDPhone { get; set; }
        public string PDEmail { get; set; }
        public string IcareCenter { get; set; }
        public string IcareAddress { get; set; }
        public string IcareMainContact { get; set; }
        public string IcarePhone { get; set; }

        public static DW_DC_Organization GetSettlementTemplate(string OrganizationID, string PeriodID, string prePeriodID)
        {
            string PROCEDURE = "p_SelectSettlementTemplate";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PeriodID";
            parameters[i].Value = PeriodID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PrePeriodID";
            parameters[i].Value = prePeriodID;
            //i++;
            //parameters[i] = new SqlParameter();
            //parameters[i].ParameterName = "@PrePeriodID2";
            //parameters[i].Value = prePeriodID2;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                Address = row["Address"].ToString(),
                SettlementDate = row["SettlementDate"].ToString(),
                //SettlementAmount = row["SettlementAmount"].ToString(),
                //CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                PaymentDate = Convert.ToDateTime(row["PaymentDate"].ToString()),
                PreSettlementDate = Convert.ToDateTime(row["PreSettlementDate"].ToString()),
                SBank = row["SBank"].ToString(),
                PreRemain = double.Parse(row["PreRemain"].ToString()),
            }).FirstOrDefault();
        }
        public DateTime PaymentDate { get; set; }
        public double PreRemain { get; set; }

        public DateTime PreSettlementDate { get; set; }
        public static List<DW_DC_Organization> GetSettlementTemplateDetail(string OrganizationID, string PeriodID, string prePeriodID)
        {
            string PROCEDURE = "p_SelectSettlementTemplateDetail";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PeriodID";
            parameters[i].Value = PeriodID;

            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PrePeriodID";
            parameters[i].Value = prePeriodID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                CustomerID = row["CustomerID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Department = row["Department"].ToString(),
                DateOfBirth = row["DateOfBirth"].ToString(),
                SettlementAmount = row["SettlementAmount"].ToString(),

            }).ToList();
        }
        public static DataTable GetSettlementTemplateDetailDataTable(string OrganizationID, string PeriodID, string prePeriodID)
        {
            string PROCEDURE = "p_SelectSettlementTemplateDetail";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PeriodID";
            parameters[i].Value = PeriodID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PrePeriodID";
            parameters[i].Value = prePeriodID;

            return SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhysicalID { get; set; }
        public string MobilePhone { get; set; }
        public string Department { get; set; }
        public string DateOfBirth { get; set; }
        public string EmployeeID { get; set; }
        public string SBank { get; set; }
        public string SettlementAmount { get; set; }
        public string CollectionNotes { get; set; }

        public string MOPAreaID { get; set; }

        public string MOPBranchName { get; set; }

        public string MOPRegionName { get; set; }

        public string PaymentType { get; set; }

        public string PartnerID { get; set; }

        public string FakeOrganizationID { get; set; }

        public string AnnouncementDays { get; set; }

        public string RegionalOPS { get; set; }

        public string EmployeeType { get; set; }

        public bool HadEmployeeList { get; set; }

        public string OrgCustomerType { get; set; }

        public string OnsiteRate { get; set; }

        public string SupportType { get; set; }

        public string Email { get; set; }

        public string RBDPhone { get; set; }

        public string RBDEmail { get; set; }

        //DungNT
        public static DW_DC_Organization DC_OrganizationRuleCredit_GetByOrganizationID(string OrganizationID)
        {
            string PROCEDURE = "p_DC_OrganizationRuleCredit_GetByOrganizationID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).FirstOrDefault();
        }

        public static List<DW_DC_Organization> CheckOrgCharectic(string OrganizationID, string UserID)
        {
            string PROCEDURE = "select A.UserID,A.AreaID,B.OrganizationID from DC_MOP_Area_RBD A";
            PROCEDURE += " left join DC_MOP_Area_Org B ON A.AreaID = B.AreaID";
            PROCEDURE += " where A.UserID = @UserID AND B.OrganizationID = @OrganizationID";

            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static List<DW_DC_Organization> GetOrgForCommissonOrg()
        {
            string PROCEDURE = "SELECT  org.OrganizationID ,org.LongName ,ISNULL(mt.Value,'') AS ContractStatus FROM DW_DC_Organization org ";
            PROCEDURE += "    LEFT JOIN DC_Organization_Meta mt ON org.OrganizationID = mt.OrganizationID AND Factor = 'contract status'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                LongName = row["LongName"].ToString(),
                ContractStatus = row["ContractStatus"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Organization> GetDC_MOP_Org_ActiveUser_GetInformation()
        {
            string PROCEDURE = "p_DC_MOP_Org_ActiveUser_GetInformation";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Organization
            {
                OrganizationID = row["OrganizationID"].ToString(),
                ActiveUser = int.Parse(row["ActiveUser"].ToString()),
                UserActivated = int.Parse(row["UserActivated"].ToString()),
                UserNotActivated = int.Parse(row["UserNotActivated"].ToString()),
                UserBlocked = int.Parse(row["UserBlocked"].ToString()),
                UserTerminated = int.Parse(row["UserTerminated"].ToString()),
                Register = int.Parse(row["Register"].ToString()),
                MRegister = int.Parse(row["MRegister"].ToString()),
                Total = int.Parse(row["Total"].ToString()),
                AreaID = row["AreaID"].ToString(),
                AreaName = row["AreaName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
                BranchID = row["BranchID"].ToString(),
                BranchName = row["BranchName"].ToString(),
            }).ToList();
        }
        public int ActiveUser { get; set; }
        public int UserActivated { get; set; }
        public int UserNotActivated { get; set; }
        public int UserBlocked { get; set; }
        public int UserTerminated { get; set; }
        public int MRegister { get; set; }
        public int Total { get; set; }
        public string AreaID { get; set; }
        public string RegionID { get; set; }
        public string BranchID { get; set; }
    }
}