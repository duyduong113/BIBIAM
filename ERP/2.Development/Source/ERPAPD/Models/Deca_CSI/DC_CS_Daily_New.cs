using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
namespace ERPAPD.Models
{
    public class DC_CS_Daily_New
    {
        [StringLengthAttribute(500)]
        public string Title { get; set; }
        //[StringLengthAttribute(maximumLength)]
        public string Content { get; set; }
        //[StringLengthAttribute(1000)]
        public Boolean Status { get; set; }
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
        public string Description { get; set; }
        public Boolean Hot { get; set; }
        public string Thumnail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public static List<DC_CS_Daily_New> GetAllDC_Articles()
        //{
        //    string PROCEDURE = "SELECT * FROM DC_CS_Daily_New where Status = 1";

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
        //    return dt.AsEnumerable().Select(row => new DC_CS_Daily_New
        //    {
        //        Id = Convert.ToInt32(row["Id"].ToString()),
        //        Title = row["Title"].ToString(),
        //        Content = row["Content"].ToString(),
        //        Status = Convert.ToBoolean(row["Status"].ToString()),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        Thumnail = row["Thumnail"].ToString(),
        //        Description = row["Description"].ToString(),
        //    }).ToList();
        //}
    }
}
