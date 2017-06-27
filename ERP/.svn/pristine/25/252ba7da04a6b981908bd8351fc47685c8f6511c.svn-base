using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class Deca_Customer_LastShippingAddress
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(50)]
        public string CustomerID { get; set; }
        [StringLengthAttribute(50)]
        public string PhysicalID { get; set; }
        [StringLength(1024)]
        [Required]
        public string Address { get; set; }
        [StringLengthAttribute(64)]
        public string CityID { get; set; }
        [StringLengthAttribute(64)]
        public string DistrictID { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
    }
}