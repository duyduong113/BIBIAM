using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ERPAPD.Models;

namespace ERPAPD.Models
{
    public class Deca_Department
    {
        #region Members
		private int _departmentid;
        public int DepartmentID { get{ return _departmentid; } set { this._departmentid = value; } }
        
        private string _department = String.Empty;
        public string Department { get{ return _department; } set { this._department = value; } }
        
		private bool _active;
        public bool Active { get{ return _active; } set { this._active = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get{ return _createddatetime; } set { this._createddatetime = value; } }
        
        private string _createduser = String.Empty;
        public string CreatedUser { get{ return _createduser; } set { this._createduser = value; } }

        private DateTime _lastupdateddatetime;
        public DateTime LastUpdatedDateTime { get{ return _lastupdateddatetime; } set { this._lastupdateddatetime = value; } }

        private string _lastupdateduser = String.Empty;
        public string LastUpdatedUser { get{ return _lastupdateduser; } set { this._lastupdateduser = value; } }

        private string _Manager = String.Empty;
        public string Manager { get { return _Manager; } set { this._Manager = value; } }
        
        #endregion
        
        public Deca_Department()
        {}
        
        public Deca_Department(int DepartmentID, string Department, bool Active, DateTime CreatedDatetime, string CreatedUser, DateTime LastUpdatedDateTime, string LastUpdatedUser)
		{ 
            this._departmentid = DepartmentID;  
            this._department = Department;  
            this._active = Active;  
            this._createddatetime = CreatedDatetime;  
            this._createduser = CreatedUser;  
            this._lastupdateddatetime = LastUpdatedDateTime;  
            this._lastupdateduser = LastUpdatedUser; 
		}
        
        public int Save()
        {
            string PROCEDURE = "p_SaveDeca_Department";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this._department;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Manager";
            parameters[i].Value = this._Manager;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this._createddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this._createduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this._lastupdateddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public int Update()
        {
            string PROCEDURE = "p_UpdateDeca_Department";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DepartmentID";
            parameters[i].Value = this._departmentid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this._department;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Manager";
            parameters[i].Value = this._Manager;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this._lastupdateddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDeca_Department";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@DepartmentID";
            parameters[0].Value = DepartmentID;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public static Deca_Department GetDeca_Department(int DepartmentID)
        {
            string PROCEDURE = "p_SelectDeca_Department";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@DepartmentID";
            parameters[0].Value = DepartmentID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
             return dt.AsEnumerable().Select(row => new Deca_Department
            {
                    DepartmentID = Convert.ToInt32(row["DepartmentID"].ToString()), 
                    Department = row["Department"].ToString(), 
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()), 
                    CreatedUser = row["CreatedUser"].ToString(), 
                    LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()), 
                    LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        
        public static List<Deca_Department> GetDeca_Departments(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDeca_DepartmentsDynamic";
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
            return dt.AsEnumerable().Select(row => new Deca_Department
            {
                    DepartmentID = Convert.ToInt32(row["DepartmentID"].ToString()), 
                    Department = row["Department"].ToString(), 
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()), 
                    CreatedUser = row["CreatedUser"].ToString(), 
                    LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()), 
                    LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }
        
        public static List<Deca_Department> GetAllDeca_Departments()
        {
            string PROCEDURE = "p_SelectDeca_DepartmentsAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Department
            {
                    DepartmentID = Convert.ToInt32(row["DepartmentID"].ToString()), 
                    Department = row["Department"].ToString(), 
                    Active = Convert.ToBoolean(row["Active"].ToString()), 
                    CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()), 
                    CreatedUser = row["CreatedUser"].ToString(), 
                    LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()), 
                    LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                    Manager = row["MANAGER"].ToString()
            }).ToList();
        }
        public static List<Deca_Department> GetDeca_Departments_TrackingOrder()
        {
            string PROCEDURE = "select  distinct ISNULL (d.DepartmentID, '') As 'DepartmentID', ISNULL (d.Department, '') As 'Department' from Deca_Tracking_Order_ReasonMail r left join Deca_Department d on r.Department = d.DepartmentID";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Department
            {
                DepartmentID = Convert.ToInt32(row["DepartmentID"].ToString()),
                Department = row["Department"].ToString(),
            }).ToList();
        }
        public static List<Deca_Department> GetList()
        {
            string PROCEDURE = "select  DepartmentID, Department from Deca_Department where active =1  order by Department";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Department
            {
                DepartmentID = Convert.ToInt32(row["DepartmentID"].ToString()),
                Department = row["Department"].ToString(),
            }).ToList();
        }
    }
}