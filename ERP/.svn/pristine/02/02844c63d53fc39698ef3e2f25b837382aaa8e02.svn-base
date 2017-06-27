using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Position.
    /// </summary>
    public class DC_Position
    {
        #region Members
        private int _positionid;
        public int PositionID { get { return _positionid; } set { this._positionid = value; } }

        private string _positionname = String.Empty;
        public string PositionName { get { return _positionname; } set { this._positionname = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get { return _createddatetime; } set { this._createddatetime = value; } }

        private string _createduser = String.Empty;
        public string CreatedUser { get { return _createduser; } set { this._createduser = value; } }

        private DateTime _lastupdateddatetime;
        public DateTime LastUpdatedDateTime { get { return _lastupdateddatetime; } set { this._lastupdateddatetime = value; } }

        private string _lastupdateduser = String.Empty;
        public string LastUpdatedUser { get { return _lastupdateduser; } set { this._lastupdateduser = value; } }

        #endregion

        public DC_Position()
        { }

        public DC_Position(int PositionID, string PositionName, bool Active, DateTime CreatedDatetime, string CreatedUser, DateTime LastUpdatedDateTime, string LastUpdatedUser)
        {
            this._positionid = PositionID;
            this._positionname = PositionName;
            this._active = Active;
            this._createddatetime = CreatedDatetime;
            this._createduser = CreatedUser;
            this._lastupdateddatetime = LastUpdatedDateTime;
            this._lastupdateduser = LastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Position";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PositionName";
            parameters[i].Value = this._positionname;
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
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this._lastupdateddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Position";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PositionID";
            parameters[i].Value = this._positionid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PositionName";
            parameters[i].Value = this._positionname;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Position";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@PositionID";
            parameters[0].Value = PositionID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Position GetDC_Position(int PositionID)
        {
            string PROCEDURE = "p_SelectDC_Position";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@PositionID";
            parameters[0].Value = PositionID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Position
            {
                PositionID = Convert.ToInt32(row["PositionID"].ToString()),
                PositionName = row["PositionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Position> GetDC_Positions(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_PositionsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Position
            {
                PositionID = Convert.ToInt32(row["PositionID"].ToString()),
                PositionName = row["PositionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Position> GetAllDC_Positions()
        {
            string PROCEDURE = "p_SelectDC_PositionsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Position
            {
                PositionID = Convert.ToInt32(row["PositionID"].ToString()),
                PositionName = row["PositionName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Position> GetPositions()
        {
            string PROCEDURE = "SELECT PositionID,PositionName FROM DC_Position where active =1 order by PositionName";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Position
            {
                PositionID = Convert.ToInt32(row["PositionID"].ToString()),
                PositionName = row["PositionName"].ToString(),
                
            }).ToList();
        }
    }
    public class DC_Position_Level
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string LevelID { get; set; }
        public string Description { get; set; }
        public string PositionID { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
    }
}
