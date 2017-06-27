using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace BIBIAM.Core.Entities
{
    public class Merchant_Product_Promotion: BaseEntity
    {
        public string ma_chuong_trinh_km { get; set; }
        public string ma_gian_hang { get; set; }
        public string ten_chuong_trinh_km { get; set; }
        public string loai { get; set; }
        public float gia_tri { get; set; }
        public DateTime? ngay_bat_dau { get; set; }
        public DateTime? ngay_ket_thuc { get; set; }
        public string trang_thai { get; set; }
    }
}
