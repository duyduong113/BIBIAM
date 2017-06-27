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
using BIBIAM.Core.Data;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_Voucher_DAO
    {
        //public string ChangeStatus(string[] ids, string connectionString, string Username, string status)
        //{
        //    using (var db = new OrmliteConnection().openConn(connectionString))
        //    {
        //        try
        //        {

        //            foreach (var id in ids)
        //            {
        //                var checkID = db.SingleOrDefault<Merchant_Voucher>("id={0}", id);
        //                if (checkID != null)
        //                {
        //                    checkID.trang_thai = (status == AllConstant.trang_thai.DANG_SU_DUNG) ? AllConstant.trang_thai.DANG_SU_DUNG : AllConstant.trang_thai.KHONG_SU_DUNG;
        //                    checkID.nguoi_cap_nhat = Username;
        //                    checkID.ngay_cap_nhat = DateTime.Now;
        //                    db.UpdateOnly(checkID,
        //                                    onlyFields: p =>
        //                                    new
        //                                    {
        //                                        p.trang_thai,
        //                                        p.nguoi_cap_nhat,
        //                                        p.ngay_cap_nhat
        //                                    },
        //                                     where: p => p.id == checkID.id);
        //                }
        //            }
        //            return "true";
        //        }
        //        catch (Exception e)
        //        {
        //            return e.Message.ToString();
        //        }
        //    }
        //}
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
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@id", id));
                            new SqlHelper().ExecuteNoneQuery("delete Merchant_Voucher where id = @id", param, CommandType.Text);
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

        public string UpsertFull(Merchant_Voucher voucher, List<Merchant_Voucher_Detail> products, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                Merchant_Voucher checkID;
                checkID = db.SingleOrDefault<Merchant_Voucher>("id={0} and ma_gian_hang = {1}", voucher.id, voucher.ma_gian_hang);
                //Transaction for voucher
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (checkID != null)
                        {
                            voucher.ma_khuyen_mai = checkID.ma_khuyen_mai;
                            checkID.ten_khuyen_mai = voucher.ten_khuyen_mai;
                            checkID.gia_tri = voucher.gia_tri;
                            checkID.loai_khuyen_mai = voucher.loai_khuyen_mai;
                            checkID.dieu_kien = voucher.dieu_kien;
                            checkID.mieu_ta = voucher.mieu_ta;
                            checkID.gia_ban = voucher.gia_ban;
                            checkID.ngay_bat_dau = voucher.ngay_bat_dau;
                            checkID.ngay_ket_thuc = voucher.ngay_ket_thuc;
                            checkID.so_luong = voucher.so_luong;
                            checkID.su_dung = voucher.su_dung;
                            checkID.url = voucher.url;
                            checkID.ma_anh_goc = voucher.ma_anh_goc;
                            checkID.ngay_tao = voucher.ngay_tao;
                            checkID.nguoi_tao = voucher.nguoi_tao;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.trang_thai = voucher.trang_thai;
                            db.Update(checkID);
                        }
                        else
                        {
                            var lastId = db.FirstOrDefault<Merchant_Voucher>("SELECT TOP 1 * FROM Merchant_Voucher ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_khuyen_mai.Contains("VC"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_khuyen_mai.Substring(2, 7)) + 1;
                                    voucher.ma_khuyen_mai = "VC" + String.Format("{0:0000000}", nextNo);
                                }
                            }
                            else
                            {
                                voucher.ma_khuyen_mai = "VC" + "0000001";
                            }
                            voucher.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                            voucher.ngay_tao = DateTime.Now;
                            voucher.nguoi_tao = UserName;
                            voucher.ngay_cap_nhat = DateTime.Parse("1900-1-1");
                            db.Insert(voucher);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
                //Transaction for voucher Details
                //using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                //{
                //    try
                //    {
                //        if (products != null)
                //        {
                //            var checkData = db.Select<Merchant_Product_Promotion_Detail>("ma_khuyen_mai = {0} and ma_gian_hang = {1}", voucher.ma_khuyen_mai, voucher.ma_gian_hang);
                //            if (checkData == null)
                //            {
                //                foreach (string ma_san_pham in products)
                //                {
                //                    Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                //                    item.id = 0;
                //                    item.ma_san_pham = ma_san_pham;
                //                    item.ma_gian_hang = voucher.ma_gian_hang;
                //                    item.ma_chuong_trinh_km = voucher.ma_khuyen_mai;
                //                    item.nguoi_tao = UserName;
                //                    item.ngay_tao = DateTime.Now;
                //                    db.Insert(item);
                //                }
                //            }
                //            else
                //            {

                //                foreach (string ma_san_pham in products)
                //                {
                //                    var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == ma_san_pham);
                //                    if (checkProDetail == null)
                //                    {
                //                        Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                //                        item.id = 0;
                //                        item.ma_san_pham = ma_san_pham;
                //                        item.ma_gian_hang = voucher.ma_gian_hang;
                //                        item.ma_chuong_trinh_km = voucher.ma_khuyen_mai;
                //                        item.nguoi_tao = UserName;
                //                        item.ngay_tao = DateTime.Now;
                //                        db.Insert(item);
                //                    }
                //                    else
                //                    {
                //                        checkData.Remove(checkProDetail);
                //                    }
                //                }
                //                if (checkData != null)
                //                {
                //                    foreach (Merchant_Product_Promotion_Detail item in checkData)
                //                        db.Delete<Merchant_Product_Promotion_Detail>("ma_khuyen_mai = {0} and ma_gian_hang = {1} and ma_san_pham = {2}", item.ma_chuong_trinh_km, item.ma_gian_hang, item.ma_san_pham);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            db.Delete<Merchant_Product_Promotion_Detail>("ma_khuyen_mai = {0} and ma_gian_hang = {1}", voucher.ma_khuyen_mai, voucher.ma_gian_hang);
                //        }
                //        dbTrans.Commit();
                //    }
                //    catch (Exception e)
                //    {
                //        dbTrans.Rollback();
                //        return e.Message.ToString();
                //    }
                //}
            }
            return "true" + voucher.ma_khuyen_mai;
        }
    }

}
