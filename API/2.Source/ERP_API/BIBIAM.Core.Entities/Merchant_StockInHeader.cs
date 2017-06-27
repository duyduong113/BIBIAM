using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Merchant_StockInHeader : BaseEntity
    {
        public string ma_phieu_nhap_kho { get; set; }
        public string ma_kho { get; set; }
        public string ma_don_hang { get; set; }
        public string ma_gian_hang { get; set; }
        public string dia_diem { get; set; }
        public DateTime? ngay_nhap_kho { get; set; }
        public string nguoi_giao { get; set; }
        public string nguoi_kiem_tra { get; set; }
        public string thu_kho { get; set; }
        public string ghi_chu { get; set; }
        public string trang_thai { get; set; }
    }		
}
