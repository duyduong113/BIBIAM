using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;
namespace ERPAPD.Models
{
    public class DC_Telesale_ReportKPI
    {
        public DateTime Date { get; set; }
        public int Month { get; set; }
        public string Agent { get; set; }

        public double KpiRevenue { get; set; }
        public double KpiCall { get; set; }
        public double KpiCustomer { get; set; }
        public double KpiOrder { get; set; }
        public double KpiTalkTime { get; set; }
        public double ActualRevenue { get; set; }
        public double ActualCall { get; set; }
        public double ActualCustomer { get; set; }
        public double ActualOrder { get; set; }
        public double ActualTalkTime { get; set; }

        public double PercentRevenue
        {
            get
            {
                if (KpiRevenue == 0)
                {
                    return 0;
                }
                return ActualRevenue / KpiRevenue * 100;
            }
        }
        public double PercentCall
        {
            get
            {
                if (KpiCall == 0)
                {
                    return 0;
                }
                return ActualCall / KpiCall;
            }
        }
        public double PercentCustomer
        {
            get
            {
                if (KpiCustomer == 0)
                {
                    return 0;
                }
                return ActualCustomer / KpiCustomer;
            }
        }
        public double PercentOrder
        {
            get
            {
                if (KpiOrder == 0)
                {
                    return 0;
                }
                return ActualOrder / KpiOrder;
            }
        }
        public double PercentTalkTime
        {
            get
            {
                if (KpiTalkTime == 0)
                {
                    return 0;
                }
                return ActualTalkTime / KpiTalkTime;
            }

        }

        public double Remain 
        {
            get 
            { 
                if ((100 - PercentRevenue) > 0 )
	            {
                    return 100 - PercentRevenue;
	            }
                return 0;
            }
        }
        public static DC_Telesale_ReportKPI getKPIHistoryToDay(string Agent)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var sp = dbConn.Query<DC_Telesale_ReportKPI>("getKPIActualForTelesaleInToDay", new { Agent = Agent }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return sp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public static List<DC_Telesale_ReportKPI> getKPIHistory7Day(string Agent)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var sp = dbConn.Query<DC_Telesale_ReportKPI>("getKPIActualForTelesaleIn7Day", new { Agent = Agent }, commandType: CommandType.StoredProcedure).ToList();
                    return sp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }
        public static DC_Telesale_ReportKPI getKPIInMonth(string Agent)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                
                    try
                    {
                        var sp = dbConn.Query<DC_Telesale_ReportKPI>("getKPIActualForTelesaleInMonth", new { Agent = Agent }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        return sp;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                

            }
        }
        public static List<DC_Telesale_ReportKPI> getKPI7Month(string Agent)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var sp = dbConn.Query<DC_Telesale_ReportKPI>("getKPIActualForTelesaleIn7Month", new { Agent = Agent }, commandType: CommandType.StoredProcedure).ToList();
                    return sp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }  
            }
        }
    }
}