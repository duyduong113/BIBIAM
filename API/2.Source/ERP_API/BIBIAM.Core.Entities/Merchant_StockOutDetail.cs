using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Merchant_StockOutDetail
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_phieu_xuat_kho { get; set; }
        public string ma_gian_hang { get; set; }
        public string ma_san_pham { get; set; }
        public string don_vi_tinh { get; set; }
        public int so_luong_yeu_cau { get; set; }
        public string ghi_chu { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime ngay_tao { get; set; }
        public string nguoi_cap_nhat { get; set; }
        public DateTime ngay_cap_nhat { get; set; }
        [Ignore]
        public string ten_san_pham { get; set; }
        [Ignore]
        public int stock_available { get; set; }
    }		

}
