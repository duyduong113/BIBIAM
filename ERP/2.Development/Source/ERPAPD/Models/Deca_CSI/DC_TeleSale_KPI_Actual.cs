using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_TeleSale_KPI_Actual
    {
        public string AgentID { get; set; }
        public double NumberCustomer { get; set; }
        public double NumberCall { get; set; }
        public double SO { get; set; }
        public double Rev { get; set; }
        public double NumberCallKPI { get; set; }
        public double SOKPI { get; set; }
        public double RevKPI { get; set; }
        public double NumberCustomerKPI { get; set; }
        public string CallPercentColor { get; set; }
        public string RevPercentColor { get; set; }
        public string SOPercentColor { get; set; }
        public string CustomerPercentColor { get; set; }
        public float RevPercent { get; set; }
        public float SOKPercent { get; set; }
        public float CustomerPercent { get; set; }
        public float CallPercent { get; set; }
        public int TalkingTimeKPI { get; set; }
        public float TalkTime { get; set; }
        public string TalkingTimePercentColor { get; set; }
        public float TalkTimePercent { get; set; }

        public static List<DC_TeleSale_KPI_Actual> GetDataKPI_ActualTeleSale(string AgentID)
        {
            string PROCEDURE = "p_SelectTeleSale_GetKPIAndActual";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = AgentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_KPI_Actual
            {
                AgentID = row["AgentID"].ToString(),
                NumberCustomer = Convert.ToDouble(row["NumberCustomer"]),
                NumberCall = Convert.ToDouble(row["NumberCall"]),
                SO = Convert.ToDouble(row["SO"]),
                Rev = Convert.ToDouble(row["Rev"]),
                NumberCallKPI = Convert.ToDouble(row["NumberCallKPI"]),
                SOKPI = Convert.ToDouble(row["SOKPI"]),
                RevKPI = Convert.ToDouble(row["RevKPI"]),
                NumberCustomerKPI = Convert.ToDouble(row["NumberCustomerKPI"]),
                CallPercentColor = row["CallPercentColor"].ToString(),
                RevPercentColor = row["RevPercentColor"].ToString(),
                SOPercentColor = row["SOPercentColor"].ToString(),
                CustomerPercentColor = row["CustomerPercentColor"].ToString(),
                RevPercent = float.Parse(row["RevPercent"].ToString()),
                SOKPercent = float.Parse(row["SOKPercent"].ToString()),
                CustomerPercent = float.Parse(row["CustomerPercent"].ToString()),
                CallPercent = float.Parse(row["CallPercent"].ToString()),

                TalkingTimeKPI = int.Parse(row["TalkingTimeKPI"].ToString()),
                TalkTime = float.Parse(row["TalkTime"].ToString()),
                TalkingTimePercentColor = row["TalkingTimePercentColor"].ToString(),
                TalkTimePercent = float.Parse(row["TalkTimePercent"].ToString())
            }).ToList();
        }

        public static List<DC_TeleSale_KPI_Actual> GetDataKPI_ActualTeleSaleThisMonth(string AgentID)
        {
            string PROCEDURE = "p_SelectTeleSale_GetKPIAndActualThisMonth";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = AgentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_KPI_Actual
            {
                AgentID = row["AgentID"].ToString(),
                NumberCustomer = Convert.ToDouble(row["NumberCustomer"]),
                NumberCall = Convert.ToDouble(row["NumberCall"]),
                SO = Convert.ToDouble(row["SO"]),
                Rev = Convert.ToDouble(row["Rev"]),
                NumberCallKPI = Convert.ToDouble(row["NumberCallKPI"]),
                SOKPI = Convert.ToDouble(row["SOKPI"]),
                RevKPI = Convert.ToDouble(row["RevKPI"]),
                NumberCustomerKPI = Convert.ToDouble(row["NumberCustomerKPI"]),
                CallPercentColor = row["CallPercentColor"].ToString(),
                RevPercentColor = row["RevPercentColor"].ToString(),
                SOPercentColor = row["SOPercentColor"].ToString(),
                CustomerPercentColor = row["CustomerPercentColor"].ToString(),
                RevPercent = float.Parse(row["RevPercent"].ToString()),
                SOKPercent = float.Parse(row["SOKPercent"].ToString()),
                CustomerPercent = float.Parse(row["CustomerPercent"].ToString()),
                CallPercent = float.Parse(row["CallPercent"].ToString()),

                TalkingTimeKPI = int.Parse(row["TalkingTimeKPI"].ToString()),
                TalkTime = float.Parse(row["TalkTime"].ToString()),
                TalkingTimePercentColor = row["TalkingTimePercentColor"].ToString(),
                TalkTimePercent = float.Parse(row["TalkTimePercent"].ToString())
            }).ToList();
        }


        public static List<DC_TeleSale_KPI_Actual> GetDataKPI_ActualTeleSaleThisMonthFoFF(string AgentID)
        {
            string PROCEDURE = "p_SelectTeleSale_GetKPIAndActualThisMonthForFF";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = AgentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_KPI_Actual
            {
                AgentID = row["AgentID"].ToString(),
                SO = Convert.ToDouble(row["SO"]),
                SOKPI = Convert.ToDouble(row["SOKPI"]),
                SOPercentColor = row["SOPercentColor"].ToString(),
                SOKPercent = float.Parse(row["SOKPercent"].ToString()),

                Rev = Convert.ToDouble(row["Rev"]),
                RevKPI = Convert.ToDouble(row["RevKPI"]),
                RevPercentColor = row["RevPercentColor"].ToString(),
                RevPercent = float.Parse(row["RevPercent"].ToString()),
            }).ToList();
        }


        public static List<DC_TeleSale_KPI_Actual> GetDataKPI_ActualTeleSaleForFF(string AgentID)
        {
            string PROCEDURE = "p_SelectTeleSale_GetKPIAndActualForFF";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AgentID";
            parameters[i].Value = AgentID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_KPI_Actual
            {
                AgentID = row["AgentID"].ToString(),
                SO = Convert.ToDouble(row["SO"]),
                SOKPI = Convert.ToDouble(row["SOKPI"]),
                SOPercentColor = row["SOPercentColor"].ToString(),
                SOKPercent = float.Parse(row["SOKPercent"].ToString()),

                Rev = Convert.ToDouble(row["Rev"]),
                RevKPI = Convert.ToDouble(row["RevKPI"]),
                RevPercentColor = row["RevPercentColor"].ToString(),
                RevPercent = float.Parse(row["RevPercent"].ToString()),
            }).ToList();
        }
    }
}