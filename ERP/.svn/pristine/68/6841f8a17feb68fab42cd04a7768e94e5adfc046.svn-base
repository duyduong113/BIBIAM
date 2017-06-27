using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Company_Management
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(20)]
        public string CompanyID { get { return "CM" + String.Format("{0:0000000}", Id); } }
        [StringLengthAttribute(255)]
        [Index(Unique = true)]
        public string Name { get; set; }
        [StringLengthAttribute(255)]
        public string Address { get; set; }
        [StringLengthAttribute(20)]
        public string Tax { get; set; }
        [StringLengthAttribute(20)]
        public string Phone { get; set; }
        [StringLengthAttribute(20)]
        public string VAT_Type { get; set; }
        [StringLengthAttribute(20)]
        public string VAT_Symbol { get; set; }
        [StringLengthAttribute(20)]
        public string VAT_No { get; set; }
        [StringLengthAttribute(20)]
        public string PXK_Type { get; set; }
        [StringLengthAttribute(20)]
        public string PXK_Symbol { get; set; }
        [StringLengthAttribute(20)]
        public string PXK_No { get; set; }
        [StringLengthAttribute(20)]
        public string Delivery_No { get; set; }
        public bool Active { get; set; }
        [StringLengthAttribute(20)]
        public string RowCreatedUser { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(20)]
        public string RowLastUpdatedUser { get; set; }
        public DateTime? RowLastUpdatedTime { get; set; }
    }

    public class DC_Company_Warehouse
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(DC_Company_Management))]
        public int CompanyID { get; set; }
        [StringLengthAttribute(20)]
        [Index(Unique = true)]
        public string WarehouseID { get; set; }
        [StringLengthAttribute(20)]
        public string RowCreatedUser { get; set; }
        public DateTime RowCreatedTime { get; set; }

    }
}