using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class Customer_Address : BaseEntity
    {
        public string ma_user { get; set; }
        public string ma_khach_hang { get; set; }
        public string ten { get; set; }
        public string email { get; set; }
        public string sdt { get; set; }
        public string dia_chi { get; set; }
        public string quan_huyen { get; set; }
        public string tinh { get; set; }
        public int diachi_macdinh { get; set; }
        public int stt_diachi_phu { get; set; }
        [Ignore]
        public string CityName { get; set; }
    }
}
