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
    public class Merchant_Image_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Image order by id desc", param);
        }

        public string DeleteForCode(string ma_gian_hang, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        db.Delete<Merchant_Image>(s => s.ma_gian_hang == ma_gian_hang);
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
                            db.Delete<Merchant_Image>(s => s.id == int.Parse(id));
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

        public string Insert(string ma_gian_hang, string ma_thong_tin_anh, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        List<SqlParameter> lstParameter = new List<SqlParameter>();
                        lstParameter.Add(new SqlParameter("@ma_gian_hang", ma_gian_hang));
                        lstParameter.Add(new SqlParameter("@ma_thong_tin_anh", ma_thong_tin_anh));
                        lstParameter.Add(new SqlParameter("@UserName", UserName));
                        new SqlHelper().ExecuteQuery("p_Insert_Merchant_Image", lstParameter);
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

        public string UpSert(List<Merchant_Image> lstProdInfo, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Merchant_Image row in lstProdInfo)
                        {
                            var checkID = db.SingleOrDefault<Merchant_Image>("id={0}", row.id);
                            if (checkID != null)
                            {
                                if (String.IsNullOrEmpty(row.ten_anh))
                                    row.ten_anh = checkID.ten_anh;
                                else
                                {
                                    if (!String.IsNullOrEmpty(checkID.ten_anh))
                                    {
                                        var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/Merchant_Info/"), checkID.ten_anh);
                                        if (!String.IsNullOrEmpty(path))
                                        {
                                            System.IO.File.Delete(path);
                                        }
                                    }
                                }
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(EmptyIfNull(row));
                            }
                            else
                            {
                                //row.ngay_duyet = row.ngay_xuat_ban = DateTime.Now;//chua biet                                 
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                //row.nguoi_duyet = row.nguoi_tao = UserName;
                                row.nguoi_tao = UserName;
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
        private Merchant_Image EmptyIfNull(Merchant_Image p1)
        {
            p1.ma_gian_hang = (p1.ma_gian_hang == null) ? "" : p1.ma_gian_hang;
            p1.ma_thong_tin_anh = (p1.ma_thong_tin_anh == null) ? "" : p1.ma_thong_tin_anh;
            p1.ten_anh = (p1.ten_anh == null) ? "" : p1.ten_anh;
            //p1.mo_ta = (p1.mo_ta == null) ? "" : p1.mo_ta;
            //p1.nguoi_duyet = (p1.nguoi_duyet == null) ? "" : p1.nguoi_duyet;
            //p1.nguoi_xuat_ban = (p1.nguoi_xuat_ban == null) ? "" : p1.nguoi_xuat_ban;
            //p1.trang_thai_duyet = (p1.trang_thai_duyet == null) ? "" : p1.trang_thai_duyet;
            //p1.trang_thai_xuat_ban = (p1.trang_thai_xuat_ban == null) ? "" : p1.trang_thai_xuat_ban;
            p1.trang_thai = (p1.trang_thai == null) ? "" : p1.trang_thai;
            return p1;
        }
    }
}
