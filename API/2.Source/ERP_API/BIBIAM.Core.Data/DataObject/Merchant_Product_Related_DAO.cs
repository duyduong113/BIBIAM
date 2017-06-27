using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using System.IO;
using System.Web;
using MySql.Data.MySqlClient;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_Product_Related_DAO
    {

        public List<Merchant_Product_Related> ReadAll(string connectionString, string ma_san_pham)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Product", param);
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                return db.SqlList<Merchant_Product_Related>("EXEC _p_Get_Product_Related @ma_san_pham", new { ma_san_pham = ma_san_pham });
            }
        }
        public string Delete(string ma_san_pham, string ma_san_pham_de_nghi, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@ma_san_pham", ma_san_pham));
                        param.Add(new SqlParameter("@ma_san_pham_de_nghi", ma_san_pham_de_nghi));
                        new SqlHelper().ExecuteNoneQuery("delete Merchant_Product_Related where ma_san_pham=@ma_san_pham and ma_san_pham_de_nghi=@ma_san_pham_de_nghi", param, CommandType.Text);
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
        public string UpSert(List<Entities.Merchant_Product_Related> list, string ma_san_pham_de_nghi, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Entities.Merchant_Product_Related row in list)
                        {
                            var checkID = db.SingleOrDefault<Entities.Merchant_Product_Related>("ma_san_pham={0}", row.ma_san_pham_de_nghi);
                            if (checkID == null)
                            {
                                string ma_tu_tang = String.Empty;
                                var lastId = db.FirstOrDefault<Hierarchy>("SELECT TOP 1 * FROM Hierarchy ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_phan_cap.Contains("CAP"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_phan_cap.Substring(3, 3)) + 1;
                                        ma_tu_tang = "CAP" + String.Format("{0:000}", nextNo);
                                    }
                                }
                                else
                                {
                                    ma_tu_tang = "CAP001";
                                }
                                row.ma_san_pham_de_nghi = ma_tu_tang;
                                db.Insert(row);
                            }
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
    }
}