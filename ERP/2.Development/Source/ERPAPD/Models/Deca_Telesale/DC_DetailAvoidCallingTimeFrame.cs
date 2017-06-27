using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_DetailAvoidCallingTimeFrame
    {
        [AutoIncrement]
        public int Id { get; set; }

        [StringLengthAttribute(128)]
        public string DetailHeaderID { get; set; }

        [DefaultValue("1900-01-01 00:00:00.000")]
        public DateTime FromHour { get; set; }

        [DefaultValue("1900-01-01 00:00:00.000")]
        public DateTime ToHour { get; set; }

        [StringLengthAttribute(128)]
        public string HeaderID { get; set; }

        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

        [Ignore]
        public string FromHourCV { get; set; }

        [Ignore]
        public string ToHourCV { get; set; }
        [Ignore]
        public string CompanyID { get; set; }
        [Ignore]
        public string Item { get; set; }
        [Ignore]
        public bool Monday { get; set; }
        [Ignore]
        public bool Tuesday { get; set; }
        [Ignore]
        public bool Wednesday { get; set; }
        [Ignore]
        public bool Thursday { get; set; }
        [Ignore]
        public bool Friday { get; set; }
        [Ignore]
        public bool Saturday { get; set; }
        [Ignore]
        public bool Sunday { get; set; }

        public static List<DC_DetailAvoidCallingTimeFrame> GetAll()
        {
            string PROCEDURE = "p_DC_DetailAvoidCallingTimeFrame_GetAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_DetailAvoidCallingTimeFrame
            {
                CompanyID = row["CompanyID"].ToString(),
                FromHour = Convert.ToDateTime(row["FromHour"].ToString()),
                ToHour = Convert.ToDateTime(row["ToHour"].ToString()),
                RowLastUpdatedTime = DateTime.Parse(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RowCreatedTime = DateTime.Parse(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                Item = row["HeaderName"].ToString(),
                Id = int.Parse(row["Id"].ToString()),
                Monday = bool.Parse(row["Monday"].ToString()),
                Tuesday = bool.Parse(row["Tuesday"].ToString()),
                Wednesday = bool.Parse(row["Wednesday"].ToString()),
                Thursday = bool.Parse(row["Thursday"].ToString()),
                Friday = bool.Parse(row["Friday"].ToString()),
                Saturday = bool.Parse(row["Saturday"].ToString()),
                Sunday = bool.Parse(row["Sunday"].ToString()),
                HeaderID = row["HeaderID"].ToString(),
                Total = int.Parse(row["Total"].ToString()),
                Register = int.Parse(row["CustomerNum"].ToString()),
            }).ToList();
        }

        [Ignore]
        public int Total { get; set; }
        [Ignore]
        public int Register { get; set; }
        [Ignore]
        public int UserBlocked { get; set; }
        [Ignore]
        public int UserTerminated { get; set; }

        [Ignore]
        public string HeaderName { get; set; }
        [Ignore]
        public int DetailID { get; set; }
        [Ignore]
        public int Header { get; set; }
        public static List<DC_DetailAvoidCallingTimeFrame> GetAllDetailAvoidCalling()
        {
            string PROCEDURE = "select A.FromHour,A.ToHour,HeaderName,A.Id DetailID,B.Id Header from MCA_DetailAvoidCallingTimeFrame A left join MCA_AvoidCallingTimeFrame B ON A.HeaderID = B.Id where B.Id is not null";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_DetailAvoidCallingTimeFrame
            {
                HeaderName = row["HeaderName"].ToString(),
                FromHour = DateTime.Parse(row["FromHour"].ToString()),
                ToHour = DateTime.Parse(row["ToHour"].ToString()),
                DetailID = int.Parse(row["DetailID"].ToString()),
                Header = int.Parse(row["Header"].ToString())
            }).ToList();
        }
    }
}