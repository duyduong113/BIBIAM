using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class CRM_Customer_Presenter
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Personal { get; set; }
        public string DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }
        public string YearOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string Note { get; set; }
        public string Gender { get; set; }
        public string OfficePhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string WorkAt { get; set; }
        public string ContactID { get; set; }
        public string LeadTypeID { get; set; }
        public DateTime? RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }
        [Ignore]
        public string[] customerArray { get; set; }
        [Ignore]
        public string CustomerID { get; set; }
        [Ignore]
        public string PersonalName { get; set; }
        [Ignore]
        public string listCustomer { get; set; }

    }

    public class CRM_CustomerLead_Generation
    {
        public string CustomerID { get; set; }
        public long PresenterID { get; set; }
        public DateTime? RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }
    }
}