using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using System.Data;

namespace ERPAPD.Models
{
    public class DC_Telesales_Agent
    {
        [AutoIncrement]
        public int Id;
        public string UserName { get; set; }
        public string Team { get; set; }
        public string XLiteID { get; set; }
        public string Region { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        [Ignore]
        public string ImportNote { get;set; }
        public static List<DC_Telesales_Agent> GetDC_Telesales_Agent()
        {
            string PROCEDURE = "p_GetAllDC_Telesales_Agent";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Telesales_Agent
            {
                UserName = row["Username"].ToString(),
                Team = row["Team"].ToString(),
                XLiteID = row["XLiteID"].ToString(),
                Region = row["Region"].ToString(),
                CreatedBy = row["CreatedBy"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
            }).ToList();
        }

    }


}