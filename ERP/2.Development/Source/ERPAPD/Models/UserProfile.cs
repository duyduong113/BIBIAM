using ServiceStack.DataAnnotations;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string LastUpdatedUser { get; set; }
        public int DepartmentID { get; set; }
        public string Phone { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public string CompanyID { get; set; }
        public string LevelID { get; set; }
        public string Description { get; set; }
        
        [Ignore]
        public string Email { get; set; }
        [Ignore]
        public int Active { get; set; }
        [Ignore]
        public string EmployeeID { get; set; }
        [Ignore]
        public string Region { get; set; }
        [Ignore]
        public string RefID { get; set; }
        [Ignore]
        public string BranchID { get; set; }
        [Ignore]
        public string RegionID { get; set; }
        [Ignore]
        public string Avatar { get; set; }
        [Ignore]
        public string LevelName { get; set; }
        [Ignore]
        public string Level { get; set; }
        [Ignore]
        public string PositionID { get; set; }
        [Ignore]
        public string Department { get; set; }
        public UserProfile()
        { }
        public static UserProfile GetUser(string UserName)
        {
            string PROCEDURE = "p_SelectUserProfile";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserName";
            parameters[0].Value = UserName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new UserProfile
            {
                UserName = row["UserName"].ToString(),
                Email = row["Email"].ToString(),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                CompanyID = string.IsNullOrEmpty(row["CompanyID"].ToString()) ? "0".ToString() : row["CompanyID"].ToString(),
                DepartmentID = string.IsNullOrEmpty(row["DepartmentID"].ToString()) ? 0 : Convert.ToInt32(row["DepartmentID"].ToString()),
                Phone = string.IsNullOrEmpty(row["Phone"].ToString()) ? "".ToString() : row["Phone"].ToString(),
                Team = string.IsNullOrEmpty(row["Team"].ToString()) ? "0".ToString() : row["Team"].ToString(),
                Position = string.IsNullOrEmpty(row["Position"].ToString()) ? "0".ToString() : row["Position"].ToString(),
                Gender = row["Gender"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                RefID = row["RefID"].ToString(),
                Avatar = row["Avatar"].ToString(),
                LevelName = row["Level"].ToString(),
                LevelID = row["LevelID"].ToString(),
                PositionID = row["PositionID"].ToString(),
                Department = row["Department"].ToString(),
                BranchID = row["BranchID"].ToString(),
                RegionID = row["RegionID"].ToString(),
            }).ToList().FirstOrDefault();
        }
    }
}