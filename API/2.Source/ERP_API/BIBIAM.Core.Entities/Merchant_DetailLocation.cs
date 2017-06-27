using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class Merchant_DetailLocation : BaseEntity
    {
        public string ma_kho { get; set; }
        public string ma_gian_hang { get; set; }
        public string ten_kho { get; set; }
        public string ten_vi_tri { get; set; }
        public string ma_san_pham { get; set; }
        public int so_luong { get; set; }
        public string don_vi_tinh { get; set; }
    }
}
