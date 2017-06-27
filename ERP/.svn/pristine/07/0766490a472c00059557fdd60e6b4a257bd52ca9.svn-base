using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
	/// This object represents the properties and methods of a DC_Company_Scale.
	/// </summary>
	public class DC_Company_Scale
	{
        #region Members
		private string _scalesid = String.Empty;
        public string ScalesID { get{ return _scalesid; } set { this._scalesid = value; } }
        
		private string _scalesname = String.Empty;
        public string ScalesName { get{ return _scalesname; } set { this._scalesname = value; } }
        
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
        
        #endregion
        
        public DC_Company_Scale()
        {}
        
        public DC_Company_Scale(string ScalesID, string ScalesName, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
		{ 
            this._scalesid = ScalesID;  
            this._scalesname = ScalesName;  
            this._xmlstring = XMLString;  
            this._rowid = RowID;  
            this._rowcreatedtime = RowCreatedTime;  
            this._rowcreateduser = RowCreatedUser;  
            this._rowlastupdatedtime = RowLastUpdatedTime;  
            this._rowlastupdateduser = RowLastUpdatedUser; 
		}

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Company_Scale";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ScalesID";
            parameters[i].Value = string.IsNullOrEmpty(this._scalesid) ? "" : this._scalesid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ScalesName";
            parameters[i].Value = string.IsNullOrEmpty(this._scalesname) ? "" : this._scalesname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@XMLString";
            parameters[i].Value = string.IsNullOrEmpty(this._xmlstring) ? "" : this._xmlstring;
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
            string PROCEDURE = "p_UpdateDC_Company_Scale";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ScalesID";
            parameters[i].Value =string.IsNullOrEmpty(this._scalesid) ? "" : this._scalesid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ScalesName";
            parameters[i].Value =string.IsNullOrEmpty(this._scalesname) ? "" : this._scalesname;
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
            string PROCEDURE = "p_DeleteDC_Company_Scale";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ScalesID";
            parameters[0].Value = ScalesID;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int ChangeStatusActive()
        {
            string PROCEDURE = "p_ActiveDC_Company_Scales";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ScalesID";
            parameters[0].Value = ScalesID;

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

        public static DC_Company_Scale GetDC_Company_Scale(string ScalesID)
        {
            string PROCEDURE = "p_SelectDC_Company_Scale";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ScalesID";
            parameters[0].Value = ScalesID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
             return dt.AsEnumerable().Select(row => new DC_Company_Scale
            {
                    ScalesID = row["ScalesID"].ToString(), 
                    ScalesName = row["ScalesName"].ToString(),
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        
        public static List<DC_Company_Scale> GetDC_Company_Scales(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Company_ScalesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Company_Scale
            {
                    ScalesID = row["ScalesID"].ToString(), 
                    ScalesName = row["ScalesName"].ToString(),
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        
        public static List<DC_Company_Scale> GetAllDC_Company_Scales()
        {
            string PROCEDURE = "p_SelectDC_Company_ScalesAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Company_Scale
            {
                    ScalesID = row["ScalesID"].ToString(), 
                    ScalesName = row["ScalesName"].ToString(), 
                    XMLString = row["XMLString"].ToString(),
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public bool Active { get; set; }
    }
}    
