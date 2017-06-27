using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class Property : BaseEntity
    {
        public string ma_thong_so { get; set; }
        public string loai { get; set; }
        public string ten_thong_so { get; set; }
        public bool trang_thai { get; set; }
        [Ignore]
        public string gia_tri_thong_so { get; set; }
        [Ignore]
        public string ma_thuoc_tinh { get; set; }
    }
}
