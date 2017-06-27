using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;


namespace ERPAPD.Models
{
    public class ERPAPD_Customer_Extensions
    {
        [AutoIncrement]
        public Int64 RowID { get; set; }
        public string CustomerID { get; set; }
        public string Value { get; set; }

        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }

       
        public object Save(ERPAPD_Customer_Extensions request, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var check = dbConn.SingleOrDefault<ERPAPD_Customer_Extensions>("CustomerID= {0}", request.CustomerID);
                    if (check == null)
                    {
                        var extensions = new ERPAPD_Customer_Extensions();
                       
                        if (!string.IsNullOrEmpty(request.Value))
                        {
                            extensions.Value = request.Value.Trim();
                        }
                        extensions.CustomerID = request.CustomerID;
                        extensions.RowCreatedUser = username;
                        extensions.RowUpdatedUser = "";
                        extensions.RowCreatedAt = DateTime.Now;
                        extensions.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        dbConn.Insert(extensions);
                        return new { success = true };
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(request.Value))
                        {
                            check.Value = request.Value.Trim();
                        }
                        check.RowUpdatedUser = username;
                        check.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(check);
                        return new { success = true};
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