using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class CRM_Debt_History
    {
        [AutoIncrement]
        [PrimaryKey]
        public int IDRow { get; set; }
        public string CustomerCode { get; set; }
        public DateTime RemindDate { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Status { get; set; }
        public int StaffID { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }

        [Ignore]
        public string StrRemindDate { get; set; }
        [Ignore]
        public string StrPaymentDate { get; set; }
        [Ignore]
        public string StaffName { get; set; }

        public static object save(CRM_Debt_History request, string userName)
        {
            
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        var exits = dbConn.SingleOrDefault<CRM_Debt_History>("IDRow= {0}", request.IDRow);
                        if (exits == null)
                        {
                            var row = new CRM_Debt_History();
                            row.CustomerCode = !string.IsNullOrEmpty(request.CustomerCode) ? request.CustomerCode.Trim() : "";
                            row.Content = !string.IsNullOrEmpty(request.Content) ? request.Content.Trim() : "";
                            row.RemindDate = !string.IsNullOrEmpty(request.StrRemindDate) ? DateTime.Parse(request.StrRemindDate) : DateTime.Parse("1900-01-01");
                            row.PaymentDate = !string.IsNullOrEmpty(request.StrPaymentDate) ? DateTime.Parse(request.StrPaymentDate) : DateTime.Parse("1900-01-01");
                            row.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                            row.StaffID = (request.StaffID != 0) ? request.StaffID : 0;
                            row.Status = 0;

                            row.RowCreatedUser = userName;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                            dbConn.Insert(row);
                    }
                    else
                        {
                            exits.Content = !string.IsNullOrEmpty(request.Content) ? request.Content.Trim() : "";
                            exits.RemindDate = !string.IsNullOrEmpty(request.StrRemindDate) ? DateTime.Parse(request.StrRemindDate) : DateTime.Parse("1900-01-01");
                            exits.PaymentDate = !string.IsNullOrEmpty(request.StrPaymentDate) ? DateTime.Parse(request.StrPaymentDate) : DateTime.Parse("1900-01-01");
                            exits.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                            exits.StaffID = (request.StaffID != 0) ? request.StaffID : 0;
                            exits.RowUpdatedUser = userName;
                            exits.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(exits);
                        }
                        
                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }

                }
            return new { success = true };
        }

    }
}