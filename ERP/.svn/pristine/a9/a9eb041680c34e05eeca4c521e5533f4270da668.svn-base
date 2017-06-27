using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_TeleSale_WaitingList
    {
      
        [DefaultValue("1900-01-01")]
        public DateTime Time { get; set; }
        public string CustomerID { get; set; }
        public string FullName { get; set; }
        public string Detail { get; set; }
        public string MSIN { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LastActivities { get; set; }
        public string ResolveBy { get; set; }
        public DateTime ResolveTime { get; set; }
        public string TicketID { get; set; }
        public string RequestOrderID { get; set; }
        public int Id { get; set; }
        public string SourceColor { get; set; }
        public int CountRequest { get; set; }
        public int CountTicket { get; set; }
        public string Replies { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime RowCreatedTime { get; set; }

        public static List<DC_TeleSale_WaitingList> GetTicketRequest(string CustomerID)
        {
            string PROCEDURE = "p_GetListTicketRequest_SaleOrderRequestByCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_WaitingList
            {
                TicketID =row["TicketID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Description = row["Description"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                ResolveBy = row["ResolveBy"].ToString(),
                ResolveTime = Convert.ToDateTime(row["ResolveTime"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
            }).ToList();
        }

    }
}