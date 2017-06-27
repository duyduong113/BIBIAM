using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class CRM_Product
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string QCType { get; set; }
        public string Device { get; set; }
        public string Page { get; set; }
        public string Position { get; set; }
        public string Capacity { get; set; }
        public string Number { get; set; }
        public bool Status { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
    }

    public class CRM_Product_Price
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int Fk_Product_Id { get; set; }
        public string Fields { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
    }
}