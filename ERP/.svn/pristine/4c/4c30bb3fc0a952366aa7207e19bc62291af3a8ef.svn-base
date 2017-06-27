using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
namespace ERPAPD.Models
{

    public class Deca_Order_CancelTracking
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderID { get; set; }
        [Required(ErrorMessage = "Lý do là bắt buộc.")]
        [StringLength(128)]
        public string ReasonID { get; set; }
        [Required(ErrorMessage="Mô tả là bắt buộc.")]
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        [Ignore]
        [Required(ErrorMessage="Không tìm thấy đơn hàng.")]
        public string listOrderID { get; set; }
    }
}