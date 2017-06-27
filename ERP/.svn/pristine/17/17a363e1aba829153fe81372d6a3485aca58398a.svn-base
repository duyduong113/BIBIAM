using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_CS_ResultList
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string SubId { get; set; }
        public bool Active { get; set; }
        [StringLengthAttribute(256)]
        public string Description { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

        public static DC_CS_ResultList GetLastID()
        {
            string PROCEDURE = "SELECT TOP 1 Id FROM DC_CS_ResultList ORDER BY RowCreatedTime DESC";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CS_ResultList
            {
                Id = row["Id"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static List<DC_CS_ResultList> GetAllResultList()
        {
            string text = "SELECT Id,Description FROM DC_CS_ResultList ORDER BY RowCreatedTime DESC";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, null);
            return dt.AsEnumerable().Select(row => new DC_CS_ResultList
            {
                Id = row["Id"].ToString(),
                Description = row["Description"].ToString(),
            }).ToList();
        }

        public static string GetDescription(string Id)
        {
            string text = "SELECT Description FROM DC_CS_ResultList WHERE Id = '" + Id + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, null);
            var data = dt.AsEnumerable().Select(row => new DC_CS_ResultList
            {
                Description = row["Description"].ToString(),
            }).ToList().FirstOrDefault();
            return data != null ? data.Description : "";
        }
    }
}