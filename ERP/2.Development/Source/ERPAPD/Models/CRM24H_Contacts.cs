using System;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class ERPAPD_Contacts
    {
        [AutoIncrement]
        public Int64 PKContactID { get; set; }
        public Int64 FKCustomer { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Vocative { get; set; }
        public DateTime? Birthday { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string TelephoneOffice { get; set; }

        public string TelephoneHome { get; set; }
        public string Email { get; set; }
        public string Preferences { get; set; }
        public string Classification { get; set; }
        public string Notes { get; set; }

        public int StaffId { get; set; }
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public string AsciiName { get; set; }
        public string UpperName { get; set; }

        public string Status { get; set; }
        public string FileName { get; set; }
        public string XmlData { get; set; }
        public Int64 FKAgency { get; set; }
        public DateTime? InputDate { get; set; }

        public string Label { get; set; }
        public int IsNew { get; set; }

        public DateTime? RowCreatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowUpdatedUser { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public string Category { get; set; }
        public int Decided { get; set; }
        public string CustomerID { get; set; }
        public string Gender { get; set; }
        [Ignore]
        public string DMYOfBirth { get; set; }
       [Ignore]
        public string COLUMN_NAME { get; set; }
        [Ignore]
        public string CustomerName { get; set; }
        public int newContact()
        {
            this.FKCustomer = 0;
            this.Name = "";
            this.Sex = "";
            this.Vocative = "";

            this.Birthday = DateTime.Parse("1900-01-01");
            this.Title = "";
            this.Address = "";
            this.Mobile = "";
            this.TelephoneOffice = "";

            this.TelephoneHome = "";
            this.Email = "";
            this.Preferences = "";
            this.Classification = "";
            this.Notes = "";

            this.StaffId = 0;
            this.UnitId = 0;
            this.DepartmentId = 0;
            this.AsciiName = "";
            this.UpperName = "";

            this.Status = "";
            this.FileName = "";
            this.XmlData = "";
            this.FKAgency = 0;
            this.InputDate = DateTime.Parse("1900-01-01");

            this.Label = "";
            this.IsNew = 0;

            this.RowCreatedAt = DateTime.Parse("1900-01-01");
            this.RowCreatedUser = "";
            this.RowUpdatedAt = DateTime.Parse("1900-01-01");
            this.RowUpdatedUser = "";
            this.FirstName = "";
            this.LastName = "";
            this.MiddleName = "";
            this.DayOfBirth = 01;
            this.MonthOfBirth = 01;
            this.YearOfBirth = 1900;
            this.Category = "";
            this.Decided = 0;
            return 0;
        }
}
     
}