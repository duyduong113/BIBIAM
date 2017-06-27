using ServiceStack.DataAnnotations;
using System;


namespace CMS.Models
{
    public class cms_Merchant_Folder_Info
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_gian_hang { get; set; }
        public string ten_thu_muc { get; set; }
        public string trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }
    }
}
