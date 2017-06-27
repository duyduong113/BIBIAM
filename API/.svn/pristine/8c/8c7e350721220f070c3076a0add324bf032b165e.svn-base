using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;
using System.Collections;

namespace BIBIAM.Core.Data.DataObject
{
    public class Property_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            return new SqlHelper(connectionString).SelectQuery("select * from Property");
        }
        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var id in ids)
                        {
                            db.DeleteById<Property>(id);
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch
                    {
                        dbTrans.Rollback();
                        return "false";
                    }
                }
            }
        }
        public string UpSert(List<Property> lstProperty, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Property row in lstProperty)
                        {
                            if (Type == "Insert")
                            {
                                string ma_thong_so = String.Empty;
                                var lastId = db.FirstOrDefault<Property>("SELECT TOP 1 * FROM Property ORDER BY ma_thong_so DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_thong_so.Contains("MTS"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_thong_so.Substring(3, 5)) + 1;
                                        ma_thong_so = "MTS" + String.Format("{0:00000}", nextNo);
                                    }
                                }
                                else
                                {
                                    ma_thong_so = "MTS" + "00001";
                                }
                                row.ma_thong_so = ma_thong_so;
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_tao = row.nguoi_cap_nhat = UserName;
                                db.Insert(row);
                            }
                            else
                            {
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(row);
                            }
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch(Exception ex)
                    {
                        dbTrans.Rollback();
                        return ex.Message;
                    }
                }
            }
        }
    }
}
