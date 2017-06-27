using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class CRM_Label
    {
        [AutoIncrement]
        public Int64 RowID { get; set; }
        public Int64 LabelID { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public object Save(CRM_Label item, string username)
        {

            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var exits = dbConn.SingleOrDefault<CRM_Label>("RowID= {0}", item.RowID);
                    if (exits == null)
                    {
                        var label = new CRM_Label();

                        label.Value = !string.IsNullOrEmpty(item.Value) ? item.Value.Trim() : "";
                        label.Name = !string.IsNullOrEmpty(item.Name) ? item.Name.Trim() : "";
                        // label.LabelID = 0;
                        label.RowCreatedUser = username;
                        label.RowUpdatedUser = "";
                        label.LabelID = 0;
                        label.RowCreatedAt = DateTime.Now;
                        label.RowUpdatedAt = DateTime.Parse("1900-01-01");

                        dbConn.Insert(label);
                    }
                    else
                    {
                        exits.Value = !string.IsNullOrEmpty(item.Value) ? item.Value.Trim() : "";
                        exits.Name = !string.IsNullOrEmpty(item.Name) ? item.Name.Trim() : "";
                        exits.RowUpdatedUser = username;
                        exits.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(exits);
                    }

                    return new { success = true };

                }
                catch (Exception e)
                {
                    return new { success = false, message = e.Message };
                }
            }
        }

    }
}