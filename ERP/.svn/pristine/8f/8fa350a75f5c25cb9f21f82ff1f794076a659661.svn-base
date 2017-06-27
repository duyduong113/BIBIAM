using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Kendo.Mvc.UI;
using ServiceStack.DataAnnotations;


namespace ERPAPD.Models
{
    public class CRM_Ticket
    {
        #region Members

        public string TicketID { get; set; }
        public string Status { get; set; }
        public string TypeID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Requestor { get; set; }
        public string CustomerSource { get; set; }
        public string RequestFrom { get; set; }
        public string RefID { get; set; }
        public DateTime? ReCallTime { get; set; }
        public string Owner { get; set; }
        public string preAssignee { get; set; }
        public string Assignee { get; set; }
        public string OrganizationID { get; set; }
        public string CustomerID { get; set; }
        public string Priority { get; set; }
        public string Impact { get; set; }
        public int ResponeTime { get; set; }
        public int ResolveTime { get; set; }
        public int ClosedTime { get; set; }
        public DateTime ResponseDate { get; set; }
        public DateTime ResolveDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime ResponseDeadline { get; set; }
        public DateTime ResolveDeadline { get; set; }
        public DateTime CloseDeadline { get; set; }
        public int RowID { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }
        public string CategoryID { get; set; }
        public int FileNumber { get; set; }
        public string LastActivities { get; set; }
        public string LastComment { get; set; }
        public int PassedTime { get; set; }
        public string CustomerName { get; set; }
        public string EscalateQueue { get; set; }
        public string preEscalateQueue { get; set; }
        public DateTime Reopentime { get; set; }
        public string RName { get; set; }
        public string REmail { get; set; }
        public string RActive { get; set; }
        public string RGender { get; set; }
        public string RDepartmentID { get; set; }
        public string RTeam { get; set; }
        public string RPosition { get; set; }
        public string RPhone { get; set; }
        public string IsFollower { get; set; }
        public string IsQueue { get; set; }
        public string IsEscalate { get; set; }
        public string IspreEscalate { get; set; }
        public DateTime CustomerExpectationTimeLine { get; set; }
        public string ImportNote { get; set; }
       // DungNT: add column IsDone.used for TeleSale
        public bool IsDone { get; set; }
        public string Feeling { get; set; }
      //  collum Carrier join tu bang Order_Header
        public string Carrier { get; set; }

       // collum Merchant join tu bang Order_Header
        public string Merchant { get; set; }
        public string OrderLink { get; set; }
        public List<CRM_Ticket_Follower> Follower { get; set; }
        #endregion

        public CRM_Ticket() { }

        private void genetime()
        {
            var work = CRM_Ticket_WorkTime.GetCRM_Ticket_WorkTime_ByUserName(this.Owner);
            var holiday = CRM_Ticket_Holiday.GetCRM_Ticket_Holiday_ByDate(this.RowCreatedTime);
            this.ResponseDeadline = addtime(work, holiday, this.ResponeTime);
            this.ResolveDeadline = addtime(work, holiday, this.ResolveTime);
            this.CloseDeadline = addtime(work, holiday, this.ClosedTime);
        }
        private DateTime addtime(CRM_Ticket_WorkTime work, List<CRM_Ticket_Holiday> holiday, int time)
        {
            DateTime result = this.RowCreatedTime.AddMinutes(time);
            return result;
        }

        public string Save()
        {
            var type = CRM_Ticket_Type.GetCRM_Ticket_Type(this.TypeID);
            if (type == null)
            {
                return "";
            }
            this.ResponeTime = 0;
            this.ResolveTime = 0;
            this.ClosedTime = 0;

            var kpi = CRM_Ticket_KPI.GetCRM_Ticket_KPI(TypeID, Priority, Impact);
            if (kpi != null)
            {
                this.ResponeTime = kpi.ResponeTime;
                this.ResolveTime = kpi.ResolveTime;
                this.ClosedTime = kpi.ClosedTime;
            }
            else
            {
                this.ResponeTime = type.ResponeTime;
                this.ResolveTime = type.ResolveTime;
                this.ClosedTime = type.ClosedTime;
            }

            this.RowLastUpdatedTime = DateTime.Now;
            genetime();

            string PROCEDURE = "p_SaveCRM_Ticket";
            SqlParameter[] parameters = new SqlParameter[27];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Title";
            parameters[i].Value = this.Title;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Detail";
            parameters[i].Value = this.Detail;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Requestor";
            parameters[i].Value = this.Requestor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Owner";
            parameters[i].Value = this.Owner;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@preAssignee";
            parameters[i].Value = this.preAssignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assignee";
            parameters[i].Value = this.Assignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this.CustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = this.OrderID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Impact";
            parameters[i].Value = this.Impact;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponeTime";
            parameters[i].Value = this.ResponeTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveTime";
            parameters[i].Value = this.ResolveTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClosedTime";
            parameters[i].Value = this.ClosedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponseDeadline";
            parameters[i].Value = this.ResponseDeadline;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveDeadline";
            parameters[i].Value = this.ResolveDeadline;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CloseDeadline";
            parameters[i].Value = this.CloseDeadline;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerSource";
            parameters[i].Value = string.IsNullOrEmpty(this.CustomerSource) ? "" : this.CustomerSource;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequestFrom";
            parameters[i].Value = string.IsNullOrEmpty(this.RequestFrom) ? "" : this.RequestFrom;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ReCallTime";
            parameters[i].Value = this.ReCallTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerName";
            parameters[i].Value = this.CustomerName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IsDone";
            parameters[i].Value = this.IsDone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RefID";
            parameters[i].Value = this.RefID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerExpectationTimeLine";
            parameters[i].Value = this.CustomerExpectationTimeLine.Year < 1900 ? DateTime.Parse("1900-01-01") : this.CustomerExpectationTimeLine;
            i++;
            foreach (var y in parameters)
            {
                if (y.Value == null)
                {
                    y.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters).ToString();
        }
       // update : x=1 : tinh lai gia tri deadline
        public int Update(int x)
        {
            if (x == 1)
            {
                var type = CRM_Ticket_Type.GetCRM_Ticket_Type(this.TypeID);

                var kpi = CRM_Ticket_KPI.GetCRM_Ticket_KPI(TypeID, Priority, Impact);
                if (kpi != null)
                {
                    this.ResponeTime = kpi.ResponeTime;
                    this.ResolveTime = kpi.ResolveTime;
                    this.ClosedTime = kpi.ClosedTime;
                }
                else
                {
                    if (type != null)
                    {
                        this.ResponeTime = type.ResponeTime;
                        this.ResolveTime = type.ResolveTime;
                        this.ClosedTime = type.ClosedTime;
                    }
                }
                genetime();
            }

            string PROCEDURE = "p_UpdateCRM_Ticket";
            SqlParameter[] parameters = new SqlParameter[30];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Title";
            parameters[i].Value = this.Title;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Detail";
            parameters[i].Value = this.Detail;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Requestor";
            parameters[i].Value = this.Requestor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Owner";
            parameters[i].Value = this.Owner;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@preAssignee";
            parameters[i].Value = this.preAssignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Assignee";
            parameters[i].Value = this.Assignee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IsDone";
            parameters[i].Value = this.IsDone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this.CustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = this.OrderID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Impact";
            parameters[i].Value = this.Impact;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponeTime";
            parameters[i].Value = this.ResponeTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveTime";
            parameters[i].Value = this.ResolveTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClosedTime";
            parameters[i].Value = this.ClosedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponseDate";
            parameters[i].Value = this.ResponseDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveDate";
            parameters[i].Value = this.ResolveDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CloseDate";
            parameters[i].Value = this.CloseDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponseDeadline";
            parameters[i].Value = this.ResponseDeadline;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveDeadline";
            parameters[i].Value = this.ResolveDeadline;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CloseDeadline";
            parameters[i].Value = this.CloseDeadline;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastActivities";
            parameters[i].Value = this.LastActivities;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastComment";
            parameters[i].Value = this.LastComment;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EscalateQueue";
            parameters[i].Value = this.EscalateQueue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@preEscalateQueue";
            parameters[i].Value = this.preEscalateQueue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Reopentime";
            parameters[i].Value = this.Reopentime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            foreach (var y in parameters)
            {
                if (y.Value == null)
                {
                    y.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }
       // chưa rõ
        public int UpdateCreateTime()
        {

            string PROCEDURE = "p_UpdateCRM_Ticket_CreateTime";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int UpdateNew()
        {

            string PROCEDURE = "update CRM_Ticket set isnew = 1 where TicketID = @TicketID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }

       // DungNT: add fuction update stautus Ticket
        public int UpdateTicketByStatus()
        {

            string PROCEDURE = "p_UpdateCRM_TicketByStatus";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteCRM_Ticket";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static CRM_Ticket GetCRM_Ticket(string TicketID)
        {
            string PROCEDURE = "p_SelectCRM_Ticket";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
                Reopentime = Convert.ToDateTime(row["Reopentime"].ToString()),
                OrderID = row["OrderID"].ToString()
            }).ToList().FirstOrDefault();
        }


        public static List<CRM_Ticket> GetAllCRM_Tickets(string UserName)
        {
            string PROCEDURE = "p_SelectCRM_TicketsAll";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CategoryID = row["CategoryID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                PassedTime = Convert.ToInt32(row["PassedTime"].ToString()),
                CustomerName = row["CustomerName"].ToString(),

                RName = row["RName"].ToString(),
                REmail = row["REmail"].ToString(),
                RActive = row["RActive"].ToString(),
                RGender = row["RGender"].ToString(),
                RDepartmentID = row["RDepartmentID"].ToString(),
                RTeam = row["RTeam"].ToString(),
                RPosition = row["RPosition"].ToString(),
                RPhone = row["RPhone"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
                IsEscalate = row["IsEscalate"].ToString(),
                IspreEscalate = row["IspreEscalate"].ToString(),
                IsFollower = row["IsFollower"].ToString(),
                IsQueue = row["IsQueue"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                MOPRegion = row["MOPRegion"].ToString(),
            }).ToList();
        }

        public static List<CRM_Ticket> GetAllCRM_Tickets_ViewAll(string UserName)
        {
            string PROCEDURE = "p_SelectCRM_TicketsAll_ViewAll";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CategoryID = row["CategoryID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                PassedTime = Convert.ToInt32(row["PassedTime"].ToString()),
                CustomerName = row["CustomerName"].ToString(),

                RName = row["RName"].ToString(),
                REmail = row["REmail"].ToString(),
                RActive = row["RActive"].ToString(),
                RGender = row["RGender"].ToString(),
                RDepartmentID = row["RDepartmentID"].ToString(),
                RTeam = row["RTeam"].ToString(),
                RPosition = row["RPosition"].ToString(),
                RPhone = row["RPhone"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
                IsEscalate = row["IsEscalate"].ToString(),
                IspreEscalate = row["IspreEscalate"].ToString(),
                IsFollower = row["IsFollower"].ToString(),
                IsQueue = row["IsQueue"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                MOPRegion = row["MOPRegion"].ToString(),
                Follower = CRM_Ticket_Follower.GetAllCRM_Ticket_Followers(row["TicketID"].ToString())
            }).ToList();
        }
        public static DataSourceResult GetAllCRM_Tickets_Dynamic(string UserName, string where, [DataSourceRequest] DataSourceRequest request)
        {
            string PROCEDURE = "p_SelectCRM_TicketsAll_Dynamic";
            var from1 = (request.Page - 1) * request.PageSize + 1;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = where;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            var data = new DataSourceResult();
            data.Data = dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CategoryID = row["CategoryID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                PassedTime = Convert.ToInt32(row["PassedTime"].ToString()),
                CustomerName = row["CustomerName"].ToString(),

                RName = row["RName"].ToString(),
                REmail = row["REmail"].ToString(),
                RActive = row["RActive"].ToString(),
                RGender = row["RGender"].ToString(),
                RDepartmentID = row["RDepartmentID"].ToString(),
                RTeam = row["RTeam"].ToString(),
                RPosition = row["RPosition"].ToString(),
                RPhone = row["RPhone"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
                IsEscalate = row["IsEscalate"].ToString(),
                IspreEscalate = row["IspreEscalate"].ToString(),
                IsFollower = row["IsFollower"].ToString(),
                IsQueue = row["IsQueue"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
                MOPRegion = row["MOPRegion"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                Carrier = row["Carrier"].ToString(),
                Merchant = row["Merchant"].ToString(),
                OrderLink = row["OrderLink"].ToString(),
                CustomerExpectationTimeLine = Convert.ToDateTime(string.IsNullOrEmpty(row["CustomerExpectationTimeLine"].ToString()) ? "1900-01-01" : row["CustomerExpectationTimeLine"].ToString()),
                Follower = CRM_Ticket_Follower.GetAllCRM_Ticket_Followers(row["TicketID"].ToString())
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();
            data.Total = total != null ? total.TotalRows : 0;
            return data;
        }
        public static DataSourceResult GetAllCRM_Tickets_ViewAll_Dynamic(string UserName, string where, [DataSourceRequest] DataSourceRequest request)
        {
            string PROCEDURE = "p_SelectCRM_TicketsAll_ViewAll_Dynamic";
            var from1 = (request.Page - 1) * request.PageSize + 1;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = where;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            var data = new DataSourceResult();
            data.Data = dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CategoryID = row["CategoryID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                PassedTime = Convert.ToInt32(row["PassedTime"].ToString()),
                CustomerName = row["CustomerName"].ToString(),

                RName = row["RName"].ToString(),
                REmail = row["REmail"].ToString(),
                RActive = row["RActive"].ToString(),
                RGender = row["RGender"].ToString(),
                RDepartmentID = row["RDepartmentID"].ToString(),
                RTeam = row["RTeam"].ToString(),
                RPosition = row["RPosition"].ToString(),
                RPhone = row["RPhone"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
                IsEscalate = row["IsEscalate"].ToString(),
                IspreEscalate = row["IspreEscalate"].ToString(),
                IsFollower = row["IsFollower"].ToString(),
                IsQueue = row["IsQueue"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
                MOPRegion = row["MOPRegion"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                Carrier = row["Carrier"].ToString(),
                Merchant = row["Merchant"].ToString(),
                OrderLink = row["OrderLink"].ToString(),
                CustomerExpectationTimeLine = Convert.ToDateTime(string.IsNullOrEmpty(row["CustomerExpectationTimeLine"].ToString()) ? "1900-01-01" : row["CustomerExpectationTimeLine"].ToString()),
                Follower = CRM_Ticket_Follower.GetAllCRM_Ticket_Followers(row["TicketID"].ToString())

            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DW_SMSMO
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();
            data.Total = total != null ? total.TotalRows : 0;
            return data;
        }

        public static List<CRM_Ticket> GetCRM_Tickets_Suggets_ViewAll(string UserName, string where)
        {
            string PROCEDURE = "p_SelectCRM_Tickets_Suggets_ViewAll";
            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = where;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN 1 AND 10";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            var data = new List<CRM_Ticket>();
            data = dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
            }).ToList();

            return data;
        }


        public static List<CRM_Ticket> GetAllCRM_Tickets_ViewAll_Dynamic_API(string fromDate, string toDate)
        {
            string PROCEDURE = "p_SelectCRM_TicketsAll_ViewAll_Dynamic";

            SqlParameter[] parameters = new SqlParameter[3];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = "data.RowLastUpdatedTime BETWEEN '" + fromDate + "' AND '" + toDate + "'";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "1=1";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = "administrator";
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);

            var data = new List<CRM_Ticket>();
            data = dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CategoryID = row["CategoryID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                PassedTime = Convert.ToInt32(row["PassedTime"].ToString()),
                CustomerName = row["CustomerName"].ToString(),

                RName = row["RName"].ToString(),
                REmail = row["REmail"].ToString(),
                RActive = row["RActive"].ToString(),
                RGender = row["RGender"].ToString(),
                RDepartmentID = row["RDepartmentID"].ToString(),
                RTeam = row["RTeam"].ToString(),
                RPosition = row["RPosition"].ToString(),
                RPhone = row["RPhone"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
                IsEscalate = row["IsEscalate"].ToString(),
                IspreEscalate = row["IspreEscalate"].ToString(),
                IsFollower = row["IsFollower"].ToString(),
                IsQueue = row["IsQueue"].ToString(),
                OrgStatus = row["OrgStatus"].ToString(),
                Approver = row["Approver"].ToString(),
                MOPRegion = row["MOPRegion"].ToString(),
                RegionalOPS = row["RegionalOPS"].ToString(),
                Follower = CRM_Ticket_Follower.GetAllCRM_Ticket_Followers(row["TicketID"].ToString())

            }).ToList();

            return data;
        }
        public static List<CRM_Ticket> GetAllCRM_Tickets_Customer(string CustomerID)
        {
            string PROCEDURE = "p_SelectCRM_TicketsAll_Customer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Where(s => s["TicketID"].ToString() != "").Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
                Status = row["Status"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Title = row["Title"].ToString(),
                Detail = row["Detail"].ToString(),
                Requestor = row["Requestor"].ToString(),
                Owner = row["Owner"].ToString(),
                preAssignee = row["preAssignee"].ToString(),
                Assignee = row["Assignee"].ToString(),
                Priority = row["Priority"].ToString(),
                Impact = row["Impact"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                ResponseDate = Convert.ToDateTime(row["ResponseDate"].ToString()),
                ResolveDate = Convert.ToDateTime(row["ResolveDate"].ToString()),
                CloseDate = Convert.ToDateTime(row["CloseDate"].ToString()),
                ResponseDeadline = Convert.ToDateTime(row["ResponseDeadline"].ToString()),
                ResolveDeadline = Convert.ToDateTime(row["ResolveDeadline"].ToString()),
                CloseDeadline = Convert.ToDateTime(row["CloseDeadline"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                CategoryID = row["CategoryID"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                PassedTime = Convert.ToInt32(row["PassedTime"].ToString()),
                RName = row["RName"].ToString(),
                REmail = row["REmail"].ToString(),
                RActive = row["RActive"].ToString(),
                RGender = row["RGender"].ToString(),
                RDepartmentID = row["RDepartmentID"].ToString(),
                RTeam = row["RTeam"].ToString(),
                RPosition = row["RPosition"].ToString(),
                RPhone = row["RPhone"].ToString(),
                EscalateQueue = row["EscalateQueue"].ToString(),
                preEscalateQueue = row["preEscalateQueue"].ToString(),
            }).ToList();
        }
        public static CRM_Ticket GetCRM_Ticket_checkcanview(string TicketID, string UserName, string Department, string Team)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_checkuser";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = string.IsNullOrEmpty(Department) ? "" : Department;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Team";
            parameters[i].Value = string.IsNullOrEmpty(Team) ? "" : Team;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket
            {
                TicketID = row["TicketID"].ToString(),
            }).ToList().FirstOrDefault();
        }
        public static CRM_Ticket GetCRM_Ticket_checkintype(string TypeID, string UserName)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_CheckUserInType";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = TypeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket
            {
                TypeID = row["TypeID"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public void GetFileNumber(string pathForSaving)
        {

            try
            {
                this.FileNumber = Directory.GetFiles(pathForSaving + this.TicketID).Length;
            }
            catch
            {
                this.FileNumber = 0;
            }

        }
        public static string CountMyTicket(string UserName)
        {
            string PROCEDURE = "select count(ticketid)  from CRM_Ticket where isnew = 1 and  Status NOT in ('Closed', 'Cancelled', 'Resolved') and Assignee = '" + UserName.Replace("'", "''") + "'";

            var result = SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null).ToString();
            return result;

        }
        public static string CountNewTicket(string UserName)
        {
            string PROCEDURE = "select count(ticketid)  from CRM_Ticket where isnew = 1 and  Status NOT in ('Closed', 'Cancelled', 'Resolved') and preAssignee = '" + UserName.Replace("'", "''") + "'  ";

            var result = SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null).ToString();
            return result;

        }
        public static string CountCreatedTicket(string UserName)
        {
            string PROCEDURE = "select count(ticketid)  from CRM_Ticket where isnew = 1 and  Status NOT in ('Closed', 'Cancelled', 'Resolved') and Requestor = '" + UserName.Replace("'", "''") + "'  ";

            var result = SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null).ToString();
            return result;

        }
        public static string CountResolvedTicket(string UserName)
        {
            string PROCEDURE = "select count(ticketid)  from CRM_Ticket where isnew = 1 and  Status =  'Resolved' and Requestor = '" + UserName.Replace("'", "''") + "'  ";

            var result = SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null).ToString();
            return result;

        }
        public static string CountQueueTicket(string UserName)
        {

            string PROCEDURE = "p_SelectCRM_Ticket_CountQueueTicket";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = UserName;
            i++;
            var result = SqlHelperAsync.ExecuteScalar(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters).ToString();
            return result;
        }

        public int UpdateIsDoneForTeleSale()
        {

            string PROCEDURE = "p_UpdateCRM_Ticket_IsDoneForTeleSale";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@IsDone";
            parameters[i].Value = this.IsDone;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public string OrgStatus { get; set; }

        public string Approver { get; set; }

        public string RegionalOPS { get; set; }

        public string MOPRegion { get; set; }

        public string OrderID { get; set; }
    }
}