using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Company.
    /// </summary>
    public class DC_Company
    {
        #region Members
        private string _companyid = String.Empty;
        public string CompanyID { get { return _companyid; } set { this._companyid = value; } }

        private string _companyname = String.Empty;
        public string CompanyName { get { return _companyname; } set { this._companyname = value; } }

        private string _companytypeid = String.Empty;
        public string CompanyTypeID { get { return _companytypeid; } set { this._companytypeid = value; } }

        private string _scalesid = String.Empty;
        public string ScalesID { get { return _scalesid; } set { this._scalesid = value; } }

        private string _companyphone = String.Empty;
        public string CompanyPhone { get { return _companyphone; } set { this._companyphone = value; } }

        private string _fax = String.Empty;
        public string Fax { get { return _fax; } set { this._fax = value; } }

        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _website = String.Empty;
        public string Website { get { return _website; } set { this._website = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }

        private string _cityid = String.Empty;
        public string CityID { get { return _cityid; } set { this._cityid = value; } }

        private int _noofemployee;
        public int NoOfEmployee { get { return _noofemployee; } set { this._noofemployee = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }

        private int _probability;
        public int Probability { get { return _probability; } set { this._probability = value; } }

        private string _stage = String.Empty;
        public string Stage { get { return _stage; } set { this._stage = value; } }

        private string _nextstep = String.Empty;
        public string NextStep { get { return _nextstep; } set { this._nextstep = value; } }

        private DateTime _expectedcloseddate;
        public DateTime ExpectedClosedDate { get { return _expectedcloseddate; } set { this._expectedcloseddate = value; } }

        private DateTime _actualcloseddate;
        public DateTime ActualClosedDate { get { return _actualcloseddate; } set { this._actualcloseddate = value; } }

        private string _partnerpdid = String.Empty;
        public string PartnerPDID { get { return _partnerpdid; } set { this._partnerpdid = value; } }

        private string _organizationid = String.Empty;
        public string OrganizationID { get { return _organizationid; } set { this._organizationid = value; } }

        private DateTime _contractdate;
        public DateTime ContractDate { get { return _contractdate; } set { this._contractdate = value; } }

        private int _rankingid;
        public int RankingID { get { return _rankingid; } set { this._rankingid = value; } }

        private string _note = String.Empty;
        public string Note { get { return _note; } set { this._note = value; } }

        private string _xmlstring = String.Empty;
        public string XMLString { get { return _xmlstring; } set { this._xmlstring = value; } }

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
        public string AreaID { get; set; }

        private string _leadby = String.Empty;
        public string LeadBy { get { return _leadby; } set { this._leadby = value; } }

        private string _bankid = String.Empty;
        public string BankID { get { return _bankid; } set { this._bankid = value; } }

        private string _buyerid = String.Empty;
        public string BuyerID { get { return _buyerid; } set { this._buyerid = value; } }

        private string _salaryday = String.Empty;
        public string SalaryDay { get { return _salaryday; } set { this._salaryday = value; } }

        private string _gift = String.Empty;
        public string Gift { get { return _gift; } set { this._gift = value; } }

        private DateTime _giftdate;
        public DateTime GiftDate { get { return _giftdate; } set { this._giftdate = value; } }


        private string _giftamount = String.Empty;
        public string GiftAmount { get { return _giftamount; } set { this._giftamount = value; } }

        private DateTime _approachdate;
        public DateTime ApproachDate { get { return _approachdate; } set { this._approachdate = value; } }

        public string NextStepStr { get; set; }

        private string _onsiteschedule = String.Empty;
        public string OnsiteSchedule { get { return _onsiteschedule; } set { this._onsiteschedule = value; } }

        private string _partnerid = String.Empty;
        public string PartnerID { get { return _partnerid; } set { this._partnerid = value; } }

        private int _actualemp;
        public int ActualEmp { get { return _actualemp; } set { this._actualemp = value; } }

        public string ContactToName { get; set; }
        public string ContactToID { get; set; }
        public string PartnerName { get; set; }
        public string CityName { get; set; }
        public string TypeName { get; set; }
        public string ScalesName { get; set; }
        public string PDName { get; set; }
        public string PDSupport { get; set; }
        #endregion

        public DC_Company()
        {
            this.AreaID = "";
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Company";
            SqlParameter[] parameters = new SqlParameter[39];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyID";
            parameters[i].Value = this._companyid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyName";
            parameters[i].Value = this._companyname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyTypeID";
            parameters[i].Value = this._companytypeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ScalesID";
            parameters[i].Value = this._scalesid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyPhone";
            parameters[i].Value = this._companyphone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Website";
            parameters[i].Value = this._website;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CityID";
            parameters[i].Value = this._cityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LeadBy";
            parameters[i].Value = this._leadby;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@NoOfEmployee";
            parameters[i].Value = this._noofemployee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Probability";
            parameters[i].Value = this._probability;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Stage";
            parameters[i].Value = this._stage;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ExpectedClosedDate";
            parameters[i].Value = this._expectedcloseddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualClosedDate";
            parameters[i].Value = this._actualcloseddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PartnerPDID";
            parameters[i].Value = this._partnerpdid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContractDate";
            parameters[i].Value = this._contractdate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RankingID";
            parameters[i].Value = this._rankingid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = this._xmlstring;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AreaID";
            parameters[i].Value = this.AreaID;
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
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BuyerID";
            parameters[i].Value = this._buyerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Gift";
            parameters[i].Value = this._gift;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@GiftAmount";
            parameters[i].Value = this._giftamount;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@GiftDate";
            parameters[i].Value = this._giftdate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SalaryDay";
            parameters[i].Value = this._salaryday;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ApproachDate";
            parameters[i].Value = this._approachdate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Event";
            parameters[i].Value = Event;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MainContact";
            parameters[i].Value = MainContact;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OnsiteSchedule";
            parameters[i].Value = this._onsiteschedule;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualEmp";
            parameters[i].Value = this._actualemp;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PartnerID";
            parameters[i].Value = this._partnerid;
            i++;
            //parameters[i] = new SqlParameter();
            //parameters[i].ParameterName = "@PDSupport";
            //parameters[i].Value = this.PDSupport;
            //i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        public string Event { get; set; }
        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Company";
            SqlParameter[] parameters = new SqlParameter[39];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyID";
            parameters[i].Value = this._companyid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyName";
            parameters[i].Value = this._companyname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyTypeID";
            parameters[i].Value = this._companytypeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ScalesID";
            parameters[i].Value = this._scalesid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyPhone";
            parameters[i].Value = this._companyphone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Website";
            parameters[i].Value = this._website;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CityID";
            parameters[i].Value = this._cityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LeadBy";
            parameters[i].Value = this._leadby;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@NoOfEmployee";
            parameters[i].Value = this._noofemployee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Probability";
            parameters[i].Value = this._probability;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Stage";
            parameters[i].Value = this._stage;
            i++;           
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ExpectedClosedDate";
            parameters[i].Value = this._expectedcloseddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualClosedDate";
            parameters[i].Value = this._actualcloseddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PartnerPDID";
            parameters[i].Value = this._partnerpdid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContractDate";
            parameters[i].Value = this._contractdate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RankingID";
            parameters[i].Value = this._rankingid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = this._xmlstring;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AreaID";
            parameters[i].Value = this.AreaID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this._rowid;
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
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BuyerID";
            parameters[i].Value = this._buyerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Gift";
            parameters[i].Value = this._gift;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@GiftAmount";
            parameters[i].Value = this._giftamount;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@GiftDate";
            parameters[i].Value = this._giftdate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SalaryDay";
            parameters[i].Value = this._salaryday;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ApproachDate";
            parameters[i].Value = this._approachdate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Event";
            parameters[i].Value = Event;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MainContact";
            parameters[i].Value = MainContact;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OnsiteSchedule";
            parameters[i].Value = this._onsiteschedule;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualEmp";
            parameters[i].Value = this._actualemp;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PartnerID";
            parameters[i].Value = this._partnerid;
            i++;
            //parameters[i] = new SqlParameter();
            //parameters[i].ParameterName = "@PDSupport";
            //parameters[i].Value = this.PDSupport;
            //i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Company";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CompanyID";
            parameters[0].Value = CompanyID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Company GetDC_Company(string CompanyID)
        {
            string PROCEDURE = "p_SelectDC_Company";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CompanyID";
            parameters[0].Value = CompanyID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                CompanyTypeID = row["CompanyTypeID"].ToString(),
                ScalesID = row["ScalesID"].ToString(),
                CompanyPhone = row["CompanyPhone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Website = row["Website"].ToString(),
                Address = row["Address"].ToString(),
                CityID = row["CityID"].ToString(),
                LeadBy = row["LeadBy"].ToString(),
                NoOfEmployee = Convert.ToInt32(row["NoOfEmployee"].ToString()),
                Description = row["Description"].ToString(),
                Probability = Convert.ToInt32(row["Probability"].ToString()),
                Stage = row["Stage"].ToString(),                
                ExpectedClosedDate = Convert.ToDateTime(row["ExpectedClosedDate"].ToString()),
                ActualClosedDate = Convert.ToDateTime(row["ActualClosedDate"].ToString()),
                PartnerPDID = row["PartnerPDID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                ContractDate = Convert.ToDateTime(row["ContractDate"].ToString()),
                RankingID = Convert.ToInt32(row["RankingID"].ToString()),
                Note = row["Note"].ToString(),
                XMLString = row["XMLString"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                SalaryDay = row["SalaryDay"].ToString(),
                BankID = row["BankID"].ToString(),
                BuyerID = row["BuyerID"].ToString(),
                ApproachDate = Convert.ToDateTime(row["ApproachDate"].ToString()),
                GiftDate = Convert.ToDateTime(row["GiftDate"].ToString()),
                Gift = row["Gift"].ToString(),
                GiftAmount = row["GiftAmount"].ToString(),
                Event = row["Event"].ToString(),
                MainContact = row["MainContact"].ToString(),
                OnsiteSchedule = row["OnsiteSchedule"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString()),
                PartnerID = row["PartnerID"].ToString(),
                PDSupport = row["PDSupport"].ToString(),
            }).ToList().FirstOrDefault();
        }
        public static DC_Company GetDC_AreaByOrg(string OrganizationID)
        {
            string PROCEDURE = "p_SelectDC_AreaByOrg";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                AreaID = row["AreaID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static DC_Company GetDataByOrg(string OrganizationID)
        {
            string PROCEDURE = "p_SelectDC_GetDataByOrg";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrganizationID";
            parameters[0].Value = OrganizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                PartnerPDID = row["PDID"].ToString(),
                NoOfEmployee = Convert.ToInt32(row["Employee"].ToString()),
                Stage = row["StageID"].ToString(),
                ExpectedClosedDate = Convert.ToDateTime(row["TentativeDeadline"].ToString()),
                ActualClosedDate = Convert.ToDateTime(row["OfficialContractDate"].ToString()),                
                ContractDate = Convert.ToDateTime(row["ContractDate"].ToString()),
                PartnerID = row["PartnerID"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString())
            }).FirstOrDefault();
        }

        //public static DC_MOP_Area GetListAreaByAreaName(string areaName)
        //{
        //    string PROCEDURE = "SELECT AreaID, AreaName FROM DC_MOP_Area WHERE [Status]=1 AND AreaName = N'" + areaName + "' order by AreaName";

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
        //    return dt.AsEnumerable().Select(row => new DC_MOP_Area
        //    {
        //        AreaID = row["AreaID"].ToString(),
        //        AreaName = row["AreaName"].ToString() + "-" + row["AreaID"].ToString()
        //    }).FirstOrDefault();
        //}

        public static List<DC_Company> GetDC_Companys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_CompanysDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                CompanyTypeID = row["CompanyTypeID"].ToString(),
                ScalesID = row["ScalesID"].ToString(),
                CompanyPhone = row["CompanyPhone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Website = row["Website"].ToString(),
                Address = row["Address"].ToString(),
                CityID = row["CityID"].ToString(),
                LeadBy = row["LeadBy"].ToString(),
                NoOfEmployee = Convert.ToInt32(row["NoOfEmployee"].ToString()),
                Description = row["Description"].ToString(),
                Probability = Convert.ToInt32(row["Probability"].ToString()),
                Stage = row["Stage"].ToString(),               
                ExpectedClosedDate = Convert.ToDateTime(row["ExpectedClosedDate"].ToString()),
                ActualClosedDate = Convert.ToDateTime(row["ActualClosedDate"].ToString()),
                PartnerPDID = row["PartnerPDID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                ContractDate = Convert.ToDateTime(row["ContractDate"].ToString()),
                RankingID = Convert.ToInt32(row["RankingID"].ToString()),
                Note = row["Note"].ToString(),
                XMLString = row["XMLString"].ToString(),               
                AreaID = row["AreaID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RegionName = row["RegionName"].ToString(),
                BranchName = row["BranchName"].ToString(),
                Recommand = row["Recommand"].ToString(),
                ResultName = row["ResultName"].ToString(),
                RegionalBD = row["RegionalBD"].ToString(),
                MainContact = row["MainContact"].ToString(),
                EmailContact = row["EmailContact"].ToString(),
                WorkshopDate = Convert.ToDateTime(row["WorkshopDate"].ToString()),
                SalaryDay = row["SalaryDay"].ToString(),
                BankID = row["BankID"].ToString(),
                BuyerID = row["BuyerID"].ToString(),
                ApproachDate = Convert.ToDateTime(row["ApproachDate"].ToString()),
                MeetingNotes = row["MeetingNotes"].ToString(),
                GiftDate = Convert.ToDateTime(row["GiftDate"].ToString()),
                Gift = row["Gift"].ToString(),
                GiftAmount = row["GiftAmount"].ToString(),
                Issue = row["Issue"].ToString(),
                AreaName = row["AreaName"].ToString(),
                Count = row["Count"].ToString(),
                Highlights = row["Highlights"].ToString(),
                StepDescription = row["StepDescription"].ToString(),
                Event = row["Event"].ToString(),
                MobileContact = row["MobileContact"].ToString(),
                NextStepStr = row["NextStep"].ToString(),
                OnsiteSchedule = row["OnsiteSchedule"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString()),
                PartnerID = row["PartnerID"].ToString(),
                PartnerName = row["PartnerName"].ToString(),
                PDSupport = row["PDSupport"].ToString(),
            }).ToList();
        }

        public static List<DC_Company> GetListDC_Companys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Company_ListDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                ContactToID = row["CompanyID"].ToString(),
                ContactToName = row["CompanyID"].ToString() + "-" + row["CompanyName"].ToString()
            }).ToList();
        }
        public string MobileContact { get; set; }
        public string StepDescription { set; get; }
        public string Highlights { set; get; }
        public string MeetingNotes { set; get; }
        public string Issue { get; set; }
        public string AreaName { get; set; }
        public string BranchName { get; set; }
        public string RegionName { get; set; }
        public string Recommand { get; set; }
        public string ResultName { get; set; }
        public string RegionalBD { get; set; }
        public string MainContact { get; set; }
        public string EmailContact { get; set; }
        public DateTime WorkshopDate { get; set; }
        public string Count { get; set; }
        
        public static List<DC_Company> GetAllDC_Companys()
        {
            string PROCEDURE = "p_SelectDC_CompanysAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                CompanyTypeID = row["CompanyTypeID"].ToString(),
                ScalesID = row["ScalesID"].ToString(),
                CompanyPhone = row["CompanyPhone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Website = row["Website"].ToString(),
                Address = row["Address"].ToString(),
                CityID = row["CityID"].ToString(),
                LeadBy = row["LeadBy"].ToString(),
                NoOfEmployee = Convert.ToInt32(row["NoOfEmployee"].ToString()),
                Description = row["Description"].ToString(),
                Probability = Convert.ToInt32(row["Probability"].ToString()),
                Stage = row["Stage"].ToString(),
                NextStep = row["NextStep"].ToString(),
                ExpectedClosedDate = Convert.ToDateTime(row["ExpectedClosedDate"].ToString()),
                ActualClosedDate = Convert.ToDateTime(row["ActualClosedDate"].ToString()),
                PartnerPDID = row["PartnerPDID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                ContractDate = Convert.ToDateTime(row["ContractDate"].ToString()),
                RankingID = Convert.ToInt32(row["RankingID"].ToString()),
                Note = row["Note"].ToString(),
                XMLString = row["XMLString"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                OnsiteSchedule = row["OnsiteSchedule"].ToString(),
                ActualEmp = Convert.ToInt32(row["ActualEmp"].ToString()),
                PartnerID = row["PartnerID"].ToString()
            }).ToList();
        }
      
        public static List<DC_Company> GetAllDC_CompanysByInfo()
        {
            string PROCEDURE = "SELECT CompanyID,CompanyName FROM DC_Company Order by CompanyName asc";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString()
            }).ToList();
        }
        public static List<DC_Company> GetAllDC_Company_List()
        {
            string PROCEDURE = "p_SelectDC_Company_List";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString()
            }).ToList();
        }

        public static List<DC_Company> GetAllDC_Company_ListByArea()
        {
            string PROCEDURE = "SELECT CompanyID,CompanyName FROM DC_Company where AreaID = '' and OrganizationID=''";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString()
            }).ToList();
        }

        public static DC_Company CheckExitCompanyID(string ComID)
        {
            string PROCEDURE = "SELECT CompanyID,CompanyName FROM DC_Company where CompanyID = '"+ ComID +"'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Company> GetAllDC_Company_Area(string whereCondition)
        {
            string PROCEDURE = "p_SelectDC_Company_MOP";

            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company
            {
                CompanyID = row["CompanyID"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                CompanyTypeID = row["CompanyTypeID"].ToString(),
                ScalesID = row["ScalesID"].ToString(),
                CompanyPhone = row["CompanyPhone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Website = row["Website"].ToString(),
                Address = row["Address"].ToString(),
                CityID = row["CityID"].ToString(),
                LeadBy = row["LeadBy"].ToString(),
                NoOfEmployee = Convert.ToInt32(row["NoOfEmployee"].ToString()),
                Description = row["Description"].ToString(),
                Probability = Convert.ToInt32(row["Probability"].ToString()),
                Stage = row["Stage"].ToString(),
                NextStep = row["NextStep"].ToString(),
                ExpectedClosedDate = Convert.ToDateTime(row["ExpectedClosedDate"].ToString()),
                ActualClosedDate = Convert.ToDateTime(row["ActualClosedDate"].ToString()),
                PartnerPDID = row["PartnerPDID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                ContractDate = Convert.ToDateTime(row["ContractDate"].ToString()),
                RankingID = Convert.ToInt32(row["RankingID"].ToString()),
                Note = row["Note"].ToString(),
                XMLString = row["XMLString"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CityName = row["CityName"].ToString(),
                TypeName = row["TypeName"].ToString(),
                ScalesName = row["ScalesName"].ToString(),
                PDName = row["PDName"].ToString(),
                PartnerName = row["PartnerName"].ToString(),
            }).ToList();
        }
    }
}
