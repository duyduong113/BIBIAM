using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ERPAPD.Models
{
    public class DC_Customer_Resigned
    {
        #region Members
        private string _customerid = String.Empty;
        public DateTime RepaidDate
        {
            get
            {
                return this._repaidDate;
            }
            set
            {
                this._repaidDate = value;
            }
        }

        public double Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public string CustomerID
        {
            get
            {
                return _customerid;
            }
            set
            {
                this._customerid = value;
            }
        }

        private DateTime _resigneddate;
        [Required]
        public DateTime ResignedDate { get { return _resigneddate; } set { this._resigneddate = value; } }

        private DateTime _informeddate;
        [Required]
        public DateTime InformedDate { get { return _informeddate; } set { this._informeddate = value; } }

        private string _resignedreason = String.Empty;
        [Required]
        public string ResignedReason { get { return _resignedreason; } set { this._resignedreason = value; } }

        private string _note = String.Empty;
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

        //partial
        private string _keyCustomerid = String.Empty;
        public string KeyCustomerID { get { return _keyCustomerid; } set { this._keyCustomerid = value; } }

        public string Reference { get; set; }

        public string RepayNote { get; set; }

        #endregion

        public DC_Customer_Resigned()
        { }

        public DC_Customer_Resigned(string CustomerID, DateTime ResignedDate, DateTime InformedDate, string ResignedReason, string Note, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._customerid = CustomerID;
            this._resigneddate = ResignedDate;
            this._informeddate = InformedDate;
            this._resignedreason = ResignedReason;
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
            string PROCEDURE = "p_SaveDC_Customer_Resigned";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResignedDate";
            parameters[i].Value = this._resigneddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@InformedDate";
            parameters[i].Value = this._informeddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResignedReason";
            parameters[i].Value = this._resignedreason;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note != null ? this._note : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheBadDebt();
            return result;
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Customer_Resigned";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResignedDate";
            parameters[i].Value = this._resigneddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@InformedDate";
            parameters[i].Value = this._informeddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResignedReason";
            parameters[i].Value = this._resignedreason;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note != null ? this._note : "";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheBadDebt();
            return result;
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Customer_Resigned";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheBadDebt();
            return result;
        }

        public static DC_Customer_Resigned GetDC_Customer_Resigned(string CustomerID)
        {
            string PROCEDURE = "p_SelectDC_Customer_Resigned";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned
            {
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = Convert.ToDateTime(row["ResignedDate"].ToString()),
                InformedDate = Convert.ToDateTime(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Reference = row["Reference"].ToString(),
                Note = row["Note"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Customer_Resigned> GetDC_Customer_Resigneds(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Customer_ResignedsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned
            {
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = Convert.ToDateTime(row["ResignedDate"].ToString()),
                InformedDate = Convert.ToDateTime(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Customer_Resigned> GetAllDC_Customer_Resigneds()
        {
            string PROCEDURE = "p_SelectDC_Customer_ResignedsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned
            {
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = Convert.ToDateTime(row["ResignedDate"].ToString()),
                InformedDate = Convert.ToDateTime(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        //partial repayment history


        private DateTime _repaidDate;
        private double _amount;
        public static async Task<List<DC_Customer_Resigned>> GetAllCustomerRepaymentHistory(string CustomerID)
        {
            string PROCEDURE = "p_DC_GetCustomerResignedRepay";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = CustomerID;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Customer_Resigned
            {
                CustomerID = row["CustomerID"].ToString(),
                RepaidDate = Convert.ToDateTime(row["Repaid Date"].ToString()),
                Amount = Double.Parse(row["Amount"].ToString()),
                RepayNote = row["Repay Note"].ToString()
            }).ToList();
        }


        // NHNAM :  Inactive Customer
        public int Remove()
        {
            string PROCEDURE = "p_ChangeActive_Customer_Resigned";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Value";
            parameters[i].Value = 0;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheBadDebt();
            return result;
        }


        // NHNAM :  Active Customer
        public int Actives()
        {
            string PROCEDURE = "p_ChangeActive_Customer_Resigned";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Value";
            parameters[i].Value = 1;
            i++;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            Helpers.RemoveCache.CacheBadDebt();
            return result;
        }

        public static List<DC_Customer_Resigned> GetReportActiveBadDebt(DateTime FromDate, DateTime ToDate)
        {
            string PROCEDURE = "p_GetReportActiveBadDebt";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FromDate";
            parameters[i].Value = FromDate;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ToDate";
            parameters[i].Value = ToDate;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Resigned
            {
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                ResignedReason = row["ResignedReason"].ToString(),
                FullName = row["FullName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                SettledBalance = Convert.ToDouble( row["SettledBalance"].ToString()),
                PD = Convert.ToDateTime(row["PD"].ToString()),
                BaselineSettledBalance = Convert.ToDouble(row["BaselineSettledBalance"].ToString()),
                DurationDebt = Convert.ToInt32(row["DurationDebt"].ToString()),
                ActionStatus = row["ActionStatus"].ToString(),
                ActionCode = row["ActionCode"].ToString(),
                Note = row["Note"].ToString(),
                ContactMode = row["ContactMode"].ToString(),
                BaselineSettledAmount = row["BaselineSettledAmount"].ToString(),
                Money = row["Money"].ToString(),
                PaymentDate = Convert.ToDateTime(row["PaymentDate"].ToString()),
                Bank = row["Bank"].ToString(),
                CollectionNotes = row["CollectionNotes"].ToString(),
                BD = row["BD"].ToString(),
                BDE = row["BDE"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList();
        }

        public string FullName { get; set; }

        public string BDE { get; set; }

        public string BD { get; set; }

        public string CollectionNotes { get; set; }

        public string Bank { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Money { get; set; }

        public string BaselineSettledAmount { get; set; }

        public string ContactMode { get; set; }

        public string ActionCode { get; set; }

        public string ActionStatus { get; set; }

        public int DurationDebt { get; set; }

        public double BaselineSettledBalance { get; set; }

        public DateTime PD { get; set; }

        public double SettledBalance { get; set; }

        public string MobilePhone { get; set; }

        public string OrganizationID { get; set; }
    }
}