using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class CRM_ExtsInfo_Meta
    {
        [AutoIncrement]
        [PrimaryKey]
        public int RowID { get; set; }
        public string CustomerID { get; set; }
        public string FactorID { get; set; }
        public string FactorName { get; set; }
        public string Value { get; set; }
        public string Value_default { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public string Object { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public static object save(Int32 RowID,int status, string userName)
        {
            if (RowID != 0)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try {
                            var row = dbConn.SingleOrDefault<CRM_ExtsInfo_Meta>("RowID= {0}", RowID);
                            if (status == 0)
                            {
                                row.Status = "ESS02";
                            }
                            else
                            {
                                row.Status = "ESS01";
                            }
                            row.RowUpdatedUser = userName;
                            row.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(row);
                    }
                    catch (Exception e) {
                        return new { success = false, message = e.Message };
                    }
                    
                }
            }
            else
            {
                return new { success = false, message = "Không có Etxs nào được chọn !" };
            }
            return new { success = true };
        }
        public static object update_all_value(IEnumerable<CRM_Ext_Meta_Value> listItem, string userName)
        {
            if(listItem != null)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try {
                        foreach (var item in listItem)
                        {
                            var row = dbConn.SingleOrDefault<CRM_ExtsInfo_Meta>("RowID= {0}", item.RowID);
                            if(row != null)
                            {
                                row.Value = item.Value;
                                row.RowUpdatedUser = userName;
                                row.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(row);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
            }
            else
            {
                return new { success = false, message = "Không có Etxs nào được chọn !" };
            }
            return new { success = true };
        }
    }
    public class CRM_Ext_Meta_Value
    {
        public int RowID { get; set; }
        public string Value { get; set; }
    }
}