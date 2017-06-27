using System;
using System.Linq;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class Users_Permissions
    {
        public Int64 UserID { get; set; }
        public Int64 RowID { get; set; }
        public string CustomerInfo { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }

        public object Save(string request, Users currentUser)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var check = dbConn.SingleOrDefault<Users_Permissions>("UserID= {0}", currentUser.Id);
                    if (check == null)
                    {
                        var columns = new Users_Permissions();

                        if (!string.IsNullOrEmpty(request))
                        {
                            columns.CustomerInfo = request;
                        }

                        columns.RowCreatedUser = currentUser.FullName;
                        columns.RowUpdatedUser = "";
                        columns.RowCreatedAt = DateTime.Now;
                        columns.RowUpdatedAt = DateTime.Parse("1900-01-01");

                        dbConn.Insert(columns);
                        return new { success = true };
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(request))
                        {
                            check.CustomerInfo = request;
                        }
                        check.RowUpdatedUser = currentUser.FullName;
                        check.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(check);
                        return new { success = true };

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