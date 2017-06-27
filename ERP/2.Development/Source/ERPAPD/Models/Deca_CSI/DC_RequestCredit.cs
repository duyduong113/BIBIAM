using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPD.Models
{
    public class DC_RequestCredit
    {
        #region Members

        private string _requestid = String.Empty;

        public string RequestID { get { return _requestid; } set { this._requestid = value; } }

        private string _organizationid = String.Empty;

        public string OrganizationID { get { return _organizationid; } set { this._organizationid = value; } }

        private string _customerid = String.Empty;

        public string CustomerID { get { return _customerid; } set { this._customerid = value; } }

        private int _status;

        public int Status { get { return _status; } set { this._status = value; } }

        private bool _active;

        public bool Active { get { return _active; } set { this._active = value; } }

        private DateTime _rowcreatedtime;

        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;

        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;

        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;

        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        private string _familyname = String.Empty;

        public string FamilyName { get { return _familyname; } set { this._familyname = value; } }

        private string _givenname = String.Empty;

        public string GivenName { get { return _givenname; } set { this._givenname = value; } }

        private string _address = String.Empty;

        public string Address { get { return _address; } set { this._address = value; } }

        private string _gender = String.Empty;

        public string Gender { get { return _gender; } set { this._gender = value; } }

        private string _email = String.Empty;

        public string Email { get { return _email; } set { this._email = value; } }

        private string _mobilephone = String.Empty;

        public string MobilePhone { get { return _mobilephone; } set { this._mobilephone = value; } }

        private string _position = String.Empty;

        public string Position { get { return _position; } set { this._position = value; } }

        private string _dwid = String.Empty;

        public string DWID { get { return _dwid; } set { this._dwid = value; } }

        //Meta
        private string _type = String.Empty;

        public string Type { get { return _type; } set { this._type = value; } }

        private double _credit = 0;

        public Double Credit { get { return _credit; } set { this._credit = value; } }

        private string _reason = String.Empty;

        public string Reason { get { return _reason; } set { this._reason = value; } }

        private string _note = String.Empty;

        public string Note { get { return _note; } set { this._note = value; } }

        private double _creditnew = 0;

        public double CreditNew { get { return _creditnew; } set { this._creditnew = value; } }

        private string _importnote = String.Empty;

        public string Importnote { get { return _importnote; } set { this._importnote = value; } }

        private string _fullname = String.Empty;

        public string FullName { get { return _fullname; } set { this._fullname = value; } }

        private string _bd = String.Empty;

        public string BD { get { return _bd; } set { this._bd = value; } }

        private double _salary = 0;

        public Double Salary { get { return _salary; } set { this._salary = value; } }

        private string _mcastatus = String.Empty;

        public string MCAStatus { get { return _mcastatus; } set { this._mcastatus = value; } }

        private string _groupId = String.Empty;

        public string GroupID { get { return _groupId; } set { this._groupId = value; } }

        private string _orderid = String.Empty;

        public string OrderID { get { return _orderid; } set { this._orderid = value; } }

        private string _reasondenied = String.Empty;

        public string ReasonDenied { get { return _reasondenied; } set { this._reasondenied = value; } }

        public string OrderStatus { get; set; }

        public string TrackingStatus { get; set; }

        public string RCLStatus { get; set; }

        public string OPSNote { get; set; }

        public string RequesterNote { get; set; }

        private double _sysncredit = 0;

        public double SysnCredit { get { return _sysncredit; } set { this._sysncredit = value; } }

        // Color
        private string _colorstatus = String.Empty;

        public string ColorStatus { get { return _colorstatus; } set { this._colorstatus = value; } }

        private string _colorstatusorder = String.Empty;

        public string ColorStatusOrder { get { return _colorstatusorder; } set { this._colorstatusorder = value; } }

        private string _rlsStatusColor = String.Empty;

        public string RCLStatusColor { get { return _rlsStatusColor; } set { this._rlsStatusColor = value; } }

        public string ColorDay { get; set; }

        public int Day { get; set; }

        public string MBVRule { get; set; }

        public string StartWorkDate { get; set; }

        public string REmail { get; set; }

        public string RPhone { get; set; }

        public string RDepartment { get; set; }

        public DateTime RollbackAt { get; set; }

        public string RollbackBy { get; set; }

        public string CreditNote { get; set; }
        public string TicketID { get; set; }

        #endregion Members

        public DC_RequestCredit()
        { }

        public DC_RequestCredit(string RequestID, string OrganizationID, string CustomerID, int Status, bool Active, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._requestid = RequestID;
            this._organizationid = OrganizationID;
            this._customerid = CustomerID;
            this._status = Status;
            this._active = Active;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_RequestCredit";
            SqlParameter[] parameters = new SqlParameter[8];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestID";
            parameters[i].Value = this._requestid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCreditRequest();
            return result;
        }

        public int UpdateCreditRequest()
        {
            string PROCEDURE = "p_UpdateDC_RequestCredit";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestID";
            parameters[i].Value = this._requestid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCreditRequest();
            return result;
        }

        public int UpdateManageRequest()
        {
            string PROCEDURE = "p_UpdateDC_ManageRequestCredit";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestID";
            parameters[i].Value = this._requestid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
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
            Helpers.RemoveCache.CacheCreditRequest();
            return result;
        }

        public int UpdateManageRequest_RollBack()
        {
            string PROCEDURE = "p_UpdateDC_ManageRequestCredit_RollBack";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestID";
            parameters[i].Value = this._requestid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this._organizationid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RollbackAt";
            parameters[i].Value = this.RollbackAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RollbackBy";
            parameters[i].Value = this.RollbackBy;
            i++;
            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCreditRequest();
            return result;
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_RequestCredit";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RequestID";
            parameters[0].Value = RequestID;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheCreditRequest();
            return result;
        }

        public static DC_RequestCredit GetDC_RequestCredit(string RequestID)
        {
            string PROCEDURE = "p_SelectDC_RequestCredit";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RequestID";
            parameters[0].Value = RequestID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Status = Convert.ToInt16(row["Status"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                TicketID = row["TicketID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static DC_RequestCredit GetDC_RequestCreditByTicketID(string TicketID)
        {
            string PROCEDURE = "p_SelectDC_RequestCreditByTicket";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                Status = Convert.ToInt16(row["Status"].ToString()),
                TicketID = row["TicketID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public int UpdateCreditRequestByStatus()
        {
            string PROCEDURE = "p_UpdateDC_RequestCreditStatus";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
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
            Helpers.RemoveCache.CacheCreditRequest();
            return result;
        }

        public static List<DC_RequestCredit> GetDC_RequestCredits(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_RequestCreditsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Status = Convert.ToInt16(row["Status"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_RequestCredit> GetAllDC_RequestCredits()
        {
            string PROCEDURE = "p_SelectDC_RequestCreditsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Status = Convert.ToInt16(row["Status"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public string Issuer { get; set; }

        public string EmployeeID { get; set; }

        public string CreditLimitRules { get; set; }

        public static async Task<List<DC_RequestCredit>> GetAllDC_Customer_CreditRequests(string WhereCondition, string from)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@from";
            parameters[i].Value = from;

            string PROCEDURE = "p_Select_DC_Customer_Level_RequestOrderCaseDynamic";

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            return ds.Tables[0].AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                //GroupID = row["GroupID"].ToString(),
                //Gender = row["Gender"].ToString(),
                //Email = row["Email"].ToString(),
                //MobilePhone = row["Phone"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                //Reason = row["Reason"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),
                TotalCustomer = row["Customer#"].ToString(),
                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString(),
            }).ToList();
        }

        public static List<DC_RequestCredit> GetAllDC_Customer_CreditRequests_ByCus(string from, string CustomerID)
        {
            string PROCEDURE = "p_Select_DC_Customer_Level_RequestOrderCase_ByCus";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@from";
            parameters[0].Value = from;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@CustomerID";
            parameters[1].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                GroupID = row["GroupID"].ToString(),
                Gender = row["Gender"].ToString(),
                Email = row["Email"].ToString(),
                MobilePhone = row["Phone"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                //Reason = row["Reason"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString()
            }).ToList();
        }

        public static List<DC_RequestCredit> GetAllDC_Customer_CreditRequestsCheck(string requestID, string status)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RequestID";
            parameters[0].Value = requestID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@RCLStatus";
            parameters[1].Value = status;
            string PROCEDURE = "p_Select_DC_Customer_Level_RequestOrderCaseCheck";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RCLStatus = row["RCLStatus"].ToString(),
            }).ToList();
        }

        public static void RequestChangeCreditLimitNotification_Submit()
        {
            string text = "exec p_RequestChangeCreditLimitNotification_Submit";

            SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, text, null);
        }

        public static void RequestChangeCreditLimitNotification_Denied()
        {
            string PROCEDURE = "p_RequestChangeCreditLimitNotification_Denied";

            SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
        }

        public string RefID { get; set; }

        public static List<DC_RequestCredit> CheckTechcombankAccount(string RefID)
        {
            string PROCEDURE = "select RefID from DW_DC_Customer_Property Where name = 'issuer' AND Value = 'tcb' AND RefID = '" + RefID + "'";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RefID = row["RefID"].ToString(),
            }).ToList();
        }

        public static DC_RequestCredit CheckCustomerInTrackingList(string org, string cus)
        {
            string PROCEDURE = "p_CheckCustomerTrackingList";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = org;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = cus;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                CustomerID = row["CustomerID"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static string CheckCustomerInTrackingListColor(string org, string cus)
        {
            string PROCEDURE = "p_CheckCustomerTrackingList";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = org;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = cus;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            if (dt.Rows.Count > 0)
            {
                return "background-color:#D81F44;color:#FFF";
            }
            else
            {
                return "";
            }
        }

        public static List<DC_RequestCredit> GetAllDC_Customer_CreditRequestsCustomer(string WhereCondition, string from)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@from";
            parameters[i].Value = from;

            string PROCEDURE = "p_getCustomerRequested";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                //GroupID = row["GroupID"].ToString(),
                //Gender = row["Gender"].ToString(),
                //Email = row["Email"].ToString(),
                //MobilePhone = row["Phone"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                //Reason = row["Reason"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),
                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString(),
            }).ToList();
        }

        public static async Task<List<DC_RequestCredit>> GetAllDC_Customer_CreditRequests_Tracking(string WhereCondition, string from)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@from";
            parameters[i].Value = from;

            string PROCEDURE = "p_GetCustomerRequestCredit_Tracking";

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            return ds.Tables[0].AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                //GroupID = row["GroupID"].ToString(),
                //Gender = row["Gender"].ToString(),
                //Email = row["Email"].ToString(),
                //MobilePhone = row["Phone"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                //Reason = row["Reason"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),

                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString(),
            }).ToList();
        }

        public static DataSourceResult GetCustomerCreditRequest(string WhereCondition, [DataSourceRequest] DataSourceRequest request, string from)
        {
            //[DataSourceRequest] DataSourceRequest request;
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

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

            string PROCEDURE = "p_GetCustomerCreditRequest";

            var data = new DataSourceResult();
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            data.Data = dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),
                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString(),

                TicketID = row["TicketID"].ToString()
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }


        public static DataSourceResult GetCustomerCreditRequestTracking(string WhereCondition, [DataSourceRequest] DataSourceRequest request, string from)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

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

            string PROCEDURE = "p_GetCustomerRequestCredit_Tracking";

            var data = new DataSourceResult();
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            data.Data = dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),
                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString(),
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }

        public static List<DC_RequestCredit> GetCustomerCreditRequest_Export(string WhereCondition)
        {

            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            string PROCEDURE = "p_GetCustomerCreditRequest_Export";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),
                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString(),
                TicketID = row["TicketID"].ToString()
            }).ToList();
        }


        public static List<DC_RequestCredit> GetCustomerCreditRequestByCustomerID(string CustomerID)
        {

            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;

            string PROCEDURE = "p_GetCustomerRequestCredit_TrackingByCustomerID";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                Type = row["Type"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                Note = row["Note"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                ColorDay = row["ColorDay"].ToString()
            }).ToList();
        }


        public static List<DC_RequestCredit> GetCustomerCreditRequest_ExportTracking(string WhereCondition)
        {

            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            string PROCEDURE = "p_GetCustomerRequestCredit_TrackingExport";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                RequestID = row["RequestID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                Salary = Convert.ToDouble(row["Salary"].ToString()),
                BD = row["BD"].ToString(),
                MCAStatus = row["MCAStatus"].ToString(),
                Type = row["Type"].ToString(),
                OrderID = row["OrderID"].ToString(),
                OrderStatus = row["OrderStatus"].ToString(),
                RCLStatus = row["RCLStatus"].ToString(),
                ReasonDenied = row["DeniedReason"].ToString(),
                Note = row["Note"].ToString(),
                RequesterNote = row["RequesterNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                Credit = Convert.ToDouble(row["CurrentCredit"]),
                CreditNew = Convert.ToDouble(row["NewCredit"]),
                SysnCredit = Convert.ToDouble(row["SynCredit"]),
                Status = Convert.ToInt16(row["StatusRequest"].ToString()),
                ColorStatus = row["StatusColor"].ToString(),
                ColorStatusOrder = row["StatusOrder"].ToString(),
                RCLStatusColor = row["RCLStatusColor"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Day = Convert.ToInt16(row["Day"].ToString()),
                Issuer = row["Issuer"].ToString(),
                MBVRule = row["MBVRule"].ToString(),
                StartWorkDate = row["StartWorkDate"].ToString(),
                REmail = row["REmail"].ToString(),
                RPhone = row["RPhone"].ToString(),
                RDepartment = row["RDepartment"].ToString(),
                RollbackAt = Convert.ToDateTime(row["RollbackAt"].ToString()),
                RollbackBy = row["RollbackBy"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString(),
                CreditNote = row["CreditNote"].ToString(),
                TrackingStatusColor = CheckCustomerInTrackingListColor(row["OrganizationID"].ToString(), row["CustomerID"].ToString()).ToString()
            }).ToList();
        }

        public static List<DC_RequestCredit> CheckOrgInArea(string UserID, string org)
        {
            string PROCEDURE = "SELECT TOP 1 * FROM DC_MOP_Area_HumanResource h LEFT JOIN DC_MOP_Area_Org o ON o.AreaID = h.AreaID WHERE h.UserID = '" + UserID + "' AND RoleID IN('UR008','UR027','UR026') AND o.OrganizationID = '" + org + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_RequestCredit
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }
        public string TrackingStatusColor { get; set; }

        public string TrackingColor { get; set; }

        public string TotalCustomer { get; set; }
    }
}