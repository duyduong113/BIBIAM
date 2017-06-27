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
    public class Merchant_StockInHeader_DAO
    {
        public string Upsert(Merchant_StockInHeader data, List<Merchant_StockInDetail> listGrid, List<Merchant_StockInDetail> lstImport, bool trang_thai, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    var checkID = db.SingleOrDefault<Merchant_StockInHeader>("ma_phieu_nhap_kho={0} and ma_gian_hang = {1}", data.ma_phieu_nhap_kho, data.ma_gian_hang);
                    try
                    {
                        if (checkID != null)// cap nhat merchant_stockin_header
                        {
                            if (checkID.trang_thai == AllConstant.trang_thai_duyet.DA_DUYET)
                            {
                                dbTrans.Rollback();
                                return "Phiếu nhập kho này đã được duyệt không được cập nhật nữa!";
                            }
                            checkID.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            checkID.ngay_nhap_kho = DateTime.Now;
                            checkID.nguoi_giao = data.nguoi_giao;
                            checkID.ma_kho = data.ma_kho;
                            checkID.thu_kho = data.thu_kho;
                            checkID.ghi_chu = data.ghi_chu;
                            checkID.dia_diem = data.dia_diem;
                            db.Update(checkID);
                        }
                        else// tao moi merchant_stockin_header
                        {
                            var lastId = db.FirstOrDefault<Merchant_StockInHeader>("SELECT TOP 1 * FROM Merchant_StockInHeader ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_phieu_nhap_kho.Contains("GRN"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_phieu_nhap_kho.Substring(3, 10)) + 1;
                                    data.ma_phieu_nhap_kho = "GRN" + String.Format("{0:0000000000}", nextNo);
                                }
                            }
                            else
                            {
                                data.ma_phieu_nhap_kho = "GRN" + "0000000001";
                            }
                            data.ngay_nhap_kho = DateTime.Now;
                            data.ngay_tao = DateTime.Now;
                            data.nguoi_tao = UserName;
                            data.ngay_cap_nhat = DateTime.Parse("1900-01-01");
                            data.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                            db.Insert(data);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        int count = 0;
                        if (listGrid != null) // kiem tra data trong listGrid xem co gi ko
                        {
                            var checkData = db.Select<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_nhap_kho, data.ma_gian_hang);// data ban đầu
                            if (checkData.Count == 0)// trong merchant_stockin_detail chua co data => them moi tất cả
                            {
                                foreach (Merchant_StockInDetail item in listGrid)
                                {
                                    var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == item.ma_san_pham);
                                    if (checkProDetail == null)// kiem tra xem sản phẩm này đã được thêm mới chưa
                                    {
                                        //item.id = 0;
                                        item.ma_phieu_nhap_kho = data.ma_phieu_nhap_kho;
                                        item.ma_gian_hang = data.ma_gian_hang;
                                        item.vi_tri = !string.IsNullOrEmpty(item.vi_tri) ? item.vi_tri : "";
                                        item.nguoi_tao = UserName;
                                        item.ngay_tao = DateTime.Now;
                                        item.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                                        db.Insert(item);
                                    }
                                }
                            }
                            else// trong merchant_stockin_detail da tồn tại data => cap nhật cái đã tồn tại
                            {
                                foreach (Merchant_StockInDetail item in listGrid)
                                {
                                    item.vi_tri = !string.IsNullOrEmpty(item.vi_tri) ? item.vi_tri : "";
                                    var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == item.ma_san_pham);// check với data ban đầu
                                    if (checkProDetail == null) // cái chưa tồn tại => thêm mới
                                    {
                                        var checkProDetailDatabase = db.Select<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1} and ma_san_pham = {2}", data.ma_phieu_nhap_kho, data.ma_gian_hang,item.ma_san_pham);// data đã thay dổi
                                        if (checkProDetailDatabase == null)// check với data đã thay dổi
                                        {

                                            //item.id = 0;
                                            item.ma_phieu_nhap_kho = data.ma_phieu_nhap_kho;
                                            item.ma_gian_hang = data.ma_gian_hang;
                                            item.nguoi_tao = UserName;
                                            item.ngay_tao = DateTime.Now;
                                            item.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                                            db.Insert(item);
                                        }
                                    }
                                    else // cái đã tồn tại => cập nhật
                                    {
                                        if(item.id>0)//kiểm tra them san pham moi mà sản phẩm đã tồn tại => ko cho phép thêm mới
                                        {
                                            //checkProDetail.vi_tri = !string.IsNullOrEmpty(item.vi_tri) ? item.vi_tri : "";
                                            checkProDetail.ghi_chu = !string.IsNullOrEmpty(item.vi_tri) ? checkProDetail.vi_tri : "";

                                            checkProDetail.so_luong_yeu_cau = item.so_luong_yeu_cau;
                                            checkProDetail.so_luong_thuc_te = item.so_luong_thuc_te;

                                            checkProDetail.don_vi_tinh = item.don_vi_tinh;
                                            checkProDetail.nguoi_cap_nhat = UserName;
                                            checkProDetail.ngay_cap_nhat = DateTime.Now;
                                            checkProDetail.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                                            db.Update(checkProDetail);

                                            checkData.Remove(checkProDetail);
                                        }
                                    }
                                }
                                if (checkData != null)
                                {
                                    foreach (Merchant_StockInDetail item in checkData)
                                        db.Delete<Merchant_StockInDetail>("id = {0}", item.id);
                                }
                            }
                        }
                        //else // nếu trong listGrid k có data thì xóa tất cả record đang có trong database merchant_stockin_detail
                        //{
                        //    db.Delete<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_nhap_kho, data.ma_gian_hang);
                        //}

                        if (lstImport != null) // kiểm tra lstImport có data ko
                        {
                            var checkData = db.Select<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_nhap_kho, data.ma_gian_hang);
                            if (checkData.Count == 0) // kiểm tra xem trong merchant_stockin_detail ko thấy có data => thêm mới toàn bộ
                            {
                                foreach (Merchant_StockInDetail item in lstImport)
                                {
                                    item.id = 0;
                                    item.ma_phieu_nhap_kho = data.ma_phieu_nhap_kho;
                                    item.ma_gian_hang = data.ma_gian_hang;
                                    item.vi_tri = !string.IsNullOrEmpty(item.vi_tri) ? item.vi_tri : "";
                                    item.nguoi_tao = UserName;
                                    item.ngay_tao = DateTime.Now;
                                    item.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                                    db.Insert(item);
                                    count++;
                                }
                            }
                            else // thấy trong merchant_stockin_detail có data => thêm mới + cập nhật
                            {
                                foreach (Merchant_StockInDetail item in lstImport)
                                {
                                    item.vi_tri = !string.IsNullOrEmpty(item.vi_tri) ? item.vi_tri : "";
                                    var checkProDetail = checkData.FirstOrDefault(s => s.ma_san_pham == item.ma_san_pham);
                                    if (checkProDetail == null)// sản phẩm này chưa có => thêm mới
                                    {
                                        item.id = 0;
                                        item.ma_phieu_nhap_kho = data.ma_phieu_nhap_kho;
                                        item.ma_gian_hang = data.ma_gian_hang;
                                        item.nguoi_tao = UserName;
                                        item.ngay_tao = DateTime.Now;
                                        item.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                                        db.Insert(item);
                                        count++;
                                    }
                                    else // sản phẩm đã có => (UPDATE) cộng dồn số lượng
                                    {
                                        checkProDetail.so_luong_thuc_te = checkProDetail.so_luong_thuc_te + item.so_luong_thuc_te;
                                        checkProDetail.so_luong_yeu_cau = checkProDetail.so_luong_yeu_cau + item.so_luong_yeu_cau;
                                        checkProDetail.ghi_chu= !string.IsNullOrEmpty(item.ghi_chu) ? item.ghi_chu : checkProDetail.ghi_chu;

                                        checkProDetail.don_vi_tinh = item.don_vi_tinh;
                                        checkProDetail.nguoi_cap_nhat = UserName;
                                        checkProDetail.ngay_cap_nhat = DateTime.Now;
                                        checkProDetail.trang_thai = trang_thai ? AllConstant.trang_thai_duyet.DA_DUYET : AllConstant.trang_thai_duyet.NHAP;
                                        db.Update(checkProDetail);
                                        count++;
                                        checkData.Remove(checkProDetail);
                                    }
                                }
                                if (checkData != null)
                                {
                                    foreach (Merchant_StockInDetail item in checkData)
                                        db.Delete<Merchant_StockInDetail>("id = {0}", item.id);
                                }
                            }
                        }

                        if(listGrid==null && lstImport ==null)// xóa hết tất cả data trong Merchant_StockInDetail khi cả 2 list data k có data nào
                            db.Delete<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_nhap_kho, data.ma_gian_hang);

                        //else
                        //{
                        //    db.Delete<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1}", data.ma_phieu_nhap_kho, data.ma_gian_hang);
                        //}

                        dbTrans.Commit();
                        return "true@@" + data.ma_phieu_nhap_kho + "@@" + count.ToString();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return "false@@" + data.ma_phieu_nhap_kho + " " + ex.Message;
                    }
                }
            }
        }

        public string UpdateInventory_StockIN(string ma_phieu_nhap_kho)
        {
            try
            {
                List<SqlParameter> lstParameter = new List<SqlParameter>();
                lstParameter.Clear();
                lstParameter.Add(new SqlParameter("@ma_phieu_nhap_kho", ma_phieu_nhap_kho));
                new SqlHelper().ExecuteNoneQuery("p_Merchant_Product_Warehouse_StockIn_SyncToMySQL", lstParameter);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateInventoryMySQL(string ma_phieu_nhap_kho, string ma_gian_hang, string username, string connectionString)
        {
            List<Merchant_StockInDetail> list = new List<Merchant_StockInDetail>();
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                list = db.Select<Merchant_StockInDetail>("ma_phieu_nhap_kho = {0} and ma_gian_hang = {1} and trang_thai = 'Aproved'".Params(ma_phieu_nhap_kho, ma_gian_hang));
            }
            if (list.Count > 0)
                using (MySqlConnection con = new MySqlConnection(AppConfigs.FEConnectionString))
                {
                    con.Open();
                    using (MySql.Data.MySqlClient.MySqlTransaction trans = con.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand("", con, trans))
                            {
                                foreach (Merchant_StockInDetail item in list)
                                {
                                    cmd.CommandText = "select * from Merchant_Inventory where ma_san_pham={0} and ma_gian_hang={1}".Params(item.ma_san_pham, item.ma_gian_hang);
                                    var rs = cmd.ExecuteReader();
                                    if (!rs.Read())
                                        cmd.CommandText = @"insert into Merchant_Inventory(ma_gian_hang, ma_san_pham, so_luong_kho, nguoi_tao, so_luong_ban, ngay_tao, ngay_cap_nhat, nguoi_cap_nhat) 
                                                       values({0},{1},{2},{3},0,NOW(),null,null)".Params(item.ma_gian_hang, item.ma_san_pham, item.so_luong_thuc_te, username);
                                    else
                                        cmd.CommandText = @"update Merchant_Inventory set so_luong_kho = so_luong_kho + {0}, ngay_cap_nhat = NOW(), nguoi_cap_nhat = {1} where ma_gian_hang = {2} and ma_san_pham = {3}".Params(item.so_luong_thuc_te, username, item.ma_gian_hang, item.ma_san_pham);
                                    rs.Close();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            trans.Commit();
                            return "true";
                        }
                        catch (Exception e)
                        {
                            trans.Rollback();
                            return e.Message.ToString();
                        }
                    }
                }
            return "Không tìm thấy thông tin Phiếu Nhập Kho";
        }
    }
}