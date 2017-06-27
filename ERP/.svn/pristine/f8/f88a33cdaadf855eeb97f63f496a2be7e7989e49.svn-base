using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_Commission_Program
    {
        [PrimaryKey]
        [StringLength(20)]
        public string Id { get; set; }
        [StringLength(256)]
        [Required]
        public string ProgramName { get; set; }
        [StringLength(10)]
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(20)]
        public string BudgetID { get; set; }
        [StringLength(10)]
        public string Status { get; set; }
        public bool Active { get; set; }
        public int Priotity { get; set; }
        public bool isCombine { get; set; }
        public bool isAccumulated { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
        public string Compute { get; set; }
        public int Threshold { get; set; }
        public string TypeMinMax { get; set; }
        public int Minimum { get; set; }
        public string ComputeBy { get; set; }
        public bool isFixed { get; set; }
        public string isBonusType { get; set; }
        public string RejectRegion { get; set; }
        public string TypeOfMinimum { get; set; }
        public string Note { get; set; }

        public static DC_Commission_Program GetLastID()
        {
            string PROCEDURE = "SELECT TOP 1 Id FROM DC_Commission_Program ORDER BY RowCreatedTime DESC";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                Id = row["Id"].ToString()
            }).ToList().FirstOrDefault();
        }

        [Ignore]
        public string BudgetName { get; set; }
        [Ignore]
        public string Description { get; set; }
        [Ignore]
        public float BonusValue { get; set; }
        [Ignore]
        public float BonusMin { get; set; }
        [Ignore]
        public float BonusMax { get; set; }
        public static List<DC_Commission_Program> GetAll_DC_Commission_ProgramGetProgramByUser(string UserID, string OrganizationID)
        {
            string PROCEDURE = "p_DC_Commission_ProgramGetProgramByUser";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@OrganizationID";
            parameters[1].Value = OrganizationID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                Id = row["Id"].ToString(),
                ProgramName = row["ProgramName"].ToString(),
                //BudgetName = row["BudgetName"].ToString(),
                Description = row["Description"].ToString(),
                BonusValue = float.Parse(row["BonusValue"].ToString()),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                BonusMin = float.Parse(row["BonusMin"].ToString()),
                BonusMax = float.Parse(row["BonusMax"].ToString()),
            }).ToList();
        }

        [Ignore]
        public float Bonus { get; set; }
        [Ignore]
        public float Actual { get; set; }
        public static List<DC_Commission_Program> GetAll_DC_Commission_ProgramProgramByUser(string UserID)
        {
            string PROCEDURE = "p_SelectDC_Commission_ProgramByUser";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                ProgramName = row["ProgramName"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Threshold = int.Parse(row["Threshold"].ToString()),
                Bonus = float.Parse(row["Bonus"].ToString()),
                Actual = float.Parse(row["Actual"].ToString())
            }).ToList();
        }

        public static DC_Commission_Program GetAll_DC_Commission_ProgramProgramGetBonusByUser(string UserID)
        {
            string PROCEDURE = "p_SelectDC_Commission_ProgramGetBonusByUser";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                Bonus = float.Parse(row["Bonus"].ToString())
            }).FirstOrDefault();
        }

        public static DC_Commission_Program GetAll_DC_Commission_ProgramProgramGetBonusByTeam(string UserID)
        {
            string PROCEDURE = "p_SelectDC_Commission_ProgramGetBonusByTeam";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                Bonus = float.Parse(row["Bonus"].ToString())
            }).FirstOrDefault();
        }

        [Ignore]
        public string StatusBG { get; set; }
        public static List<DC_Commission_Program> GetAllData()
        {
            string PROCEDURE = "p_DC_Commission_ProgramGetAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                Id = row["Id"].ToString(),
                ProgramName = row["ProgramName"].ToString(),
                Type = row["Type"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                BudgetID = row["BudgetID"].ToString(),
                Status = row["Status"].ToString(),
                Active = bool.Parse(row["Active"].ToString()),
                Priotity = int.Parse(row["Priotity"].ToString()),
                isCombine = bool.Parse(row["isCombine"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                isAccumulated = bool.Parse(row["isAccumulated"].ToString()),
                Compute = row["Compute"].ToString(),
                Threshold = int.Parse(row["Threshold"].ToString()),
                ComputeBy = row["ComputeBy"].ToString(),
                isFixed = bool.Parse(row["isFixed"].ToString()),
                isBonusType = row["isBonusType"].ToString(),
                RejectRegion = row["RejectRegion"].ToString(),
                Minimum = int.Parse(row["Minimum"].ToString()),
                TypeMinMax = row["TypeMinMax"].ToString(),
                TypeOfMinimum = row["TypeOfMinimum"].ToString(),
                StatusBG = row["StatusBG"].ToString(),
                Note = row["Note"].ToString()
            }).ToList();
        }

        public static List<DC_Commission_Program> GetAllDataReport(string StartDate, string EndDate)
        {
            string PROCEDURE = "p_DC_Commission_Report";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@StartDate";
            parameters[0].Value = StartDate;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@EndDate";
            parameters[1].Value = EndDate;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Commission_Program
            {
                Id = row["Id"].ToString(),
                ProgramName = row["ProgramName"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Status = row["Status"].ToString(),
                TypeMinMax = row["TypeMinMax"].ToString(),
                TypeOfMinimum = row["TypeOfMinimum"].ToString(),
                SourceID = row["SourceID"].ToString(),
                UserName = row["UserName"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                Bonus = float.Parse(row["Bonus"].ToString()),
                Actual = float.Parse(row["Actual"].ToString()),
                ActualCommission = float.Parse(row["ActualCommission"].ToString()),
                Note = row["Note"].ToString(),
                DayAt = row["DayAt"].ToString(),
                MonthAt = row["MonthAt"].ToString(),
                YearAt = row["YearAt"].ToString(),
                approve = bool.Parse(row["approve"].ToString()),
            }).ToList();
        }

        [Ignore]
        public string SourceID { get; set; }
        [Ignore]
        public string UserName { get; set; }
        [Ignore]
        public string EmployeeID { get; set; }
        [Ignore]
        public float ActualCommission { get; set; }
        [Ignore]
        public string DayAt { get; set; }
        [Ignore]
        public string MonthAt { get; set; }
        [Ignore]
        public string YearAt { get; set; }
        [Ignore]
        public bool approve { get; set; }
    }
}