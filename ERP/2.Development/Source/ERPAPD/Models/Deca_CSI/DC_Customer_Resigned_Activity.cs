using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPD.Models
{
    public class DC_Customer_Resigned_Activity
    {
        #region Members
        private string _customerid = String.Empty;
        public string CustomerID { get { return _customerid; } set { this._customerid = value; } }

        private string _actionstatus = String.Empty;
        public string ActionStatus { get { return _actionstatus; } set { this._actionstatus = value; } }

        private string _note = String.Empty;
        [Required]
        public string Note { get { return _note; } set { this._note = value; } }

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



        #endregion

        // meta 
        public string ActionCode { get; set; }
        public string ContactMode { get; set; }
        public string PersonalContacted { get; set; }

        public DateTime DateOfAppointment { get; set; }
        public DC_Customer_Resigned_Activity()
        { }

        public DC_Customer_Resigned_Activity(string CustomerID, string ActionStatus, string Note, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._customerid = CustomerID;
            this._actionstatus = ActionStatus;
            this._note = Note;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Customer_Resigned_Activity";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActionStatus";
            parameters[i].Value = this._actionstatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
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
            parameters[i].ParameterName = "@Id";
            parameters[i].Direction = ParameterDirection.InputOutput;
            parameters[i].Value = this._rowid;
            i++;

            int rs = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return int.Parse(parameters[parameters.Length - 1].Value.ToString());
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Customer_Resigned_Activity";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this._rowid;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActionStatus";
            parameters[i].Value = this._actionstatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
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
            string PROCEDURE = "p_DeleteDC_Customer_Resigned_Activity";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActionStatus";
            parameters[i].Value = this._actionstatus;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Customer_Resigned_Activity GetDC_Customer_Resigned_Activity(string CustomerID, string ActionStatus)
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_Activity";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActionStatus";
            parameters[i].Value = ActionStatus;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity
            {
                CustomerID = row["CustomerID"].ToString(),
                ActionStatus = row["ActionStatus"].ToString(),
                Note = row["Note"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static int UpdateLastActivityInToBadDebt(string CustomerID, string ActionStatus, string ActionCode, string Note, DateTime DateOfAppointment, DateTime LastUpdatedTime)
        {
            string PROCEDURE = "p_UpdateActivityResigned_ByCustomerID";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActionCode";
            parameters[i].Value = ActionCode;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActionStatus";
            parameters[i].Value = ActionStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = Note;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DateOfAppointment";
            parameters[i].Value = DateOfAppointment;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedTime ";
            parameters[i].Value = LastUpdatedTime;
            i++;

            Helpers.RemoveCache.CacheBadDebt();
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static List<DC_Customer_Resigned_Activity> GetDC_Customer_Resigned_Activitys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_ActivitiesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity
            {
                CustomerID = row["CustomerID"].ToString(),
                ActionStatus = row["ActionStatus"].ToString(),
                Note = row["Note"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public string Bank { get; set; }
        public double Money { get; set; }
        public DateTime PaymentDate { get; set; }
        public static List<DC_Customer_Resigned_Activity> GetDC_Customer_Resigned_Activitys(string cusID)
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_Activity";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = cusID;
            i++;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity
            {
                CustomerID = row["CustomerID"].ToString(),
                ActionStatus = row["ActionStatus"].ToString(),
                Note = row["Note"].ToString(),
                ActionCode = row["ActionCode"].ToString(),
                ContactMode = row["ContactMode"].ToString(),
                DateOfAppointment = Convert.ToDateTime(row["DateOfAppointment"].ToString()),
                Active = Convert.ToBoolean(row["Active"]),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Bank = row["Bank"].ToString(),
                PaymentDate = Convert.ToDateTime(row["PaymentDate"].ToString()),
                Money = Convert.ToDouble(row["Money"].ToString()),
                PersonalContacted = row["PersonalContacted"].ToString(),
                BaselineSettledAmount = Convert.ToDouble(row["BaselineSettledAmount"].ToString()),
            }).ToList();
        }


        public static List<DC_Customer_Resigned_Activity> GetAllDC_Customer_Resigned_Activitys()
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned_ActivitysAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity
            {
                CustomerID = row["CustomerID"].ToString(),
                ActionStatus = row["ActionStatus"].ToString(),
                Note = row["Note"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }


        public static async Task<List<DC_Customer_Resigned_Activity>> GetCountRemindMinute()
        {
            string PROCEDURE = "p_CountRemindMinute";

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Customer_Resigned_Activity
            {
                CustomerID = row["CustomerID"].ToString()
            }).ToList();
        }
        public static List<DC_Customer_Resigned_Activity> GetCountRemindDay()
        {
            string PROCEDURE = "p_CountRemindDay";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned_Activity
            {
                CustomerID = row["CustomerID"].ToString()
            }).ToList();
        }
        public int count { get; set; }

        public double BaselineSettledAmount { get; set; }
    }
}