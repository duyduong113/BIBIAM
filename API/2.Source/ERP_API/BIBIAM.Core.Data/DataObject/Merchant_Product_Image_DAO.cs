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

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_Product_Image_DAO
    {
        public DataTable ReadByProductID(string ProductID, string MerchantID, string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("{0}", ProductID));
            param.Add(new SqlParameter("{1}", MerchantID));
            return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Product_Image where ma_san_pham = {0} and ma_gian_hang = {1}", param);
        }
        public string Create(List<Merchant_Product_Image> list, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (list == null)
                        {
                            dbTrans.Rollback();
                            return "No Data";
                        }
                        db.Delete<Merchant_Product_Image>("ma_san_pham = {0} and ma_gian_hang = {1}", list.ElementAt(0).ma_san_pham, list.ElementAt(0).ma_gian_hang);
                        foreach (Merchant_Product_Image item in list)
                        {
                            item.ngay_tao = DateTime.Now;
                            item.nguoi_tao = UserName;
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
