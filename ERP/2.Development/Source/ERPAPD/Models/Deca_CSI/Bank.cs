using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class Bank
    {
        #region Members
        private string _bankid;
        public string BankID { get { return _bankid; } set { this._bankid = value; } }
       
        private string _bankname;
        public string BankName { get { return _bankname; } set { this._bankname = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get { return _createddatetime; } set { this._createddatetime = value; } }

        private string _createduser;
        public string CreatedUser { get { return _createduser; } set { this._createduser = value; } }

        private DateTime _lastupdateddatetime;
        public DateTime LastUpdatedDateTime { get { return _lastupdateddatetime; } set { this._lastupdateddatetime = value; } }

        private string _lastupdateduser;
        public string LastUpdatedUser { get { return _lastupdateduser; } set { this._lastupdateduser = value; } }
        #endregion

        public static List<Bank> GetAllBanks()
        {
            string PROCEDURE = "p_SelectBanksAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Bank
            {
                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<Bank> GetAllBanks_isActive()
        {
            string PROCEDURE = "p_SelectBanksAll_isActive";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Bank
            {
                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveBank";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankName";
            parameters[i].Value = this._bankname;
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
            string PROCEDURE = "p_UpdateBank";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = this._bankid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankName";
            parameters[i].Value = this._bankname;
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
            string PROCEDURE = "p_DeleteBank";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static Bank GetBank(string BankID)
        {
            string PROCEDURE = "p_SelectBank";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankID";
            parameters[0].Value = BankID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank
            {

                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = DateTime.Parse(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = DateTime.Parse(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static Bank GetBankName(string BankName)
        {
            string PROCEDURE = "p_SelectBankName";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BankName";
            parameters[0].Value = BankName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank
            {
                BankID = row["BankID"].ToString(),
                BankName = row["BankName"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static Bank CheckBankExist(string BankID,string BankName)
        {
            string PROCEDURE = "p_SelectBankName_isExist";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankID";
            parameters[i].Value = BankID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BankName";
            parameters[i].Value = BankName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Bank
            {
                BankID = row["BankID"].ToString()
            }).ToList().FirstOrDefault();
        }


    }
}