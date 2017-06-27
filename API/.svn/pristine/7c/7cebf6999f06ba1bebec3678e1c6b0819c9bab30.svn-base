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
    public class Merchant_Product_Promotion_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Product_Promotion", param);
        }
        public List<Merchant_Product_Promotion> ReadByMerchantID(string connectionString, string MerchantID)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                if (MerchantID == "All")
                    return db.Select<Merchant_Product_Promotion>();
                return db.Select<Merchant_Product_Promotion>(s => s.ma_gian_hang == MerchantID);
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
                        var checkID = db.SingleOrDefault<Merchant_Product>("id={0}", ids[0]);
                        foreach (var id in ids)
                        {
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@id", id));
                            new SqlHelper().ExecuteNoneQuery("delete Merchant_Product_Promotion where id = @id", param, CommandType.Text);
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
        public string Upsert(List<Merchant_Product_Promotion> list, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Merchant_Product_Promotion item in list)
                        {
                            var checkID = db.SingleOrDefault<Merchant_Product_Promotion>("id={0}", item.id);
                            if (checkID != null)
                            {
                                checkID.nguoi_cap_nhat = UserName;
                                checkID.ngay_cap_nhat = DateTime.Now;
                                checkID.ten_chuong_trinh_km = item.ten_chuong_trinh_km;
                                checkID.loai = item.loai;
                                checkID.gia_tri = item.gia_tri;
                                db.Update(checkID);
                            }
                            else
                            {
                                string PromotionID = String.Empty;
                                var lastId = db.FirstOrDefault<Merchant_Product_Promotion>("SELECT TOP 1 * FROM Merchant_Product_Promotion ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_chuong_trinh_km.Contains("KM"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_chuong_trinh_km.Substring(2, 7)) + 1;
                                        PromotionID = "KM" + String.Format("{0:0000000}", nextNo);
                                    }
                                }
                                else
                                {
                                    PromotionID = "KM" + "0000001";
                                }
                                item.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                                item.ma_chuong_trinh_km = PromotionID;
                                item.ngay_tao = DateTime.Now;
                                item.nguoi_tao = UserName;
                                item.ngay_cap_nhat = DateTime.Parse("1900-1-1");
                                db.Insert(item);
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
        public string UpsertFull(Merchant_Product_Promotion promotion, List<string> products, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                Merchant_Product_Promotion checkID;
                checkID = db.SingleOrDefault<Merchant_Product_Promotion>("id={0} and ma_gian_hang = {1}", promotion.id, promotion.ma_gian_hang);
                //Transaction for Promotion
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (checkID != null)
                        {
                            promotion.ma_chuong_trinh_km = checkID.ma_chuong_trinh_km;
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            checkID.ten_chuong_trinh_km = promotion.ten_chuong_trinh_km;
                            checkID.loai = promotion.loai;
                            checkID.gia_tri = promotion.gia_tri;
                            checkID.trang_thai = promotion.trang_thai;
                            checkID.ngay_bat_dau = promotion.ngay_bat_dau;
                            checkID.ngay_ket_thuc = promotion.ngay_ket_thuc;
                            db.Update(checkID);
                        }
                        else
                        {
                            var lastId = db.FirstOrDefault<Merchant_Product_Promotion>("SELECT TOP 1 * FROM Merchant_Product_Promotion ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_chuong_trinh_km.Contains("KM"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_chuong_trinh_km.Substring(2, 7)) + 1;
                                    promotion.ma_chuong_trinh_km = "KM" + String.Format("{0:0000000}", nextNo);
                                }
                            }
                            else
                            {
                                promotion.ma_chuong_trinh_km = "KM" + "0000001";
                            }
                            promotion.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                            promotion.ngay_tao = DateTime.Now;
                            promotion.nguoi_tao = UserName;
                            promotion.ngay_cap_nhat = DateTime.Parse("1900-1-1");
                            db.Insert(promotion);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
                //Transaction for Promotion Details
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (products != null)
                        {
                            var checkData = db.Select<Merchant_Product_Promotion_Detail>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1}", promotion.ma_chuong_trinh_km, promotion.ma_gian_hang);
                            if (checkData == null)
                            {
                                foreach (string ma_san_pham in products)
                                {
                                    Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                                    item.id = 0;
                                    item.ma_san_pham = ma_san_pham;
                                    item.ma_gian_hang = promotion.ma_gian_hang;
                                    item.ma_chuong_trinh_km = promotion.ma_chuong_trinh_km;
                                    item.nguoi_tao = UserName;
                                    item.ngay_tao = DateTime.Now;
                                    db.Insert(item);
                                }
                            }
                            else
                            {

                                foreach (string ma_san_pham in products)
                                {
                                    var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == ma_san_pham);
                                    if (checkProDetail == null)
                                    {
                                        Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                                        item.id = 0;
                                        item.ma_san_pham = ma_san_pham;
                                        item.ma_gian_hang = promotion.ma_gian_hang;
                                        item.ma_chuong_trinh_km = promotion.ma_chuong_trinh_km;
                                        item.nguoi_tao = UserName;
                                        item.ngay_tao = DateTime.Now;
                                        db.Insert(item);
                                    }
                                    else
                                    {
                                        checkData.Remove(checkProDetail);
                                    }
                                }
                                if (checkData != null)
                                {
                                    foreach (Merchant_Product_Promotion_Detail item in checkData)
                                        db.Delete<Merchant_Product_Promotion_Detail>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1} and ma_san_pham = {2}", item.ma_chuong_trinh_km, item.ma_gian_hang, item.ma_san_pham);
                                }
                            }
                        }
                        else
                        {
                            db.Delete<Merchant_Product_Promotion_Detail>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1}", promotion.ma_chuong_trinh_km, promotion.ma_gian_hang);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
            return "true" + promotion.ma_chuong_trinh_km;
        }
        /*
        public string UpsertFullOld(Merchant_Product_Promotion promotion, List<string> products, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var checkID = db.SingleOrDefault<Merchant_Product_Promotion>("id={0} and ma_gian_hang = {1}", promotion.id, promotion.ma_gian_hang);
                        if (checkID != null)
                        {
                            promotion.ma_chuong_trinh_km = checkID.ma_chuong_trinh_km;
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            checkID.ten_chuong_trinh_km = promotion.ten_chuong_trinh_km;
                            checkID.loai = promotion.loai;
                            checkID.gia_tri = promotion.gia_tri;
                            checkID.ngay_bat_dau = promotion.ngay_bat_dau;
                            checkID.ngay_ket_thuc = promotion.ngay_ket_thuc;
                            db.Update(checkID);
                        }
                        else
                        {
                            string PromotionID = String.Empty;
                            var lastId = db.FirstOrDefault<Merchant_Product_Promotion>("SELECT TOP 1 * FROM Merchant_Product_Promotion ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_chuong_trinh_km.Contains("KM"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_chuong_trinh_km.Substring(2, 7)) + 1;
                                    promotion.ma_chuong_trinh_km = "KM" + String.Format("{0:0000000}", nextNo);
                                }
                            }
                            else
                            {
                                promotion.ma_chuong_trinh_km = "KM" + "0000001";
                            }
                            promotion.trang_thai = "Active";
                            promotion.ngay_tao = DateTime.Now;
                            promotion.nguoi_tao = UserName;
                            promotion.ngay_cap_nhat = DateTime.Parse("1900-1-1");
                            db.Insert(promotion);
                        }
                        if (products != null)
                        {
                            //db.Delete<Merchant_Product_Promotion_Detail>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1}", checkID.ma_chuong_trinh_km, checkID.ma_gian_hang);
                            //foreach (string ma_san_pham in products)
                            //{
                            //    Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                            //    item.id = 0;
                            //    item.ma_san_pham = ma_san_pham;                                
                            //    item.ma_gian_hang = promotion.ma_gian_hang;
                            //    item.ma_chuong_trinh_km = promotion.ma_chuong_trinh_km;
                            //    item.nguoi_tao = UserName;
                            //    item.ngay_tao = DateTime.Now;
                            //    db.Insert(item);
                            //}

                            var checkData = db.Select<Merchant_Product_Promotion_Detail>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1}", promotion.ma_chuong_trinh_km, promotion.ma_gian_hang);
                            if (checkData == null)
                            {
                                foreach (string ma_san_pham in products)
                                {
                                    Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                                    item.id = 0;
                                    item.ma_san_pham = ma_san_pham;
                                    item.ma_gian_hang = promotion.ma_gian_hang;
                                    item.ma_chuong_trinh_km = promotion.ma_chuong_trinh_km;
                                    item.nguoi_tao = UserName;
                                    item.ngay_tao = DateTime.Now;
                                    db.Insert(item);
                                }
                            }
                            else
                            {
                                foreach (string ma_san_pham in products)
                                {
                                    var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == ma_san_pham);
                                    if (checkProDetail == null)
                                    {
                                        Merchant_Product_Promotion_Detail item = new Merchant_Product_Promotion_Detail();
                                        item.id = 0;
                                        item.ma_san_pham = ma_san_pham;
                                        item.ma_gian_hang = promotion.ma_gian_hang;
                                        item.ma_chuong_trinh_km = promotion.ma_chuong_trinh_km;
                                        item.nguoi_tao = UserName;
                                        item.ngay_tao = DateTime.Now;
                                        db.Insert(item);
                                    }
                                    else
                                    {
                                        checkData.Remove(checkProDetail);
                                    }
                                }
                                if (checkData != null)
                                {
                                    foreach (Merchant_Product_Promotion_Detail item in checkData)
                                        db.Delete<Merchant_Product_Promotion_Detail>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1} and ma_san_pham = {2}", item.ma_chuong_trinh_km, item.ma_gian_hang, item.ma_san_pham);
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
        }*/
    }
}
