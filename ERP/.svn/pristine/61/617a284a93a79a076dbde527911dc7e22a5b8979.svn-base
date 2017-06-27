using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
	/// This object represents the properties and methods of a DC_Company_Result.
	/// </summary>
	public class DC_Company_Result
	{
        #region Members
		private string _resultid = String.Empty;
        public string ResultID { get{ return _resultid; } set { this._resultid = value; } }
        
		private string _resultname = String.Empty;
        public string ResultName { get{ return _resultname; } set { this._resultname = value; } }
        
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

        private string _recommand = String.Empty;
        public string Recommand { get { return _recommand; } set { this._recommand = value; } }
        #endregion
        
        public DC_Company_Result()
        {}
        
        public DC_Company_Result(string ResultID, string ResultName, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser, string Recommand)
		{ 
            this._resultid = ResultID;  
            this._resultname = ResultName;  
            this._xmlstring = XMLString;  
            this._rowid = RowID;  
            this._rowcreatedtime = RowCreatedTime;  
            this._rowcreateduser = RowCreatedUser;  
            this._rowlastupdatedtime = RowLastUpdatedTime;  
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._recommand = Recommand;
		}
        
        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Company_Result";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultID";
            parameters[i].Value =string.IsNullOrEmpty(this._resultid) ? "" : this._resultid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultName";
            parameters[i].Value =string.IsNullOrEmpty(this._resultname) ? "" : this._resultname;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Recommand";
            parameters[i].Value = string.IsNullOrEmpty(this._recommand) ? "" : this._recommand;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Company_Result";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultID";
            parameters[i].Value =string.IsNullOrEmpty(this._resultid) ? "" : this._resultid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResultName";
            parameters[i].Value = string.IsNullOrEmpty(this._resultname) ? "" : this._resultname;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Recommand";
            parameters[i].Value = string.IsNullOrEmpty(this._recommand) ? "" : this._recommand;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Company_Result";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ResultID";
            parameters[0].Value = ResultID;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }


        public int ChangeStatusActive()
        {
            string PROCEDURE = "p_ActiveDC_Company_Result";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ResultID";
            parameters[0].Value = ResultID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Active";
            parameters[1].Value = Active;

            parameters[2] = new SqlParameter();
            parameters[2].ParameterName = "@RowLastUpdatedTime";
            parameters[2].Value = this._rowlastupdatedtime;

            parameters[3] = new SqlParameter();
            parameters[3].ParameterName = "@RowLastUpdatedUser";
            parameters[3].Value = this._rowlastupdateduser;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        public static DC_Company_Result GetDC_Company_Result(string ResultID)
        {
            string PROCEDURE = "p_SelectDC_Company_Result";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ResultID";
            parameters[0].Value = ResultID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
             return dt.AsEnumerable().Select(row => new DC_Company_Result
            {
                    ResultID = row["ResultID"].ToString(), 
                    ResultName = row["ResultName"].ToString(),
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt16(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                    Recommand = row["Recommand"].ToString()
            }).ToList().FirstOrDefault();
        }
        
        public static List<DC_Company_Result> GetDC_Company_Results(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Company_ResultsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company_Result
            {
                    ResultID = row["ResultID"].ToString(), 
                    ResultName = row["ResultName"].ToString(),
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt16(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                    Recommand = row["Recommand"].ToString()
            }).ToList();
        }
        
        public static List<DC_Company_Result> GetAllDC_Company_Results()
        {
            string PROCEDURE = "p_SelectDC_Company_ResultsAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Result
            {
                    ResultID = row["ResultID"].ToString(), 
                    ResultName = row["ResultName"].ToString(),
                    Active = Convert.ToBoolean( row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                    Recommand = row["Recommand"].ToString()
            }).ToList();
        }

        public bool Active { get; set; }
    }
}    
