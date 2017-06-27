using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using ERPAPD.Helpers;


namespace ERPAPD.Models
{
    public class CRM_Customer_Debt_Adjust
    {
        public string CustomerCode { get; set; }
        [Ignore]
        public int RowID { get; set; }
        public int Amount { get; set; }
        public int Type { get; set; }
        public string Note { get; set; }
        public DateTime AdjustDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public static DataTable ReadData(string CustomerCode)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerCode", CustomerCode));
            return new SqlHelper().ExecuteString("select * from CRM_Customer_Debt_Adjust where CustomerCode = @CustomerCode", param);
        }

    }
}