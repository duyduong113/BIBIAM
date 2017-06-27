using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using ERPAPD.Helpers;

namespace ERPAPD.Models
{
    public class CRM_Customer_Debt
    {
        #region Members
        public string Salesman { get; set; }
        public DateTime PayDay { get; set; }
        public double TotalAmount { get; set; }
        public double Published { get; set; }
        public double PublishedToDay { get; set; }
        public double Collected { get; set; }
        public double Remain { get; set; }
        public double TotalRemain { get; set; }
        public double Invoiced { get; set; }
        public double NotInvoiced { get; set; }
        [Ignore]
        public DateTime LastRemindDate { get; set; }
        [Ignore]
        public string LastNote { get; set; }
        [Ignore]
        public string LastUser { get; set; }
        public string Status { get; set; }
        public string CustomerCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Ignore]
        public string CustomerName { get; set; }
        [Ignore]
        public string CustomerType { get; set; }
        public string ContractInDebt { get; set; }
        [Ignore]
        public int OutDate { get; set; }
        public double Adjust { get; set; }

        #endregion

        public static DataTable ReadRemindDebt(int Page, int PageSize, string where)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", Page));
            param.Add(new SqlParameter("@PageSize", PageSize));
            param.Add(new SqlParameter("@WhereCondition", where));
            param.Add(new SqlParameter("@Sort", ""));
            return new SqlHelper().ExecuteQuery("p_CRM_Contract_Debt_ReadRemindDebt", param);
        }
        public static DataTable ReadData(int Page, int PageSize, string where)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", Page));
            param.Add(new SqlParameter("@PageSize", PageSize));
            param.Add(new SqlParameter("@WhereCondition", where));
            param.Add(new SqlParameter("@Sort", ""));
            return new SqlHelper().ExecuteQuery("p_CRM_Customer_Debt_ReadData", param);
        }
    }
}