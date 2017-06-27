using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_TeleSale_Agent_Region
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(32)]
        public string RegionID { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

        [Ignore]
        public string BranchName { get; set; }
        [Ignore]
        public string RegionName { get; set; }
        [Ignore]
        public string Description { get; set; }
        [Ignore]
        public string RegionalBD { get; set; }
        [Ignore]
        public string RegionalDirector { get; set; }
        [Ignore]
        public string ImportNote { get; set; }

        public static List<DC_TeleSale_Agent_Region> GetAll()
        {
            string PROCEDURE = "select A.*,B.BranchName,R.RegionName from DC_TeleSale_Agent_Region A ";
                    PROCEDURE += " left join MCA_MOP_Branch B ON B.BranchID = A.BranchID ";
                    PROCEDURE += " left join MCA_MOP_Region R ON R.RegionID = A.RegionID ";
                    PROCEDURE += " order by UserID ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_Agent_Region
            {
                Id = int.Parse(row["Id"].ToString()),
                UserName = row["UserID"].ToString(),
                BranchName = row["BranchName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                RegionName = row["RegionName"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
            }).ToList();
        
        }

        public static List<DC_TeleSale_Agent_Region> GetReadData(string UserName)
        {
            string PROCEDURE = "select A.*, R.RegionName from DC_TeleSale_Agent_Region A ";
            PROCEDURE += " left join DC_Location_Region R ON R.RegionID = A.RegionID ";
            PROCEDURE += " WHERE a.UserName =  '"+UserName+"' ";
            PROCEDURE += " order by UserName ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_TeleSale_Agent_Region
            {
                Id = int.Parse(row["Id"].ToString()),
                UserName = row["UserName"].ToString(),
                RegionName = row["RegionName"].ToString(),
                RegionID = row["RegionID"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
            }).ToList();
        
        }
      
    }
}