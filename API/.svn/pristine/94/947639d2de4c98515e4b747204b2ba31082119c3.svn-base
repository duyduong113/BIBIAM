using BIBIAM.Core.Data.Providers;
using BIBIAM.Core.Entities;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_Product_Warehouse_DAO
    {
        public string CreateUpdate(string ma_san_pham, string ma_gian_hang,string username, string connectionString = "")
        {
            using (var db = connectionString != "" ? new OrmliteConnection().openConn(connectionString) : new OrmliteConnection().openConn())
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var exist = db.FirstOrDefault<Merchant_Product_Warehouse>("ma_san_pham = {0}", ma_san_pham);
                        Merchant_Product_Warehouse warehouse = new Merchant_Product_Warehouse();
                        warehouse.ngay_tao = DateTime.Now;
                        warehouse.nguoi_tao = username;
                        warehouse.ma_san_pham = ma_san_pham;
                        warehouse.ma_gian_hang = ma_gian_hang;
                        warehouse.stock_available = warehouse.stock_onhand = 0;
                        if (exist==null)
                        {
                            db.Insert<Merchant_Product_Warehouse>(warehouse);
                        }
                        else
                        {
                            db.Update<Merchant_Product_Warehouse>(warehouse);
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
