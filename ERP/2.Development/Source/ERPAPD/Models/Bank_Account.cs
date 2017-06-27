using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class Bank_Account
    {
        #region Members
        private string _id;
        public string ID { get { return _id; } set { this._id = value; } }

        private string _bankaccountnumber;
        public string BankAccountNumber { get { return _bankaccountnumber; } set { this._bankaccountnumber = value; } }

        private string _bankaccountname;
        public string BankAccountName { get { return _bankaccountname; } set { this._bankaccountname = value; } }

        private string _bankid;
        public string BankID { get { return _bankid; } set { this._bankid = value; } }

        public string BankName { get; set; }

        private string _bankbranchid;
        public string BankBranchID { get { return _bankbranchid; } set { this._bankbranchid = value; } }

        public string BankBranchName { get; set; }

        private bool _isdefault;
        public bool isDefault { get { return _isdefault; } set { this._isdefault = value; } }

        private string _UserName;
        public string UserName { get { return _UserName; } set { this._UserName = value; } }

        public DateTime CreatedDatetime { get; set; }

        public string CreatedUser { get; set; }

        public DateTime LastUpdatedDateTime { get; set; }

        public string LastUpdatedUser { get; set; }


        #endregion

        public static List<Bank_Account> Get_BankAccounts(string UserName)
        {
            string PROCEDURE = "p_SelectBank_Account";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserName";
            parameters[0].Value = UserName;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Account
            {
                ID = row["ID"].ToString(),
                BankAccountNumber = row["BankAccountNumber"].ToString(),
                BankAccountName = row["BankAccountName"].ToString(),
                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString(),
                BankBranchID = row["BankBranchID"].ToString(),
                BankBranchName = row["BankBranchName"].ToString(),
                isDefault = Convert.ToBoolean(row["isDefault"].ToString()),
                CreatedDatetime = DateTime.Parse(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = DateTime.Parse(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        /// <summary>
        /// check Bank Account exist in bank
        /// </summary>
        /// <param name="BankAccountNumber"></param>
        /// <param name="BankBranchID"></param>
        /// <returns></returns>
        public static Bank_Account Get_BankAccounts(string BankAccountNumber, string BankBranchID)
        {
            string PROCEDURE = "p_SelectBank_AccountNumber";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankAccountNumber";
            parameters[i].Value = BankAccountNumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = BankBranchID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Account
            {
                ID = row["ID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static Bank_Account CheckExist(string BankAccountNumber, string BankBranchID, string ID)
        {
            string PROCEDURE = "p_SelectBank_AccountNumber_isExist";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankAccountNumber";
            parameters[i].Value = BankAccountNumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = BankBranchID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = ID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Account
            {
                ID = row["ID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static Bank_Account CheckExistBankAccountForBranch(string BankBranchID)
        {
            string PROCEDURE = "p_SelectBank_AccountNumber_FromBranch";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankBranchID";
            parameters[0].Value = BankBranchID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Account
            {
                BankAccountNumber = row["BankAccountNumber"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static Bank_Account CheckExistBankAccountForBank(string BankID)
        {
            string PROCEDURE = "p_SelectBank_AccountNumber_FromBank";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Account
            {
                BankAccountNumber = row["BankAccountNumber"].ToString(),
            }).ToList().FirstOrDefault();
        }
        public int Save()
        {
            string PROCEDURE = "p_SaveBank_Account";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankAccountNumber";
            parameters[i].Value = this.BankAccountNumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankAccountName";
            parameters[i].Value = this.BankAccountName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = this.BankBranchID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@isDefault";
            parameters[i].Value = this.isDefault;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserName";
            parameters[i].Value = this.UserName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this.CreatedUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this.CreatedDatetime;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateBank_Account";
            SqlParameter[] parameters = new SqlParameter[8];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankAccountNumber";
            parameters[i].Value = this.BankAccountNumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankAccountName";
            parameters[i].Value = this.BankAccountName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = this.BankBranchID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@isDefault";
            parameters[i].Value = this.isDefault;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserName";
            parameters[i].Value = this.UserName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this.LastUpdatedUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this.LastUpdatedDateTime;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteBank_Account";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = this.ID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
    }
}