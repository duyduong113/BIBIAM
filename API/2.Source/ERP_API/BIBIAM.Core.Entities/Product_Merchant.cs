using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Product_Merchant: BaseEntity
    {
        public string ma_san_pham { get; set; }
        public string ma_loai_san_pham { get; set; }
        public string ten_san_pham { get; set; }
        public string ma_gian_hang { get; set; }
        public string mo_ta { get; set; }
        public int trong_so { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime ngay_duyet { get; set; }
        public string trang_thai_duyet { get; set; }
        public string trang_thai { get; set; }
        [Ignore]
        public string ten_loai_san_pham { get; set; }
        [Ignore]
        public string ten_gian_hang { get; set; }
    }
}
