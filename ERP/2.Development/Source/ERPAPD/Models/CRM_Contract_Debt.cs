using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using ERPAPD.Helpers;


namespace ERPAPD.Models
{
    public class CRM_Contract_Debt
    { 
        #region Members
        public long pk_contract {get;set;}
        public string Salesman {get;set;}
        public DateTime PayDay {get;set;}
        public double TotalAmount {get;set;}
        public double Published {get;set;}
        public double PublishedToDay {get;set;}
        public double Collected {get;set;}
        public double Remain {get;set;}
        public double TotalRemain {get;set;}
        public double Invoiced {get;set;}
        public double NotInvoiced {get;set;}
        public string Status { get; set; }
        public string CustomerCode { get; set; }
        public string CreatedBy {get;set;}
        public DateTime CreatedAt {get;set;}
        public string UpdatedBy {get;set;}
        public DateTime UpdatedAt { get; set; }
        [Ignore]
        public DateTime LastRemindDate { get; set; }
        [Ignore]
        public string LastNote { get; set; }
        [Ignore]
        public string LastUser { get; set; }
        [Ignore]
        public string ContractType { get; set; }
        [Ignore]
        public string CustomerName { get; set; }
        [Ignore]
        public string CustomerType { get; set; }
        [Ignore]
        public string ContractInDebt { get; set; }
        [Ignore]
        public int OutDate { get; set; }
        [Ignore]
        public string c_code { get; set; }
        [Ignore]
        public string c_contract_type { get; set; }
        [Ignore]
        public DateTime c_ngay_tt_theo_hd { get; set; }
        [Ignore]
        public string IsFinish { get; set; }
        
        #endregion
     
        public static DataTable ReadRemindDetail(int Page, int PageSize,string where, string CustomerCode)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", Page));
            param.Add(new SqlParameter("@PageSize", PageSize));
            param.Add(new SqlParameter("@WhereCondition", where));
            param.Add(new SqlParameter("@CustomerCode", CustomerCode));
            return new SqlHelper().ExecuteQuery("p_CRM_Contract_Debt_ReadRemindDetail", param);
        }
        public static DataTable ReadContractDebt(int Page, int PageSize, string where)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", Page));
            param.Add(new SqlParameter("@PageSize", PageSize));
            param.Add(new SqlParameter("@WhereCondition", where));
            return new SqlHelper().ExecuteQuery("p_CRM_Contract_Debt_ReadContractDebt", param);
        }
    }
}