using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class Merchant_Product : BaseEntity
    {
        public string ma_san_pham { get; set; }
        public string ma_loai_san_pham { get; set; }
        public string ma_gian_hang { get; set; }
        public string ten_san_pham { get; set; }
        public string part_no { get; set; }
        public string mo_ta { get; set; }
        public string noi_dung { get; set; }
        public int trong_so { get; set; }
        public double khoi_luong { get; set; }
        public float don_gia { get; set; }
        public float gia_si { get; set; }
        public string tu_khoa { get; set; }
        public string tag { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string xuat_xu { get; set; }
        public string thuong_hieu { get; set; }
        
        public string model { get; set; }
        public int minimum_order { get; set; }
        public string catalog { get; set; }
        public string nguoi_xuat_ban { get; set; }
        public DateTime? ngay_xuat_ban { get; set; }
        public string trang_thai_xuat_ban { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime? ngay_duyet { get; set; }
        public string trang_thai_duyet { get; set; }
        public string trang_thai { get; set; }
        public string ly_do_tu_choi_duyet { get; set; }
        [Ignore]
        public string ten_thuong_hieu { get; set; }
        [Ignore]
        public string nganh_hang { get; set; }
        [Ignore]
        public string ten_gian_hang { get; set; }

        [Ignore]
        public string ten_trang_thai { get; set; }
        [Ignore]
        public string ten_trang_thai_duyet { get; set; }
        [Ignore]
        public List<Property_Detail> detail { get; set; }
    }
    public class Json_Product
    {
        public string ma_san_pham { get; set; }
        public string ten_san_pham { get; set; }
    }
}
