using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;

namespace BIBIAM.Core.Data.DataObject
{
    public class Customer_DAO
    {
        public DataTable ReadAll()
        {
            List<MySql.Data.MySqlClient.MySqlParameter> param = new List<MySql.Data.MySqlClient.MySqlParameter>();
            return new MySqlHelper(AppConfigs.FEConnectionString).ExecuteString("select * from Customer order by id desc", param);
        }

        public DataTable GetAddressesFromCustomer(string ma_khach_hang)
        {
            List<MySql.Data.MySqlClient.MySqlParameter> param = new List<MySql.Data.MySqlClient.MySqlParameter>();
            param.Add(new MySql.Data.MySqlClient.MySqlParameter("@ma_khach_hang", ma_khach_hang));
            return new MySqlHelper(AppConfigs.FEConnectionString).ExecuteString("select * from Customer_Address where ma_khach_hang = @ma_khach_hang order by id desc", param);
        }

        public string Delete(string ma_khach_hang,string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        db.Delete<Customer_Address>(s => s.ma_khach_hang == ma_khach_hang);                    
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
