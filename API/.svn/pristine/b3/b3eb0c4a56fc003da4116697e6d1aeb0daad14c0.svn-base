using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Merchant_OrderHeader : BaseEntity
    {
        public string ma_don_hang { get; set; }
        public string ma_don_hang_cha { get; set; }
        public string ma_gian_hang { get; set; }
        public string ly_do_huy { get; set; }
        public string ma_khach_hang { get; set; }
        public string hoten { get; set; }
        public string dia_chi_giao_hang { get; set; }
        public string quan_huyen_giao { get; set; }
        public string tinh_thanh_giao { get; set; }
        public string so_dien_thoai_giao { get; set; }
        public float so_loai_san_pham { get; set; }
        public float so_luong_san_pham { get; set; }
        public float so_luong_khuyen_mai { get; set; }
        public float tong_tien_don_hang { get; set; }
        public float tong_tien_khuyen_mai { get; set; }
        public float tong_tien_thanh_toan { get; set; }
        public string hinh_thuc_thanh_toan { get; set; }
        public string trang_thai_don_hang { get; set; }
        public DateTime ngay_xac_nhan { get; set; }
        public DateTime ngay_hen_giao_hang { get; set; }
        public DateTime ngay_lay_hang { get; set; }
        public DateTime ngay_giao_hang { get; set; }
        [Ignore]
        public string ten_quan_huyen { get; set; }
        [Ignore]
        public string ten_tinh_thanh { get; set; }

    }		

    public class Merchant_OrderDetail : BaseEntity
    {
        public string ma_don_hang { get; set; }
        public string ma_gian_hang { get; set; }
        public string ma_san_pham { get; set; }
        public string ten_san_pham { get; set; }
        public float don_gia { get; set; }
        public int so_luong { get; set; }
        public float khuyen_mai { get; set; }
        public float thanh_tien { get; set; }
        public int sp_khuyen_mai { get; set; }
        [Ignore]
        public int book_available { get; set; }
    }		
    public class location_district: BaseEntity
    {
        public string ma_quan_huyen { get; set; }
        public string ten_quan_huyen { get; set; }
        public string ma_thanh_pho { get; set; }
        public string trang_thai { get; set; }
    }
    public class location_city: BaseEntity
    {
        public string ma_thanh_pho { get; set; }
        public string ten_thanh_pho { get; set; }
        public string ma_vung { get; set; }
        public string trang_thai { get; set; }
    }
}