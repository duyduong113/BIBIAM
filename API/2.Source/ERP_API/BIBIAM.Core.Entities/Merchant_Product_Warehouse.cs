using ServiceStack.DataAnnotations;
using System;

namespace BIBIAM.Core.Entities
{
    public class Merchant_Product_Warehouse: BaseEntity
    {
        public string ma_san_pham { get; set; }
        public string ma_gian_hang { get; set; }
        public int stock_available { get; set; }
        public int stock_onhand { get; set; }
        public int book_available { get; set; }
    }
}
