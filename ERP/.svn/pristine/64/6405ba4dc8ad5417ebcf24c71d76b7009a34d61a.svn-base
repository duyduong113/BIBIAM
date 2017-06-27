using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class CRM_Appointment
    {
        [AutoIncrement]
        [PrimaryKey]
        public Int32 RowID { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string Address { get; set; }
        public int Hours { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public string Description { get; set; }
        public string CustomerID { get; set; }
        public string Person_contact { get; set; }
        public string Guests { get; set; }
        public string Bcc { get; set; }
        public string Files { get; set; }
        public string TicketID { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowUpdatedUser { get; set; }

        [Ignore]
        public string CustomerName { get; set; }
        [Ignore]
        public string Name { get; set; }
        [Ignore]
        public string TypeName { get; set; }

        public static object Save(CRM_Ticket_Task_Appointment request, string username, string ticket = "")
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var checkExits = dbConn.SingleOrDefault<CRM_Appointment>("RowID= {0}", request.RowID);
                    if (checkExits == null)
                    {
                        var appointment = new CRM_Appointment();
                        appointment.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        appointment.CustomerID = !string.IsNullOrEmpty(request.CustomerID) ? request.CustomerID.Trim() : "";
                        appointment.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        appointment.Address = !string.IsNullOrEmpty(request.Address) ? request.Address.Trim() : "";
                        appointment.Hours = !(request.Hours == 0) ? request.Hours : 0;
                        appointment.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : "";
                        appointment.Person_contact = !string.IsNullOrEmpty(request.Person_contact) ? request.Person_contact.Trim() : "";
                        appointment.Guests = !string.IsNullOrEmpty(request.Guests) ? request.Guests.Trim() : "";
                        appointment.Bcc = !string.IsNullOrEmpty(request.Bcc) ? request.Bcc.Trim() : "";
                        appointment.Date = !string.IsNullOrEmpty(request.Date.ToString()) ? request.Date : DateTime.Parse("1900-01-01");
                        appointment.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                        appointment.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "YET";
                        appointment.TicketID = ticket;
                        if (ticket != "")
                        {
                            appointment.Date = !string.IsNullOrEmpty(request.RecallTime.ToString()) ? request.RecallTime : DateTime.Parse("1900-01-01");
                            appointment.Type = "APS01";
                        }
                        appointment.RowCreatedUser = username;
                        appointment.RowUpdatedUser = "";
                        appointment.RowCreatedAt = DateTime.Now;
                        appointment.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        dbConn.Insert(appointment);
                        Portal_Message.SendMessage("administrator", "addAppointment", "Thêm lịch hẹn thành công !", "");
                        return new { success = true, message = "Inserted success!" };
                    }
                    else
                    {
                        if (checkExits.Status != "DONE")
                        {
                            checkExits.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                            checkExits.CustomerID = !string.IsNullOrEmpty(request.CustomerID) ? request.CustomerID.Trim() : "";
                            checkExits.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                            checkExits.Address = !string.IsNullOrEmpty(request.Address) ? request.Address.Trim() : "";
                            checkExits.Hours = !(request.Hours == 0) ? request.Hours : 0;
                            checkExits.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : "";
                            checkExits.Person_contact = !string.IsNullOrEmpty(request.Person_contact) ? request.Person_contact.Trim() : "";
                            checkExits.Guests = !string.IsNullOrEmpty(request.Guests) ? request.Guests.Trim() : "";
                            checkExits.Bcc = !string.IsNullOrEmpty(request.Bcc) ? request.Bcc.Trim() : "";
                            checkExits.Date = !string.IsNullOrEmpty(request.Date.ToString()) ? request.Date : DateTime.Parse("1900-01-01");
                            checkExits.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                            checkExits.RowUpdatedAt = DateTime.Now;
                            checkExits.RowUpdatedUser = username;
                            dbConn.Update(checkExits);
                            Portal_Message.SendMessage("administrator", "addAppointment", "Update lịch hẹn thành công !", "");
                            return new { success = true, message = "Update status" };
                        }
                        return new { success = false, message = "Đã làm xong không thể cập nhật" };
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
                    var appointment = dbConn.SingleOrDefault<CRM_Appointment>("RowID= {0}", RowID);
                    if (appointment != null)
                    {
                        appointment.Status = "DONE";
                        appointment.RowUpdatedUser = userName;
                        appointment.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(appointment);
                        Portal_Message.SendMessage("administrator", "addAppointment", "Hoàn tất lịch hẹn !", "");
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