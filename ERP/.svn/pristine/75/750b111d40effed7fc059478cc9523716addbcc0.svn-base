using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class CRM_Works

    {
        [AutoIncrement]
        [PrimaryKey]
        public Int32 RowID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string CustomerID { get; set; }
        public string Person_contact { get; set; }
        public string Files { get; set; }
        public string Performer { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? RemindDate { get; set; }

        public Int32 Priority { get; set; }
        public string TicketID { get; set; }

        public DateTime? RowCreatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string TypeName { get; set; }
        [Ignore]
        public string CustomerName { get; set; }
        [Ignore]
        public string EmName { get; set; }
        [Ignore]
        public string Name { get; set; }
        [Ignore]
        public string EmEmail { get; set; }
        public static object Save(CRM_Ticket_Task_Appointment request, string username, string ticket = "")
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var checkExits = dbConn.SingleOrDefault<CRM_Works>("RowID= {0}", request.RowID);
                    if (checkExits == null)
                    {
                        var work = new CRM_Works();
                        work.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        work.CustomerID = !string.IsNullOrEmpty(request.CustomerID) ? request.CustomerID.Trim() : "";
                        work.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        work.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : "";
                        work.Person_contact = !string.IsNullOrEmpty(request.Person_contact) ? request.Person_contact.Trim() : "";
                        work.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                        work.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "YET";
                        work.Deadline = !string.IsNullOrEmpty(request.Deadline.ToString()) ? request.Deadline : DateTime.Parse("1900-01-01");
                        work.RemindDate = !string.IsNullOrEmpty(request.RemindDate.ToString()) ? request.RemindDate : DateTime.Parse("1900-01-01");
                        work.Performer = !string.IsNullOrEmpty(request.Performer) ? request.Performer.Trim() : "";
                        work.Priority = request.Priority != 0 ? request.Priority : 0;
                        work.TicketID = ticket;
                        work.RowCreatedUser = username;
                        work.RowUpdatedUser = "";
                        work.RowCreatedAt = DateTime.Now;
                        work.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        dbConn.Insert(work);
                        Portal_Message.SendMessage("administrator", "addWork", "Thêm công việc thành công", "");
                        return new { success = true, message = "Inserted success!" };
                    }
                    else {
                        if (checkExits.Status != "DONE")
                        {
                            checkExits.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                            checkExits.CustomerID = !string.IsNullOrEmpty(request.CustomerID) ? request.CustomerID.Trim() : "";
                            checkExits.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                            checkExits.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : "";
                            checkExits.Person_contact = !string.IsNullOrEmpty(request.Person_contact) ? request.Person_contact.Trim() : "";
                            checkExits.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                            checkExits.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "YET";
                            checkExits.Deadline = !string.IsNullOrEmpty(request.Deadline.ToString()) ? request.Deadline : DateTime.Parse("1900-01-01");
                            checkExits.Performer = !string.IsNullOrEmpty(request.Performer) ? request.Performer.Trim() : "";
                            checkExits.Priority = request.Priority != 0 ? request.Priority : 0;
                            checkExits.RowUpdatedUser = username;
                            checkExits.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(checkExits);
                            return new { success = true, message = "Update status" };
                        }
                        else
                        {
                            return new { success = false, message = "Không thể cập nhật trạng thái Hoàn tất" };

                        }
                    }
                }
                catch (Exception e)
                {
                    return new { success = false, message = e.Message };
                }
            }

        }
        public static object Update_Status(Int32 RowID, string userName)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var work = dbConn.SingleOrDefault<CRM_Works>("RowID= {0}", RowID);
                    if (work != null)
                    {
                        work.Status = "DONE";
                        work.RowUpdatedUser = userName;
                        work.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(work);
                        Portal_Message.SendMessage("administrator", "addWork", "Work update !", "");
                        return new { success = true, message = "Update status DONE" };
                    }
                    else
                    {
                        return new { success = false, message = "Failed" };
                    }
                }
                catch (Exception e)
                {
                    return new { success = false, message = e.Message };
                }
            }
        }
    }
}