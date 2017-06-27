using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ERPAPD.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DevTrends.MvcDonutCaching;

namespace ERPAPD.Models
{
    public class DC_Tele_Hint
    {
        #region Members
        private string _articleid = String.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage =
            "Name must contain only letters and numbers.")]
        public string ArticleID { get { return _articleid; } set { this._articleid = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }

        private bool _showall;
        [Required]
        public bool ShowAll { get { return _showall; } set { this._showall = value; } }

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

        private string _fakeArticleid = String.Empty;
        public string FakeArticleID { get { return _fakeArticleid; } set { this._fakeArticleid = value; } }

        private string _organization = String.Empty;
        public string Organization { get { return _organization; } set { this._organization = value; } }

        private List<String> _listOrganization;
        public List<String> ListOrganization { get { return _listOrganization; } set { this._listOrganization = value; } }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public string Intro { get; set; }

        public string TypeTab { get; set; }

        #endregion

        public DC_Tele_Hint()
        { }

        public DC_Tele_Hint(string ArticleID, string Description, bool ShowAll, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._articleid = ArticleID;
            this._description = Description;
            this._showall = ShowAll;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Tele_Hint";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ArticleID";
            parameters[i].Value = this._articleid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShowAll";
            parameters[i].Value = this._showall;
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

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("TelesalePluginCode", "ReadByType");
            return result;
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Tele_Hint";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ArticleID";
            parameters[i].Value = this._articleid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShowAll";
            parameters[i].Value = this._showall;
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
            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("TelesalePluginCode", "ReadByType");
            return result;
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Tele_Hint";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ArticleID";
            parameters[0].Value = ArticleID;

            int result = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("TelesalePluginCode", "ReadByType");
            return result;
        }

        public static DC_Tele_Hint GetDC_Tele_Hint(string ArticleID)
        {
            string PROCEDURE = "p_SelectDC_Tele_Hint";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ArticleID";
            parameters[0].Value = ArticleID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Hint
            {
                FakeArticleID = Locdau.convertToUnSign3(row["ArticleID"].ToString()),
                ArticleID = row["ArticleID"].ToString(),
                Description = row["Description"].ToString(),
                ShowAll = Convert.ToBoolean(row["ShowAll"].ToString()),
                Organization = getlistOrg(row["ArticleID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Intro = row["Intro"].ToString(),
                TypeTab = row["TypeTab"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Tele_Hint> GetDC_Tele_Hints(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Tele_HintsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Tele_Hint
            {
                FakeArticleID = Locdau.convertToUnSign3(row["ArticleID"].ToString()),
                ArticleID = row["ArticleID"].ToString(),
                Description = row["Description"].ToString(),
                ShowAll = Convert.ToBoolean(row["ShowAll"].ToString()),
                Organization = getlistOrg(row["ArticleID"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Tele_Hint> GetAllDC_Tele_Hints()
        {
            string PROCEDURE = "p_SelectDC_Tele_HintsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tele_Hint
            {
                FakeArticleID = Locdau.convertToUnSign3(row["ArticleID"].ToString()),
                ArticleID = row["ArticleID"].ToString(),
                Description = row["Description"].ToString(),
                ShowAll = Convert.ToBoolean(row["ShowAll"].ToString()),
                Organization = row["Organization"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Intro = row["Intro"].ToString(),
                TypeTab = row["TypeTab"].ToString()
            }).ToList();
        }


        public static List<DC_Tele_Hint> GetAllDC_Tele_HintsByType(string Type)
        {
            string PROCEDURE = "p_SelectDC_Tele_HintsAllByType";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Type";
            parameters[i].Value = Type;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Hint
            {
                FakeArticleID = Locdau.convertToUnSign3(row["ArticleID"].ToString()),
                ArticleID = row["ArticleID"].ToString(),
                Description = row["Description"].ToString(),
                ShowAll = Convert.ToBoolean(row["ShowAll"].ToString()),
                Organization = row["Organization"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Intro = row["Intro"].ToString(),
                TypeTab = row["TypeTab"].ToString()
            }).ToList();
        }

        public static List<DC_Tele_Hint> GetAllDC_Tele_HintsByPromotion(string Type,string Organization)
        {
            string PROCEDURE = "p_SelectDC_Tele_HintsAllByPromotion";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Type";
            parameters[i].Value = Type;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Organization";
            parameters[i].Value = Organization;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Hint
            {
                FakeArticleID = Locdau.convertToUnSign3(row["ArticleID"].ToString()),
                ArticleID = row["ArticleID"].ToString(),
                Description = row["Description"].ToString(),
                ShowAll = Convert.ToBoolean(row["ShowAll"].ToString()),
                Organization = row["Organization"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Intro = row["Intro"].ToString(),
                TypeTab = row["TypeTab"].ToString()
            }).ToList();
        }

        public static string getlistOrg(string articleId)
        {
            string list = "";
            var hintmeta = DC_Tele_Hint_Meta.GetAllDC_Tele_Hint_Metas().Where(hm => hm.ArticleID == articleId);
            foreach (var item in hintmeta)
            {
                list += item.Value + ",";
            }
            return list;
        }

        //getlistOrtherHint
        private string _customerID = String.Empty;
        public string CustomerID { get { return _customerID; } set { this._customerID = value; } }

        private string _creditLimit = String.Empty;
        public string CreditLimit { get { return _creditLimit; } set { this._creditLimit = value; } }

        private string _creditUsable = String.Empty;
        public string CreditUsable { get { return _creditUsable; } set { this._creditUsable = value; } }

        private string _totalSpending = String.Empty;
        public string TotalSpending { get { return _totalSpending; } set { this._totalSpending = value; } }

        private string _last4MonthSpending = String.Empty;
        public string Last4MonthSpending { get { return _last4MonthSpending; } set { this._last4MonthSpending = value; } }

        private string _position = String.Empty;
        public string Position { get { return _position; } set { this._position = value; } }

        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _cashAdvanceLimit = String.Empty;
        public string CashAdvanceLimit { get { return _cashAdvanceLimit; } set { this._cashAdvanceLimit = value; } }

        private string _note = String.Empty;
        public string Note { get { return _note; } set { this._note = value; } }

        private string _creditLimitRules = String.Empty;
        public string CreditLimitRules { get { return _creditLimitRules; } set { this._creditLimitRules = value; } }

        //get customer info hint
        public static async Task<List<DC_Tele_Hint>> GetOthersHintCustomer(string customerID)
        {
            string PROCEDURE = "p_DC_getInfoCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = customerID;


            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_Tele_Hint
            {
                CustomerID = row["CustomerID"].ToString(),
                CreditLimit = row["CreditLimit"].ToString(),
                CreditUsable = row["CreditUsable"].ToString(),
                TotalSpending = row["TotalSpending"].ToString(),
                Last4MonthSpending = row["Last4MonthSpending"].ToString(),
                Position = row["Position"].ToString(),
                Email = row["Email"].ToString(),
                CashAdvanceLimit = row["CashAdvanceLimit"].ToString(),
                Note = row["Note"].ToString(),
                CreditLimitRules = row["CreditLimitRules"].ToString()
            }).ToList();
        }


        //get spending info hint
        public static List<DC_Tele_Hint> GetSpendingInfoCustomer(string customerID)
        {
            string PROCEDURE = "p_DC_getSpendingInfoCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CustomerID";
            parameters[0].Value = customerID;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tele_Hint
            {
                TotalSpending = row["TotalSpending"].ToString(),
                Last4MonthSpending = row["Last4MonthSpending"].ToString(),
            }).ToList();
        }

    }
}