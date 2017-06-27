using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class CRM_CustomerAccountant
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Presenter { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string BankAccount { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }


    }
}