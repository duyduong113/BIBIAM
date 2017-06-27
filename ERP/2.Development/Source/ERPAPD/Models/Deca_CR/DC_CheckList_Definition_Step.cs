using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_CheckList_Definition_Step
    {
        #region Members
        private string _checklistsubid = String.Empty;
        public string ChecklistSubID { get { return _checklistsubid; } set { this._checklistsubid = value; } }

        private string _checklistid = String.Empty;
        public string ChecklistID { get { return _checklistid; } set { this._checklistid = value; } }

        private string _stepid = String.Empty;
        public string StepID { get { return _stepid; } set { this._stepid = value; } }

        private int _order;
        public int Order { get { return _order; } set { this._order = value; } }

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

        #endregion

        public DC_CheckList_Definition_Step()
        { }

        public DC_CheckList_Definition_Step(string ChecklistSubID, string ChecklistID, string StepID, int Order, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._checklistsubid = ChecklistSubID;
            this._checklistid = ChecklistID;
            this._stepid = StepID;
            this._order = Order;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_CheckList_Definition_Step";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistSubID";
            parameters[i].Value = this._checklistsubid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistID";
            parameters[i].Value = this._checklistid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StepID";
            parameters[i].Value = this._stepid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Order";
            parameters[i].Value = this._order;
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
            string PROCEDURE = "p_UpdateDC_CheckList_Definition_Step";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistSubID";
            parameters[i].Value = this._checklistsubid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ChecklistID";
            parameters[i].Value = this._checklistid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StepID";
            parameters[i].Value = this._stepid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Order";
            parameters[i].Value = this._order;
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
            string PROCEDURE = "p_DeleteDC_CheckList_Definition_Step";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ChecklistSubID";
            parameters[0].Value = ChecklistSubID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static List<DC_CheckList_Definition_Step> GetDC_CheckList_Definition_Step(string ChecklistID)
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_Step";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ChecklistID";
            parameters[0].Value = ChecklistID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_CheckList_Definition_Step
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                StepID = row["StepID"].ToString(),
                Order = Convert.ToInt32(row["Order"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Description = row["Description"].ToString(),
                Stage = row["Stage"].ToString(),
            }).ToList();
        }
        public string Stage { get; set; }
        public static List<DC_CheckList_Definition_Step> GetDC_CheckList_Definition_Steps(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_StepsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_CheckList_Definition_Step
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                StepID = row["StepID"].ToString(),
                Order = Convert.ToInt32(row["Order"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_CheckList_Definition_Step> GetAllDC_CheckList_Definition_Steps()
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_StepsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CheckList_Definition_Step
            {
                ChecklistSubID = row["ChecklistSubID"].ToString(),
                ChecklistID = row["ChecklistID"].ToString(),
                StepID = row["StepID"].ToString(),
                Order = Convert.ToInt32(row["Order"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        public static List<DC_CheckList_Definition_Step> GetStepList(string ChecklistID)
        {
            string PROCEDURE = "p_SelectDC_CheckList_Definition_Org_GetStepList";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ChecklistID";
            parameters[0].Value = ChecklistID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_CheckList_Definition_Step
            {
                Description = row["Description"].ToString(),
                StepID = row["StepID"].ToString(),
            }).ToList();
        }
        public string Description { get; set; }
    }
}