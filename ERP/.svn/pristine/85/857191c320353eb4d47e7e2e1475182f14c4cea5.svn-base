using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;


namespace ERPAPD.Models
{
    public class CRM_Telesale_History_Call
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        public string Title { get; set; }
        public string CustomerID { get; set; }
        public string Description { get; set; }
        public DateTime? RecallTime { get; set; }
        public string TrendID { get; set; }
        public string GroupTypeID { get; set; }
        public string TypeRequestID { get; set; }
        public string BehavID { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public static object Save(CRM_Ticket_Task_Appointment request, string username, string ticket)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var checkExits = dbConn.SingleOrDefault<CRM_Telesale_History_Call>("ID= {0}", request.ID);
                    if (checkExits == null)
                    {
                        var work = new CRM_Telesale_History_Call();
                        work.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        work.CustomerID = !string.IsNullOrEmpty(request.CustomerID) ? request.CustomerID.Trim() : "";
                        work.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : "";
                        work.GroupTypeID = !string.IsNullOrEmpty(request.GroupTypeID) ? request.GroupTypeID.Trim() : "";
                        work.TypeRequestID = !string.IsNullOrEmpty(request.TypeRequestID) ? request.TypeRequestID.Trim() : "";
                        work.TrendID = !string.IsNullOrEmpty(request.TrendID) ? request.TrendID.Trim() : "";
                        work.RecallTime = !string.IsNullOrEmpty(request.RecallTime.ToString()) ? request.RecallTime : DateTime.Parse("1900-01-01");
                        work.BehavID = !string.IsNullOrEmpty(request.BehavID) ? request.BehavID.Trim() : "";
                        work.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "YET";
                        work.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";

                        work.RowCreatedUser = username;
                        work.RowUpdatedUser = "";
                        work.RowCreatedAt = DateTime.Now;
                        work.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        dbConn.Insert(work);
                        //Portal_Message.SendMessage("administrator", "addWork", "Work insert !", "");
                        return new { success = true, message = "Inserted success!" };
                    }
                    else {

                        checkExits.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        checkExits.CustomerID = !string.IsNullOrEmpty(request.CustomerID) ? request.CustomerID.Trim() : "";
                        checkExits.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : "";
                        checkExits.GroupTypeID = !string.IsNullOrEmpty(request.GroupTypeID) ? request.GroupTypeID.Trim() : "";
                        checkExits.TypeRequestID = !string.IsNullOrEmpty(request.TypeRequestID) ? request.TypeRequestID.Trim() : "";
                        checkExits.TrendID = !string.IsNullOrEmpty(request.TrendID) ? request.TrendID.Trim() : "";
                        checkExits.RecallTime = !string.IsNullOrEmpty(request.RecallTime.ToString()) ? request.RecallTime : DateTime.Parse("1900-01-01");
                        checkExits.BehavID = !string.IsNullOrEmpty(request.BehavID) ? request.BehavID.Trim() : "";
                        checkExits.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "YET";
                        checkExits.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";

                        checkExits.RowUpdatedUser = username;
                        checkExits.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(checkExits);
                        return new { success = true, message = "Update status" };
                    }
                }
                catch (Exception e)
                {
                    return new { success = false, message = e.Message };
                }
            }

        }
    }
    public class CRM_Ticket_Task_Appointment
    {
        public Int32 RowID { get; set; }
        public Int32 IDTask { get; set; }
        public Int32 IDAppoint { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string CustomerID { get; set; }
        public string Description { get; set; }
        public DateTime? RecallTime { get; set; }
        public string TrendID { get; set; }
        public string GroupTypeID { get; set; }
        public string TypeRequestID { get; set; }
        public string BehavID { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public string Person_contact { get; set; }
        public string Files { get; set; }
        public string Performer { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? RemindDate { get; set; }
        public Int32 Priority { get; set; }

        public string Address { get; set; }
        public int Hours { get; set; }
        public string Guests { get; set; }
        public string Bcc { get; set; }
        public DateTime? Date { get; set; }

    }
}