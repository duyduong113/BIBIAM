﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BIBIAM.Core.Entities
{
    public class Product_Property : BaseEntity
    {
        public string ma_san_pham { get; set; }
        public string ma_gian_hang { get; set; }
        public string ma_loai_san_pham { get; set; }
        public string ma_thong_so { get; set; }
        public string ten_thong_so { get; set; }
        public string gia_tri_thong_so { get; set; }
        public DateTime den_ngay { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime ngay_duyet { get; set; }
        public string trang_thai_duyet { get; set; }
        public string trang_thai { get; set; }
        [Ignore]
        public string loai { get; set; }
    }
}