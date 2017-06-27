using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DecaPay.Models
{
    public class DC_Tele_Sales_History
    {
        #region Members
        private string _customerid = String.Empty;
        public string CustomerID { get { return _customerid; } set { this._customerid = value; } }

        private DateTime _calltime;
        public DateTime CallTime { get { return _calltime; } set { this._calltime = value; } }

        private string _resultid = String.Empty;
        [Required]
        public string ResultID { get { return _resultid; } set { this._resultid = value; } }

        private string _orderid = String.Empty;
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage =
            "Name must contain only letters and numbers.")]
        public string OrderID { get { return _orderid; } set { this._orderid = value; } }

        private string _note = String.Empty;
        [Required]
        public string Note { get { return _note; } set { this._note = value; } }

        private bool _inbound;
        public bool Inbound { get { return _inbound; } set { this._inbound = value; } }

        private string _source = String.Empty;
        public string Source { get { return _source; } set { this._source = value; } }

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
        private string _validOrder;
        public string ValidOrder { get { return _validOrder; } set { this._validOrder = value; } }


        private double _amount;
        public double Amount { get { return _amount; } set { this._amount = value; } }

        private DateTime _recalldate;
        public DateTime RecallDate { get { return _recalldate; } set { this._recalldate = value; } }


        public string FullName { get; set; }
        public string MobilePhone { get; set; }
        public string IsDone { get; set; }
        public string OrganizationID { get; set; }


        #endregion

        public DC_Tele_Sales_History()
        { }

        public DC_Tele_Sales_History(string CustomerID, DateTime CallTime, string ResultID, string OrderID, string Note, bool IsInbound, string Source, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser, DateTime RecallDate)
        {
            this._customerid = CustomerID;
            this._calltime = CallTime;
            this._resultid = ResultID;
            this._orderid = OrderID;
            this._note = Note;
            this._inbound = Inbound;
            this._source = Source;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._recalldate = RecallDate;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Tele_Sales_History";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CallTime";
            parameters[i].Value = this._calltime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultID";
            parameters[i].Value = this._resultid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = this._orderid == null ? "" : this._orderid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Inbound";
            parameters[i].Value = this._inbound;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Source";
            parameters[i].Value = this._source == null ? "" : this._source;
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
            parameters[i].ParameterName = "@RecallDate";
            parameters[i].Value = this._recalldate;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Tele_Sales_History";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CallTime";
            parameters[i].Value = this._calltime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultID";
            parameters[i].Value = this._resultid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = this._orderid == null ? "" : this._orderid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Inbound";
            parameters[i].Value = this._inbound;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Source";
            parameters[i].Value = this._source == null ? "" : this._source;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RecallDate";
            parameters[i].Value = this._recalldate;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }


        public int Update_Done()
        {
            string PROCEDURE = "p_UpdateDC_Tele_Sales_History_Done";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this.RowID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Tele_Sales_History";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }

        public static DC_Tele_Sales_History GetDC_Tele_Sales_History(string CustomerID)
        {
            string PROCEDURE = "p_SelectDC_Tele_Sales_History";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                CustomerID = row["CustomerID"].ToString(),
                CallTime = Convert.ToDateTime(row["CallTime"].ToString()),
                ResultID = row["ResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Note = row["Note"].ToString(),
                Inbound = Convert.ToBoolean(row["Inbound"].ToString()),
                Source = row["Source"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RecallDate = string.IsNullOrEmpty(row["RecallDate"].ToString()) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(row["RecallDate"].ToString())
            }).ToList().FirstOrDefault();
        }

        public static DC_Tele_Sales_History GetDC_Tele_Sales_History_ByRowID(string RowID)
        {
            string PROCEDURE = "p_SelectDC_Tele_Sales_History_ByRowID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@RowID";
            parameters[0].Value = RowID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                CustomerID = row["CustomerID"].ToString(),
                CallTime = Convert.ToDateTime(row["CallTime"].ToString()),
                ResultID = row["ResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Note = row["Note"].ToString(),
                Inbound = Convert.ToBoolean(row["Inbound"].ToString()),
                Source = row["Source"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                IsDone = row["IsDone"].ToString(),
                RecallDate = string.IsNullOrEmpty(row["RecallDate"].ToString()) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(row["RecallDate"].ToString())
            }).ToList().FirstOrDefault();
        }
        public static List<DC_Tele_Sales_History> GetDC_Tele_Sales_Historys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Tele_Sales_HistoriesDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                CustomerID = row["CustomerID"].ToString(),
                CallTime = Convert.ToDateTime(row["CallTime"].ToString()),
                ResultID = row["ResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Note = row["Note"].ToString(),
                Inbound = Convert.ToBoolean(row["Inbound"].ToString()),
                Source = row["Source"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ValidOrder = row["ValidOrder"].ToString(),
                Amount = Convert.ToDouble(row["Amount"].ToString()),
                RecallDate = Convert.ToDateTime(row["RecallDate"].ToString())
            }).ToList();
        }

        public static List<DC_Tele_Sales_History> GetAllDC_Tele_Sales_Historys()
        {
            string PROCEDURE = "p_SelectDC_Tele_Sales_HistoriesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                CustomerID = row["CustomerID"].ToString(),
                CallTime = Convert.ToDateTime(row["CallTime"].ToString()),
                ResultID = row["ResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Note = row["Note"].ToString(),
                Inbound = Convert.ToBoolean(row["Inbound"].ToString()),
                Source = row["Source"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RecallDate = string.IsNullOrEmpty(row["RecallDate"].ToString()) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(row["RecallDate"].ToString())
            }).ToList();
        }



        public static DataSourceResult GetAllDC_Tele_Sales_Historys_ManageCallBack(string where, [DataSourceRequest] DataSourceRequest request)
        {
            string PROCEDURE = "p_SelectManageCallback";
            var from1 = (request.Page - 1) * request.PageSize + 1;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@where";
            parameters[i].Value = where;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            var data = new DataSourceResult();
            data.Data = dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                Note = row["Note"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FullName = row["FullName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                RecallDate = Convert.ToDateTime(row["RecallDate"].ToString()),
                CallTime = Convert.ToDateTime(row["CallTime"].ToString()),
                //Inbound = Convert.ToBoolean(row["Inbound"].ToString()),
                //Source = row["Source"].ToString(),
                //IsDone = row["IsDone"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                OrganizationID = row["OrganizationID"].ToString(),
                ResultID = row["Description"].ToString(),
            }).ToList();
            var total = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();
            data.Total = total != null ? total.TotalRows : 0;
            return data;
        }

        //DungNT: Update
        public static DC_Tele_Sales_History SelectCount()
        {
            string PROCEDURE = "select top 1 RowID  from DC_Tele_Sales_History order by RowID desc";
           
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                RowID = Convert.ToInt32(row["RowID"].ToString())
            }).ToList().FirstOrDefault();
        }

        public static DC_Tele_Sales_History GetRule(string CustomerID)
        {
            string PROCEDURE = "Select top 1 CustomerID,ISNULL(AssigmentRule,'') AS 'AssigmentRule' from DC_CallActivities_Rules where CustomerID = '" + CustomerID + "'order by RowID desc";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                CustomerID =row["CustomerID"].ToString(),
                AssigmentRule = row["AssigmentRule"].ToString()
            }).ToList().FirstOrDefault();
        }

        public string AssigmentRule { get; set; }


        public static List<DC_Tele_Sales_History> GetAllDC_Tele_Sales_HistorysCheckCode(string OrderID)
        {
            string PROCEDURE = "SELECT [CustomerID],[CallTime],[ResultID],[OrderID],[Note],[Inbound],[Source],[Active],[RowID],[RowCreatedTime],[RowCreatedUser],[RowLastUpdatedTime],[RowLastUpdatedUser],[RecallDate]  FROM  [dbo].[DC_Tele_Sales_History] WHERE [OrderID] = '" + OrderID + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tele_Sales_History
            {
                CustomerID = row["CustomerID"].ToString(),
                CallTime = Convert.ToDateTime(row["CallTime"].ToString()),
                ResultID = row["ResultID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Note = row["Note"].ToString(),
                Inbound = Convert.ToBoolean(row["Inbound"].ToString()),
                Source = row["Source"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RecallDate = string.IsNullOrEmpty(row["RecallDate"].ToString()) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(row["RecallDate"].ToString())
            }).ToList();
        }

        public static int AutoDone(string CustomerID, string UserID)
        {
            string PROCEDURE = "p_UpdateDC_Tele_Sales_Histories_AutoDone";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserID;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
           
        }
    }
}