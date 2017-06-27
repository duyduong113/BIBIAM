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
    public class Merchant_Contact_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select a.*, isnull(logo_gian_hang,'') as logo_gian_hang from Merchant_Contact a left join Merchant_Info b on a.ma_gian_hang = b.ma_gian_hang order by ma_gian_hang", param);
        }
        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var checkID = db.SingleOrDefault<Merchant_Contact>("id={0}", ids[0]);
                        foreach (var id in ids)
                        {
                            db.Delete<Merchant_Contact>(s => s.id == int.Parse(id));
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
        public string UpSert(List<Merchant_Contact> list, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Merchant_Contact row in list)
                        {
                            var checkID = db.SingleOrDefault<Merchant_Contact>("id={0}", row.id);
                            if (checkID != null)
                            {                                
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                row.ngay_tao = checkID.ngay_tao;
                                row.nguoi_cap_nhat = checkID.nguoi_tao;
                                db.Update(EmptyIfNull(row));
                            }
                            else
                            {                                                                                          
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_tao = row.nguoi_cap_nhat = UserName;
                                db.Insert(EmptyIfNull(row));
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
        private Merchant_Contact EmptyIfNull(Merchant_Contact p1)
        {
            p1.ma_gian_hang = (p1.ma_gian_hang == null) ? "" : p1.ma_gian_hang;
            p1.ten_gian_hang = (p1.ten_gian_hang == null) ? "" : p1.ten_gian_hang;
            p1.ten_nguoi_lien_he = (p1.ten_nguoi_lien_he == null) ? "" : p1.ten_nguoi_lien_he;
            p1.danh_xung = (p1.danh_xung == null) ? "" : p1.danh_xung;
            p1.email = (p1.email == null) ? "" : p1.email;
            p1.so_dien_thoai = (p1.so_dien_thoai == null) ? "" : p1.so_dien_thoai;
            p1.chuc_vu = (p1.chuc_vu == null) ? "Nhân Viên" : p1.chuc_vu;
            p1.ghi_chu = (p1.ghi_chu == null) ? "" : p1.ghi_chu;
            p1.nguoi_tao = (p1.nguoi_tao == null) ? "" : p1.nguoi_tao;
            p1.nguoi_cap_nhat = (p1.nguoi_cap_nhat == null) ? "" : p1.nguoi_cap_nhat;
            return p1;
        }
    }
}
