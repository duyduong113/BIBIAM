using System;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;
using System.ComponentModel;
using ERPAPD.Helpers;

namespace ERPAPD.Models
{
    public class CRM_Contract_Extra
    {
        [AutoIncrement]
        public int RowID { get; set; }
        public string NameID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
       
        public DateTime RowCreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public static object Save(CRM_Contract_Extra item, string userName)
        {
            if(item != null)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    try
                    {
                        var exits = dbConn.SingleOrDefault<CRM_Contract_Extra>("RowID= {0}", item.RowID);
                        if (exits == null)
                        {
                            var row = new CRM_Contract_Extra();
                            row.Title = !string.IsNullOrEmpty(item.Title) ? item.Title.Trim().ToUpper() : "";
                            row.Type = !string.IsNullOrEmpty(item.Type) ? item.Type.Trim() : "";
                            row.Description = !string.IsNullOrEmpty(item.Description) ? item.Description.Trim() : "";
                            row.Content = !string.IsNullOrEmpty(item.Content) ? item.Content.Trim() : "";
                            row.Status = !string.IsNullOrEmpty(item.Status) ? item.Status.Trim() : "";
                            row.NameID = row.Type + "_" + convertToUnSign3.Init(item.Title).Replace(" ", "_").ToUpper();

                            row.RowCreatedUser = userName;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                            dbConn.Insert(row);
                            return new { success = true, message = "Insert" };
                        }
                        else {
                            exits.Title = !string.IsNullOrEmpty(item.Title) ? item.Title.Trim().ToUpper() : "";
                            exits.Type = !string.IsNullOrEmpty(item.Type) ? item.Type.Trim() : "";
                            exits.Description = !string.IsNullOrEmpty(item.Description) ? item.Description.Trim() : "";
                            exits.Content = !string.IsNullOrEmpty(item.Content) ? item.Content.Trim() : "";
                            exits.Status = !string.IsNullOrEmpty(item.Status) ? item.Status.Trim() : "";
                            exits.NameID = exits.Type + "_" + convertToUnSign3.Init(item.Title).Replace(" ", "_").ToUpper();

                            exits.RowUpdatedUser = userName;
                            exits.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(exits);
                            return new { success = true, message = "Update" };
                        }
                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
            }
            return new { success = false, message = "không có data" };
        }
    }
}