using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Company_Contact_List.
    /// </summary>
    public class DC_Company_Contact_List
    {
        #region Members
        private string _contactid = String.Empty;
        public string ContactID { get { return _contactid; } set { this._contactid = value; } }

        private string _companyid = String.Empty;
        public string CompanyID { get { return _companyid; } set { this._companyid = value; } }

        private string _fullname = String.Empty;
        public string FullName { get { return _fullname; } set { this._fullname = value; } }

        private bool _gender;
        public bool Gender { get { return _gender; } set { this._gender = value; } }

        private DateTime _birthday;
        public DateTime Birthday { get { return _birthday; } set { this._birthday = value; } }

        private string _mobile = String.Empty;
        public string Mobile { get { return _mobile; } set { this._mobile = value; } }

        private string _officephone = String.Empty;
        public string Officephone { get { return _officephone; } set { this._officephone = value; } }

        private string _fax = String.Empty;
        public string Fax { get { return _fax; } set { this._fax = value; } }

        private string _email = String.Empty;
        public string Email { get { return _email; } set { this._email = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }

        private string _position = String.Empty;
        public string Position { get { return _position; } set { this._position = value; } }

        private string _leadby = String.Empty;
        public string Leadby { get { return _leadby; } set { this._leadby = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }

        private string _note = String.Empty;
        public string Note { get { return _note; } set { this._note = value; } }

        private string _xmlstring = String.Empty;
        public string XMLString { get { return _xmlstring; } set { this._xmlstring = value; } }

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

        public string CompanyName { get; set; }
        public string FullNameLead { get; set; }
        public string GenderName { get; set; }

        public string ContactToName { get; set; }
        public string ContactToID { get; set; }
        #endregion

        public DC_Company_Contact_List()
        { }

        public DC_Company_Contact_List(string ContactID, string CompanyID, string FullName, bool Gender, DateTime Birthday, string Mobile, string Officephone, string Fax, string Email, string Address, string Position, string Leadby, string Description, string Note, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._contactid = ContactID;
            this._companyid = CompanyID;
            this._fullname = FullName;
            this._gender = Gender;
            this._birthday = Birthday;
            this._mobile = Mobile;
            this._officephone = Officephone;
            this._fax = Fax;
            this._email = Email;
            this._address = Address;
            this._position = Position;
            this._leadby = Leadby;
            this._description = Description;
            this._note = Note;
            this._xmlstring = XMLString;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Company_Contact_List";
            SqlParameter[] parameters = new SqlParameter[17];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactID";
            parameters[i].Value = this._contactid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyID";
            parameters[i].Value = this._companyid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Gender";
            parameters[i].Value = this._gender;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Birthday";
            parameters[i].Value = this._birthday;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Mobile";
            parameters[i].Value = this._mobile;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Officephone";
            parameters[i].Value = this._officephone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Position";
            parameters[i].Value = this._position;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Leadby";
            parameters[i].Value = this._leadby;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = this._xmlstring;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Company_Contact_List";
            SqlParameter[] parameters = new SqlParameter[18];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactID";
            parameters[i].Value = this._contactid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CompanyID";
            parameters[i].Value = this._companyid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this._fullname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Gender";
            parameters[i].Value = this._gender;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Birthday";
            parameters[i].Value = this._birthday;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Mobile";
            parameters[i].Value = this._mobile;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Officephone";
            parameters[i].Value = this._officephone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fax";
            parameters[i].Value = this._fax;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this._email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Position";
            parameters[i].Value = this._position;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Leadby";
            parameters[i].Value = this._leadby;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = this._xmlstring;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this._rowid;
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
            string PROCEDURE = "p_DeleteDC_Company_Contact_List";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ContactID";
            parameters[0].Value = ContactID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Company_Contact_List GetDC_Company_Contact_List(string ContactID)
        {
            string PROCEDURE = "p_SelectDC_Company_Contact_List";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ContactID";
            parameters[0].Value = ContactID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                ContactID = row["ContactID"].ToString(),
                CompanyID = row["CompanyID"].ToString(),
                FullName = row["FullName"].ToString(),
                Gender = Convert.ToBoolean(row["Gender"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                Mobile = row["Mobile"].ToString(),
                Officephone = row["Officephone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Position = row["Position"].ToString(),
                Leadby = row["Leadby"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                GenderName = row["GenderName"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Company_Contact_List> GetUserCreate()
        {
            string PROCEDURE = "SELECT distinct RowCreatedUser FROM DC_Company_Contact_List";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList();
        }
        public static List<DC_Company_Contact_List> GetDC_Company_Contact_Lists(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Company_Contact_ListsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                ContactID = row["ContactID"].ToString(),
                CompanyID = row["CompanyID"].ToString(),
                FullName = row["FullName"].ToString(),
                Gender = Convert.ToBoolean(row["Gender"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                Mobile = row["Mobile"].ToString(),
                Officephone = row["Officephone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Position = row["Position"].ToString(),
                Leadby = row["Leadby"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                //XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                FullNameLead = row["FullNameLead"].ToString(),
                GenderName = row["GenderName"].ToString()
            }).ToList();
        }

        public static List<DC_Company_Contact_List> GetAllDC_Company_Contact_Lists()
        {
            string PROCEDURE = "p_SelectDC_Company_Contact_ListsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                ContactID = row["ContactID"].ToString(),
                CompanyID = row["CompanyID"].ToString(),
                FullName = row["FullName"].ToString(),
                Gender = Convert.ToBoolean(row["Gender"].ToString()),
                Birthday = Convert.ToDateTime(row["Birthday"].ToString()),
                Mobile = row["Mobile"].ToString(),
                Officephone = row["Officephone"].ToString(),
                Fax = row["Fax"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Position = row["Position"].ToString(),
                Leadby = row["Leadby"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Company_Contact_List> GetAllDC_Company_Contact_ListsByInfo()
        {
            string PROCEDURE = "SELECT ContactID,FullName FROM DC_Company_Contact_List Order by FullName asc";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                ContactID = row["ContactID"].ToString(),
                FullName = row["FullName"].ToString()
            }).ToList();
        }

        public static List<DC_Company_Contact_List> GetListDC_Company_Contact_Lists(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_ContactList_ListDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                ContactToID = row["ContactID"].ToString(),
                CompanyID = row["CompanyID"].ToString(),
                ContactToName = row["ContactID"].ToString() + "-" + row["FullName"].ToString(),               
            }).ToList();
        }

        public static List<DC_Company_Contact_List> GetMainContactByLeader(string leader)
        {
            string str = string.Empty;
            if (leader == "")
            {
                str = "SELECT ContactID,FullName FROM DC_Company_Contact_List Order by FullName asc";
            }
            else
            {
                str = "SELECT ContactID,FullName FROM DC_Company_Contact_List WHERE Leadby = '" + leader + "' Order by FullName asc";

            }

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, str, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Contact_List
            {
                ContactID = row["ContactID"].ToString(),
                FullName = row["FullName"].ToString()
            }).ToList();
        }
    }
}
