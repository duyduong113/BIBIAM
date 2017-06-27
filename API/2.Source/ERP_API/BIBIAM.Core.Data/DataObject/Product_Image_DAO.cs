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
    public class Product_Image_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Product_Image order by id desc", param);
        }

        public string DeleteForCode(string ma_san_pham, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        db.Delete<Product_Image>(s => s.ma_san_pham == ma_san_pham);
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
                            db.Delete<Product_Image>(s => s.id == int.Parse(id));
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

        public string Insert(string ma_san_pham, string ma_thong_tin_anh, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        List<SqlParameter> lstParameter = new List<SqlParameter>();
                        lstParameter.Add(new SqlParameter("@ma_san_pham", ma_san_pham));
                        lstParameter.Add(new SqlParameter("@ma_thong_tin_anh", ma_thong_tin_anh));
                        lstParameter.Add(new SqlParameter("@UserName", UserName));
                        new SqlHelper().ExecuteQuery("p_Insert_Product_Image", lstParameter);
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

        public string UpSert(List<Product_Image> lstProdInfo, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Product_Image row in lstProdInfo)
                        {
                            var checkID = db.SingleOrDefault<Product_Image>("id={0}", row.id);
                            if (checkID != null)
                            {
                                if (String.IsNullOrEmpty(row.ten_anh))
                                    row.ten_anh = checkID.ten_anh;
                                else
                                {
                                    if (!String.IsNullOrEmpty(checkID.ten_anh))
                                    {
                                        var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/Product_Info/"), checkID.ten_anh);
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
        private Product_Image EmptyIfNull(Product_Image p1)
        {
            p1.ma_san_pham = (p1.ma_san_pham == null) ? "" : p1.ma_san_pham;
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
