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
    public class DC_TeleSales_Pre_Order
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string RequestOrderId { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string MSIN { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public string ShippingAddress { get; set; }
        public string Note { get; set; }
        public string Telesales { get; set; }
        public bool IsDone { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

        [StringLength(2000)]
        public string TeleSaleNote { get; set; }
        [Ignore]
        public string Status { get; set; }
        [Ignore]
        public string LastTouchedBy { get; set; }

        public string OrganizationID { get; set; }
        public string LastCallUser { get; set; }

        public static List<DC_TeleSales_Pre_Order> GetDC_TeleSales_Pre_OrderDynamic(string whereCondition)
        {
            string PROCEDURE = "p_SelectDC_TeleSales_Pre_OrderDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSales_Pre_Order
            {
                Id = Convert.ToInt32(row["Id"].ToString()),
                RequestOrderId = row["RequestOrderId"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                Phone = row["Phone"].ToString(),
                MSIN = row["MSIN"].ToString(),
                ItemName = row["ItemName"].ToString(),
                ShippingAddress = row["ShippingAddress"].ToString(),
                Note = row["Note"].ToString(),
                Telesales = row["Telesales"].ToString(),
                IsDone = Convert.ToBoolean(row["IsDone"].ToString()), 
                Qty = Convert.ToInt32(row["Qty"].ToString()),
                Status = row["Status"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
    
}