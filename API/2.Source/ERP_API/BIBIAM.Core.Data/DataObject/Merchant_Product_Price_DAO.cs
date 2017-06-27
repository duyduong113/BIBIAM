using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_Product_Price_DAO
    {
        public DataTable ReadAll()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper().ExecuteString("select * from Merchant_Product_Price order by id desc", param);
        }
        public int Delete(string[] ids)
        {
            using (var db = new OrmliteConnection().openConn())
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var id in ids)
                        {
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@id", id));
                            new SqlHelper().ExecuteNoneQuery("delete Merchant_Product_Price where id = @id", param, CommandType.Text);
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
        public void UpSert(List<Merchant_Product_Price> lstMerchant_Product_Price, string UserName, string Type)
        {
            using (var db = new OrmliteConnection().openConn())
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {

                        foreach (Merchant_Product_Price row in lstMerchant_Product_Price)
                        {
                            if (Type == "Insert")
                            {
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_tao = row.nguoi_cap_nhat = UserName;
                                db.Save(row);
                            }
                            else
                            {
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(row);
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
