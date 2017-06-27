using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Product_Price : BaseEntity
    {
        public string ma_gia_san_pham { get; set; }
        public string ma_san_pham { get; set; }
        public double gia_mua { get; set; }
        public double gia_ban_le { get; set; }
        public double gia_ban_si { get; set; }
        public double gia_khuyen_mai { get; set; }
        public double gia_luu_kho { get; set; }
        public DateTime? ngay_bat_dau { get; set; }
        public DateTime? ngay_ket_thuc { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime? ngay_duyet { get; set; }
        public string trang_thai_duyet { get; set; }
        public string nguoi_xuat_ban { get; set; }
        public DateTime? ngay_xuat_ban { get; set; }
        public string trang_thai_xuat_ban { get; set; }
        public string trang_thai { get; set; }
    }
}
