using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
namespace ERPAPD.Models
{

    public class Deca_Order_Status
    {

        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
        [Required]
        [StringLength(5)]
        public string RefID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
    }
}