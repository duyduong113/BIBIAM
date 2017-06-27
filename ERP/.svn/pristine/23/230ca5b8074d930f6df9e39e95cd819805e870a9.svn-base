using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Ticket_KPI.
    /// </summary>
    public class DC_Ticket_KPI
    {
        #region Members

        public string KPIID { get; set; }


        public string TypeID { get; set; }


        public string Description { get; set; }


        public string Priority { get; set; }


        public string Impact { get; set; }


        public int ResponeTime { get; set; }


        public int ResolveTime { get; set; }


        public int ClosedTime { get; set; }


        public String Status { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }

        #endregion

        public DC_Ticket_KPI()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Ticket_KPI";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Impact";
            parameters[i].Value = this.Impact;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponeTime";
            parameters[i].Value = this.ResponeTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveTime";
            parameters[i].Value = this.ResolveTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClosedTime";
            parameters[i].Value = this.ClosedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Ticket_KPI";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPIID";
            parameters[i].Value = this.KPIID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Impact";
            parameters[i].Value = this.Impact;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponeTime";
            parameters[i].Value = this.ResponeTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveTime";
            parameters[i].Value = this.ResolveTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClosedTime";
            parameters[i].Value = this.ClosedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Ticket_KPI";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@KPIID";
            parameters[0].Value = KPIID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static DC_Ticket_KPI GetDC_Ticket_KPI(string KPIID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_KPI";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@KPIID";
            parameters[0].Value = KPIID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_KPI
            {
                KPIID = row["KPIID"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Description = row["Description"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                Status =row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        public static DC_Ticket_KPI GetDC_Ticket_KPI(string TypeID, string Priority, string Impact)
        {
            string PROCEDURE = "p_SelectDC_Ticket_KPI_ByType";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TypeID";
            parameters[0].Value = TypeID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@Priority";
            parameters[1].Value = Priority;

            parameters[2] = new SqlParameter();
            parameters[2].ParameterName = "@Impact";
            parameters[2].Value = Impact;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_KPI
            {
                KPIID = row["KPIID"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Description = row["Description"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        //public static List<DC_Ticket_KPI> GetDC_Ticket_KPIs(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDC_Ticket_KPIsDynamic";
        //    SqlParameter[] parameters = new SqlParameter[2];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@WhereCondition";
        //    parameters[i].Value = whereCondition;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@OrderByExpression";
        //    parameters[i].Value = orderByExpression;

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new DC_Ticket_KPI
        //    {
        //        KPIID = row["KPIID"].ToString(),
        //        TypeID = row["TypeID"].ToString(),
        //        Description = row["Description"].ToString(),
        //        Priority = row["Priority"].ToString(),
        //        Impact = row["Impact"].ToString(),
        //        ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
        //        ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
        //        ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
        //        Status = Convert.ToBoolean(row["Status"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<DC_Ticket_KPI> GetAllDC_Ticket_KPIs()
        {
            string PROCEDURE = "p_SelectDC_Ticket_KPIsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Ticket_KPI
            {
                KPIID = row["KPIID"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Description = row["Description"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
      
    }
}
