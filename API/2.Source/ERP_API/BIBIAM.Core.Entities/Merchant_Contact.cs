using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Merchant_Contact: BaseEntity
    {
        public string ma_gian_hang { get; set; }
        public string ten_gian_hang { get; set; }
        public string ten_nguoi_lien_he { get; set; }
        public string danh_xung { get; set; }
        public string email { get; set; }
        public string so_dien_thoai { get; set; }
        public string chuc_vu { get; set; }
        public Boolean quan_tri_gian_hang { get; set; }
        public string ghi_chu { get; set; }
        [Ignore]
        public string logo_gian_hang { get; set; }
    }
}