using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class cms_PostNotify
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ten { get; set; }
        public string noi_dung { get; set; }
        public Boolean trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }
        public Boolean allUser { get; set; }
        public Boolean isDefault { get; set; }
        public string hashtagcode { get; set; }
        public DateTime ngay_thong_bao { get; set; }
        public string ma_website { get; set; }

    }

}







