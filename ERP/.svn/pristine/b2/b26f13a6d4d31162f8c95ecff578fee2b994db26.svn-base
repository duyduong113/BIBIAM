using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Telesales_AvoidCallingTimeCompany
    {
        [AutoIncrement]
        public int Id { get; set; }

        [StringLengthAttribute(128)]
        public string CompanyID { get; set; }

        [StringLengthAttribute(128)]
        public string HeaderID { get; set; }

        [StringLengthAttribute(128)]
        public string DetailHeaderID { get; set; }

        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

        [Ignore]
        [DefaultValue("1900-01-01 00:00:00.000")]
        public DateTime FromHour { get; set; }
        [Ignore]
        [DefaultValue("1900-01-01 00:00:00.000")]
        public DateTime ToHour { get; set; }
        [Ignore]
        public string HeaderName { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public static List<DC_Telesales_AvoidCallingTimeCompany> GetDC_Telesales_AvoidCallingTimeCompany(string CompanyID)
        {
            string PROCEDURE = "p_SelectDC_Telesales_AvoidCallingTimeCompany";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CompanyID";
            parameters[0].Value = CompanyID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Telesales_AvoidCallingTimeCompany
            {
                CompanyID = row["CompanyID"].ToString(),
                HeaderName = row["HeaderID"].ToString(),
                FromHour = Convert.ToDateTime(row["FromHour"].ToString()),
                ToHour = Convert.ToDateTime(row["ToHour"].ToString()),
                Id = Int32.Parse(row["Id"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_Telesales_AvoidCallingTimeCompany> GetCompanyID()
        {
            string PROCEDURE = "select Distinct CompanyID AS CompanyID from DC_Telesales_AvoidCallingTimeCompany";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_AvoidCallingTimeCompany
            {
                CompanyID = row["CompanyID"].ToString()
            }).ToList();
        }

        [Ignore]
        public int CountCom { get; set; }
        [Ignore]
        public int CountCustomer { get; set; }
        public static DC_Telesales_AvoidCallingTimeCompany CountCompany()
        {
            string PROCEDURE = "select COUNT(Distinct CompanyID) AS CountCom from DC_Telesales_AvoidCallingTimeCompany";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_AvoidCallingTimeCompany
            {
                CountCom = int.Parse(row["CountCom"].ToString())
            }).FirstOrDefault();
        }
        public static DC_Telesales_AvoidCallingTimeCompany CountCustomerInCom()
        {
            string PROCEDURE = "select COUNT(customerID) AS CountCustomer from Deca_Customer c ";
            PROCEDURE += "INNER JOIN ( select Distinct CompanyID from DC_Telesales_AvoidCallingTimeCompany )av ON av.CompanyID = LEFT(c.CustomerID, CHARINDEX(':',c.CustomerID)-1)";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_AvoidCallingTimeCompany
            {
                CountCustomer = int.Parse(row["CountCustomer"].ToString())
            }).FirstOrDefault();
        }
    }

}