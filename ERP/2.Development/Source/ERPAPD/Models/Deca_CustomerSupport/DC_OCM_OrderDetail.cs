using System;
namespace ERPAPD.Models
{
    public class DC_OCM_OrderDetail
    {
        public string OrderID { get; set; }

        public DateTime CreatedOrderDate { get; set; }

        public decimal OrderStatus { get; set; }

        public decimal ProductID { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public string SKU { get; set; }

        public string Name { get; set; }

        public int UnitPrice { get; set; }

        public int Qty { get; set; }

        public int TotalAmt { get; set; }
    }
}
