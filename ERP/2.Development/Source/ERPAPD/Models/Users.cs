using System.Data;
using System.Data.SqlClient;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    public class Users
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        [StringLengthAttribute(100)]
        [Required]
        public string UserName { get; set; }
        [StringLengthAttribute(64)]
        public string Password { get; set; }
        [StringLengthAttribute(100)]
        [Required]
        public string FullName { get; set; }
        [StringLengthAttribute(100)]
        [Required]
        [Index(Unique = true)]
        public string Email { get; set; }
        [StringLengthAttribute(100)]
        public string Phone { get; set; }
        [StringLengthAttribute(1000)]
        public string ImageUrl { get; set; }
        public List<GroupViewModel> Groups { get; set; }
        public bool Active { get; set; }
        public bool IsAppUser { get; set; }
        public bool IsAppReset { get; set; }
        public DateTime? CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
        [Ignore]
        public DateTime? CreatedDatetime { get; set; }
        [Ignore]
        public string CreatedUser { get; set; }
        [Ignore]
        public DateTime? LastUpdatedDateTime { get; set; }
        [Ignore]
        public string LastUpdatedUser { get; set; }
        [Ignore]
        public string DepartmentID { get; set; }
        [Ignore]
        public string Team { get; set; }
        [Ignore]
        public string Position { get; set; }
        [Ignore]
        public string Gender { get; set; }
        [Ignore]
        public string CompanyID { get; set; }
        [Ignore]
        public string LevelID { get; set; }
        [Ignore]
        public string Description { get; set; }
        [Ignore]
        public DateTime? Birthday { get; set; }
        [Ignore]
        public DateTime? StartWorkingDay { get; set; }
        [Ignore]
        public DateTime? TerminatedDate { get; set; }

        private string _xLiteID;
        [Ignore]
        public string XLiteID
        {
            get
            {
                if (_xLiteID == null)
                {
                    if (info != null)
                    {
                        _xLiteID = info.XLiteID;
                    }
                }
                return _xLiteID;
            }
            set
            {
                this._xLiteID = value;
            }
        }

        private string _xLiteCode;
        [Ignore]
        public string XLiteCode
        {
            get
            {
                if (_xLiteCode == null)
                {
                    if (info != null)
                    {
                        _xLiteCode = info.XLiteCode;
                    }
                }
                return _xLiteCode;
            }
            set
            {
                this._xLiteCode = value;
            }
        }
        //get user Info
        [Ignore]
        public EmployeeInfo info
        {
            get
            {
                var data = new EmployeeInfo();
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    data = dbConn.FirstOrDefault<EmployeeInfo>("UserName={0}", this.UserName);
                    return data;
                }
            }
        }
        [Ignore]
        public string LevelDescription
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (this.info != null && !string.IsNullOrEmpty(this.info.LevelID))
                    {
                        return dbConn.FirstOrDefault<DC_Position_Level>("LevelID={0}", this.info.LevelID) != null ? dbConn.FirstOrDefault<DC_Position_Level>("LevelID={0}", this.info.LevelID).Description : "";
                    }
                    else return "";
                }
            }
        }
        [Ignore]
        public string DepartmentDescription
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (this.info != null && this.info.DepartmentID > 0)
                    {
                        return "";
                        //return dbConn.FirstOrDefault<Deca_Department>("DepartmentID={0}", this.info.DepartmentID) != null ? dbConn.FirstOrDefault<Deca_Department>("DepartmentID={0}", this.info.DepartmentID).Department : "";
                    }
                    else return "";
                }
            }
        }

        //[Ignore]
        //public string TeamDescription
        //{
        //    get
        //    {
        //        using (var dbConn = Helpers.OrmliteConnection.openConn())
        //        {
        //            if (this.info != null && !string.IsNullOrEmpty(this.info.Team))
        //            {
        //             //   return dbConn.FirstOrDefault<Deca_Team>("TeamID={0}", this.info.Team) != null ? dbConn.FirstOrDefault<Deca_Team>("TeamID={0}", this.info.Team).TeamName : "";
        //            }
        //            else return "";
        //        }
        //    }
        //}

        public static string Avatar()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                string text = "SELECT TOP 1 ImageUrl FROM Users WHERE UserName = '" + System.Web.HttpContext.Current.User.Identity.Name + "'";
                var imageUrl = dbConn.Scalar<string>(text);
                if (!String.IsNullOrEmpty(imageUrl))
                {
                    return imageUrl;
                }
                else
                {
                    return "default_avartar.png";
                }
            }

        }

        public static string ListAsset()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var user = dbConn.SingleOrDefault<Users>("UserName={0}", System.Web.HttpContext.Current.User.Identity.Name);
                if (user != null)
                {
                    var groups = user.Groups.Select(s => s.Id).Distinct().ToList();
                    if (groups.Contains(1))
                    {
                        return "All";
                    }
                    else
                    {
                        var listAsset = dbConn.Select<Assets>().Where(s => s.View != null && s.View.Intersect(groups).Count() > 0);
                        if (listAsset.Count() > 0)
                        {
                            return string.Join(",", listAsset.Select(s => s.Name));
                        }
                    }
                }
                return "";
            }
        }

        public static List<Users> GetUsers(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectUsersDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Users
            {
                //KeyUserID = Helpers.Locdau.convertToUnSign3(row["ID"].ToString()),
                //Id = row["UserID"].ToString(),
                UserName = row["UserName"].ToString(),
                Email = row["Email"].ToString(),
                Password = row["Password"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                //Groups = Users.GetAllGroups(row["UserID"].ToString()),
                //CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                //CreatedUser = row["CreatedUser"].ToString(),
                //LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                //LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                //DepartmentID = string.IsNullOrEmpty(row["DepartmentID"].ToString()) ? "0".ToString() : row["DepartmentID"].ToString(),
                Phone = string.IsNullOrEmpty(row["Phone"].ToString()) ? "".ToString() : row["Phone"].ToString(),
                //Team = string.IsNullOrEmpty(row["Team"].ToString()) ? "0".ToString() : row["Team"].ToString(),
                //Position = string.IsNullOrEmpty(row["Position"].ToString()) ? "0".ToString() : row["Position"].ToString(),
                //Gender = row["Gender"].ToString(),
                //EmployeeID = row["EmployeeID"].ToString(),
                //RefID = row["RefID"].ToString(),
            }).ToList();
        }
        public static List<Users> GetUsers()
        {
            string PROCEDURE = "p_SelectUsersDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Users
            {
                //KeyUserID = Helpers.Locdau.convertToUnSign3(row["ID"].ToString()),
                //Id = row["UserID"].ToString(),
                UserName = row["UserName"].ToString(),
                Email = row["Email"].ToString(),
                Password = row["Password"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                //Groups = Users.GetAllGroups(row["UserID"].ToString()),
                //CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                //CreatedUser = row["CreatedUser"].ToString(),
                //LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                //LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                //DepartmentID = string.IsNullOrEmpty(row["DepartmentID"].ToString()) ? "0".ToString() : row["DepartmentID"].ToString(),
                Phone = string.IsNullOrEmpty(row["Phone"].ToString()) ? "".ToString() : row["Phone"].ToString(),
                //Team = string.IsNullOrEmpty(row["Team"].ToString()) ? "0".ToString() : row["Team"].ToString(),
                //Position = string.IsNullOrEmpty(row["Position"].ToString()) ? "0".ToString() : row["Position"].ToString(),
                //Gender = row["Gender"].ToString(),
                //EmployeeID = row["EmployeeID"].ToString(),
                //RefID = row["RefID"].ToString(),
            }).ToList();
        }
    }
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}