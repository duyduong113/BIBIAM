using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Product_Info: BaseEntity
    {
        public string ma_san_pham { get; set; }
        public string ma_loai_san_pham { get; set; }
        public string ten_san_pham { get; set; }
        public string mo_ta { get; set; }
        public string tu_khoa { get; set; }
        public string tag { get; set; }
        public string slug { get; set; }
        public int trong_so { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime? ngay_duyet { get; set; }
        public string trang_thai_duyet { get; set; }
        public string nguoi_xuat_ban { get; set; }
        public DateTime? ngay_xuat_ban { get; set; }
        public string trang_thai_xuat_ban { get; set; }
        public string url { get; set; }
        public string trang_thai { get; set; }
        [Ignore]
        public string ten_trang_thai { get; set; }
        [Ignore]
        public string ten_trang_thai_duyet { get; set; }
    }
}
