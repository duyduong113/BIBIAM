using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Merchant_Info : BaseEntity
    {
        public string ma_gian_hang { get; set; }
        public string ten_gian_hang { get; set; }
        public string ten_viet_tat { get; set; }
        public string ten_tieng_anh { get; set; }
        public string logo_gian_hang { get; set; }
        public string website { get; set; }
        public string dien_thoai { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string dia_chi_tru_so { get; set; }
        public string ma_tinh_tp { get; set; }
        public string ten_tinh_tp { get; set; }
        public string ma_quan_huyen { get; set; }
        public string ten_quan_huyen { get; set; }
        public DateTime? ngay_tiep_xuc { get; set; }
        public DateTime? ngay_ky_hop_dong { get; set; }
        public DateTime? ngay_hoat_dong { get; set; }
        public string ly_do_giay_to_phap_ly { get; set; }
        public string tai_khoan_thanh_toan { get; set; }
        public string dao_tao_quan_ly { get; set; }
        public string trang_thai_duyet { get; set; }
        public string trang_thai_xuat_ban { get; set; }
        public string trang_thai_xac_thuc { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime? ngay_duyet { get; set; }
        public string mo_ta { get; set; }
        public string slug { get; set; }
        [Ignore]
        public Merchant_Info history { get; set; }
    }
}
