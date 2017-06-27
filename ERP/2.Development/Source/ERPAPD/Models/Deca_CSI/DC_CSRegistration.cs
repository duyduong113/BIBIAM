using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_CSRegistration
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        [StringLength(10)]
        public string RegisId { get; set; }
        [StringLength(512)]
        public string Content { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }

        public static DC_CSRegistration GetLastID()
        {
            string PROCEDURE = "Select top 1 * from DC_CSRegistration order by Id desc";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_CSRegistration
            {
                Id = int.Parse(row["Id"].ToString()),
                RegisId = row["RegisId"].ToString()
            }).ToList().FirstOrDefault();
        }
    }
}