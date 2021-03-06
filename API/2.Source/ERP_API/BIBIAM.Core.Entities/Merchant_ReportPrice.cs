﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class Merchant_ReportPriceHeader
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_gian_hang { get; set; }
        public string ma_bao_gia { get; set; }
        public string ten_khach_hang { get; set; }
        public string email { get; set; }
        public string so_dien_thoai { get; set; }
        public string nguoi_bao_gia { get; set; }
        public string dia_chi { get; set; }
        public DateTime ngay_bao_gia { get; set; }
        public string ghi_chu { get; set; }
    }
    public class Merchant_ReportPriceDetail
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_gian_hang { get; set; }
        public string ma_bao_gia { get; set; }
        public string ma_san_pham { get; set; }
        public string ten_san_pham { get; set; }
        public string don_vi_tinh { get; set; }
        public float don_gia { get; set; }
        public int so_luong { get; set; }
        public float thanh_tien { get; set; }
        public string ghi_chu { get; set; }
        [Ignore]
        public string mo_ta { get; set; }
        [Ignore]
        public string xuat_xu { get; set; }
        [Ignore]
        public string url { get; set; }

    }		

}
