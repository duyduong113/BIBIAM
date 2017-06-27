using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_OCM_Create_Order
    {
        public string p_id_nguoi_mua_hang { get; set; }
        public int p_id_gian_hang { get; set; }
        public Dictionary<string, DC_OCM_Item> san_pham { get; set; }
        public int p_id_tinh { get; set; }
        public int p_id_quan_huyen { get; set; }
        public int p_so_luong_san_pham { get; set; }
        public string p_loai_nt { get; set; }
        public string p_ten_nguoi_nhan_hang { get; set; }
        public string p_dia_chi_nhan_hang { get; set; }
        public string p_dien_thoai_nhan_hang { get; set; }
        public string p_ten_nguoi_nhan_hang_khong_dau { get; set; }
        public string p_dia_chi_nhan_hang_khong_dau { get; set; }
        public string p_dien_thoai_di_dong_chuan { get; set; }
        public int p_id_cong_thanh_toan { get; set; }
        public string p_tai_khoan_thanh_toan { get; set; }
        public string ma_ngan_hang { get; set; }
        public int p_loai_thanh_toan { get; set; }
        public double gia_tri_thanh_toan { get; set; }
        public string code_token { get; set; }
        public string ma_giao_dich { get; set; }
        public string ngay_giao_dich { get; set; }
        public string api_key { get; set; }
        public string api_secret { get; set; }
        public int goi_tra_gop { get; set; }
        public int trang_thai_thanh_toan { get; set; }
        public string ghi_chu { get; set; }
    }

    public class DC_OCM_Item
    {
        public double price { get; set; }
        public int number { get; set; }
        public string code_price { get; set; }
    }

    public class DC_Order_Response
    {
        public string response_code { get; set; }
        public string error_message { get; set; }
        public string code_order { get; set; }
        public string message { get; set; }
    }

    public class DC_OCM_Update
    {
        public string ma_don_hang { get; set; }
        public string code_token { get; set; }
        public string ma_giao_dich { get; set; }
        public string ngay_giao_dich { get; set; }
        public string p_tai_khoan_thanh_toan { get; set; }
        public string ma_ngan_hang { get; set; }
        public int goi_tra_gop { get; set; }
        public int p_id_cong_thanh_toan { get; set; }
        public int p_loai_thanh_toan { get; set; }
        public string api_key { get; set; }
        public string api_secret { get; set; }
        public int trang_thai_don_hang { get; set; }
        public int id_ly_do_huy { get; set; }
    }

    public class DC_OCM_Create_Order_Log
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DC_OCM_Create_Order log { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
    }

    public class DC_OCM_Update_Log
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DC_OCM_Update log { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(50)]
        public string CreatedBy { get; set; }
    }
}