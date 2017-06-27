using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;
using System.Linq;
using MySql.Data.MySqlClient;


namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_StockOutHeader_DAO
    {
        //public string Upsert(Merchant_StockOutHeader data, List<Merchant_StockOutDetail> list, bool trang_thai, string UserName, string connectionString)
        //{
        //    using (var db = new OrmliteConnection().openConn(connectionString))
        //    {
        //        using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
        //        {
        //            var checkID = db.SingleOrDefault<Merchant_StockOutHeader>("ma_phieu_xuat_kho={0} and ma_gian_hang = {1}", data.ma_phieu_xuat_kho, data.ma_gian_hang);
        //            try
        //            {
        //                if (checkID != null)
        //                {
        //                    if (checkID.trang_thai == AllConstant.trang_thai_duyet.DA_DUYET)
        //                    {
        //                        dbTrans.Rollback();
        //                        return "Phiếu nhập kho này đã được duyệt không được cập nhật nữa!";
        //                    }
        //                    checkID.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
        //                    checkID.nguoi_cap_nhat = UserName;
        //                    checkID.ngay_cap_nhat = DateTime.Now;
        //                    checkID.ngay_xuat_kho = data.ngay_xuat_kho;
        //                    checkID.nguoi_nhan = data.nguoi_nhan;
        //                    checkID.ma_kho = data.ma_kho;
        //                    checkID.thu_kho = data.thu_kho;
        //                    checkID.ghi_chu = data.ghi_chu;
        //                    checkID.dia_diem = data.dia_diem;
        //                    db.Update(checkID);
        //                }
        //                else
        //                {
        //                    var lastId = db.FirstOrDefault<Merchant_StockOutHeader>("SELECT TOP 1 * FROM Merchant_StockOutHeader ORDER BY id DESC");
        //                    if (lastId != null)
        //                    {
        //                        if (lastId.ma_phieu_xuat_kho.Contains("GDN"))
        //                        {
        //                            var nextNo = Int32.Parse(lastId.ma_phieu_xuat_kho.Substring(3, 10)) + 1;
        //                            data.ma_phieu_xuat_kho = "GDN" + String.Format("{0:0000000000}", nextNo);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        data.ma_phieu_xuat_kho = "GDN" + "0000000001";
        //                    }
        //                    data.ngay_tao = DateTime.Now;
        //                    data.nguoi_tao = UserName;
        //                    data.ngay_cap_nhat = DateTime.Parse("1900-01-01");
        //                    data.trang_thai = AllConstant.trang_thai_duyet.NHAP;
        //                    db.Insert(data);                            
        //                }
        //                dbTrans.Commit();
        //            }
        //            catch (Exception e)
        //            {
        //                dbTrans.Rollback();
        //                return e.Message.ToString();
        //            }
        //        }
        //        using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
        //        {
        //            try
        //            {
        //                if (list != null)
        //                {
        //                    var checkData = db.Select<Merchant_StockOutDetail>("ma_phieu_xuat_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_xuat_kho, data.ma_gian_hang);
        //                    if (checkData.Count == 0)
        //                    {
        //                        foreach (Merchant_StockOutDetail item in list)
        //                        {
        //                            item.id = 0;
        //                            item.ma_phieu_xuat_kho = data.ma_phieu_xuat_kho;
        //                            item.ma_gian_hang = data.ma_gian_hang;
        //                            item.nguoi_tao = UserName;
        //                            item.ngay_tao = DateTime.Now;
        //                            item.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.CHUA_DUYET;
        //                            db.Insert(item);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        foreach (Merchant_StockOutDetail item in list)
        //                        {
        //                            var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == item.ma_san_pham && s.vi_tri == item.vi_tri);
        //                            if (checkProDetail == null)
        //                            {
        //                                item.id = 0;
        //                                item.ma_phieu_xuat_kho = data.ma_phieu_xuat_kho;
        //                                item.ma_gian_hang = data.ma_gian_hang;
        //                                item.nguoi_tao = UserName;
        //                                item.ngay_tao = DateTime.Now;
        //                                item.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
        //                                db.Insert(item);
        //                            }
        //                            else
        //                            {
        //                                checkProDetail.ghi_chu = item.ghi_chu;
        //                                checkProDetail.so_luong_yeu_cau = item.so_luong_yeu_cau;
        //                                checkProDetail.so_luong_thuc_te = item.so_luong_thuc_te;
        //                                checkProDetail.don_vi_tinh = item.don_vi_tinh;
        //                                checkProDetail.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
        //                                db.Update(checkProDetail);
        //                                checkData.Remove(checkProDetail);
        //                            }
        //                        }
        //                        if (checkData != null)
        //                        {
        //                            foreach (Merchant_StockOutDetail item in checkData)
        //                                db.Delete<Merchant_StockOutDetail>("id = {0}", item.id);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    db.Delete<Merchant_StockOutDetail>("ma_phieu_xuat_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_xuat_kho, data.ma_gian_hang);
        //                }
        //                dbTrans.Commit();
        //                return "true@@" + data.ma_phieu_xuat_kho;
        //            }
        //            catch (Exception)
        //            {
        //                dbTrans.Rollback();
        //                return "true@@" + data.ma_phieu_xuat_kho;
        //            }
        //        }
        //    }
        //}
        //public string UpdateInventoryMySQL(string ma_phieu_xuat_kho, string ma_gian_hang, string username, string connectionString)
        //{
        //    List<Merchant_StockOutDetail> list = new List<Merchant_StockOutDetail>();
        //    using (var db = new OrmliteConnection().openConn(connectionString))
        //    {
        //        list = db.Select<Merchant_StockOutDetail>("ma_phieu_xuat_kho = {0} and ma_gian_hang = {1} and trang_thai = 'Aproved'".Params(ma_phieu_xuat_kho, ma_gian_hang));
        //    }
        //    if (list.Count > 0)
        //        using (MySqlConnection con = new MySqlConnection(AppConfigs.FEConnectionString))
        //        {
        //            con.Open();
        //            using (MySql.Data.MySqlClient.MySqlTransaction trans = con.BeginTransaction())
        //            {
        //                try
        //                {
        //                    using (MySqlCommand cmd = new MySqlCommand("", con, trans))
        //                    {
        //                        foreach (Merchant_StockOutDetail item in list)
        //                        {
        //                            cmd.CommandText = @"update Merchant_Inventory set so_luong_kho = so_luong_kho - {0}, ngay_cap_nhat = NOW(), nguoi_cap_nhat = {1} where ma_gian_hang = {2} and ma_san_pham = {3}".Params(item.so_luong_thuc_te, username, item.ma_gian_hang, item.ma_san_pham);
        //                            cmd.ExecuteNonQuery();
        //                        }
        //                    }
        //                    trans.Commit();
        //                    return "true";
        //                }
        //                catch (Exception e)
        //                {
        //                    trans.Rollback();
        //                    return e.Message.ToString();
        //                }
        //            }
        //        }
        //    return "Không tìm thấy thông tin Phiếu Xuất Kho";
        //}
    }
}