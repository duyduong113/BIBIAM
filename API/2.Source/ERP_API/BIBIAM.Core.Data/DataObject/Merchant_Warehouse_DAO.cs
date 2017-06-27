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
    public class Merchant_Warehouse_DAO
    {
        public string CreateUpdate(string ma_kho, string ma_gian_hang,string username, string connectionString = "")
        {
            using (var db = connectionString != "" ? new OrmliteConnection().openConn(connectionString) : new OrmliteConnection().openConn())
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var exist = db.FirstOrDefault<Merchant_Warehouse>("ma_kho = {0}", ma_kho);
                        Merchant_Warehouse warehouse = new Merchant_Warehouse();
                        warehouse.ngay_tao = DateTime.Now;
                        warehouse.nguoi_tao = username;
                        warehouse.ma_kho = ma_kho;
                        warehouse.ma_gian_hang = ma_gian_hang;
                       
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
        public string ChangeStatus(string[] ids, string connectionString, string Username, string status)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {

                    foreach (var id in ids)
                    {
                        var checkID = db.SingleOrDefault<Merchant_Warehouse>("id={0}", id);
                        if (checkID != null)
                        {
                            checkID.trang_thai = (status == AllConstant.trang_thai.DANG_SU_DUNG) ? AllConstant.trang_thai.DANG_SU_DUNG : AllConstant.trang_thai.KHONG_SU_DUNG;
                            checkID.nguoi_cap_nhat = Username;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.UpdateOnly(checkID,
                                            onlyFields: p =>
                                            new
                                            {
                                                p.trang_thai,
                                                p.nguoi_cap_nhat,
                                                p.ngay_cap_nhat
                                            },
                                             where: p => p.id == checkID.id);
                           
                        }
                    }
                    return "true";
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }
        }

        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                    foreach (var id in ids)
                    {
                        db.Delete<Merchant_Warehouse>(s => s.ma_kho == id);
                    }
                    return "true";
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }
        }
    }
}
