using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
	/// This object represents the properties and methods of a DC_Company_Activity.
	/// </summary>
	public class DC_Company_Activity
	{
        #region Members
		private string _activityid = String.Empty;
        public string ActivityID { get{ return _activityid; } set { this._activityid = value; } }
        
		private string _description = String.Empty;
        public string Description { get{ return _description; } set { this._description = value; } }
        
		private string _activitytypeid = String.Empty;
        public string ActivityTypeID { get{ return _activitytypeid; } set { this._activitytypeid = value; } }
        
		private string _contacttypeid = String.Empty;
        public string ContactTypeID { get{ return _contacttypeid; } set { this._contacttypeid = value; } }
        
		private string _contacttoid = String.Empty;
        public string ContactToID { get{ return _contacttoid; } set { this._contacttoid = value; } }
        
		private DateTime _date;
        public DateTime Date { get{ return _date; } set { this._date = value; } }
        
		private string _resultid = String.Empty;
        public string ResultID { get{ return _resultid; } set { this._resultid = value; } }
        
		private string _assignee = String.Empty;
        public string Assignee { get{ return _assignee; } set { this._assignee = value; } }
        
		private string _xmlstring = String.Empty;
        public string XMLString { get{ return _xmlstring; } set { this._xmlstring = value; } }
        
		private int _rowid;
        public int RowID { get{ return _rowid; } set { this._rowid = value; } }
        
		private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get{ return _rowcreatedtime; } set { this._rowcreatedtime = value; } }
        
		private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get{ return _rowcreateduser; } set { this._rowcreateduser = value; } }
        
		private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get{ return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }
        
		private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get{ return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        public string ContactName { get; set; }


        private string _importnote = String.Empty;
        public string ImportNote { get { return _importnote; } set { this._importnote = value; } }
        
        #endregion
        
        public DC_Company_Activity()
        {}
        
        public DC_Company_Activity(string ImportNote, string ActivityID, string Description, string ActivityTypeID, string ContactTypeID, string ContactToID, DateTime Date, string ResultID, string Assignee, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
		{ 
            this._activityid = ActivityID;  
            this._description = Description;  
            this._activitytypeid = ActivityTypeID;  
            this._contacttypeid = ContactTypeID;  
            this._contacttoid = ContactToID;  
            this._date = Date;  
            this._resultid = ResultID;  
            this._assignee = Assignee;  
            this._xmlstring = XMLString;  
            this._rowid = RowID;  
            this._rowcreatedtime = RowCreatedTime;  
            this._rowcreateduser = RowCreatedUser;  
            this._rowlastupdatedtime = RowLastUpdatedTime;  
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._importnote = ImportNote;
		}
        
        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Company_Activity";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActivityID";
            parameters[i].Value =string.IsNullOrEmpty(this._activityid) ? "" : this._activityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value =string.IsNullOrEmpty(this._description) ? "" : this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActivityTypeID";
            parameters[i].Value =string.IsNullOrEmpty(this._activitytypeid) ? "" : this._activitytypeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactTypeID";
            parameters[i].Value =string.IsNullOrEmpty(this._contacttypeid) ? "" : this._contacttypeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactToID";
            parameters[i].Value =string.IsNullOrEmpty(this._contacttoid) ? "" : this._contacttoid;
            i++;
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Date";
            parameters[i].Value = this._date;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultID";
            parameters[i].Value =string.IsNullOrEmpty(this._resultid) ? "" : this._resultid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assignee";
            parameters[i].Value =string.IsNullOrEmpty(this._assignee) ? "" : this._assignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value =string.IsNullOrEmpty(this._xmlstring) ? "" : this._xmlstring;
            i++;         
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;           
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Company_Activity";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActivityID";
            parameters[i].Value =string.IsNullOrEmpty(this._activityid) ? "" : this._activityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value =string.IsNullOrEmpty(this._description) ? "" : this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActivityTypeID";
            parameters[i].Value =string.IsNullOrEmpty(this._activitytypeid) ? "" : this._activitytypeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactTypeID";
            parameters[i].Value =string.IsNullOrEmpty(this._contacttypeid) ? "" : this._contacttypeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ContactToID";
            parameters[i].Value =string.IsNullOrEmpty(this._contacttoid) ? "" : this._contacttoid;
            i++;
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Date";
            parameters[i].Value = this._date;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultID";
            parameters[i].Value =string.IsNullOrEmpty(this._resultid) ? "" : this._resultid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assignee";
            parameters[i].Value =string.IsNullOrEmpty(this._assignee) ? "" : this._assignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value =string.IsNullOrEmpty(this._xmlstring) ? "" : this._xmlstring;
            i++;           
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Company_Activity";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ActivityID";
            parameters[0].Value = ActivityID;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public static DC_Company_Activity GetDC_Company_Activity(string ActivityID)
        {
            string PROCEDURE = "p_SelectDC_Company_Activity";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ActivityID";
            parameters[0].Value = ActivityID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
             return dt.AsEnumerable().Select(row => new DC_Company_Activity
            {
                    ActivityID = row["ActivityID"].ToString(), 
                    Description = row["Description"].ToString(), 
                    ActivityTypeID = row["ActivityTypeID"].ToString(), 
                    ContactTypeID = row["ContactTypeID"].ToString(), 
                    ContactToID = row["ContactToID"].ToString(), 
                    Date = Convert.ToDateTime(row["Date"].ToString()), 
                    ResultID = row["ResultID"].ToString(), 
                    Assignee = row["Assignee"].ToString(), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt16(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        
        public static List<DC_Company_Activity> GetDC_Company_Activitys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Company_ActivitysDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Company_Activity
            {
                    ActivityID = row["ActivityID"].ToString(), 
                    Description = row["Description"].ToString(), 
                    ActivityTypeID = row["ActivityTypeID"].ToString(), 
                    ContactTypeID = row["ContactTypeID"].ToString(), 
                    ContactToID = row["ContactToID"].ToString(), 
                    Date = Convert.ToDateTime(row["Date"].ToString()), 
                    ResultID = row["ResultID"].ToString(), 
                    Assignee = row["Assignee"].ToString(), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt16(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                    ContactName = row["ContactName"].ToString()
            }).ToList();
        }
        
        public static List<DC_Company_Activity> GetAllDC_Company_Activitys()
        {
            string PROCEDURE = "p_SelectDC_Company_ActivitysAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Activity
            {
                    ActivityID = row["ActivityID"].ToString(), 
                    Description = row["Description"].ToString(), 
                    ActivityTypeID = row["ActivityTypeID"].ToString(), 
                    ContactTypeID = row["ContactTypeID"].ToString(), 
                    ContactToID = row["ContactToID"].ToString(), 
                    Date = Convert.ToDateTime(row["Date"].ToString()), 
                    ResultID = row["ResultID"].ToString(), 
                    Assignee = row["Assignee"].ToString(), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt16(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()                   
            }).ToList();
        }
    }
}    
