using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class Assets
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string Name { get; set; }
        public List<int> View { get; set; }
        public List<int> Create { get; set; }
        public List<int> Update { get; set; }
        public List<int> Delete { get; set; }
        public List<int> Export { get; set; }
        public List<int> Import { get; set; }
        public List<string> Columns { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }

        [Ignore]
        public int Group { get; set; }
    }

    public class Asset
    {
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Export { get; set; }
        public bool Import { get; set; }
        public static Asset GetAsset(int ID)
        {
            string PROCEDURE = "p_SelectAsset";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Asset
            {
                //ID = Convert.ToInt32(row["ID"].ToString()),
                //ParentID = Convert.ToInt32(row["ParentID"].ToString()),
                //Name = row["Name"].ToString(),
                //Description = row["Description"].ToString(),
                //View = row["View"].ToString(),
                //Rules = row["Rules"].ToString(),
                //Type = row["Type"].ToString(),
                //CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                //CreatedUser = row["CreatedUser"].ToString(),
                //LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                //LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
    }
}