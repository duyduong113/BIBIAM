using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Survey_Management
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Detail { get; set; }
        public int Target { get; set; }
        public int Actual { get; set; }
        public double Percent { get; set; }
        public int QuestionCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
    }
    public class DC_Survey_Management_Customer
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int SurveyManagementID { get; set; }
        [StringLength(100)]
        public string CustomerID { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        public string Name { get; set; }
        [StringLength(16)]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(16)]
        public string PhysicalID { get; set; }
        [StringLength(1024)]
        public string Address { get; set; }
        public bool IsDone { get; set; }
        //them cho CS
        public string OrderID { get; set; }
        public string CustomerRank { get; set; }
        public string Merchant { get; set; }
        public string Carrier { get; set; }
    }
    public class DC_Survey_Management_Question
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int SurveyManagementID { get; set; }
        [StringLength(100)]
        public string QuestionID { get; set; }
        public int SortOrder { get; set; }
        [Ignore]
        public string CategoryID { get; set; }
        [Ignore]
        public string Description { get; set; }
        [Ignore]
        public string Type { get; set; }
    }
    public class DC_Survey_Management_User
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        [Required]
        public int SurveyManagementID { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }

        [Ignore]
        [Required]
        public string listUserName { get; set; }
        [Ignore]
        public int UserGroup { get; set; }
    }
    public class DC_Survey_Management_Skip
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int ManagementCustomerID { get; set; }
        public int SurveyManagementID { get; set; }
        [StringLength(50)]
        [Required]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime? RecallTime { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        //public string Visibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class DC_Survey_Management_Proceeded
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int SurveyManagementID { get; set; }
        [StringLength(100)]
        public string QuestionID { get; set; }
        [StringLength(16)]
        public string QuestionType { get; set; }
        [StringLength(100)]
        public string AnswerID { get; set; }
        [StringLength(1024)]
        public string Answer { get; set; }
        [StringLength(100)]
        public string CustomerID { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        public string CustomerName { get; set; }
        [StringLength(16)]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string Phone { get; set; }
        [StringLength(16)]
        public string PhysicalID { get; set; }
        [StringLength(20)]
        public string Source { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        //public string Visibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //them cho CS
        public string OrderID { get; set; }
        public string CustomerRank { get; set; }
        public string Merchant { get; set; }
        public string Carrier { get; set; }
    }
    public class DC_Survey_Management_Proceeded_Answer
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int ProceededID { get; set; }
        [StringLength(100)]
        public string AnswerID { get; set; }
        [StringLength(1024)]
        public string Answer { get; set; }
    }
    public class DC_Survey_Category
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCategoryID { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public Boolean Active { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
    }
    public class DC_Survey_Question
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string CategoryID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string QuestionID { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public Boolean Active { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [Ignore]
        public string Name { get; set; }
        [Ignore]
        public string QuestionFrequency { get; set; }
        [Ignore]
        public string ManagementID { get; set; }
        [Ignore]
        public string UserSurvey { get; set; }
        [Ignore]
        public int Limit { get; set; }
        public static List<DC_Survey_Question> GetReviewResult(string ManagementID, string OrganizationID, string EmployeeID)
        {
            string PROCEDURE = "DC_Survey_List_Showing_Review";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ManagementID";
            parameters[i].Value = ManagementID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmployeeID";
            parameters[i].Value = EmployeeID;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Survey_Question
            {
                Description = row["Description"].ToString(),
                QuestionID = row["QuestionID"].ToString(),
                Type = row["Type"].ToString(),
                Answer = row["Answer"].ToString(),
                AnswerID = row["AnswerID"].ToString(),
                results = GetQuestionList(row["QuestionID"].ToString()),
            }).ToList();
        }
        public static List<DC_Survey_Question> GetQuestionByOrg(string ManagementID, string OrganizationID, int QuestionFrequency, string UserSurvey)
        {
            string PROCEDURE = "p_SelectDC_SurveyQuestion";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QuestionFrequency";
            parameters[i].Value = QuestionFrequency;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ManagementID";
            parameters[i].Value = ManagementID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserSurvey";
            parameters[i].Value = UserSurvey;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Survey_Question
            {
                Description = row["Description"].ToString(),
                QuestionID = row["QuestionID"].ToString(),
                Type = row["Type"].ToString(),
                results = GetQuestionList(row["QuestionID"].ToString()),
                Limit = int.Parse(row["Limit"].ToString()),
                Display = row["Display"].ToString(),
            }).ToList();
        }
        public static List<DC_Survey_Question> GetQuestionList(string QuestionID)
        {
            string PROCEDURE = "select * from DC_Survey_AnswerList where QuestionID = '" + QuestionID + "' AND Active = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Survey_Question
            {
                AnswerDescription = row["AnswerDescription"].ToString(),
                AnswerID = row["AnswerID"].ToString(),
                Answer = row["Answer"].ToString(),
                Description = row["Content"].ToString(),
            }).ToList();
        }
        [Ignore]
        public string Answer { get; set; }
        [Ignore]
        public string Display { get; set; }
        [Ignore]
        public string AnswerDescription { get; set; }
        [Ignore]
        public string AnswerID { get; set; }
        [Ignore]
        public List<DC_Survey_Question> results { set; get; }
    }
    public class DC_Survey_AnswerList
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string AnswerID { get; set; }
        public string AnswerDescription { get; set; }
        [Default(typeof(string),"No")]
        public string Answer { get; set; }
        public string Content { get; set; }
        public string QuestionID { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public Boolean Active { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }

    }
    public class DC_Survey_AnswerConfig
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int Detail { get; set; }
        public string Description { get; set; }
        public string QuestionID { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public Boolean Active { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [Ignore]
        public string AnswerID { get; set; }
    }
    public class DC_Survey_ExcelView
    {
        public string SurveyTitle { get; set; }
        public int ProceededID { get; set; }
        public string CustomerID { get; set; }
        public string QuestionID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string PhysicalID { get; set; }
        public string CustomerSource { get; set; }
        public string QuestionContent { get; set; }
        public string QuestionType { get; set; }
        public string AnswerID { get; set; }
        public string Note { get; set; }
        public DateTime SurveyTime { get; set; }
        public string SurveyBy { get; set; }

        //them cho CS
        public string OrderID { get; set; }
        public string CustomerRank { get; set; }
        public string Merchant { get; set; }
        public string Carrier { get; set; }
    }
}