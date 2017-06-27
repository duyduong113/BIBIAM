using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
	/// This object represents the properties and methods of a DC_Stage_Definition.
	/// </summary>
	public class DC_Stage_Definition
	{
        #region Members
		private string _stageid = String.Empty;
        public string StageID { get{ return _stageid; } set { this._stageid = value; } }
        
		private string _description = String.Empty;
        public string Description { get{ return _description; } set { this._description = value; } }
        
		private bool _active;
        public bool Active { get{ return _active; } set { this._active = value; } }
        
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
        
        public DC_Stage_Definition()
        {}
        
        public DC_Stage_Definition(string StageID, string Description, bool Active, string XMLString, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
		{ 
            this._stageid = StageID;  
            this._description = Description;  
            this._active = Active;  
            this._xmlstring = XMLString;  
            this._rowid = RowID;  
            this._rowcreatedtime = RowCreatedTime;  
            this._rowcreateduser = RowCreatedUser;  
            this._rowlastupdatedtime = RowLastUpdatedTime;  
            this._rowlastupdateduser = RowLastUpdatedUser; 
		}
        
        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Stage_Definition";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StageID";
            parameters[i].Value =string.IsNullOrEmpty(this._stageid) ? "" : this._stageid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value =string.IsNullOrEmpty(this._description) ? "" : this._description;
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
            string PROCEDURE = "p_UpdateDC_Stage_Definition";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StageID";
            parameters[i].Value =string.IsNullOrEmpty(this._stageid) ? "" : this._stageid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value =string.IsNullOrEmpty(this._description) ? "" : this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
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
            string PROCEDURE = "p_DeleteDC_Stage_Definition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@StageID";
            parameters[0].Value = StageID;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public static DC_Stage_Definition GetDC_Stage_Definition(string StageID)
        {
            string PROCEDURE = "p_SelectDC_Stage_Definition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@StageID";
            parameters[0].Value = StageID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
             return dt.AsEnumerable().Select(row => new DC_Stage_Definition
            {
                    StageID = row["StageID"].ToString(), 
                    Description = row["Description"].ToString(), 
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        
        public static List<DC_Stage_Definition> GetDC_Stage_Definitions(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Stage_DefinitionsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Stage_Definition
            {
                    StageID = row["StageID"].ToString(), 
                    Description = row["Description"].ToString(), 
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        
        public static List<DC_Stage_Definition> GetAllDC_Stage_Definitions()
        {
            string PROCEDURE = "p_SelectDC_Stage_DefinitionsAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Stage_Definition
            {
                    StageID = row["StageID"].ToString(), 
                    Description = row["Description"].ToString(), 
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    XMLString = row["XMLString"].ToString(), 
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public int ChangeStatusActive()
        {
            string PROCEDURE = "p_ActiveDC_StageDefinition";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@StageID";
            parameters[0].Value = StageID;

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
    }
}    
