using System;
using ServiceStack.DataAnnotations;
namespace ERPAPD.Models
{
    public class tien_te
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string ma_tien_te { get; set; }
        public string ten_tien_te { get; set; }
        public string quoc_gia { get; set; }
        public string ki_hieu_tien_te { get; set; }
        public bool trang_thai { get; set; }

    }
    public class ti_gia_tien_te
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string ma_tien_te { get; set; }
        public double ti_le_chuyen_doi { get; set; }
        public bool trang_thai { get; set; }
        public bool tien_te_mac_dinh { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }
        [Ignore]
        public string ten_tien_te { get; set; }
        [Ignore]
        public string ki_hieu_tien_te { get; set; }
        [Ignore]
        public string quoc_gia { get; set; }
    }
    public class ti_gia_quy_doi
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string ma_tien_te { get; set; }
        public int don_vi_quy_doi { get; set; }
        public double so_tien_quy_doi { get; set; }
        public DateTime? ngay_quy_doi { get; set; }
        public string nguoi_quy_doi { get; set; }
        public string ChangeFrom { get; set; }
    }
}