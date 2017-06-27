using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using WebMatrix.WebData;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Company_Appointment.
    /// </summary>
    public class DC_Company_Appointment
    {
        #region Members
        private string _appointmentid = String.Empty;
        public string AppointmentID { get { return _appointmentid; } set { this._appointmentid = value; } }

        private DateTime _startdatetime;
        public DateTime StartDateTime { get { return _startdatetime; } set { this._startdatetime = value; } }

        private DateTime _enddatetime;
        public DateTime EndDateTime { get { return _enddatetime; } set { this._enddatetime = value; } }

        private string _duration = String.Empty;
        public string Duration { get { return _duration; } set { this._duration = value; } }

        private string _location = String.Empty;
        public string Location { get { return _location; } set { this._location = value; } }

        private string _subject = String.Empty;
        public string Subject { get { return _subject; } set { this._subject = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }

        private string _guest = String.Empty;
        public string Guest { get { return _guest; } set { this._guest = value; } }

        private string _follower = String.Empty;
        public string Follower { get { return _follower; } set { this._follower = value; } }

        private string _assignee = String.Empty;
        public string Assignee { get { return _assignee; } set { this._assignee = value; } }

        private string _activitytype = String.Empty;
        public string ActivityType { get { return _activitytype; } set { this._activitytype = value; } }

        private string _contacttype = String.Empty;
        public string ContactType { get { return _contacttype; } set { this._contacttype = value; } }

        private string _contactto = String.Empty;
        public string ContactTo { get { return _contactto; } set { this._contactto = value; } }

        private string _reminder = String.Empty;
        public string Reminder { get { return _reminder; } set { this._reminder = value; } }

        private string _status = String.Empty;
        public string Status { get { return _status; } set { this._status = value; } }

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

        public string ContactName { get; set; }
        public string StatusBG { get; set; }
        public string CurrUser { get; set; }
        public int countMyAppointment { get; set; }
        #endregion

        public DC_Company_Appointment()
        { }

        public DC_Company_Appointment(string AppointmentID, DateTime StartDateTime, DateTime EndDateTime, string Duration, string Location, string Subject, string Description, string Guest, string Follower, string Assignee, string ActivityType, string ContactType, string ContactTo, string Reminder, string Status, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._appointmentid = AppointmentID;
            this._startdatetime = StartDateTime;
            this._enddatetime = EndDateTime;
            this._duration = Duration;
            this._location = Location;
            this._subject = Subject;
            this._description = Description;
            this._guest = Guest;
            this._follower = Follower;
            this._assignee = Assignee;
            this._activitytype = ActivityType;
            this._contacttype = ContactType;
            this._contactto = ContactTo;
            this._reminder = Reminder;
            this._status = Status;
            this._xmlstring = XMLString;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Company_Appointment";
            SqlParameter[] parameters = new SqlParameter[19];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AppointmentID";
            parameters[i].Value = this._appointmentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartDateTime";
            parameters[i].Value = this._startdatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndDateTime";
            parameters[i].Value = this._enddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Duration";
            parameters[i].Value = this._duration;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Location";
            parameters[i].Value = this._location;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Subject";
            parameters[i].Value = this._subject;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Guest";
            parameters[i].Value = this._guest;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Follower";
            parameters[i].Value = this._follower;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assignee";
            parameters[i].Value = this._assignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActivityType";
            parameters[i].Value = this._activitytype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactType";
            parameters[i].Value = this._contacttype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactTo";
            parameters[i].Value = this._contactto;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Reminder";
            parameters[i].Value = this._reminder;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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
            string PROCEDURE = "p_UpdateDC_Company_Appointment";
            SqlParameter[] parameters = new SqlParameter[19];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AppointmentID";
            parameters[i].Value = this._appointmentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartDateTime";
            parameters[i].Value = this._startdatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndDateTime";
            parameters[i].Value = this._enddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Duration";
            parameters[i].Value = this._duration;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Location";
            parameters[i].Value = this._location;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Subject";
            parameters[i].Value = this._subject;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Guest";
            parameters[i].Value = this._guest;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Follower";
            parameters[i].Value = this._follower;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assignee";
            parameters[i].Value = this._assignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActivityType";
            parameters[i].Value = this._activitytype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactType";
            parameters[i].Value = this._contacttype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactTo";
            parameters[i].Value = this._contactto;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Reminder";
            parameters[i].Value = this._reminder;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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

        public int UpdateStatus()
        {
            string PROCEDURE = "p_UpdateDC_Company_AppointmentStatus";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AppointmentID";
            parameters[i].Value = this._appointmentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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
            string PROCEDURE = "p_DeleteDC_Company_Appointment";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AppointmentID";
            parameters[0].Value = AppointmentID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Company_Appointment GetDC_Company_Appointment(string AppointmentID)
        {
            string PROCEDURE = "p_SelectDC_Company_Appointment";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AppointmentID";
            parameters[0].Value = AppointmentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                AppointmentID = row["AppointmentID"].ToString(),
                StartDateTime = Convert.ToDateTime(row["StartDateTime"].ToString()),
                EndDateTime = Convert.ToDateTime(row["EndDateTime"].ToString()),
                Duration = row["Duration"].ToString(),
                Location = row["Location"].ToString(),
                Subject = row["Subject"].ToString(),
                Description = row["Description"].ToString(),
                Guest = row["Guest"].ToString(),
                Follower = row["Follower"].ToString(),
                Assignee = row["Assignee"].ToString(),
                ActivityType = row["ActivityType"].ToString(),
                ContactType = row["ContactType"].ToString(),
                ContactTo = row["ContactTo"].ToString(),
                Reminder = row["Reminder"].ToString(),
                Status = row["Status"].ToString(),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ContactName = row["ContactName"].ToString(),
               // CurrUser = WebSecurity.CurrentUserName
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Company_Appointment> GetDC_Company_Appointments(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Company_AppointmentsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                AppointmentID = row["AppointmentID"].ToString(),
                StartDateTime = Convert.ToDateTime(row["StartDateTime"].ToString()),
                EndDateTime = Convert.ToDateTime(row["EndDateTime"].ToString()),
                Duration = row["Duration"].ToString(),
                Location = row["Location"].ToString(),
                Subject = row["Subject"].ToString(),
                Description = HttpUtility.HtmlDecode(row["Description"].ToString()),
                Guest = row["Guest"].ToString(),
                Follower = row["Follower"].ToString(),
                Assignee = row["Assignee"].ToString(),
                ActivityType = row["ActivityType"].ToString(),
                ContactType = row["ContactType"].ToString(),
                ContactTo = row["ContactTo"].ToString(),
                Reminder = row["Reminder"].ToString(),
                Status = row["Status"].ToString(),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ContactName = row["ContactName"].ToString(),
                StatusBG = row["StatusBG"].ToString()
            }).ToList();
        }

        public static List<DC_Company_Appointment> GetAllDC_Company_Appointments()
        {
            string PROCEDURE = "p_SelectDC_Company_AppointmentsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                AppointmentID = row["AppointmentID"].ToString(),
                StartDateTime = Convert.ToDateTime(row["StartDateTime"].ToString()),
                EndDateTime = Convert.ToDateTime(row["EndDateTime"].ToString()),
                Duration = row["Duration"].ToString(),
                Location = row["Location"].ToString(),
                Subject = row["Subject"].ToString(),
                Description = row["Description"].ToString(),
                Guest = row["Guest"].ToString(),
                Follower = row["Follower"].ToString(),
                Assignee = row["Assignee"].ToString(),
                ActivityType = row["ActivityType"].ToString(),
                ContactType = row["ContactType"].ToString(),
                ContactTo = row["ContactTo"].ToString(),
                Reminder = row["Reminder"].ToString(),
                Status = row["Status"].ToString(),
                XMLString = row["XMLString"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Company_Appointment> GetAllDC_Company_AppointmentsRemindApp(string currUser)
        {
            string PROCEDURE = "p_SelectDC_Company_AppointmentRemindApp";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@currUser";
            parameters[i].Value = currUser;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                StartDateTime = Convert.ToDateTime(row["StartDateTime"].ToString()),
                Duration = row["Duration"].ToString(),
                Location = row["Location"].ToString(),
                Subject = row["Subject"].ToString(),
                Assignee = row["Assignee"].ToString(),
                Des1 = row["Des1"].ToString(),
                Des2 = row["Des2"].ToString(),
                BGColor = row["BGColor"].ToString(),
                AppointmentID = row["AppointmentID"].ToString(),
                TimeString = Convert.ToDateTime(row["StartDateTime"].ToString()).ToString("dd-MM-yyyy HH:mm")
            }).ToList();
        }

        public static DC_Company_Appointment GetDC_CheckCurrentAppointment(string user)
        {
            string PROCEDURE = "p_GetCurrentAppointment";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = user;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                countMyAppointment = int.Parse(row["CountMyAppointment"].ToString())
            }).ToList().FirstOrDefault();
        }

        public static DC_Company_Appointment CheckExitContactToID(string @ID)
        {
            string PROCEDURE = "p_CheckExitContactToID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = ID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                ContactTo = row["ID"].ToString(),
                ContactName = row["Name"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public string Des1 { get; set; }
        public string Des2 { get; set; }
        public string BGColor { get; set; }
        public string TimeString { get; set; }
        public string Email { set; get; }
        public static DC_Company_Appointment GetUserSendMail(string userid)
        {
            string PROCEDURE = "select Email from [User] where UserID = '" + userid + "'";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Appointment
            {
                Email = row["Email"].ToString(),
            }).ToList().FirstOrDefault();
        }
    }
}
