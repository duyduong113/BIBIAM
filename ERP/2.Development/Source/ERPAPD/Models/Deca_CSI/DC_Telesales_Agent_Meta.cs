using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_Telesales_Agent_Meta
    {
        #region Members
        private string _agentid = String.Empty;
        public string AgentID { get { return _agentid; } set { this._agentid = value; } }

        private string _factor = String.Empty;
        public string Factor { get { return _factor; } set { this._factor = value; } }

        private string _value = String.Empty;
        public string Value { get { return _value; } set { this._value = value; } }

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

        private string _team = String.Empty;
        public string Team { get { return _team; } set { this._team = value; } }

        private string _imporNote = String.Empty;
        public string ImportNote { get { return _imporNote; } set { this._imporNote = value; } }

        private string _issend = String.Empty;
        public string IsSend { get { return _issend; } set { this._issend = value; } }

        public string DCUser { get; set; }
        public string Region { get; set; }
        #endregion

        public DC_Telesales_Agent_Meta()
        { }

        public DC_Telesales_Agent_Meta(string AgentID, string Factor, string Value, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._agentid = AgentID;
            this._factor = Factor;
            this._value = Value;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Telesales_Agent_Meta";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = this._agentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Factor";
            parameters[i].Value = this._factor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Value";
            parameters[i].Value = this._value;
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

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Telesales_Agent_Meta";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = this._agentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Factor";
            parameters[i].Value = this._factor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Value";
            parameters[i].Value = this._value;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Telesales_Agent_Meta";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AgentID";
            parameters[0].Value = AgentID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Factor";
            parameters[1].Value = Factor;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }


        public int Delete(string id, string factor)
        {
            string PROCEDURE = "p_DeleteDC_Telesales_Agent_Meta";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AgentID";
            parameters[0].Value = id;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Factor";
            parameters[1].Value = factor;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        public static DC_Telesales_Agent_Meta GetDC_Telesales_Agent_Meta(string AgentID)
        {
            string PROCEDURE = "p_SelectDC_Telesales_Agent_Meta";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AgentID";
            parameters[0].Value = AgentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Telesales_Agent_Meta> GetDC_Telesales_Agent_Metas(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Telesales_Agent_MetasDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetAllDC_Telesales_Agent_Metas()
        {
            string PROCEDURE = "p_SelectDC_Telesales_Agent_MetasAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString(),
                Factor = row["Factor"].ToString(),
                Value = row["Value"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<EmployeeInfo> GetDC_Telesales_Agent_Metas()
        {
            string PROCEDURE = "p_GetAllDC_Telesales_Agent_Meta";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new EmployeeInfo
            {
                //UserID = row["UserID"].ToString(),
                //Team = row["Team"].ToString(),
                //DCUser = row["DCUser"].ToString(),
                //Rangking = row["Rangking"].ToString(),
                //CreatedUser = row["CreatedUser"].ToString(),
                //CreatedDatetime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //XLiteID = row["XLiteID"].ToString(),
                //Region = row["Region"].ToString(),
                //IsLeader = row["IsLeader"].ToString(),
                //IsManager = row["IsManager"].ToString(),
                //FakeUser = row["UserID"].ToString().Replace(".", "___").ToString(),
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetDCDistinc_Telesales_Agent_Metas()
        {
            string PROCEDURE = "p_GetTeamDistinctTelesales_Agent_Meta";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {

                Value = row["Value"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetDCUserDistinc_Telesales_Agent_Metas()
        {
            string PROCEDURE = "SELECT DISTINCT Value FROM DW_DC_Order_Property WHERE Name = 'admin_user'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                Value = row["Value"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetAllListAgent_Metas()
        {
            string PROCEDURE = "SELECT DISTINCT AgentID FROM DC_Telesales_Agent_Meta";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetAllListAgentAutoFilter_Metas(string agent)
        {

            string PROCEDURE = "getListAgentAutoFilter ";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = agent;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetAllListTeamAutoFilter_Metas(string agent)
        {

            string PROCEDURE = "p_getTeamAutoFilter ";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Team";
            parameters[i].Value = agent;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                Team = row["Team"].ToString()
            }).ToList();
        }
        public static List<DC_Telesales_Agent_Meta> GetListAgent_Metas()
        {
            string PROCEDURE = "SELECT DISTINCT AgentID FROM DC_Telesales_Agent_Meta ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetListTeamAgent_Metas()
        {
            string PROCEDURE = "p_getListTeamTelesalesAgentMeta";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                Team = row["Team"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_Agent_Meta> GetAllListAgentID()
        {
            string PROCEDURE = "getListAgentID";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                AgentID = row["AgentID"].ToString()
            }).ToList();
        }


        public static List<DC_Telesales_Agent_Meta> GetDCUserDistinc_Telesales_ByTeam()
        {
            string PROCEDURE = "select Distinct Value AS Team from DC_Telesales_Agent_Meta Where Factor = 'Team' And Value != ''  Order by Value";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                Team = row["Team"].ToString()
            }).ToList();
        }

        //DungNT
        public static List<DC_Telesales_Agent_Meta> GetDCUserDistinc_Telesales_ByTeamDungNT()
        {
            string PROCEDURE = "select Distinct Value AS Team from DC_Telesales_Agent_Meta Where Factor = 'Team' And Value != ''  Order by Value";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                TeamID = row["Team"].ToString(),
                BDTeam = row["Team"].ToString()
            }).ToList();
        }
        public static List<DC_Telesales_Agent_Meta> GetDCUserSearch_Telesales_ByTeamDungNT(string Team)
        {
            string PROCEDURE = "SELECT Distinct Value, AgentID FROM DC_Telesales_Agent_Meta WHERE Factor = 'Team' AND Value <> '' AND Value like N'" + Team + "' ORDER BY AgentID";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                UserID = row["AgentID"].ToString()
            }).ToList();
        }

        public static DC_Telesales_Agent_Meta GetTeam(string UserID)
        {
            string PROCEDURE = "select * from DC_Telesales_Agent_Meta where Factor = 'Team' AND AgentID = '" + UserID + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent_Meta
            {
                Value = row["Value"].ToString()
            }).FirstOrDefault();
        }

        public string UserID { get; set; }
        public string TeamID { get; set; }
        public string BDTeam { get; set; }

        public static int ResetListTelesaleWaiting(string region)
        {
            string PROCEDURE = "delete from DC_Telesales_Agent_Meta where Factor = 'IsAssign' AND AgentID IN ( select AgentID from DC_Telesales_Agent_Meta WHERE Factor = 'Region' AND Value = 'A0001') ";
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
        }

        //vuongnd
        public static Users GetRandomTelesalesPreOrder()
        {
            string PROCEDURE = "p_GetAllDC_Telesales_Agent_MetaPreOrder";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Users
            {
                //UserID = row["UserID"].ToString(),
                //IsLeader = row["IsLeader"].ToString(),
                //IsSend = row["IsSend"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static int UpdatePreOrderIsSend()
        {
            string text = "UPDATE DC_Telesales_Agent_Meta SET Value = 'false' WHERE Factor = 'IsSend'";
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, null);
        }

        public static int RemoveUserFromTeles(string UserID)
        {
            string text = "DELETE UserInGroup WHERE GroupID = 6 AND UserID = '" + UserID + "' DELETE DC_Telesales_Agent_Meta WHERE AgentID = '" + UserID + "' DELETE DC_TeleSale_Agent_Branch_Region WHERE UserID = '" + UserID + "'";
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, null);
        }
    }
}