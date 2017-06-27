using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DW_SMSMO.
    /// </summary>
    public class DW_SMSMO
    {
        #region Members
        private long _rowid;
        public long RowID { get { return _rowid; } set { this._rowid = value; } }

        private string _agentid = String.Empty;
        public string AgentID { get { return _agentid; } set { this._agentid = value; } }

        private string _requestid = String.Empty;
        public string RequestID { get { return _requestid; } set { this._requestid = value; } }

        private string _serviceid = String.Empty;
        public string ServiceID { get { return _serviceid; } set { this._serviceid = value; } }

        private string _commandcode = String.Empty;
        public string CommandCode { get { return _commandcode; } set { this._commandcode = value; } }

        private string _userid = String.Empty;
        public string UserID { get { return _userid; } set { this._userid = value; } }

        private string _message = String.Empty;
        public string Message { get { return _message; } set { this._message = value; } }

        private long _messagetype;
        public long MessageType { get { return _messagetype; } set { this._messagetype = value; } }

        private DateTime _createdat;
        public DateTime CreatedAt { get { return _createdat; } set { this._createdat = value; } }

        private DateTime _pickedat;
        public DateTime PickedAt { get { return _pickedat; } set { this._pickedat = value; } }

        private string _status = String.Empty;
        public string Status { get { return _status; } set { this._status = value; } }

        private long _dwid;
        public long DWID { get { return _dwid; } set { this._dwid = value; } }

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

        public string Telco { get; set; }
        public string IsCorrect { get; set; }
        public string Description { get; set; }
        public string MCA_Process { get; set; }
        public string CustomerID { get; set; }
        public string CreditLimit { get; set; }
        public string CustomerName { get; set; }
        public string ProcessStatusColor { get; set; }
        public string CheckSyntaxColor { get; set; }
        public string ProcessStatus { get; set; }
        public string CheckSyntax { get; set; }

        #endregion

        public DW_SMSMO()
        {
        }

        public DW_SMSMO(long RowID, string AgentID, string RequestID, string ServiceID, string CommandCode, string UserID, string Message, long MessageType, DateTime CreatedAt, DateTime PickedAt, string Status, long DWID, bool Active, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser, string note, string status)
        {
            this._rowid = RowID;
            this._agentid = AgentID;
            this._requestid = RequestID;
            this._serviceid = ServiceID;
            this._commandcode = CommandCode;
            this._userid = UserID;
            this._message = Message;
            this._messagetype = MessageType;
            this._createdat = CreatedAt;
            this._pickedat = PickedAt;
            this._status = Status;
            this._dwid = DWID;
            this._active = Active;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
            NoteInMeta = note;
            Status = status;
        }


        public static DW_SMSMO GetDW_SMSMO(long RowID)
        {
            string PROCEDURE = "p_SelectDW_SMSMO";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RowID";
            parameters[0].Value = RowID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DW_SMSMO> GetDW_SMSMOs(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_SMSMOsDynamic";
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
            return dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DW_SMSMO> GetAllDW_SMSMOsDynamic(string whereCondition, string UserID)
        {
            string PROCEDURE = "p_SelectDW_SMSMOsAllDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LoginUser";
            parameters[i].Value = UserID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt2"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                //Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                NetworkName = row["NetworkName"].ToString(),
                CheckSyntax = row["CheckSyntax"].ToString(),
                Description = row["SyntaxDescription"].ToString(),
                ProcessStatus = row["ProcessStatus"].ToString(),
                //CustomerID = row["CustomerID"].ToString(),
                //CreditLimit = string.Format("{0:0,0}",row["CreditLimit"]),
                //CustomerName = row["CustomerName"].ToString(),
                ProcessStatusColor = row["ProcessStatusColor"].ToString(),
                CheckSyntaxColor = row["CheckSyntaxColor"].ToString(),
                ReasonInMeta = row["ReasonInMeta"].ToString(),
                NoteInMeta = row["NoteInMeta"].ToString(),
                LastUpdateBy = row["LastUpdateBy"].ToString(),
                LastUpdateAt = DateTime.Parse(row["LastUpdateAt"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }




        public string FullName { get; set; }
        public string XAccountID { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }

        public string PhysicalID { get; set; }
        public string MCAStatus { get; set; }

        public double DueLimit { get; set; }
        public double RunningBalance { get; set; }
        public double SettledBalance { get; set; }
        public double DueBalance { get; set; }
        public string OrganizationID { get; set; }

        public static DataSourceResult GetAllDW_SMSMOsDynamicDS(string WhereCondition, [DataSourceRequest] DataSourceRequest request, string UserID)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_SelectDW_SMSMOsAllDynamicDS";
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
            parameters[i].ParameterName = "@LoginUser";
            parameters[i].Value = UserID;
            i++;

            var data = new DataSourceResult();

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            data.Data = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt2"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                //Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                NetworkName = row["NetworkName"].ToString(),
                CheckSyntax = row["CheckSyntax"].ToString(),
                Description = row["SyntaxDescription"].ToString(),
                ProcessStatus = row["ProcessStatus"].ToString(),
                //CustomerID = row["CustomerID"].ToString(),
                //CreditLimit = string.Format("{0:0,0}",row["CreditLimit"]),
                //CustomerName = row["CustomerName"].ToString(),
                ProcessStatusColor = row["ProcessStatusColor"].ToString(),
                CheckSyntaxColor = row["CheckSyntaxColor"].ToString(),
                ReasonInMeta = row["ReasonInMeta"].ToString(),
                NoteInMeta = row["NoteInMeta"].ToString(),
                LastUpdateBy = row["LastUpdateBy"].ToString(),
                LastUpdateAt = DateTime.Parse(row["LastUpdateAt"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                OrganizationID = row["OrganizationID"].ToString()
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }


        public static List<DW_SMSMO> GetAllDW_SMSMOsDynamicExportSensitive(string WhereCondition,  string UserID)
        {
            string PROCEDURE = "p_SelectDW_SMSMOsAllDynamicDS";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN 1 AND (SELECT MAX(RowNum) FROM SMSTemp)";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LoginUser";
            parameters[i].Value = UserID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt2"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                NetworkName = row["NetworkName"].ToString(),
                CheckSyntax = row["CheckSyntax"].ToString(),
                Description = row["SyntaxDescription"].ToString(),
                ProcessStatus = row["ProcessStatus"].ToString(),
                ProcessStatusColor = row["ProcessStatusColor"].ToString(),
                CheckSyntaxColor = row["CheckSyntaxColor"].ToString(),
                ReasonInMeta = row["ReasonInMeta"].ToString(),
                NoteInMeta = row["NoteInMeta"].ToString(),
                LastUpdateBy = row["LastUpdateBy"].ToString(),
                LastUpdateAt = DateTime.Parse(row["LastUpdateAt"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                OrganizationID = row["OrganizationID"].ToString()
            }).ToList();
        }


        public static DataSourceResult GetAllDW_SMSMOsDynamicDS_byOrg(string WhereCondition, [DataSourceRequest] DataSourceRequest request, string UserID)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_SelectDW_SMSMOsAllDynamicDSByOrg";
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
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserID;
            i++;

            var data = new DataSourceResult();
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            data.Data = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt2"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                //Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                NetworkName = row["NetworkName"].ToString(),
                CheckSyntax = row["CheckSyntax"].ToString(),
                Description = row["SyntaxDescription"].ToString(),
                ProcessStatus = row["ProcessStatus"].ToString(),
                //CustomerID = row["CustomerID"].ToString(),
                //CreditLimit = string.Format("{0:0,0}",row["CreditLimit"]),
                //CustomerName = row["CustomerName"].ToString(),
                ProcessStatusColor = row["ProcessStatusColor"].ToString(),
                CheckSyntaxColor = row["CheckSyntaxColor"].ToString(),
                ReasonInMeta = row["ReasonInMeta"].ToString(),
                NoteInMeta = row["NoteInMeta"].ToString(),
                LastUpdateBy = row["LastUpdateBy"].ToString(),
                LastUpdateAt = DateTime.Parse(row["LastUpdateAt"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;


        }
        public string ReasonInMeta { get; set; }
        public string NoteInMeta { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateAt { get; set; }
        public static string Correct(string Text)
        {
            string Pre = Text.Substring(0, Text.LastIndexOf("") + 1).Trim();
            string Suf = Text.Substring(Text.LastIndexOf("") + 1).Trim();
            string PROCEDURE = "p_Select_SMS_Iscorrect";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Pre";
            parameters[i].Value = Pre;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Suf";
            parameters[i].Value = Suf;
            i++;
            return SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters).ToString();
        }

        public static string Process(string Text)
        {
            string Pre = Text.Substring(0, Text.LastIndexOf("") + 1).Trim();
            string Suf = Text.Substring(Text.LastIndexOf("") + 1).Trim();
            string PROCEDURE = "p_Select_SMS_Status";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Pre";
            parameters[i].Value = Pre;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Suf";
            parameters[i].Value = Suf;
            i++;
            return SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters).ToString();
        }
        public int TotalRows { get; set; }

        public static List<DW_SMSMO> GetAllDW_SMSMOs(string userid, string listagent, string message)
        {
            string PROCEDURE = "";

            if (string.IsNullOrEmpty(listagent))
            {
                PROCEDURE = "select * from vw_SelectDW_SMSMOsAll where UserID like '%" + userid + "%' and [Message] like '%" + message + "%'";
            }
            else
            {
                PROCEDURE = "select * from vw_SelectDW_SMSMOsAll where UserID like '%" + userid + "%' and AgentID in (" + listagent + ") and [Message] like '%" + message + "%'";
            }

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                RowID = long.Parse(row["RowID"].ToString()),
                AgentID = row["AgentID"].ToString(),
                RequestID = row["RequestID"].ToString(),
                ServiceID = row["ServiceID"].ToString(),
                CommandCode = row["CommandCode"].ToString(),
                UserID = row["UserID"].ToString(),
                Message = row["Message"].ToString(),
                MessageType = long.Parse(row["MessageType"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                PickedAt = Convert.ToDateTime(row["PickedAt"].ToString()),
                Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                NetworkName = row["NetworkName"].ToString()
            }).ToList();
        }
        public string NetworkName { get; set; }

    }
}
