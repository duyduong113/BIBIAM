using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class Bank_Branch
    {
        #region Members
        private string _bankbranchid;
        public string BankBranchID { get { return _bankbranchid; } set { this._bankbranchid = value; } }

        private string _bankbranchname;
        public string BankBranchName { get { return _bankbranchname; } set { this._bankbranchname = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private string _bankid;
        public string BankID { get { return _bankid; } set { this._bankid = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get { return _createddatetime; } set { this._createddatetime = value; } }

        private string _createduser;
        public string CreatedUser { get { return _createduser; } set { this._createduser = value; } }

        private DateTime _lastupdateddatetime;
        public DateTime LastUpdatedDateTime { get { return _lastupdateddatetime; } set { this._lastupdateddatetime = value; } }

        private string _lastupdateduser;
        public string LastUpdatedUser { get { return _lastupdateduser; } set { this._lastupdateduser = value; } }
        #endregion

        public static List<Bank_Branch> GetAllBank_Branchs()
        {
            string PROCEDURE = "p_SelectBank_BranchsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
                BankBranchName = row["BankBranchName"].ToString(),
                BankID = row["BankID"].ToString(),
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = DateTime.Parse(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = DateTime.Parse(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<Bank_Branch> GetBank_Branch(string BankID)
        {
            string PROCEDURE = "p_SelectBank_Branch";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
                BankBranchName = row["BankBranchName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = DateTime.Parse(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = DateTime.Parse(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static Bank_Branch GetBank_BranchID(string BankBranchID)
        {
            string PROCEDURE = "p_SelectBank_BranchID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankBranchID";
            parameters[0].Value = BankBranchID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
                BankID = row["BankID"].ToString(),
                BankBranchName = row["BankBranchName"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static Bank_Branch GetBank_BranchName(string BankBranchID, string BankID)
        {
            string PROCEDURE = "p_SelectBank_BranchName";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = BankBranchID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = BankID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
                BankBranchName = row["BankBranchName"].ToString(),
                BankID = row["BankID"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static Bank_Branch CheckBranch(string BankBranchName, string BankID) 
        {
            string PROCEDURE = "p_SelectBank_Branch_fromBranchName";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchName";
            parameters[i].Value = BankBranchName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = BankID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static Bank_Branch CheckBranchName(string BankBranchID, string BankBranchName)
        {
            string PROCEDURE = "p_SelectBank_Branch_fromBranchID";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = BankBranchID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchName";
            parameters[i].Value = BankBranchName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static Bank_Branch CheckBank_BranchExist(string BankBranchID, string BankBranchName,string BankID)
        {
            string PROCEDURE = "p_SelectBank_BranchName_isExist";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = BankBranchID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchName";
            parameters[i].Value = BankBranchName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = BankID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank_Branch
            {
                BankBranchID = row["BankBranchID"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveBank_Branch";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchName";
            parameters[i].Value = this._bankbranchname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this._createddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this._createduser;
            i++;
            foreach (var x in parameters)
            {
                if (x.Value == null)
                {
                    x.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }


        public int Update()
        {
            string PROCEDURE = "p_UpdateBank_Branch";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchID";
            parameters[i].Value = this._bankbranchid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankBranchName";
            parameters[i].Value = this._bankbranchname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this._lastupdateddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;

            foreach (var x in parameters)
            {
                if (x.Value == null)
                {
                    x.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }


        public int Delete()
        {
            string PROCEDURE = "p_DeleteBank_Branch";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankBranchID";
            parameters[0].Value = BankBranchID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

    }
}