using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;
using ERPAPD.Helpers;

namespace ERPAPD.Models
{
    public class CRM_ExtsInfo
    {
        [AutoIncrement]
        [PrimaryKey]
        public int RowID { get; set; }
        public string Name { get; set; }
        public string IDName { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Object { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public static object Save(IEnumerable<CRM_ExtsInfo> listExts, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if(listExts != null)
                {
                    try{
                        foreach (var request in listExts)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_ExtsInfo>("RowID= {0}", request.RowID);
                            if (checkExits == null)
                            {
                                if (!string.IsNullOrEmpty(request.Name))
                                {
                                    var row = new CRM_ExtsInfo();
                                    row.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.Trim() : "";
                                    row.IDName = convertToUnSign3.Init(row.Name).Replace(" ","_");
                                    row.Value = !string.IsNullOrEmpty(request.Value) ? request.Value.Trim() : "";
                                    row.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "ESS01";
                                    row.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                                    row.Object = !string.IsNullOrEmpty(request.Object) ? request.Object.Trim() : "";
                                    row.RowCreatedUser = username;
                                    row.RowUpdatedUser = "";
                                    row.RowCreatedAt = DateTime.Now;
                                    row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                    dbConn.Insert(row);
                                    Save_Meta(row.IDName, row.Name, row.Value, row.Type, row.Status, row.Object, username);
                                }
                                else
                                {
                                    return new { success = false, message = "Name not null!" };
                                }
                                //Portal_Message.SendMessage("administrator", "addWork", "Work insert !", "");
                            }
                            else {
                                var IDName = checkExits.IDName;
                                checkExits.Value = !string.IsNullOrEmpty(request.Value) ? request.Value.Trim() : "";
                                checkExits.Status = !string.IsNullOrEmpty(request.Status) ? request.Status.Trim() : "ESS01";
                                checkExits.Type = !string.IsNullOrEmpty(request.Type) ? request.Type.Trim() : "";
                                checkExits.Object = !string.IsNullOrEmpty(request.Object) ? request.Object.Trim() : "";

                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                                Update_Meta(checkExits.IDName,checkExits.Value,checkExits.Status,checkExits.Type,checkExits.Object, username);
                            }
                        }
                        return new { success = true, message = "Save success!" };

                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
                else
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
        public static bool Save_Meta(string keyName, string name, string value,string type,string status,string obj, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string strQuery = @"INSERT INTO CRM_ExtsInfo_Meta (CustomerID, FactorID, FactorName, Value,Value_default, Type, Status,Object, RowCreatedAt, RowUpdatedAt, RowCreatedUser, RowUpdatedUser)
                                    SELECT CustomerID,'" + keyName + "',N'" + name + "','',N'"+ value +"','"+type+"','"+status+"','"+obj+"','"+ DateTime.Now + "','','"+username+"','' FROM ERPAPD_Customer";
                dbConn.ExecuteNonQuery(strQuery);
            }
            return true;
        }
        public static bool Update_Meta(string keyName,string value, string status, string type, string obj,string userName)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string strQuery = @"UPDATE CRM_ExtsInfo_Meta SET Value_default = N'"
                                    + value + "',Status = '"+status+"', Type = '"+type+"',Object = '"
                                    +obj+"',RowUpdatedUser = '"+userName+"',RowUpdatedAt = '"
                                    + DateTime.Now + "' WHERE FactorID = '"+ keyName + "'";
                dbConn.ExecuteNonQuery(strQuery);
            }
            return true;
        }


    }
}