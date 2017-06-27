using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;


namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Price_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Product_Price order by id desc", param);
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
                            db.Delete<Product_Price>(s => s.id == int.Parse(id));
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
        public string UpSert(List<Product_Price> lstProductPrice, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Product_Price row in lstProductPrice)
                        {
                            var checkId = db.SingleOrDefault<Product_Price>("id={0}", row.id);
                            if (checkId != null)
                            {
                                row.ngay_tao = checkId.ngay_tao;
                                row.ngay_duyet = checkId.ngay_duyet;
                                row.ngay_xuat_ban = checkId.ngay_xuat_ban;

                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;

                                if (checkId.trang_thai_duyet != row.trang_thai_duyet && (row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.DA_DUYET || row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.TU_CHOI))
                                {
                                    row.ngay_duyet = DateTime.Now;
                                    row.nguoi_duyet = UserName;
                                }

                                if (checkId.trang_thai_xuat_ban != row.trang_thai_xuat_ban && (row.trang_thai_xuat_ban == BIBIAM.Core.AllConstant.trang_thai_xuat_ban.DA_XUAT_BAN))
                                {
                                    row.ngay_xuat_ban = DateTime.Now;
                                    row.nguoi_xuat_ban = UserName;
                                }
                                db.Update(EmptyIfNull(row));
                            }
                            else
                            {
                                string ProductPriceID = String.Empty;
                                var lastId = db.FirstOrDefault<Product_Price>("SELECT TOP 1 * FROM Product_Price ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_gia_san_pham.Contains("GIA"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_gia_san_pham.Substring(3, 10)) + 1;
                                        ProductPriceID = "GIA" + String.Format("{0:0000000000}", nextNo);
                                    }
                                }
                                else
                                {
                                    ProductPriceID = "GIA0000000001";
                                }
                                row.ma_gia_san_pham = ProductPriceID;
                                //row.ngay_duyet = row.ngay_xuat_ban = DateTime.Now;//chua biet
                                row.ngay_tao = DateTime.Now;
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


        private Product_Price EmptyIfNull(Product_Price p1)
        {
            p1.ma_san_pham = (p1.ma_san_pham == null) ? "" : p1.ma_san_pham;
            p1.gia_mua = string.IsNullOrEmpty(p1.gia_mua.ToString()) ? 0 : p1.gia_mua;
            p1.gia_ban_le = string.IsNullOrEmpty(p1.gia_ban_le.ToString()) ? 0 : p1.gia_ban_le;
            p1.gia_ban_si = string.IsNullOrEmpty(p1.gia_ban_si.ToString()) ? 0 : p1.gia_ban_si;
            p1.gia_khuyen_mai = string.IsNullOrEmpty(p1.gia_khuyen_mai.ToString()) ? 0 : p1.gia_khuyen_mai;
            p1.gia_luu_kho = string.IsNullOrEmpty(p1.gia_luu_kho.ToString()) ? 0 : p1.gia_luu_kho;
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