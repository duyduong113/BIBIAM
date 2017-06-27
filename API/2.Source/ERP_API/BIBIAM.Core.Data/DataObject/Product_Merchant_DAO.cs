using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;

namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Merchant_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Product_Merchant order by id desc", param);
        }
        public int Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var id in ids)
                        {
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@id", id));
                            new SqlHelper().ExecuteNoneQuery("delete Product_Merchant where id = @id", param, CommandType.Text);
                        }
                        dbTrans.Commit();
                        return 1;
                    }
                    catch
                    {
                        dbTrans.Rollback();
                        return 0;
                    }
                }
            }
        }
        public void UpSert(List<Product_Merchant> lstProduct_Merchant, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {

                        foreach (Product_Merchant row in lstProduct_Merchant)
                        {
                            var checkID = db.SingleOrDefault<Product_Merchant>("id={0}", row.id);
                            if (checkID != null)
                            {
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                if (checkID.trang_thai_duyet != row.trang_thai_duyet && (row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.DA_DUYET || row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.TU_CHOI))
                                {
                                    row.ngay_duyet = DateTime.Now;
                                    row.nguoi_duyet = UserName;
                                }
                                db.Update(row);
                            }
                            else
                            {
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_tao = row.nguoi_cap_nhat = UserName;
                                db.Save(row);
                            }
                        }
                        dbTrans.Commit();
                    }
                    catch
                    {
                        dbTrans.Rollback();
                    }
                }
            }
        }
    }
}