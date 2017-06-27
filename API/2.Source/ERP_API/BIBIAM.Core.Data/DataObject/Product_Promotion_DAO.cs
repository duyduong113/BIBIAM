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
using BIBIAM.Core.Data.DataObject;

namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Promotion_DAO
    {

        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Product_Promotion order by id desc", param);
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
                            var checkID = db.FirstOrDefault<Product_Promotion>(s => s.id == int.Parse(id));
                            db.Delete<Product_Promotion>(s=>s.id==int.Parse(id));
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
        public string UpSert(List<Product_Promotion> list, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Product_Promotion row in list)
                        {
                            var checkID = db.SingleOrDefault<Product_Promotion>("id={0}", row.id);
                            if (checkID != null)
                            {
                                row.ngay_tao = checkID.ngay_tao;
                                row.ngay_duyet = checkID.ngay_duyet;
                                row.ngay_xuat_ban = checkID.ngay_xuat_ban;

                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;

                                if (checkID.trang_thai_duyet != row.trang_thai_duyet && (row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.DA_DUYET || row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.TU_CHOI))
                                {
                                    row.ngay_duyet = DateTime.Now;
                                    row.nguoi_duyet = UserName;
                                }

                                if (checkID.trang_thai_xuat_ban != row.trang_thai_xuat_ban && (row.trang_thai_xuat_ban == BIBIAM.Core.AllConstant.trang_thai_xuat_ban.DA_XUAT_BAN))
                                {
                                    row.ngay_xuat_ban = DateTime.Now;
                                    row.nguoi_xuat_ban = UserName;
                                }

                                db.Update(EmptyIfNull(row));
                            }
                            else
                            {
                                if(string.IsNullOrEmpty(row.ma_km) && string.IsNullOrEmpty(row.ma_gia_san_pham))
                                {
                                    dbTrans.Rollback();
                                    return "Chưa nhập đủ mã khuyến mãi & mã giá sản phẩm";
                                }
                                else
                                {
                                    string ProductPromotionID = String.Empty;
                                    var lastId = db.FirstOrDefault<Product_Promotion>("SELECT TOP 1 * FROM Product_Promotion ORDER BY id DESC");
                                    if (lastId != null)
                                    {
                                        if (lastId.ma_km.Contains("GKM"))
                                        {
                                            var nextNo = Int32.Parse(lastId.ma_gia_san_pham.Substring(3, 10)) + 1;
                                            ProductPromotionID = "GKM" + String.Format("{0:0000000000}", nextNo);
                                        }
                                    }
                                    else
                                    {
                                        ProductPromotionID = "GKM0000000001";
                                    }
                                    row.ma_km = ProductPromotionID;
                                    row.ngay_tao = DateTime.Now;
                                    row.nguoi_tao = UserName;
                                    db.Insert(EmptyIfNull(row));
                                }
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

        private Product_Promotion EmptyIfNull(Product_Promotion p1)
        {
            p1.ma_gia_san_pham = (p1.ma_gia_san_pham == null) ? "" : p1.ma_gia_san_pham;
            p1.loai_km = string.IsNullOrEmpty(p1.loai_km.ToString()) ? 0 : p1.loai_km;
            p1.tien_km = string.IsNullOrEmpty(p1.tien_km.ToString()) ? 0 : p1.tien_km;
            p1.phan_tram_km = string.IsNullOrEmpty(p1.phan_tram_km.ToString()) ? 0 : p1.phan_tram_km;
            p1.gia_sau_km = string.IsNullOrEmpty(p1.gia_sau_km.ToString()) ? 0 : p1.gia_sau_km;
            p1.so_ngay_km = string.IsNullOrEmpty(p1.so_ngay_km.ToString()) ? 0 : p1.so_ngay_km;
            p1.ngay_bat_dau = string.IsNullOrEmpty(p1.ngay_bat_dau.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_bat_dau;
            p1.ngay_ket_thuc = string.IsNullOrEmpty(p1.ngay_ket_thuc.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_ket_thuc;
            p1.nguoi_duyet = (p1.nguoi_duyet == null) ? "" : p1.nguoi_duyet;
            p1.ngay_duyet = string.IsNullOrEmpty(p1.ngay_duyet.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_duyet;
            p1.nguoi_xuat_ban = (p1.nguoi_xuat_ban == null) ? "" : p1.nguoi_xuat_ban;
            p1.ngay_xuat_ban = string.IsNullOrEmpty(p1.ngay_xuat_ban.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_xuat_ban;
            p1.trang_thai_duyet = (p1.trang_thai_duyet == null) ? "" : p1.trang_thai_duyet;
            p1.trang_thai_xuat_ban = (p1.trang_thai_xuat_ban == null) ? "" : p1.trang_thai_xuat_ban;
            p1.trang_thai = (p1.trang_thai == null) ? "" : p1.trang_thai;
            return p1;
        }
    }
}
