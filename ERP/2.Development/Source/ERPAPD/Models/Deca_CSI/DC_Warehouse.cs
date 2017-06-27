using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace ERPAPD.Models
{
    public class DC_WareHouse_List
    {
        public Dictionary<string, string> data;
        public DC_WareHouse_List()
        {
            this.data = new Dictionary<string, string>();
            var listdata = DC_WareHouse.GetAllDC_WareHouses();
            foreach (var d in listdata)
            {
                data.Add(d.WareHouseID, d.Name);
            }
        }

        public string SearchID(String CodeID)
        {
            try
            {
                return this.data[CodeID];
            }
            catch { }
            return "";
        }
    }

    public class DC_WareHouse
    {
        #region Members
		private string _warehouseid = String.Empty;
        public string WareHouseID { get{ return _warehouseid; } set { this._warehouseid = value; } }
        
    
		private string _name = String.Empty;
        public string Name { get { return _name; } set { this._name = value; } }
            
		private string _Address = String.Empty;
        public string Address { get{ return _Address; } set { this._Address = value; } }
        
		private string _warehousekeeper = String.Empty;
        public string WareHouseKeeper { get{ return _warehousekeeper; } set { this._warehousekeeper = value; } }
        
		private string _note = String.Empty;
        public string Note { get{ return _note; } set { this._note = value; } }
        
		private string _status = String.Empty;
        public string Status { get{ return _status; } set { this._status = value; } }
        
		private string _RowID = String.Empty;
        public string RowID { get{ return _RowID; } set { this._RowID = value; } }
        
		private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get{ return _rowcreatedtime; } set { this._rowcreatedtime = value; } }
        
		private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get{ return _rowcreateduser; } set { this._rowcreateduser = value; } }
        
		private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get{ return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }
        
		private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get{ return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        private string _ImportNote = String.Empty;
        public string ImportNote { get { return _ImportNote; } set { this._ImportNote = value; } }
        
        #endregion
        
        public DC_WareHouse()
        {}
        
        public DC_WareHouse(string WareHouseID, string Name, string WareHouseKeeper, string Note, string Status, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
		{ 
            this._warehouseid = WareHouseID;  
            this._name = Name;  
            this._warehousekeeper = WareHouseKeeper;  
            this._note = Note;  
            this._status = Status;  
            this._rowcreatedtime = RowCreatedTime;  
            this._rowcreateduser = RowCreatedUser;  
            this._rowlastupdatedtime = RowLastUpdatedTime;  
            this._rowlastupdateduser = RowLastUpdatedUser; 
		}
        
        public int Save()
        {
            string PROCEDURE = "p_SaveDC_WareHouse";
            SqlParameter[] parameters = new SqlParameter[7];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Name";
            parameters[i].Value = this._name;
            i++;
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._Address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseKeeper";
            parameters[i].Value = this._warehousekeeper;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
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
         public int UpdateID()
        {
            string PROCEDURE = "p_UpdateDC_WareHouseID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseID";
            parameters[i].Value = this._warehouseid;
           
           return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
     
        }
        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_WareHouse";
            SqlParameter[] parameters = new SqlParameter[8];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseID";
            parameters[i].Value = this._warehouseid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Name";
            parameters[i].Value = this._name;
            i++;
             parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Address";
            parameters[i].Value = this._Address;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseKeeper";
            parameters[i].Value = this._warehousekeeper;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this._note;
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
           return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
     
        }
        
        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_WareHouse";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@WareHouseID";
            parameters[0].Value = WareHouseID;
            
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }
        
        public static DC_WareHouse GetDC_WareHouse(string WareHouseID)
        {
            string PROCEDURE = "p_SelectDC_WareHouse";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@WareHouseID";
            parameters[0].Value = WareHouseID;
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
             return dt.AsEnumerable().Select(row => new DC_WareHouse
            {
                    WareHouseID = row["WareHouseID"].ToString(), 
                    Name = row["Name"].ToString(), 
                    Address = row["Address"].ToString(), 
                    WareHouseKeeper = row["WareHouseKeeper"].ToString(), 
                    Note = row["Note"].ToString(), 
                    Status = row["Status"].ToString(), 
                    RowID = row["RowID"].ToString(), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }


        public static DC_WareHouse GetStockInDate(string WareHouseID)
        {
            string PROCEDURE = "p_SelectDC_WareHouse";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@WareHouseID";
            parameters[0].Value = WareHouseID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_WareHouse
            {
                WareHouseID = row["WareHouseID"].ToString(),
                Name = row["Name"].ToString(),
                Address = row["Address"].ToString(),
                WareHouseKeeper = row["WareHouseKeeper"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                RowID = row["RowID"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        public static List<DC_WareHouse> GetDC_WareHouses(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_WareHousesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_WareHouse
            {
                    WareHouseID = row["WareHouseID"].ToString(), 
                    Name = row["Name"].ToString(), 
                    Address = row["Address"].ToString(),
                    WareHouseKeeper = row["WareHouseKeeper"].ToString(), 
                    Note = row["Note"].ToString(), 
                    Status = row["Status"].ToString(), 
                    RowID = row["RowID"].ToString(), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        
        public static List<DC_WareHouse> GetAllDC_WareHouses()
        {
            string PROCEDURE = "p_SelectDC_WareHousesAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_WareHouse
            {
                    WareHouseID = row["WareHouseID"].ToString(), 
                    Name = row["Name"].ToString(), 
                    Address = row["Address"].ToString(),
                    WareHouseKeeper = row["WareHouseKeeper"].ToString(), 
                    Note = row["Note"].ToString(), 
                    Status = row["Status"].ToString(),
                    RowID = row["RowID"].ToString(),  
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

       
    }
}    
