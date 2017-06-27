using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class DC_Customer_Rule
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public string Rule { get; set; }
        public string AreaID { get; set; }
        public string RegionID { get; set; }
        public string BranchID { get; set; }
        public string TerritoryRegionID { get; set; }
        public DateTime Date { get; set; }
        public bool isRequest { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        [Ignore]
        public string Organization { get { return CustomerID.Split(':')[0]; } }

        public static List<DC_Customer_Rule> getR0()
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R0";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString()
            }).ToList();
        }

        public static List<DC_Customer_Rule> getR2()
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R2";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RegionID = row["RegionID"].ToString(),
                BranchID = row["BranchID"].ToString(),
                TerritoryRegionID = row["TerritoryRegionID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Rule> getR2Dynamic(string WhereCondition)
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R2Dynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RegionID = row["RegionID"].ToString(),
                BranchID = row["BranchID"].ToString(),
                TerritoryRegionID = row["TerritoryRegionID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Rule> getR3()
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R3";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RegionID = row["RegionID"].ToString(),
                BranchID = row["BranchID"].ToString(),
                TerritoryRegionID = row["TerritoryRegionID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Rule> getR3Dynamic(string WhereCondition)
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R3Dynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RegionID = row["RegionID"].ToString(),
                BranchID = row["BranchID"].ToString(),
                TerritoryRegionID = row["TerritoryRegionID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Rule> getR4()
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R4";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RegionID = row["RegionID"].ToString(),
                BranchID = row["BranchID"].ToString(),
                TerritoryRegionID = row["TerritoryRegionID"].ToString(),
            }).ToList();
        }

        public static List<DC_Customer_Rule> getR4Dynamic(string WhereCondition)
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Rule_R4Dynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Customer_Rule
            {
                CustomerID = row["CustomerID"].ToString(),
                AreaID = row["AreaID"].ToString(),
                RegionID = row["RegionID"].ToString(),
                BranchID = row["BranchID"].ToString(),
                TerritoryRegionID = row["TerritoryRegionID"].ToString(),
            }).ToList();
        }

        public static string getNextCustomer()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var customer = dbConn.SqlList<DC_Request_Customer_InDay>("exec p_DC_Customer_Rule_NextCustomer '" + System.Web.HttpContext.Current.User.Identity.Name + "'").FirstOrDefault();
                    return string.IsNullOrEmpty(customer.CustomerID) ? "AAA" : customer.CustomerID;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static string getNextCustomerForFF()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var customer = dbConn.SqlList<DC_Request_Customer_InDay>("exec p_DC_Customer_Rule_NextCustomerForFF '" + System.Web.HttpContext.Current.User.Identity.Name + "'").FirstOrDefault();
                    return string.IsNullOrEmpty(customer.CustomerID) ? "AAA" : customer.CustomerID;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
    }
}