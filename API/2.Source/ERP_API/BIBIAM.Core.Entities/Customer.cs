using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string ma_khach_hang { get; set; }
        public string ma_user { get; set; }
        public string email { get; set; }
        public string sdt { get; set; }
        public string hoten { get; set; }
        //[Ignore]
        //public string ten_address { get; set; }
        //[Ignore]
        //public string sdt_address { get; set; }
        //[Ignore]
        //public string email_address { get; set; }
        //[Ignore]
        //public string dia_chi { get; set; }
        //[Ignore]
        //public string ten_quan_huyen { get; set; }
        //[Ignore]
        //public string ten_thanh_pho { get; set; }
        [Ignore]
        public string ma_gian_hang { get; set; }


    }
    public class CustomerModelView
    {
        public string ma_khach_hang { set; get; }
        public string hoten{ set; get; }
        public string sdt { set; get; }
    }
    public class Merchant_Customer
    {
        public string ma_gian_hang { set; get; }
        public string ma_khach_hang { set; get; }
    }
}
