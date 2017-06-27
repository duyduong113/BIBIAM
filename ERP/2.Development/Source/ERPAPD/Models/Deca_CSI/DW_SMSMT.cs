using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DW_SMSMT.
    /// </summary>
    public class DW_SMSMT
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

        public string ImportNote { get; set; }

        public string StatusColor { get; set; }

        public string Telco { get; set; }

        public string NetworkName { get; set; }

        public int ID { get; set; }
        #endregion

        public DW_SMSMT()
        { }

        public DW_SMSMT(long RowID, string AgentID, string RequestID, string ServiceID, string CommandCode, string UserID, string Message, long MessageType, DateTime CreatedAt, string Status, long DWID, bool Active, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
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
            this._status = Status;
            this._dwid = DWID;
            this._active = Active;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public long Save(int limit)
        {
            if (this._message.Length > limit)
            {
                return -1;
            }
            string PROCEDURE = "p_SaveDW_SMSMT";
            SqlParameter[] parameters = new SqlParameter[16];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].DbType = DbType.Int32;
            parameters[i].Direction = ParameterDirection.Output;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = this._agentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestID";
            parameters[i].Value = this._requestid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ServiceID";
            parameters[i].Value = this._serviceid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CommandCode";
            parameters[i].Value = this._commandcode;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this._userid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Message";
            parameters[i].Value = this._message;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MessageType";
            parameters[i].Value = this._messagetype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedAt";
            parameters[i].Value = this._createdat;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DWID";
            parameters[i].Value = this._dwid;
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
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            try
            {
                i = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
                this._rowid = long.Parse(parameters[0].Value.ToString());
                return this._rowid;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDW_SMSMT";
            SqlParameter[] parameters = new SqlParameter[16];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this._rowid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = this._agentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestID";
            parameters[i].Value = this._requestid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ServiceID";
            parameters[i].Value = this._serviceid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CommandCode";
            parameters[i].Value = this._commandcode;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this._userid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Message";
            parameters[i].Value = this._message;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MessageType";
            parameters[i].Value = this._messagetype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedAt";
            parameters[i].Value = this._createdat;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DWID";
            parameters[i].Value = this._dwid;
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
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDW_SMSMT";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RowID";
            parameters[0].Value = RowID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DW_SMSMT GetDW_SMSMT(long RowID)
        {
            string PROCEDURE = "p_SelectDW_SMSMT";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RowID";
            parameters[0].Value = RowID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_SMSMT
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
                Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DW_SMSMT> GetDW_SMSMTs(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_SMSMTsDynamic";
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
            return dt.AsEnumerable().Select(row => new DW_SMSMT
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
                Status = row["Status"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DW_SMSMT> GetAllDW_SMSMTs()
        {
            string PROCEDURE = "p_SelectDW_SMSMTsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_SMSMT
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
                Status = row["Status"].ToString(),
                StatusColor = row["Status"].ToString().Contains("Fail") ? "background-color:#D81F44;color:#FFF;" : row["Status"].ToString().Contains("Success") ? "background-color:#3E9BCE;color:#FFF;" : "background-color:#F1E377;color:#AF6B15;",
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString()
            }).ToList();
        }

        public static List<DW_SMSMT> GetAllDW_SMSMTsDynamic(string WhereCondition)
        {
            string PROCEDURE = "p_SelectDW_SMSMTsAllDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_SMSMT
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
                Status = row["Status"].ToString(),
                StatusColor = row["StatusColor"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                ID = Int32.Parse(row["ID"].ToString()),
                NetworkName = row["NetworkName"].ToString(),
            }).ToList();
        }

        public static DataSourceResult GetAllDW_SMSMTsDS(string WhereCondition, [DataSourceRequest] DataSourceRequest request)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_SelectDW_SMSMTsAllDS";
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

            data.Data = dt.AsEnumerable().Select(row => new DW_SMSMT
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
                Status = row["Status"].ToString(),
                StatusColor = row["StatusColor"].ToString(),
                DWID = long.Parse(row["DWID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Telco = row["Telco"].ToString(),
                ID = Int32.Parse(row["ID"].ToString()),
                NetworkName = row["NetworkName"].ToString()
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_SMSMT
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }
        public int TotalRows { get; set; }

        public string CheckTelco(string Telco)
        {
            List<DC_HomeNetwork> data = new List<DC_HomeNetwork>();
            return "";
        }
        public bool CheckPhoneNumber(List<DC_NumberNetwork> dauso)
        {
            string number = this.UserID;
            long c = 0;
            if (!long.TryParse(number, out c))
                return false;
            if (number.Length < 10 || number.Length > 11)
                return false;

            var number2 = number.Substring(0, number.Length - 7);
            foreach (var item in dauso)
            {
                if (number2 == item.Number)
                {
                    if (item.ValidText != "" && !this.Message.Contains(item.ValidText))
                    {
                        this.Message += " " + item.ValidText;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
