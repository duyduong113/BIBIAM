using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BIBIAM.Core.Entities
{
    public class Product_Hierarchy_Property: BaseEntity
    {
        public string ma_cay_phan_cap { get; set; }
        public string ma_thong_so { get; set; }
        public bool trang_thai { get; set; }
        [Ignore]
        public string loai { get; set; }
        [Ignore]
        public string ten_thong_so { get; set; }
        [Ignore]
        public string ten_phan_cap { get; set; }
        [Ignore]
        public List<Property_Detail> detail { get; set; }
    }
}
