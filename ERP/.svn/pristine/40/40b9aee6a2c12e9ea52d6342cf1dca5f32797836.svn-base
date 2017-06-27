using ERPAPD.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class Deca_Business_Unit
    {
        #region Members
        private string _businessid = String.Empty;
        public string BusinessID { get { return _businessid; } set { this._businessid = value; } }

        private string _name = String.Empty;
        public string Name { get { return _name; } set { this._name = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }

        private string _phone = String.Empty;
        public string Phone { get { return _phone; } set { this._phone = value; } }

        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _fax = String.Empty;
        public string Fax { get { return _fax; } set { this._fax = value; } }

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

        private string _owner = String.Empty;
        public string Owner { get { return _owner; } set { this._owner = value; } }

        #endregion

        public Deca_Business_Unit()
        { }

        public Deca_Business_Unit(string BusinessID, string Name, string Description, string Phone, string Email, string Fax, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._businessid = BusinessID;
            this._name = Name;
            this._description = Description;
            this._phone = Phone;
            this._email = Email;
            this._fax = Fax;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDeca_Business_Unit";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BusinessID";
            parameters[i].Value = this._businessid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Name";
            parameters[i].Value = this._name;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
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
            parameters[i].ParameterName = "@Owner";
            parameters[i].Value = this._owner;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDeca_Business_Unit";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BusinessID";
            parameters[i].Value = this._businessid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Name";
            parameters[i].Value = this._name;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Phone";
            parameters[i].Value = this._phone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
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
            parameters[i].ParameterName = "@Owner";
            parameters[i].Value = this._owner;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDeca_Business_Unit";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BusinessID";
            parameters[0].Value = BusinessID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static Deca_Business_Unit GetDeca_Business_Unit(string BusinessID)
        {
            string PROCEDURE = "p_SelectDeca_Business_Unit";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@BusinessID";
            parameters[0].Value = BusinessID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_Business_Unit
            {
                BusinessID = row["BusinessID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Phone = row["Phone"].ToString(),
                Email = row["Email"].ToString(),
                Fax = row["Fax"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Owner = row["Owner"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static List<Deca_Business_Unit> GetDeca_Business_Units(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDeca_Business_UnitsDynamic";
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
            return dt.AsEnumerable().Select(row => new Deca_Business_Unit
            {
                BusinessID = row["BusinessID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Phone = row["Phone"].ToString(),
                Email = row["Email"].ToString(),
                Fax = row["Fax"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Owner = row["Owner"].ToString(),
            }).ToList();
        }

        public static List<Deca_Business_Unit> GetAllDeca_Business_Units()
        {
            string PROCEDURE = "p_SelectDeca_Business_UnitsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Business_Unit
            {
                BusinessID = row["BusinessID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Phone = row["Phone"].ToString(),
                Email = row["Email"].ToString(),
                Fax = row["Fax"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Owner = row["Owner"].ToString(),
            }).ToList();
        }
    }
}