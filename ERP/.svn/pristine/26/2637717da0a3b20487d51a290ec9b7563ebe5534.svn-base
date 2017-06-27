using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Plan_HomePage.
    /// </summary>
    public class DC_Plan_HomePage
    {
        #region Members
        public string Month { get; set; }
        public double Revenue { get; set; }
        public double Register { get; set; }
        public double PlanRevenue { get; set; }
        public double PlanRegister { get; set; }
        public int RowID { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }
        #endregion

        public DC_Plan_HomePage()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Plan_HomePage";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Month";
            parameters[i].Value = this.Month;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Revenue";
            parameters[i].Value = this.Revenue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Register";
            parameters[i].Value = this.Register;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            foreach (var x in parameters)
            {
                if (x.Value == null)
                {
                    x.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Plan_HomePage";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Month";
            parameters[i].Value = this.Month;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Revenue";
            parameters[i].Value = this.Revenue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Register";
            parameters[i].Value = this.Register;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            foreach (var x in parameters)
            {
                if (x.Value == null)
                {
                    x.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Plan_HomePage";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@Month";
            parameters[0].Value = Month;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static DC_Plan_HomePage GetDC_Plan_HomePage(string Month)
        {
            string PROCEDURE = "p_SelectDC_Plan_HomePage";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@Month";
            parameters[0].Value = Month;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Plan_HomePage
            {
                Month = row["Month"].ToString(),
                Revenue = Convert.ToDouble(row["Revenue"].ToString()),
                Register = Convert.ToDouble(row["Register"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Plan_HomePage> GetDC_Plan_HomePages(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Plan_HomePagesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Plan_HomePage
            {
                Month = row["Month"].ToString(),
                Revenue = Convert.ToDouble(row["Revenue"].ToString()),
                Register = Convert.ToDouble(row["Register"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Plan_HomePage> GetAllDC_Plan_HomePages()
        {
            string PROCEDURE = "p_SelectDC_Plan_HomePagesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Plan_HomePage
            {
                Month = row["Month"].ToString(),
                Revenue = Convert.ToDouble(row["Revenue"].ToString()),
                Register = Convert.ToDouble(row["Register"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        public static DC_Plan_HomePage GetMonthPlan()
        {
            string PROCEDURE = "p_SelectDC_Plan_HomePage_GetMonthPlan";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Plan_HomePage
            {
                Month = row["Month"].ToString(),
                Revenue = Convert.ToDouble(row["Revenue"].ToString()),
                Register = Convert.ToDouble(row["Register"].ToString()),
                PlanRevenue = Convert.ToDouble(row["PlanRevenue"].ToString()),
                PlanRegister = Convert.ToDouble(row["PlanRegister"].ToString()),
            }).ToList().FirstOrDefault();
        }

    }
}
